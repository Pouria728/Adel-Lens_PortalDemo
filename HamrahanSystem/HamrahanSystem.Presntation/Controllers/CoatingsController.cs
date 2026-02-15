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
	public class CoatingsController(ITblLnsCoatingService tblLnsCoatingService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Coating_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,Coating_Create")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "admin,Coating_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsCoatingDto tblLnsCoatingDto)
		{
 			var item= tblLnsCoatingService.Add(tblLnsCoatingDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Coating_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsCoatingService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Coating_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsCoatingDto tblLnsCoatingDto)
		{
			var item = tblLnsCoatingService.Update(tblLnsCoatingDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,Coating_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblLnsCoatingService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Coating_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsCoatingService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
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
						   id=p.CoatingId,
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
