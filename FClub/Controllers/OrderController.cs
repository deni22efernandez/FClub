using FClub.Data.Repository.IRepository;
using FClub.Models.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Controllers
{
	//[Authorize(Roles="Admin")]
	public class OrderController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public OrderController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index(int currentPage=1)
		{
			var headers = await _unitOfWork.OrderHeaderRepository.GetAllAync(includeProperties: "AppUser");
			var headersCount = headers.Count();
			OrderIndexVM orderIndexVM = new OrderIndexVM
			{
				OrderHeaders = headers.Skip((currentPage-1)*2).Take(2),
				paginationModel = new Models.Models.PaginationModel
				{
					CurrentPage = currentPage,
					ItemsPerPage = 2,
					TotalIems = headersCount,
					Uri = "/Order/Index?currentPage=:"
				}
			};
			return View(orderIndexVM);
		}
	}
}
