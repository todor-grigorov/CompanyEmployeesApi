using CompanyEmployees.IntegrationTests.Factories;

namespace CompanyEmployees.IntegrationTests.Tests
{
    public class CompaniesResponseTests : IClassFixture<CompanyEmployeesTestcontainersFactory>
    {
        private readonly HttpClient _client;
        private const string CompaniesUrl = "/api/companies";

        public CompaniesResponseTests(CompanyEmployeesTestcontainersFactory factory)
        {
            _client = factory.CreateClient();
        }
    }
}
