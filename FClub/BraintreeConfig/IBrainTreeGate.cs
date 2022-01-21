using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.BraintreeConfig
{
	public interface IBrainTreeGate
	{
		IBraintreeGateway CreateGateWay();
		IBraintreeGateway GetGateWay();
	}
}
