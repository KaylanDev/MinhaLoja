using MinhaLoja.Core.Dtos;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.ProdutosTestes
{
    public class ProdutosTestesOk 
    {
        private readonly Mock<IProdutoService> _produtoService;

        public ProdutosTestesOk()
        {
            var produtoService = new Mock<IProdutoService>();
            _produtoService = produtoService;
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeProdutos()
        {
            // Arrange
            var produtosEsperados = new List<Produto>
            {
                new Produto { Nome = "Produto 1", Preco = 10.0m },
                new Produto { Nome = "Produto 2", Preco = 20.0m }
            };
            _produtoService.Setup(s => s.ObterTodos())
                .ReturnsAsync(FluentResults.Result.Ok((ICollection<ProdutosDTO>)produtosEsperados));
            // Act
            var resultado = await _produtoService.Object.ObterTodos();
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(produtosEsperados.Count, resultado.Value.Count);
            Assert.Equal(produtosEsperados[0].Nome, resultado.Value.ElementAt(0).Nome);
            Assert.Equal(produtosEsperados[1].Nome, resultado.Value.ElementAt(1).Nome);
        }
    }
}
