using CompanyEmployees.IntegrationTests.Factories;
using Shared.DataTransferObjects;
using System.Net;
using System.Net.Http.Json;

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

        [Fact]
        public async Task WhenUserRegistration_ThenReturnsCreated()
        {
            // Arrange
            var userForRegistration = new UserForRegistrationDto
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "jdoe",
                Email = "contact@jdoe.com",
                Password = "YouWillNeverGuess_1234",
                PhoneNumber = "1234567890",
                Roles = ["Manager"]
            };

            // Act
            var response = await _client.PostAsJsonAsync(AuthUrl, userForRegistration);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task WhenUserLogin_ThenReturnsOkResultAndTokens()
        {
            // Arrange
            var userForRegistration = new UserForRegistrationDto
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "jdoe2",
                Email = "contact2@jdoe.com",
                Password = "YouWillNeverGuess_1234",
                PhoneNumber = "1234567890",
                Roles = ["Manager"]
            };

            var user = new UserForAuthenticationDto
            {
                UserName = "jdoe2",
                Password = "YouWillNeverGuess_1234"
            };

            // Act
            var responseRegistration = await _client.PostAsJsonAsync(AuthUrl, userForRegistration);
            var responseAuthentication = await _client.PostAsJsonAsync($"{AuthUrl}/login", user);
            var result = await responseAuthentication.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Created, responseRegistration.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseAuthentication.StatusCode);
            Assert.Contains("accessToken", result);
            Assert.Contains("refreshToken", result);
        }
    }
}
