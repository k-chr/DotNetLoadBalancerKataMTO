using NHamcrest;
using Xunit;
using Assert = NHamcrest.XUnit.Assert;

namespace LoadBalancerKataTests
{
	public class ServerLoadBalancerTest
	{
		[Fact]
		public void ItCompiles()
		{
			Assert.That(true, Is.True());
		}

		[Fact]
		public void ServerShouldStayEmptyIfHadNoVMsDuringBalancing()
		{
			Server server = A(Server().WithCapacity(1));

			Balance(AListOfServersWith(server), AnEmptyListOfVMs());
			Assert.That(server, HasLoadPercentageOf(0.0d));
		}
	}
}
