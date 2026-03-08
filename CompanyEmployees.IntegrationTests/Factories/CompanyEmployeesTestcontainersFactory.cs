using CompanyEmployees.Infrastructure.Persistence;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CompanyEmployees.IntegrationTests.Factories
{
    public class CompanyEmployeesTestcontainersFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private const string Database = "master";
        private const string Username = "sa";
        private const string Password = "yourStrong(!)Password";
        private const ushort PostgresPort = 5432;

        private readonly IContainer _postgresContainer;

        public CompanyEmployeesTestcontainersFactory()
        {
            //_postgresContainer = new PostgreSqlBuilder().Build();

            _postgresContainer = new ContainerBuilder()
                .WithImage("postgres:latest")
                .WithPortBinding(PostgresPort)
                .WithEnvironment("POSTGRES_DB", Database)
                .WithEnvironment("POSTGRES_USER", Username)
                .WithEnvironment("POSTGRES_PASSWORD", Password)
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var host = _postgresContainer.Hostname;
            var port = _postgresContainer.GetMappedPublicPort(PostgresPort);
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<RepositoryContext>));

                services.AddDbContext<RepositoryContext>(options =>
                    options.UseNpgsql($"Host={host};Port={port};Database={Database};Username={Username};Password={Password}")
                        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var appContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
                try
                {
                    appContext.Database.Migrate(); // Apply all migrations to keep schema in sync
                }
                catch (Exception ex)
                {
                    // Log or handle migration exceptions if needed
                    throw;
                }
            });
        }

        public async Task InitializeAsync()
        {
            await _postgresContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _postgresContainer.DisposeAsync();
        }
    }
    public partial class Program { }
}
