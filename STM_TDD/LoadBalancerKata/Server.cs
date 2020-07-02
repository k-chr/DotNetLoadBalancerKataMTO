namespace LoadBalancerKata
{
	public class Server
	{
		public double CurrentLoadPercentage;

		public Server(int capacity)
		{
			Capacity = capacity;
		}

		public double Capacity { get;}
	}
}
