using FClub.CustomMapper;
using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
using FClub.Models.Models.DTOs.InstructorDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	//[Authorize(Roles ="Admin")]
	public class InstructorController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private IWebHostEnvironment _hostEnvironment;
		public InstructorController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_hostEnvironment = hostEnvironment;
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
				var files = HttpContext.Request.Form.Files;				

				if (files != null)//upload picture
				{
					string fileName = Guid.NewGuid().ToString();
					var webRoot = _hostEnvironment.WebRootPath;
					string uploads = Path.Combine(webRoot, @"images\profile");
					var extention = Path.GetExtension(files[0].FileName);

					using(FileStream fileStream = new FileStream(Path.Combine(uploads,fileName+extention), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}
					dto.ProfilePicture = @"\images\profile\" + fileName + extention;
				}
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
				var files = HttpContext.Request.Form.Files;
				if (files != null)
				{
					string fileName = Guid.NewGuid().ToString();
					var extention = Path.GetExtension(files[0].FileName);
					var webRootPath = _hostEnvironment.WebRootPath;
					var uploads = Path.Combine(webRootPath, @"images\profile");

					var oldPic = Path.Combine(webRootPath, dto.ProfilePicture.TrimStart('\\'));
					if (oldPic!=null)
					{
						if (System.IO.File.Exists(oldPic))
							System.IO.File.Delete(oldPic);
					}
					using(var fileStream = new FileStream(Path.Combine(uploads, fileName+extention), FileMode.Create))
					{
						files[0].CopyTo(fileStream);
					}
					dto.ProfilePicture = @"\images\profile\" + fileName + extention;
				}
				_unitOfWork.InstructorRepository.Update(dto.Map<Instructor>());
				if (await _unitOfWork.SaveAsync())
					return RedirectToAction(nameof(Index));
				else
					BadRequest();
			}
			return View(dto);
		}
		#region		
		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var toDelete = await _unitOfWork.InstructorRepository.GetAync(x => x.Id == id);
			if (toDelete != null)
			{
				//elimnar picture
				await _unitOfWork.InstructorRepository.DeleteAsync(toDelete);
				if(await _unitOfWork.SaveAsync())
				{
					return new JsonResult("Delete successfull");
				}
				return new JsonResult("Delete went wrong");
			}
			return NotFound();
		}
		#endregion

	}
}
