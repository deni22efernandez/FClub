using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.ViewModels
{
	public class OrderIndexVM
	{
		public IEnumerable<OrderHeader> OrderHeaders { get; set; }
		public PaginationModel paginationModel { get; set; }
	}
}
