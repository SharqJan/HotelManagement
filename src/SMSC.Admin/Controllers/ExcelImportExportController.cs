using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using SMSC.Application.DTO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


namespace SMSC.Admin.Controllers
{
    [Authorize(Policy = "RequireEmail")]
    public class ExcelImportExportController : Controller
    {
        private readonly ILogger<ExcelImportExportController> _logger;

        public ExcelImportExportController(ILogger<ExcelImportExportController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        //Import 
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("No file uploaded.");
                return BadRequest("Please upload a valid Excel file.");
            }

            try
            {
                using var package = new ExcelPackage(file.OpenReadStream());
                var worksheet = package.Workbook.Worksheets[0]; 
                var rowCount = worksheet.Dimension.Rows;

                var roles = new List<RoleDTO>(); 

               
                for (int row = 2; row <= rowCount; row++)
                {
                    var RoleId =  worksheet.Cells[row, 1].Text; 
                    var RoleName = worksheet.Cells[row, 2].Text; 
                    var IsActive = worksheet.Cells[row, 3].Text; 
                    var CreatedDateTime = worksheet.Cells[row, 4].Text;
                    var role = new RoleDTO
                        {
                            RoleId = int.Parse(RoleId),
                            RoleName = RoleName,
                            IsActive = bool.Parse(IsActive),
                            CreatedDateTime = DateTime.Parse(CreatedDateTime)

                        };
                        roles.Add(role);
                
                }

                  /* var roleNames = from r in roles
                                     select r.RoleName;*/


                _logger.LogInformation("Imported {roles.Count} Roles from the Excel file.", roles.Count);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Excel import.");
                return StatusCode(500, "Internal server error while importing data.");
            }
        }


        //Export
        public IActionResult ExportExcel()
        {

            var users = UserData.GetSampleUsers();
            if (users == null || users.Count == 0)
            {
                _logger.LogWarning("No users found for export.");
                return BadRequest("No users found to export.");
            }

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Users");

            // Set the headers
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Age";

            // Make headers bold and center aligned
            using (var headerRange = worksheet.Cells[1, 1, 1, 3])  //first 2 starting (1,1)[first header cell] and last 2 (1,3)[last header cell] (x,y) first row x=1 y=1 to n 
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            // Add user data and center align
            for (int i = 0; i < users.Count; i++)
            { 
                worksheet.Cells[i + 2, 1].Value = users[i].Id;
                worksheet.Cells[i + 2, 2].Value = users[i].Name;
                worksheet.Cells[i + 2, 3].Value = users[i].Age;

                // Center the data in each cell
                worksheet.Cells[i + 2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[i + 2, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[i + 2, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            // Autofit columns for better visibility
            worksheet.Cells.AutoFitColumns();

            var stream = new MemoryStream();
            package.SaveAs(stream);
            var fileName = "Users.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            stream.Position = 0;

            _logger.LogInformation("Excel export successful for {UserCount} users.", users.Count);
            return File(stream, contentType, fileName);
        }

    }
}








