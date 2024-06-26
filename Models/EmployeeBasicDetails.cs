using Newtonsoft.Json;
using System.Net;

namespace EmployeeManagementSystems_A6.Models
{

    public class EmployeeBasicDetails : BaseEntity
    {
        [JsonProperty("dType")]
        public string DType { get; set; }
        public string Salutory { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string EmployeeID { get; set; }
        public string Role { get; set; }
        public string ReportingManagerUId { get; set; }
        public string ReportingManagerName { get; set; }
        public string Address { get; set; }
    }
}
