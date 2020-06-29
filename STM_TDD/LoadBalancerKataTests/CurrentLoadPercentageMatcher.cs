using System;
using LoadBalancerKata;
using NHamcrest;
using NHamcrest.Core;

namespace LoadBalancerKataTests
{
	public class CurrentLoadPercentageMatcher : Matcher<Server>
	{
		private const double Epsilon = 0.001d;
		private readonly double _expectedLoadPercentage;
		private readonly bool _fullMatch;

		public CurrentLoadPercentageMatcher(double expectedLoadPercentage) =>
			_expectedLoadPercentage = expectedLoadPercentage;

		private CurrentLoadPercentageMatcher(bool full)
		{
			_fullMatch = full;
		}

		public override void DescribeTo(IDescription description) =>
			description.AppendText("This server has a load percentage that equals: ")
			   .AppendValue(_expectedLoadPercentage);

		public override void DescribeMismatch(Server item, IDescription mismatchDescription) =>
			mismatchDescription.AppendText("This server has a load percentage that equals: ")
			   .AppendValue(item.CurrentLoadPercentage);

		public override bool Matches(Server server)
		{
			return _fullMatch ? CompareDoubles(server.CurrentLoadPercentage, server.Capacity) : CompareDoubles(server.CurrentLoadPercentage, _expectedLoadPercentage);
		}

		private static bool CompareDoubles(double first, double second) => Math.Abs(first - second) < Epsilon;

		public static Matcher<Server> HasLoadPercentageOf(double value) => new CurrentLoadPercentageMatcher(value);

		public static IMatcher<Server> StaysFull() => new CurrentLoadPercentageMatcher(true);
		
   }
}