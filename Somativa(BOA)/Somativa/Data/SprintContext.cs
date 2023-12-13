using Microsoft.EntityFrameworkCore;
using Somativa.Models;

namespace Somativa.Data
{
    public class SprintContext : DbContext
    {
        public SprintContext(DbContextOptions<SprintContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<CompraItem> CompraItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {			
			modelBuilder.Entity<Cliente>().ToTable("tbClientes");
            modelBuilder.Entity<Fornecedor>().ToTable("tbFornecedores");
            modelBuilder.Entity<Categoria>().ToTable("tbCategorias");
            modelBuilder.Entity<Produto>().ToTable("tbProdutos");
            modelBuilder.Entity<Venda>().ToTable("tbVendas");
            modelBuilder.Entity<VendaItem>().ToTable("tbVendaItens");
            modelBuilder.Entity<Compra>().ToTable("tbCompras");
            modelBuilder.Entity<CompraItem>().ToTable("tbCompraItens");
        }
                
    }
}
