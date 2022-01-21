using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.Paggination
{
	public class PageInfo
	{
		public int TotalIems { get; set; }
		public int TotalPages => (int)Math.Ceiling((decimal)TotalIems / ItemsPerPage);
		public int ItemsPerPage { get; set; }
		public string Uri { get; set; }
		public int CurrentPage { get; set; }
	}
}
