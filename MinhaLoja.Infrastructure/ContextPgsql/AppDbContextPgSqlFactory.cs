using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.IO;

namespace MinhaLoja.Infrastructure.ContextPgsql;

public class AppDbContextPgSqlFactory : IDesignTimeDbContextFactory<AppDbContextPgSql>
{
    public AppDbContextPgSql CreateDbContext(string[] args)
    {
        // 🧭 Obtém o caminho da raiz do projeto onde está o appsettings.json
        var basePath = Directory.GetCurrentDirectory();

        // 🏗️ Monta o configuration builder para ler appsettings
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        // 🔑 Lê a connection string
        var connectionString = configuration["PostgreSQL:ConnectionString"];

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'PostgreSQL' não encontrada no appsettings.");

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContextPgSql>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContextPgSql(optionsBuilder.Options);
    }
}


