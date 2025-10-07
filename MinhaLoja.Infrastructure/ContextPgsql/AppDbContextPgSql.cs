using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaLoja.Infrastructure.ContextPgsql
{
    public class AppDbContextPgSql : IdentityDbContext<UserIdentity>
    {
        public AppDbContextPgSql(DbContextOptions<AppDbContextPgSql> options) : base(options)
        {
        }

        DbSet<UserIdentity> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Configurações adicionais do modelo podem ser feitas aqui
        }
    }
}
