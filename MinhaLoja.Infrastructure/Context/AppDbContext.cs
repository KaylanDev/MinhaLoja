using Microsoft.EntityFrameworkCore;
using MinhaLoja.Domain.Models;
using MongoDB.EntityFrameworkCore.Extensions;

namespace MinhaLoja.Infrastructure.Context
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       public DbSet<Produto> Produto { get; set; }
       public DbSet<Usuario> Usuario { get; set; }
       public DbSet<ItemCarrinho> ItemCarrinho { get; set; }
       public DbSet<ItemPedido> ItemPedido { get; set; }
       public DbSet<Carrinho> Carrinho { get; set; }
       public DbSet<Pedido> Pedido { get; set; }
       public DbSet<Categoria> Categoria { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Produto>().ToCollection("Produtos");
            modelBuilder.Entity<Usuario>().ToCollection("Usuarios");
            modelBuilder.Entity<Carrinho>().ToCollection("Carrinhos");
            modelBuilder.Entity<Pedido>().ToCollection("Pedidos");
            modelBuilder.Entity<Categoria>().ToCollection("Categorias");


        }


    }
}
