using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models
{
	public class Instructor : Person
	{
		public override int Id { get; set; }
		public override string Name { get; set; }
		public override string LastName { get; set; }
		public override string Email { get; set; }
		public override string Mobile { get; set; }
		public override string Address { get; set; }
		public string About { get; set; }
		public override string Username { get; set; }
		public override string Password { get; set; }
		public string ProfilePicture { get; set; }
		public DateTime HireDate { get; set; }
		public ICollection<Activitty> Activities { get; set; }
	}
}
