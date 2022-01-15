using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FClub.Models.Models
{
	public class AppUser : Person
	{
		public override int Id { get; set; }
		public override string Name { get; set; }
		public override string LastName { get; set; }
		public override string Email { get; set; }
		public override string Mobile { get; set; }
		public override string Address { get; set; }
		[Required]
		public override string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public override string Password { get; set; }
		public string EnrollmentDate { get; set; }
		public ICollection<Enrollment> Enrollments { get; set; }
		
	
	}
}
