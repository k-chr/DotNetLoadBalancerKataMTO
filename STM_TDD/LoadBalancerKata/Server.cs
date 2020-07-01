using System.Collections.Generic;
using System.Collections.Immutable;

namespace LoadBalancerKata
{
	public class Server
	{
		private readonly List<Vm> _vms;
		public double CurrentLoadPercentage { get; private set; }

		public Server(int capacity)
		{
			Capacity = capacity;
			_vms = new List<Vm>();
		}

		private double Capacity { get; }
		public IImmutableList<Vm> Vms => _vms.ToImmutableList();
		private const double MaxPercentage = 100.0;

		public void AddVm(Vm vm)
		{
			if (vm == null) return;
			CurrentLoadPercentage += (vm.Size / Capacity) * MaxPercentage;
			_vms.Add(vm);
		}
	}
}