using FClub.Data.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	//[Authorize(Roles ="Admin")]
	public class ActivittyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ActivittyController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _unitOfWork.ActivittyRepository.GetAllAync(includeProperties:"Instructor,FromToPeriod"));
		}
	}
}
