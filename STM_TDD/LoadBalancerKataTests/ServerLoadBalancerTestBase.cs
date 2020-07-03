using System.Collections.Generic;
using LoadBalancerKata;
using LoadBalancerKataTests.Builders;

namespace LoadBalancerKataTests
{
	public class ServerLoadBalancerTestBase
	{
		protected static ICollection<Vm> AListOfVms(params Vm[] vm) => vm;

		protected static void Balance(ICollection<Server> servers, ICollection<Vm> vms) =>
			new ServerLoadBalancer().Balance(servers, vms);

		protected static ICollection<Server> AListOfServersWith(params Server[] values) => values;
		protected static ICollection<Vm> AnEmptyListOfVMs() => new List<Vm>();
		public static T A<T>(IBuilder<T> builder) => builder.Build();
	}
}