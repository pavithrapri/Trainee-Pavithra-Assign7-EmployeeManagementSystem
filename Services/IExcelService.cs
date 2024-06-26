using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Services
{
    public interface IExcelService
    {
        Task<byte[]> ExportToExcelAsync();
        Task ImportFromExcelAsync(IFormFile file);
    }
}
