using MinhaLoja.Domain.Models;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.Repository
{
    public class ProdutoRepository : Repository<Produto> , IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
