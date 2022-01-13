using FClub.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FClub.Models
{
	public class ActivittyDays
	{
		public int Id { get; set; }
		[Required]
		public int ActivittyId { get; set; }
		[ForeignKey("ActivittyId")]
		public Activitty Activitty { get; set; }
		[Required]
		public int WeekDayId { get; set; }
		[ForeignKey("WeekDayId")]
		public WeekDays WeekDay { get; set; }
	}
}
