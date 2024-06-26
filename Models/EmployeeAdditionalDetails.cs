using System;
using Newtonsoft.Json;

namespace EmployeeManagementSystems_A6.Models
{
    public class EmployeeAdditionalDetails : BaseEntity
    {
        [JsonProperty("employeeBasicDetailsUId")]
        public string EmployeeBasicDetailsUId { get; set; }

        [JsonProperty("alternateEmail")]
        public string AlternateEmail { get; set; }

        [JsonProperty("alternateMobile")]
        public string AlternateMobile { get; set; }

        [JsonProperty("workInformation")]
        public WorkInfo_ WorkInformation { get; set; }

        [JsonProperty("personalDetails")]
        public PersonalDetails_ PersonalDetails { get; set; }

        [JsonProperty("identityInformation")]
        public IdentityInfo_ IdentityInformation { get; set; }

        [JsonProperty("dType")]
        public string DType { get; set; } // Add this property
    }

    public class WorkInfo_
    {
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string LocationName { get; set; }
        public string EmployeeStatus { get; set; }
        public string SourceOfHire { get; set; }
        public DateTime? DateOfJoining { get; set; }
    }

    public class PersonalDetails_
    {
        public DateTime? DateOfBirth { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
    }

    public class IdentityInfo_
    {
        public string PAN { get; set; }
        public string Aadhar { get; set; }
        public string Nationality { get; set; }
        public string PassportNumber { get; set; }
        public string PFNumber { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
