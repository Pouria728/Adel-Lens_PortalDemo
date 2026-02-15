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
	public class RolesController(IRoleService roleService, IPermissionService permissionService, ICacheService cacheService) : Controller
	{
		[Authorize(Roles = "admin,Role_Index")]
		public IActionResult Index()
		{

			return View();
		}
		[Authorize(Roles = "admin,Role_Create")]
		public IActionResult Create()
		{
			ViewBag.ListPermission = permissionService.GetAll().Result;
			return View();
		}
		[Authorize(Roles = "admin,Role_Create")]
		[HttpPost]
		public IActionResult Create(RoleDto roleDto, int[] permission)
		{

			roleDto.TenantId = 1;
			roleDto.RoleKey = "";
			roleDto.RolePermissions = (from p in permission select new RolePermissionDto { PermissionId = p, PermissionKey = "" }).ToList();
			var item = roleService.Add(roleDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,Role_Edit")]
		public IActionResult Edit(int id)
		{
			ViewBag.ListPermission = permissionService.GetAll().Result;
			var item = roleService.GetById(id);
			return View(item.Result);
		}
		[Authorize(Roles = "admin,Role_Edit")]
		[HttpPost]
		public IActionResult Edit(RoleDto roleDto, int[] permission)
		{

			roleDto.RolePermissions = (from p in permission select new RolePermissionDto { PermissionId = p, PermissionKey = "" }).ToList();

			var item = roleService.Update(roleDto);
			return Json(new
			{
				success = true,
				message = ""
			});
		}
        [Authorize(Roles = "admin,Role_Delete")]
        [HttpPost]
        public IActionResult Delete(int id)
        {


            var item = roleService.Delete(id);
            return Json(new
            {
                success = true,
                message = ""
            });
        }
        [Authorize(Roles = "admin,Role_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = roleService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);// GetByTaxPayerId(taxPayerId,null,page,rows);
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
						   id = p.RoleId,
						   name = p.RoleName,
						   //isActive=p.IsActive==true?"فعال":"غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
