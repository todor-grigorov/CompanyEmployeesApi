using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CompanyEmployees.IntegrationTests.Factories
{
    public class CompanyEmployeesTestcontainersFactory : WebApplicationFactory<Program>
    {
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

        private const string Database = "master";
        private const string Username = "sa";
        private const string Password = "yourStrong(!)Password";
        private const ushort PostgresPort = 5432;

        private readonly IContainer _postgresContainer;
    }
    public partial class Program { }
}
