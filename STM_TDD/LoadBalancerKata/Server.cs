namespace LoadBalancerKata
{
	public class Server
	{
		public double CurrentLoadPercentage { get; set; }

		public Server(int capacity)
		{
			Capacity = capacity;
		}

		public double Capacity { get;}
	}
}
