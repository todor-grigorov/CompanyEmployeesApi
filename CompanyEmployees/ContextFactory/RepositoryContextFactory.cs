using CompanyEmployees.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CompanyEmployees.ContextFactory
{
    public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
    {
        public RepositoryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true)
           .AddUserSecrets(typeof(Program).Assembly) // Ensure user secrets are loaded
           .Build();

            var connectionString = configuration.GetConnectionString("postgresConnection");

            var builder = new DbContextOptionsBuilder<RepositoryContext>()
                .UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("CompanyEmployees.Infrastructure.Persistence"));

            return new RepositoryContext(builder.Options);
        }
    }
}
