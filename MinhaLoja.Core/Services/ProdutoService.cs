using Amazon.Runtime.CredentialManagement.Internal;
using FluentResults;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Core.Models;
using MinhaLoja.Domain.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
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
        public async Task<Result<ICollection<Produto>>> ObterTodos()
        {
            var entities = await _produtoRepository.GetAllAsync();
            if (entities is null)
            {
                return  Result.Fail("Nenhum produto encontrado");
            }
            return Result.Ok(entities);
        }
        public async Task<Result<Produto>> ObterPorId(ObjectId id)
        {
            var entity = await _produtoRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Result.Fail("Produto não encontrado!");
            }
            return Result.Ok(entity);
        }

        
        public async Task<Result<Produto>> Adicionar(Produto produto)
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
            return Result.Ok(createdProduto);

        }

        public async Task<Result<Produto>> Atualizar(Produto produto)
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

            return Result.Ok(produtoAtualizado);
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
    }
}
