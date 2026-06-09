using Microsoft.EntityFrameworkCore;
using GymSystemApi.Models.Entities;

namespace GymSystemApi.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor que recibe las opciones de configuración
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Cada DbSet representa una tabla en la base de datos
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilCliente> Perfiles { get; set; }
        public DbSet<Rutina> Rutinas { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<RegistroProgreso> RegistrosProgreso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Email único por usuario
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Un usuario tiene un perfil
            modelBuilder.Entity<PerfilCliente>()
                .HasOne(p => p.Usuario)
                .WithOne(u => u.Perfil)
                .HasForeignKey<PerfilCliente>(p => p.UsuarioId);

            // Una rutina pertenece a un cliente
            modelBuilder.Entity<Rutina>()
                .HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.ClienteId);

            // Un ejercicio pertenece a una rutina
            modelBuilder.Entity<Ejercicio>()
                .HasOne(e => e.Rutina)
                .WithMany(r => r.Ejercicios)
                .HasForeignKey(e => e.RutinaId);

            // Un registro de progreso pertenece a un cliente
            modelBuilder.Entity<RegistroProgreso>()
                .HasOne(rp => rp.Cliente)
                .WithMany()
                .HasForeignKey(rp => rp.ClienteId);
        }
    }
}
