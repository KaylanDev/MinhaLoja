using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Dtos
{
    public class CarrinhoDTO
    {
        public ObjectId Id { get; set; }
        public ObjectId UsuarioId { get; set; }
        public DateTime DataCriacao { get; set; }
        public ICollection<ItemCarrinhoDTO> Itens { get; set; } = new List<ItemCarrinhoDTO>();

        public static implicit operator CarrinhoDTO(Carrinho carrinho)
        {
            if (carrinho == null) return null;
            return new CarrinhoDTO
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                DataCriacao = carrinho.DataCriacao,
               
            };
        }

        public static implicit operator Carrinho(CarrinhoDTO carrinho)
        {
            if (carrinho == null) return null;
            return new Carrinho
            {
                Id = carrinho.Id,
                UsuarioId = carrinho.UsuarioId,
                DataCriacao = carrinho.DataCriacao,

            };
        }

    }
}
