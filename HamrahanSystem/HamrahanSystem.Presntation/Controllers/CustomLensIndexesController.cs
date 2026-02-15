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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HamrahanSystem.Presntation.Controllers
{
    [Authorize]
    public class CustomLensIndexesController(ITblLnsCustomLensIndexService tblLnsCustomLensIndexService, ITblLnsDesignTypeService tblLnsDesignTypeService, ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsBrandService tblLnsBrandService) : Controller
    {
        private const bool CustomFlag = true;

        [Authorize(Roles = "admin,CustomLensIndex_Index")]
        public IActionResult Index(int? designTypeId, bool embedded = false)
        {
            ViewBag.Embedded = embedded;
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                if (!TryGetHierarchy(designTypeId.Value, requireActive: true, out var designType, out var lensType, out var brand))
                {
                    return NotFound();
                }

                ViewBag.DesignTypeId = designTypeId.Value;
                ViewBag.DesignTypeName = designType.Name ?? string.Empty;
                ViewBag.LensTypeName = lensType?.Name ?? string.Empty;
                ViewBag.BrandName = brand?.Name ?? string.Empty;
            }
            else
            {
                ViewBag.DesignTypeId = 0;
                ViewBag.DesignTypeName = "همه Design Type ها";
                ViewBag.LensTypeName = "همه Lens Type ها";
                ViewBag.BrandName = "همه برندها";
            }

            return View();
        }

        [Authorize(Roles = "admin,CustomLensIndex_Index")]
        public JsonResult Detail(int? designTypeId)
        {
            var payload = GetRequestPayload();
            var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
            int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
            int pageSize = itemGrid.Rows;

            var allItems = tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>();
            var filtered = allItems;
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                filtered = filtered.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var designTypes = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
                .Where(x => x.IsSpecial == CustomFlag)
                .ToDictionary(x => x.DesignTypeId, x => x);
            var lensTypes = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
                .ToDictionary(x => x.LensTypeId, x => x);
            var brands = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
                .ToDictionary(x => x.BrandId, x => x.Name ?? string.Empty);

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
                       select new
                       {
                           id = p.CustomLensIndexId,
                           brandName = GetBrandName(p.DesignTypeId, designTypes, lensTypes, brands),
                           lensTypeName = GetLensTypeName(p.DesignTypeId, designTypes, lensTypes),
                           designTypeName = GetDesignTypeName(p.DesignTypeId, designTypes),
                           lensIndexName = p.LensIndexName,
                           orderId = p.OrderId,
                           isActive = p.IsActive == true ? "فعال" : "غیرفعال",
                           hasColoringType = p.HasColoringType
                       }
            };
            return Json(jsonData);
        }

        [Authorize(Roles = "admin,CustomLensIndex_Create")]
        public IActionResult Create(int? designTypeId)
        {
            if (!designTypeId.HasValue || designTypeId.Value <= 0)
            {
                return NotFound();
            }

            if (!TryGetHierarchy(designTypeId.Value, requireActive: true, out var designType, out var lensType, out var brand))
            {
                return NotFound();
            }

            ViewBag.BrandName = brand?.Name ?? string.Empty;
            ViewBag.LensTypeName = lensType?.Name ?? string.Empty;
            ViewBag.DesignTypeName = designType.Name ?? string.Empty;

            var dto = new TblLnsCustomLensIndexDto
            {
                DesignTypeId = designTypeId.Value,
                OrderId = 1,
                IsActive = true,
                HasColoringType = false
            };
            return View(dto);
        }

        [Authorize(Roles = "admin,CustomLensIndex_Create")]
        [HttpPost]
        public IActionResult Create(TblLnsCustomLensIndexDto dto)
        {
            try
            {
                if (!dto.DesignTypeId.HasValue || dto.DesignTypeId.Value <= 0)
                {
                    return Json(new { success = false, message = "Design Type نامعتبر است." });
                }

                if (!TryGetHierarchy(dto.DesignTypeId.Value, requireActive: true, out _, out _, out _))
                {
                    return Json(new { success = false, message = "Design Type نامعتبر است." });
                }

                dto.LensIndexName = Normalize(dto.LensIndexName);
                if (string.IsNullOrWhiteSpace(dto.LensIndexName))
                {
                    return Json(new { success = false, message = "ستون Lens Index خالی است." });
                }
                if (dto.OrderId <= 0)
                {
                    return Json(new { success = false, message = "ترتیب نمایش باید عدد مثبت باشد." });
                }

                var existing = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>())
                    .Where(x => x.DesignTypeId == dto.DesignTypeId.Value)
                    .ToList();

                if (existing.Any(x => x.OrderId == dto.OrderId))
                {
                    return Json(new { success = false, message = "ترتیب نمایش تکراری است." });
                }
                if (existing.Any(x => string.Equals(Normalize(x.LensIndexName), dto.LensIndexName, StringComparison.OrdinalIgnoreCase)))
                {
                    return Json(new { success = false, message = "این Lens Index قبلاً ثبت شده است." });
                }

                tblLnsCustomLensIndexService.Add(dto);
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

        [Authorize(Roles = "admin,CustomLensIndex_Edit")]
        public IActionResult Edit(int id, int? designTypeId)
        {
            var item = tblLnsCustomLensIndexService.GetById(id).Result;
            if (item == null || (designTypeId.HasValue && designTypeId.Value > 0 && item.DesignTypeId != designTypeId.Value))
            {
                return NotFound();
            }

            ViewBag.DesignTypeId = designTypeId ?? 0;
            if (item.DesignTypeId.HasValue && TryGetHierarchy(item.DesignTypeId.Value, requireActive: false, out var designType, out var lensType, out var brand))
            {
                ViewBag.BrandName = brand?.Name ?? string.Empty;
                ViewBag.LensTypeName = lensType?.Name ?? string.Empty;
                ViewBag.DesignTypeName = designType.Name ?? string.Empty;
            }
            else
            {
                ViewBag.BrandName = string.Empty;
                ViewBag.LensTypeName = string.Empty;
                ViewBag.DesignTypeName = string.Empty;
            }
            return View(item);
        }

        [Authorize(Roles = "admin,CustomLensIndex_Edit")]
        [HttpPost]
        public IActionResult Edit(TblLnsCustomLensIndexDto dto)
        {
            try
            {
                var existing = tblLnsCustomLensIndexService.GetById(dto.CustomLensIndexId).Result;
                if (existing == null || (dto.DesignTypeId.HasValue && dto.DesignTypeId.Value > 0 && existing.DesignTypeId != dto.DesignTypeId))
                {
                    return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
                }

                existing.LensIndexName = Normalize(dto.LensIndexName);
                existing.OrderId = dto.OrderId;
                existing.IsActive = dto.IsActive;
                existing.HasColoringType = dto.HasColoringType;

                if (string.IsNullOrWhiteSpace(existing.LensIndexName))
                {
                    return Json(new { success = false, message = "ستون Lens Index خالی است." });
                }
                if (existing.OrderId <= 0)
                {
                    return Json(new { success = false, message = "ترتیب نمایش باید عدد مثبت باشد." });
                }

                var siblings = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>())
                    .Where(x => x.DesignTypeId == existing.DesignTypeId && x.CustomLensIndexId != existing.CustomLensIndexId)
                    .ToList();

                if (siblings.Any(x => x.OrderId == existing.OrderId))
                {
                    return Json(new { success = false, message = "ترتیب نمایش تکراری است." });
                }
                if (siblings.Any(x => string.Equals(Normalize(x.LensIndexName), existing.LensIndexName, StringComparison.OrdinalIgnoreCase)))
                {
                    return Json(new { success = false, message = "این Lens Index قبلاً ثبت شده است." });
                }

                tblLnsCustomLensIndexService.Update(existing);
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

        [Authorize(Roles = "admin,CustomLensIndex_Delete")]
        [HttpPost]
        public IActionResult Delete(int id, int? designTypeId)
        {
            var item = tblLnsCustomLensIndexService.GetById(id).Result;
            if (item == null || (designTypeId.HasValue && designTypeId.Value > 0 && item.DesignTypeId != designTypeId.Value))
            {
                return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
            }

            tblLnsCustomLensIndexService.Delete(id);
            return Json(new { success = true, message = "" });
        }

        [Authorize(Roles = "admin,CustomLensIndex_Index")]
        [HttpGet]
        public IActionResult ExportExcel(int? designTypeId)
        {
            var items = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>());
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                items = items.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var ordered = items
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomLensIndexId)
                .ToList();

            using var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("CustomLensIndex");

            ws.Cell(1, 1).Value = "Lens Index";
            ws.Cell(1, 2).Value = "ترتیب نمایش";
            ws.Cell(1, 3).Value = "وضعیت";
            ws.Cell(1, 4).Value = "Coloring Type";

            var row = 2;
            foreach (var item in ordered)
            {
                ws.Cell(row, 1).Value = Normalize(item.LensIndexName);
                ws.Cell(row, 2).Value = item.OrderId;
                ws.Cell(row, 3).Value = item.IsActive ? "فعال" : "غیرفعال";
                ws.Cell(row, 4).Value = item.HasColoringType ? "دارد" : "ندارد";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomLensIndex.xlsx");
        }

        [Authorize(Roles = "admin,CustomLensIndex_Create")]
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file, int? designTypeId)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "فایلی انتخاب نشده است." });
            }
            if (!designTypeId.HasValue || designTypeId.Value <= 0)
            {
                return Json(new { success = false, message = "Design Type نامعتبر است." });
            }

            if (!TryGetHierarchy(designTypeId.Value, requireActive: true, out _, out _, out _))
            {
                return Json(new { success = false, message = "Design Type نامعتبر است." });
            }

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (extension != ".xlsx" && extension != ".xls")
            {
                return Json(new { success = false, message = "فرمت فایل معتبر نیست. فقط فایل اکسل با پسوند xlsx یا xls قابل قبول است." });
            }

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var rows = new List<(int Row, string LensIndexName, int OrderId, bool IsActive, bool HasColoringType)>();
                var errors = new List<string>();
                var orderIdRows = new Dictionary<int, List<int>>();
                var nameRows = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

                using var stream = file.OpenReadStream();
                using var reader = ExcelReaderFactory.CreateReader(stream);

                var rowIndex = 0;
                while (reader.Read())
                {
                    rowIndex++;
                    if (rowIndex == 1 && LooksLikeHeader(reader))
                        continue;

                    if (reader.FieldCount < 2)
                    {
                        errors.Add($"ردیف {rowIndex}: تعداد ستون‌ها کمتر از مقدار مورد نیاز است. ستون‌های لازم: Lens Index، ترتیب نمایش، وضعیت (اختیاری)، Coloring Type (اختیاری).");
                        continue;
                    }

                    var lensIndexName = Normalize(ReadCellString(reader, 0));
                    var orderId = ReadCellInt(reader, 1);
                    var statusText = ReadCellString(reader, 2);
                    var isActive = ParseStatus(statusText, out var statusValid);
                    var coloringText = ReadCellString(reader, 3);
                    var hasColoringType = ParseColoringType(coloringText, out var coloringValid);

                    if (string.IsNullOrWhiteSpace(lensIndexName) && orderId == null)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(lensIndexName))
                    {
                        errors.Add($"ردیف {rowIndex}: ستون Lens Index خالی است.");
                        continue;
                    }
                    if (!orderId.HasValue)
                    {
                        errors.Add($"ردیف {rowIndex}: ستون ترتیب نمایش نامعتبر است.");
                        continue;
                    }
                    if (orderId.Value <= 0)
                    {
                        errors.Add($"ردیف {rowIndex}: ترتیب نمایش باید عدد مثبت باشد.");
                        continue;
                    }
                    if (!statusValid)
                    {
                        errors.Add($"ردیف {rowIndex}: مقدار ستون وضعیت نامعتبر است. مقادیر مجاز: فعال/غیرفعال یا 1/0 یا true/false.");
                        continue;
                    }
                    if (!coloringValid)
                    {
                        errors.Add($"ردیف {rowIndex}: مقدار ستون Coloring Type نامعتبر است. مقادیر مجاز: دارد/ندارد یا 1/0 یا true/false.");
                        continue;
                    }

                    rows.Add((rowIndex, lensIndexName, orderId.Value, isActive, hasColoringType));

                    if (!orderIdRows.TryGetValue(orderId.Value, out var orderList))
                    {
                        orderList = new List<int>();
                        orderIdRows[orderId.Value] = orderList;
                    }
                    orderList.Add(rowIndex);

                    if (!nameRows.TryGetValue(lensIndexName, out var nameList))
                    {
                        nameList = new List<int>();
                        nameRows[lensIndexName] = nameList;
                    }
                    nameList.Add(rowIndex);
                }

                if (rows.Count == 0 && errors.Count == 0)
                {
                    errors.Add("هیچ ردیف معتبری در فایل پیدا نشد.");
                }

                foreach (var dup in orderIdRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join("، ", dup.Value);
                    errors.Add($"ترتیب نمایش تکراری در فایل: {dup.Key} (ردیف‌های {rowList})");
                }

                foreach (var dup in nameRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join("، ", dup.Value);
                    errors.Add($"Lens Index تکراری در فایل: {dup.Key} (ردیف‌های {rowList})");
                }

                var existing = (tblLnsCustomLensIndexService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensIndexDto>())
                    .Where(x => x.DesignTypeId == designTypeId.Value)
                    .ToList();

                var existingOrderIds = existing.Select(x => x.OrderId).ToHashSet();
                var existingNames = new HashSet<string>(
                    existing.Select(x => Normalize(x.LensIndexName)),
                    StringComparer.OrdinalIgnoreCase);

                foreach (var row in rows)
                {
                    if (existingOrderIds.Contains(row.OrderId))
                    {
                        errors.Add($"ردیف {row.Row}: ترتیب نمایش {row.OrderId} قبلاً در سیستم ثبت شده است.");
                    }
                    if (existingNames.Contains(row.LensIndexName))
                    {
                        errors.Add($"ردیف {row.Row}: Lens Index {row.LensIndexName} قبلاً در سیستم ثبت شده است.");
                    }
                }

                if (errors.Count > 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "خطا در ایمپورت اکسل. لطفاً موارد زیر را اصلاح کنید.",
                        errors
                    });
                }

                foreach (var row in rows)
                {
                    var dto = new TblLnsCustomLensIndexDto
                    {
                        DesignTypeId = designTypeId.Value,
                        LensIndexName = row.LensIndexName,
                        OrderId = row.OrderId,
                        IsActive = row.IsActive,
                        HasColoringType = row.HasColoringType
                    };
                    tblLnsCustomLensIndexService.Add(dto);
                }

                return Json(new { success = true, message = $"ایمپورت با موفقیت انجام شد. تعداد: {rows.Count}" });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "خطا در پردازش فایل اکسل. لطفاً از سالم بودن فایل و صحیح بودن ستون‌ها مطمئن شوید."
                });
            }
        }

        private static IEnumerable<TblLnsCustomLensIndexDto> ApplySort(IEnumerable<TblLnsCustomLensIndexDto> items, string? sidx, string? sord)
        {
            if (string.IsNullOrWhiteSpace(sidx))
            {
                return items;
            }

            var prop = typeof(TblLnsCustomLensIndexDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return items;
            }

            return string.Equals(sord, "desc", StringComparison.OrdinalIgnoreCase)
                ? items.OrderByDescending(x => prop.GetValue(x, null))
                : items.OrderBy(x => prop.GetValue(x, null));
        }

        private static bool LooksLikeHeader(IExcelDataReader reader)
        {
            var c0 = ReadCellString(reader, 0).ToLowerInvariant();
            var c1 = ReadCellString(reader, 1).ToLowerInvariant();

            return c0.Contains("lens index") || c0.Contains("index") || c0.Contains("لنز ایندکس")
                || c1.Contains("order") || c1.Contains("ترتیب");
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
            if (normalized == "1" || normalized == "true" || normalized == "فعال" || normalized == "active")
            {
                isValid = true;
                return true;
            }
            if (normalized == "0" || normalized == "false" || normalized == "غیرفعال" || normalized == "inactive")
            {
                isValid = true;
                return false;
            }

            isValid = false;
            return true;
        }

        private static bool ParseColoringType(string? text, out bool isValid)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                isValid = true;
                return false;
            }

            var normalized = text.Trim().ToLowerInvariant();
            if (normalized == "1" || normalized == "true" || normalized == "دارد" || normalized == "yes")
            {
                isValid = true;
                return true;
            }
            if (normalized == "0" || normalized == "false" || normalized == "ندارد" || normalized == "no")
            {
                isValid = true;
                return false;
            }

            isValid = false;
            return false;
        }

        private static string Normalize(string? value)
        {
            return value?.Trim() ?? string.Empty;
        }

        private bool TryGetHierarchy(int designTypeId, bool requireActive, out TblLnsDesignTypeDto designType, out TblLnsLensTypeDto? lensType, out TblLnsBrandDto? brand)
        {
            designType = null!;
            lensType = null;
            brand = null;

            var currentDesignType = tblLnsDesignTypeService.GetById(designTypeId).Result;
            if (currentDesignType == null || currentDesignType.IsSpecial == false || (requireActive && currentDesignType.IsActive == false))
            {
                return false;
            }

            if (!currentDesignType.LensTypeId.HasValue || currentDesignType.LensTypeId.Value <= 0)
            {
                return false;
            }

            var currentLensType = tblLnsLensTypeService.GetById(currentDesignType.LensTypeId.Value).Result;
            if (currentLensType == null || currentLensType.IsSpecial == false || (requireActive && currentLensType.IsActive == false))
            {
                return false;
            }

            if (!currentLensType.BrandId.HasValue || currentLensType.BrandId.Value <= 0)
            {
                return false;
            }

            var currentBrand = tblLnsBrandService.GetById(currentLensType.BrandId.Value).Result;
            if (currentBrand == null || currentBrand.IsSpecial == false || (requireActive && currentBrand.IsActive == false))
            {
                return false;
            }

            designType = currentDesignType;
            lensType = currentLensType;
            brand = currentBrand;
            return true;
        }

        private static string GetBrandName(int? designTypeId, Dictionary<int, TblLnsDesignTypeDto> designTypes, Dictionary<int, TblLnsLensTypeDto> lensTypes, Dictionary<int, string> brands)
        {
            if (!designTypeId.HasValue || !designTypes.TryGetValue(designTypeId.Value, out var designType))
                return string.Empty;
            if (!designType.LensTypeId.HasValue || !lensTypes.TryGetValue(designType.LensTypeId.Value, out var lensType))
                return string.Empty;
            if (!lensType.BrandId.HasValue || !brands.TryGetValue(lensType.BrandId.Value, out var brandName))
                return string.Empty;
            return brandName;
        }

        private static string GetLensTypeName(int? designTypeId, Dictionary<int, TblLnsDesignTypeDto> designTypes, Dictionary<int, TblLnsLensTypeDto> lensTypes)
        {
            if (!designTypeId.HasValue || !designTypes.TryGetValue(designTypeId.Value, out var designType))
                return string.Empty;
            if (!designType.LensTypeId.HasValue || !lensTypes.TryGetValue(designType.LensTypeId.Value, out var lensType))
                return string.Empty;
            return lensType.Name ?? string.Empty;
        }

        private static string GetDesignTypeName(int? designTypeId, Dictionary<int, TblLnsDesignTypeDto> designTypes)
        {
            if (!designTypeId.HasValue || !designTypes.TryGetValue(designTypeId.Value, out var designType))
                return string.Empty;
            return designType.Name ?? string.Empty;
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

