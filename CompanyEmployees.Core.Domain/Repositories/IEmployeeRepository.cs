using CompanyEmployees.Core.Domain.Entities;

namespace CompanyEmployees.Core.Domain.Repositories
{
    public interface 
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);

        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

        void CreateEmployeeForCompany(Guid companyId, Employee employee);

        void DeleteEmployee(Company company, Employee employee);
    }
}
