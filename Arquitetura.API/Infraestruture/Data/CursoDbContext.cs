using Arquitetura.API.Business.Entities;
using Arquitetura.API.Infraestruture.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Arquitetura.API.Infraestruture.Data
{
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options) : base(options)
        {
        }

        public DbSet<Curso> Curso { get; set; }
        public DbSet<Usuario> Usuario{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
