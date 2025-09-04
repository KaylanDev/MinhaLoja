using FluentResults;
using MinhaLoja.Core.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces;

public interface IProdutoService
{
   public Task<Result<ICollection<Produto>>> ObterTodos();

   public Task<Result<Produto>> ObterPorId(ObjectId id);

   public Task<Result<Produto>> Adicionar(Produto produto);

   public Task<Result<Produto>> Atualizar(Produto produto);

   public Task<Result> Remover(ObjectId id);
}
