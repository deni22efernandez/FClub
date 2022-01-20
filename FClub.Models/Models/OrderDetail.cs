using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FClub.Models.Models
{
	public class OrderDetail
	{
		public int Id { get; set; }
		[Required]
		public int OrderHeaderId { get; set; }
		[ForeignKey("OrderHeaderId")]
		public OrderHeader OrderHeader { get; set; }
		[Required]
		public int ActivittyId { get; set; }
		[ForeignKey("ActivittyId")]
		public Activitty Activitty { get; set; }
		public int Price { get; set; }
	}
}
