using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.DTOs.InstructorDtos
{
	public class InstructorGetDto
	{
		public  int Id { get; set; }
		public  string Name { get; set; }
		public string LastName { get; set; }
		public string ProfilePicture { get; set; }
		public DateTime HireDate { get; set; }
		public ICollection<Activitty> Activities { get; set; }
	}
}
