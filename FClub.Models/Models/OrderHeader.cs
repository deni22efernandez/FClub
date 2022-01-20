using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FClub.Models.Models
{
	public class OrderHeader
	{
		public int Id { get; set; }
		[Required]
		public int AppUserId { get; set; }
		[ForeignKey("AppUserId")]
		public AppUser AppUser { get; set; }
		[Required]
		public DateTime OrderDate { get; set; }
		[Required]
		public int OrderTotal { get; set; }
		public string OrderStatus { get; set; }
		public string PaymentStatus { get; set; }
		public string TransactionId { get; set; }
		public  string Email { get; set; }
		public  string Mobile { get; set; }
		public  string Address { get; set; }
		//public string PostalCode { get; set; }
		
	}
}
