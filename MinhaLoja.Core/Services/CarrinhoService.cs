using FluentResults;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
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
        private readonly IItemCarrinhoRepository _itemCarrinhoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, IProdutoRepository produtoRepository, IItemCarrinhoRepository itemCarrinhoRepository, IUsuarioRepository usuarioRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
            _itemCarrinhoRepository = itemCarrinhoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Result<ItemCarrinho>> AdicionarItemAsync(ObjectId usuarioId, string produtoName, int quantidade)
        {
            var carrinhoResult = await ObterCarrinhoDoUsuarioAsync(usuarioId);
            if (carrinhoResult.IsFailed) return Result.Fail(carrinhoResult.Errors);

            if (quantidade <= 0) return Result.Fail("Quantidade deve ser maior que zero");

            var produto = await _produtoRepository.GetByNameAsync(produtoName);
            if (produto is null) return Result.Fail("Produto não encontrado");
            if (produto.Estoque < quantidade) return Result.Fail("Estoque insuficiente");
            if (carrinhoResult.Value.Itens.FirstOrDefault(i => i.ProdutoId == produto.Id) is not null)
            {
                return await AtualizarQuantidadeAsync(carrinhoResult.Value.Id,quantidade);
            }

            var itemCarrinho = new ItemCarrinho
            {
                CarrinhoId = carrinhoResult.Value.Id,
                ProdutoId = produto.Id,
                Quantidade = quantidade,
                PrecoUnitario = produto.Preco
            };

            carrinhoResult.Value.Itens.Add(itemCarrinho);

            await _carrinhoRepository.UpdateAsync(carrinhoResult.Value);

            return Result.Ok(itemCarrinho);
        }

        public Task<Result<ItemCarrinho>> AtualizarQuantidadeAsync(ObjectId itemCarrinhoId, int novaQuantidade)
        {
            
        }

        public Task<Result> LimparCarrinhoAsync(ObjectId usuarioId)
        {
            throw new NotImplementedException();
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

        public Task<Result<ItemCarrinho>> RemoverItemAsync(ObjectId itemCarrinhoId)
        {
            throw new NotImplementedException();
        }
    }
}
