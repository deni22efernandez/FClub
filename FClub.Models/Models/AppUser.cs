using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models
{
	public class AppUser:Person
	{
		public string EnrollmentDate { get; set; }
		public ICollection<Enrollment> Enrollments { get; set; }
	}
}
