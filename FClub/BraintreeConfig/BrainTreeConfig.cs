using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.BraintreeConfig
{
	public class BrainTreeConfig
	{
		public string PrivateKey { get; set; }
		public string PublicKey { get; set; }
		public string Environment { get; set; }
		public string MerchantId { get; set; }
	}
}
