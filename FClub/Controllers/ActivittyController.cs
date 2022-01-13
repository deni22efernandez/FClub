using FClub.CustomMapper;
using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using FClub.Models.Models.DTOs.ActivittyDtos;
using FClub.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	//[Authorize(Roles ="Admin")]
	public class ActivittyController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _hostEnv;
		public ActivittyController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnv)
		{
			_unitOfWork = unitOfWork;
			_hostEnv = hostEnv;
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
			//var days=await _unitOfWork.
			
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
				

		};
			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ActivittyCreateVM model)
		{
			if (ModelState.IsValid)
			{
				var files = HttpContext.Request.Form.Files;
				if (files != null)
				{
					var rootPath = _hostEnv.WebRootPath;
					var fileName = Guid.NewGuid().ToString();
					var extention = Path.GetExtension(files[0].FileName);
					var uploads = Path.Combine(rootPath, @"images\activities");

					using(var fileStream= new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}
					model.Activity.Image = @"\images\activities\" + fileName + extention;
				}
				await _unitOfWork.ActivittyRepository.CreateAsync(model.Activity.Map<Activitty>());
				//if(await _unitOfWork.SaveAsync())
				//return RedirectToAction(nameof(Index));
			}
			var periodList = await _unitOfWork.FromToPeriodRepository.GetAllAync();
			var instructors = await _unitOfWork.InstructorRepository.GetAllAync();

			model.FromToPeriodList = periodList.Select(x => new SelectListItem
			{
				Text = x.Period,
				Value = x.Id.ToString()
			});
			model.InstructorList = instructors.Select(x => new SelectListItem
			{
				Text = x.FulName,
				Value = x.Id.ToString()
			});
			
			
			return View(model);
		}

	}
}
