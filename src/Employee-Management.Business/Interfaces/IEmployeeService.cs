using Employee_Management.Business.DTOs;
using Employee_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<OperationResult> AddEmployeeAsync(EmployeeDto employeeDto);
        Task<OperationResult> UpdateAddressAsync(UpdateAddressDto updateAddressDto);

        Task<List<Address>> GetEmployeeAddressesAsync(int employeeId);

    }
}
