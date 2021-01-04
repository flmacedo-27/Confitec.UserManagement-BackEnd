using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;

namespace UserManagement.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nome)
                .HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Sobrenome)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.DataNascimento)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Escolaridade)
                .IsRequired();
        }       
    }
}
