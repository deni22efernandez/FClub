using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FClub.Models.Models.ViewModels
{
	public class RegisterVM
	{
		[Required]
		public  string Username { get; set; }
		[Required]
		public  string Password { get; set; }
		[Required]
		[Display(Name = "ConfirmPassword")]
		[Compare("Password", ErrorMessage ="Password dont match")]
		public string ConfirmPassword { get; set; }
	}
}
