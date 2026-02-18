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
		ITblWfwResultStepRepository tblWfwResultStepRepository,
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
			ViewBag.NextStepResultMapJson = "{}";
			List<SelectListItem> procedures = Enum.GetValues(typeof(Procedure)).Cast<Procedure>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListProcedure = procedures;


			return View(new TblWfwProcessStepDto());
		}
		[Authorize(Roles = "admin,Step_Create")]
		[HttpPost]
		public IActionResult Create(TblWfwProcessStepDto tblWfwProcessStepDto, int[] role, int[] nextStep, string? nextStepResultConfig)
		{
			try
			{
				role ??= Array.Empty<int>();
				nextStep ??= Array.Empty<int>();
				tblWfwProcessStepDto.TblWfwRoleStepes = (from p in role select new TblWfwRoleStepDto { RoleId = p }).ToList();
				var processStepId = tblWfwProcessStepService.Add(tblWfwProcessStepDto).Result;
				var normalizedNextSteps = NormalizeNextStepIds(tblWfwProcessStepDto.ProcessId ?? 0, null, nextStep);
				var relationMap = tblWfwRelationStepRepository.SyncForFromStep(processStepId, normalizedNextSteps).Result;
				ReplaceResultOptionsForStep(Array.Empty<int>(), relationMap, ParseNextStepResultConfig(nextStepResultConfig));

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
			try
			{
				ViewBag.ListRole = roleService.GetAll().Result;
				List<SelectListItem> procedures = Enum.GetValues(typeof(Procedure)).Cast<Procedure>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
				ViewBag.ListProcedure = procedures;
				var item = tblWfwProcessStepService.GetById(id).Result;
				if (item == null)
				{
					return Json(new
					{
						success = false,
						message = "گام مورد نظر یافت نشد."
					});
				}

				var processId = item.ProcessId ?? item.TblWfwProcess?.ProcessId ?? 0;
				if (item.TblWfwProcess == null && processId > 0)
				{
					item.TblWfwProcess = tblWfwProcessService.GetById(processId).Result;
				}

				ViewBag.ProcessName = item.TblWfwProcess?.Name ?? string.Empty;
				ViewBag.NextStepOptions = BuildNextStepOptions(processId, item.ProcessStepId);
				ViewBag.SelectedNextStepIds = Array.Empty<int>();
				ViewBag.NextStepResultMapJson = "{}";

				try
				{
					var relationSteps = tblWfwRelationStepRepository.GetByFromProcessStepId(id)
						.Where(x => x.ToProcessStepId.HasValue)
						.ToList();

					ViewBag.SelectedNextStepIds = relationSteps
						.Select(x => x.ToProcessStepId!.Value)
						.Distinct()
						.ToArray();

					ViewBag.NextStepResultMapJson = BuildResultConfigJsonForRelations(relationSteps);
				}
				catch
				{
					// If legacy/broken relation data exists, keep form editable with empty transition config.
					ViewBag.SelectedNextStepIds = Array.Empty<int>();
					ViewBag.NextStepResultMapJson = "{}";
				}

				return View(item);
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
		[HttpPost]
		public IActionResult Edit(TblWfwProcessStepDto tblWfwProcessStepDto, int[] role, int[] nextStep, string? nextStepResultConfig)
		{
			try
			{
				role ??= Array.Empty<int>();
				nextStep ??= Array.Empty<int>();
				var relationIdsBefore = tblWfwRelationStepRepository
					.GetByFromProcessStepId(tblWfwProcessStepDto.ProcessStepId)
					.Select(x => x.RelationStepId)
					.ToList();

				tblWfwProcessStepDto.TblWfwRoleStepes = (from p in role select new TblWfwRoleStepDto { RoleId = p }).ToList();
				tblWfwProcessStepService.Update(tblWfwProcessStepDto).Wait();
				var normalizedNextSteps = NormalizeNextStepIds(tblWfwProcessStepDto.ProcessId ?? 0, tblWfwProcessStepDto.ProcessStepId, nextStep);
				var relationMap = tblWfwRelationStepRepository.SyncForFromStep(tblWfwProcessStepDto.ProcessStepId, normalizedNextSteps).Result;
				ReplaceResultOptionsForStep(relationIdsBefore, relationMap, ParseNextStepResultConfig(nextStepResultConfig));

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
			try
			{
				tblWfwProcessStepService.Delete(id).Wait();
				return Json(new
				{
					success = true,
					message = ""
				});
			}
			catch (Exception ex)
			{
				try
				{
					var currentStep = tblWfwProcessStepService.GetById(id).Result;
					if (currentStep != null)
					{
						currentStep.IsActive = false;
						tblWfwProcessStepService.Update(currentStep).Wait();
						return Json(new
						{
							success = true,
							message = "به دلیل وجود سابقه/ارتباط، گام غیرفعال شد."
						});
					}
				}
				catch
				{
					// ignore nested failure and return original error
				}

				return Json(new
				{
					success = false,
					message = ex.InnerException?.Message ?? ex.Message
				});
			}
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

		private string BuildResultConfigJsonForRelations(IEnumerable<TblWfwRelationStep> relationSteps)
		{
			var relations = (relationSteps ?? Enumerable.Empty<TblWfwRelationStep>())
				.Where(x => x.RelationStepId > 0 && x.ToProcessStepId.HasValue)
				.ToList();

			if (!relations.Any())
			{
				return "{}";
			}

			var relationToStepMap = relations
				.ToDictionary(x => x.RelationStepId, x => x.ToProcessStepId!.Value);

			var relationResultMap = tblWfwResultStepRepository
				.GetByRelationStepIds(relations.Select(x => x.RelationStepId))
				.Where(x => x.RelationStepId.HasValue && relationToStepMap.ContainsKey(x.RelationStepId.Value))
				.Where(x => x.IsActive == null || x.IsActive == 1)
				.Where(x => !string.IsNullOrWhiteSpace(x.Name))
				.OrderBy(x => x.ResultStepId)
				.GroupBy(x => relationToStepMap[x.RelationStepId!.Value])
				.ToDictionary(
					x => x.Key.ToString(),
					x => x.Select(y => y.Name!.Trim()).Distinct().ToList());

			return relationResultMap.Any()
				? JsonConvert.SerializeObject(relationResultMap)
				: "{}";
		}

		private static Dictionary<int, List<string>> ParseNextStepResultConfig(string? rawConfig)
		{
			if (string.IsNullOrWhiteSpace(rawConfig))
			{
				return new Dictionary<int, List<string>>();
			}

			Dictionary<string, List<string>>? rawMap;
			try
			{
				rawMap = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(rawConfig);
			}
			catch
			{
				rawMap = null;
			}

			if (rawMap == null || rawMap.Count == 0)
			{
				return new Dictionary<int, List<string>>();
			}

			var parsedMap = new Dictionary<int, List<string>>();
			foreach (var entry in rawMap)
			{
				if (!int.TryParse(entry.Key, out var toStepId) || toStepId <= 0)
				{
					continue;
				}

				var options = (entry.Value ?? new List<string>())
					.Select(x => x?.Trim())
					.Where(x => !string.IsNullOrWhiteSpace(x))
					.Select(x => x!.Length > 100 ? x[..100] : x!)
					.Distinct(StringComparer.OrdinalIgnoreCase)
					.ToList();

				if (options.Any())
				{
					parsedMap[toStepId] = options;
				}
			}

			return parsedMap;
		}

		private void ReplaceResultOptionsForStep(
			IEnumerable<int> relationIdsBefore,
			IDictionary<int, int> relationMapByToStepId,
			IDictionary<int, List<string>> optionsByToStepId)
		{
			var optionsByRelationId = new Dictionary<int, IEnumerable<string>>();
			foreach (var entry in optionsByToStepId ?? new Dictionary<int, List<string>>())
			{
				if (relationMapByToStepId.TryGetValue(entry.Key, out var relationStepId) && relationStepId > 0)
				{
					optionsByRelationId[relationStepId] = entry.Value ?? new List<string>();
				}
			}

			var relationIdsToClear = (relationIdsBefore ?? Enumerable.Empty<int>())
				.Concat(relationMapByToStepId.Values)
				.Where(x => x > 0)
				.Distinct()
				.ToList();

			var userId = HttpContext.Session.Get<int?>("UserId");
			tblWfwResultStepRepository.ReplaceForRelations(relationIdsToClear, optionsByRelationId, userId).Wait();
		}

	}
}
