using FluentResults;
using MinhaLoja.Core.Dtos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Interfaces
{
    public interface ICategoriaService
    {

        public Task<Result<ICollection<CategoriaDTO>>> ObterTodos();

        public Task<Result<CategoriaDTO>> ObterPorId(ObjectId id);
        public Task<Result<CategoriaDTO>> ObterPorName(string name);

        public Task<Result<CategoriaDTO>> Adicionar(CategoriaDTO produto);

        public Task<Result<CategoriaDTO>> Atualizar(CategoriaDTO produto);

        public Task<Result> Remover(string name);
    }
}
