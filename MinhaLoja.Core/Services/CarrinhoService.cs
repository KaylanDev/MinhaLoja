using FluentResults;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace MinhaLoja.Core.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<Carrinho>> ObterCarrinhoDoUsuarioAsync(ObjectId usuarioId)
        {
            var carrinho = await _carrinhoRepository.GetByUserIdAsync(usuarioId);
            if (carrinho is null) return await _carrinhoRepository.CreateAsync(new Carrinho
            {
                UsuarioId = usuarioId,
                DataCriacao = DateTime.UtcNow
            });

            return Result.Ok(carrinho);
        }

        public async Task<Result<ItemCarrinho>> AdicionarItemAsync(ObjectId usuarioId,ObjectId itemCarrinhoId, ObjectId produtoId, int quantidade)
        {
            var carrinhoResult = await ObterCarrinhoDoUsuarioAsync(usuarioId);
            if (carrinhoResult.IsFailed) return Result.Fail(carrinhoResult.Errors);

            if (quantidade <= 0) return Result.Fail("Quantidade deve ser maior que zero");

            var produto = await _produtoRepository.GetByIdAsync(produtoId);
            if (produto is null) return Result.Fail("Produto não encontrado");

            if (produto.Estoque < quantidade) return Result.Fail("Estoque insuficiente");
            var item = carrinhoResult.Value.Itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
            if (item is not null)
            {
               item.Quantidade += quantidade;
                await _carrinhoRepository.UpdateAsync(carrinhoResult.Value);
                return Result.Ok(item);
            }

            var itemCarrinho = new ItemCarrinho
            {
                CarrinhoId = carrinhoResult.Value.Id,
                ProdutoId = produto.Id,
                Quantidade = quantidade,
                PrecoUnitario = produto.Preco,
                Produto = produto
            };
            carrinhoResult.Value.Itens.Add(itemCarrinho);
            await _carrinhoRepository.UpdateAsync(carrinhoResult.Value);

            return Result.Ok(itemCarrinho);
        }

        public async Task<Result<ItemCarrinho>> AtualizarQuantidadeAsync(ObjectId carrinhoId, ObjectId itemCarrinhoId, int novaQuantidade)
        {
            if (novaQuantidade <= 0) return Result.Fail("a quantidade n pode ser menor q 1");

            var itemCarrinho = await _carrinhoRepository.GetByIdAsync(carrinhoId);
            if (itemCarrinho is null) return Result.Fail("Carrinho n encontrado");

            var item = itemCarrinho.Itens.FirstOrDefault(i => i.Id == itemCarrinhoId);

            if (item is null) return Result.Fail("Item do carrinho n encontrado");

            item.Quantidade = novaQuantidade;

            await _carrinhoRepository.UpdateAsync(itemCarrinho);

            return Result.Ok(item);

        }

        public async Task<Result> LimparCarrinhoAsync(ObjectId usuarioId)
        {
           var carrinho = await _carrinhoRepository.GetByUserIdAsync(usuarioId);
            if (carrinho is null) return Result.Fail("Carrinho n encontrado");
            if (carrinho.Itens.Count() == 0)
            {
                return Result.Fail("Carrinho ja esta vazio");
            }
            carrinho.Itens.Clear();
          await  _carrinhoRepository.UpdateAsync(carrinho);
            return Result.Ok();
        }

        public async Task<Result<ItemCarrinho>> RemoverItemAsync(ObjectId itemCarrinhoId, ObjectId carrinhoId)
        {
           var carrinho =  _carrinhoRepository.GetByIdAsync(carrinhoId);

            if (carrinho is null) return Result.Fail("Carrinho n encontrado");

            var item = carrinho.Result.Itens.FirstOrDefault(i => i.Id == itemCarrinhoId);

            if (item is null) return Result.Fail("Item do carrinho n encontrado");
            carrinho.Result.Itens.Remove(item);

          await  _carrinhoRepository.UpdateAsync(carrinho.Result);

            return Result.Ok(item);
        }
    }
}
