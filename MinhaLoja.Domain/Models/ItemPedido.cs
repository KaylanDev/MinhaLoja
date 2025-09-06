using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models
{
    public class ItemPedido
    {
        public ObjectId Id { get; set; }
        public ObjectId PedidoId { get; set; }
        public ObjectId ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
