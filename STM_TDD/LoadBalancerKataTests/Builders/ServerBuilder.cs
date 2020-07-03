using LoadBalancerKata;

namespace LoadBalancerKataTests.Builders
{
	public class ServerBuilder : IBuilder<Server>
	{
		private int _capacity;
		private Vm _vm;

		private ServerBuilder()
		{
		}

		public ServerBuilder WithCapacity(int capacity)
		{
			_capacity = capacity;
			return this;
		}

		public Server Build()
		{
			var server = new Server(_capacity);
			if (_vm != null)
			{
				server.AddVm(_vm);
			}

			return server;
		}

		public static ServerBuilder Server() => new ServerBuilder();

		public ServerBuilder Having(Vm vm)
		{
			_vm = vm;
			return this;
		}
	}
}