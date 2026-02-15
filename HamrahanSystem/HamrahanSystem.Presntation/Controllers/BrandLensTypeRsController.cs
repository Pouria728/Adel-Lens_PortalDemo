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
	public class BrandLensTypeRsController(ITblLnsBrandLensTypeRService tblLnsBrandLensTypeRService,ITblLnsBrandService tblLnsBrandService,ITblLnsLensTypeRService tblLnsLensTypeRService, ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index" )]
        public IActionResult Index(int Id)
		{
			ViewBag.BrandId = Id;
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListLensTypeR=tblLnsLensTypeRService.GetAll().Result.ToList();
			ViewBag.ListBrand=tblLnsBrandService.GetById(Id).Result;
			return View(new TblLnsBrandLensTypeRDto());
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsBrandLensTypeRDto tblLnsBrandLensTypeRDto)
		{
			var item = tblLnsBrandLensTypeRService.Add(tblLnsBrandLensTypeRDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListLensTypeR = tblLnsLensTypeRService.GetAll().Result.ToList();
			var item = tblLnsBrandLensTypeRService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsBrandLensTypeRDto tblLnsBrandLensTypeRDto)
		{
			var item = tblLnsBrandLensTypeRService.Update(tblLnsBrandLensTypeRDto);
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
            var item = tblLnsBrandLensTypeRService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Brand_Index")]
		[HttpPost]
		public JsonResult Detail()
		{

			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblLnsBrandLensTypeRService.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows,itemGrid.Sord,itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id = p.BrandLensTypeRId,
						   brandName = p.TblLnsBrand.Name,
						   lenstype = p.TblLnsLensTypeR.Name,
						   orderId = p.OrderId,

					   }
			};
			return Json(jsonData); ;

		}

	}
}
