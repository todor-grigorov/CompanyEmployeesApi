using CompanyEmployees.Core.Domain.Entities;

namespace CompanyEmployees.Core.Services.Abstractions
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
    }
}
