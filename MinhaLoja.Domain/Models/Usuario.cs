using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Models
{
    public class Usuario
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Eamil { get; set; }
        public string SenhaHash { get; set; }
    }
}
