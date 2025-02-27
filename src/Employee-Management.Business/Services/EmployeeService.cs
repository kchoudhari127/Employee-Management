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

        public async Task<OperationResult> AddEmployeeAsync(EmployeeDto employeeDto)
        {
            try
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

                int rowsAffected = await _employeeRepository.AddEmployeeAsync(employee);

                return new OperationResult
                {
                    Success = true,
                    Message = "Employee added successfully.",
                    StatusCode = 201 // Created
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding an employee.");
                throw; // Rethrow the exception to be caught by the middleware
            }

        }

        public async Task<OperationResult> UpdateAddressAsync(UpdateAddressDto updateAddressDto)
        {
            var address = new Address
            {       
                    Id   = updateAddressDto.AddressId,
                    City = updateAddressDto.City,
                    Area = updateAddressDto.Area,
                    PinCode = updateAddressDto.PinCode
            };

            try
            {
                int rowsAffected = await _employeeRepository.UpdateAddressAsync(address);

                if (rowsAffected > 0)
                {
                    return new OperationResult
                    {
                        Success = true,
                        Message = "Address updated successfully.",
                        StatusCode = 200 // OK
                    };
                }
                else
                {
                    return new OperationResult
                    {
                        Success = false,
                        Message = "Address not found.",
                        StatusCode = 404 // Not Found
                    };
                }

            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed

                return new OperationResult
                {
                    Success = false,
                    Message = $"Error updating address: {ex.Message}",
                    StatusCode = 500 // Internal Server Error
                };
            }
        }


        public async Task<List<GetAddressDto>> GetEmployeeAddressesAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployeeAddressesAsync(employeeId);
        }

        public async Task<List<GetEmployeeDto>> GetEmployeesReportingToManagerAsync(int managerId)
        {
            return await _employeeRepository.GetEmployeesReportingToManagerAsync(managerId);
        }

        public async Task<List<ManagerDto>> GetManagerForEmployeeAsync(int employeeId)
        {
            return await _employeeRepository.GetManagerForEmployeeAsync(employeeId);
        }



    }
}
