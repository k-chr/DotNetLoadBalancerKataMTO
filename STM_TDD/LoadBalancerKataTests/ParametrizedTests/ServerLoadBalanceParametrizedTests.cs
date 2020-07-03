using System.Collections.Generic;
using System.Linq;
using LoadBalancerKata;
using LoadBalancerKataTests.ParametrizedTests.ClassDatas;
using NHamcrest;
using static LoadBalancerKataTests.Builders.ServerBuilder;
using static LoadBalancerKataTests.Builders.VmBuilder;
using static LoadBalancerKataTests.Matchers.CurrentLoadPercentageMatcher;
using static LoadBalancerKataTests.Matchers.ServerVmMatcher;
using static NHamcrest.Is;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;

namespace LoadBalancerKataTests.ParametrizedTests
{
	public class ServerLoadBalanceParametrizedTests : ServerLoadBalancerTestBase
	{
		[Fact]
		public void BalancingOneServerWithOneSlotCapacityAndOneSlotVmFillsTheServerWithTheVm()
		{
			var server = A(Server().WithCapacity(1));
			var vm = A(Vm().WithSize(1));
			Balance(AListOfServersWith(server), AListOfVms(vm));
			Assert.That(server, HasLoadPercentageOf(100.0));
			Assert.That(server.Vms, Has.Item(EqualTo(vm)));
		}

		[Theory]
		[ClassData(typeof(OneServerOneVmTestData))]
		public void
			BalancingOneServerWithSomeCapacityAndVmWithLesserOrEqualSizeThanServerCapacityFillsTheServerWithTheVm(
				Server server, Vm vm, double expectedPercentage)
		{
			Balance(AListOfServersWith(server), AListOfVms(vm));
			Assert.That(server, HasLoadPercentageOf(expectedPercentage));
			Assert.That(server.Vms, Has.Item(EqualTo(vm)));
		}

		[Theory]
		[ClassData(typeof(ManyServersManyVmsTestData))]
		public void BalancingEmptyServersAndSomeListOfVmsShouldResultInNearlyEqualVmDistribution(
			ICollection<Server> servers, ICollection<Vm> vms, List<ICollection<Vm>> expectedVmsList,
			List<double> expectedLoadPercentages)
		{
			Balance(servers, vms);
			var serversList = servers.ToList();
			for (var i = 0; i < serversList.Count; ++i)
			{
				var expectedVms = expectedVmsList[i];
				var expectedPercentage = expectedLoadPercentages[i];
				var server = serversList[i];
				Assert.That(server, HasLoadPercentageOf(expectedPercentage));
				Assert.That(server, HasVmsEqualTo(expectedVms));
			}
		}
	}
}