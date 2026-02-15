using FastReport.Web;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Application.UseCaseImplementation;
using HamrahanSystem.Application.UseCaseInterface;
using HamrahanSystem.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Hosting.Internal;
using NetTopologySuite.Operation.Buffer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HamrahanSystem.Presntation.Controllers
{
	[Authorize]
	public class UsersController(IUserService userService,IRoleService roleService,ICacheService cacheService, IViewSalListCustomerService viewSalListCustomerService) : Controller
	{

		[Authorize(Roles = "admin,User_Index")]
		public IActionResult Index()
		{
			
			return View();
		}
		[Authorize(Roles = "admin,User_Create")]
		public IActionResult Create()
		{
			ViewBag.ListRole = roleService.GetAll().Result;
			UserDto item = new UserDto();
			return View(item);
		}
		[Authorize(Roles = "admin,User_Create")]
		[HttpPost]
		public IActionResult Create(UserDto userDto)
		{
			string passkey= Guid.NewGuid().ToString();
			userDto.PasswordHash = Helper.EncryptPassword(userDto.PasswordHash, passkey);
			userDto.PasswordSalt=passkey;
			userDto.InsertDate = DateTime.Now;
			userDto.InsertUserId = 1;
			userDto.DisplayName = "";
			userDto.Source = "";
			userDto.NationalCode = "";
			userDto.Mobile = "";
			userDto.Email = "";
			userDto.LastDirectoryUpdate = DateTime.Now;
			userDto.UserImage = "";
			userDto.FiscalYear = "";
			

 			var item= userService.Add(userDto);
			
			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,User_Edit")]
		public IActionResult Edit(int id)
		{
			
			ViewBag.ListRole = roleService.GetAll().Result;
			var item = userService.GetById(id);
			var itemDto = item.Result;
			if (itemDto.InfoCustomerId.HasValue)
			{
				var itemgg = new List<ViewSalListCustomerDto>();	
				itemgg.Add(viewSalListCustomerService.GetById(itemDto.InfoCustomerId.Value).Result);
				ViewBag.ListCustomer = itemgg;
			}
			else
			{
				var itemgg = new List<ViewSalListCustomerDto>();
				//itemgg.Add(viewSalListCustomerService.GetById(0).Result);
				ViewBag.ListCustomer = itemgg;
				
			}
			try
			{
				itemDto.PasswordHash = Helper.DecryptPassword(itemDto.PasswordHash, itemDto.PasswordSalt);
			}
			catch
			{ }
			return View(itemDto);
		}
		[Authorize(Roles = "admin,User_Edit")]
		[HttpPost]
		public IActionResult Edit(UserDto userDto)
		{
			string passkey = Guid.NewGuid().ToString();
			userDto.PasswordHash = Helper.EncryptPassword(userDto.PasswordHash, passkey);
			userDto.PasswordSalt = passkey;
			userDto.UpdateDate = DateTime.Now;
			userDto.DisplayName = "";
			userDto.Source = "";
			userDto.NationalCode = "";
			userDto.Mobile = "";
			userDto.Email = "";
			userDto.LastDirectoryUpdate = DateTime.Now;
			userDto.UserImage = "";
			userDto.FiscalYear = "";
			

			var item = userService.Update(userDto);

			return Json(new
			{
				success = true,
				message = ""
			});
		}
		[Authorize(Roles = "admin,User_Index")]
		public JsonResult Detail()
		{
			GridDto itemGrid = JsonConvert.DeserializeObject<GridDto>(Request.Form.First().Key);
			int pageIndex = Convert.ToInt32(itemGrid.Page) - 1;
			int pageSize = itemGrid.Rows;
			int totalRecords = 0;
			var item = userService.GetAll(null, itemGrid.Page, itemGrid.Rows, itemGrid.Sord, itemGrid.Sidx);
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
						   id=p.UserId,
						   firstName=p.FirstName,
						   lastName= p.LastName,
						   isActive=p.IsActive==true?"فعال":"غیر فعال"
					   }
			};
			return Json(jsonData); ;

		}

	}
}
