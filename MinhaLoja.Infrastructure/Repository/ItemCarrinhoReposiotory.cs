using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.Repository
{
    public class ItemCarrinhoReposiotory : Repository<ItemCarrinho>, IItemCarrinhoRepository
    {
        public ItemCarrinhoReposiotory(AppDbContext context) : base(context)
        {
        }

    }
}
