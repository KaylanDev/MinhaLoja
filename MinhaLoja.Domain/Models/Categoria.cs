using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models
{
    public   class Categoria
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public ICollection<int> ProdutosId { get; set; } 
    }
}
