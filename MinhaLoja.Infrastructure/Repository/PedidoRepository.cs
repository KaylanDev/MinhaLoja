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
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Pedido>> ObterPedidoPorUserId(ObjectId usuarioId)
        {
            var pedido = await _context.Pedidos.Where(p => p.UsuarioId == usuarioId).ToListAsync();

            return pedido ?? null;
        }
    }
}
