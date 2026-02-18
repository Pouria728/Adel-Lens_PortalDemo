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
    public class CustomLensTypeMaterialsController(ITblLnsCustomLensTypeMaterialService tblLnsCustomLensTypeMaterialService, ITblLnsDesignTypeService tblLnsDesignTypeService, ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsBrandService tblLnsBrandService, ITblLnsCustomLensIndexService tblLnsCustomLensIndexService, ITblClrDefineObjectService tblClrDefineObjectService) : Controller
    {
        private const bool CustomFlag = true;

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Index")]
        public IActionResult Index(int? designTypeId, int? lensIndexId, bool embedded = false)
        {
            ViewBag.Embedded = embedded;
            if (lensIndexId.HasValue && lensIndexId.Value > 0)
            {
                var lensIndex = tblLnsCustomLensIndexService.GetById(lensIndexId.Value).Result;
                if (lensIndex == null || !lensIndex.DesignTypeId.HasValue || lensIndex.DesignTypeId.Value <= 0)
                {
                    return NotFound();
                }

                ViewBag.LensIndexId = lensIndexId.Value;
                ViewBag.LensIndexName = lensIndex.LensIndexName ?? string.Empty;

                var designType = tblLnsDesignTypeService.GetById(lensIndex.DesignTypeId.Value).Result;
                if (designType == null || designType.IsSpecial == false)
                {
                    return NotFound();
                }

                ViewBag.DesignTypeId = designType.DesignTypeId;
                ViewBag.DesignTypeName = designType.Name ?? string.Empty;

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
            else if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                var designType = tblLnsDesignTypeService.GetById(designTypeId.Value).Result;
                if (designType == null || designType.IsSpecial == false)
                {
                    return NotFound();
                }

                ViewBag.DesignTypeId = designTypeId.Value;
                ViewBag.DesignTypeName = designType.Name ?? string.Empty;

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
                ViewBag.LensIndexId = 0;
                ViewBag.LensIndexName = "همه Lens Index ها";
            }
            else
            {
                ViewBag.DesignTypeId = 0;
                ViewBag.DesignTypeName = "همه Design Type ها";
                ViewBag.LensTypeName = "همه Lens Type ها";
                ViewBag.BrandName = "همه برندها";
                ViewBag.LensIndexId = 0;
                ViewBag.LensIndexName = "همه Lens Index ها";
            }

            return View();
        }
        [Authorize(Roles = "admin,CustomLensTypeMaterial_Index")]
        public JsonResult Detail(int? designTypeId, int? lensIndexId)
        {
            var payload = GetRequestPayload();
            var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
            int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
            int pageSize = itemGrid.Rows;

            var allItems = tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>();
            var filtered = allItems;
            if (lensIndexId.HasValue && lensIndexId.Value > 0)
            {
                filtered = filtered.Where(x => x.LensIndexId.HasValue && x.LensIndexId.Value == lensIndexId.Value);
            }
            else if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                filtered = filtered.Where(x => x.DesignTypeId == designTypeId.Value && (!x.LensIndexId.HasValue || x.LensIndexId.Value == 0));
            }

            filtered = ApplySort(filtered, itemGrid.Sidx, itemGrid.Sord);

            var totalRecords = filtered.Count();
            if (totalRecords <= 0)
            {
                return Json(new { total = 0, page = itemGrid.Page, records = 0, rows = Array.Empty<object>() });
            }

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var paged = filtered.Skip(pageIndex * pageSize).Take(pageSize);

            var defineObjects = (tblClrDefineObjectService.GetAll().Result ?? Enumerable.Empty<TblClrDefineObjectDto>()).ToList();
            var defineObjectLookup = defineObjects.ToDictionary(x => x.DefineObjectId, x => x);

            var jsonData = new
            {
                total = totalPages,
                page = itemGrid.Page,
                records = totalRecords,
                rows = from p in paged
                       select new
                       {
                           id = p.CustomLensTypeMaterialId,
                           lensTypeName = p.LensTypeName,
                           defineObjectText = (p.DefineObjectId.HasValue && defineObjectLookup.TryGetValue(p.DefineObjectId.Value, out var defObj))
                               ? BuildDefineObjectText(defObj)
                               : "",
                           materialName = p.MaterialName,
                           orderId = p.OrderId,
                           isActive = p.IsActive == true ? "فعال" : "غیرفعال"
                       }
            };
            return Json(jsonData);
        }

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Create")]
        public IActionResult Create(int? designTypeId, int? lensIndexId)
        {
            if ((!designTypeId.HasValue || designTypeId.Value <= 0) && (!lensIndexId.HasValue || lensIndexId.Value <= 0))
            {
                return NotFound();
            }

            int designTypeValue = 0;
            int? lensIndexValue = null;
            if (lensIndexId.HasValue && lensIndexId.Value > 0)
            {
                var lensIndex = tblLnsCustomLensIndexService.GetById(lensIndexId.Value).Result;
                if (lensIndex == null || !lensIndex.DesignTypeId.HasValue || lensIndex.DesignTypeId.Value <= 0)
                {
                    return NotFound();
                }
                designTypeValue = lensIndex.DesignTypeId.Value;
                lensIndexValue = lensIndex.CustomLensIndexId;
            }
            else
            {
                designTypeValue = designTypeId.Value;
            }

            var designType = tblLnsDesignTypeService.GetById(designTypeValue).Result;
            if (designType == null || designType.IsSpecial == false)
            {
                return NotFound();
            }

            var lensTypeName = "";
            if (designType.LensTypeId.HasValue)
            {
                var lensType = tblLnsLensTypeService.GetById(designType.LensTypeId.Value).Result;
                lensTypeName = lensType?.Name ?? "";
            }

            var dto = new TblLnsCustomLensTypeMaterialDto
            {
                DesignTypeId = designTypeValue,
                LensIndexId = lensIndexValue,
                LensTypeName = lensTypeName,
                OrderId = 1,
                IsActive = true
            };
            ViewBag.DesignTypeId = designTypeValue;
            ViewBag.LensIndexId = lensIndexValue ?? 0;
            ViewBag.DefineObjectName = string.Empty;
            ViewBag.DefineObjectBarcode = string.Empty;
            ViewBag.DefineObjectCode = string.Empty;
            return View(dto);
        }

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Create")]
        [HttpPost]
        public IActionResult Create(TblLnsCustomLensTypeMaterialDto dto)
        {
            try
            {
                if ((!dto.LensIndexId.HasValue || dto.LensIndexId.Value <= 0)
                    && int.TryParse(Request.Query["lensIndexId"], out var queryLensIndexId)
                    && queryLensIndexId > 0)
                {
                    dto.LensIndexId = queryLensIndexId;
                }
                if ((!dto.DesignTypeId.HasValue || dto.DesignTypeId.Value <= 0)
                    && int.TryParse(Request.Query["designTypeId"], out var queryDesignTypeId)
                    && queryDesignTypeId > 0)
                {
                    dto.DesignTypeId = queryDesignTypeId;
                }

                int designTypeValue = 0;
                if (dto.LensIndexId.HasValue && dto.LensIndexId.Value > 0)
                {
                    var lensIndex = tblLnsCustomLensIndexService.GetById(dto.LensIndexId.Value).Result;
                    if (lensIndex == null || !lensIndex.DesignTypeId.HasValue || lensIndex.DesignTypeId.Value <= 0)
                    {
                        return Json(new { success = false, message = "Lens Index نامعتبر است." });
                    }
                    designTypeValue = lensIndex.DesignTypeId.Value;
                    dto.DesignTypeId = designTypeValue;
                }
                else if (dto.DesignTypeId.HasValue && dto.DesignTypeId.Value > 0)
                {
                    designTypeValue = dto.DesignTypeId.Value;
                }
                else
                {
                    return Json(new { success = false, message = "Design Type نامعتبر است." });
                }

                var designType = tblLnsDesignTypeService.GetById(designTypeValue).Result;
                if (designType == null || designType.IsSpecial == false)
                {
                    return Json(new { success = false, message = "Design Type نامعتبر است." });
                }

                if (!dto.DefineObjectId.HasValue || dto.DefineObjectId.Value <= 0)
                {
                    return Json(new { success = false, message = "Warehouse item is required." });
                }
                var defineObject = tblClrDefineObjectService.GetById(dto.DefineObjectId.Value).Result;
                if (defineObject == null)
                {
                    return Json(new { success = false, message = "Warehouse item is invalid." });
                }

                dto.LensTypeName = Normalize(dto.LensTypeName);
                dto.MaterialName = Normalize(dto.MaterialName);
                if (string.IsNullOrWhiteSpace(dto.MaterialName))
                {
                    return Json(new { success = false, message = "ستون Material خالی است." });
                }
                if (dto.OrderId <= 0)
                {
                    return Json(new { success = false, message = "ترتیب نمایش باید عدد مثبت باشد." });
                }

                var existing = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
                    .Where(x => dto.LensIndexId.HasValue && dto.LensIndexId.Value > 0
                        ? x.LensIndexId == dto.LensIndexId
                        : x.DesignTypeId == dto.DesignTypeId)
                    .ToList();

                if (existing.Any(x => x.OrderId == dto.OrderId))
                {
                    return Json(new { success = false, message = "ترتیب نمایش تکراری است." });
                }
                if (existing.Any(x => string.Equals(Normalize(x.MaterialName), dto.MaterialName, StringComparison.OrdinalIgnoreCase)))
                {
                    return Json(new { success = false, message = "این Material قبلاً ثبت شده است." });
                }

                tblLnsCustomLensTypeMaterialService.Add(dto);
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

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Edit")]
        public IActionResult Edit(int id, int? designTypeId, int? lensIndexId)
        {
            var item = tblLnsCustomLensTypeMaterialService.GetById(id).Result;
            if (item == null)
            {
                return NotFound();
            }

            var safeDesignTypeId = item.DesignTypeId ?? designTypeId ?? 0;
            var safeLensIndexId = item.LensIndexId ?? lensIndexId ?? 0;
            ViewBag.DesignTypeId = safeDesignTypeId;
            ViewBag.LensIndexId = safeLensIndexId;
            ViewBag.DefineObjectName = string.Empty;
            ViewBag.DefineObjectBarcode = string.Empty;
            ViewBag.DefineObjectCode = string.Empty;
            if (item.DefineObjectId.HasValue)
            {
                var defineObject = tblClrDefineObjectService.GetById(item.DefineObjectId.Value).Result;
                if (defineObject != null)
                {
                    ViewBag.DefineObjectName = Normalize(defineObject.NameObject);
                    ViewBag.DefineObjectBarcode = Normalize(defineObject.TechnicalSpecs);
                    ViewBag.DefineObjectCode = Normalize(defineObject.CodeObject);
                }
            }
            return View(item);
        }

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Edit")]
        [HttpPost]
        public IActionResult Edit(TblLnsCustomLensTypeMaterialDto dto)
        {
            try
            {
                if ((!dto.LensIndexId.HasValue || dto.LensIndexId.Value <= 0)
                    && int.TryParse(Request.Query["lensIndexId"], out var queryLensIndexId)
                    && queryLensIndexId > 0)
                {
                    dto.LensIndexId = queryLensIndexId;
                }
                if ((!dto.DesignTypeId.HasValue || dto.DesignTypeId.Value <= 0)
                    && int.TryParse(Request.Query["designTypeId"], out var queryDesignTypeId)
                    && queryDesignTypeId > 0)
                {
                    dto.DesignTypeId = queryDesignTypeId;
                }

                var existing = tblLnsCustomLensTypeMaterialService.GetById(dto.CustomLensTypeMaterialId).Result;
                if (existing == null)
                {
                    return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
                }

                if (!dto.DefineObjectId.HasValue || dto.DefineObjectId.Value <= 0)
                {
                    return Json(new { success = false, message = "Warehouse item is required." });
                }
                var defineObject = tblClrDefineObjectService.GetById(dto.DefineObjectId.Value).Result;
                if (defineObject == null)
                {
                    return Json(new { success = false, message = "Warehouse item is invalid." });
                }

                existing.LensTypeName = Normalize(dto.LensTypeName);
                existing.MaterialName = Normalize(dto.MaterialName);
                existing.OrderId = dto.OrderId;
                existing.IsActive = dto.IsActive;
                existing.DefineObjectId = dto.DefineObjectId;
                if (dto.LensIndexId.HasValue && dto.LensIndexId.Value > 0)
                {
                    existing.LensIndexId = dto.LensIndexId;
                }
                if (dto.DesignTypeId.HasValue && dto.DesignTypeId.Value > 0)
                {
                    existing.DesignTypeId = dto.DesignTypeId;
                }

                if (string.IsNullOrWhiteSpace(existing.MaterialName))
                {
                    return Json(new { success = false, message = "ستون Material خالی است." });
                }
                if (existing.OrderId <= 0)
                {
                    return Json(new { success = false, message = "ترتیب نمایش باید عدد مثبت باشد." });
                }

                var siblings = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
                    .Where(x => (existing.LensIndexId.HasValue && existing.LensIndexId.Value > 0)
                        ? x.LensIndexId == existing.LensIndexId
                        : x.DesignTypeId == existing.DesignTypeId)
                    .Where(x => x.CustomLensTypeMaterialId != existing.CustomLensTypeMaterialId)
                    .ToList();

                if (siblings.Any(x => x.OrderId == existing.OrderId))
                {
                    return Json(new { success = false, message = "ترتیب نمایش تکراری است." });
                }
                if (siblings.Any(x => string.Equals(Normalize(x.MaterialName), existing.MaterialName, StringComparison.OrdinalIgnoreCase)))
                {
                    return Json(new { success = false, message = "این Material قبلاً ثبت شده است." });
                }

                tblLnsCustomLensTypeMaterialService.Update(existing);
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

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Delete")]
        [HttpPost]
        public IActionResult Delete(int id, int? designTypeId, int? lensIndexId)
        {
            var item = tblLnsCustomLensTypeMaterialService.GetById(id).Result;
            if (item == null)
            {
                return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
            }

            tblLnsCustomLensTypeMaterialService.Delete(id);
            return Json(new { success = true, message = "" });
        }

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Index")]
        [HttpGet]
        public IActionResult ExportExcel(int? designTypeId, int? lensIndexId)
        {
            var items = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>());
            if (lensIndexId.HasValue && lensIndexId.Value > 0)
            {
                items = items.Where(x => x.LensIndexId == lensIndexId.Value);
            }
            else if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                items = items.Where(x => x.DesignTypeId == designTypeId.Value);
            }

            var ordered = items
                .OrderBy(x => x.OrderId)
                .ThenBy(x => x.CustomLensTypeMaterialId)
                .ToList();

            var defineObjects = (tblClrDefineObjectService.GetAll().Result ?? Enumerable.Empty<TblClrDefineObjectDto>()).ToList();
            var defineObjectLookup = defineObjects.ToDictionary(x => x.DefineObjectId, x => x);

            using var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("CustomLensTypeMaterial");

            ws.Cell(1, 1).Value = "Warehouse Item";
            ws.Cell(1, 2).Value = "Material";
            ws.Cell(1, 3).Value = "ترتیب نمایش";
            ws.Cell(1, 4).Value = "وضعیت";

            var row = 2;
            foreach (var item in ordered)
            {
                var defineObjectText = (item.DefineObjectId.HasValue && defineObjectLookup.TryGetValue(item.DefineObjectId.Value, out var defObj))
                    ? BuildDefineObjectText(defObj)
                    : "";
                ws.Cell(row, 1).Value = defineObjectText;
                ws.Cell(row, 2).Value = Normalize(item.MaterialName);
                ws.Cell(row, 3).Value = item.OrderId;
                ws.Cell(row, 4).Value = item.IsActive ? "فعال" : "غیرفعال";
                row++;
            }

            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomLensTypeMaterial.xlsx");
        }

        [Authorize(Roles = "admin,CustomLensTypeMaterial_Create")]
        [HttpPost]
        public IActionResult ImportExcel(IFormFile file, int? designTypeId, int? lensIndexId)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "فایلی انتخاب نشده است." });
            }
            int designTypeValue = 0;
            if (lensIndexId.HasValue && lensIndexId.Value > 0)
            {
                var lensIndex = tblLnsCustomLensIndexService.GetById(lensIndexId.Value).Result;
                if (lensIndex == null || !lensIndex.DesignTypeId.HasValue || lensIndex.DesignTypeId.Value <= 0)
                {
                    return Json(new { success = false, message = "Lens Index نامعتبر است." });
                }
                designTypeValue = lensIndex.DesignTypeId.Value;
            }
            else if (designTypeId.HasValue && designTypeId.Value > 0)
            {
                designTypeValue = designTypeId.Value;
            }
            else
            {
                return Json(new { success = false, message = "Design Type نامعتبر است." });
            }

            var designType = tblLnsDesignTypeService.GetById(designTypeValue).Result;
            if (designType == null || designType.IsSpecial == false)
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

                var rows = new List<(int Row, int? DefineObjectId, string MaterialName, int OrderId, bool IsActive)>();
                var errors = new List<string>();
                var orderIdRows = new Dictionary<int, List<int>>();
                var nameRows = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);
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
                        errors.Add($"ردیف {rowIndex}: تعداد ستون ها کمتر از مقدار مورد نیاز است. ستون های لازم: Warehouse Item، Material، ترتیب نمایش، وضعیت (اختیاری).");
                        continue;
                    }

                    var defineObjectText = columnMap.DefineObjectIndex >= 0 ? ReadCellString(reader, columnMap.DefineObjectIndex) : string.Empty;
                    var materialName = Normalize(ReadCellString(reader, columnMap.MaterialIndex));
                    var orderId = ReadCellInt(reader, columnMap.OrderIndex);
                    var statusText = ReadCellString(reader, columnMap.StatusIndex);
                    var isActive = ParseStatus(statusText, out var statusValid);

                    if (string.IsNullOrWhiteSpace(materialName) && orderId == null && string.IsNullOrWhiteSpace(defineObjectText))
                    {
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(defineObjectText))
                    {
                        errors.Add($"ردیف {rowIndex}: ستون کالای انبار خالی است.");
                        continue;
                    }
                    var defineObjectId = ResolveDefineObjectId(defineObjectText, out var defineError);
                    if (defineError != null)
                    {
                        errors.Add($"ردیف {rowIndex}: {defineError}");
                        continue;
                    }

                    if (string.IsNullOrWhiteSpace(materialName))
                    {
                        errors.Add($"ردیف {rowIndex}: ستون Material خالی است.");
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

                    rows.Add((rowIndex, defineObjectId, materialName, orderId.Value, isActive));

                    if (!orderIdRows.TryGetValue(orderId.Value, out var orderList))
                    {
                        orderList = new List<int>();
                        orderIdRows[orderId.Value] = orderList;
                    }
                    orderList.Add(rowIndex);

                    if (!nameRows.TryGetValue(materialName, out var nameList))
                    {
                        nameList = new List<int>();
                        nameRows[materialName] = nameList;
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
                    errors.Add($"ترتیب نمایش تکراری در فایل: {dup.Key} (ردیف های {rowList})");
                }

                foreach (var dup in nameRows.Where(x => x.Value.Count > 1))
                {
                    var rowList = string.Join("، ", dup.Value);
                    errors.Add($"Material تکراری در فایل: {dup.Key} (ردیف های {rowList})");
                }

                var existing = (tblLnsCustomLensTypeMaterialService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>())
                    .Where(x => lensIndexId.HasValue && lensIndexId.Value > 0
                        ? x.LensIndexId == lensIndexId.Value
                        : x.DesignTypeId == designTypeValue)
                    .ToList();

                var existingOrderIds = existing.Select(x => x.OrderId).ToHashSet();
                var existingNames = new HashSet<string>(
                    existing.Select(x => Normalize(x.MaterialName)),
                    StringComparer.OrdinalIgnoreCase);

                foreach (var row in rows)
                {
                    if (existingOrderIds.Contains(row.OrderId))
                    {
                        errors.Add($"ردیف {row.Row}: ترتیب نمایش {row.OrderId} قبلاً در سیستم ثبت شده است.");
                    }
                    if (existingNames.Contains(row.MaterialName))
                    {
                        errors.Add($"ردیف {row.Row}: Material {row.MaterialName} قبلاً در سیستم ثبت شده است.");
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
                    var dto = new TblLnsCustomLensTypeMaterialDto
                    {
                        DesignTypeId = designTypeValue,
                        LensIndexId = lensIndexId,
                        DefineObjectId = row.DefineObjectId,
                        MaterialName = row.MaterialName,
                        OrderId = row.OrderId,
                        IsActive = row.IsActive
                    };
                    tblLnsCustomLensTypeMaterialService.Add(dto);
                }

                return Json(new { success = true, message = $"ایمپورت با موفقیت انجام شد. تعداد: {rows.Count}" });
            }
            catch
            {
                return Json(new
                {
                    success = false,
                    message = "خطا در پردازش فایل اکسل. لطفاً از سالم بودن فایل و صحیح بودن ستون ها مطمئن شوید."
                });
            }
        }

        private static IEnumerable<TblLnsCustomLensTypeMaterialDto> ApplySort(IEnumerable<TblLnsCustomLensTypeMaterialDto> items, string? sidx, string? sord)
        {
            if (string.IsNullOrWhiteSpace(sidx))
            {
                return items;
            }

            var prop = typeof(TblLnsCustomLensTypeMaterialDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
            {
                return items;
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

            var hasDefineObject = c0.Contains("warehouse") || c0.Contains("stock") || c0.Contains("item") || c0.Contains("define") || c0.Contains("کالا");
            var hasMaterial = c0.Contains("material") || c1.Contains("material") || c0.Contains("متریال") || c1.Contains("متریال");

            if (hasDefineObject)
            {
                map.DefineObjectIndex = 0;
                map.MaterialIndex = 1;
                map.OrderIndex = 2;
                map.StatusIndex = 3;
                return true;
            }

            if (hasMaterial || c0.Contains("order") || c1.Contains("order") || c2.Contains("order") || c1.Contains("ترتیب") || c2.Contains("ترتیب"))
            {
                map.DefineObjectIndex = -1;
                map.MaterialIndex = 0;
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

        private sealed class ExcelColumnMap
        {
            public int DefineObjectIndex { get; set; } = -1;
            public int MaterialIndex { get; set; } = 0;
            public int OrderIndex { get; set; } = 1;
            public int StatusIndex { get; set; } = 2;
        }

        [HttpPost]
        public JsonResult SearchDefineObject(string search, string field = "all")
        {
            var results = tblClrDefineObjectService.Search(search).Result ?? new List<TblClrDefineObjectDto>();
            var normalizedField = Normalize(field).ToLowerInvariant();
            var term = Normalize(search);
            if (!string.IsNullOrWhiteSpace(term))
            {
                results = results
                    .Where(item => MatchDefineObjectByField(item, term, normalizedField))
                    .ToList();
            }
            var items = new
            {
                results = results.Select(item => new
                {
                    id = item.DefineObjectId,
                    text = BuildDefineObjectDisplay(item, normalizedField),
                    name = Normalize(item.NameObject),
                    barcode = Normalize(item.TechnicalSpecs),
                    code = Normalize(item.CodeObject)
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

        private static bool MatchDefineObjectByField(TblClrDefineObjectDto item, string term, string field)
        {
            if (item == null || string.IsNullOrWhiteSpace(term))
            {
                return false;
            }

            var valueName = Normalize(item.NameObject);
            var valueBarcode = Normalize(item.TechnicalSpecs);
            var valueCode = Normalize(item.CodeObject);

            bool Match(string value) => value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;

            return field switch
            {
                "name" => Match(valueName),
                "barcode" => Match(valueBarcode),
                "code" => Match(valueCode),
                _ => Match(valueName) || Match(valueBarcode) || Match(valueCode)
            };
        }

        private static string BuildDefineObjectDisplay(TblClrDefineObjectDto item, string field)
        {
            var text = field switch
            {
                "name" => Normalize(item?.NameObject),
                "barcode" => Normalize(item?.TechnicalSpecs),
                "code" => Normalize(item?.CodeObject),
                _ => BuildDefineObjectText(item)
            };

            return string.IsNullOrWhiteSpace(text) ? BuildDefineObjectText(item) : text;
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

        private static string Normalize(string? value)
        {
            return value?.Trim() ?? string.Empty;
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


