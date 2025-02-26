using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
using Employee_Management.Core.Entities;
using Employee_Management.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Management.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Designation = employeeDto.Designation,
                ReportsToId = employeeDto.ReportsToId,
                Addresses = employeeDto.Addresses.Select(a => new Address
                {
                    City = a.City,
                    Area = a.Area,
                    PinCode = a.PinCode
                }).ToList()
            };

            return await _employeeRepository.AddEmployeeAsync(employee);
        }
    }
}
