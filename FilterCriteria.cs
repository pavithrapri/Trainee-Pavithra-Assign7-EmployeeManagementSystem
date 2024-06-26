namespace EmployeeManagementSystems_A6.Models
{
    public class FilterCriteria
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }
        public string FilterAttribute { get; set; }
    }
}
