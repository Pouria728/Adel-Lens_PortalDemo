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
	public class LensIndexRSphsController(ITblLnsLensIndexRSphService tblLnsLensIndexRSphService,ITblLnsLensTypeRLensIndexRService tblLnsLensTypeRLensIndexRService ,ITblLnsSphService tblLnsSphService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index(int id)
		{
			ViewBag.BrandLensTypeRId = tblLnsLensTypeRLensIndexRService.GetById(id).Result.BrandLensTypeRId;
			ViewBag.lensIndexRId = id;


			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListSph = tblLnsSphService.GetAll().Result.ToList();
			ViewBag.LensTypeRLensIndexR = tblLnsLensTypeRLensIndexRService.GetById(Id).Result;
			return View(new TblLnsLensIndexRSphDto());
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsLensIndexRSphDto tblLnsLensIndexRSphDto)
		{
 			var item= tblLnsLensIndexRSphService.Add(tblLnsLensIndexRSphDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListSph = tblLnsSphService.GetAll().Result.ToList();
			var item = tblLnsLensIndexRSphService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsLensIndexRSphDto tblLnsLensIndexRSphDto)
		{
			var item = tblLnsLensIndexRSphService.Update(tblLnsLensIndexRSphDto);
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
            var item = tblLnsLensIndexRSphService.Delete(id);
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
			var item = tblLnsLensIndexRSphService.GetAll(itemGrid.Id,null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id=p.LensIndexRSphId,
						   brand=p.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsBrand.Name,
						   lensType = p.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsLensTypeR.Name,
						   lensIndex = p.TblLnsLensTypeRLensIndexR.TblLnsLensIndexR.Name,
						   sph = p.TblLnsSph.Name,
						   orderId=p.OrderId,
					   }
			};
			return Json(jsonData); ;

		}

	}
}
