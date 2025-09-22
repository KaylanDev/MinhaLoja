using FluentResults;
using MinhaLoja.Core.Dtos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces;

public interface IProdutoService
{
   public Task<Result<ICollection<ProdutosDTO>>> ObterTodos();

   public Task<Result<ProdutosDTO>> ObterPorId(ObjectId id);
   public Task<Result<ProdutosDTO>> ObterPorName(string name);

   public Task<Result<ProdutosDTO>> Adicionar(ProdutosDTO produto);

   public Task<Result<ProdutosDTO>> Atualizar(ProdutosDTO produto);

   public Task<Result> Remover(ObjectId id);
}
