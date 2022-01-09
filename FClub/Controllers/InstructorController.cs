using FClub.CustomMapper;
using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using FClub.Models.Models.DTOs.InstructorDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	public class InstructorController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public InstructorController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var instructor = await _unitOfWork.InstructorRepository.GetAllAync();
			return View(instructor.Map<IEnumerable<InstructorGetDto>>());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View(new InstructorCreateDto());
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Create")]
		public async Task<IActionResult> CreateAsync(InstructorCreateDto dto)
		{
			if (ModelState.IsValid)
			{
				await _unitOfWork.InstructorRepository.CreateAsync(dto.Map<Instructor>());
				if (await _unitOfWork.SaveAsync())
					return RedirectToAction(nameof(Index));
				else
					BadRequest();
			}
			return View(dto);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var instructor = await _unitOfWork.InstructorRepository.GetAync(x => x.Id == id);
			if (instructor != null)
				return View(instructor.Map<InstructorUpdateDto>());
			return NotFound();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateAsync(InstructorUpdateDto dto)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.InstructorRepository.Update(dto.Map<Instructor>());
				if (await _unitOfWork.SaveAsync())
					return RedirectToAction(nameof(Index));
				else
					BadRequest();
			}
			return View(dto);
		}
	}
}
