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
        Task<Result<ItemCarrinho>> AdicionarItemAsync(ObjectId carrinhoId,ObjectId itemCarrinhoId, ObjectId produtoId, int quantidade);
        Task<Result<ItemCarrinho>> RemoverItemAsync(ObjectId itemCarrinhoId, ObjectId carrinhoId);
        Task<Result<ItemCarrinho>> AtualizarQuantidadeAsync(ObjectId carrinhoId,ObjectId itemCarrinhoId, int novaQuantidade);
        Task<Result> LimparCarrinhoAsync(ObjectId usuarioId);
    }
}
