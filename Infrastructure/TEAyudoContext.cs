using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TEAyudo_Usuarios;
using Microsoft.Extensions.Options;

namespace TEAyudo_Usuarios;
public class TEAyudoContext :DbContext
{
    public DbSet<EstadoUsuario> EstadoUsuarios{ get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public TEAyudoContext(DbContextOptions<TEAyudoContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.EstadoUsuario)
            .WithOne(eu => eu.Usuario)
            .HasForeignKey<EstadoUsuario>(eu => eu.EstadoUsuarioId);

        }


protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=TEAyudo_Usuarios;Trusted_Connection=True;TrustServerCertificate=True");
    }

}



