using System.Collections;
using System.Collections.Generic;
using static LoadBalancerKataTests.Builders.ServerBuilder;
using static LoadBalancerKataTests.Builders.VmBuilder;
using static LoadBalancerKataTests.ServerLoadBalancerTestBase;

namespace LoadBalancerKataTests.ParametrizedTests.ClassDatas
{
	public class OneServerOneVmTestData : IEnumerable<object[]>
	{
		public IEnumerator<object[]> GetEnumerator()
		{
			yield return new object[] {A(Server().WithCapacity(10)), A(Vm().WithSize(10)), 100.0};
			yield return new object[] {A(Server().WithCapacity(20)), A(Vm().WithSize(5)), 25.0};
			yield return new object[] {A(Server().WithCapacity(6)), A(Vm().WithSize(3)), 50.0};
			yield return new object[] {A(Server().WithCapacity(12)), A(Vm().WithSize(8)), 66.666666};
			yield return new object[] {A(Server().WithCapacity(15)), A(Vm().WithSize(5)), 33.333333};
			yield return new object[] {A(Server().WithCapacity(8)), A(Vm().WithSize(7)), 87.5};
			yield return new object[] {A(Server().WithCapacity(100)), A(Vm().WithSize(3)), 3.0};
			yield return new object[] {A(Server().WithCapacity(14)), A(Vm().WithSize(7)), 50.0};
			yield return new object[] {A(Server().WithCapacity(90)), A(Vm().WithSize(9)), 10.0};
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}