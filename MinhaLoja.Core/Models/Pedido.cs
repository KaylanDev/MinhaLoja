using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;\
using MinhaLoja.Core.Models.Enums;

namespace MinhaLoja.Core.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataPedido { get; set; }
        public Status Status { get; set; }
        public decimal Total { get; set; }
        public ICollection<ItemPedido> Itens { get; set; }
    }
}
