using FClub.Data;
using FClub.Data.Repository;
using FClub.Data.Repository.IRepository;
using FClub.Models;
using FClub.Models.Models;
using FClub.Models.Models.DTOs;
using FClub.Models.Models.ViewModels;
using FClub.SessionXtention;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	public class HomeController : Controller
	{
		//private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public HomeController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IActionResult> Index()
		{
			var actList = await _unitOfWork.ActivittyRepository.GetAllAync(includeProperties: "Instructor,FromToPeriod,ActivittyDays");
			foreach (var item in actList)
			{
				item.ActivittyDays = (ICollection<ActivittyDays>)await _unitOfWork.ActivittyDaysRepository.GetAllAync(x => x.ActivittyId == item.Id, includeProperties: "WeekDay");
			}
			return View(actList);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var act = await _unitOfWork.ActivittyRepository.GetAsync(x => x.Id == id, includeProperties: "Instructor,FromToPeriod,ActivittyDays");
			act.ActivittyDays = (ICollection<ActivittyDays>)await _unitOfWork.ActivittyDaysRepository.GetAllAync(x => x.ActivittyId == id, includeProperties: "WeekDay");

			IList<ShoppingCart> carts = new List<ShoppingCart>();
			if (HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart") != null &&
				   HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart").Count() > 0)
			{
				carts = HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart").ToList();
			}

			HomeDetailsVM homeDetailsVM = new HomeDetailsVM
			{
				Activity = act,
				PriceSelected = carts.Where(x => x.ActivityId == id).Select(y => y.PriceSelected).FirstOrDefault(),
				InCart = carts.Any(x=>x.ActivityId==id)
			};

			if (homeDetailsVM.PriceSelected != 0)
			{
				if (homeDetailsVM.PriceSelected == homeDetailsVM.Activity.FreePass)
					TempData["radio"] = 4;
				else if (homeDetailsVM.PriceSelected == homeDetailsVM.Activity.PricePerClass)
					TempData["radio"] = 1;
				else if (homeDetailsVM.PriceSelected == homeDetailsVM.Activity.PricePerMonth)
					TempData["radio"] = 2;
				else
					TempData["radio"] = 3;
			}
			

			return View(homeDetailsVM);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Details(IFormCollection formCollection, HomeDetailsVM homeDetailsVM)
		{
			if (ModelState.IsValid)
			{
				var priceSelected = Convert.ToInt32(formCollection["radio"]);
				var price = 0;
				switch (priceSelected)
				{
					case 1:
						price = homeDetailsVM.Activity.PricePerClass;
						break;
					case 2:
						price = homeDetailsVM.Activity.PricePerMonth;
						break;
					case 3:
						price = homeDetailsVM.Activity.PricePer3Months;
						break;
					case 4:
						price = homeDetailsVM.Activity.FreePass;
						break;
				}


				IList<ShoppingCart> carts = new List<ShoppingCart>();
				if (HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart") != null &&
					   HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart").Count() > 0)
				{
					carts = HttpContext.Session.GetSession<IEnumerable<ShoppingCart>>("sessionCart").ToList();
				}

				if (carts.Count() > 0 && carts.Where(x=>x.ActivityId==homeDetailsVM.Activity.Id).Count() > 0)
				{
					carts.Where(x => x.ActivityId == homeDetailsVM.Activity.Id).FirstOrDefault().PriceSelected = price;
				}
				else
				{
					carts.Add(new ShoppingCart
					{
						ActivityId = homeDetailsVM.Activity.Id,
						PriceSelected = price
					});
				}
				

				HttpContext.Session.SetSession<IEnumerable<ShoppingCart>>("sessionCart", carts);


				//cart setted!
				return RedirectToAction(nameof(Index));

			}
			return View(homeDetailsVM);
		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
