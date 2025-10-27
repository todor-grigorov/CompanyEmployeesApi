﻿using CompanyEmployees.Core.Domain.Entities;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Core.Services.Abstractions
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges);

        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

        Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreation,
                                                bool trackChanges);

        Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges);

        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id,
                                        EmployeeForUpdateDto employeeForUpdate, bool compTrackChanges, bool empTrackChanges);

        Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(
                                        Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges);

        Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity);

    }
}
