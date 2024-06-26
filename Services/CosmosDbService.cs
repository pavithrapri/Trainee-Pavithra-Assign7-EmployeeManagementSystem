using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly Container _container;
        private readonly ILogger<CosmosDbService> _logger;

        public CosmosDbService(CosmosClient cosmosClient, string databaseName, string containerName, ILogger<CosmosDbService> logger)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
            _logger = logger;
        }

        public async Task AddItemAsync<T>(T item, string partitionKey)
        {
            _logger.LogInformation($"Adding item with partition key: {partitionKey}");
            await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
        }

        public async Task DeleteItemAsync<T>(string id, string partitionKey)
        {
            _logger.LogInformation($"Deleting item with ID: {id} and partition key: {partitionKey}");
            await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
        }

        public async Task<T> GetItemAsync<T>(string id, string partitionKey)
        {
            try
            {
                _logger.LogInformation($"Getting item with ID: {id} and partition key: {partitionKey}");
                var response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning($"Item with ID: {id} and partition key: {partitionKey} not found.");
                return default;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>(string queryString)
        {
            _logger.LogInformation($"Getting items with query: {queryString}");
            var query = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateItemAsync<T>(string id, T item, string partitionKey)
        {
            _logger.LogInformation($"Updating item with ID: {id} and partition key: {partitionKey}");
            await _container.UpsertItemAsync(item, new PartitionKey(partitionKey));
        }
    }
}
