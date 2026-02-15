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
	public class LensIndexsController(ITblLnsLensIndexService tblLnsLensIndexService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,LensType_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,LensType_Create")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "admin,LensType_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsLensIndexDto tblLnsLensIndexDto)
		{
 			var item= tblLnsLensIndexService.Add(tblLnsLensIndexDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,LensType_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsLensIndexService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,LensType_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsLensIndexDto tblLnsLensIndexDto)
		{
			var item = tblLnsLensIndexService.Update(tblLnsLensIndexDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,LensType_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblLnsLensIndexService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,LensType_Index")]
		public JsonResult Detail(string sidx, string sord, int page = 1, int rows = 10)
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsLensIndexService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
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
						   id=p.LensIndexId,
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
