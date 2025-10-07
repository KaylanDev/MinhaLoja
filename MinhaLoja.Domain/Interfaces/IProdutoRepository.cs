using MinhaLoja.Domain.Models;
using MinhaLoja.Domain.Interface;

namespace MinhaLoja.Domain.Interfaces
{
    public  interface IProdutoRepository : IRepository<Produto>
    {
        public  Task<Produto> GetByNameAsync(string name);


    }
}