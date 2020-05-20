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

		public CurrentLoadPercentageMatcher(double expectedLoadPercentage) =>
			_expectedLoadPercentage = expectedLoadPercentage;

		public override void DescribeTo(IDescription description) =>
			description.AppendText("This server has a load percentage that equals: ").AppendValue(_expectedLoadPercentage);

		public override void DescribeMismatch(Server item, IDescription mismatchDescription) =>
			mismatchDescription.AppendText("This server has a load percentage that equals: ").AppendValue(item.CurrentLoadPercentage);

		public override bool Matches(Server server) => CompareDoubles(server.CurrentLoadPercentage, _expectedLoadPercentage);

		private static bool CompareDoubles(double first, double second) => Math.Abs(first - second) < Epsilon;

		public static Matcher<Server> HasLoadPercentageOf(double value) => new CurrentLoadPercentageMatcher(value);
	}
}
