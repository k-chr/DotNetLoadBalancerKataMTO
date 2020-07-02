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
					var filteredServers = servers.Where(server1 => server1.CanAddVm(vm)).ToList();
					var server = GetServerOfMinimumLoadRate(filteredServers);
					server?.AddVm(vm);
				}
			}
		}

		private static Server GetServerOfMinimumLoadRate(ICollection<Server> servers)
		{
			if (!servers.Any()) return null;
			var min = servers.Min(s => s.CurrentLoadPercentage);
			var server = servers.First(s => s.CurrentLoadPercentage <= min);
			return server;
		}
	}
}