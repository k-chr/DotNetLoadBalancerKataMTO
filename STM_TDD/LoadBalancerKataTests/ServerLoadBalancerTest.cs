using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

		[Fact]
		public void ServerShouldStayNotFullyLoadedIfVMUsesOnlyThePartOfServerResourcesAfterBalancing()
		{
			var server = A(Server().WithCapacity(10));
			var vm = A(Vm().WithSize(5));
			Balance(AListOfServersWith(server), AListOfVms(vm));
			Assert.That(server, HasLoadPercentageOf(50.0));
		}

		[Fact]
		public void ServerShouldFillSomeVmsAfterBalancing()
		{
			var server = A(Server().WithCapacity(20));
			var (vm1, vm2, vm3) = (A(Vm().WithSize(2)), A(Vm().WithSize(3)), A(Vm().WithSize(7)));
			Balance(AListOfServersWith(server), AListOfVms(vm1, vm2, vm3));
			Assert.That(server, HasLoadPercentageOf(60.0));
			Assert.That(server, HasVmsEqualTo(AListOfVms(vm1, vm2, vm3)));
		}

		private IMatcher<Server> HasVmsEqualTo(ICollection<Vm> aListOfVms) => new ServerVmMatcher(aListOfVms);

		private static ICollection<Vm> AListOfVms(params Vm[] vm) => vm;

		private static void Balance(ICollection<Server> servers, ICollection<Vm> vms) => new ServerLoadBalancer().Balance(servers, vms);

		private static ICollection<Server> AListOfServersWith(params Server[] values) => values;

		private static ICollection<Vm> AnEmptyListOfVMs() => new List<Vm>();

		private static T A<T>(IBuilder<T> builder) => builder.Build();
	}

	internal class ServerVmMatcher : IMatcher<Server>
	{
		private readonly ICollection<Vm> _aListOfVms;

		public ServerVmMatcher(ICollection<Vm> aListOfVms)
		{
			_aListOfVms = aListOfVms;
		}

		public void DescribeTo(IDescription description)
		{
			throw new System.NotImplementedException();
		}

		public bool Matches(Server item) => _aListOfVms.SequenceEqual(item?.Vms ?? new List<Vm>());

		public void DescribeMismatch(Server item, IDescription mismatchDescription)
		{
			throw new System.NotImplementedException();
		}
	}
}
