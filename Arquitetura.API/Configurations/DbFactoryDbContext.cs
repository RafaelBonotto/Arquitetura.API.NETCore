using Arquitetura.API.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Arquitetura.API.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        private readonly IConfiguration _configuration;

        public DbFactoryDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
// Executar as migrations que estiverem pendentes:
//var migrationsPendentes = contexto.Database.GetPendingMigrations();
//if (migrationsPendentes.Count() > 0)
//{
//    contexto.Database.Migrate();
//}