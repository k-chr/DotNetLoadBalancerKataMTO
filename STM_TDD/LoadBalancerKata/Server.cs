using System.Collections.Generic;
using System.Collections.Immutable;

namespace LoadBalancerKata
{
	public class Server
	{
		public double CurrentLoadPercentage { get; private set; }

		public Server(int capacity)
		{
			Capacity = capacity;
			_vms = new List<Vm>();
		}

		private double Capacity { get;}
		private readonly List<Vm> _vms;
		private const double MaxPercentage = 100.0;
		public ImmutableList<Vm> Vms => _vms.ToImmutableList();
		public void Add(Vm vm)
		{
			_vms.Add(vm);
			CurrentLoadPercentage += CostOfVm(vm);
		}

		private double CostOfVm(Vm vm) => (vm.Size / Capacity) * MaxPercentage;
	}
}
