using Microsoft.EntityFrameworkCore;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Infrastructure.Context;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.Repository
{
    public class CarrinhoRepository : Repository<Carrinho>, ICarrinhoRepository
    {
        public CarrinhoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Carrinho> GetByUserIdAsync(ObjectId usuarioId)
        {
            var carrinho = await _context.Carrinho.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            if (carrinho is null) return null;
            return carrinho;
        }
    }
}
