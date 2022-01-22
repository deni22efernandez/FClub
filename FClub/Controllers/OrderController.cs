using Braintree;
using FClub.BraintreeConfig;
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
		private readonly IBrainTreeGate _brainTreeGate;
		public OrderController(IUnitOfWork unitOfWork, IBrainTreeGate brainTreeGate)
		{
			_unitOfWork = unitOfWork;
			_brainTreeGate = brainTreeGate;
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
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			OrderDetailsVM orderDetailsVM = new OrderDetailsVM
			{
				OrderHeader = await _unitOfWork.OrderHeaderRepository.GetAsync(x => x.Id == id, includeProperties: "AppUser"),
				OrderDetails = await _unitOfWork.OrderDetailRepository.GetAllAync(x => x.OrderHeaderId == id, includeProperties:"Activitty")
			};
			return View(orderDetailsVM);
		}
		[HttpGet]
		public async Task<IActionResult> StartProcessing(int id)
		{
			//change status to processing
			var order = await _unitOfWork.OrderHeaderRepository.GetAsync(x => x.Id == id);
			if (order != null && order.OrderStatus == "Approved")
			{
				order.OrderStatus = "Processing";
				await _unitOfWork.SaveAsync();
			}
			return RedirectToAction(nameof(Index));
		}
		[HttpGet]
		public async Task<IActionResult> Cancel(int id)
		{
			//change status to refunded/cancelled
			var order = await _unitOfWork.OrderHeaderRepository.GetAsync(x => x.Id == id);
			if (order != null)
			{
				var gateway = _brainTreeGate.GetGateWay();
				Transaction transaction = gateway.Transaction.Find(order.TransactionId);
				if (transaction.Status == TransactionStatus.AUTHORIZED ||
				   transaction.Status == TransactionStatus.SUBMITTED_FOR_SETTLEMENT)
				{
					//no refund
					Result<Transaction> result = gateway.Transaction.Void(order.TransactionId);
				}
				else
				{
					//refund
					Result<Transaction> result = gateway.Transaction.Refund(order.TransactionId);
				}
				order.OrderStatus = "Refunded";
				await _unitOfWork.SaveAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
