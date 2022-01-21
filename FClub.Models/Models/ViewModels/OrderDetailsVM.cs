using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.ViewModels
{
	public class OrderDetailsVM
	{
		public OrderHeader OrderHeader { get; set; }
		public IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}
