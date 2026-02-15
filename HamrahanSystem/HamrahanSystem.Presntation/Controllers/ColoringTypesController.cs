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
	public class ColoringTypesController(ITblLnsColoringTypeService tblLnsColoringTypeService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,ColoringType_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,ColoringType_Create")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "admin,ColoringType_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsColoringTypeDto tblLnsColoringTypeDto )
		{
 			var item= tblLnsColoringTypeService.Add(tblLnsColoringTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,ColoringType_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsColoringTypeService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,ColoringType_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsColoringTypeDto tblLnsColoringTypeDto)
		{
			var item = tblLnsColoringTypeService.Update(tblLnsColoringTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,ColoringType_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblLnsColoringTypeService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,ColoringType_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsColoringTypeService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
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
						   id=p.ColoringTypeId,
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
