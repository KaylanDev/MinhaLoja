using Microsoft.EntityFrameworkCore;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Domain.Models;
using MinhaLoja.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.Repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public   async Task<Categoria> GetByNameAsync(string name)
    {
        var categoria = await _context.Categoria.FirstOrDefaultAsync(p => string.Equals(p.Nome.ToLowerInvariant(), name.ToLowerInvariant()));
        if (categoria is null)
        {
            return null;
        }
        return categoria;
    }
}


