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
	public class SphCylResController(ITblLnsSphCylService tblLnsSphCylService,ITblLnsLensIndexRSphService tblLnsLensIndexRSphService , ITblLnsCylService tblLnsCylService,ITblClrDefineObjectService tblClrDefineObjectService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index(int id)
		{
			ViewBag.LensTypeRLensIndexRId = tblLnsLensIndexRSphService.GetById(id).Result.LensTypeRLensIndexRId;
			ViewBag.LensIndexRSphId=id;
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListCyl = tblLnsCylService.GetAll().Result.ToList();
			ViewBag.LensIndexRSph = tblLnsLensIndexRSphService.GetById(Id).Result;
			return View(new TblLnsSphCylDto());
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsSphCylDto tblLnsSphCylDto)
		{
 			var item= tblLnsSphCylService.Add(tblLnsSphCylDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListCyl = tblLnsCylService.GetAll().Result.ToList();
			var item = tblLnsSphCylService.GetById(id).Result;
			if (item.DefineObjectId.HasValue)
			{
				var itemgg = new List<TblClrDefineObjectDto>();
				itemgg.Add(tblClrDefineObjectService.GetById(item.DefineObjectId.Value).Result);
				ViewBag.ListDefineObject = itemgg;
			}
			else
			{
				var itemgg = new List<ViewSalListCustomerDto>();
				//itemgg.Add(viewSalListCustomerService.GetById(0).Result);
				ViewBag.ListDefineObject = itemgg;

			}
			return View(item);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsSphCylDto tblLnsSphCylDto)
		{
			var item = tblLnsSphCylService.Update(tblLnsSphCylDto);
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
            var item = tblLnsSphCylService.Delete(id);
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
			var item = tblLnsSphCylService.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id = p.SphCylId,
						   brand = p.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsBrand.Name,
						   lensType = p.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsBrandLensTypeR.TblLnsLensTypeR.Name,
						   lensIndex = p.TblLnsLensIndexRSph.TblLnsLensTypeRLensIndexR.TblLnsLensIndexR.Name,
						   sph = p.TblLnsLensIndexRSph.TblLnsSph.Name,
						   cyl=p.TblLnsCyl.Name,
						   orderId = p.OrderId,
					   }
			};
			return Json(jsonData); ;

		}
		[HttpPost]
		public JsonResult ListDefineDefineObjectSearch(string search)
		{
			List<TblClrDefineObjectDto> productes = new List<TblClrDefineObjectDto>();
				productes = tblClrDefineObjectService.GetByName(search).Result;

			var items = new
			{
				results = (from item in productes
						   select new
						   {
							   id = item.DefineObjectId,
							   text = item.NameObject
						   })
			};
			return Json(items);
		}

	}
}
