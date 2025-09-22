using MinhaLoja.Core.Dtos;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Models;
using Moq;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Testes.ProdutosTestes
{
    public class ProdutosTestesOk 
    {
        private readonly Mock<IProdutoService> _produtoService;

        public ProdutosTestesOk()
        {
            _produtoService = new Mock<IProdutoService>();
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarListaDeProdutos()
        {
            // Arrange
            var produtosEsperados = new List<ProdutosDTO>
            {
                new ProdutosDTO { Id = ObjectId.GenerateNewId(), Nome = "Produto 1", Preco = 10.0m },
                new ProdutosDTO { Id = ObjectId.GenerateNewId(), Nome = "Produto 2", Preco = 20.0m }
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

        [Fact]
        public async Task ObterPorId_DeveRetornarProdutoQuandoExistir()
        {
            // Arrange
            var produtoEsperado = new ProdutosDTO { Id = ObjectId.GenerateNewId(), Nome = "Produto 1", Preco = 10.0m };
            _produtoService.Setup(s => s.ObterPorId(produtoEsperado.Id))
                .ReturnsAsync(FluentResults.Result.Ok(produtoEsperado));
            // Act
            var resultado = await _produtoService.Object.ObterPorId(produtoEsperado.Id);
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(produtoEsperado.Nome, resultado.Value.Nome);
            Assert.Equal(produtoEsperado.Id, resultado.Value.Id);
        }

        [Fact]
        public async Task ObterPorName_DeveRetornarProdutoQuandoExistir()
        {
            // Arrange
            var produtoEsperado = new ProdutosDTO { Id = ObjectId.GenerateNewId(), Nome = "Produto 1", Preco = 10.0m };
            _produtoService.Setup(s => s.ObterPorName(produtoEsperado.Nome))
                .ReturnsAsync(FluentResults.Result.Ok(produtoEsperado));
            // Act
            var resultado = await _produtoService.Object.ObterPorName(produtoEsperado.Nome);
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(produtoEsperado.Nome, resultado.Value.Nome);
            Assert.Equal(produtoEsperado.Id, resultado.Value.Id);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarProdutoAdicionado()
        {
            // Arrange
            var produtoParaAdicionar = new ProdutosDTO { Id = ObjectId.GenerateNewId(), Nome = "Produto Novo", Preco = 30.0m };
            _produtoService.Setup(s => s.Adicionar(produtoParaAdicionar))
                .ReturnsAsync(FluentResults.Result.Ok(produtoParaAdicionar));
            // Act
            var resultado = await _produtoService.Object.Adicionar(produtoParaAdicionar);
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(produtoParaAdicionar.Nome, resultado.Value.Nome);
            Assert.Equal(produtoParaAdicionar.Id, resultado.Value.Id);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarProdutoAtualizado()
        {
            // Arrange
            var produtoAtualizado = new ProdutosDTO { Id = ObjectId.GenerateNewId(), Nome = "Produto Atualizado", Preco = 40.0m };
            _produtoService.Setup(s => s.Atualizar(produtoAtualizado))
                .ReturnsAsync(FluentResults.Result.Ok(produtoAtualizado));
            // Act
            var resultado = await _produtoService.Object.Atualizar(produtoAtualizado);
            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal(produtoAtualizado.Nome, resultado.Value.Nome);
            Assert.Equal(produtoAtualizado.Id, resultado.Value.Id);
        }

        [Fact]
        public async Task Remover_DeveRetornarSucessoAoRemover()
        {
            // Arrange
            var idParaRemover = ObjectId.GenerateNewId();
            _produtoService.Setup(s => s.Remover(idParaRemover))
                .ReturnsAsync(FluentResults.Result.Ok());
            // Act
            var resultado = await _produtoService.Object.Remover(idParaRemover);
            // Assert
            Assert.True(resultado.IsSuccess);
        }                                                                                               
    }
}
