using EmployeeManagementSystems_A6.Models;
using EmployeeManagementSystems_A6.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystems_A6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public EmployeeAdditionalDetailsController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAdditionalDetails>>> Get([FromQuery] FilterCriteria filter)
        {
            var queryString = "SELECT * FROM c";
            if (!string.IsNullOrEmpty(filter.FilterAttribute))
            {
                queryString += $" WHERE c.DType = '{filter.FilterAttribute}'";
            }

            var items = await _cosmosDbService.GetItemsAsync<EmployeeAdditionalDetails>(queryString);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAdditionalDetails>> Get(string id, [FromQuery] string partitionKey)
        {
            var item = await _cosmosDbService.GetItemAsync<EmployeeAdditionalDetails>(id, partitionKey);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeAdditionalDetails item)
        {
            await _cosmosDbService.AddItemAsync(item, item.DType);
            return CreatedAtAction(nameof(Get), new { id = item.Id, partitionKey = item.DType }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EmployeeAdditionalDetails item, [FromQuery] string partitionKey)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            await _cosmosDbService.UpdateItemAsync(id, item, partitionKey);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id, [FromQuery] string partitionKey)
        {
            await _cosmosDbService.DeleteItemAsync<EmployeeAdditionalDetails>(id, partitionKey);
            return NoContent();
        }
    }
}
