using System.Collections.Generic;

namespace LoadBalancerKata
{
	public class Server
	{
		public double CurrentLoadPercentage { get; set; }

		public Server(int capacity)
		{
			Capacity = capacity;
			Vms = new List<Vm>();
		}

		public double Capacity { get;}
		public List<Vm> Vms { get; }
	}
}
