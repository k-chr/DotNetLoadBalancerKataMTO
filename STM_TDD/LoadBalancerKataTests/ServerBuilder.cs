using LoadBalancerKata;

namespace LoadBalancerKataTests
{
	public class ServerBuilder : IBuilder<Server>
	{
		private int _capacity;

		public ServerBuilder()
		{

		}

		public ServerBuilder WithCapacity(int capacity)
		{
			_capacity = capacity;
			return this;
		}

		public Server Build()
		{
			return new Server(_capacity);
		}

		public static ServerBuilder Server() => new ServerBuilder();
	}
}
