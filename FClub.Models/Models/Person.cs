using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FClub.Models.Models
{
	public abstract class Person
	{
		public abstract int Id { get; set; }
		public abstract string Name { get; set; }
		public abstract string LastName { get; set; }
		public  string FulName { get { return this.Name + ", " + this.LastName; } }
		public abstract string Email { get; set; }
		public abstract string Mobile { get; set; }
		public abstract string Address { get; set; }
		[Required]
		public abstract string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public abstract string Password { get; set; }
	}
}
