using MinhaLoja.Domain.Models.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models
{
    public class Pedido
    {
        public ObjectId Id { get; set; }
        public ObjectId UsuarioId { get; set; }
        public DateTime DataPedido { get; set; }
        public Status Status { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemPedido> Itens { get; set; }
    }
}
