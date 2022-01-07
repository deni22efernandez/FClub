using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models
{
	public class Instructor : Person
	{
		public string About { get; set; }
		public string ProfilePicture { get; set; }
		public DateTime HireDate { get; set; }
		public ICollection<Activitty> Activities { get; set; }
	}
}
