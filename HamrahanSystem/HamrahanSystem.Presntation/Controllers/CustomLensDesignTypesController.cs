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
using System.Reflection;
using System.Text;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class CustomLensDesignTypesController(ITblLnsDesignTypeService tblLnsDesignTypeService, ITblLnsLensTypeService tblLnsLensTypeService, ITblLnsBrandService tblLnsBrandService, ITblLnsCustomLensTypeCoatingService tblLnsCustomLensTypeCoatingService, ITblLnsCustomDesignTypeAdditionService tblLnsCustomDesignTypeAdditionService) : Controller
	{
		private const bool CustomFlag = true;

		[Authorize(Roles = "admin,CustomLensDesignType_Index")]
		public IActionResult Index(int? lensTypeId, bool embedded = false)
		{
			ViewBag.Embedded = embedded;
			if (lensTypeId.HasValue && lensTypeId.Value > 0)
			{
				if (!TryGetLensTypeHierarchy(lensTypeId.Value, requireActive: true, out var lensType, out var brand))
				{
					return NotFound();
				}

				ViewBag.LensTypeId = lensTypeId.Value;
				ViewBag.LensTypeName = lensType.Name ?? string.Empty;
				ViewBag.BrandName = brand?.Name ?? string.Empty;
			}
			else
			{
				ViewBag.LensTypeId = 0;
				ViewBag.LensTypeName = "همه لنز تایپ‌ها";
				ViewBag.BrandName = "همه برندها";
			}

			return View();
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Index")]
		public JsonResult Detail(int? lensTypeId)
		{
			var payload = GetRequestPayload();
			var itemGrid = JsonConvert.DeserializeObject<GridDto>(payload) ?? new GridDto();
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;

			var allItems = tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>();
			var filtered = allItems.Where(IsCustomDesignType);
			if (lensTypeId.HasValue && lensTypeId.Value > 0)
			{
				filtered = filtered.Where(x => x.LensTypeId == lensTypeId.Value);
			}

			var lensTypes = (tblLnsLensTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsLensTypeDto>())
				.ToList();
			var lensTypeLookup = lensTypes
				.ToDictionary(x => x.LensTypeId, x => x.Name ?? string.Empty);
			var lensTypeBrandLookup = lensTypes
				.Where(x => x.BrandId.HasValue)
				.ToDictionary(x => x.LensTypeId, x => x.BrandId!.Value);
			var brandLookup = (tblLnsBrandService.GetAll().Result ?? Enumerable.Empty<TblLnsBrandDto>())
				.ToDictionary(x => x.BrandId, x => x.Name ?? string.Empty);

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
						   id = p.DesignTypeId,
						   brandName = p.LensTypeId.HasValue && lensTypeBrandLookup.ContainsKey(p.LensTypeId.Value) && brandLookup.ContainsKey(lensTypeBrandLookup[p.LensTypeId.Value])
							   ? brandLookup[lensTypeBrandLookup[p.LensTypeId.Value]]
							   : "",
						   lensTypeName = p.LensTypeId.HasValue && lensTypeLookup.ContainsKey(p.LensTypeId.Value) ? lensTypeLookup[p.LensTypeId.Value] : "",
						   code = p.Code,
						   name = p.Name,
						   description = p.Description,
						   orderId = p.OrderId,
						   isActive = p.IsActive == true ? "فعال" : "غیر فعال",
						   sphPlus = p.SphPlus,
						   sphMinus = p.SphMinus,
						   addition = p.Addition,
						   additionValue = p.AdditionValue
					   }
			};
			return Json(jsonData);
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Create")]
		public IActionResult Create(int? lensTypeId)
		{
			if (!lensTypeId.HasValue || lensTypeId.Value <= 0)
			{
				return NotFound();
			}

			if (!TryGetLensTypeHierarchy(lensTypeId.Value, requireActive: true, out var lensType, out var brand))
			{
				return NotFound();
			}

			ViewBag.BrandName = brand?.Name ?? string.Empty;
			ViewBag.LensTypeName = lensType.Name ?? string.Empty;

			var dto = new TblLnsDesignTypeDto
			{
				LensTypeId = lensTypeId.Value,
				OrderId = 1,
				IsActive = true,
				IsSpecial = CustomFlag
			};

			return View(dto);
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsDesignTypeDto dto)
		{
			try
			{
				if (!dto.LensTypeId.HasValue || dto.LensTypeId.Value <= 0)
				{
					return Json(new { success = false, message = "Lens Type is invalid." });
				}

				if (!TryGetLensTypeHierarchy(dto.LensTypeId.Value, requireActive: true, out _, out _))
				{
					return Json(new { success = false, message = "Lens Type is invalid." });
				}

				dto.Name = Normalize(dto.Name);
				dto.Code = Normalize(dto.Code);
				dto.Description = Normalize(dto.Description);

				if (string.IsNullOrWhiteSpace(dto.Name))
				{
					return Json(new { success = false, message = "Name is required." });
				}
				if (string.IsNullOrWhiteSpace(dto.Code))
				{
					return Json(new { success = false, message = "Code is required." });
				}
				if (dto.OrderId <= 0)
				{
					return Json(new { success = false, message = "Order must be greater than zero." });
				}

				var siblings = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
					.Where(x => IsCustomDesignType(x) && x.LensTypeId == dto.LensTypeId.Value)
					.ToList();

				if (siblings.Any(x => x.OrderId == dto.OrderId))
				{
					return Json(new { success = false, message = "Display order is duplicate." });
				}
				if (siblings.Any(x => string.Equals(Normalize(x.Code), dto.Code, StringComparison.OrdinalIgnoreCase)))
				{
					return Json(new { success = false, message = "Code is duplicate." });
				}

				dto.IsSpecial = CustomFlag;
				if (!dto.Addition)
				{
					dto.AdditionValue = null;
				}

				tblLnsDesignTypeService.Add(dto);
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

		[Authorize(Roles = "admin,CustomLensDesignType_Edit")]
		public IActionResult Edit(int id, int? lensTypeId)
		{
			var item = tblLnsDesignTypeService.GetById(id).Result;
			if (item == null || !IsCustomDesignType(item) || (lensTypeId.HasValue && lensTypeId.Value > 0 && item.LensTypeId != lensTypeId.Value))
			{
				return NotFound();
			}

			ViewBag.LensTypeId = lensTypeId ?? 0;
			if (item.LensTypeId.HasValue && item.LensTypeId.Value > 0
				&& TryGetLensTypeHierarchy(item.LensTypeId.Value, requireActive: false, out var lensType, out var brand))
			{
				ViewBag.BrandName = brand?.Name ?? string.Empty;
				ViewBag.LensTypeName = lensType.Name ?? string.Empty;
			}
			else
			{
				ViewBag.BrandName = string.Empty;
				ViewBag.LensTypeName = string.Empty;
			}
			return View(item);
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsDesignTypeDto dto)
		{
			try
			{
				var existing = tblLnsDesignTypeService.GetById(dto.DesignTypeId).Result;
				if (existing == null || !IsCustomDesignType(existing) || (dto.LensTypeId.HasValue && dto.LensTypeId.Value > 0 && existing.LensTypeId != dto.LensTypeId))
				{
					return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
				}

				var newName = Normalize(dto.Name);
				var newCode = Normalize(dto.Code);
				var newDescription = Normalize(dto.Description);
				var newOrderId = dto.OrderId;
				var newLensTypeId = dto.LensTypeId.HasValue && dto.LensTypeId.Value > 0 ? dto.LensTypeId : existing.LensTypeId;

				if (newLensTypeId.HasValue && newLensTypeId.Value > 0)
				{
					var siblings = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
						.Where(x => IsCustomDesignType(x) && x.LensTypeId == newLensTypeId.Value && x.DesignTypeId != existing.DesignTypeId)
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
				existing.SphPlus = dto.SphPlus;
				existing.SphMinus = dto.SphMinus;
				if (!dto.Addition && existing.Addition)
				{
					var hasAdditions = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
						.Any(x => x.DesignTypeId == existing.DesignTypeId);
					if (hasAdditions)
					{
						return Json(new { success = false, message = "برای غیرفعال کردن ادیشن ابتدا مقادیر ادیشن را حذف کنید." });
					}
				}

				existing.Addition = dto.Addition;
				if (!existing.Addition)
				{
					existing.AdditionValue = null;
				}
				existing.IsSpecial = CustomFlag;
				if (dto.LensTypeId.HasValue && dto.LensTypeId.Value > 0)
				{
					existing.LensTypeId = dto.LensTypeId;
				}

				tblLnsDesignTypeService.Update(existing);
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

		[Authorize(Roles = "admin,CustomLensDesignType_Delete")]
		[HttpPost]
		public IActionResult Delete(int id, int? lensTypeId)
		{
			var item = tblLnsDesignTypeService.GetById(id).Result;
			if (item == null || !IsCustomDesignType(item) || (lensTypeId.HasValue && lensTypeId.Value > 0 && item.LensTypeId != lensTypeId.Value))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			var hasCoatings = (tblLnsCustomLensTypeCoatingService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>())
				.Any(x => x.DesignTypeId == id);
			if (hasCoatings)
			{
				return Json(new
				{
					success = false,
					message = "این دیزاین در فرم «Lens Type & Coating» استفاده شده است و امکان حذف ندارد. ابتدا موارد مرتبط را حذف کنید."
				});
			}

			var hasAdditions = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
				.Any(x => x.DesignTypeId == id);
			if (hasAdditions)
			{
				return Json(new
				{
					success = false,
					message = "برای حذف این دیزاین ابتدا ادیشن‌های ثبت شده را حذف کنید."
				});
			}

			tblLnsDesignTypeService.Delete(id);
			return Json(new { success = true, message = "" });
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Edit")]
		[HttpPost]
		public IActionResult ToggleSphPlus(int id, int? lensTypeId)
		{
			var item = tblLnsDesignTypeService.GetById(id).Result;
			if (item == null || !IsCustomDesignType(item) || (lensTypeId.HasValue && lensTypeId.Value > 0 && item.LensTypeId != lensTypeId.Value))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			item.SphPlus = !item.SphPlus;
			tblLnsDesignTypeService.Update(item);
			return Json(new { success = true, message = "", value = item.SphPlus });
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Edit")]
		[HttpPost]
		public IActionResult ToggleSphMinus(int id, int? lensTypeId)
		{
			var item = tblLnsDesignTypeService.GetById(id).Result;
			if (item == null || !IsCustomDesignType(item) || (lensTypeId.HasValue && lensTypeId.Value > 0 && item.LensTypeId != lensTypeId.Value))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			item.SphMinus = !item.SphMinus;
			tblLnsDesignTypeService.Update(item);
			return Json(new { success = true, message = "", value = item.SphMinus });
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Edit")]
		[HttpPost]
		public IActionResult ToggleAddition(int id, int? lensTypeId)
		{
			var item = tblLnsDesignTypeService.GetById(id).Result;
			if (item == null || !IsCustomDesignType(item) || (lensTypeId.HasValue && lensTypeId.Value > 0 && item.LensTypeId != lensTypeId.Value))
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			if (item.Addition)
			{
				var hasAdditions = (tblLnsCustomDesignTypeAdditionService.GetAll().Result ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>())
					.Any(x => x.DesignTypeId == item.DesignTypeId);
				if (hasAdditions)
				{
					return Json(new { success = false, message = "برای غیرفعال کردن ادیشن ابتدا مقادیر ادیشن را حذف کنید." });
				}

				item.Addition = false;
				item.AdditionValue = null;
			}
			else
			{
				item.Addition = true;
			}

			tblLnsDesignTypeService.Update(item);
			return Json(new { success = true, message = "", value = item.Addition });
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Index")]
		[HttpGet]
		public IActionResult ExportExcel(int? lensTypeId)
		{
			var items = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
				.Where(IsCustomDesignType);
			if (lensTypeId.HasValue && lensTypeId.Value > 0)
			{
				items = items.Where(x => x.LensTypeId == lensTypeId.Value);
			}

			var ordered = items
				.OrderBy(x => x.OrderId)
				.ThenBy(x => x.DesignTypeId)
				.ToList();

			using var workbook = new XLWorkbook();
			var ws = workbook.AddWorksheet("CustomDesignTypes");

			ws.Cell(1, 1).Value = "نام";
			ws.Cell(1, 2).Value = "کد";
			ws.Cell(1, 3).Value = "توضیحات";
			ws.Cell(1, 4).Value = "ترتیب نمایش";
			ws.Cell(1, 5).Value = "وضعیت";

			var row = 2;
			foreach (var item in ordered)
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
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomDesignTypes.xlsx");
		}

		[Authorize(Roles = "admin,CustomLensDesignType_Create")]
		[HttpPost]
		public IActionResult ImportExcel(IFormFile file, int? lensTypeId)
		{
			if (file == null || file.Length == 0)
			{
				return Json(new { success = false, message = "فایلی انتخاب نشده است." });
			}
			if (!lensTypeId.HasValue || lensTypeId.Value <= 0)
			{
				return Json(new { success = false, message = "لنز تایپ نامعتبر است." });
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

				var existing = (tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>())
					.Where(x => IsCustomDesignType(x) && x.LensTypeId == lensTypeId.Value)
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
					var dto = new TblLnsDesignTypeDto
					{
						Name = row.Name,
						Code = row.Code,
						Description = row.Description,
						OrderId = row.OrderId,
						IsActive = row.IsActive,
						IsSpecial = CustomFlag,
						LensTypeId = lensTypeId.Value
					};
					tblLnsDesignTypeService.Add(dto);
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

		private static IEnumerable<TblLnsDesignTypeDto> ApplySort(IEnumerable<TblLnsDesignTypeDto> items, string? sidx, string? sord)
		{
			if (string.IsNullOrWhiteSpace(sidx))
			{
				return items;
			}

			var prop = typeof(TblLnsDesignTypeDto).GetProperty(sidx, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
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

		private static bool IsCustomDesignType(TblLnsDesignTypeDto item)
		{
			return item != null && item.IsSpecial == CustomFlag;
		}

		private bool TryGetLensTypeHierarchy(int lensTypeId, bool requireActive, out TblLnsLensTypeDto lensType, out TblLnsBrandDto? brand)
		{
			lensType = null!;
			brand = null;

			var currentLensType = tblLnsLensTypeService.GetById(lensTypeId).Result;
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

			lensType = currentLensType;
			brand = currentBrand;
			return true;
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
					'۰' => '0',
					'۱' => '1',
					'۲' => '2',
					'۳' => '3',
					'۴' => '4',
					'۵' => '5',
					'۶' => '6',
					'۷' => '7',
					'۸' => '8',
					'۹' => '9',
					'٠' => '0',
					'١' => '1',
					'٢' => '2',
					'٣' => '3',
					'٤' => '4',
					'٥' => '5',
					'٦' => '6',
					'٧' => '7',
					'٨' => '8',
					'٩' => '9',
					',' => '.',
					'/' => '.',
					_ => ch
				});
			}

			return sb.ToString();
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
