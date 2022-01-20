using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.DTOs
{
	public class ShoppingCart
	{
		public Activitty Activity { get; set; }
		public int ActivityId { get; set; }
		public int PriceSelected { get; set; }
	}
}
