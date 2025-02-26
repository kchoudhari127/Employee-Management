using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
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
                var addresses = await _employeeService.GetEmployeeAddressesAsync(employeeId);

                if (addresses == null || addresses.Count == 0)
                {
                    return NotFound(new { message = "No addresses found for the specified employee." });
                }

                return Ok(addresses);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return StatusCode(500, new { message = "An error occurred while retrieving addresses.", details = ex.Message });
            }
        }



    }
}
