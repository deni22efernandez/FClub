using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.DTOs.InstructorDtos
{
	public class InstructorUpdateDto
	{
		public  int Id { get; set; }
		public  string Name { get; set; }
		public  string LastName { get; set; }
		public  string Email { get; set; }
		public  string Mobile { get; set; }
		public  string Address { get; set; }
		public string About { get; set; }
		public  string Username { get; set; }
		public  string Password { get; set; }
		public string ProfilePicture { get; set; }
		public DateTime HireDate { get; set; }
	}
}
