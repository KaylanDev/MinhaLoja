using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models
{
    public class Carrinho
    {
        public ObjectId Id { get; set; }
        public ObjectId UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<ItemCarrinho> Itens { get; set; } = new List<ItemCarrinho>();
    }
}
