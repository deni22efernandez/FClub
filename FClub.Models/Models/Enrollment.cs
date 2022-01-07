using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FClub.Models.Models
{
	public class Enrollment
	{
		public int Id { get; set; }
		
		[Required]
		public int ActivittyId { get; set; }
		[ForeignKey("ActivittyId")]
		public Activitty Activitty { get; set; }
		[Required]
		public int AppUserId { get; set; }
		[ForeignKey("AppUserId")]
		public AppUser AppUser { get; set; }
		
	}
}
