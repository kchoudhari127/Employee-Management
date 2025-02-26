using Employee_Management.Business.DTOs;
using Employee_Management.Business.Interfaces;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> CreateEmployee(EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null");
            }

            var employee = await _employeeService.AddEmployeeAsync(employeeDto);
            return Ok(employee);
        }
    }
}
