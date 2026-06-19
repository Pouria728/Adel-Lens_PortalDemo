using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Presntation.Models;
using HamrahanSystem.Presntation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq.Expressions;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class ProcessController(
		ITblWfwProcessService tblWfwProcessService,
		ITblWfwProcessStepService tblWfwProcessStepService,
		ICacheService cacheService,
		ICustomLensPrintSettingsService customLensPrintSettingsService) : Controller
	{
		[Authorize(Roles = "admin,Process_Index")]
		public IActionResult Index()
		{
			
			return View();
		}

		[Authorize(Roles = "admin,Process_Index")]
		public async Task<IActionResult> PrintSettings(int? processId, int? processStepId, string? returnUrl)
		{
			processId = processId.HasValue && processId.Value > 0 ? processId.Value : null;
			processStepId = processStepId.HasValue && processStepId.Value > 0 ? processStepId.Value : null;
			returnUrl ??= string.Empty;

			if (processStepId.HasValue)
			{
				var step = await tblWfwProcessStepService.GetById(processStepId.Value);
				if (step == null)
				{
					TempData["PrintSettingsError"] = "گام انتخاب‌شده یافت نشد.";
					return RedirectToAction(nameof(Index));
				}

				processId ??= step.ProcessId;
			}

			if (processId.HasValue)
			{
				var process = await tblWfwProcessService.GetById(processId.Value);
				if (process == null)
				{
					TempData["PrintSettingsError"] = "فرآیند انتخاب‌شده یافت نشد.";
					return RedirectToAction(nameof(Index));
				}

				if (!IsCustomLensProcess(process))
				{
					TempData["PrintSettingsError"] = "تنظیمات چاپ خودکار فقط برای فرآیند عدسی سفارشی فعال است.";
					return RedirectToAction(nameof(Index));
				}

				ViewBag.PrintSettingsProcessName = process.Name ?? string.Empty;
			}

			var scoped = await customLensPrintSettingsService.ResolveAsync(processId, processStepId);
			var model = new CustomLensPrintSettingsViewModel
			{
				ProcessId = processId,
				ProcessStepId = processStepId,
				ReturnUrl = returnUrl,
				IsEnabled = scoped.IsEnabled,
				ReportUrlTemplate = scoped.ReportUrlTemplate,
				PrinterNamesText = string.Join(Environment.NewLine, scoped.Printers)
			};

			if (processStepId.HasValue)
			{
				ViewBag.PrintSettingsScopeTitle = "تنظیمات چاپ گام فرآیند";
			}
			else if (processId.HasValue)
			{
				ViewBag.PrintSettingsScopeTitle = "تنظیمات پیش‌فرض چاپ فرآیند";
			}
			else
			{
				ViewBag.PrintSettingsScopeTitle = "تنظیمات چاپ خودکار عدسی سفارشی";
			}

			return View(model);
		}

		[Authorize(Roles = "admin,Process_Index")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> PrintSettings(CustomLensPrintSettingsViewModel model)
		{
			model ??= new CustomLensPrintSettingsViewModel();

			var printerNames = (model.PrinterNamesText ?? string.Empty)
				.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(x => x.Trim())
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();

			var scopedSettings = new CustomLensPrintScopedSettings
			{
				IsEnabled = model.IsEnabled,
				ReportUrlTemplate = (model.ReportUrlTemplate ?? string.Empty).Trim(),
				Printers = printerNames
			};

			var processId = model.ProcessId.HasValue && model.ProcessId.Value > 0 ? model.ProcessId.Value : 0;
			var processStepId = model.ProcessStepId.HasValue && model.ProcessStepId.Value > 0 ? model.ProcessStepId.Value : 0;

			if (processStepId > 0)
			{
				var step = await tblWfwProcessStepService.GetById(processStepId);
				if (step == null)
				{
					TempData["PrintSettingsError"] = "گام انتخاب‌شده یافت نشد.";
					return RedirectToAction(nameof(Index));
				}
				processId = processId > 0 ? processId : (step.ProcessId ?? 0);
			}

			if (processId > 0)
			{
				var process = await tblWfwProcessService.GetById(processId);
				if (process == null || !IsCustomLensProcess(process))
				{
					TempData["PrintSettingsError"] = "تنظیمات چاپ خودکار فقط برای فرآیند عدسی سفارشی فعال است.";
					return RedirectToAction(nameof(Index));
				}
			}

			if (processStepId > 0)
			{
				await customLensPrintSettingsService.SaveStepSettingsAsync(processStepId, scopedSettings);
				TempData["PrintSettingsSaved"] = "تنظیمات چاپ گام با موفقیت ذخیره شد.";
			}
			else if (processId > 0)
			{
				await customLensPrintSettingsService.SaveProcessSettingsAsync(processId, scopedSettings);
				TempData["PrintSettingsSaved"] = "تنظیمات پیش‌فرض چاپ فرآیند با موفقیت ذخیره شد.";
			}
			else
			{
				var current = await customLensPrintSettingsService.GetAsync();
				current.IsEnabled = model.IsEnabled;
				current.ReportUrlTemplate = (model.ReportUrlTemplate ?? string.Empty).Trim();
				current.Printers = printerNames;
				await customLensPrintSettingsService.SaveAsync(current);
				TempData["PrintSettingsSaved"] = "تنظیمات چاپ با موفقیت ذخیره شد.";
			}

			if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
			{
				return Redirect(model.ReturnUrl);
			}

			return RedirectToAction(nameof(PrintSettings), new
			{
				processId = processId > 0 ? (int?)processId : null,
				processStepId = processStepId > 0 ? (int?)processStepId : null
			});
		}
		[Authorize(Roles = "admin,Process_Create")]
		public IActionResult Create()
		{
			List<SelectListItem> indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListDocument=indexDocuments;
			return View();
		}
		[Authorize(Roles = "admin,Process_Create")]
		[HttpPost]
		public IActionResult Create(TblWfwProcessDto tblWfwProcessDto)
		{
 			var item= tblWfwProcessService.Add(tblWfwProcessDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Process_Edit")]
		public IActionResult Edit(int id)
		{
			var itemResult = tblWfwProcessService.GetById(id);
			var item = itemResult.Result;
			List<SelectListItem> indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString(),Selected=item.IndexDocument.Equals(((int)o).ToString()) }).ToList();
			ViewBag.ListDocument = indexDocuments;
			
			return View(item);
		}
		[Authorize(Roles = "admin,Process_Edit")]
		[HttpPost]
		public IActionResult Edit(TblWfwProcessDto tblWfwProcessDto)
		{
			var item = tblWfwProcessService.Update(tblWfwProcessDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,Process_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblWfwProcessService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Process_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblWfwProcessService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id=p.ProcessId,
						   name=p.Name,
						   code=p.Code,
                           isActive = p.IsActive == true ? "فعال" : "غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

		private static bool IsCustomLensProcess(TblWfwProcessDto process)
		{
			return process != null &&
				process.IndexDocument.HasValue &&
				process.IndexDocument.Value == (short)IndexDocument.LnsOrder;
		}

	}
}
