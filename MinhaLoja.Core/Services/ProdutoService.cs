using Amazon.Runtime.CredentialManagement.Internal;
using FluentResults;
using MinhaLoja.Core.Dtos;
using MinhaLoja.Core.Dtos.Extensions;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<Result<ICollection<ProdutosDTO>>> ObterTodos()
        {
            var entities = await _produtoRepository.GetAllAsync();
            if (entities is null)
            {
                return  Result.Fail("Nenhum produto encontrado");
            }
           var entityList = entities.FromEntities<Produto,ProdutosDTO>();
            return Result.Ok(entityList);
        }
        public async Task<Result<ProdutosDTO>> ObterPorId(ObjectId id)
        {
            var entity = await _produtoRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Result.Fail("Produto não encontrado!");
            }
            ProdutosDTO entityDto = entity;
            return Result.Ok(entityDto);
        }

        
        public async Task<Result<ProdutosDTO>> Adicionar(ProdutosDTO produto)
        {
            if (produto is null )
            {
                return Result.Fail("Produto n pode ser nulo!");
            }
            var createdProduto = await _produtoRepository.CreateAsync(produto);
            if (createdProduto is null)
            {
                return Result.Fail("Falha ao Adcionar produto!");
            }
            ProdutosDTO createdProdutoDto = createdProduto;
            return Result.Ok(createdProdutoDto);

        }

        public async Task<Result<ProdutosDTO>> Atualizar(ProdutosDTO produto)
        {
            if (produto is null)
            {
                return Result.Fail("Produto n pode ser nulo");
            }
            var produtoExistente = await _produtoRepository.GetByIdAsync(produto.Id);
            if (produtoExistente is null)
            {
                return Result.Fail("Produto n encontrado");
            }
            produtoExistente = produto;
            var produtoAtualizado = await _produtoRepository.UpdateAsync(produtoExistente);

            if (produtoAtualizado is null)
            {
                return Result.Fail("Falha ao atualizar produto");
            }
            ProdutosDTO produtoAtualizadoDto = produtoAtualizado;

            return Result.Ok(produtoAtualizadoDto);
        }

        public async Task<Result> Remover(ObjectId id)
        {
            
            var produtoExistente = await _produtoRepository.GetByIdAsync(id);
            if (produtoExistente is null)
            {
                return Result.Fail("O produito n existe!");
            }
            var result = await _produtoRepository.DeleteAsync(produtoExistente);

            return result ? Result.Ok() : Result.Fail("Falha ao remover produto");

        }

        public async Task<Result<ProdutosDTO>> ObterPorName(string name)
        {
            if (name is null)
            {
                return Result.Fail("O nome nn pode ser nulo");
            }

            var entity = await _produtoRepository.GetByNameAsync(name);

            if (entity is null)
            {
                return Result.Fail("Produto nao encontrado!");
            }
            ProdutosDTO entityDto = entity;

            return Result.Ok(entityDto);

        }
    }
}
