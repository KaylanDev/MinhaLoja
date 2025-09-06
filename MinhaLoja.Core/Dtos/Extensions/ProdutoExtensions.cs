using MinhaLoja.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Dtos.Extensions
{
    public static class ProdutoExtensions
    {
        public static ICollection<ProdutosDTO> FromProdutos(this ICollection<Produto> produtos)
        {
            if (produtos == null) return null;
            return produtos.Select(p => (ProdutosDTO)p).ToList();
        }
    }
}
