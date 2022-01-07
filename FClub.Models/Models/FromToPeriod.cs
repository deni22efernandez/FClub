using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FClub.Models.Models
{
	public class FromToPeriod
	{
		public int Id { get; set; }
		public string Period { get; set; }
		[Required]
		public int ShiftId { get; set; }
		[ForeignKey("ShiftId")]
		public Shift Shift { get; set; }
	}
}
