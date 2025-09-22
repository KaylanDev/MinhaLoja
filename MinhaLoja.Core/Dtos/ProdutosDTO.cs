
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Dtos
{
    public class ProdutosDTO
    {
      public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public ObjectId CategoriaId { get; set; }
        public bool Ativo { get; set; }


        public static implicit operator ProdutosDTO(Produto produto)
        {
            if (produto == null) return null;
            return new ProdutosDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                CategoriaId = produto.CategoriaId,
                Ativo = produto.Ativo
            };
        }

        public static implicit operator Produto(ProdutosDTO produtoDto)
        {
            if (produtoDto == null) return null;
            return new Produto
            {
                Id = produtoDto.Id,
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Preco = produtoDto.Preco,
                Estoque = produtoDto.Estoque,
                CategoriaId = produtoDto.CategoriaId,
                Ativo = produtoDto.Ativo
            };
        }

        // Remova o operador implícito para ICollection<ProdutosDTO> pois não é permitido conversão definida pelo usuário para interfaces.
        // Substitua por um método utilitário estático para realizar a conversão.

      
    }
}
