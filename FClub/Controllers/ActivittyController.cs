using FClub.CustomMapper;
using FClub.Data.Repository.IRepository;
using FClub.Models;
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
			var days = await _unitOfWork.WeekDaysRepository.GetAllAync();

			ActivittyUpsertVM model = new ActivittyUpsertVM
			{
				Activity = new ActivittyUpsertDto(),
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
				WeekDays = days.Select(x => new SelectListItem
				{
					Text = x.WeekDay,
					Value = x.Id.ToString()
				})			

		};
			return View(model);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ActivittyUpsertVM model)
		{
			if (ModelState.IsValid)
			{
				var files = HttpContext.Request.Form.Files;
				if (files.Count>0)
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
				model.Activity.CurrentCapacity = model.Activity.TotalCapacity;
				var actCreated = model.Activity.Map<Activitty>();
				await _unitOfWork.ActivittyRepository.CreateAsync(actCreated);
				if (await _unitOfWork.SaveAsync())
				{
					foreach (var item in model.Activity.WeekDays)
					{
						ActivittyDays activittyDays = new ActivittyDays
						{
							ActivittyId = actCreated.Id,
							WeekDayId = item

						};
						await _unitOfWork.ActivittyDaysRepository.CreateAsync(activittyDays);
					}
					
					if (await _unitOfWork.SaveAsync())
						return RedirectToAction(nameof(Index));
				}
					
			}
			var periodList = await _unitOfWork.FromToPeriodRepository.GetAllAync();
			var instructors = await _unitOfWork.InstructorRepository.GetAllAync();
			var days = await _unitOfWork.WeekDaysRepository.GetAllAync();
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
			model.WeekDays = days.Select(x => new SelectListItem
			{
				Text = x.WeekDay,
				Value = x.Id.ToString()
			});

			return View(model);
		}

		//[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var daysSelected = await _unitOfWork.ActivittyDaysRepository.GetAllAync(x => x.ActivittyId == id);
			var selected = daysSelected.Select(x => x.WeekDayId).ToList();
			var periods = await _unitOfWork.FromToPeriodRepository.GetAllAync();
			var instructors = await _unitOfWork.InstructorRepository.GetAllAync();
			var weekdays = await _unitOfWork.WeekDaysRepository.GetAllAync();

			ActivittyUpdateVM model = new ActivittyUpdateVM
			{
				Activity = await _unitOfWork.ActivittyRepository.GetAsync(x => x.Id == id, includeProperties: null),
				DaysSelected=selected,
				FromToPeriodList = periods.Select(x => new SelectListItem
				{
					Text = x.Period,
					Value = x.Id.ToString()
				}),
				InstructorList = instructors.Select(x => new SelectListItem
				{
					Text = x.FulName,
					Value = x.Id.ToString()
				}),
				WeekDays = weekdays.Select(x => new SelectListItem
				{
					Text = x.WeekDay,
					Value = x.Id.ToString(),
					Selected=selected.Contains(x.Id)//no los selecciona en la UI
				})
			};
			return View(model);
		}

		//[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(ActivittyUpdateVM model)
		{
			if (ModelState.IsValid)
			{
				var files = HttpContext.Request.Form.Files;
				if (files.Count>0)
				{
					var webRootPath = _hostEnv.WebRootPath;
					var oldImg = model.Activity.Image;
					
					if (oldImg != null)
					{						
						var oldImgPath = Path.Combine(webRootPath,oldImg.TrimStart('\\'));//
						if (System.IO.File.Exists(oldImgPath))
						{
							System.IO.File.Delete(oldImgPath);
						}
					}
					var ImgName = Guid.NewGuid().ToString();
					var extention = Path.GetExtension(files[0].FileName);
					var uploads = Path.Combine(webRootPath,@"images\activities");				

					using(var fileStream = new FileStream(Path.Combine(uploads,ImgName+extention), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}

					model.Activity.Image = @"\images\activities\" + ImgName + extention;//
				}
				List<ActivittyDays> activitties = new List<ActivittyDays>();
				foreach (var item in model.DaysSelected)
				{
					ActivittyDays activittyDays = new ActivittyDays
					{
						ActivittyId = model.Activity.Id,
						WeekDayId = item
					};
					activitties.Add(activittyDays);
					
				}
				await _unitOfWork.ActivittyDaysRepository.UpdateAsync(model.Activity.Id, activitties);
				_unitOfWork.ActivittyRepository.Update(model.Activity);
				if(await _unitOfWork.SaveAsync())
				{
					return RedirectToAction(nameof(Index));
				}
				//else tempadata error
			}
			var periods = await _unitOfWork.FromToPeriodRepository.GetAllAync();
			var instructors = await _unitOfWork.InstructorRepository.GetAllAync();
			var weekdays = await _unitOfWork.WeekDaysRepository.GetAllAync();
			model.FromToPeriodList = periods.Select(x => new SelectListItem
			{
				Text = x.Period,
				Value = x.Id.ToString()
			});
			model.InstructorList = instructors.Select(x => new SelectListItem
			{
				Text = x.FulName,
				Value = x.Id.ToString()
			});
			model.WeekDays = weekdays.Select(x => new SelectListItem
			{
				Text = x.WeekDay,
				Value = x.Id.ToString()
			});
			return View(model);
		}

	}
}
