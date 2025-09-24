﻿using Shared.DataTransferObjects;

namespace CompanyEmployees.Core.Services.Abstractions
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
    }
}
