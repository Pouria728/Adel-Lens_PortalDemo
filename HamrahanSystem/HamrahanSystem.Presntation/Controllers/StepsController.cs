using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using HamrahanSystem.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Security;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class StepsController(
		ITblWfwProcessStepService tblWfwProcessStepService,
		ITblWfwProcessService tblWfwProcessService,
		ITblWfwRelationStepRepository tblWfwRelationStepRepository,
		IRoleService roleService,
		ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Step_Index")]
		public IActionResult Index(int id)
		{
		ViewBag.ProcessId=id;	
			return View();
		}
		[Authorize(Roles = "admin,Step_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListRole = roleService.GetAll().Result;
			ViewBag.Process = tblWfwProcessService.GetById(Id).Result;
			ViewBag.NextStepOptions = BuildNextStepOptions(Id, null);
			ViewBag.SelectedNextStepIds = Array.Empty<int>();
			List<SelectListItem> procedures = Enum.GetValues(typeof(Procedure)).Cast<Procedure>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListProcedure = procedures;


			return View(new TblWfwProcessStepDto());
		}
		[Authorize(Roles = "admin,Step_Create")]
		[HttpPost]
		public IActionResult Create(TblWfwProcessStepDto tblWfwProcessStepDto, int[] role, int[] nextStep)
		{
			try
			{
				role ??= Array.Empty<int>();
				nextStep ??= Array.Empty<int>();
				tblWfwProcessStepDto.TblWfwRoleStepes = (from p in role select new TblWfwRoleStepDto { RoleId = p }).ToList();
				var processStepId = tblWfwProcessStepService.Add(tblWfwProcessStepDto).Result;
				var normalizedNextSteps = NormalizeNextStepIds(tblWfwProcessStepDto.ProcessId ?? 0, null, nextStep);
				tblWfwRelationStepRepository.ReplaceForFromStep(processStepId, normalizedNextSteps).Wait();

				return Json(new
				{
					success = true,
					message = ""
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					message = ex.InnerException?.Message ?? ex.Message
				});
			}
		}
		[Authorize(Roles = "admin,Step_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListRole = roleService.GetAll().Result;
			List<SelectListItem> procedures = Enum.GetValues(typeof(Procedure)).Cast<Procedure>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListProcedure = procedures;
			var item = tblWfwProcessStepService.GetById(id);
			var processId = item.Result.ProcessId ?? item.Result.TblWfwProcess?.ProcessId ?? 0;
			ViewBag.NextStepOptions = BuildNextStepOptions(processId, item.Result.ProcessStepId);
			ViewBag.SelectedNextStepIds = tblWfwRelationStepRepository.GetByFromProcessStepId(id)
				.Where(x => x.ToProcessStepId.HasValue)
				.Select(x => x.ToProcessStepId!.Value)
				.Distinct()
				.ToArray();
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Step_Edit")]
		[HttpPost]
		public IActionResult Edit(TblWfwProcessStepDto tblWfwProcessStepDto, int[] role, int[] nextStep)
		{
			try
			{
				role ??= Array.Empty<int>();
				nextStep ??= Array.Empty<int>();

				tblWfwProcessStepDto.TblWfwRoleStepes = (from p in role select new TblWfwRoleStepDto { RoleId = p }).ToList();
				tblWfwProcessStepService.Update(tblWfwProcessStepDto).Wait();
				var normalizedNextSteps = NormalizeNextStepIds(tblWfwProcessStepDto.ProcessId ?? 0, tblWfwProcessStepDto.ProcessStepId, nextStep);
				tblWfwRelationStepRepository.ReplaceForFromStep(tblWfwProcessStepDto.ProcessStepId, normalizedNextSteps).Wait();

				return Json(new
				{
					success = true,
					message = ""
				});
			}
			catch (Exception ex)
			{
				return Json(new
				{
					success = false,
					message = ex.InnerException?.Message ?? ex.Message
				});
			}
		}
		[Authorize(Roles = "admin,Step_Delete")]
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var item = tblWfwProcessStepService.Delete(id);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Step_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblWfwProcessStepService.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id=p.ProcessStepId,
						   name=p.Name,
						   code=p.Code,
						   orderId=p.OrderId,
						   isPrint = p.IsPrint == true ? "فعال" : "غیر فعال",
						   isActive =p.IsActive==true?"فعال":"غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

		private List<SelectListItem> BuildNextStepOptions(int processId, int? currentStepId)
		{
			var steps = tblWfwProcessStepService.GetAll(processId, null, null, null, null, null).Result.Item1 ?? new List<TblWfwProcessStepDto>();

			return steps
				.Where(x => !currentStepId.HasValue || x.ProcessStepId != currentStepId.Value)
				.OrderBy(x => x.OrderId)
				.Select(x => new SelectListItem
				{
					Value = x.ProcessStepId.ToString(),
					Text = $"{x.OrderId} - {x.Name} ({x.Code})"
				})
				.ToList();
		}

		private IEnumerable<int> NormalizeNextStepIds(int processId, int? currentStepId, IEnumerable<int> nextStepIds)
		{
			var validIds = BuildNextStepOptions(processId, currentStepId)
				.Select(x => int.TryParse(x.Value, out var parsed) ? parsed : 0)
				.Where(x => x > 0)
				.ToHashSet();

			return (nextStepIds ?? Enumerable.Empty<int>())
				.Where(validIds.Contains)
				.Distinct()
				.ToList();
		}

	}
}
