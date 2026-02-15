using ClosedXML.Excel;
using ExcelDataReader;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace HamrahanSystem.Presntation.Controllers
{
    [Authorize]
    public class CustomLensCylsController(ITblLnsCustomCylService tblLnsCustomCylService) : Controller
    {
        [Authorize(Roles = "admin,CustomLensCyl_Index")]
        public IActionResult Index(bool embedded = false)
        {
            ViewBag.Embedded = embedded;
            return View();
        }

        [Authorize(Roles = "admin,CustomLensCyl_Index")]
        public JsonResult Detail(string sidx, string sord, int page = 1, int rows = 10)
        {
            GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
            int pageSize = itemGrid.Rows;
            var item = tblLnsCustomCylService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
            int totalRecords = item.Result.Item2;
            if (totalRecords <= 0)
            {
                return Json(new { total = 0, page, records = 0, rows = Array.Empty<object>() });
            }

            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = from p in item.Result.Item1
                       select new
                       {
                           id = p.CustomCylId,
                           name = FormatNameForDisplay(p.Name),
                           code = p.Code,
                           description = p.Description,
                           orderId = p.OrderId,
                           isActive = p.IsActive ? "فعال" : "غیرفعال"
                       }
            };
            return Json(jsonData);
        }

        [Authorize(Roles = "admin,CustomLensCyl_Create")]
        public IActionResult Create()
        {
            return View(new TblLnsCustomCylDto { IsActive = true, OrderId = 1 });
        }

        [Authorize(Roles = "admin,CustomLensCyl_Create")]
        [HttpPost]
        public IActionResult Create(TblLnsCustomCylDto dto)
        {
            try
            {
                tblLnsCustomCylService.Add(dto);
                return Json(new { success = true, message = "" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch
            {
                return Json(new { success = false, message = "" });
            }
        }

        [Authorize(Roles = "admin,CustomLensCyl_Edit")]
        public IActionResult Edit(int id)
        {
            var item = tblLnsCustomCylService.GetById(id);
            return View(item.Result);
        }

        [Authorize(Roles = "admin,CustomLensCyl_Edit")]
        [HttpPost]
        public IActionResult Edit(TblLnsCustomCylDto dto)
        {
            try
            {
                tblLnsCustomCylService.Update(dto);
                return Json(new { success = true, message = "" });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch
            {
                return Json(new { success = false, message = "" });
            }
        }

        [Authorize(Roles = "admin,CustomLensCyl_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            tblLnsCustomCylService.Delete(id);
            return Json(new { success = true, message = "" });
        }

        [Authorize(Roles = "admin,CustomLensCyl_Index")]
        [HttpGet]
        public IActionResult ExportExcel()
        {
            var items = tblLnsCustomCylService.GetAll().Result
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomCylId)
                .ToList();

            using var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("CustomCyl");

            ws.Cell(1, 1).Value = "نام";
            ws.Cell(1, 2).Value = "کد";
            ws.Cell(1, 3).Value = "توضیحات";
            ws.Cell(1, 4).Value = "ترتیب نمایش";
            ws.Cell(1, 5).Value = "وضعیت";

            var row = 2;
            foreach (var item in items)
            {
                ws.Cell(row, 1).Value = FormatNameForDisplay(item.Name);
                ws.Cell(row, 2).Value = item.Code ?? string.Empty;
                ws.Cell(row, 3).Value = item.Description ?? string.Empty;
                ws.Cell(row, 4).Value = item.OrderId;
                ws.Cell(row, 5).Value = item.IsActive ? "فعال" : "غیرفعال";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomLensCyl.xlsx");
        }

        [Authorize(Roles = "admin,CustomLensCyl_Create")]
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "No file selected." });
            }

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (extension != ".xlsx" && extension != ".xls")
            {
                return Json(new { success = false, message = "Invalid file format. Use xlsx or xls." });
            }

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var rows = new List<(int Row, string Name, string Code, string Description, int OrderId, bool IsActive)>();
                var errors = new List<string>();
                var orderIdRows = new Dictionary<int, List<int>>();

                using var stream = file.OpenReadStream();
                using var reader = ExcelReaderFactory.CreateReader(stream);

                var rowIndex = 0;
                while (reader.Read())
                {
                    rowIndex++;
                    if (rowIndex == 1 && LooksLikeHeader(reader))
                    {
                        continue;
                    }

                    if (reader.FieldCount < 4)
                    {
                        errors.Add($"Row {rowIndex}: invalid column count.");
                        continue;
                    }

                    var name = ReadCellString(reader, 0);
                    var code = ReadCellString(reader, 1);
                    var description = ReadCellString(reader, 2);
                    var orderId = ReadCellInt(reader, 3);
                    var statusText = ReadCellString(reader, 4);
                    var isActive = ParseStatus(statusText, out var statusValid);

                    if (string.IsNullOrWhiteSpace(name) &&
                        string.IsNullOrWhiteSpace(code) &&
                        string.IsNullOrWhiteSpace(description) &&
                        orderId == null)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        errors.Add($"Row {rowIndex}: Name is required.");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(code))
                    {
                        errors.Add($"Row {rowIndex}: Code is required.");
                        continue;
                    }

                    if (!orderId.HasValue || orderId.Value <= 0)
                    {
                        errors.Add($"Row {rowIndex}: OrderId must be a positive number.");
                        continue;
                    }

                    if (!statusValid)
                    {
                        errors.Add($"Row {rowIndex}: invalid status value.");
                        continue;
                    }

                    rows.Add((rowIndex, name, code, description ?? string.Empty, orderId.Value, isActive));
                    if (!orderIdRows.TryGetValue(orderId.Value, out var list))
                    {
                        list = new List<int>();
                        orderIdRows[orderId.Value] = list;
                    }
                    list.Add(rowIndex);
                }

                if (rows.Count == 0 && errors.Count == 0)
                {
                    errors.Add("No valid rows were found in the file.");
                }

                foreach (var dup in orderIdRows.Where(x => x.Value.Count > 1))
                {
                    errors.Add($"Duplicate OrderId {dup.Key} in file.");
                }

                var existingOrderIds = tblLnsCustomCylService.GetAll().Result.Select(x => x.OrderId).ToHashSet();
                foreach (var row in rows)
                {
                    if (existingOrderIds.Contains(row.OrderId))
                    {
                        errors.Add($"Row {row.Row}: OrderId {row.OrderId} already exists.");
                    }
                }

                if (errors.Count > 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Excel import failed.",
                        errors
                    });
                }

                foreach (var row in rows)
                {
                    var dto = new TblLnsCustomCylDto
                    {
                        Name = row.Name,
                        Code = row.Code,
                        Description = row.Description,
                        OrderId = row.OrderId,
                        IsActive = row.IsActive
                    };
                    tblLnsCustomCylService.Add(dto);
                }

                return Json(new { success = true, message = $"Import completed. Count: {rows.Count}" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Excel read error.", errors = new[] { ex.Message } });
            }
        }

        private static bool LooksLikeHeader(IExcelDataReader reader)
        {
            var first = ReadCellString(reader, 0);
            return !string.IsNullOrWhiteSpace(first) &&
                   (first.Contains("name", StringComparison.OrdinalIgnoreCase) || first.Contains("نام", StringComparison.OrdinalIgnoreCase));
        }

        private static string? ReadCellString(IExcelDataReader reader, int index)
        {
            if (index >= reader.FieldCount)
            {
                return null;
            }

            var value = reader.GetValue(index);
            return value?.ToString()?.Trim();
        }

        private static int? ReadCellInt(IExcelDataReader reader, int index)
        {
            var value = ReadCellString(reader, index);
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (int.TryParse(value, out var result))
            {
                return result;
            }

            if (double.TryParse(value, out var dbl))
            {
                return (int)dbl;
            }

            return null;
        }

        private static bool ParseStatus(string? value, out bool valid)
        {
            valid = true;
            if (string.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            value = value.Trim();
            if (value.Equals("فعال", StringComparison.OrdinalIgnoreCase) || value.Equals("1") || value.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (value.Equals("غیرفعال", StringComparison.OrdinalIgnoreCase) || value.Equals("0") || value.Equals("false", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            valid = false;
            return true;
        }

        private static string FormatNameForDisplay(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            if (decimal.TryParse(name, NumberStyles.Any, CultureInfo.InvariantCulture, out var number) ||
                decimal.TryParse(name, NumberStyles.Any, CultureInfo.CurrentCulture, out number))
            {
                return number.ToString("0.00", CultureInfo.InvariantCulture);
            }

            return name;
        }
    }
}
