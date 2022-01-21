using FClub.BraintreeConfig;
using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using FClub.Models.Models.DTOs;
using FClub.Models.Models.ViewModels;
using FClub.SessionXtention;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
		private readonly IBrainTreeGate _brainTree;
		public CartController(IUnitOfWork unitOfWork, IBrainTreeGate brainTree)
		{
			_unitOfWork = unitOfWork;
			_brainTree = brainTree;
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
			var gateway = _brainTree.GetGateWay();
			var clientToken = gateway.ClientToken.Generate();
			ViewBag.ClientToken = clientToken;


			return View(summaryVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Summary(IFormCollection formCollection, SummaryVM summaryVM)
		{
			if (ModelState.IsValid)
			{
				OrderHeader orderHeader = new OrderHeader
				{
					AppUser = await _unitOfWork.AppUserRepository.GetAsync(x => x.Id == summaryVM.AppUser.Id),
					Address = summaryVM.AppUser.Address,
					Email = summaryVM.AppUser.Email,
					Mobile = summaryVM.AppUser.Mobile,
					OrderDate = DateTime.Today,
					OrderStatus = "pending",
					OrderTotal = summaryVM.Total,//check//sUM(SUMMARYvM.PRICESELECTED)			 

				};
				await _unitOfWork.OrderHeaderRepository.CreateAsync(orderHeader);//check id created
				if(await _unitOfWork.SaveAsync())
				{
					foreach (var item in summaryVM.ShoppingCarts)
					{
						OrderDetail orderDetail = new OrderDetail
						{
							ActivittyId = item.ActivityId,
							OrderHeaderId = orderHeader.Id,
							Price = item.PriceSelected
						};
						await _unitOfWork.OrderDetailRepository.CreateAsync(orderDetail);
					}
					if(await _unitOfWork.SaveAsync())
					{
						return RedirectToAction(nameof(OrderConfirmation));
					}
				}
				//error while creating order
				
			}
			//error in modelState
			return View(summaryVM);
		}
		public IActionResult Remove(int id)
		{
			var sessionCarts = HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart") ?? default;
			sessionCarts = sessionCarts.Where(x => x.ActivityId != id);
			HttpContext.Session.SetSession<IEnumerable<ShoppingCart>>("sessionCart", sessionCarts);
			return RedirectToAction(nameof(Index));
		}
		public IActionResult ClearCart()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home");
		}
		public IActionResult OrderConfirmation()
		{
			//ORDEROnUMBER
			return View();
		}
	}
}
