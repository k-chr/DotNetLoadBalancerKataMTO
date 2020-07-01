using System.Collections.Generic;
using System.Collections.Immutable;

namespace LoadBalancerKata
{
	public class Server
	{
		private List<Vm> _vms;
		public double CurrentLoadPercentage { get; set; }

		public Server(int capacity)
		{
			Capacity = capacity;
			_vms = new List<Vm>();
		}

		public void AddVm(Vm machine)
		{
			if(machine == null) return;
			_vms.Add(machine);
		}

		public double Capacity { get;}
		public IImmutableList<Vm> Vms => _vms.ToImmutableList();
	}
}
