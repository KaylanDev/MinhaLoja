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

        DbSet<Produto> Produto { get; set; }
        DbSet<Usuario> Usuario { get; set; }
        DbSet<ItemCarrinho> ItemCarrinho { get; set; }
        DbSet<ItemPedido> ItemPedido { get; set; }
        DbSet<Carrinho> Carrinho { get; set; }
        DbSet<Pedido> Pedido { get; set; }
        DbSet<Categoria> Categoria { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Produto>().ToCollection("Produtos");
            modelBuilder.Entity<Usuario>().ToCollection("Usuarios");
            modelBuilder.Entity<ItemCarrinho>().ToCollection("ItensCarrinho");
            modelBuilder.Entity<ItemPedido>().ToCollection("ItensPedido");
            modelBuilder.Entity<Carrinho>().ToCollection("Carrinhos");
            modelBuilder.Entity<Pedido>().ToCollection("Pedidos");
            modelBuilder.Entity<Categoria>().ToCollection("Categorias");


        }


    }
}
