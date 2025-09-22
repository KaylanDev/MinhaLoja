using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinhaLoja.Core.Interfaces;
using MinhaLoja.Core.Services;
using MinhaLoja.Domain.Interface;
using MinhaLoja.Domain.Interfaces;
using MinhaLoja.Infrastructure.Context;
using MinhaLoja.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.CrossCutting.IoC;

public static class DependencyInjetion
{
    public static IServiceCollection Addinfrastruture(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConnectionString = configuration["Database:ConnectionString"];
        services.AddDbContext<AppDbContext>(options =>
            options.UseMongoDB(mongoConnectionString, "MinhaLoja"));
        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ICategoriaService, CategoriaService>();

        services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
        services.AddScoped<ICarrinhoService, CarrinhoService>();

        services.AddScoped<IItemCarrinhoRepository, ItemCarrinhoReposiotory>();


        return services;
    }
}
