using ClosedXML.Excel;
using ExcelDataReader;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HamrahanSystem.Presntation.Controllers
{
    [Authorize]
    public class CustomLensAdditionValuesController(ITblLnsCustomDesignTypeAdditionService tblLnsCustomDesignTypeAdditionService, ITblLnsDesignTypeService tblLnsDesignTypeService, ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsBrandService tblLnsBrandService, ITblClrDefineObjectService tblClrDefineObjectService) : Controller
    {
        private const bool CustomFlag = true;

        [Authorize(Roles = "admin,CustomLensAddition_Index")]
        public IActionResult Index(int? designTypeId, bool embedded = false)
        {
            ViewBag.Embedded = embedded;
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                var designType = tblLnsDesignTypeService.GetById(designTypeId.Value).Result;
                if (designType == null || designType.IsSpecial == false)
                {
                    return NotFound();
                }

                ViewBag.DesignTypeId = designTypeId.Value;
                ViewBag.DesignTypeName = "All Design Types";

                if (designType.LensTypeId.HasValue)
                {
                    var lensType = tblLnsLensTypeService.GetById(designType.LensTypeId.Value).Result;
                    ViewBag.LensTypeName = "All Lens Types";
                    if (lensType?.BrandId != null)
                    {
                        var brand = tblLnsBrandService.GetById(lensType.BrandId.Value).Result;
                        ViewBag.BrandName = "All Brands";
                    }
                    else
                    {
                        ViewBag.BrandName = "All Brands";
                    }
                }
                else
                {
                    ViewBag.LensTypeName = "All Lens Types";
                    ViewBag.BrandName = "All Brands";
                }
            }
            else
            {
                ViewBag.DesignTypeId = 0;
                ViewBag.DesignTypeName = "All Design Types";
                ViewBag.LensTypeName = "All Lens Types";
                ViewBag.BrandName = "All Brands";
            }

            return View();
        }

        [Authorize(Roles = "admin,CustomLensAddition_Index")]
        public JsonResult Detail(int? designTypeId)
        {
            var payload = GetRequestPayload();
            var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
            int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
            int pageSize = itemGrid.Rows;

            var allItems = tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>();
            var filtered = allItems;
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                filtered = filtered.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var designTypes = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
                .Where(x => x != null && x.IsSpecial == CustomFlag)
                .ToList();
            var lensTypes = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>()).ToList();
            var brands = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>()).ToList();

            var designTypeLookup = designTypes.ToDictionary(x => x.DesignTypeId, x => x);
            var lensTypeLookup = lensTypes.ToDictionary(x => x.LensTypeId, x => x);
            var brandLookup = brands.ToDictionary(x => x.BrandId, x => x.Name ?? string.Empty);
            var defineObjects = (tblClrDefineObjectService.GetAll().Result ?? Enumerable.Empty<TblClrDefineObjectDto>()).ToList();
            var defineObjectLookup = defineObjects.ToDictionary(x => x.DefineObjectId, x => x);

            filtered = ApplySort(filtered, itemGrid.Sidx, itemGrid.Sord);

            var totalRecords = filtered.Count();
            if (totalRecords <= 0)
            {
                return Json(new { total = 0, page = itemGrid.Page, records = 0, rows = Array.Empty<object>() });
            }

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var paged = filtered.Skip(pageIndex * pageSize).Take(pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = itemGrid.Page,
                records = totalRecords,
                rows = from p in paged
                       let designType = (p.DesignTypeId.HasValue && designTypeLookup.ContainsKey(p.DesignTypeId.Value)) ? designTypeLookup[p.DesignTypeId.Value] : null
                       let lensType = (designType != null && designType.LensTypeId.HasValue && lensTypeLookup.ContainsKey(designType.LensTypeId.Value)) ? lensTypeLookup[designType.LensTypeId.Value] : null
                       let brandName = (lensType != null && lensType.BrandId.HasValue && brandLookup.ContainsKey(lensType.BrandId.Value)) ? brandLookup[lensType.BrandId.Value] : ""
                       select new
                       {
                           id = p.CustomDesignTypeAdditionId,
                           brandName = brandName,
                           lensTypeName = lensType?.Name ?? "",
                           designTypeName = designType?.Name ?? "",
                           defineObjectText = (p.DefineObjectId.HasValue && defineObjectLookup.TryGetValue(p.DefineObjectId.Value, out var defObj))
                               ? BuildDefineObjectText(defObj)
                               : "",
                           additionValue = FormatAdditionDisplay(p.AdditionValue),
                           orderId = p.OrderId,
                           isActive = p.IsActive == true ? "Active" : "Inactive"
                       }
            };
            return Json(jsonData);
        }

        [Authorize(Roles = "admin,CustomLensAddition_Create")]
        public IActionResult Create(int? designTypeId)
        {
            if (!designTypeId.HasValue || designTypeId.Value <= 0)
            {
                return NotFound();
            }

            var designType = tblLnsDesignTypeService.GetById(designTypeId.Value).Result;
            if (designType == null || designType.IsSpecial == false)
            {
                return NotFound();
            }

            var dto = new TblLnsCustomDesignTypeAdditionDto
            {
                DesignTypeId = designTypeId.Value,
                        DefineObjectId = null,
                OrderId = 1,
                IsActive = true
            };
            return View(dto);
        }

        [Authorize(Roles = "admin,CustomLensAddition_Create")]
        [HttpPost]
        public IActionResult Create(TblLnsCustomDesignTypeAdditionDto dto)
        {
            try
            {
                if (!dto.DesignTypeId.HasValue || dto.DesignTypeId.Value <= 0)
                {
                    return Json(new { success = false, message = "Design Type is invalid." });
                }

                var designType = tblLnsDesignTypeService.GetById(dto.DesignTypeId.Value).Result;
                if (designType == null || designType.IsSpecial == false)
                {
                    return Json(new { success = false, message = "Design Type is invalid." });
                }

                var additionValue = ParseAdditionValue(Request.Form["AdditionValue"].FirstOrDefault(), out var parseError);
                if (parseError != null)
                {
                    return Json(new { success = false, message = parseError });
                }
                dto.AdditionValue = additionValue;

                if (dto.DefineObjectId.HasValue && dto.DefineObjectId.Value > 0)
                {
                    var defineObject = tblClrDefineObjectService.GetById(dto.DefineObjectId.Value).Result;
                    if (defineObject == null)
                    {
                        return Json(new { success = false, message = "Invalid request." });
                    }
                }
                else
                {
                    dto.DefineObjectId = null;
                }

                if (dto.OrderId <= 0)
                {
                    return Json(new { success = false, message = "Invalid request." });
                }

                var existing = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
                    .Where(x => x.DesignTypeId == dto.DesignTypeId.Value)
                    .ToList();

                if (existing.Any(x => x.OrderId == dto.OrderId))
                {
                    return Json(new { success = false, message = "Invalid request." });
                }
                if (existing.Any(x => x.AdditionValue.HasValue && dto.AdditionValue.HasValue && x.AdditionValue.Value == dto.AdditionValue.Value))
                {
                    return Json(new { success = false, message = "Invalid request." });
                }

                tblLnsCustomDesignTypeAdditionService.Add(dto);
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

        [Authorize(Roles = "admin,CustomLensAddition_Edit")]
        public IActionResult Edit(int id, int? designTypeId)
        {
            var item = tblLnsCustomDesignTypeAdditionService.GetById(id).Result;
            if (item == null || (designTypeId.HasValue && designTypeId.Value > 0 && item.DesignTypeId != designTypeId.Value))
            {
                return NotFound();
            }

            ViewBag.DesignTypeId = designTypeId ?? 0;
            if (item.DefineObjectId.HasValue && item.DefineObjectId.Value > 0)
            {
                var defineObject = tblClrDefineObjectService.GetById(item.DefineObjectId.Value).Result;
                if (defineObject != null)
                {
                    ViewBag.ListDefineObject = new List<SelectListItem>
                    {
                        new SelectListItem
                        {
                            Value = defineObject.DefineObjectId.ToString(),
                            Text = BuildDefineObjectText(defineObject),
                            Selected = true
                        }
                    };
                }
            }
            else
            {
                ViewBag.ListDefineObject = new List<SelectListItem>();
            }
            return View(item);
        }

        [Authorize(Roles = "admin,CustomLensAddition_Edit")]
        [HttpPost]
        public IActionResult Edit(TblLnsCustomDesignTypeAdditionDto dto)
        {
            try
            {
                var existing = tblLnsCustomDesignTypeAdditionService.GetById(dto.CustomDesignTypeAdditionId).Result;
                if (existing == null || (dto.DesignTypeId.HasValue && dto.DesignTypeId.Value > 0 && existing.DesignTypeId != dto.DesignTypeId))
                {
                    return Json(new { success = false, message = "Invalid request." });
                }

                var additionValue = ParseAdditionValue(Request.Form["AdditionValue"].FirstOrDefault(), out var parseError);
                if (parseError != null)
                {
                    return Json(new { success = false, message = parseError });
                }

                if (dto.OrderId <= 0)
                {
                    return Json(new { success = false, message = "Invalid request." });
                }

                var siblings = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
                    .Where(x => x.DesignTypeId == existing.DesignTypeId && x.CustomDesignTypeAdditionId != existing.CustomDesignTypeAdditionId)
                    .ToList();

                if (siblings.Any(x => x.OrderId == dto.OrderId))
                {
                    return Json(new { success = false, message = "Invalid request." });
                }
                if (siblings.Any(x => x.AdditionValue.HasValue && additionValue.HasValue && x.AdditionValue.Value == additionValue.Value))
                {
                    return Json(new { success = false, message = "Invalid request." });
                }

                if (dto.DefineObjectId.HasValue && dto.DefineObjectId.Value > 0)
                {
                    var defineObject = tblClrDefineObjectService.GetById(dto.DefineObjectId.Value).Result;
                    if (defineObject == null)
                    {
                        return Json(new { success = false, message = "Warehouse item is invalid." });
                    }
                }
                else
                {
                    dto.DefineObjectId = null;
                }

                existing.AdditionValue = additionValue;
                existing.OrderId = dto.OrderId;
                existing.IsActive = dto.IsActive;
                existing.DefineObjectId = dto.DefineObjectId;

                tblLnsCustomDesignTypeAdditionService.Update(existing);
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

        [Authorize(Roles = "admin,CustomLensAddition_Delete")]
        [HttpPost]
        public IActionResult Delete(int id, int? designTypeId)
        {
            var item = tblLnsCustomDesignTypeAdditionService.GetById(id).Result;
            if (item == null || (designTypeId.HasValue && designTypeId.Value > 0 && item.DesignTypeId != designTypeId.Value))
            {
                return Json(new { success = false, message = "Invalid request." });
            }

            tblLnsCustomDesignTypeAdditionService.Delete(id);
            return Json(new { success = true, message = "" });
        }

        [Authorize(Roles = "admin,CustomLensAddition_Index")]
        [HttpGet]
        public IActionResult ExportExcel(int? designTypeId)
        {
            var items = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>());
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                items = items.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var ordered = items
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomDesignTypeAdditionId)
                .ToList();

            using var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("CustomDesignTypeAddition");

            ws.Cell(1, 1).Value = "Addition";
            ws.Cell(1, 2).Value = "Display Order";
            ws.Cell(1, 3).Value = "Status";

            var row = 2;
            foreach (var item in ordered)
            {
                ws.Cell(row, 1).Value = item.AdditionValue;
                ws.Cell(row, 2).Value = item.OrderId;
                ws.Cell(row, 3).Value = item.IsActive ? "Active" : "Inactive";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomDesignTypeAddition.xlsx");
        }

        [Authorize(Roles = "admin,CustomLensAddition_Create")]
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file, int? designTypeId)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "Invalid request." });
            }
            if (!designTypeId.HasValue || designTypeId.Value <= 0)
            {
                return Json(new { success = false, message = "Design Type is invalid." });
            }

            var designType = tblLnsDesignTypeService.GetById(designTypeId.Value).Result;
            if (designType == null || designType.IsSpecial == false)
            {
                return Json(new { success = false, message = "Design Type is invalid." });
            }

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (extension != ".xlsx" && extension != ".xls")
            {
                return Json(new { success = false, message = "Invalid request." });
            }

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var rows = new List<(int Row, decimal AdditionValue, int OrderId, bool IsActive)>();
                var errors = new List<string>();
                var orderIdRows = new Dictionary<int, List<int>>();
                var additionRows = new Dictionary<decimal, List<int>>();
                var columnMap = new ExcelColumnMap();

                using var stream = file.OpenReadStream();
                using var reader = ExcelReaderFactory.CreateReader(stream);

                var rowIndex = 0;
                while (reader.Read())
                {
                    rowIndex++;
                    if (rowIndex == 1 && TryReadHeader(reader, columnMap))
                        continue;

                    if (reader.FieldCount < 2)
                    {
                        errors.Add($"Row {rowIndex}: Invalid columns. Required: Addition, Order, Status (optional).");
                        continue;
                    }

                    var defineObjectText = columnMap.DefineObjectIndex >= 0 ? ReadCellString(reader, columnMap.DefineObjectIndex) : string.Empty;
                    var additionText = ReadCellString(reader, columnMap.AdditionIndex);
                    var orderId = ReadCellInt(reader, columnMap.OrderIndex);
                    var statusText = ReadCellString(reader, columnMap.StatusIndex);
                    var isActive = ParseStatus(statusText, out var statusValid);

                    if (string.IsNullOrWhiteSpace(additionText) && orderId == null && string.IsNullOrWhiteSpace(defineObjectText))
                    {
                        continue;
                    }

                    var additionValue = ParseAdditionValue(additionText, out var additionError);
                    if (additionError != null)
                    {
                        errors.Add($"Row {rowIndex}: {additionError}");
                        continue;
                    }

                    if (!orderId.HasValue)
                    {
                        errors.Add($"Row {rowIndex}: Invalid data.");
                        continue;
                    }
                    if (orderId.Value <= 0)
                    {
                        errors.Add($"Row {rowIndex}: Invalid data.");
                        continue;
                    }
                    if (!statusValid)
                    {
                        errors.Add($"Row {rowIndex}: Invalid data.");
                        continue;
                    }

                    rows.Add((rowIndex, additionValue.Value, orderId.Value, isActive));

                    if (!orderIdRows.TryGetValue(orderId.Value, out var orderList))
                    {
                        orderList = new List<int>();
                        orderIdRows[orderId.Value] = orderList;
                    }
                    orderList.Add(rowIndex);

                    if (!additionRows.TryGetValue(additionValue.Value, out var additionList))
                    {
                        additionList = new List<int>();
                        additionRows[additionValue.Value] = additionList;
                    }
                    additionList.Add(rowIndex);
                }

                if (rows.Count == 0 && errors.Count == 0)
                {
                    errors.Add("Invalid data.");
                }

                foreach (var dup in orderIdRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join(", ", dup.Value);
                    errors.Add("Invalid data.");
                }

                foreach (var dup in additionRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join(", ", dup.Value);
                    errors.Add("Invalid data.");
                }

                var existing = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
                    .Where(x => x.DesignTypeId == designTypeId.Value)
                    .ToList();

                var existingOrderIds = existing.Select(x => x.OrderId).ToHashSet();
                var existingAdditions = new HashSet<decimal>(
                    existing.Where(x => x.AdditionValue.HasValue).Select(x => x.AdditionValue!.Value));

                foreach (var row in rows)
                {
                    if (existingOrderIds.Contains(row.OrderId))
                    {
                        errors.Add($"Row {row.Row}: Invalid data.");
                    }
                    if (existingAdditions.Contains(row.AdditionValue))
                    {
                        errors.Add($"Row {row.Row}: Invalid data.");
                    }
                }

                if (errors.Count > 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Import failed. Please review the errors.",
                        errors
                    });
                }

                foreach (var row in rows)
                {
                    var dto = new TblLnsCustomDesignTypeAdditionDto
                    {
                        DesignTypeId = designTypeId.Value,
                        DefineObjectId = null,
                        AdditionValue = row.AdditionValue,
                        OrderId = row.OrderId,
                        IsActive = row.IsActive
                    };
                    tblLnsCustomDesignTypeAdditionService.Add(dto);
                }

                return Json(new { success = true, message = $"Import completed. Rows: {rows.Count}" });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "Error reading file."
                });
            }
        }

        private static IEnumerable<TblLnsCustomDesignTypeAdditionDto> ApplySort(IEnumerable<TblLnsCustomDesignTypeAdditionDto> items, string? sidx, string? sord)
        {
            if (string.IsNullOrWhiteSpace(sidx))
            {
                return items.OrderBy(x => x.OrderId).ThenBy(x => x.CustomDesignTypeAdditionId);
            }

            var prop = typeof(TblLnsCustomDesignTypeAdditionDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return items.OrderBy(x => x.OrderId).ThenBy(x => x.CustomDesignTypeAdditionId);
            }

            return string.Equals(sord, "desc", StringComparison.OrdinalIgnoreCase)
                ? items.OrderByDescending(x => prop.GetValue(x, null))
                : items.OrderBy(x => prop.GetValue(x, null));
        }

                        private static bool TryReadHeader(IExcelDataReader reader, ExcelColumnMap map)
        {
            var c0 = ReadCellString(reader, 0).ToLowerInvariant();
            var c1 = ReadCellString(reader, 1).ToLowerInvariant();
            var c2 = ReadCellString(reader, 2).ToLowerInvariant();
            var c3 = ReadCellString(reader, 3).ToLowerInvariant();

            var hasDefineObject = c0.Contains("warehouse") || c0.Contains("stock") || c0.Contains("item") || c0.Contains("define");
            var hasAddition = c0.Contains("addition") || c1.Contains("addition");

            if (hasDefineObject)
            {
                map.DefineObjectIndex = 0;
                map.AdditionIndex = 1;
                map.OrderIndex = 2;
                map.StatusIndex = 3;
                return true;
            }

            if (hasAddition || c0.Contains("order") || c1.Contains("order") || c2.Contains("order"))
            {
                map.DefineObjectIndex = -1;
                map.AdditionIndex = 0;
                map.OrderIndex = 1;
                map.StatusIndex = 2;
                return true;
            }

            return false;
        }
private static string ReadCellString(IExcelDataReader reader, int index)
        {
            if (index < 0 || index >= reader.FieldCount)
                return string.Empty;
            var value = reader.GetValue(index);
            return value?.ToString()?.Trim() ?? string.Empty;
        }

        private static int? ReadCellInt(IExcelDataReader reader, int index)
        {
            if (index < 0 || index >= reader.FieldCount)
                return null;
            var value = reader.GetValue(index);
            if (value == null)
                return null;
            if (value is double d)
                return Convert.ToInt32(d);
            if (value is int i)
                return i;
            var text = value.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(text))
                return null;
            if (int.TryParse(text, out var parsed))
                return parsed;
            if (double.TryParse(text, out var parsedDouble))
                return Convert.ToInt32(parsedDouble);
            return null;
        }

                        private static bool ParseStatus(string? text, out bool isValid)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                isValid = true;
                return true;
            }

            var normalized = text.Trim().ToLowerInvariant();
            if (normalized == "1" || normalized == "true" || normalized == "active")
            {
                isValid = true;
                return true;
            }
            if (normalized == "0" || normalized == "false" || normalized == "inactive")
            {
                isValid = true;
                return false;
            }

            isValid = false;
            return true;
        }
        private static decimal? ParseAdditionValue(string? value, out string? error)
        {
            var normalized = NormalizeNumber(value);
            if (string.IsNullOrWhiteSpace(normalized))
            {
                error = "Addition value is required.";
                return null;
            }

            if (!decimal.TryParse(normalized, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var parsed))
            {
                error = "Invalid addition value. Enter a number.";
                return null;
            }
            if (parsed <= 0)
            {
                error = "Addition value must be greater than zero.";
                return null;
            }

            error = null;
            return parsed;
        }
        private static string FormatAdditionDisplay(decimal? value)
        {
            if (!value.HasValue)
            {
                return string.Empty;
            }

            return value.Value.ToString("0.00", CultureInfo.InvariantCulture).Replace('.', ',');
        }
private static string NormalizeNumber(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var trimmed = value.Trim();
            var sb = new StringBuilder(trimmed.Length);
            foreach (var ch in trimmed)
            {
                                sb.Append(ch switch
                {
                    '\u06F0' => '0',
                    '\u06F1' => '1',
                    '\u06F2' => '2',
                    '\u06F3' => '3',
                    '\u06F4' => '4',
                    '\u06F5' => '5',
                    '\u06F6' => '6',
                    '\u06F7' => '7',
                    '\u06F8' => '8',
                    '\u06F9' => '9',
                    '\u0660' => '0',
                    '\u0661' => '1',
                    '\u0662' => '2',
                    '\u0663' => '3',
                    '\u0664' => '4',
                    '\u0665' => '5',
                    '\u0666' => '6',
                    '\u0667' => '7',
                    '\u0668' => '8',
                    '\u0669' => '9',
                    ',' => '.',
                    '/' => '.',
                    _ => ch
                });
            }

            return sb.ToString();
        }

        [HttpPost]
        public JsonResult SearchDefineObject(string search)
        {
            var results = tblClrDefineObjectService.Search(search).Result ?? new List<TblClrDefineObjectDto>();
            var items = new
            {
                results = results.Select(item => new
                {
                    id = item.DefineObjectId,
                    text = BuildDefineObjectText(item)
                })
            };
            return Json(items);
        }

        private int? ResolveDefineObjectId(string rawValue, out string? error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return null;
            }

            var term = rawValue.Trim();
            var candidates = tblClrDefineObjectService.Search(term).Result ?? new List<TblClrDefineObjectDto>();
            var exact = candidates
                .Where(x =>
                    string.Equals(x.CodeObject, term, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(x.NameObject, term, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(x.TechnicalSpecs, term, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (exact.Count == 1)
            {
                return exact[0].DefineObjectId;
            }
            if (exact.Count > 1)
            {
                error = $"Warehouse item is duplicated: {term}.";
                return null;
            }

            error = $"Warehouse item not found: {term}.";
            return null;
        }

        private static string BuildDefineObjectText(TblClrDefineObjectDto item)
        {
            if (item == null)
                return string.Empty;

            var parts = new List<string>();
            if (!string.IsNullOrWhiteSpace(item.CodeObject))
            {
                parts.Add(item.CodeObject.Trim());
            }
            if (!string.IsNullOrWhiteSpace(item.NameObject))
            {
                parts.Add(item.NameObject.Trim());
            }
            if (!string.IsNullOrWhiteSpace(item.TechnicalSpecs))
            {
                parts.Add(item.TechnicalSpecs.Trim());
            }
            return string.Join(" - ", parts);
        }

        private sealed class ExcelColumnMap
        {
            public int DefineObjectIndex { get; set; } = -1;
            public int AdditionIndex { get; set; } = 0;
            public int OrderIndex { get; set; } = 1;
            public int StatusIndex { get; set; } = 2;
        }

        private string GetRequestPayload()
        {
            if (Request.HasFormContentType && Request.Form.Count > 0)
            {
                return Request.Form.First().Key;
            }

            Request.EnableBuffering();
            using var reader = new StreamReader(Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
            var payload = reader.ReadToEnd();
            Request.Body.Position = 0;
            return string.IsNullOrWhiteSpace(payload) ? "{}" : payload;
        }
    }
}




















