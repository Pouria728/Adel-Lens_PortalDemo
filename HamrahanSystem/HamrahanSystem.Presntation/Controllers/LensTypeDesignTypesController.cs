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
	public class LensTypeDesignTypesController(ITblLnsBrandDesignTypeService tblLnsBrandDesignTypeServiceervice,ITblLnsBrandLensTypeService tblLnsBrandLensTypeService,ITblLnsDesignTypeService tblLnsDesignTypeService ,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index(int id)
		{
			ViewBag.BrandId = tblLnsBrandLensTypeService.GetById(id).Result.BrandId;
			ViewBag.BrandLensTypeId = id;
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListDesignType = tblLnsDesignTypeService.GetAll().Result.ToList();
			ViewBag.BrandLensType = tblLnsBrandLensTypeService.GetById(Id).Result;
			return View(new TblLnsBrandDesignTypeDto());
			
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsBrandDesignTypeDto tblLnsBrandDesignTypeDto)
		{
 			var item= tblLnsBrandDesignTypeServiceervice.Add(tblLnsBrandDesignTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListDesignType = tblLnsDesignTypeService.GetAll().Result.ToList();
			var item = tblLnsBrandDesignTypeServiceervice.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsBrandDesignTypeDto tblLnsBrandDesignTypeDto)
		{
			var item = tblLnsBrandDesignTypeServiceervice.Update(tblLnsBrandDesignTypeDto);
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
            var item = tblLnsBrandDesignTypeServiceervice.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Brand_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsBrandDesignTypeServiceervice.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id=p.BrandDesignTypeId,
						   designType=p.TblLnsDesignType.Name,
						   brand=p.TblLnsBrandLensType.TblLnsBrand.Name,
						   lensType = p.TblLnsBrandLensType.TblLnsLensType.Name,
						   orderId=p.OrderId,
					   }
			};
			return Json(jsonData); ;

		}

	}
}
