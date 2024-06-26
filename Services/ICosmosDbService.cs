using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Services
{
    public interface ICosmosDbService
    {
        Task AddItemAsync<T>(T item, string partitionKey);
        Task DeleteItemAsync<T>(string id, string partitionKey);
        Task<T> GetItemAsync<T>(string id, string partitionKey);
        Task<IEnumerable<T>> GetItemsAsync<T>(string queryString);
        Task UpdateItemAsync<T>(string id, T item, string partitionKey);
    }
}
