using System.Collections.Generic;
using LoadBalancerKata;
using NHamcrest;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;
using static LoadBalancerKataTests.CurrentLoadPercentageMatcher;
using static LoadBalancerKataTests.ServerBuilder;
using static LoadBalancerKataTests.VmBuilder;
using static NHamcrest.Is;
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

		[Fact]
		public void ServerShouldStayNotFullyLoadedIfVMUsesOnlyThePartOfServerResourcesAfterBalancing()
		{
			var server = A(Server().WithCapacity(10));
			var vm = A(Vm().WithSize(5));
			Balance(AListOfServersWith(server), AListOfVms(vm));
			Assert.That(server, HasLoadPercentageOf(50.0));
		}

		[Fact]
		public void ServerWithLargeCapacityShouldContainAllProvidedVMsAfterBalancingAndHaveSpecifiedLoadPercentageRate()
		{
			var server = A(Server().WithCapacity(20));
			var (vm1, vm2, vm3, vm4, vm5) = (A(Vm().WithSize(2)), A(Vm().WithSize(3)), A(Vm().WithSize(1)),
				A(Vm().WithSize(5)), A(Vm().WithSize(4)));
			Balance(AListOfServersWith(server), AListOfVms(vm1, vm2, vm3, vm4, vm5));
			var vms = server.Vms;
			Assert.That(vms, Has.Item(EqualTo(vm1)));
			Assert.That(vms, Has.Item(EqualTo(vm2)));
			Assert.That(vms, Has.Item(EqualTo(vm3)));
			Assert.That(vms, Has.Item(EqualTo(vm4)));
			Assert.That(vms, Has.Item(EqualTo(vm5)));
			Assert.That(server, HasLoadPercentageOf(75.0));
		}

		private static ICollection<Vm> AListOfVms(params Vm[] vm) => vm;

		private static void Balance(ICollection<Server> servers, ICollection<Vm> vms) =>
			new ServerLoadBalancer().Balance(servers, vms);

		private static ICollection<Server> AListOfServersWith(params Server[] values) => values;

		private static ICollection<Vm> AnEmptyListOfVMs() => new List<Vm>();

		private static T A<T>(IBuilder<T> builder) => builder.Build();
	}
}