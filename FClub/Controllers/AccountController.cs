using FClub.Data.Repository.IRepository;
using FClub.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public AccountController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View(new LoginVM());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if (ModelState.IsValid)
			{
				bool isAuthenticated = false;
				var user=await _unitOfWork.AppUserRepository.GetAync(x => x.Username == loginVM.Username && x.Password == loginVM.Password);
				if (user != null)
				{
					ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
					claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
					claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Username));
					claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));
					isAuthenticated = true;
					if (isAuthenticated)
					{
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
														new ClaimsPrincipal(claimsIdentity));
						//returnurl
						return RedirectToAction("Index", "Home");
					}
					
				}
			}
			return View(new LoginVM());
		}
	}
}
