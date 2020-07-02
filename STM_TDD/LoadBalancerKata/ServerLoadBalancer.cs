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
				foreach (var vm in vms)
				{
					var server = GetServerOfMinimumLoadRate(servers);
					server.AddVm(vm);
				}
			}
		}

		private static Server GetServerOfMinimumLoadRate(ICollection<Server> servers)
		{
			var min = servers.Min(s => s.CurrentLoadPercentage);
			var server = servers.First(s => s.CurrentLoadPercentage <= min);
			return server;
		}
	}
}