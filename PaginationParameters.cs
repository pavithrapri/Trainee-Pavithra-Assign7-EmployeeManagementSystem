namespace EmployeeManagementSystems_A6.Models
{
    public class PaginationParameters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int Skip
        {
            get { return (Page - 1) * PageSize; }
        }
    }
}
