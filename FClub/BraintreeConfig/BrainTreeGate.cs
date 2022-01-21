using Braintree;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FClub.BraintreeConfig
{
	public class BrainTreeGate : IBrainTreeGate
	{
		private readonly BrainTreeConfig _brainTree;
		private IBraintreeGateway _gateway;
		public BrainTreeGate(IOptions<BrainTreeConfig> brainTree)
		{
			_brainTree = brainTree.Value;
		}

		public IBraintreeGateway CreateGateWay()
		{
			_gateway = new BraintreeGateway(_brainTree.Environment, _brainTree.MerchantId, _brainTree.PublicKey, _brainTree.PrivateKey);
			return _gateway;
		}

		public IBraintreeGateway GetGateWay()
		{
			if (_gateway == null)
			{
				return CreateGateWay();
			}
			return _gateway;
		}

		
	}
}
