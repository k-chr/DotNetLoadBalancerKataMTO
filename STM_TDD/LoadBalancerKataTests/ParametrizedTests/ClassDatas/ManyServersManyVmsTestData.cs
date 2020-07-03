using System.Collections;
using System.Collections.Generic;
using static LoadBalancerKataTests.Builders.ServerBuilder;
using static LoadBalancerKataTests.Builders.VmBuilder;
using static LoadBalancerKataTests.ServerLoadBalancerTestBase;

namespace LoadBalancerKataTests.ParametrizedTests.ClassDatas
{
   public class ManyServersManyVmsTestData : IEnumerable<object[]>
   {
	   public IEnumerator<object[]> GetEnumerator()
	   {
		   throw new System.NotImplementedException();
	   }

	   IEnumerator IEnumerable.GetEnumerator()
	   {
		   return GetEnumerator();
	   }
   }
}
