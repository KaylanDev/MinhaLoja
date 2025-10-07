using MinhaLoja.Domain.Interface;
using MinhaLoja.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        public Task<Categoria> GetByNameAsync(string nome);
    }
}
