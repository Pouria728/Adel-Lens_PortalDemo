using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using NetTopologySuite.Operation.Buffer;
using System.Linq.Expressions;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class DesignTypeLensIndexsController(ITblLnsDesignTypeLensIndexService tblLnsDesignTypeLensIndexService,ITblLnsBrandDesignTypeService tblLnsBrandDesignTypeService ,ITblLnsLensIndexService tblLnsLensIndexService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index(int Id)
		{

			ViewBag.BrandLensTypeId = tblLnsBrandDesignTypeService.GetById(Id).Result.BrandLensTypeId;
			ViewBag.BrandDesignTypeId = Id;
			return View();

		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListLensIndex = tblLnsLensIndexService.GetAll().Result.ToList();
			ViewBag.BrandDesignType = tblLnsBrandDesignTypeService.GetById(Id).Result;
			return View(new TblLnsDesignTypeLensIndexDto());
			
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsDesignTypeLensIndexDto tblLnsDesignTypeLensIndexDto)
		{
 			var item= tblLnsDesignTypeLensIndexService.Add(tblLnsDesignTypeLensIndexDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListLensIndex = tblLnsLensIndexService.GetAll().Result.ToList();
			var item = tblLnsDesignTypeLensIndexService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsDesignTypeLensIndexDto tblLnsDesignTypeLensIndexDto)
		{
			var item = tblLnsDesignTypeLensIndexService.Update(tblLnsDesignTypeLensIndexDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,Brand_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblLnsDesignTypeLensIndexService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Brand_Index")]
		public JsonResult Detail(int designTypeId,string sidx, string sord, int page = 1, int rows = 10)
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsDesignTypeLensIndexService.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
			totalRecords = item.Result.Item2;
			if (totalRecords <= 0)
			{
				return Json(new { });
			}

			int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
			var jsonData = new
			{
				total = totalPages,
				page = page,
				records = totalRecords,
				rows = from p in item.Result.Item1
					   select new
					   {
						   id=p.DesignTypeLensIndexId,
						   lensIndx=p.TblLnsLensIndex.Name,
						   brand=p.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsBrand.Name,
						   lenstype=p.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsLensType.Name,
						   designType=p.TblLnsBrandDesignType.TblLnsDesignType.Name,
						   orderId=p.OrderId,
					   }
			};
			return Json(jsonData); ;

		}

	}
}
