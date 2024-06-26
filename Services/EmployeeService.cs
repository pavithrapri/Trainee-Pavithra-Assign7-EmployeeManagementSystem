using EmployeeManagementSystems_A6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public EmployeeService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<IEnumerable<EmployeeBasicDetails>> GetEmployeesAsync(PaginationParameters paginationParameters, string filterAttribute = null)
        {
            string queryString = "SELECT * FROM c";
            if (!string.IsNullOrEmpty(filterAttribute))
            {
                queryString += $" WHERE c.{filterAttribute}";
            }

            var employees = await _cosmosDbService.GetItemsAsync<EmployeeBasicDetails>(queryString);

            // Implement pagination
            employees = employees.Skip(paginationParameters.Skip).Take(paginationParameters.PageSize);

            return employees;
        }

        public async Task<IEnumerable<EmployeeAdditionalDetails>> GetAdditionalDetailsAsync(PaginationParameters paginationParameters, string filterAttribute = null)
        {
            string queryString = "SELECT * FROM c";
            if (!string.IsNullOrEmpty(filterAttribute))
            {
                queryString += $" WHERE c.{filterAttribute}";
            }

            var additionalDetails = await _cosmosDbService.GetItemsAsync<EmployeeAdditionalDetails>(queryString);

            // Implement pagination
            additionalDetails = additionalDetails.Skip(paginationParameters.Skip).Take(paginationParameters.PageSize);

            return additionalDetails;
        }

        public async Task AddEmployeeAsync(EmployeeBasicDetails employee)
        {
            await _cosmosDbService.AddItemAsync(employee, employee.DType);
        }
    }
}
