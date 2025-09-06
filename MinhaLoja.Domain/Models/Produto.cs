using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Domain.Models;

public class Produto
{
    public ObjectId Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public ObjectId CategoriaId { get; set; }
    public bool Ativo { get; set; }
}
