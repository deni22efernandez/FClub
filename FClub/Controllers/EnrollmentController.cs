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
	public class EnrollmentController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public EnrollmentController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> Create(int id)
		{
			//id=orderId...create enrollment for user in act from orderDetails
			return View();
		}
	}
}
