using FClub.Data.Repository.IRepository;
using FClub.Models.Models.DTOs.ActivittyDtos;
using FClub.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _unitOfWork.ActivittyRepository.GetAllAync(includeProperties: "Instructor,FromToPeriod"));
		}
		[HttpGet]
		[ActionName("Create")]
		public async Task<IActionResult> CreateAsync()
		{
			var periodList = await _unitOfWork.FromToPeriodRepository.GetAllAync();
			var instructors = await _unitOfWork.InstructorRepository.GetAllAync();		
			
			ActivittyCreateVM model = new ActivittyCreateVM
			{
				Activity = new ActivittyCreateDto(),
				FromToPeriodList = periodList.Select(x => new SelectListItem
				{
					Text = x.Period,
					Value = x.Id.ToString()
				}),
				InstructorList = instructors.Select(x => new SelectListItem
				{
					Text = x.FulName,
					Value = x.Id.ToString()
				}),
				WeekDays = Enum.GetValues(typeof(Models.Models.WeekDays)).Cast<Models.Models.WeekDays>().Select(x=>new SelectListItem { 
				Text=x.ToString(),
				Value=((int)x).ToString()
				}).ToList()

		};
			return View(model);
		}

	}
}
