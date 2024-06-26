namespace EmployeeManagementSystems_A6.Models
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string ReportingManagerName { get; set; }
    }
}
