using CompanyEmployees.IntegrationTests.Factories;

namespace CompanyEmployees.IntegrationTests.Tests
{
    public class ValidationTests : IClassFixture<CompanyEmployeesTestcontainersFactory>
    {
        private readonly HttpClient _client;
        private const string CompaniesUrl = "/api/companies";

        public ValidationTests(CompanyEmployeesTestcontainersFactory factory)
        {
            _client = factory.CreateClient();
        }
    }
}
