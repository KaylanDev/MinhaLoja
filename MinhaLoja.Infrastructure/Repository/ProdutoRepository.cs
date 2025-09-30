using MinhaLoja.Domain.Models;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MinhaLoja.Infrastructure.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }
        public  async Task<Produto> GetByNameAsync(string name)
        {

            var produto = await _context.Produto.FirstOrDefaultAsync(p => string.Equals(p.Nome.ToLowerInvariant(), name.ToLowerInvariant()));
            if (produto is null)
            {
                return null;
            }
            return produto;

        }


    }
}
