using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using FClub.Models.Models.DTOs;
using FClub.Models.Models.ViewModels;
using FClub.SessionXtention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var sessionCarts = HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart") ?? default;

			foreach (var item in sessionCarts)
			{
				item.Activity = await _unitOfWork.ActivittyRepository.GetAsync(x => x.Id == item.ActivityId, includeProperties: "FromToPeriod");
			}

			return View(sessionCarts);
		}
		[HttpGet]
		public async Task<IActionResult> Summary()
		{
			IList<ShoppingCart> carts = new List<ShoppingCart>();
			if (HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart") != null &&
				HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart").Count() > 0)
			{
				carts = HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart").ToList();
			}
			foreach (var item in carts)
			{
				item.Activity = await _unitOfWork.ActivittyRepository.GetAsync(x => x.Id == item.ActivityId);
			}
			var claim = (ClaimsIdentity)User.Identity;
			var userId = Convert.ToInt32(claim.FindFirst(ClaimTypes.NameIdentifier).Value);
			SummaryVM summaryVM = new SummaryVM
			{
				ShoppingCarts = carts,
				AppUser = await _unitOfWork.AppUserRepository.GetAsync(x => x.Id == userId)
			};
				//create orderheader
				//enroll after confirmed

			return View(summaryVM);
		}
		public IActionResult Remove(int id)
		{
			var sessionCarts = HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart") ?? default;
			sessionCarts = sessionCarts.Where(x => x.ActivityId != id);
			HttpContext.Session.SetSession<IEnumerable<ShoppingCart>>("sessionCart", sessionCarts);
			return RedirectToAction(nameof(Index));
		}
	}
}
