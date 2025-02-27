using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
using Employee_Management.Core.DTOs;
using Employee_Management.Core.Entities;
using Employee_Management.Core.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            try
            {
                int rowsAffected = await _employeeRepository.AddEmployeeAsync(employee);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while adding an employee.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }
        public async Task<int> UpdateAddressAsync(Address address)
        {
            try
            {
                int rowsAffected = await _employeeRepository.UpdateAddressAsync(address);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while updating address.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }


        public async Task<List<GetAddressDto>> GetEmployeeAddressesAsync(int employeeId)
        {
            try
            {
                return await _employeeRepository.GetEmployeeAddressesAsync(employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while Getting address.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }

        public async Task<List<GetEmployeeDto>> GetEmployeesReportingToManagerAsync(int managerId)
        {
            try
            {
                return await _employeeRepository.GetEmployeesReportingToManagerAsync(managerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while Getting Employees Reporting To Manager.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }

        public async Task<List<ManagerDto>> GetManagerForEmployeeAsync(int employeeId)
        {
            try
            { 
                  return await _employeeRepository.GetManagerForEmployeeAsync(employeeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while Getting Manager For Employee.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }



    }
}
