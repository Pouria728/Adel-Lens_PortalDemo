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
	public class BrandsController(ITblLnsBrandService tblLnsBrandService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Brand_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		public IActionResult Create()
		{
			return View();
		}
		[Authorize(Roles = "admin,Brand_Create")]
		[HttpPost]
		public IActionResult Create(TblLnsBrandDto taxPayerCustomerDto)
		{
 			var item= tblLnsBrandService.Add(taxPayerCustomerDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		public IActionResult Edit(int id)
		{
			var item = tblLnsBrandService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Brand_Edit")]
		[HttpPost]
		public IActionResult Edit(TblLnsBrandDto tblLnsBrandDto)
		{
			var item = tblLnsBrandService.Update(tblLnsBrandDto);
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
            var item = tblLnsBrandService.Delete(id);
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
			var item = tblLnsBrandService.GetAll(null, itemGrid.Page, itemGrid.Rows,itemGrid.Sord,itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id=p.BrandId,
						   code=p.Code,
						   name=p.Name,
						   description=p.Description,
						   orderId=p.OrderId,
						   isActive=p.IsActive==true?"فعال":"غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
