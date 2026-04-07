using CompanyEmployees.IntegrationTests.Factories;
using Shared.DataTransferObjects;
using System.Net;
using System.Net.Http.Json;

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

        [Fact]
        public async Task WhenGetEndpointsRequested_ThenReturnsOKStatusCode()
        {
            // Act
            var response = await _client.GetAsync($"{CompaniesUrl}/3d490a70-94ce-4d15-9494-5248280c2ce3");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<CompanyDto>();
            Assert.IsType<CompanyDto>(result);
            Assert.False(string.IsNullOrEmpty(result.Name));
            Assert.False(string.IsNullOrEmpty(result.FullAddress));
        }
    }
}
