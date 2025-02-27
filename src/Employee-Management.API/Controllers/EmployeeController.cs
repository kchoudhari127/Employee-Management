using Azure;
using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
using Employee_Management.Business.Services;
using Employee_Management.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeService> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            try
            {
                if (employeeDto == null)
                {
                    return BadRequest("Employee data is null");
                }

                if (!ModelState.IsValid)
                {
                    // Collect all error messages
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new { Errors = errors });
                }

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

                var result = await _employeeService.AddEmployeeAsync(employee);

                var response = new OperationResult
                {
                    Success = true,
                    Message = "Employee added successfully.",
                    StatusCode = 201 // Created
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the employee: {@Employee}", employeeDto.FirstName);

                throw; // Rethrow the exception to be caught by the middleware
            }
        }

        [HttpPut]
        [Route("updateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressDto updateAddressDto)
        {
           try
           { 
                if (!ModelState.IsValid)
                {
                    // Collect all error messages
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(new { Errors = errors });
                }

                var address = new Address
                {
                   Id = updateAddressDto.AddressId,
                   City = updateAddressDto.City,
                   Area = updateAddressDto.Area,
                   PinCode = updateAddressDto.PinCode
                };

                var result = await _employeeService.UpdateAddressAsync(address);

                var response = new OperationResult();

               if (result > 0)
               {
                   response = new OperationResult
                   {
                       Success = true,
                       Message = "Address updated successfully.",
                       StatusCode = 200 // OK
                   };
               }
              else
              {
                  response = new OperationResult
                  {
                      Success = false,
                      Message = "Address not found.",
                      StatusCode = 404 // Not Found
                  };
              }

               return Ok(response);

           }
           catch (Exception ex)
           {
                  _logger.LogError(ex.Message, "An error occurred while updating an employee.");

                  throw; // Rethrow the exception to be caught by the middleware
           }

        }

        [HttpGet]
        [Route("{employeeId}/addresses")]
        public async Task<IActionResult> GetEmployeeAddresses(int employeeId)
        {
            try
            {
                var results = await _employeeService.GetEmployeeAddressesAsync(employeeId);

                var response = new AddressResponceDto();

                if (results.Count > 0)
                {
                    response = new AddressResponceDto
                    {
                        Success = true,
                        Message = "The address records have been retrieved successfully.",
                        StatusCode = 200, // OK
                        addresses = results

                    };
                }
                else
                {
                    response = new AddressResponceDto
                    {
                        Success = false,
                        Message = "No addresses found for the specified employee.",
                        StatusCode = 404, // Not Found
                        addresses = results
                    };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while retrieving addresses.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }

        [HttpGet]
        [Route("{managerId}/employees")]
        public async Task<IActionResult> GetEmployeesReportingToManager(int managerId)
        {
            try
            {
                var result = await _employeeService.GetEmployeesReportingToManagerAsync(managerId);

                var response = new EmployeeResponceDto();

                if (result.Count > 0)
                {
                    response = new EmployeeResponceDto
                    {
                        Success = true,
                        Message = "The employee records have been retrieved successfully.",
                        StatusCode = 200 , // OK
                        employees = result

                    };
                }
                else
                {
                    response = new EmployeeResponceDto
                    {
                        Success = false,
                        Message = "No employees found reporting to the specified manager.",
                        StatusCode = 404, // Not Found
                        employees = result
                    };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while retrieving employees reporting to the specified manager.");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }

        [HttpGet]
        [Route("{employeeId}/manager")]
        public async Task<IActionResult> GetManagerForEmployee(int employeeId)
        {
            try
            {
                var manager = await _employeeService.GetManagerForEmployeeAsync(employeeId);

                var response = new ManagerResponceDto();

                if (manager.Count > 0)
                {
                    response = new ManagerResponceDto
                    {
                        Success = true,
                        Message = "The manager records have been retrieved successfully.",
                        StatusCode = 200, // OK
                        managers = manager

                    };
                }
                else
                {
                    response = new ManagerResponceDto
                    {
                        Success = false,
                        Message = "No manages found reporting to the specified manager.",
                        StatusCode = 404, // Not Found
                        managers = manager
                    };
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error occurred while retrieving the manager..");
                throw; // Rethrow the exception to be caught by the middleware
            }
        }
    }
}
