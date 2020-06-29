using System.Collections.Generic;
using System.Linq;

namespace LoadBalancerKata
{
	public class ServerLoadBalancer
	{
		public void Balance(ICollection<Server> servers, ICollection<Vm> vms)
		{
			if (vms.Any())
			{
				foreach (var server in servers)
				{
					server.CurrentLoadPercentage = (vms.First().Size/server.Capacity) * 100.0;
				}
			}
		}
	}
}
