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
	public class CustomLensTypesController(ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsBrandService tblLnsBrandService, ITblLnsDesignTypeService tblLnsDesignTypeService) : Controller
	{
		private const bool CustomFlag = true;

		[Authorize(Roles = "admin,CustomLensType_Index")]
		public IActionResult Index(int? brandId, bool embedded = false)
		{
			ViewBag.Embedded = embedded;
			if (brandId.HasValue && brandId.Value > 0)
			{
				var brand = tblLnsBrandService.GetById(brandId.Value).Result;
				if (brand == null || brand.IsSpecial == false || !brand.IsActive)
				{
					return NotFound();
				}
				ViewBag.BrandId = brandId.Value;
				ViewBag.BrandName = brand.Name;
			}
			else
			{
				ViewBag.BrandId = 0;
				ViewBag.BrandName = "همه برندها";
			}
			return View();
		}

		[Authorize(Roles = "admin,CustomLensType_Index")]
		public JsonResult Detail(int? brandId)
		{
			var payload = GetRequestPayload();
			var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;

			var allItems = tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>();
			var filtered = allItems.Where(IsCustomLensType);
			if (brandId.HasValue && brandId.Value > 0)
			{
				filtered = filtered.Where(x => x.BrandId == brandId.Value);
			}

			var brandLookup = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
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
						   id = p.LensTypeId,
						   brandName = p.BrandId.HasValue && brandLookup.ContainsKey(p.BrandId.Value) ? brandLookup[p.BrandId.Value] : "",
						   code = p.Code,
						   name = p.Name,
						   description = p.Description,
						   orderId = p.OrderId,
						   isActive = p.IsActive == true ? "فعال" : "غیر فعال",
						   corridor = p.IsCorridor
					   }
			};
			return Json(jsonData);
		}

		[Authorize(Roles = "admin,CustomLensType_Edit")]
		public IActionResult Edit(int id, int? brandId)
		{
			var item = tblLnsLensTypeService.GetById(id).Result;
			if (item == null || !IsCustomLensType(item) || (brandId.HasValue && brandId.Value > 0 && item.BrandId != brandId.Value))
			{
				return NotFound();
			}

			ViewBag.BrandId = brandId ?? 0;
			ViewBag.BrandName = item.BrandId.HasValue
				? (tblLnsBrandService.GetById(item.BrandId.Value).Result?.Name ?? string.Empty)
				: string.Empty;
			return View(item);
		}

		[Authorize(Roles = "admin,CustomLensType_Create")]
		public IActionResult Create(int? brandId)
		{
			var dto = new TblLnsLensTypeDto
			{
				IsActive = true
			};

			if (brandId.HasValue && brandId.Value > 0)
			{
				var brand = tblLnsBrandService.GetById(brandId.Value).Result;
				if (brand == null || brand.IsSpecial == false || !brand.IsActive)
				{
					return NotFound();
				}

				dto.BrandId = brandId.Value;
				ViewBag.BrandId = brandId.Value;
				ViewBag.BrandName = brand.Name ?? string.Empty;
			}
			else
			{
				ViewBag.BrandId = 0;
				ViewBag.BrandName = "همه برندها";
				var brands = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
					.Where(x => x.IsSpecial == CustomFlag && x.IsActive)
					.OrderBy(x => x.Name)
					.ToList();
				ViewBag.ListBrand = brands;
			}

			return View(dto);
		}

		[Authorize(Roles = "admin,CustomLensType_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsLensTypeDto dto)
		{
			try
			{
				if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Code) || dto.OrderId <= 0)
				{
					return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست." });
				}

				if (!dto.BrandId.HasValue || dto.BrandId.Value <= 0)
				{
					return Json(new { success = false, message = "برند نامعتبر است." });
				}

				var brand = tblLnsBrandService.GetById(dto.BrandId.Value).Result;
				if (brand == null || brand.IsSpecial == false || !brand.IsActive)
				{
					return Json(new { success = false, message = "برند نامعتبر است." });
				}

				dto.Name = Normalize(dto.Name);
				dto.Code = Normalize(dto.Code);
				dto.Description = Normalize(dto.Description);
				dto.IsSpecial = CustomFlag;

				var siblings = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
					.Where(x => IsCustomLensType(x) && x.BrandId == dto.BrandId.Value)
					.ToList();

				if (siblings.Any(x => x.OrderId == dto.OrderId))
				{
					return Json(new { success = false, message = "ترتیب نمایش تکراری است." });
				}
				if (siblings.Any(x => string.Equals(Normalize(x.Code), dto.Code, StringComparison.OrdinalIgnoreCase)))
				{
					return Json(new { success = false, message = "کد تکراری است." });
				}

				tblLnsLensTypeService.Add(dto);
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

		[Authorize(Roles = "admin,CustomLensType_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsLensTypeDto dto)
		{
			try
			{
				var existing = tblLnsLensTypeService.GetById(dto.LensTypeId).Result;
				if (existing == null || !IsCustomLensType(existing) || (dto.BrandId.HasValue && dto.BrandId.Value > 0 && existing.BrandId != dto.BrandId))
				{
					return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
				}

				var newName = Normalize(dto.Name);
				var newCode = Normalize(dto.Code);
				var newDescription = Normalize(dto.Description);
				var newOrderId = dto.OrderId;
				var newBrandId = dto.BrandId.HasValue && dto.BrandId.Value > 0 ? dto.BrandId.Value : existing.BrandId;

				if (newBrandId.HasValue && newBrandId.Value > 0)
				{
					var siblings = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
						.Where(x => IsCustomLensType(x) && x.BrandId == newBrandId.Value && x.LensTypeId != existing.LensTypeId)
						.ToList();

					if (siblings.Any(x => x.OrderId == newOrderId))
					{
						return Json(new { success = false, message = "ترتیب نمایش تکراری است." });
					}
					if (siblings.Any(x => string.Equals(Normalize(x.Code), newCode, StringComparison.OrdinalIgnoreCase)))
					{
						return Json(new { success = false, message = "کد تکراری است." });
					}
				}

				existing.Name = newName;
				existing.Code = newCode;
				existing.Description = newDescription;
				existing.OrderId = newOrderId;
				existing.IsActive = dto.IsActive;
				existing.IsCorridor = dto.IsCorridor;
				existing.IsSpecial = CustomFlag;
				if (newBrandId.HasValue && newBrandId.Value > 0)
				{
					existing.BrandId = newBrandId;
				}

				tblLnsLensTypeService.Update(existing);
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

		[Authorize(Roles = "admin,CustomLensType_Delete")]
		[HttpPost]
		public IActionResult Delete(int id, int? brandId)
		{
			var item = tblLnsLensTypeService.GetById(id).Result;
			if (item == null || !IsCustomLensType(item) || (brandId.HasValue && brandId.Value > 0 && item.BrandId != brandId.Value))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			var hasDesignTypes = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
				.Any(x => x.IsSpecial == CustomFlag && x.LensTypeId == id);
			if (hasDesignTypes)
			{
				return Json(new
				{
					success = false,
					message = "این لنز تایپ در فرم «Design Type» استفاده شده است و امکان حذف ندارد. ابتدا دیزاین‌های مرتبط را حذف کنید."
				});
			}

			tblLnsLensTypeService.Delete(id);
			return Json(new { success = true, message = "" });
		}

		[Authorize(Roles = "admin,CustomLensType_Index")]
		[HttpGet]
		public IActionResult ExportExcel(int? brandId)
		{
			var items = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
				.Where(IsCustomLensType);
			if (brandId.HasValue && brandId.Value > 0)
			{
				items = items.Where(x => x.BrandId == brandId.Value);
			}
			items = items
				.OrderBy(x => x.OrderId)
				.ThenBy(x => x.LensTypeId)
				.ToList();

			using var workbook = new XLWorkbook();
			var ws = workbook.AddWorksheet("CustomLensTypes");

			ws.Cell(1, 1).Value = "نام";
			ws.Cell(1, 2).Value = "کد";
			ws.Cell(1, 3).Value = "توضیحات";
			ws.Cell(1, 4).Value = "ترتیب نمایش";
			ws.Cell(1, 5).Value = "وضعیت";
			ws.Cell(1, 6).Value = "کریدور";

			var row = 2;
			foreach (var item in items)
			{
				ws.Cell(row, 1).Value = Normalize(item.Name);
				ws.Cell(row, 2).Value = Normalize(item.Code);
				ws.Cell(row, 3).Value = Normalize(item.Description);
				ws.Cell(row, 4).Value = item.OrderId;
				ws.Cell(row, 5).Value = item.IsActive ? "فعال" : "غیرفعال";
				ws.Cell(row, 6).Value = item.IsCorridor ? "دارد" : "ندارد";
				row++;
			}

			ws.Columns().AdjustToContents();

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomLensTypes.xlsx");
		}

		[Authorize(Roles = "admin,CustomLensType_Create")]
		[HttpPost]
		public IActionResult ImportExcel(IFormFile file, int? brandId)
		{
			if (file == null || file.Length == 0)
			{
				return Json(new { success = false, message = "فایلی انتخاب نشده است." });
			}
			if (!brandId.HasValue || brandId.Value <= 0)
			{
				return Json(new { success = false, message = "برند نامعتبر است." });
			}

			var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
			if (extension != ".xlsx" && extension != ".xls")
			{
				return Json(new { success = false, message = "فرمت فایل معتبر نیست. فقط فایل اکسل با پسوند xlsx یا xls قابل قبول است." });
			}

			try
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

				var rows = new List<(int Row, string Name, string Code, string Description, int OrderId, bool IsActive, bool IsCorridor)>();
				var errors = new List<string>();
				var orderIdRows = new Dictionary<int, List<int>>();
				var codeRows = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

				using var stream = file.OpenReadStream();
				using var reader = ExcelReaderFactory.CreateReader(stream);

				var rowIndex = 0;
				while (reader.Read())
				{
					rowIndex++;
					if (rowIndex == 1 && LooksLikeHeader(reader))
						continue;

					if (reader.FieldCount < 4)
					{
						errors.Add($"ردیف {rowIndex}: تعداد ستون‌ها کمتر از مقدار مورد نیاز است. ستون‌های لازم: نام، کد، توضیحات، ترتیب نمایش، وضعیت (اختیاری)، کریدور (اختیاری).");
						continue;
					}

					var name = Normalize(ReadCellString(reader, 0));
					var code = Normalize(ReadCellString(reader, 1));
					var description = Normalize(ReadCellString(reader, 2));
					var orderId = ReadCellInt(reader, 3);
					var statusText = ReadCellString(reader, 4);
					var isActive = ParseStatus(statusText, out var statusValid);
					var corridorText = ReadCellString(reader, 5);
					var isCorridor = ParseCorridor(corridorText, out var corridorValid);

					if (string.IsNullOrWhiteSpace(name) &&
						string.IsNullOrWhiteSpace(code) &&
						string.IsNullOrWhiteSpace(description) &&
						orderId == null)
					{
						continue;
					}

					if (string.IsNullOrWhiteSpace(name))
					{
						errors.Add($"ردیف {rowIndex}: ستون نام خالی است.");
						continue;
					}
					if (string.IsNullOrWhiteSpace(code))
					{
						errors.Add($"ردیف {rowIndex}: ستون کد خالی است.");
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
					if (!corridorValid)
					{
						errors.Add($"ردیف {rowIndex}: مقدار ستون کریدور نامعتبر است. مقادیر مجاز: دارد/ندارد یا 1/0 یا true/false یا بله/خیر.");
						continue;
					}

					rows.Add((rowIndex, name, code, description ?? string.Empty, orderId.Value, isActive, isCorridor));

					if (!orderIdRows.TryGetValue(orderId.Value, out var orderList))
					{
						orderList = new List<int>();
						orderIdRows[orderId.Value] = orderList;
					}
					orderList.Add(rowIndex);

					if (!codeRows.TryGetValue(code, out var codeList))
					{
						codeList = new List<int>();
						codeRows[code] = codeList;
					}
					codeList.Add(rowIndex);
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

				foreach (var dup in codeRows.Where(x => x.Value.Count > 1))
				{
					var rowList = string.Join("، ", dup.Value);
					errors.Add($"کد تکراری در فایل: {dup.Key} (ردیف‌های {rowList})");
				}

				var existing = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
					.Where(x => IsCustomLensType(x) && x.BrandId == brandId.Value)
					.ToList();

				var existingOrderIds = existing.Select(x => x.OrderId).ToHashSet();
				var existingCodes = new HashSet<string>(
					existing.Select(x => Normalize(x.Code)),
					StringComparer.OrdinalIgnoreCase);

				foreach (var row in rows)
				{
					if (existingOrderIds.Contains(row.OrderId))
					{
						errors.Add($"ردیف {row.Row}: ترتیب نمایش {row.OrderId} قبلاً در سیستم ثبت شده است.");
					}
					if (existingCodes.Contains(row.Code))
					{
						errors.Add($"ردیف {row.Row}: کد {row.Code} قبلاً در سیستم ثبت شده است.");
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
					var dto = new TblLnsLensTypeDto
					{
						Name = row.Name,
						Code = row.Code,
						Description = row.Description,
						OrderId = row.OrderId,
						IsActive = row.IsActive,
						IsCorridor = row.IsCorridor,
						IsSpecial = CustomFlag,
						BrandId = brandId.Value
					};
					tblLnsLensTypeService.Add(dto);
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

		private static IEnumerable<TblLnsLensTypeDto> ApplySort(IEnumerable<TblLnsLensTypeDto> items, string? sidx, string? sord)
		{
			if (string.IsNullOrWhiteSpace(sidx))
			{
				return items;
			}

			var prop = typeof(TblLnsLensTypeDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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
			var c3 = ReadCellString(reader, 3).ToLowerInvariant();

			return c0.Contains("name") || c0.Contains("نام")
				|| c1.Contains("code") || c1.Contains("کد")
				|| c3.Contains("order") || c3.Contains("ترتیب");
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

		private static bool ParseCorridor(string? text, out bool isValid)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				isValid = true;
				return false;
			}

			var normalized = text.Trim().ToLowerInvariant();
			if (normalized == "1" || normalized == "true" || normalized == "دارد" || normalized == "بله" || normalized == "yes" || normalized == "corridor")
			{
				isValid = true;
				return true;
			}
			if (normalized == "0" || normalized == "false" || normalized == "ندارد" || normalized == "خیر" || normalized == "no")
			{
				isValid = true;
				return false;
			}

			isValid = false;
			return false;
		}

		[Authorize(Roles = "admin,CustomLensType_Edit")]
		[HttpPost]
		public IActionResult ToggleCorridor(int id, int? brandId)
		{
			var existing = tblLnsLensTypeService.GetById(id).Result;
			if (existing == null || !IsCustomLensType(existing) || (brandId.HasValue && brandId.Value > 0 && existing.BrandId != brandId.Value))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			existing.IsCorridor = !existing.IsCorridor;
			tblLnsLensTypeService.Update(existing);
			return Json(new { success = true, message = "", value = existing.IsCorridor });
		}

		private static string Normalize(string? value)
		{
			return value?.Trim() ?? string.Empty;
		}

		private static bool IsCustomLensType(TblLnsLensTypeDto item)
		{
			return item != null && item.IsSpecial == CustomFlag;
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
