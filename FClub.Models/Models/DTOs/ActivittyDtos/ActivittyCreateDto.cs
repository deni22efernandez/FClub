using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FClub.Models.Models.DTOs.ActivittyDtos
{
	public class ActivittyCreateDto
	{
		[Display(Name = "Activity")]
		public string ActivittyName { get; set; }
		[Display(Name = "Total Capacity")]
		public int TotalCapacity { get; set; }
		[Display(Name = "Current Capacity")]
		public int CurrentCapacity { get; set; }
		[Required]
		public int InstructorId { get; set; }
		[Display(Name = "Days")]
		public ICollection<WeekDays> WeekDays { get; set; }
		[Display(Name = "Price per class")]
		public double PricePerClass { get; set; }
		[Display(Name = "Price per month")]
		public double PricePerMonth { get; set; }
		[Display(Name = "Price per 3 months")]
		public double PricePer3Months { get; set; }
		[Display(Name = "Free pass")]
		public double FreePass { get; set; }
		[Required]
		[Display(Name = "Time period")]
		public int FromToPeriodId { get; set; }
	}
}
