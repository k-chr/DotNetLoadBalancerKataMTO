using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LoadBalancerKata;
using NHamcrest;
using NHamcrest.Core;

namespace LoadBalancerKataTests.Matchers
{
	public class ServerVmMatcher : Matcher<Server>
	{
		private readonly ICollection<Vm> _expectedVmsOfServer;

		private ServerVmMatcher(ICollection<Vm> expectedVmsOfServer) => _expectedVmsOfServer = expectedVmsOfServer;

		public override void DescribeTo(IDescription description) =>
			description.AppendText("Expected collection of virtual machines of server is equal to:\n")
			   .AppendValueList("[", ", ", "]", _expectedVmsOfServer);

		public override void DescribeMismatch(Server item, IDescription mismatchDescription) =>
			mismatchDescription.AppendText("This server stores a collection of virtual machines equal to:\n")
			   .AppendValueList("[", ", ", "]", item.Vms);

		public override bool Matches(Server item) =>
			_expectedVmsOfServer.SequenceEqual(item?.Vms ?? new List<Vm>().ToImmutableList());

		public static IMatcher<Server> HasVmsEqualTo(ICollection<Vm> expected) =>
			new ServerVmMatcher(expected);
	}
}