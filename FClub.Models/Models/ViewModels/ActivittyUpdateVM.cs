using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace FClub.Models.Models.ViewModels
{
	public class ActivittyUpdateVM
	{
		public Activitty Activity { get; set; }
		public List<int> DaysSelected { get; set; }
		public IEnumerable<SelectListItem> InstructorList { get; set; }
		public IEnumerable<SelectListItem> WeekDays { get; set; }
		public IEnumerable<SelectListItem> FromToPeriodList { get; set; }
	}
}
