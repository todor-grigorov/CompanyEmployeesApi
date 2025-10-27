using AutoMapper;
using CompanyEmployees.Core.Domain.Entities;
using CompanyEmployees.Core.Domain.Exceptions;
using CompanyEmployees.Core.Services.Abstractions;
using CompanyEmployees.Infrastructure.Persistence.Repositories;
using LoggingService;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Core.Services
{
    internal sealed class EmployeeService : IEmployeeService 
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                    throw new CompanyNotFoundException(companyId);

            var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

            return employeesDto;
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeDb is null)
                throw new EmployeeNotFoundException(id);

            var employee = _mapper.Map<EmployeeDto>(employeeDb);

            return employee;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation,
                                                    bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);

            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = _repository.Employee.GetEmployee(companyId, id, trackChanges);
            if (employeeForCompany is null)
                throw new EmployeeNotFoundException(id);

            _repository.Employee.DeleteEmployee(company, employeeForCompany);
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
                                                bool compTrackChanges, bool empTrackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);
            await _repository.SaveAsync();
        }


        public (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch
                                                         (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            var company = _repository.Company.GetCompany(companyId, compTrackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(companyId);

            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

            return (employeeToPatch, employeeEntity);
        }

        public void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            _repository.Save();
        }
    }
}
