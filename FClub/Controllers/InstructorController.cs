using FClub.CustomMapper;
using FClub.Data.Repository.IRepository;
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
	}
}
