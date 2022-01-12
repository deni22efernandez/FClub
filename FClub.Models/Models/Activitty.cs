using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FClub.Models.Models
{
	public class Activitty
	{
		public int Id { get; set; }
		[Display(Name ="Activity")]
		public string ActivittyName { get; set; }
		[Display(Name = "Total Capacity")]
		public int TotalCapacity { get; set; }
		[Display(Name = "Current Capacity")]
		public int CurrentCapacity { get; set; }		
		[Required]
		public int InstructorId { get; set; }
		[ForeignKey("InstructorId")]
		[Display(Name = "Instructor name")]
		public Instructor Instructor { get; set; }
		[Display(Name = "Days")]
		public WeekDays WeekDays { get; set; }
		[Display(Name = "Price per class")]
		public double PricePerClass { get; set; }
		[Display(Name = "Price per month")]
		public double PricePerMonth { get; set; }
		[Display(Name = "Price per 3 months")]
		public double PricePer3Months { get; set; }
		[Display(Name = "Free pass")]
		public double FreePass { get; set; }
		[Required]
		public int FromToPeriodId { get; set; }
		[ForeignKey("FromToPeriodId")]
		[Display(Name = "Time period")]
		public FromToPeriod FromToPeriod { get; set; }
		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}
	public enum WeekDays
	{
		Monday,Tuesday,Wednesday,Thursday,Friday
	}
}
