using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinhaLoja.Core.Dtos;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Core.Services;
using FluentResults;
using Moq;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Testes.ProdutosTestes
{
    public class ProdutoTestesFail
    {
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly ProdutoService _produtoService;

        public ProdutoTestesFail()
        {
            _produtoRepository = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepository.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarFail_QuandoNenhumProdutoEncontrado()
        {
            // Arrange
            _produtoRepository.Setup(r => r.GetAllAsync()).ReturnsAsync((ICollection<Produto>)null);

            // Act
            var resultado = await _produtoService.ObterTodos();

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Nenhum produto encontrado", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarFail_QuandoProdutoNaoEncontrado()
        {
            // Arrange
            _produtoRepository.Setup(r => r.GetByIdAsync(It.IsAny<ObjectId>()))
                .ReturnsAsync((ProdutosDTO)null);

            // Act
            var resultado = await _produtoService.ObterPorId(ObjectId.GenerateNewId());

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Produto não encontrado!", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarFail_QuandoProdutoForNulo()
        {
            // Act
            var resultado = await _produtoService.Adicionar(null);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Produto n pode ser nulo!", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarFail_QuandoFalhaAoAdicionar()
        {
            // Arrange
            var produto = new ProdutosDTO();
            _produtoRepository.Setup(r => r.CreateAsync(produto))
                .ReturnsAsync((ProdutosDTO)null);

            // Act
            var resultado = await _produtoService.Adicionar(produto);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Falha ao Adcionar produto!", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFail_QuandoProdutoForNulo()
        {
            // Act
            var resultado = await _produtoService.Atualizar(null);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Produto n pode ser nulo", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFail_QuandoProdutoNaoEncontrado()
        {
            // Arrange
            var produto = new ProdutosDTO { Id = ObjectId.GenerateNewId() };
            _produtoRepository.Setup(r => r.GetByIdAsync(produto.Id))
                .ReturnsAsync((ProdutosDTO)null);

            // Act
            var resultado = await _produtoService.Atualizar(produto);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Produto n encontrado", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarFail_QuandoFalhaAoAtualizar()
        {
            // Arrange
            var produto = new ProdutosDTO { Id = ObjectId.GenerateNewId() };
            _produtoRepository.Setup(r => r.GetByIdAsync(produto.Id))
                .ReturnsAsync(produto);
            _produtoRepository.Setup(r => r.UpdateAsync(produto))
                .ReturnsAsync((ProdutosDTO)null);

            // Act
            var resultado = await _produtoService.Atualizar(produto);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Falha ao atualizar produto", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Remover_DeveRetornarFail_QuandoProdutoNaoExiste()
        {
            // Arrange
            _produtoRepository.Setup(r => r.GetByIdAsync(It.IsAny<ObjectId>()))
                .ReturnsAsync((ProdutosDTO)null);

            // Act
            var resultado = await _produtoService.Remover(ObjectId.GenerateNewId());

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("O produito n existe!", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task Remover_DeveRetornarFail_QuandoFalhaAoRemover()
        {
            // Arrange
            var produto = new ProdutosDTO { Id = ObjectId.GenerateNewId() };
            _produtoRepository.Setup(r => r.GetByIdAsync(produto.Id))
                .ReturnsAsync(produto);
            _produtoRepository.Setup(r => r.DeleteAsync(produto))
                .ReturnsAsync(false);

            // Act
            var resultado = await _produtoService.Remover(produto.Id);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Falha ao remover produto", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task ObterPorName_DeveRetornarFail_QuandoNomeForNulo()
        {
            // Act
            var resultado = await _produtoService.ObterPorName(null);

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("O nome nn pode ser nulo", resultado.Errors[0].Message);
        }

        [Fact]
        public async Task ObterPorName_DeveRetornarFail_QuandoProdutoNaoEncontrado()
        {
            // Arrange
            _produtoRepository.Setup(r => r.GetByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((ProdutosDTO)null);

            // Act
            var resultado = await _produtoService.ObterPorName("Produto Inexistente");

            // Assert
            Assert.True(resultado.IsFailed);
            Assert.Equal("Produto nao encontrado!", resultado.Errors[0].Message);
        }
    }
}
