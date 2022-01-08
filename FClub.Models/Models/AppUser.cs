using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models
{
	public class AppUser:Person
	{
		public override string Username { get; set; }
		public override string Password { get; set; }
		public string EnrollmentDate { get; set; }
		public ICollection<Enrollment> Enrollments { get; set; }
	}
}
