using System;
using Newtonsoft.Json;

namespace EmployeeManagementSystems_A6.Models
{
    public class BaseEntity
    {
        [JsonProperty("id")] // Specify the JSON property name
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
