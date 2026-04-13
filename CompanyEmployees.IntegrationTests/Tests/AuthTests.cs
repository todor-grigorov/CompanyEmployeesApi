using CompanyEmployees.IntegrationTests.Factories;

namespace CompanyEmployees.IntegrationTests.Tests
{
    public class AuthTests : IClassFixture<CompanyEmployeesTestcontainersFactory>
    {
        private readonly HttpClient _client;
        private const string AuthUrl = "/api/authentication";
        private const string CompaniesUrl = "/api/companies";

        public AuthTests(CompanyEmployeesTestcontainersFactory factory)
        {
            _client = factory.CreateClient();
        }
    }
}
