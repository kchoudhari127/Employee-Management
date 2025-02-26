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
<<<<<<< HEAD
        Task<List<Address>> GetEmployeeAddressesAsync(int employeeId);
=======
>>>>>>> a2ad6e61066e0de017e7c58fc38960ec8de31505
        //Task<IEnumerable<EmployeeDto>> GetItems();
        //Task<EmployeeDto> GetItemById(int Id);
        //Task AddItem(EmployeeDto itemDto);
        //Task UpdateItem(EmployeeDto itemDto);
        //Task DeleteItem(int Id);
    }
}
