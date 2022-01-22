using FClub.CustomMapper;
using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
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
		public async Task<IActionResult> LoginAsync(LoginVM loginVM, string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				bool isAuthenticated = false;
				var user = await _unitOfWork.AppUserRepository.GetAsync(x => x.Username == loginVM.Username && x.Password == loginVM.Password);
				
				if (user != null)
				{
					ClaimsIdentity claimsIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
					if (user.Discriminator == "AppUser") {
						
						claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
						claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Username));
						claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "User"));
						isAuthenticated = true;
					}
					else if (user.Discriminator == "Instructor")
					{

						claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
						claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Username));
						claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Instructor"));
						isAuthenticated = true;
					}
					else
					{
						claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
						claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Username));
						claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
						isAuthenticated = true;
					}
					if (isAuthenticated)
					{
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
														new ClaimsPrincipal(claimsIdentity));
						//returnurl
						if (returnUrl != null)
						{
							if (Url.IsLocalUrl(returnUrl))
							{
								return LocalRedirect(returnUrl);
							}
						}
						return RedirectToAction("Index", "Home");
					}

				}
			}
			return View(new LoginVM());
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View(new RegisterVM());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult>Register(RegisterVM registerVM)
		{
			if (ModelState.IsValid)
			{
				var user = await _unitOfWork.AppUserRepository.GetAsync(x => x.Username == registerVM.Username);
				if (user == null)
				{
					AppUser newUser = registerVM.Map<AppUser>();
					newUser.Email = newUser.Username;
					await _unitOfWork.AppUserRepository.CreateAsync(newUser);
					if (await _unitOfWork.SaveAsync())
						//successfull registration msg
						return RedirectToAction(nameof(Login));
				}
				//user already exists!
				ModelState.AddModelError("error", "User alredy exists!");
			}
			//error msg
			return View(registerVM);
		}
		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync();
			return RedirectToAction("Index","Home");
		}
	}
}
