using FClub.Models.Models.DTOs.ActivittyDtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FClub.Models.Models.ViewModels
{
	public class ActivittyCreateVM
	{
		public ActivittyCreateDto Activity { get; set; }
		public IEnumerable<SelectListItem> InstructorList { get; set; }
		public IEnumerable<SelectListItem> WeekDays { get; set; }
		public IEnumerable<SelectListItem> FromToPeriodList { get; set; }
	}
}
