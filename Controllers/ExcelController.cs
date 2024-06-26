using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelService _excelService;

        public ExcelController(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var file = await _excelService.ExportToExcelAsync();
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }

            await _excelService.ImportFromExcelAsync(file);
            return Ok();
        }
    }
}
