using MinhaLoja.Domain.Interface;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Interfaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {

        public Task<IEnumerable<Pedido>> ObterPedidoPorUserId(ObjectId usuarioId);

    }
}
