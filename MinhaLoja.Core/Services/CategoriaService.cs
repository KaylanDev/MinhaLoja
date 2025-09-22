using FluentResults;
using MinhaLoja.Core.Dtos;
using MinhaLoja.Core.Dtos.Extensions;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Infrastructure.Repository;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver.Core.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Core.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }
    public async Task<Result<CategoriaDTO>> ObterPorId(ObjectId id)
    {
        var entity = await _categoriaRepository.GetByIdAsync(id);
        if (entity == null)
        {
            return Result.Fail("Categoria não encontrada!");
        }
        CategoriaDTO entityDto = entity;
        return Result.Ok(entityDto);
    }

    public async Task<Result<CategoriaDTO>> ObterPorName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Fail<CategoriaDTO>("Nome inválido");
        }
        var entity = await _categoriaRepository.GetByNameAsync(name);
        if (entity == null)
        {
            return Result.Fail("Categoria não encontrada!");
        }
        CategoriaDTO entityDto = entity;
        return Result.Ok(entityDto);
    }

    public async Task<Result<ICollection<CategoriaDTO>>> ObterTodos()
    {
        var entities =  await _categoriaRepository.GetAllAsync();
        if (entities is null)
        {
            return Result.Fail<ICollection<CategoriaDTO>>("Nenhuma categoria encontrada");
        }
        var entityList =  entities.FromEntities<Categoria,CategoriaDTO>();
        return Result.Ok(entityList);

    }
    public async Task<Result<CategoriaDTO>> Adicionar(CategoriaDTO produto)
    {
        if (produto is null) return Result.Fail("produto n pode ser nulo!!");

       var entity = produto;
     var entityCreate = await _categoriaRepository.CreateAsync(entity);
        CategoriaDTO entityDto = entityCreate;
        return Result.Ok(entityDto);

    }

    public async Task<Result<CategoriaDTO>> Atualizar(CategoriaDTO produto)
    {
        if (produto is null) return Result.Fail<CategoriaDTO>("produto n pode ser nulo!!");

        var entity = produto;
        var entityUpdate = await _categoriaRepository.UpdateAsync(entity);
        if (entityUpdate is null) return Result.Fail<CategoriaDTO>("Erro ao atualizar a categoria");
        CategoriaDTO entityDto = entityUpdate;
        return Result.Ok(entityDto);
    }

    public async Task<Result> Remover(string name)
    {
        if(String.IsNullOrWhiteSpace(name)) return Result.Fail("Nome inválido");

        var entity = await _categoriaRepository.GetByNameAsync(name);
        if (entity is null) return Result.Fail("Categoria não encontrada");
        var result = await _categoriaRepository.DeleteAsync(entity);
        if (!result) return Result.Fail("Erro ao deletar a categoria");
        return Result.Ok();

    }
}
