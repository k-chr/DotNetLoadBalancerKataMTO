using System.Collections.Generic;
using System.Collections.Immutable;

namespace LoadBalancerKata
{
	public class Server
	{
		private readonly List<Vm> _vms;
		public double CurrentLoadPercentage { get; private set; }
		private double Capacity { get; }
		public IImmutableList<Vm> Vms => _vms.ToImmutableList();
		private const double MaxPercentage = 100.0;

		public Server(int capacity)
		{
			Capacity = capacity;
			_vms = new List<Vm>();
		}

		public void AddVm(Vm vm)
		{
			if (vm == null) return;
			CurrentLoadPercentage += VmSize(vm);
			_vms.Add(vm);
		}

		private double VmSize(Vm vm) => (vm.Size / Capacity) * MaxPercentage;

		public bool CanAddVm(Vm vm) => CurrentLoadPercentage + VmSize(vm) <= MaxPercentage;
	}
}