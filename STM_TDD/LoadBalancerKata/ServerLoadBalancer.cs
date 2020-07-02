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
				var min = servers.Min(s => s.CurrentLoadPercentage);
				var server = servers.First(s => s.CurrentLoadPercentage <= min);
				foreach (var vm in vms)
				{
					server.AddVm(vm);
				}
			}
		}
	}
}