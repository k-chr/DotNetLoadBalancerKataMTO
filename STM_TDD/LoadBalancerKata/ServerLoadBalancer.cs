using System.Collections.Generic;
using System.Linq;

namespace LoadBalancerKata
{
	public class ServerLoadBalancer
	{
		private const double MaxPercentage = 100.0;

		public void Balance(ICollection<Server> servers, ICollection<Vm> vms)
		{
			if (vms.Any())
			{
				foreach (var server in servers)
				{
					foreach (var vm in vms)
					{
						server.CurrentLoadPercentage += (vm.Size / server.Capacity) * MaxPercentage;
						server.AddVm(vm);
					}
				}
			}
		}
	}
}