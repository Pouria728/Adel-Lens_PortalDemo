using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class DesignTypesController(ITblLnsDesignTypeService tblLnsDesignTypeService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,DesignType_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,DesignType_Create")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "admin,DesignType_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsDesignTypeDto tblLnsDesignTypeDto)
		{
 			var item= tblLnsDesignTypeService.Add(tblLnsDesignTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,DesignType_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsDesignTypeService.GetById(id);
			if (item.Result == null || item.Result.IsSpecial)
			{
				return NotFound();
			}
			return View(item.Result);
		}
		[Authorize(Roles = "admin,DesignType_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsDesignTypeDto tblLnsDesignTypeDto)
		{
			var item = tblLnsDesignTypeService.Update(tblLnsDesignTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,DesignType_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
			var existing = tblLnsDesignTypeService.GetById(id).Result;
			if (existing == null || existing.IsSpecial)
			{
				return Json(new { success = false, message = "رکورد موردنظر یافت نشد." });
			}

			var item = tblLnsDesignTypeService.Delete(id);
			return Json(new
			{
				success = true,
				message = ""
			});
        }
        [Authorize(Roles = "admin,DesignType_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;

			var allItems = tblLnsDesignTypeService.GetAll().Result ?? Enumerable.Empty<TblLnsDesignTypeDto>();
			var filtered = allItems.Where(x => x.IsSpecial == false);

			if (!string.IsNullOrWhiteSpace(itemGrid.Sidx))
			{
				var prop = typeof(TblLnsDesignTypeDto).GetProperty(itemGrid.Sidx, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				if (prop != null)
				{
					filtered = string.Equals(itemGrid.Sord, "desc", StringComparison.OrdinalIgnoreCase)
						? filtered.OrderByDescending(x => prop.GetValue(x, null))
						: filtered.OrderBy(x => prop.GetValue(x, null));
				}
			}

			var totalRecords = filtered.Count();
			if (totalRecords <= 0)
			{
				return Json(new { });
			}

			int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
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
						   name = p.Name,
						   code = p.Code,
						   description = p.Description,
						   orderId = p.OrderId,
						   isActive = p.IsActive == true ? "فعال" : "غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
