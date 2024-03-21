using KriaHubTest.Models;
using Microsoft.EntityFrameworkCore;

namespace KriaHubTest.Data
{
    public class BancoContext : DbContext
    {
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<RepositorioModel> Repositorios { get; set; }
        public DbSet<UsuarioRepositorioModel> UsuariosRepositorios { get; set; }

        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>()
                .HasMany(u => u.RepositoriosProprios)
                .WithOne(r => r.Usuario)
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<UsuarioModel>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}
