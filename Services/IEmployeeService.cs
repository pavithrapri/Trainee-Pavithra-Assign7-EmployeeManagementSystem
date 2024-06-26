using EmployeeManagementSystems_A6.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Services
{

    public interface IEmployeeService
    {

        Task<IEnumerable<EmployeeBasicDetails>> GetEmployeesAsync(PaginationParameters paginationParameters, string filterAttribute = null);
        Task<IEnumerable<EmployeeAdditionalDetails>> GetAdditionalDetailsAsync(PaginationParameters paginationParameters, string filterAttribute = null);
        Task AddEmployeeAsync(EmployeeBasicDetails employee);
    }
   
}
