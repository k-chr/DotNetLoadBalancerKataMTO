using NHamcrest;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;
using static LoadBalancerKataTests.Matchers.CurrentLoadPercentageMatcher;
using static LoadBalancerKataTests.Matchers.ServerVmMatcher;
using static LoadBalancerKataTests.Builders.ServerBuilder;
using static LoadBalancerKataTests.Builders.VmBuilder;
using static NHamcrest.Is;

namespace LoadBalancerKataTests
{
	public class ServerLoadBalancerTest : ServerLoadBalancerTestBase
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

		[Fact]
		public void
			IfTwoServersAreProvidedAndOnlyOneMachineIsAvailableToAssignTheServerWithTheLesserLoadRateShouldReceiveVm()
		{
			var serverWithSomeVm = A(Server().WithCapacity(10).Having(A(Vm().WithSize(2))));
			var emptyServer = A(Server().WithCapacity(10));
			var vmToBalance = A(Vm().WithSize(5));
			Balance(AListOfServersWith(serverWithSomeVm, emptyServer), AListOfVms(vmToBalance));
			Assert.That(serverWithSomeVm.Vms, Not(Has.Item(EqualTo(vmToBalance))));
			Assert.That(emptyServer.Vms, Has.Item(EqualTo(vmToBalance)));
			Assert.That(emptyServer, HasLoadPercentageOf(50.0));
			Assert.That(serverWithSomeVm, HasLoadPercentageOf(20.0));
		}

		[Fact]
		public void IfServerHasNotEnoughPlaceToFitAnotherVmItShouldHaveTheSameLoadRateAfterBalancing()
		{
			var serverWithSomeLargeVm = A(Server().WithCapacity(10).Having(A(Vm().WithSize(9))));
			var tooLargeVm = A(Vm().WithSize(5));
			Balance(AListOfServersWith(serverWithSomeLargeVm), AListOfVms(tooLargeVm));
			Assert.That(serverWithSomeLargeVm.Vms, Not(Has.Item(EqualTo(tooLargeVm))));
			Assert.That(serverWithSomeLargeVm, HasLoadPercentageOf(90.0));
		}

		[Fact]
		public void BalancingEmptyServersAndSomeListOfVmsShouldResultInNearlyEqualVmDistribution()
		{
			var firstServer = A(Server().WithCapacity(8));
			var secondServer = A(Server().WithCapacity(12));
			var (vm1, vm2, vm3, vm4, vm5) = (A(Vm().WithSize(3)), A(Vm().WithSize(2)), A(Vm().WithSize(1)),
				A(Vm().WithSize(5)), A(Vm().WithSize(4)));
			Balance(AListOfServersWith(firstServer, secondServer), AListOfVms(vm1, vm2, vm3, vm4, vm5));
			Assert.That(firstServer, HasLoadPercentageOf(87.5));
			Assert.That(secondServer, HasLoadPercentageOf(66.6666));
			var expectedVmsOfFirstServer = AListOfVms(vm1, vm5);
			var expectedVmsOfSecondServer = AListOfVms(vm2, vm3, vm4);

			Assert.That(firstServer, HasVmsEqualTo(expectedVmsOfFirstServer));
			Assert.That(secondServer, HasVmsEqualTo(expectedVmsOfSecondServer));
		}
	}
}