using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Dtos;

public class CategoriaDTO
{
    
    public string Nome { get; set; }
    public ICollection<int> ProdutosId { get; set; }


    public static implicit operator CategoriaDTO(Categoria categoria)
    {
        return new CategoriaDTO
        {
            
            Nome = categoria.Nome,
            ProdutosId = categoria.ProdutosId
        };
    }

    public static implicit operator Categoria(CategoriaDTO categoria)
    {
        return new Categoria
        {
            
            Nome = categoria.Nome,
            ProdutosId = categoria.ProdutosId
        };
    }
}
