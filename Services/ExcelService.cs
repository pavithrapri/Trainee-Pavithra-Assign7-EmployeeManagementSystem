using EmployeeManagementSystems_A6.Models;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagementSystems_A6.Services
{
    public class ExcelService : IExcelService
    {
        private readonly ICosmosDbService _cosmosDbService;

        public ExcelService(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<byte[]> ExportToExcelAsync()
        {
            var employees = await _cosmosDbService.GetItemsAsync<EmployeeBasicDetails>("SELECT * FROM c WHERE c.DType = 'EmployeeBasicDetails'");
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");
                worksheet.Cells[1, 1].Value = "Sr.No";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone No";
                worksheet.Cells[1, 6].Value = "Reporting Manager Name";
                worksheet.Cells[1, 7].Value = "Date Of Birth";
                worksheet.Cells[1, 8].Value = "Date of Joining";

                int row = 2;
                foreach (var emp in employees)
                {
                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = emp.FirstName;
                    worksheet.Cells[row, 3].Value = emp.LastName;
                    worksheet.Cells[row, 4].Value = emp.Email;
                    worksheet.Cells[row, 5].Value = emp.Mobile;
                    worksheet.Cells[row, 6].Value = emp.ReportingManagerName;

                    var additionalDetails = await _cosmosDbService.GetItemsAsync<EmployeeAdditionalDetails>($"SELECT * FROM c WHERE c.employeeBasicDetailsUId = '{emp.Id}' AND c.DType = 'EmployeeAdditionalDetails'");
                    if (additionalDetails != null)
                    {
                        var details = additionalDetails.FirstOrDefault();
                        if (details != null)
                        {
                            worksheet.Cells[row, 7].Value = details.PersonalDetails?.DateOfBirth?.ToShortDateString() ?? string.Empty;
                            worksheet.Cells[row, 8].Value = details.WorkInformation?.DateOfJoining?.ToShortDateString() ?? string.Empty;
                        }
                    }

                    row++;
                }

                return package.GetAsByteArray();
            }
        }

        public async Task ImportFromExcelAsync(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var emp = new EmployeeBasicDetails
                        {
                            FirstName = worksheet.Cells[row, 2].Text,
                            LastName = worksheet.Cells[row, 3].Text,
                            Email = worksheet.Cells[row, 4].Text,
                            Mobile = worksheet.Cells[row, 5].Text,
                            ReportingManagerName = worksheet.Cells[row, 6].Text,
                            DType = "EmployeeBasicDetails"
                        };

                        await _cosmosDbService.AddItemAsync(emp, emp.DType);

                        var dateOfBirth = worksheet.Cells[row, 7].Text;
                        var dateOfJoining = worksheet.Cells[row, 8].Text;

                        if (!string.IsNullOrEmpty(dateOfBirth) || !string.IsNullOrEmpty(dateOfJoining))
                        {
                            var additionalDetails = new EmployeeAdditionalDetails
                            {
                                EmployeeBasicDetailsUId = emp.Id,
                                DType = "EmployeeAdditionalDetails",
                                PersonalDetails = new PersonalDetails_
                                {
                                    DateOfBirth = string.IsNullOrEmpty(dateOfBirth) ? (DateTime?)null : DateTime.Parse(dateOfBirth)
                                },
                                WorkInformation = new WorkInfo_
                                {
                                    DateOfJoining = string.IsNullOrEmpty(dateOfJoining) ? (DateTime?)null : DateTime.Parse(dateOfJoining)
                                }
                            };

                            await _cosmosDbService.AddItemAsync(additionalDetails, additionalDetails.DType);
                        }
                    }
                }
            }
        }
    }
}
