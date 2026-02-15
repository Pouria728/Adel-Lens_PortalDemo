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
	public class BrandLensTypesController(ITblLnsBrandLensTypeService tblLnsBrandLensTypeService, ITblLnsBrandService tblLnsBrandService, ITblLnsLensTypeService tblLnsLensTypeService, ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index(int Id)
		{
			ViewBag.BrandId = Id;
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListLensType = tblLnsLensTypeService.GetAll().Result.Where(x => x.IsSpecial == false).ToList();
			ViewBag.ListBrand = tblLnsBrandService.GetById(Id).Result;
			return View(new TblLnsBrandLensTypeDto());
			
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsBrandLensTypeDto tblLnsBrandLensTypeDto)
		{
 			var item= tblLnsBrandLensTypeService.Add(tblLnsBrandLensTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListLensType = tblLnsLensTypeService.GetAll().Result.Where(x => x.IsSpecial == false).ToList();
			var item = tblLnsBrandLensTypeService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsBrandLensTypeDto tblLnsBrandLensTypeDto)
		{
			var item = tblLnsBrandLensTypeService.Update(tblLnsBrandLensTypeDto);
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
            var item = tblLnsBrandLensTypeService.Delete(id);
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
			var item = tblLnsBrandLensTypeService.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id = p.BrandLensTypeId,
						   brandName = p.TblLnsBrand.Name,
						   lenstype = p.TblLnsLensType.Name,
						   orderId = p.OrderId,
					   }
			};
			return Json(jsonData); ;

		}

	}
}
