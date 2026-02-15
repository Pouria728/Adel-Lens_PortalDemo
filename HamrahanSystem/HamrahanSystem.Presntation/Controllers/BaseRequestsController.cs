using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.MappingImplementation;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore.Query.Internal;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq.Expressions;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class BaseRequestsController(IBaseRequesteService baseRequesteService, ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,BaseRequest_Index")]
		public IActionResult Index()
		{

			return View();
		}
		[Authorize(Roles = "admin,BaseRequest_Create")]
		public IActionResult Create()
		{
            List<SelectListItem> typeRequestes = Enum.GetValues(typeof(TypeRequest)).Cast<TypeRequest>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
            ViewBag.ListTypeRequest = typeRequestes;
            //ViewBag.ListPermission = baseRequesteService.GetAll().Result;
            return View();
		}
		[Authorize(Roles = "admin,BaseRequest_Create")]
		[HttpPost]
		public IActionResult Create(BaseRequesteDto baseRequesteDto)
		{

			
			var item = baseRequesteService.Add(baseRequesteDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,BaseRequest_Edit")]
		public IActionResult Edit(int id)
		{
            List<SelectListItem> typeRequestes = Enum.GetValues(typeof(TypeRequest)).Cast<TypeRequest>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
            ViewBag.ListTypeRequest = typeRequestes;
            var item = baseRequesteService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,BaseRequest_Edit")]
		[HttpPost]
		public IActionResult Edit(BaseRequesteDto baseRequesteDto)
		{


			var item = baseRequesteService.Update(baseRequesteDto);
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
			var item = baseRequesteService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id = p.BaseRequestId,
						   name = p.Title,
						   isActive=p.IsActive==true?"فعال":"غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
