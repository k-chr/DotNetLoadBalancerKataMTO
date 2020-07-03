using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LoadBalancerKata;
using NHamcrest;

namespace LoadBalancerKataTests
{
	internal class ServerVmMatcher : IMatcher<Server>
	{
		private readonly ICollection<Vm> _aListOfVms;

		private ServerVmMatcher(ICollection<Vm> aListOfVms)
		{
			_aListOfVms = aListOfVms;
		}

		public void DescribeTo(IDescription description)
		{
			throw new NotImplementedException();
		}

		public bool Matches(Server item) => _aListOfVms.SequenceEqual(item?.Vms ?? new List<Vm>().ToImmutableList());

		public void DescribeMismatch(Server item, IDescription mismatchDescription)
		{
			throw new NotImplementedException();
		}

		public static IMatcher<Server> HasVmsEqualTo(ICollection<Vm> aListOfVms) => new ServerVmMatcher(aListOfVms);
	}
}