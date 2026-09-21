using ControlGastos.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlGastos.Api.Data
{
    public class AppDbContext : IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Categoria>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany()
                .HasForeignKey(g => g.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany(c => c.Gastos)
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
