using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FClub.Models.Models.DTOs.AppUserDtos
{
	public class AppUserDto
	{
		public  int Id { get; set; }
		public  string Name { get; set; }
		public  string LastName { get; set; }
		public  string Email { get; set; }
		public  string Mobile { get; set; }
		public  string Address { get; set; }
		[Required]
		public  string Username { get; set; }
	}
}
