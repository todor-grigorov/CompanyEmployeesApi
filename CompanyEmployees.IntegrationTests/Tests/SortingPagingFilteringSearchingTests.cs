using CompanyEmployees.IntegrationTests.Factories;

namespace CompanyEmployees.IntegrationTests.Tests
{
    public class SortingPagingFilteringSearchingTests : IClassFixture<CompanyEmployeesTestcontainersFactory>
    {
        private readonly HttpClient _client;
        private const string EmployeesUrl = "/api/companies";

        public SortingPagingFilteringSearchingTests(CompanyEmployeesTestcontainersFactory factory)
        {
            _client = factory.CreateClient();
        }
    }
}
