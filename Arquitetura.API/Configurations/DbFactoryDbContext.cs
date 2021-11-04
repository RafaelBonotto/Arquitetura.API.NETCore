using Arquitetura.API.Infraestruture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Arquitetura.API.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
                                                Initial Catalog=master;
                                                Integrated Security=True;
                                                Connect Timeout=30;
                                                Encrypt=False;
                                                TrustServerCertificate=False;
                                                ApplicationIntent=ReadWrite;
                                                MultiSubnetFailover=False");
            

            CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

            return contexto;
        }
    }
}
