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
	public class CustomLensBrandsController(
		ITblLnsBrandService tblLnsBrandService,
		ITblLnsLensTypeService tblLnsLensTypeService,
		ITblLnsDesignTypeService tblLnsDesignTypeService,
		ITblLnsCustomLensTypeCoatingService tblLnsCustomLensTypeCoatingService,
		ITblLnsCustomLensIndexService tblLnsCustomLensIndexService,
		ITblLnsCustomLensTypeMaterialService tblLnsCustomLensTypeMaterialService,
		ITblLnsCustomDesignTypeAdditionService tblLnsCustomDesignTypeAdditionService,
		ITblLnsCustomSphService tblLnsCustomSphService,
		ITblClrDefineObjectService tblClrDefineObjectService) : Controller
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

		[Authorize(Roles = "admin,CustomLensBrand_Index")]
		[HttpGet]
		public IActionResult ExportCombinedSampleExcel()
		{
			var headers = new[]
			{
				"BrandName",
				"BrandCode",
				"BrandOrder",
				"BrandStatus",
				"LensTypeName",
				"LensTypeCode",
				"LensTypeOrder",
				"LensTypeStatus",
				"LensTypeIsCorridor",
				"DesignTypeName",
				"DesignTypeCode",
				"DesignTypeOrder",
				"DesignTypeStatus",
				"DesignTypeSphPlus",
				"DesignTypeSphMinus",
				"DesignTypeAddition",
				"CoatingName",
				"CoatingOrder",
				"CoatingStatus",
				"CoatingIsDefault",
				"LensIndexName",
				"LensIndexOrder",
				"LensIndexStatus",
				"LensIndexHasColoringType",
				"MaterialName",
				"MaterialOrder",
				"MaterialStatus",
				"ProductBarcode",
				"AdditionValue",
				"AdditionOrder",
				"AdditionStatus",
				"AdditionMin",
				"AdditionMax",
				"AdditionStep",
				"SphMin",
				"SphMax",
				"SphStep"
			};

			var sampleValues = new[]
			{
				"Sample Brand",
				"SAMPLE-BRAND",
				"1",
				"Active",
				"Progressive",
				"PROG",
				"1",
				"Active",
				"1",
				"FreeForm",
				"FF",
				"1",
				"Active",
				"1",
				"1",
				"1",
				"HMC",
				"1",
				"Active",
				"1",
				"1.60",
				"1",
				"Active",
				"0",
				"Organic",
				"1",
				"Active",
				"PUT-WAREHOUSE-BARCODE-HERE",
				"",
				"1",
				"Active",
				"0.75",
				"3.00",
				"0.25",
				"-10.00",
				"6.00",
				"0.25"
			};

			using var workbook = new XLWorkbook();
			var ws = workbook.AddWorksheet("CustomLensImport");

			for (var i = 0; i < headers.Length; i++)
			{
				var cell = ws.Cell(1, i + 1);
				cell.Value = headers[i];
				cell.Style.Font.Bold = true;
				cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#EAF2FF");
				ws.Cell(2, i + 1).Value = sampleValues[i];
			}

			ws.SheetView.FreezeRows(1);
			ws.Columns().AdjustToContents();

			using var stream = new MemoryStream();
			workbook.SaveAs(stream);
			var content = stream.ToArray();
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomLensImportSample.xlsx");
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

		[Authorize(Roles = "admin,CustomLensBrand_Create")]
		[HttpPost]
		public IActionResult ImportCombinedExcel(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return Json(new { success = false, processed = 0, message = "No file selected." });
			}

			var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
			if (extension != ".xlsx" && extension != ".xls")
			{
				return Json(new { success = false, processed = 0, message = "Invalid Excel file. Use xlsx or xls." });
			}

			try
			{
				Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

				var readErrors = new List<string>();
				var rows = ReadCombinedImportRows(file, readErrors);
				if (rows.Count == 0 && readErrors.Count == 0)
				{
					readErrors.Add("No valid rows were found.");
				}

				var summary = new CombinedImportSummary();
				var context = LoadCombinedImportContext();
				var errors = new List<string>(readErrors);

				foreach (var row in rows)
				{
					var rowErrors = ValidateCombinedRow(row, out var parsed);
					if (rowErrors.Count > 0)
					{
						errors.AddRange(rowErrors.Select(x => $"Row {row.RowNumber}: {x}"));
						continue;
					}

					try
					{
						var brand = EnsureBrand(parsed, context, summary);
						var lensType = EnsureLensType(parsed, brand.BrandId, context, summary);
						var designType = EnsureDesignType(parsed, lensType.LensTypeId, context, summary);

						if (!string.IsNullOrWhiteSpace(parsed.CoatingName))
						{
							EnsureCoating(parsed, designType, context, summary);
						}

						var lensIndex = EnsureLensIndex(parsed, designType.DesignTypeId, context, summary);
						EnsureMaterial(parsed, designType.DesignTypeId, lensIndex.CustomLensIndexId, context, summary);

						foreach (var additionValue in parsed.AdditionValues)
						{
							EnsureAddition(parsed, designType.DesignTypeId, additionValue, context, summary);
						}

						foreach (var sphValue in parsed.SphValues)
						{
							EnsureSph(sphValue, context, summary);
						}

						summary.ProcessedRows++;
					}
					catch (InvalidOperationException ex)
					{
						errors.Add($"Row {row.RowNumber}: {ex.Message}");
					}
				}

				var message = errors.Count == 0
					? $"Import completed. Rows: {summary.ProcessedRows}"
					: $"Import completed with row errors. Imported rows: {summary.ProcessedRows}. Errors: {errors.Count}";

				return Json(new
				{
					success = errors.Count == 0,
					processed = summary.ProcessedRows,
					message,
					errors,
					summary
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					processed = 0,
					message = "Error while reading Excel file.",
					errors = new[] { ex.Message }
				});
			}
		}

		private List<CombinedLensImportRow> ReadCombinedImportRows(IFormFile file, List<string> errors)
		{
			using var stream = file.OpenReadStream();
			using var reader = ExcelReaderFactory.CreateReader(stream);

			if (!reader.Read())
			{
				errors.Add("Excel file is empty.");
				return new List<CombinedLensImportRow>();
			}

			var columns = CombinedLensImportColumns.FromHeader(reader, errors);
			if (errors.Count > 0)
			{
				return new List<CombinedLensImportRow>();
			}

			var rows = new List<CombinedLensImportRow>();
			var rowNumber = 1;
			while (reader.Read())
			{
				rowNumber++;
				if (IsEmptyRow(reader))
				{
					continue;
				}

				rows.Add(CombinedLensImportRow.FromReader(reader, columns, rowNumber));
			}

			return rows;
		}

		private List<string> ValidateCombinedRow(CombinedLensImportRow row, out ParsedCombinedLensRow parsed)
		{
			parsed = new ParsedCombinedLensRow { RowNumber = row.RowNumber };
			var errors = new List<string>();

			parsed.BrandName = RequireText(row.BrandName, "BrandName", errors);
			parsed.BrandCode = RequireText(row.BrandCode, "BrandCode", errors);
			parsed.BrandOrder = RequirePositiveInt(row.BrandOrder, "BrandOrder", errors);
			parsed.BrandStatus = ParseFlexibleBool(row.BrandStatus, true, "BrandStatus", errors);

			parsed.LensTypeName = RequireText(row.LensTypeName, "LensTypeName", errors);
			parsed.LensTypeCode = RequireText(row.LensTypeCode, "LensTypeCode", errors);
			parsed.LensTypeOrder = RequirePositiveInt(row.LensTypeOrder, "LensTypeOrder", errors);
			parsed.LensTypeStatus = ParseFlexibleBool(row.LensTypeStatus, true, "LensTypeStatus", errors);
			parsed.LensTypeIsCorridor = ParseFlexibleBool(row.LensTypeIsCorridor, false, "LensTypeIsCorridor", errors);

			parsed.DesignTypeName = RequireText(row.DesignTypeName, "DesignTypeName", errors);
			parsed.DesignTypeCode = RequireText(row.DesignTypeCode, "DesignTypeCode", errors);
			parsed.DesignTypeOrder = RequirePositiveInt(row.DesignTypeOrder, "DesignTypeOrder", errors);
			parsed.DesignTypeStatus = ParseFlexibleBool(row.DesignTypeStatus, true, "DesignTypeStatus", errors);
			parsed.DesignTypeSphPlus = ParseFlexibleBool(row.DesignTypeSphPlus, false, "DesignTypeSphPlus", errors);
			parsed.DesignTypeSphMinus = ParseFlexibleBool(row.DesignTypeSphMinus, false, "DesignTypeSphMinus", errors);
			parsed.DesignTypeAddition = ParseFlexibleBool(row.DesignTypeAddition, false, "DesignTypeAddition", errors);

			parsed.CoatingName = Normalize(row.CoatingName);
			if (!string.IsNullOrWhiteSpace(parsed.CoatingName))
			{
				parsed.CoatingOrder = RequirePositiveInt(row.CoatingOrder, "CoatingOrder", errors);
				parsed.CoatingStatus = ParseFlexibleBool(row.CoatingStatus, true, "CoatingStatus", errors);
				parsed.CoatingIsDefault = ParseFlexibleBool(row.CoatingIsDefault, false, "CoatingIsDefault", errors);
			}

			parsed.LensIndexName = RequireText(row.LensIndexName, "LensIndexName", errors);
			parsed.LensIndexOrder = RequirePositiveInt(row.LensIndexOrder, "LensIndexOrder", errors);
			parsed.LensIndexStatus = ParseFlexibleBool(row.LensIndexStatus, true, "LensIndexStatus", errors);
			parsed.LensIndexHasColoringType = ParseFlexibleBool(row.LensIndexHasColoringType, false, "LensIndexHasColoringType", errors);

			parsed.MaterialName = RequireText(row.MaterialName, "MaterialName", errors);
			parsed.MaterialOrder = RequirePositiveInt(row.MaterialOrder, "MaterialOrder", errors);
			parsed.MaterialStatus = ParseFlexibleBool(row.MaterialStatus, true, "MaterialStatus", errors);
			parsed.ProductBarcode = RequireText(row.ProductBarcode, "ProductBarcode", errors);
			if (!string.IsNullOrWhiteSpace(parsed.ProductBarcode))
			{
				parsed.ProductDefineObjectId = ResolveDefineObjectByBarcode(parsed.ProductBarcode, errors);
			}

			var additionValues = ParseAdditionValues(row, parsed.DesignTypeAddition, errors);
			parsed.AdditionValues = additionValues.Values;
			parsed.AdditionOrder = additionValues.BaseOrder;
			parsed.AdditionStatus = additionValues.Status;

			parsed.SphValues = ParseSphValues(row, errors);

			return errors;
		}

		private CombinedImportContext LoadCombinedImportContext()
		{
			return new CombinedImportContext
			{
				Brands = LoadCustomBrands(),
				LensTypes = LoadCustomLensTypes(),
				DesignTypes = LoadCustomDesignTypes(),
				Coatings = LoadCustomCoatings(),
				LensIndexes = LoadCustomLensIndexes(),
				Materials = LoadCustomMaterials(),
				Additions = LoadCustomAdditions(),
				Sphs = LoadCustomSphs()
			};
		}

		private List<TblLnsBrandDto> LoadCustomBrands()
		{
			return RunSync(tblLnsBrandService.GetAll()).Where(IsCustomBrand).ToList();
		}

		private List<TblLnsLensTypeDto> LoadCustomLensTypes()
		{
			return RunSync(tblLnsLensTypeService.GetAll()).Where(IsCustomLensType).ToList();
		}

		private List<TblLnsDesignTypeDto> LoadCustomDesignTypes()
		{
			return RunSync(tblLnsDesignTypeService.GetAll()).Where(IsCustomDesignType).ToList();
		}

		private List<TblLnsCustomLensTypeCoatingDto> LoadCustomCoatings()
		{
			return (RunSync(tblLnsCustomLensTypeCoatingService.GetAll()) ?? Enumerable.Empty<TblLnsCustomLensTypeCoatingDto>()).ToList();
		}

		private List<TblLnsCustomLensIndexDto> LoadCustomLensIndexes()
		{
			return (RunSync(tblLnsCustomLensIndexService.GetAll()) ?? Enumerable.Empty<TblLnsCustomLensIndexDto>()).ToList();
		}

		private List<TblLnsCustomLensTypeMaterialDto> LoadCustomMaterials()
		{
			return (RunSync(tblLnsCustomLensTypeMaterialService.GetAll()) ?? Enumerable.Empty<TblLnsCustomLensTypeMaterialDto>()).ToList();
		}

		private List<TblLnsCustomDesignTypeAdditionDto> LoadCustomAdditions()
		{
			return (RunSync(tblLnsCustomDesignTypeAdditionService.GetAll()) ?? Enumerable.Empty<TblLnsCustomDesignTypeAdditionDto>()).ToList();
		}

		private List<TblLnsCustomSphDto> LoadCustomSphs()
		{
			return (RunSync(tblLnsCustomSphService.GetAll()) ?? Enumerable.Empty<TblLnsCustomSphDto>()).ToList();
		}

		private TblLnsBrandDto EnsureBrand(ParsedCombinedLensRow row, CombinedImportContext context, CombinedImportSummary summary)
		{
			var existing = context.Brands.FirstOrDefault(x => SameText(x.Code, row.BrandCode))
				?? context.Brands.FirstOrDefault(x => SameText(x.Name, row.BrandName));

			var conflict = context.Brands.FirstOrDefault(x => x.OrderId == row.BrandOrder && (existing == null || x.BrandId != existing.BrandId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"BrandOrder {row.BrandOrder} is already used by {conflict.Name}.");
			}

			if (existing == null)
			{
				var dto = new TblLnsBrandDto
				{
					Name = row.BrandName,
					Code = row.BrandCode,
					Description = string.Empty,
					OrderId = row.BrandOrder,
					IsActive = row.BrandStatus,
					IsSpecial = CustomFlag,
					IsStock = false,
					IsStockGranty = false
				};
				RunSync(tblLnsBrandService.Add(dto));
				context.Brands = LoadCustomBrands();
				summary.BrandsCreated++;
				return context.Brands.First(x => SameText(x.Code, row.BrandCode));
			}

			var changed = !SameText(existing.Name, row.BrandName)
				|| !SameText(existing.Code, row.BrandCode)
				|| existing.OrderId != row.BrandOrder
				|| existing.IsActive != row.BrandStatus
				|| existing.IsSpecial != CustomFlag
				|| existing.IsStock
				|| existing.IsStockGranty;

			if (changed)
			{
				existing.Name = row.BrandName;
				existing.Code = row.BrandCode;
				existing.OrderId = row.BrandOrder;
				existing.IsActive = row.BrandStatus;
				existing.IsSpecial = CustomFlag;
				existing.IsStock = false;
				existing.IsStockGranty = false;
				RunSync(tblLnsBrandService.Update(existing));
				context.Brands = LoadCustomBrands();
				summary.BrandsUpdated++;
				return context.Brands.First(x => x.BrandId == existing.BrandId);
			}

			return existing;
		}

		private TblLnsLensTypeDto EnsureLensType(ParsedCombinedLensRow row, int brandId, CombinedImportContext context, CombinedImportSummary summary)
		{
			var siblings = context.LensTypes.Where(x => x.BrandId == brandId).ToList();
			var existing = siblings.FirstOrDefault(x => SameText(x.Code, row.LensTypeCode))
				?? siblings.FirstOrDefault(x => SameText(x.Name, row.LensTypeName));

			var conflict = siblings.FirstOrDefault(x => x.OrderId == row.LensTypeOrder && (existing == null || x.LensTypeId != existing.LensTypeId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"LensTypeOrder {row.LensTypeOrder} is already used by {conflict.Name}.");
			}

			if (existing == null)
			{
				var dto = new TblLnsLensTypeDto
				{
					Name = row.LensTypeName,
					Code = row.LensTypeCode,
					Description = string.Empty,
					OrderId = row.LensTypeOrder,
					IsActive = row.LensTypeStatus,
					IsCorridor = row.LensTypeIsCorridor,
					IsSpecial = CustomFlag,
					BrandId = brandId
				};
				RunSync(tblLnsLensTypeService.Add(dto));
				context.LensTypes = LoadCustomLensTypes();
				summary.LensTypesCreated++;
				return context.LensTypes.First(x => x.BrandId == brandId && SameText(x.Code, row.LensTypeCode));
			}

			var changed = !SameText(existing.Name, row.LensTypeName)
				|| !SameText(existing.Code, row.LensTypeCode)
				|| existing.OrderId != row.LensTypeOrder
				|| existing.IsActive != row.LensTypeStatus
				|| existing.IsCorridor != row.LensTypeIsCorridor
				|| existing.IsSpecial != CustomFlag
				|| existing.BrandId != brandId;

			if (changed)
			{
				existing.Name = row.LensTypeName;
				existing.Code = row.LensTypeCode;
				existing.OrderId = row.LensTypeOrder;
				existing.IsActive = row.LensTypeStatus;
				existing.IsCorridor = row.LensTypeIsCorridor;
				existing.IsSpecial = CustomFlag;
				existing.BrandId = brandId;
				RunSync(tblLnsLensTypeService.Update(existing));
				context.LensTypes = LoadCustomLensTypes();
				summary.LensTypesUpdated++;
				return context.LensTypes.First(x => x.LensTypeId == existing.LensTypeId);
			}

			return existing;
		}

		private TblLnsDesignTypeDto EnsureDesignType(ParsedCombinedLensRow row, int lensTypeId, CombinedImportContext context, CombinedImportSummary summary)
		{
			var siblings = context.DesignTypes.Where(x => x.LensTypeId == lensTypeId).ToList();
			var existing = siblings.FirstOrDefault(x => SameText(x.Code, row.DesignTypeCode))
				?? siblings.FirstOrDefault(x => SameText(x.Name, row.DesignTypeName));

			var conflict = siblings.FirstOrDefault(x => x.OrderId == row.DesignTypeOrder && (existing == null || x.DesignTypeId != existing.DesignTypeId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"DesignTypeOrder {row.DesignTypeOrder} is already used by {conflict.Name}.");
			}

			if (existing == null)
			{
				var dto = new TblLnsDesignTypeDto
				{
					Name = row.DesignTypeName,
					Code = row.DesignTypeCode,
					Description = string.Empty,
					OrderId = row.DesignTypeOrder,
					IsActive = row.DesignTypeStatus,
					IsSpecial = CustomFlag,
					LensTypeId = lensTypeId,
					SphPlus = row.DesignTypeSphPlus,
					SphMinus = row.DesignTypeSphMinus,
					Addition = row.DesignTypeAddition
				};
				RunSync(tblLnsDesignTypeService.Add(dto));
				context.DesignTypes = LoadCustomDesignTypes();
				summary.DesignTypesCreated++;
				return context.DesignTypes.First(x => x.LensTypeId == lensTypeId && SameText(x.Code, row.DesignTypeCode));
			}

			var changed = !SameText(existing.Name, row.DesignTypeName)
				|| !SameText(existing.Code, row.DesignTypeCode)
				|| existing.OrderId != row.DesignTypeOrder
				|| existing.IsActive != row.DesignTypeStatus
				|| existing.IsSpecial != CustomFlag
				|| existing.LensTypeId != lensTypeId
				|| existing.SphPlus != row.DesignTypeSphPlus
				|| existing.SphMinus != row.DesignTypeSphMinus
				|| existing.Addition != row.DesignTypeAddition;

			if (changed)
			{
				existing.Name = row.DesignTypeName;
				existing.Code = row.DesignTypeCode;
				existing.OrderId = row.DesignTypeOrder;
				existing.IsActive = row.DesignTypeStatus;
				existing.IsSpecial = CustomFlag;
				existing.LensTypeId = lensTypeId;
				existing.SphPlus = row.DesignTypeSphPlus;
				existing.SphMinus = row.DesignTypeSphMinus;
				existing.Addition = row.DesignTypeAddition;
				if (!existing.Addition)
				{
					existing.AdditionValue = null;
				}
				RunSync(tblLnsDesignTypeService.Update(existing));
				context.DesignTypes = LoadCustomDesignTypes();
				summary.DesignTypesUpdated++;
				return context.DesignTypes.First(x => x.DesignTypeId == existing.DesignTypeId);
			}

			return existing;
		}

		private void EnsureCoating(ParsedCombinedLensRow row, TblLnsDesignTypeDto designType, CombinedImportContext context, CombinedImportSummary summary)
		{
			var siblings = context.Coatings.Where(x => x.DesignTypeId == designType.DesignTypeId).ToList();
			var existing = siblings.FirstOrDefault(x => SameText(x.CoatingName, row.CoatingName));
			var conflict = siblings.FirstOrDefault(x => x.OrderId == row.CoatingOrder && (existing == null || x.CustomLensTypeCoatingId != existing.CustomLensTypeCoatingId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"CoatingOrder {row.CoatingOrder} is already used by {conflict.CoatingName}.");
			}

			if (row.CoatingIsDefault)
			{
				foreach (var item in siblings.Where(x => x.IsDefault && (existing == null || x.CustomLensTypeCoatingId != existing.CustomLensTypeCoatingId)))
				{
					item.IsDefault = false;
					RunSync(tblLnsCustomLensTypeCoatingService.Update(item));
					summary.CoatingsUpdated++;
				}
				context.Coatings = LoadCustomCoatings();
				siblings = context.Coatings.Where(x => x.DesignTypeId == designType.DesignTypeId).ToList();
				existing = siblings.FirstOrDefault(x => SameText(x.CoatingName, row.CoatingName));
			}

			if (existing == null)
			{
				var dto = new TblLnsCustomLensTypeCoatingDto
				{
					DesignTypeId = designType.DesignTypeId,
					LensTypeName = Normalize(designType.Name),
					CoatingName = row.CoatingName,
					IsDefault = row.CoatingIsDefault,
					OrderId = row.CoatingOrder,
					IsActive = row.CoatingIsDefault || row.CoatingStatus
				};
				RunSync(tblLnsCustomLensTypeCoatingService.Add(dto));
				context.Coatings = LoadCustomCoatings();
				summary.CoatingsCreated++;
				return;
			}

			var newActive = row.CoatingIsDefault || row.CoatingStatus;
			var changed = !SameText(existing.LensTypeName, designType.Name)
				|| !SameText(existing.CoatingName, row.CoatingName)
				|| existing.IsDefault != row.CoatingIsDefault
				|| existing.OrderId != row.CoatingOrder
				|| existing.IsActive != newActive;

			if (changed)
			{
				existing.LensTypeName = Normalize(designType.Name);
				existing.CoatingName = row.CoatingName;
				existing.IsDefault = row.CoatingIsDefault;
				existing.OrderId = row.CoatingOrder;
				existing.IsActive = newActive;
				RunSync(tblLnsCustomLensTypeCoatingService.Update(existing));
				context.Coatings = LoadCustomCoatings();
				summary.CoatingsUpdated++;
			}
		}

		private TblLnsCustomLensIndexDto EnsureLensIndex(ParsedCombinedLensRow row, int designTypeId, CombinedImportContext context, CombinedImportSummary summary)
		{
			var siblings = context.LensIndexes.Where(x => x.DesignTypeId == designTypeId).ToList();
			var existing = siblings.FirstOrDefault(x => SameText(x.LensIndexName, row.LensIndexName));
			var conflict = siblings.FirstOrDefault(x => x.OrderId == row.LensIndexOrder && (existing == null || x.CustomLensIndexId != existing.CustomLensIndexId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"LensIndexOrder {row.LensIndexOrder} is already used by {conflict.LensIndexName}.");
			}

			if (existing == null)
			{
				var dto = new TblLnsCustomLensIndexDto
				{
					DesignTypeId = designTypeId,
					LensIndexName = row.LensIndexName,
					OrderId = row.LensIndexOrder,
					IsActive = row.LensIndexStatus,
					HasColoringType = row.LensIndexHasColoringType
				};
				RunSync(tblLnsCustomLensIndexService.Add(dto));
				context.LensIndexes = LoadCustomLensIndexes();
				summary.LensIndexesCreated++;
				return context.LensIndexes.First(x => x.DesignTypeId == designTypeId && SameText(x.LensIndexName, row.LensIndexName));
			}

			var changed = existing.OrderId != row.LensIndexOrder
				|| existing.IsActive != row.LensIndexStatus
				|| existing.HasColoringType != row.LensIndexHasColoringType;
			if (changed)
			{
				existing.OrderId = row.LensIndexOrder;
				existing.IsActive = row.LensIndexStatus;
				existing.HasColoringType = row.LensIndexHasColoringType;
				RunSync(tblLnsCustomLensIndexService.Update(existing));
				context.LensIndexes = LoadCustomLensIndexes();
				summary.LensIndexesUpdated++;
				return context.LensIndexes.First(x => x.CustomLensIndexId == existing.CustomLensIndexId);
			}

			return existing;
		}

		private void EnsureMaterial(ParsedCombinedLensRow row, int designTypeId, int lensIndexId, CombinedImportContext context, CombinedImportSummary summary)
		{
			var siblings = context.Materials.Where(x => x.LensIndexId == lensIndexId).ToList();
			var existing = siblings.FirstOrDefault(x => SameText(x.MaterialName, row.MaterialName));
			var conflict = siblings.FirstOrDefault(x => x.OrderId == row.MaterialOrder && (existing == null || x.CustomLensTypeMaterialId != existing.CustomLensTypeMaterialId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"MaterialOrder {row.MaterialOrder} is already used by {conflict.MaterialName}.");
			}

			if (existing == null)
			{
				var dto = new TblLnsCustomLensTypeMaterialDto
				{
					DesignTypeId = designTypeId,
					LensIndexId = lensIndexId,
					LensTypeName = string.Empty,
					MaterialName = row.MaterialName,
					DefineObjectId = row.ProductDefineObjectId,
					OrderId = row.MaterialOrder,
					IsActive = row.MaterialStatus
				};
				RunSync(tblLnsCustomLensTypeMaterialService.Add(dto));
				context.Materials = LoadCustomMaterials();
				summary.MaterialsCreated++;
				return;
			}

			var changed = existing.DesignTypeId != designTypeId
				|| existing.LensIndexId != lensIndexId
				|| !SameText(existing.MaterialName, row.MaterialName)
				|| existing.DefineObjectId != row.ProductDefineObjectId
				|| existing.OrderId != row.MaterialOrder
				|| existing.IsActive != row.MaterialStatus;

			if (changed)
			{
				existing.DesignTypeId = designTypeId;
				existing.LensIndexId = lensIndexId;
				existing.MaterialName = row.MaterialName;
				existing.DefineObjectId = row.ProductDefineObjectId;
				existing.OrderId = row.MaterialOrder;
				existing.IsActive = row.MaterialStatus;
				RunSync(tblLnsCustomLensTypeMaterialService.Update(existing));
				context.Materials = LoadCustomMaterials();
				summary.MaterialsUpdated++;
			}
		}

		private void EnsureAddition(ParsedCombinedLensRow row, int designTypeId, decimal value, CombinedImportContext context, CombinedImportSummary summary)
		{
			var orderId = row.AdditionOrder + row.AdditionValues.IndexOf(value);
			var siblings = context.Additions.Where(x => x.DesignTypeId == designTypeId).ToList();
			var existing = siblings.FirstOrDefault(x => x.AdditionValue.HasValue && x.AdditionValue.Value == value);
			var conflict = siblings.FirstOrDefault(x => x.OrderId == orderId && (existing == null || x.CustomDesignTypeAdditionId != existing.CustomDesignTypeAdditionId));
			if (conflict != null)
			{
				throw new InvalidOperationException($"AdditionOrder {orderId} is already used by {conflict.AdditionValue}.");
			}

			if (existing == null)
			{
				var dto = new TblLnsCustomDesignTypeAdditionDto
				{
					DesignTypeId = designTypeId,
					DefineObjectId = null,
					AdditionValue = value,
					OrderId = orderId,
					IsActive = row.AdditionStatus
				};
				RunSync(tblLnsCustomDesignTypeAdditionService.Add(dto));
				context.Additions = LoadCustomAdditions();
				summary.AdditionsCreated++;
				return;
			}

			if (existing.OrderId != orderId || existing.IsActive != row.AdditionStatus || existing.DefineObjectId != null)
			{
				existing.OrderId = orderId;
				existing.IsActive = row.AdditionStatus;
				existing.DefineObjectId = null;
				RunSync(tblLnsCustomDesignTypeAdditionService.Update(existing));
				context.Additions = LoadCustomAdditions();
				summary.AdditionsUpdated++;
			}
		}

		private void EnsureSph(decimal signedValue, CombinedImportContext context, CombinedImportSummary summary)
		{
			var value = decimal.Abs(signedValue);
			var name = FormatLensNumber(value);
			var existing = context.Sphs.FirstOrDefault(x => TryParseDecimal(x.Name, out var current) && decimal.Abs(current) == value)
				?? context.Sphs.FirstOrDefault(x => SameText(x.Code, name));

			if (existing == null)
			{
				var dto = new TblLnsCustomSphDto
				{
					Name = name,
					Code = name,
					Description = string.Empty,
					OrderId = (int)(value * 100) + 1,
					IsActive = true
				};
				RunSync(tblLnsCustomSphService.Add(dto));
				context.Sphs = LoadCustomSphs();
				summary.SphsCreated++;
				return;
			}

			var targetOrder = (int)(value * 100) + 1;
			if (!SameText(existing.Name, name) || !SameText(existing.Code, name) || existing.OrderId != targetOrder || !existing.IsActive)
			{
				existing.Name = name;
				existing.Code = name;
				existing.OrderId = targetOrder;
				existing.IsActive = true;
				RunSync(tblLnsCustomSphService.Update(existing));
				context.Sphs = LoadCustomSphs();
				summary.SphsUpdated++;
			}
		}

		private int ResolveDefineObjectByBarcode(string barcode, List<string> errors)
		{
			var candidates = RunSync(tblClrDefineObjectService.Search(barcode)) ?? new List<TblClrDefineObjectDto>();
			var exact = candidates
				.Where(x => SameText(x.TechnicalSpecs, barcode))
				.ToList();

			if (exact.Count == 1)
			{
				return exact[0].DefineObjectId;
			}

			if (exact.Count > 1)
			{
				errors.Add($"ProductBarcode is duplicated in warehouse: {barcode}");
				return 0;
			}

			errors.Add($"ProductBarcode was not found in warehouse: {barcode}");
			return 0;
		}

		private AdditionParseResult ParseAdditionValues(CombinedLensImportRow row, bool designTypeAddition, List<string> errors)
		{
			var result = new AdditionParseResult { Status = true };
			var hasRange = !string.IsNullOrWhiteSpace(row.AdditionMin)
				|| !string.IsNullOrWhiteSpace(row.AdditionMax)
				|| !string.IsNullOrWhiteSpace(row.AdditionStep);
			var hasSingle = !string.IsNullOrWhiteSpace(row.AdditionValue);

			if (!hasRange && !hasSingle)
			{
				return result;
			}

			if (!designTypeAddition)
			{
				errors.Add("Addition values require DesignTypeAddition = 1.");
				return result;
			}

			result.BaseOrder = RequirePositiveInt(row.AdditionOrder, "AdditionOrder", errors);
			result.Status = ParseFlexibleBool(row.AdditionStatus, true, "AdditionStatus", errors);

			if (hasRange)
			{
				var min = RequireDecimal(row.AdditionMin, "AdditionMin", errors);
				var max = RequireDecimal(row.AdditionMax, "AdditionMax", errors);
				var step = RequireDecimal(row.AdditionStep, "AdditionStep", errors);
				if (errors.Count == 0 || (min.HasValue && max.HasValue && step.HasValue))
				{
					if (min <= 0 || max <= 0)
					{
						errors.Add("AdditionMin and AdditionMax must be greater than zero.");
					}
					else
					{
						result.Values = BuildDecimalRange(min, max, step, "Addition", errors);
					}
				}
				return result;
			}

			var value = RequireDecimal(row.AdditionValue, "AdditionValue", errors);
			if (value.HasValue)
			{
				if (value.Value <= 0)
				{
					errors.Add("AdditionValue must be greater than zero.");
				}
				else
				{
					result.Values.Add(value.Value);
				}
			}

			return result;
		}

		private List<decimal> ParseSphValues(CombinedLensImportRow row, List<string> errors)
		{
			var hasSph = !string.IsNullOrWhiteSpace(row.SphMin)
				|| !string.IsNullOrWhiteSpace(row.SphMax)
				|| !string.IsNullOrWhiteSpace(row.SphStep);
			if (!hasSph)
			{
				return new List<decimal>();
			}

			var min = RequireDecimal(row.SphMin, "SphMin", errors);
			var max = RequireDecimal(row.SphMax, "SphMax", errors);
			var step = RequireDecimal(row.SphStep, "SphStep", errors);
			var range = BuildDecimalRange(min, max, step, "SPH", errors);
			return range.Select(decimal.Abs).Distinct().OrderBy(x => x).ToList();
		}

		private static List<decimal> BuildDecimalRange(decimal? min, decimal? max, decimal? step, string fieldName, List<string> errors)
		{
			var values = new List<decimal>();
			if (!min.HasValue || !max.HasValue || !step.HasValue)
			{
				return values;
			}
			if (step.Value <= 0)
			{
				errors.Add($"{fieldName} step must be greater than zero.");
				return values;
			}
			if (min.Value > max.Value)
			{
				errors.Add($"{fieldName} min must be less than or equal to max.");
				return values;
			}

			var current = min.Value;
			var guard = 0;
			while (current <= max.Value)
			{
				values.Add(decimal.Round(current, 2, MidpointRounding.AwayFromZero));
				current += step.Value;
				guard++;
				if (guard > 2000)
				{
					errors.Add($"{fieldName} range is too large.");
					break;
				}
			}
			return values.Distinct().ToList();
		}

		private static string RequireText(string? value, string column, List<string> errors)
		{
			var normalized = Normalize(value);
			if (string.IsNullOrWhiteSpace(normalized))
			{
				errors.Add($"{column} is required.");
			}
			return normalized;
		}

		private static int RequirePositiveInt(string? value, string column, List<string> errors)
		{
			var parsed = ReadInt(value);
			if (!parsed.HasValue || parsed.Value <= 0)
			{
				errors.Add($"{column} must be a positive number.");
				return 0;
			}
			return parsed.Value;
		}

		private static decimal? RequireDecimal(string? value, string column, List<string> errors)
		{
			if (!TryParseDecimal(value, out var parsed))
			{
				errors.Add($"{column} must be a valid number.");
				return null;
			}
			return parsed;
		}

		private static int? ReadInt(string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}
			var normalized = NormalizeNumber(value);
			if (int.TryParse(normalized, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
			{
				return parsed;
			}
			if (decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out var parsedDecimal))
			{
				return Convert.ToInt32(parsedDecimal);
			}
			return null;
		}

		private static bool ParseFlexibleBool(string? value, bool defaultValue, string column, List<string> errors)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return defaultValue;
			}

			var normalized = Normalize(value).ToLowerInvariant();
			if (normalized is "1" or "true" or "yes" or "y" or "active" or "دار" or "دارد" or "فعال" or "بله")
			{
				return true;
			}
			if (normalized is "0" or "false" or "no" or "n" or "inactive" or "ندار" or "ندارد" or "غیرفعال" or "غيرفعال" or "خیر" or "خير")
			{
				return false;
			}

			errors.Add($"{column} has invalid boolean/status value.");
			return defaultValue;
		}

		private static bool TryParseDecimal(string? value, out decimal parsed)
		{
			parsed = 0;
			var normalized = NormalizeNumber(value);
			if (string.IsNullOrWhiteSpace(normalized))
			{
				return false;
			}

			return decimal.TryParse(normalized, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out parsed);
		}

		private static string NormalizeNumber(string? value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}

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

		private static string FormatLensNumber(decimal value)
		{
			return value.ToString("0.00", CultureInfo.InvariantCulture);
		}

		private static bool SameText(string? left, string? right)
		{
			return string.Equals(Normalize(left), Normalize(right), StringComparison.OrdinalIgnoreCase);
		}

		private static bool IsCustomLensType(TblLnsLensTypeDto item)
		{
			return item != null && item.IsSpecial == CustomFlag;
		}

		private static bool IsCustomDesignType(TblLnsDesignTypeDto item)
		{
			return item != null && item.IsSpecial == CustomFlag;
		}

		private static T RunSync<T>(Task<T> task)
		{
			return task.GetAwaiter().GetResult();
		}

		private static void RunSync(Task task)
		{
			task.GetAwaiter().GetResult();
		}

		private static bool IsEmptyRow(IExcelDataReader reader)
		{
			for (var i = 0; i < reader.FieldCount; i++)
			{
				if (!string.IsNullOrWhiteSpace(ReadCellString(reader, i)))
				{
					return false;
				}
			}
			return true;
		}

		private sealed class CombinedLensImportColumns
		{
			public int BrandName { get; private set; }
			public int BrandCode { get; private set; }
			public int BrandOrder { get; private set; }
			public int BrandStatus { get; private set; }
			public int LensTypeName { get; private set; }
			public int LensTypeCode { get; private set; }
			public int LensTypeOrder { get; private set; }
			public int LensTypeStatus { get; private set; }
			public int LensTypeIsCorridor { get; private set; }
			public int DesignTypeName { get; private set; }
			public int DesignTypeCode { get; private set; }
			public int DesignTypeOrder { get; private set; }
			public int DesignTypeStatus { get; private set; }
			public int DesignTypeSphPlus { get; private set; }
			public int DesignTypeSphMinus { get; private set; }
			public int DesignTypeAddition { get; private set; }
			public int CoatingName { get; private set; }
			public int CoatingOrder { get; private set; }
			public int CoatingStatus { get; private set; }
			public int CoatingIsDefault { get; private set; }
			public int LensIndexName { get; private set; }
			public int LensIndexOrder { get; private set; }
			public int LensIndexStatus { get; private set; }
			public int LensIndexHasColoringType { get; private set; }
			public int MaterialName { get; private set; }
			public int MaterialOrder { get; private set; }
			public int MaterialStatus { get; private set; }
			public int ProductBarcode { get; private set; }
			public int AdditionValue { get; private set; }
			public int AdditionOrder { get; private set; }
			public int AdditionStatus { get; private set; }
			public int SphMin { get; private set; }
			public int SphMax { get; private set; }
			public int SphStep { get; private set; }
			public int AdditionMin { get; private set; } = -1;
			public int AdditionMax { get; private set; } = -1;
			public int AdditionStep { get; private set; } = -1;

			public static CombinedLensImportColumns FromHeader(IExcelDataReader reader, List<string> errors)
			{
				var header = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
				for (var i = 0; i < reader.FieldCount; i++)
				{
					var key = NormalizeHeader(ReadCellString(reader, i));
					if (!string.IsNullOrWhiteSpace(key) && !header.ContainsKey(key))
					{
						header[key] = i;
					}
				}

				var columns = new CombinedLensImportColumns
				{
					BrandName = Required(header, "BrandName", errors),
					BrandCode = Required(header, "BrandCode", errors),
					BrandOrder = Required(header, "BrandOrder", errors),
					BrandStatus = Required(header, "BrandStatus", errors),
					LensTypeName = Required(header, "LensTypeName", errors),
					LensTypeCode = Required(header, "LensTypeCode", errors),
					LensTypeOrder = Required(header, "LensTypeOrder", errors),
					LensTypeStatus = Required(header, "LensTypeStatus", errors),
					LensTypeIsCorridor = Required(header, "LensTypeIsCorridor", errors),
					DesignTypeName = Required(header, "DesignTypeName", errors),
					DesignTypeCode = Required(header, "DesignTypeCode", errors),
					DesignTypeOrder = Required(header, "DesignTypeOrder", errors),
					DesignTypeStatus = Required(header, "DesignTypeStatus", errors),
					DesignTypeSphPlus = Required(header, "DesignTypeSphPlus", errors),
					DesignTypeSphMinus = Required(header, "DesignTypeSphMinus", errors),
					DesignTypeAddition = Required(header, "DesignTypeAddition", errors),
					CoatingName = Required(header, "CoatingName", errors),
					CoatingOrder = Required(header, "CoatingOrder", errors),
					CoatingStatus = Required(header, "CoatingStatus", errors),
					CoatingIsDefault = Required(header, "CoatingIsDefault", errors),
					LensIndexName = Required(header, "LensIndexName", errors),
					LensIndexOrder = Required(header, "LensIndexOrder", errors),
					LensIndexStatus = Required(header, "LensIndexStatus", errors),
					LensIndexHasColoringType = Required(header, "LensIndexHasColoringType", errors),
					MaterialName = Required(header, "MaterialName", errors),
					MaterialOrder = Required(header, "MaterialOrder", errors),
					MaterialStatus = Required(header, "MaterialStatus", errors),
					ProductBarcode = Required(header, "ProductBarcode", errors),
					AdditionValue = Required(header, "AdditionValue", errors),
					AdditionOrder = Required(header, "AdditionOrder", errors),
					AdditionStatus = Required(header, "AdditionStatus", errors),
					SphMin = Required(header, "SphMin", errors),
					SphMax = Required(header, "SphMax", errors),
					SphStep = Required(header, "SphStep", errors),
					AdditionMin = Optional(header, "AdditionMin"),
					AdditionMax = Optional(header, "AdditionMax"),
					AdditionStep = Optional(header, "AdditionStep")
				};

				if (!header.ContainsKey(NormalizeHeader("BrandName")))
				{
					errors.Add("Combined import header was not detected. The first row must contain BrandName, BrandCode, ... columns.");
				}

				return columns;
			}

			private static int Required(Dictionary<string, int> header, string name, List<string> errors)
			{
				var key = NormalizeHeader(name);
				if (header.TryGetValue(key, out var index))
				{
					return index;
				}

				errors.Add($"Missing column: {name}");
				return -1;
			}

			private static int Optional(Dictionary<string, int> header, string name)
			{
				return header.TryGetValue(NormalizeHeader(name), out var index) ? index : -1;
			}

			private static string NormalizeHeader(string? value)
			{
				return Normalize(value).Replace(" ", string.Empty).Replace("_", string.Empty).Replace("-", string.Empty);
			}
		}

		private sealed class CombinedLensImportRow
		{
			public int RowNumber { get; private set; }
			public string BrandName { get; private set; } = string.Empty;
			public string BrandCode { get; private set; } = string.Empty;
			public string BrandOrder { get; private set; } = string.Empty;
			public string BrandStatus { get; private set; } = string.Empty;
			public string LensTypeName { get; private set; } = string.Empty;
			public string LensTypeCode { get; private set; } = string.Empty;
			public string LensTypeOrder { get; private set; } = string.Empty;
			public string LensTypeStatus { get; private set; } = string.Empty;
			public string LensTypeIsCorridor { get; private set; } = string.Empty;
			public string DesignTypeName { get; private set; } = string.Empty;
			public string DesignTypeCode { get; private set; } = string.Empty;
			public string DesignTypeOrder { get; private set; } = string.Empty;
			public string DesignTypeStatus { get; private set; } = string.Empty;
			public string DesignTypeSphPlus { get; private set; } = string.Empty;
			public string DesignTypeSphMinus { get; private set; } = string.Empty;
			public string DesignTypeAddition { get; private set; } = string.Empty;
			public string CoatingName { get; private set; } = string.Empty;
			public string CoatingOrder { get; private set; } = string.Empty;
			public string CoatingStatus { get; private set; } = string.Empty;
			public string CoatingIsDefault { get; private set; } = string.Empty;
			public string LensIndexName { get; private set; } = string.Empty;
			public string LensIndexOrder { get; private set; } = string.Empty;
			public string LensIndexStatus { get; private set; } = string.Empty;
			public string LensIndexHasColoringType { get; private set; } = string.Empty;
			public string MaterialName { get; private set; } = string.Empty;
			public string MaterialOrder { get; private set; } = string.Empty;
			public string MaterialStatus { get; private set; } = string.Empty;
			public string ProductBarcode { get; private set; } = string.Empty;
			public string AdditionValue { get; private set; } = string.Empty;
			public string AdditionOrder { get; private set; } = string.Empty;
			public string AdditionStatus { get; private set; } = string.Empty;
			public string SphMin { get; private set; } = string.Empty;
			public string SphMax { get; private set; } = string.Empty;
			public string SphStep { get; private set; } = string.Empty;
			public string AdditionMin { get; private set; } = string.Empty;
			public string AdditionMax { get; private set; } = string.Empty;
			public string AdditionStep { get; private set; } = string.Empty;

			public static CombinedLensImportRow FromReader(IExcelDataReader reader, CombinedLensImportColumns columns, int rowNumber)
			{
				return new CombinedLensImportRow
				{
					RowNumber = rowNumber,
					BrandName = ReadCellString(reader, columns.BrandName),
					BrandCode = ReadCellString(reader, columns.BrandCode),
					BrandOrder = ReadCellString(reader, columns.BrandOrder),
					BrandStatus = ReadCellString(reader, columns.BrandStatus),
					LensTypeName = ReadCellString(reader, columns.LensTypeName),
					LensTypeCode = ReadCellString(reader, columns.LensTypeCode),
					LensTypeOrder = ReadCellString(reader, columns.LensTypeOrder),
					LensTypeStatus = ReadCellString(reader, columns.LensTypeStatus),
					LensTypeIsCorridor = ReadCellString(reader, columns.LensTypeIsCorridor),
					DesignTypeName = ReadCellString(reader, columns.DesignTypeName),
					DesignTypeCode = ReadCellString(reader, columns.DesignTypeCode),
					DesignTypeOrder = ReadCellString(reader, columns.DesignTypeOrder),
					DesignTypeStatus = ReadCellString(reader, columns.DesignTypeStatus),
					DesignTypeSphPlus = ReadCellString(reader, columns.DesignTypeSphPlus),
					DesignTypeSphMinus = ReadCellString(reader, columns.DesignTypeSphMinus),
					DesignTypeAddition = ReadCellString(reader, columns.DesignTypeAddition),
					CoatingName = ReadCellString(reader, columns.CoatingName),
					CoatingOrder = ReadCellString(reader, columns.CoatingOrder),
					CoatingStatus = ReadCellString(reader, columns.CoatingStatus),
					CoatingIsDefault = ReadCellString(reader, columns.CoatingIsDefault),
					LensIndexName = ReadCellString(reader, columns.LensIndexName),
					LensIndexOrder = ReadCellString(reader, columns.LensIndexOrder),
					LensIndexStatus = ReadCellString(reader, columns.LensIndexStatus),
					LensIndexHasColoringType = ReadCellString(reader, columns.LensIndexHasColoringType),
					MaterialName = ReadCellString(reader, columns.MaterialName),
					MaterialOrder = ReadCellString(reader, columns.MaterialOrder),
					MaterialStatus = ReadCellString(reader, columns.MaterialStatus),
					ProductBarcode = ReadCellString(reader, columns.ProductBarcode),
					AdditionValue = ReadCellString(reader, columns.AdditionValue),
					AdditionOrder = ReadCellString(reader, columns.AdditionOrder),
					AdditionStatus = ReadCellString(reader, columns.AdditionStatus),
					SphMin = ReadCellString(reader, columns.SphMin),
					SphMax = ReadCellString(reader, columns.SphMax),
					SphStep = ReadCellString(reader, columns.SphStep),
					AdditionMin = ReadCellString(reader, columns.AdditionMin),
					AdditionMax = ReadCellString(reader, columns.AdditionMax),
					AdditionStep = ReadCellString(reader, columns.AdditionStep)
				};
			}
		}

		private sealed class ParsedCombinedLensRow
		{
			public int RowNumber { get; set; }
			public string BrandName { get; set; } = string.Empty;
			public string BrandCode { get; set; } = string.Empty;
			public int BrandOrder { get; set; }
			public bool BrandStatus { get; set; }
			public string LensTypeName { get; set; } = string.Empty;
			public string LensTypeCode { get; set; } = string.Empty;
			public int LensTypeOrder { get; set; }
			public bool LensTypeStatus { get; set; }
			public bool LensTypeIsCorridor { get; set; }
			public string DesignTypeName { get; set; } = string.Empty;
			public string DesignTypeCode { get; set; } = string.Empty;
			public int DesignTypeOrder { get; set; }
			public bool DesignTypeStatus { get; set; }
			public bool DesignTypeSphPlus { get; set; }
			public bool DesignTypeSphMinus { get; set; }
			public bool DesignTypeAddition { get; set; }
			public string CoatingName { get; set; } = string.Empty;
			public int CoatingOrder { get; set; }
			public bool CoatingStatus { get; set; }
			public bool CoatingIsDefault { get; set; }
			public string LensIndexName { get; set; } = string.Empty;
			public int LensIndexOrder { get; set; }
			public bool LensIndexStatus { get; set; }
			public bool LensIndexHasColoringType { get; set; }
			public string MaterialName { get; set; } = string.Empty;
			public int MaterialOrder { get; set; }
			public bool MaterialStatus { get; set; }
			public string ProductBarcode { get; set; } = string.Empty;
			public int ProductDefineObjectId { get; set; }
			public List<decimal> AdditionValues { get; set; } = new();
			public int AdditionOrder { get; set; }
			public bool AdditionStatus { get; set; }
			public List<decimal> SphValues { get; set; } = new();
		}

		private sealed class AdditionParseResult
		{
			public List<decimal> Values { get; set; } = new();
			public int BaseOrder { get; set; }
			public bool Status { get; set; } = true;
		}

		private sealed class CombinedImportContext
		{
			public List<TblLnsBrandDto> Brands { get; set; } = new();
			public List<TblLnsLensTypeDto> LensTypes { get; set; } = new();
			public List<TblLnsDesignTypeDto> DesignTypes { get; set; } = new();
			public List<TblLnsCustomLensTypeCoatingDto> Coatings { get; set; } = new();
			public List<TblLnsCustomLensIndexDto> LensIndexes { get; set; } = new();
			public List<TblLnsCustomLensTypeMaterialDto> Materials { get; set; } = new();
			public List<TblLnsCustomDesignTypeAdditionDto> Additions { get; set; } = new();
			public List<TblLnsCustomSphDto> Sphs { get; set; } = new();
		}

		private sealed class CombinedImportSummary
		{
			public int ProcessedRows { get; set; }
			public int BrandsCreated { get; set; }
			public int BrandsUpdated { get; set; }
			public int LensTypesCreated { get; set; }
			public int LensTypesUpdated { get; set; }
			public int DesignTypesCreated { get; set; }
			public int DesignTypesUpdated { get; set; }
			public int CoatingsCreated { get; set; }
			public int CoatingsUpdated { get; set; }
			public int LensIndexesCreated { get; set; }
			public int LensIndexesUpdated { get; set; }
			public int MaterialsCreated { get; set; }
			public int MaterialsUpdated { get; set; }
			public int AdditionsCreated { get; set; }
			public int AdditionsUpdated { get; set; }
			public int SphsCreated { get; set; }
			public int SphsUpdated { get; set; }
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
