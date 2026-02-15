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
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Linq.Expressions;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class ProcessController(ITblWfwProcessService tblWfwProcessService,ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Process_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,Process_Create")]
		public IActionResult Create()
		{
			List<SelectListItem> indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString() }).ToList();
			ViewBag.ListDocument=indexDocuments;
			return View();
		}
		[Authorize(Roles = "admin,Process_Create")]
		[HttpPost]
		public IActionResult Create(TblWfwProcessDto tblWfwProcessDto)
		{
 			var item= tblWfwProcessService.Add(tblWfwProcessDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Process_Edit")]
		public IActionResult Edit(int id)
		{
			var itemResult = tblWfwProcessService.GetById(id);
			var item = itemResult.Result;
			List<SelectListItem> indexDocuments = Enum.GetValues(typeof(IndexDocument)).Cast<IndexDocument>().Select(o => new SelectListItem() { Text = ((DescriptionAttribute[])(o.GetType().GetField(o.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)))[0].Description, Value = ((int)o).ToString(),Selected=item.IndexDocument.Equals(((int)o).ToString()) }).ToList();
			ViewBag.ListDocument = indexDocuments;
			
			return View(item);
		}
		[Authorize(Roles = "admin,Process_Edit")]
		[HttpPost]
		public IActionResult Edit(TblWfwProcessDto tblWfwProcessDto)
		{
			var item = tblWfwProcessService.Update(tblWfwProcessDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,Process_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = tblWfwProcessService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Process_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = tblWfwProcessService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id=p.ProcessId,
						   name=p.Name,
						   code=p.Code,
                           isActive = p.IsActive == true ? "فعال" : "غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
