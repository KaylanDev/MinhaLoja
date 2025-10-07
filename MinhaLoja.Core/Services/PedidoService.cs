using FluentResults;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Domain.Models.Enums;
using MongoDB.Bson;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace MinhaLoja.Core.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly ICarrinhoService _carrinhoService;
        private readonly IProdutoService _produtoService;
        public PedidoService(IPedidoRepository pedidoRepository, ICarrinhoService carrinhoService, IProdutoService produtoService)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoService = carrinhoService;
            _produtoService = produtoService;
        }

        public async Task<Result> CancelarPedido(ObjectId usuarioId, ObjectId pedidoId)
        {
          var pedido = _pedidoRepository.GetByIdAsync(pedidoId).Result;
            if (pedido == null)
            {
                return Result.Fail("Pedido não encontrado");
            }
            if (pedido.UsuarioId != usuarioId)
            {
                return Result.Fail("Usuário não autorizado a cancelar este pedido");
            }
            if (pedido.Status <= Status.Processando)
            {
                return Result.Fail("Somente pedidos em processamento podem ser cancelados");
            }
            pedido.Status = Status.Cancelado;
            var updateResult = await _pedidoRepository.UpdateAsync(pedido);
            if (updateResult == null)
            {
                return Result.Fail("Falha ao atualizar o pedido");
            }
            // Repor o estoque dos produtos
            foreach (var item in pedido.Itens)
            {
                var produtoResult = await _produtoService.ObterPorName(item.NomeProduto);
                if (produtoResult.IsFailed)
                {
                    return Result.Fail("Falha ao obter o produto para atualização de estoque: " + string.Join(", ", produtoResult.Errors.Select(e => e.Message)));
                }
                var produto = produtoResult.Value;
                produto.Estoque += item.Quantidade;
                var updateProdutoResult = await _produtoService.Atualizar(produto);
                if (updateProdutoResult.IsFailed)
                {
                    return Result.Fail("Falha ao atualizar o estoque do produto: " + string.Join(", ", updateProdutoResult.Errors.Select(e => e.Message)));
                }
            }
            return Result.Ok();
        }

        public async Task<Result<Pedido>> CriarPedidoAsync(ObjectId usuarioId)
        {
            var carrinhoResult = await _carrinhoService.ObterCarrinhoDoUsuarioAsync(usuarioId);
            if (carrinhoResult.IsFailed)
            {
                Result.Fail("Falha ao obter o carrinho do usuário: " + string.Join(", ", carrinhoResult.Errors.Select(e => e.Message)));
            }
            var carrinho = carrinhoResult.Value;
            if (carrinho.Itens.Count == 0)
            {
                return Result.Fail("O carrinho está vazio.");
            }

            // Verificar estoque para cada item no carrinho
            foreach (var item in carrinho.Itens)
            {
                var produtoResult = await _produtoService.ObterPorId(item.ProdutoId);
                if (produtoResult.IsFailed)
                {
                    return Result.Fail("Falha ao obter o produto: " + string.Join(", ", produtoResult.Errors.Select(e => e.Message)));
                }
                var produto = produtoResult.Value;
                if (item.Quantidade > produto.Estoque)
                {
                    return Result.Fail($"Quantidade insuficiente em estoque para o produto {produto.Nome}. Disponível: {produto.Estoque}, Solicitado: {item.Quantidade}");
                }
            }

            // Criar o pedido
            var pedido = new Pedido
            {
                UsuarioId = usuarioId,
                Itens = carrinho.Itens.Select(i => new ItemPedido
                {
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario,
                    NomeProduto = i.Produto.Nome ?? "Produto Desconhecido"

                }).ToList(),
                DataPedido = DateTime.UtcNow,
                Status = Domain.Models.Enums.Status.Processando
            };
            foreach (var item in pedido.Itens)
            {
                pedido.Total += item.PrecoUnitario * item.Quantidade;
            }

            var pedidoResult = await _pedidoRepository.CreateAsync(pedido);
            if (pedidoResult is null)
            {
                return Result.Fail("Falha ao criar o pedido");
            }
            // Atualizar o estoque dos produtos
            foreach (var item in carrinho.Itens)
            {
                var produtoResult = await _produtoService.ObterPorId(item.ProdutoId);
                if (produtoResult.IsFailed)
                {
                    return Result.Fail("Falha ao obter o produto para atualização de estoque: " + string.Join(", ", produtoResult.Errors.Select(e => e.Message)));
                }
                var produto = produtoResult.Value;
                produto.Estoque -= item.Quantidade;
                var updateResult = await _produtoService.Atualizar(produto);
                if (updateResult.IsFailed)
                {
                    return Result.Fail("Falha ao atualizar o estoque do produto: " + string.Join(", ", updateResult.Errors.Select(e => e.Message)));
                }
            }
            // Limpar o carrinho
            var carrinhoUpdate = await _carrinhoService.LimparCarrinhoAsync(usuarioId);
            if (carrinhoUpdate.IsFailed)
            {
                return Result.Fail("Falha ao limpar o carrinho: " + string.Join(", ", carrinhoUpdate.Errors.Select(e => e.Message)));
            }

            return Result.Ok(pedidoResult);

        }

        public async Task<Result> MarcarComoEntregueAsync(string pedidoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(new ObjectId(pedidoId));

            if (pedido.Status != Status.Enviado)
            {
                return Result.Fail("Somente pedidos enviados podem ser marcados como entregues");
            }

            pedido.Status = Status.Entregue;
            var updateResult = await _pedidoRepository.UpdateAsync(pedido);
            if (updateResult == null)
            {
                return Result.Fail("Falha ao atualizar o pedido");
            }
            return Result.Ok();

        }

        public async Task<Result> MarcarComoEnviadoAsync(ObjectId pedidoId, string codigoRastreio)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);

            if (pedido == null)
            {
                return Result.Fail("Pedido não encontrado");
            }
            if (pedido.Status <= Status.AguardandoEnvio)
            {
                return Result.Fail("Somente pedidos em aguardando envio podem ser marcados como enviados");
            }
            pedido.Status = Status.Enviado;
            // implementar codigo de rastreio 
            //pedido.CodigoRastreio = codigoRastreio;
            var updateResult = await _pedidoRepository.UpdateAsync(pedido);
            if (updateResult == null)
            {
                return Result.Fail("Falha ao atualizar o pedido");
            }
            return Result.Ok(); 

        }

        public async Task<Result> MarcarComoPagoParaEnvioAsync(ObjectId pedidoId, string transacaoId)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);

            if (pedido == null)
            {
                return Result.Fail("Pedido não encontrado");
            }
            if (pedido.Status != Status.Processando)
            {
                return Result.Fail("Somente pedidos em Processando podem ser marcados como Aguardando envio");
            }
            pedido.Status = Status.AguardandoEnvio;
            // implementar codigo de rastreio 
            //pedido.CodigoRastreio = codigoRastreio;
            var updateResult = await _pedidoRepository.UpdateAsync(pedido);
            if (updateResult == null)
            {
                return Result.Fail("Falha ao atualizar o pedido");
            }
            return Result.Ok();

        }

        public async Task<Result<Pedido>> ObterPorIdAsync(string pedidoId)
        {
            var pedido = _pedidoRepository.GetByIdAsync(new ObjectId(pedidoId));
            if (pedido == null)
            {
                return Result.Fail("Pedido não encontrado");
            }

            return Result.Ok(pedido.Result);
        }

        public async Task<Result<IEnumerable<Pedido>>> ObterPorUsuarioIdAsync(string usuarioId)
        {
            var pedidos = await _pedidoRepository.ObterPedidoPorUserId(new ObjectId(usuarioId));

            if (pedidos is null)
            {
                return Result.Fail("Nenhum pedido encontrado para este usuário");
            }

            return Result.Ok(pedidos);
        }

        public async Task<Result<IEnumerable<Pedido>>> ObterTodosParaAdminAsync(string status)
        {
            var pedidos = await _pedidoRepository.GetAllAsync();

            if (String.IsNullOrEmpty(status))
            {
                return Result.Fail("insira o status");
            }

            if (pedidos is null)
            {
                return Result.Fail("Nenhum pedido encontrado");
            }

            if (status is "Todos")
            {
                IEnumerable < Pedido > p = pedidos;
                return Result.Ok(p);
            }

            if (Enum.TryParse<Status>(status, out var statusEnum))
            {
                var pedidosFiltrados = pedidos.Where(p => p.Status == statusEnum);
                return Result.Ok(pedidosFiltrados);
            }
            else
            {
                return Result.Fail("Status inválido");
            }
        }
    }
}
 