using System.Collections.Generic;
using LoadBalancerKata;
using NHamcrest;
using NHamcrest.Core;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;
namespace LoadBalancerKataTests
{
	public class ServerLoadBalancerTest
	{
		[Fact]
		public void ItCompiles() => Assert.That(true, Is.True());

		[Fact]
		public void ServerShouldStayEmptyIfHadNoVMsDuringBalancing()
		{
			Server server = A(Server().WithCapacity(1));

			Balance(AListOfServersWith(server), AnEmptyListOfVMs());
			Assert.That(server, HasLoadPercentageOf(0.0d));
		}

		private void Balance(IEnumerable<Server> servers, IEnumerable<Vm> vms) => new ServerLoadBalancer().Balance(servers, vms);
		
		private IEnumerable<Server> AListOfServersWith(params Server[] values) => values;
		
		private IEnumerable<Vm> AnEmptyListOfVMs() => new List<Vm>();
		
		private Matcher<Server> HasLoadPercentageOf(double value) => new CurrentLoadPercentageMatcher(value);

		public static ServerBuilder Server() => new ServerBuilder();

		private Server A(ServerBuilder builder) => builder.Build();
	}
}
