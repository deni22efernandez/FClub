using FClub.Models.Models.DTOs;
using FClub.Models.Models.DTOs.AppUserDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.ViewModels
{
	public class SummaryVM
	{
		public AppUserDto AppUser { get; set; }
		public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
	}
}
