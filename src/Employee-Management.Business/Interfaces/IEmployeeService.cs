﻿using Employee_Management.Business.DTOs;
using Employee_Management.Core.DTOs;
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
        Task<int> AddEmployeeAsync(Employee employee);
        Task<int> UpdateAddressAsync(Address address);
        Task<List<GetAddressDto>> GetEmployeeAddressesAsync(int employeeId);
        Task<List<GetEmployeeDto>> GetEmployeesReportingToManagerAsync(int managerId);
        Task<List<ManagerDto>> GetManagerForEmployeeAsync(int employeeId);

    }
}
