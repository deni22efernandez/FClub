using FClub.Data;
using FClub.Data.Repository;
using FClub.Data.Repository.IRepository;
using FClub.Models;
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
				item.ActivittyDays = (ICollection<ActivittyDays>)await _unitOfWork.ActivittyDaysRepository.GetAllAync(includeProperties: "WeekDay");
			}
			return View(actList);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var act = await _unitOfWork.ActivittyRepository.GetAync(includeProperties: "Instructor,FromToPeriod,ActivittyDays");
			act.ActivittyDays = (ICollection<ActivittyDays>)await _unitOfWork.ActivittyDaysRepository.GetAllAync(includeProperties: "WeekDay");

			return View(act);
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
