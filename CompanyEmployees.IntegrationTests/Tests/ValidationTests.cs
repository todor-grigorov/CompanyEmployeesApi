using CompanyEmployees.IntegrationTests.Factories;
using System.Net;

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

        [Fact]
        public async Task WhenEntityDoesntExist_ThenReturns404NotFound()
        {
            // Act
            var response = await _client.GetAsync($"{CompaniesUrl}/d9d4c053-49b6-410c-bc78-2d54a9991870");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
