using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models
{
    public class ItemCarrinho
    {
        public ObjectId Id { get; set; }
        public ObjectId CarrinhoId { get; set; }
        public ObjectId ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Produto? Produto { get; set; }
    }
}
