using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HamrahanSystem.Application.DTOs;
using HamrahanSystem.Presntation.Models;

using System.Diagnostics;
using System.Security.Claims;
using HamrahanSystem.Application.UseCaseInterface;
using NetTopologySuite.Index.HPRtree;

namespace HamrahanSystem.Presntation.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {
        

        public async Task<IActionResult> LogIn()
        {

			HttpContext.Session.Clear();
			//var principal = CreatePrincipal(new PersonDto());

			//await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			return View();

			
        }
		[HttpPost]
		public async Task<IActionResult> LogIn(string email,string password)
		{
			HttpContext.Session.Clear();
			var userItems=userService.GetByUserName(email).Result;
			foreach (var item in userItems)
			{
				if (Helper.EncryptPassword(password,item.PasswordSalt) == item.PasswordHash)
				{
					var principal = CreatePrincipal(item);

					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
					return RedirectToAction("Index", "Home");
				}

			}
		

			return View();


		}
		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Clear();
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("LogIn", "Account");
		}
		private ClaimsPrincipal CreatePrincipal(UserDto userDto)
		{
			
			var claims = new List<Claim>
	{
		new Claim("UserId", userDto.UserId.ToString()),
		new Claim("UserName", $"{userDto.FirstName} {userDto.LastName}"),
		new Claim(ClaimTypes.Role, "admin")
	};
			var principal = new ClaimsPrincipal();
			HttpContext.Session.Set<int>("UserId", userDto.UserId);
			HttpContext.Session.Set<string>("UserName", $"{userDto.FirstName} {userDto.LastName}");
			HttpContext.Session.Set<int?>("CustomerId",userDto.InfoCustomerId);
			HttpContext.Session.Set<bool?>("IsAdmin", userDto.IsAdmin);
			HttpContext.Session.Set<int?>("RoleId", userDto.UserRoles.Any()? userDto.UserRoles.First().RoleId:null);
			if (userDto.UserRoles.Count() > 0 && userDto.UserRoles?.First().Role.RolePermissions.Count >0 )
			{
				
				var itemPermission = userDto.UserRoles.First().Role.RolePermissions.Select(x => x.Permission.PermissionKey).ToList();
				if (userDto.IsAdmin)
					itemPermission.Add("admin");
				HttpContext.Session.Set<List<string>>("Roles", itemPermission);
			}
			else
			{
				var itemPermission = new List<string>() { ""};
				if (userDto.IsAdmin)
					itemPermission.Add("admin");
				HttpContext.Session.Set<List<string>>("Roles", itemPermission);
			}


				//HttpContext.Session.Set<List<string>>("Roles", new List<string> {"admin"});
				principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
			return principal;
		}


	}
}
