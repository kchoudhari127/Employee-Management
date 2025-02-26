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
        Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto);
        //Task<IEnumerable<EmployeeDto>> GetItems();
        //Task<EmployeeDto> GetItemById(int Id);
        //Task AddItem(EmployeeDto itemDto);
        //Task UpdateItem(EmployeeDto itemDto);
        //Task DeleteItem(int Id);
    }
}
