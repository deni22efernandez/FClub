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
		
		public string ActivittyName { get; set; }
		
		public int TotalCapacity { get; set; }
		
		public int CurrentCapacity { get; set; }		
		[Required]
		public int InstructorId { get; set; }
		[ForeignKey("InstructorId")]
		public Instructor Instructor { get; set; }
	
		public WeekDays WeekDays { get; set; }
	
		public double PricePerClass { get; set; }
	
		public double PricePerMonth { get; set; }
	
		public double PricePer3Months { get; set; }

		public double FreePass { get; set; }
		[Required]
		public int FromToPeriodId { get; set; }
		[ForeignKey("FromToPeriodId")]
		
		public FromToPeriod FromToPeriod { get; set; }
		public virtual ICollection<Enrollment> Enrollments { get; set; }
	}
	public enum WeekDays
	{
		Monday,Tuesday,Wednesday,Thursday,Friday
	}
}
