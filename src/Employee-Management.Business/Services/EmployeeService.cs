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

        public async Task<OperationResult> AddEmployeeAsync(EmployeeDto employeeDto)
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

              try
              { 
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
               // Log the exception (ex) as needed

                return new OperationResult
                {
                   Success = false,
                   Message = $"Error adding employee: {ex.Message}",
                   StatusCode = 500 // Internal Server Error
                };
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
<<<<<<< HEAD

        public async Task<List<Address>> GetEmployeeAddressesAsync(int employeeId)
        {
            return await _employeeRepository.GetEmployeeAddressesAsync(employeeId);
        }

=======
>>>>>>> a2ad6e61066e0de017e7c58fc38960ec8de31505
    }
}
