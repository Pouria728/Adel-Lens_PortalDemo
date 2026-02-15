using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR.Protocol;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using ExcelDataReader;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using System;
using System.Linq.Expressions;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class CylesController(ITblLnsCylService tblLnsCylService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Cyl_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,Cyl_Create")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "admin,Cyl_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsCylDto tblLnsCylDto)
		{
			try
			{
 				var item = tblLnsCylService.Add(tblLnsCylDto);
				return Json(new
				{
					success = true,
					message = ""
				});
			}
			catch (InvalidOperationException ex)
			{
				return Json(new
				{
					success = false,
					message = ex.Message
				});
			}
			catch
			{
				return Json(new
				{
					success = false,
					message = ""
				});
			}
		}
		[Authorize(Roles = "admin,Cyl_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsCylService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Cyl_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsCylDto tblLnsCylDto)
		{
			try
			{
				var item = tblLnsCylService.Update(tblLnsCylDto);
				return Json(new
				{
					success = true,
					message = ""
				});
			}
			catch (InvalidOperationException ex)
			{
				return Json(new
				{
					success = false,
					message = ex.Message
				});
			}
			catch
			{
				return Json(new
				{
					success = false,
					message = ""
				});
			}
		}
        [Authorize(Roles = "admin,Cyl_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblLnsCylService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
		[Authorize(Roles = "admin,Cyl_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsCylService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
			totalRecords = item.Result.Item2;
			if (totalRecords <= 0)
			{
				return Json(new { });
			}

			int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
			var jsonData = new
			{
				total = totalPages,
				page = itemGrid.Page,
				records = totalRecords,
				rows = from p in item.Result.Item1
					   select new
					   {
						   id=p.CylId,
						   name = p.Name,
						   code = p.Code,
						   description = p.Description,
						   orderId = p.OrderId,
						   isActive = p.IsActive == true ? "فعال" : "غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

		[Authorize(Roles = "admin,Cyl_Index")]
		[HttpGet]
		public IActionResult ExportExcel()
		{
			var items = tblLnsCylService.GetAll().Result
				.OrderBy(x => x.OrderId)
				.ThenBy(x => x.CylId)
				.ToList();

			using var workbook = new XLWorkbook();
			var ws = workbook.AddWorksheet("Cyl");

			ws.Cell(1, 1).Value = "نام";
			ws.Cell(1, 2).Value = "کد";
			ws.Cell(1, 3).Value = "توضیحات";
			ws.Cell(1, 4).Value = "ترتیب نمایش";
			ws.Cell(1, 5).Value = "وضعیت";

			var row = 2;
			foreach (var item in items)
			{
				ws.Cell(row, 1).Value = item.Name ?? string.Empty;
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
			return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Cyl.xlsx");
		}

		[Authorize(Roles = "admin,Cyl_Create")]
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
					if (!orderIdRows.TryGetValue(orderId.Value, out var list))
					{
						list = new List<int>();
						orderIdRows[orderId.Value] = list;
					}
					list.Add(rowIndex);
				}

				if (rows.Count == 0 && errors.Count == 0)
				{
					errors.Add("هیچ ردیف معتبری در فایل پیدا نشد.");
				}

				var duplicateInFile = orderIdRows.Where(x => x.Value.Count > 1).ToList();
				if (duplicateInFile.Count > 0)
				{
					foreach (var dup in duplicateInFile)
					{
						var rowList = string.Join("، ", dup.Value);
						errors.Add($"ترتیب نمایش تکراری در فایل: {dup.Key} (ردیف‌های {rowList})");
					}
				}

				var existingOrderIds = tblLnsCylService.GetAll().Result.Select(x => x.OrderId).ToHashSet();
				foreach (var row in rows)
				{
					if (existingOrderIds.Contains(row.OrderId))
					{
						errors.Add($"ردیف {row.Row}: ترتیب نمایش {row.OrderId} قبلاً در سیستم ثبت شده است.");
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
					var dto = new TblLnsCylDto
					{
						Name = row.Name,
						Code = row.Code,
						Description = row.Description,
						OrderId = row.OrderId,
						IsActive = row.IsActive
					};
					tblLnsCylService.Add(dto);
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

		private static bool ReadCellBool(IExcelDataReader reader, int index, bool defaultValue)
		{
			if (index < 0 || index >= reader.FieldCount)
				return defaultValue;
			var value = reader.GetValue(index);
			if (value == null)
				return defaultValue;
			if (value is bool b)
				return b;
			if (value is double d)
				return d != 0;

			var text = value.ToString()?.Trim().ToLowerInvariant();
			if (string.IsNullOrWhiteSpace(text))
				return defaultValue;
			if (text == "1" || text == "true" || text == "فعال" || text == "active")
				return true;
			if (text == "0" || text == "false" || text == "غیرفعال" || text == "inactive")
				return false;
			return defaultValue;
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
	}
}
