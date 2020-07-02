using System.Collections.Generic;
using LoadBalancerKata;
using NHamcrest;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;
using static LoadBalancerKataTests.CurrentLoadPercentageMatcher;
using static LoadBalancerKataTests.ServerBuilder;
using static LoadBalancerKataTests.VmBuilder;
namespace LoadBalancerKataTests
{
	public class ServerLoadBalancerTest
	{
		[Fact]
		public void ItCompiles() => Assert.That(true, Is.True());

		[Fact]
		public void ServerShouldStayEmptyIfHadNoVMsDuringBalancing()
		{
			var server = A(Server().WithCapacity(1));

			Balance(AListOfServersWith(server), AnEmptyListOfVMs());
			Assert.That(server, HasLoadPercentageOf(0.0d));
		}

		[Fact]
		public void ServerShouldStayFullIfHadCapacityForOnlyOneVMDuringBalancing()
		{
			var server = A(Server().WithCapacity(1));
			var vm = A(Vm().WithSize(1));
			Balance(AListOfServersWith(server), AListOfVms(vm));
			Assert.That(server, StaysFull());
		}

		private static IEnumerable<Vm> AListOfVms(params Vm[] vm) => vm;

		private static void Balance(IEnumerable<Server> servers, IEnumerable<Vm> vms) => new ServerLoadBalancer().Balance(servers, vms);

		private static IEnumerable<Server> AListOfServersWith(params Server[] values) => values;

		private static IEnumerable<Vm> AnEmptyListOfVMs() => new List<Vm>();

		private static T A<T>(IBuilder<T> builder) => builder.Build();
	}
}
