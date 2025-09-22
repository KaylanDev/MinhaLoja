using FluentResults;
using MinhaLoja.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces
{
    public interface ICarrinhoService
    {
        Task<Result<Carrinho>> ObterCarrinhoDoUsuarioAsync(int usuarioId);
        Task<Result<ItemCarrinho>> AdicionarItemAsync(int usuarioId, int produtoId, int quantidade);
        Task<Result<ItemCarrinho>> RemoverItemAsync(int itemCarrinhoId);
        Task<Result<ItemCarrinho>> AtualizarQuantidadeAsync(int itemCarrinhoId, int novaQuantidade);
        Task<Result> LimparCarrinhoAsync(int usuarioId);
    }
}
