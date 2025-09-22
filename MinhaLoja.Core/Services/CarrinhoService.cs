using FluentResults;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IItemCarrinhoRepository _itemCarrinhoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, IProdutoRepository produtoRepository, IItemCarrinhoRepository itemCarrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
            _itemCarrinhoRepository = itemCarrinhoRepository;
        }

        public async Task<Result<ItemCarrinho>> AdicionarItemAsync(int usuarioId, int produtoId, int quantidade)
        {
            if (usuarioId == 0) return Result.Fail("Id do usuario invalido!");

           
        }

        public Task<Result<ItemCarrinho>> AtualizarQuantidadeAsync(int itemCarrinhoId, int novaQuantidade)
        {
            throw new NotImplementedException();
        }

        public Task<Result> LimparCarrinhoAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Carrinho>> ObterCarrinhoDoUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ItemCarrinho>> RemoverItemAsync(int itemCarrinhoId)
        {
            throw new NotImplementedException();
        }
    }
}
