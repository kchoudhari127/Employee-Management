using Azure;
using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
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

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null");
            }

           var result = await _employeeService.AddEmployeeAsync(employeeDto);

            return Ok(result);
        }

        [HttpPut]
        [Route("updateAddress")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressDto updateAddressDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _employeeService.UpdateAddressAsync(updateAddressDto);

            return Ok(result);
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
                // Log the exception (ex) as needed
                return StatusCode(500, new { message = "An error occurred while retrieving addresses.", details = ex.Message });
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
                // Log the exception (ex) as needed
                return StatusCode(500, new { message = "An error occurred while retrieving employees.", details = ex.Message });
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
                // Log the exception (ex) as needed
                return StatusCode(500, new { message = "An error occurred while retrieving the manager.", details = ex.Message });
            }
        }
    }
}
