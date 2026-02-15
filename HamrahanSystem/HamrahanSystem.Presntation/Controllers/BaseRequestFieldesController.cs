using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class BaseRequestFieldesController(IBaseRequestFieldService baseRequestFieldService,IBaseRequesteService baseRequesteService, ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,BaseRequest_Index")]
		public IActionResult Index()
		{

			return View();
		}
		[Authorize(Roles = "admin,BaseRequest_Create")]
		public IActionResult Create(int id=0)
		{
            ViewBag.ListBaseRequest = baseRequesteService.GetAll().Result;
            var item = new BaseRequestFieldDto();
            if (id>0)
			{
				item.BaseRequestFielde_BaseRequestParentFieldId=baseRequestFieldService.GetById(id).Result;
                item.BaseRequestParentFieldId = id;
				item.BaseRequestId = item.BaseRequestFielde_BaseRequestParentFieldId.BaseRequestId;
            }
            
			return View(item);
		}
		[Authorize(Roles = "admin,BaseRequest_Create")]
		[HttpPost]
		public IActionResult Create(BaseRequestFieldDto baseRequestFieldDto)
		{

			var item = baseRequestFieldService.Add(baseRequestFieldDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,BaseRequest_Edit")]
		public IActionResult Edit(int id)
		{
			var item = baseRequestFieldService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,BaseRequest_Edit")]
		[HttpPost]
		public IActionResult Edit(BaseRequestFieldDto baseRequestFieldDto)
		{

			var item = baseRequestFieldService.Update(baseRequestFieldDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,BaseRequest_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = baseRequestFieldService.GetAll(null, itemGrid.Page, itemGrid.Rows,itemGrid.Sord,itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id = p.BaseRequestFieldId,
						   name = p.Title,
						   parentId=p.FieldType==6 ? 1 : 0,
						   //isActive=p.IsActive==true?"فعال":"غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
