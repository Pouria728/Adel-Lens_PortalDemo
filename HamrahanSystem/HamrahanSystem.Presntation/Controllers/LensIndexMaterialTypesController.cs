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
	public class LensIndexMaterialTypesController(ITblLnsLensIndexMaterialTypeService tblLnsLensIndexMaterialTypeService,ITblLnsMaterialTypeService tblLnsMaterialTypeService,ITblLnsDesignTypeLensIndexService tblLnsDesignTypeLensIndexService,ITblClrDefineObjectService tblClrDefineObjectService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index(int id)
		{
			ViewBag.BrandLensTypeId= tblLnsDesignTypeLensIndexService.GetById(id).Result.BrandDesignTypeId;
			ViewBag.DesignTypeLensIndexId = id;
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create(int Id)
		{
			ViewBag.ListMaterialType = tblLnsMaterialTypeService.GetAll().Result.ToList();
			ViewBag.DesignTypeLensIndex = tblLnsDesignTypeLensIndexService.GetById(Id).Result;
			return View(new TblLnsLensIndexMaterialTypeDto());
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsLensIndexMaterialTypeDto tblLnsLensIndexMaterialTypeDto)
		{
 			var item= tblLnsLensIndexMaterialTypeService.Add(tblLnsLensIndexMaterialTypeDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListMaterialType = tblLnsMaterialTypeService.GetAll().Result.ToList();
			var item = tblLnsLensIndexMaterialTypeService.GetById(id).Result;
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
		public IActionResult Edit(TblLnsLensIndexMaterialTypeDto tblLnsLensIndexMaterialTypeDto)
		{
			var item = tblLnsLensIndexMaterialTypeService.Update(tblLnsLensIndexMaterialTypeDto);
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
            var item = tblLnsLensIndexMaterialTypeService.Delete(id);
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
			var item = tblLnsLensIndexMaterialTypeService.GetAll(itemGrid.Id, null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   
						   id = p.DesignTypeLensIndexId,
						   materialType = p.TblLnsMaterialType.Name,
						   lensIndx = p.TblLnsDesignTypeLensIndex.TblLnsLensIndex.Name,
						   brand = p.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsBrand.Name,
						   lenstype = p.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsBrandLensType.TblLnsLensType.Name,
						   designType = p.TblLnsDesignTypeLensIndex.TblLnsBrandDesignType.TblLnsDesignType.Name,
						   
					   }
			};
			return Json(jsonData); ;

		}

	}
}
