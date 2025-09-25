using FluentResults;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces
{
    public interface ICarrinhoService
    {
        Task<Result<Carrinho>> ObterCarrinhoDoUsuarioAsync(ObjectId usuarioId);
        Task<Result<ItemCarrinho>> AdicionarItemAsync(ObjectId usuarioId, string produtoName, int quantidade);
        Task<Result<ItemCarrinho>> RemoverItemAsync(ObjectId itemCarrinhoId);
        Task<Result<ItemCarrinho>> AtualizarQuantidadeAsync(ObjectId itemCarrinhoId, int novaQuantidade);
        Task<Result> LimparCarrinhoAsync(ObjectId usuarioId);
    }
}
