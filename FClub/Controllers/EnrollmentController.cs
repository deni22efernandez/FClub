using FClub.Data.Repository.IRepository;
using FClub.Models.Models;
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
			var order = await _unitOfWork.OrderHeaderRepository.GetAsync(x => x.Id == id);
			if (order != null)
			{
				var activitties = await _unitOfWork.OrderDetailRepository.GetAllAync(x => x.OrderHeaderId == order.Id, includeProperties:"Activitty");
				var addDays = 0;
				foreach (var item in activitties)
				{
				    if (item.Price == item.Activitty.PricePerMonth)
					{
						addDays = 1;
					}
					else if (item.Price == item.Activitty.PricePer3Months)
					{
						addDays = 2;
					}
					else
						addDays = 3;

					Enrollment enrollment = new Enrollment
					{
						AppUserId = order.AppUserId,
						ActivittyId = item.ActivittyId,
						StartDate=DateTime.Now
					};

					switch (addDays)
					{
						case 1: enrollment.EndDate = DateTime.Today.AddMonths(1);
							break;
						case 2:
							enrollment.EndDate = DateTime.Today.AddMonths(3);
							break;
						case 3:
							enrollment.EndDate = DateTime.Today.AddYears(1);
							break;
					}
					await _unitOfWork.EnrollmentRepository.CreateAsync(enrollment);
					//update capacity of related actvity
					item.Activitty.CurrentCapacity -= 1;
				}
				order.OrderStatus = "Enrolled";
				await _unitOfWork.SaveAsync();
			}
			return View();
		}
	}
}
