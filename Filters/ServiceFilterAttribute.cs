using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EmployeeManagementSystems_A6.Filters
{
    public class ServiceFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is EmployeeFilterCriteria);
            if (param.Value == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            EmployeeFilterCriteria filterCriteria = (EmployeeFilterCriteria)param.Value;
            var statusFilter = filterCriteria.Filters.Find(f => f.FieldName == "status");
            if (statusFilter == null)
            {
                statusFilter = new FilterCriteria
                {
                    FieldName = "status",
                    Value = "Active"
                };
                filterCriteria.Filters.Add(statusFilter);
            }

            filterCriteria.Filters.RemoveAll(f => string.IsNullOrEmpty(f.FieldName));

            // You can perform additional operations here if needed.

            await next();
        }
    }

    public class EmployeeFilterCriteria
    {
        public List<FilterCriteria> Filters { get; set; } = new List<FilterCriteria>();
    }

    public class FilterCriteria
    {
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
