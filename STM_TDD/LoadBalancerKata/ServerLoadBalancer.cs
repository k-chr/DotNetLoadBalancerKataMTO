using System.Collections.Generic;
using System.Linq;

namespace LoadBalancerKata
{
	public class ServerLoadBalancer
	{
		public void Balance(ICollection<Server> servers, ICollection<Vm> vms)
		{
			if (!vms.Any()) return;
			foreach (var vm in vms)
			{
				AddVmToServerIfPossible(servers, vm);
			}
		}

		private static void AddVmToServerIfPossible(ICollection<Server> servers, Vm vm)
		{
			var filteredServers = GetServersWithEnoughLoadRateToFitVm(servers, vm);
			var server = GetServerOfMinimumLoadRate(filteredServers);
			server?.AddVm(vm);
		}

		private static List<Server> GetServersWithEnoughLoadRateToFitVm(ICollection<Server> servers, Vm vm) =>
			servers.Where(server1 => server1.CanAddVm(vm)).ToList();

		private static Server GetServerOfMinimumLoadRate(ICollection<Server> servers)
		{
			if (!servers.Any()) return null;
			var min = servers.Min(s => s.CurrentLoadPercentage);
			var server = servers.First(s => s.CurrentLoadPercentage <= min);
			return server;
		}
	}
}