using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class TEAyudoContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }

    public TEAyudoContext(DbContextOptions<TEAyudoContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasKey(f => f.UsuarioId);
        modelBuilder.Entity<Usuario>().Property(f => f.EstadoUsuarioId).IsRequired(false);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=TEAyudo_Usuarios;Trusted_Connection=True;TrustServerCertificate=True");
    }

}



