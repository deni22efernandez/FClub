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
using System.Threading.Tasks;

namespace FClub.Controllers
{
	//[Authorize]
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
			List<Activitty> actList = new List<Activitty>();
			foreach (var item in sessionCarts)
			{
				Activitty activitty = await _unitOfWork.ActivittyRepository.GetAsync(x => x.Id == item.ActivityId);
				actList.Add(activitty);
			}
			CartIndexVM cartIndexVM = new CartIndexVM()
			{
				ShoppingCarts = sessionCarts,
				Activitties = actList
			};
			return View(cartIndexVM);
		}
	}
}
