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
    public class CustomLensTypeCoatingsController(ITblLnsCustomLensTypeCoatingService tblLnsCustomLensTypeCoatingService, ITblLnsDesignTypeService tblLnsDesignTypeService, ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsBrandService tblLnsBrandService) : Controller
    {
        [Authorize(Roles = "admin,CustomLensTypeCoating_Index")]
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
                ViewBag.DesignTypeName = designType.Name;

                if (designType.LensTypeId.HasValue)
                {
                    var lensType = tblLnsLensTypeService.GetById(designType.LensTypeId.Value).Result;
                    ViewBag.LensTypeName = lensType?.Name ?? string.Empty;
                    if (lensType?.BrandId != null)
                    {
                        var brand = tblLnsBrandService.GetById(lensType.BrandId.Value).Result;
                        ViewBag.BrandName = brand?.Name ?? string.Empty;
                    }
                    else
                    {
                        ViewBag.BrandName = string.Empty;
                    }
                }
                else
                {
                    ViewBag.LensTypeName = string.Empty;
                    ViewBag.BrandName = string.Empty;
                }
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

        [Authorize(Roles = "admin,CustomLensTypeCoating_Index")]
        public JsonResult Detail(int? designTypeId)
        {
            var payload = GetRequestPayload();
            var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
            int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
            int pageSize = itemGrid.Rows;

            var allItems = tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>();
            var filtered = allItems;
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                filtered = filtered.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var designTypeLookup = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
                .ToDictionary(x => x.DesignTypeId, x => x.Name ?? string.Empty);

            filtered = ApplySort(filtered, itemGrid.Sidx, itemGrid.Sord);

            var totalRecords = filtered.Count();
            if (totalRecords <= 0)
            {
                return Json(new { });
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
                           id = p.CustomLensTypeCoatingId,
                           designTypeName = p.DesignTypeId.HasValue && designTypeLookup.ContainsKey(p.DesignTypeId.Value)
                               ? designTypeLookup[p.DesignTypeId.Value]
                               : string.Empty,
                           coatingName = p.CoatingName,
                           isDefault = p.IsDefault == true ? "پیش‌فرض" : "-",
                           orderId = p.OrderId,
                           isActive = p.IsActive == true ? "فعال" : "غير فعال"
                       }
            };
            return Json(jsonData);
        }

        [Authorize(Roles = "admin,CustomLensTypeCoating_Create")]
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

            var dto = new TblLnsCustomLensTypeCoatingDto
            {
                DesignTypeId = designTypeId.Value,
                LensTypeName = designType.Name ?? string.Empty,
                IsDefault = false,
                OrderId = 1,
                IsActive = true
            };
            ViewBag.DesignTypeName = designType.Name ?? string.Empty;
            return View(dto);
        }

        [Authorize(Roles = "admin,CustomLensTypeCoating_Create")]
        [HttpPost]
        public IActionResult Create(TblLnsCustomLensTypeCoatingDto dto)
        {
            try
            {
                if (!dto.DesignTypeId.HasValue || dto.DesignTypeId.Value <= 0)
                {
                    return Json(new { success = false, message = "Design Type نامعتبر است." });
                }

                var designType = tblLnsDesignTypeService.GetById(dto.DesignTypeId.Value).Result;
                if (designType == null || designType.IsSpecial == false)
                {
                    return Json(new { success = false, message = "Design Type نامعتبر است." });
                }

                dto.LensTypeName = Normalize(designType.Name);
                dto.CoatingName = Normalize(dto.CoatingName);

                if (string.IsNullOrWhiteSpace(dto.CoatingName))
                {
                    return Json(new { success = false, message = "ستون Coating خالی است." });
                }
                if (dto.OrderId <= 0)
                {
                    return Json(new { success = false, message = "ترتيب نمايش بايد عدد مثبت باشد." });
                }

                if (dto.IsDefault)
                {
                    dto.IsActive = true;
                    ClearDefaultCoating(dto.DesignTypeId.Value);
                }

                var siblings = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
                    .Where(x => x.DesignTypeId == dto.DesignTypeId.Value)
                    .ToList();

                if (siblings.Any(x => x.OrderId == dto.OrderId))
                {
                    return Json(new { success = false, message = "ترتيب نمايش تکراري است." });
                }
                if (siblings.Any(x =>
                    string.Equals(Normalize(x.LensTypeName), dto.LensTypeName, StringComparison.OrdinalIgnoreCase) &&
                    string.Equals(Normalize(x.CoatingName), dto.CoatingName, StringComparison.OrdinalIgnoreCase)))
                {
                    return Json(new { success = false, message = "ترکيب Design Type و Coating تکراری است." });
                }

                tblLnsCustomLensTypeCoatingService.Add(dto);
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

        [Authorize(Roles = "admin,CustomLensTypeCoating_Edit")]
        public IActionResult Edit(int id, int? designTypeId)
        {
            var item = tblLnsCustomLensTypeCoatingService.GetById(id).Result;
            if (item == null || (designTypeId.HasValue && designTypeId.Value > 0 && item.DesignTypeId != designTypeId.Value))
            {
                return NotFound();
            }

            ViewBag.DesignTypeId = designTypeId ?? 0;
            return View(item);
        }

        [Authorize(Roles = "admin,CustomLensTypeCoating_Edit")]
        [HttpPost]
        public IActionResult Edit(TblLnsCustomLensTypeCoatingDto dto)
        {
            try
            {
                var existing = tblLnsCustomLensTypeCoatingService.GetById(dto.CustomLensTypeCoatingId).Result;
                if (existing == null || (dto.DesignTypeId.HasValue && dto.DesignTypeId.Value > 0 && existing.DesignTypeId != dto.DesignTypeId))
                {
                    return Json(new { success = false, message = "رکورد موردنظر يافت نشد." });
                }

                existing.LensTypeName = Normalize(dto.LensTypeName);
                existing.CoatingName = Normalize(dto.CoatingName);
                existing.IsDefault = dto.IsDefault;
                existing.OrderId = dto.OrderId;
                existing.IsActive = dto.IsActive;
                if (dto.DesignTypeId.HasValue && dto.DesignTypeId.Value > 0)
                {
                    existing.DesignTypeId = dto.DesignTypeId;
                }

                if (existing.IsDefault)
                {
                    existing.IsActive = true;
                    if (existing.DesignTypeId.HasValue && existing.DesignTypeId.Value > 0)
                    {
                        ClearDefaultCoating(existing.DesignTypeId.Value, existing.CustomLensTypeCoatingId);
                    }
                }

                tblLnsCustomLensTypeCoatingService.Update(existing);
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

        [Authorize(Roles = "admin,CustomLensTypeCoating_Delete")]
        [HttpPost]
        public IActionResult Delete(int id, int? designTypeId)
        {
            var item = tblLnsCustomLensTypeCoatingService.GetById(id).Result;
            if (item == null || (designTypeId.HasValue && designTypeId.Value > 0 && item.DesignTypeId != designTypeId.Value))
            {
                return Json(new { success = false, message = "رکورد موردنظر يافت نشد." });
            }

            tblLnsCustomLensTypeCoatingService.Delete(id);
            return Json(new { success = true, message = "" });
        }

        [Authorize(Roles = "admin,CustomLensTypeCoating_Index")]
        [HttpGet]
        public IActionResult ExportExcel(int? designTypeId)
        {
            var items = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>());
            if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                items = items.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var ordered = items
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomLensTypeCoatingId)
                .ToList();

            using var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("CustomLensTypeCoating");

            ws.Cell(1, 1).Value = "Lens Type";
            ws.Cell(1, 2).Value = "Coating";
            ws.Cell(1, 3).Value = "ترتيب نمايش";
            ws.Cell(1, 4).Value = "وضعيت";

            var row = 2;
            foreach (var item in ordered)
            {
                ws.Cell(row, 1).Value = Normalize(item.LensTypeName);
                ws.Cell(row, 2).Value = Normalize(item.CoatingName);
                ws.Cell(row, 3).Value = item.OrderId;
                ws.Cell(row, 4).Value = item.IsActive ? "فعال" : "غيرفعال";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomLensTypeCoating.xlsx");
        }

        [Authorize(Roles = "admin,CustomLensTypeCoating_Create")]
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file, int? designTypeId)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "فايلي انتخاب نشده است." });
            }
            if (!designTypeId.HasValue || designTypeId.Value <= 0)
            {
                return Json(new { success = false, message = "Design Type نامعتبر است." });
            }
            var designType = tblLnsDesignTypeService.GetById(designTypeId.Value).Result;
            if (designType == null || designType.IsSpecial == false)
            {
                return Json(new { success = false, message = "Design Type نامعتبر است." });
            }

            var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (extension != ".xlsx" && extension != ".xls")
            {
                return Json(new { success = false, message = "فرمت فايل معتبر نيست. فقط فايل اکسل با پسوند xlsx يا xls قابل قبول است." });
            }

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var rows = new List<(int Row, string LensTypeName, string CoatingName, int OrderId, bool IsActive)>();
                var errors = new List<string>();
                var orderIdRows = new Dictionary<int, List<int>>();
                var pairRows = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

                using var stream = file.OpenReadStream();
                using var reader = ExcelReaderFactory.CreateReader(stream);

                var rowIndex = 0;
                while (reader.Read())
                {
                    rowIndex++;
                    if (rowIndex == 1 && LooksLikeHeader(reader))
                        continue;

                    if (reader.FieldCount < 3)
                    {
                        errors.Add($"رديف {rowIndex}: تعداد ستون‌ها کمتر از مقدار مورد نياز است. ستون‌هاي لازم: Lens Type، Coating، ترتيب نمايش، وضعيت (اختياري).");
                        continue;
                    }

                    var lensTypeName = Normalize(ReadCellString(reader, 0));
                    var coatingName = Normalize(ReadCellString(reader, 1));
                    var orderId = ReadCellInt(reader, 2);
                    var statusText = ReadCellString(reader, 3);
                    var isActive = ParseStatus(statusText, out var statusValid);

                    if (string.IsNullOrWhiteSpace(lensTypeName) && string.IsNullOrWhiteSpace(coatingName) && orderId == null)
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(lensTypeName))
                    {
                        errors.Add($"رديف {rowIndex}: ستون Lens Type خالي است.");
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(coatingName))
                    {
                        errors.Add($"رديف {rowIndex}: ستون Coating خالي است.");
                        continue;
                    }
                    if (!orderId.HasValue)
                    {
                        errors.Add($"رديف {rowIndex}: ستون ترتيب نمايش نامعتبر است.");
                        continue;
                    }
                    if (orderId.Value <= 0)
                    {
                        errors.Add($"رديف {rowIndex}: ترتيب نمايش بايد عدد مثبت باشد.");
                        continue;
                    }
                    if (!statusValid)
                    {
                        errors.Add($"رديف {rowIndex}: مقدار ستون وضعيت نامعتبر است. مقادير مجاز: فعال/غيرفعال يا 1/0 يا true/false.");
                        continue;
                    }

                    rows.Add((rowIndex, lensTypeName, coatingName, orderId.Value, isActive));

                    if (!orderIdRows.TryGetValue(orderId.Value, out var orderList))
                    {
                        orderList = new List<int>();
                        orderIdRows[orderId.Value] = orderList;
                    }
                    orderList.Add(rowIndex);

                    var pairKey = (lensTypeName + "|" + coatingName).ToLowerInvariant();
                    if (!pairRows.TryGetValue(pairKey, out var pairList))
                    {
                        pairList = new List<int>();
                        pairRows[pairKey] = pairList;
                    }
                    pairList.Add(rowIndex);
                }

                if (rows.Count == 0 && errors.Count == 0)
                {
                    errors.Add("هيچ رديف معتبري در فايل پيدا نشد.");
                }

                foreach (var dup in orderIdRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join("، ", dup.Value);
                    errors.Add($"ترتيب نمايش تکراري در فايل: {dup.Key} (رديف‌هاي {rowList})");
                }

                foreach (var dup in pairRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join("، ", dup.Value);
                    errors.Add($"ترکيب Lens Type و Coating تکراري در فايل: {rowList}");
                }

                var existing = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
                    .Where(x => x.DesignTypeId == designTypeId.Value)
                    .ToList();

                var existingOrderIds = existing.Select(x => x.OrderId).ToHashSet();
                var existingPairs = new HashSet<string>(
                    existing.Select(x => (Normalize(x.LensTypeName) + "|" + Normalize(x.CoatingName)).ToLowerInvariant()),
                    StringComparer.OrdinalIgnoreCase);

                foreach (var row in rows)
                {
                    if (existingOrderIds.Contains(row.OrderId))
                    {
                        errors.Add($"رديف {row.Row}: ترتيب نمايش {row.OrderId} قبلاً در سيستم ثبت شده است.");
                    }
                    var pairKey = (row.LensTypeName + "|" + row.CoatingName).ToLowerInvariant();
                    if (existingPairs.Contains(pairKey))
                    {
                        errors.Add($"رديف {row.Row}: ترکيب Lens Type و Coating قبلاً در سيستم ثبت شده است.");
                    }
                }

                if (errors.Count > 0)
                {
                    return Json(new
                    {
                        success = false,
                        message = "خطا در ايمپورت اکسل. لطفاً موارد زير را اصلاح کنيد.",
                        errors
                    });
                }

                foreach (var row in rows)
                {
                    var dto = new TblLnsCustomLensTypeCoatingDto
                    {
                        DesignTypeId = designTypeId.Value,
                        LensTypeName = row.LensTypeName,
                        CoatingName = row.CoatingName,
                        OrderId = row.OrderId,
                        IsActive = row.IsActive
                    };
                    tblLnsCustomLensTypeCoatingService.Add(dto);
                }

                return Json(new { success = true, message = $"ايمپورت با موفقيت انجام شد. تعداد: {rows.Count}" });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "خطا در پردازش فايل اکسل. لطفاً از سالم بودن فايل و صحيح بودن ستون‌ها مطمئن شويد."
                });
            }
        }

        private static IEnumerable<TblLnsCustomLensTypeCoatingDto> ApplySort(IEnumerable<TblLnsCustomLensTypeCoatingDto> items, string? sidx, string? sord)
        {
            if (string.IsNullOrWhiteSpace(sidx))
            {
                return items;
            }

            var prop = typeof(TblLnsCustomLensTypeCoatingDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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
            var c2 = ReadCellString(reader, 2).ToLowerInvariant();

            return c0.Contains("lens") || c0.Contains("لنز")
                || c1.Contains("coating") || c1.Contains("کوتينگ")
                || c2.Contains("order") || c2.Contains("ترتيب");
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
            if (normalized == "0" || normalized == "false" || normalized == "غيرفعال" || normalized == "inactive")
            {
                isValid = true;
                return false;
            }

            isValid = false;
            return true;
        }

        private static string Normalize(string? value)
        {
            return value?.Trim() ?? string.Empty;
        }

        private void ClearDefaultCoating(int designTypeId, int? excludeId = null)
        {
            var defaultItems = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
                .Where(x => x.DesignTypeId == designTypeId && x.IsDefault)
                .Where(x => !excludeId.HasValue || x.CustomLensTypeCoatingId != excludeId.Value)
                .ToList();

            foreach (var item in defaultItems)
            {
                item.IsDefault = false;
                tblLnsCustomLensTypeCoatingService.Update(item);
            }
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

