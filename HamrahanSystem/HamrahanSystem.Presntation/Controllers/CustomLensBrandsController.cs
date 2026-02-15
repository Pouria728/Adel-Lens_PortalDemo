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
	public class CustomLensBrandsController(ITblLnsBrandService tblLnsBrandService, ITblLnsLensTypeService tblLnsLensTypeService) : Controller
	{
		private const bool CustomFlag = true;

		[Authorize(Roles = "admin,CustomLensBrand_Index")]
		public IActionResult Index(bool embedded = false)
		{
			ViewBag.Embedded = embedded;
			return View();
		}

		[Authorize(Roles = "admin,CustomLensBrand_Index")]
		public JsonResult Detail()
		{
			var payload = GetRequestPayload();
			var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;

			var allItems = tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>();
			var filtered = allItems.Where(IsCustomBrand);

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
						   id = p.BrandId,
						   code = p.Code,
						   name = p.Name,
						   description = p.Description,
						   orderId = p.OrderId,
						   isActive = p.IsActive == true ? "فعال" : "غیر فعال"
					   }
			};
			return Json(jsonData);
		}

		[Authorize(Roles = "admin,CustomLensBrand_Create")]
		public IActionResult Create()
		{
			var dto = new TblLnsBrandDto
			{
				IsActive = true
			};
			return View(dto);
		}

		[Authorize(Roles = "admin,CustomLensBrand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsBrandDto dto)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Code) || dto.OrderId <= 0)
				{
					return Json(new { success = false, message = "اطلاعات وارد شده معتبر نیست." });
				}

				dto.Name = Normalize(dto.Name);
				dto.Code = Normalize(dto.Code);
				dto.Description = Normalize(dto.Description);
				dto.IsSpecial = CustomFlag;
				dto.IsStock = false;
				dto.IsStockGranty = false;

				tblLnsBrandService.Add(dto);
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

		[Authorize(Roles = "admin,CustomLensBrand_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsBrandService.GetById(id).Result;
			if (item == null || !IsCustomBrand(item))
			{
				return NotFound();
			}

			return View(item);
		}

		[Authorize(Roles = "admin,CustomLensBrand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsBrandDto dto)
		{
			try
			{
				var existing = tblLnsBrandService.GetById(dto.BrandId).Result;
				if (existing == null || !IsCustomBrand(existing))
				{
					return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
				}

				existing.Name = Normalize(dto.Name);
				existing.Code = Normalize(dto.Code);
				existing.Description = Normalize(dto.Description);
				existing.OrderId = dto.OrderId;
				existing.IsActive = dto.IsActive;
				existing.IsSpecial = CustomFlag;
				existing.IsStock = false;
				existing.IsStockGranty = false;

				tblLnsBrandService.Update(existing);
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

		[Authorize(Roles = "admin,CustomLensBrand_Delete")]
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var item = tblLnsBrandService.GetById(id).Result;
			if (item == null || !IsCustomBrand(item))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			var hasLensTypes = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
				.Any(x => x.IsSpecial == CustomFlag && x.BrandId == id);
			if (hasLensTypes)
			{
				return Json(new
				{
					success = false,
					message = "این برند در فرم «Lens Type» استفاده شده است و امکان حذف ندارد. ابتدا لنز تایپ‌های مرتبط را حذف کنید."
				});
			}

			tblLnsBrandService.Delete(id);
			return Json(new { success = true, message = "" });
		}

		[Authorize(Roles = "admin,CustomLensBrand_Index")]
		[HttpGet]
		public IActionResult ExportExcel()
		{
			var items = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
				.Where(IsCustomBrand)
				.OrderBy(x => x.OrderId)
				.ThenBy(x => x.BrandId)
				.ToList();

			using var workbook = new XLWorkbook();
			var ws = workbook.AddWorksheet("CustomBrands");

			ws.Cell(1, 1).Value = "نام";
			ws.Cell(1, 2).Value = "کد";
			ws.Cell(1, 3).Value = "توضیحات";
			ws.Cell(1, 4).Value = "ترتیب نمایش";
			ws.Cell(1, 5).Value = "وضعیت";

			var row = 2;
			foreach (var item in items)
			{
				ws.Cell(row, 1).Value = Normalize(item.Name);
				ws.Cell(row, 2).Value = Normalize(item.Code);
				ws.Cell(row, 3).Value = Normalize(item.Description);
				ws.Cell(row, 4).Value = item.OrderId;
				ws.Cell(row, 5).Value = item.IsActive ? "فعال" : "غیرفعال";
				row++;
			}

			ws.Columns().AdjustToContents();

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomBrands.xlsx");
		}

		[Authorize(Roles = "admin,CustomLensBrand_Create")]
		[HttpPost]
		public IActionResult ImportExcel(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return Json(new { success = false, message = "فایلی انتخاب نشده است." });
			}

			var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
			if (extension != ".xlsx" && extension != ".xls")
			{
				return Json(new { success = false, message = "فرمت فایل معتبر نیست. فقط فایل اکسل با پسوند xlsx یا xls قابل قبول است." });
			}

			try
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

				var rows = new List<(int Row, string Name, string Code, string Description, int OrderId, bool IsActive)>();
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
						errors.Add($"ردیف {rowIndex}: تعداد ستون‌ها کمتر از مقدار مورد نیاز است. ستون‌های لازم: نام، کد، توضیحات، ترتیب نمایش، وضعیت (اختیاری).");
						continue;
					}

					var name = Normalize(ReadCellString(reader, 0));
					var code = Normalize(ReadCellString(reader, 1));
					var description = Normalize(ReadCellString(reader, 2));
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

					rows.Add((rowIndex, name, code, description ?? string.Empty, orderId.Value, isActive));

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

				var existing = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
					.Where(IsCustomBrand)
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
					var dto = new TblLnsBrandDto
					{
						Name = row.Name,
						Code = row.Code,
						Description = row.Description,
						OrderId = row.OrderId,
						IsActive = row.IsActive,
						IsSpecial = CustomFlag,
						IsStock = false,
						IsStockGranty = false
					};
					tblLnsBrandService.Add(dto);
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

		private static IEnumerable<TblLnsBrandDto> ApplySort(IEnumerable<TblLnsBrandDto> items, string? sidx, string? sord)
		{
			if (string.IsNullOrWhiteSpace(sidx))
			{
				return items;
			}

			var prop = typeof(TblLnsBrandDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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

		private static string Normalize(string? value)
		{
			return value?.Trim() ?? string.Empty;
		}

		private static bool IsCustomBrand(TblLnsBrandDto item)
		{
			return item != null && item.IsSpecial == CustomFlag && item.IsStock == false && item.IsStockGranty == false;
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
