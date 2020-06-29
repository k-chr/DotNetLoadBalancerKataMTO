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

		private IEnumerable<Vm> AListOfVms(params Vm[] vm) => vm;

		private void Balance(IEnumerable<Server> servers, IEnumerable<Vm> vms) => new ServerLoadBalancer().Balance(servers, vms);

		private IEnumerable<Server> AListOfServersWith(params Server[] values) => values;

		private IEnumerable<Vm> AnEmptyListOfVMs() => new List<Vm>();

		private Server A(ServerBuilder builder) => builder.Build();

		private Vm A(VmBuilder builder) => builder.Build();
	}

	internal class VmBuilder
	{
		private int _size;
		public Vm Build()
		{
			return new Vm();
		}

		public static VmBuilder Vm()
		{
			return new VmBuilder();
		}

		public VmBuilder WithSize(int size)
		{
			_size = size;
			return this;
		}
	}


}
