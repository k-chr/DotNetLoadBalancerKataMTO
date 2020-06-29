using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LoadBalancerKata
{
	public class ServerLoadBalancer
	{
		public void Balance(IEnumerable<Server> servers, IEnumerable<Vm> vms)
		{
			if (vms.Any())
			{
				foreach (var server in servers)
				{
					server.CurrentLoadPercentage = 100.0;
				}
			}
		}
	}
}
