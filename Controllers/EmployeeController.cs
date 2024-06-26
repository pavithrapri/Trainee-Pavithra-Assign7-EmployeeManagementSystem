using EmployeeManagementSystems_A6.Models;
using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Controllers
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

        [HttpGet("basic")]
        public async Task<IActionResult> GetEmployees([FromQuery] PaginationParameters paginationParameters)
        {
            var employees = await _employeeService.GetEmployeesAsync(paginationParameters);
            return Ok(employees);
        }

        [HttpGet("additional")]
        public async Task<IActionResult> GetAdditionalDetails([FromQuery] PaginationParameters paginationParameters)
        {
            var additionalDetails = await _employeeService.GetAdditionalDetailsAsync(paginationParameters);
            return Ok(additionalDetails);
        }

        [HttpPost("basic")]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeBasicDetails employee)
        {
            await _employeeService.AddEmployeeAsync(employee);
            return Ok();
        }
    }
}
