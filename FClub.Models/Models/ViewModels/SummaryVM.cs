using FClub.Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.ViewModels
{
	public class SummaryVM
	{
		public AppUser AppUser { get; set; }
		public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
	}
}
