using System.Collections;
using System.Collections.Generic;
using LoadBalancerKata;
using static LoadBalancerKataTests.Builders.ServerBuilder;
using static LoadBalancerKataTests.Builders.VmBuilder;
using static LoadBalancerKataTests.ServerLoadBalancerTestBase;

namespace LoadBalancerKataTests.ParametrizedTests.ClassDatas
{
	public class ManyServersManyVmsTestData : IEnumerable<object[]>
	{
		private readonly List<DataStructHelperClass> _data = new List<DataStructHelperClass>();

		public ManyServersManyVmsTestData()
		{
			var (vm1, vm2, vm3, vm4, vm5) = (A(Vm()), A(Vm()), A(Vm()), A(Vm()), A(Vm()));
			var (vm6, vm7, vm8, vm9, vm10) = (A(Vm()), A(Vm()), A(Vm()), A(Vm()), A(Vm()));
			var (vm11, vm12, vm13, vm14, vm15) = (A(Vm()), A(Vm()), A(Vm()), A(Vm()), A(Vm()));
			var (vm16, vm17, vm18, vm19, vm20) = (A(Vm()), A(Vm()), A(Vm()), A(Vm()), A(Vm()));

			var (s1, s2, s3) = (A(Server()), A(Server()), A(Server()));
			var (s4, s5) = (A(Server()), A(Server()));
			var (s6, s7, s8, s9) = (A(Server()), A(Server()), A(Server()), A(Server()));
			var (s10, s11, s12, s13, s14) = (A(Server()), A(Server()), A(Server()), A(Server()), A(Server()));

			var data1 = new DataStructHelperClass
			{
				ExpectedLoadPercentages = new List<double> {3.7, 4.5, 23},
				ExpectedVms = new List<List<Vm>>
				{
				}
			};

			var data2 = new DataStructHelperClass { };
			var data3 = new DataStructHelperClass{};
			var data4 = new DataStructHelperClass{};

			_data.Add(data1);
			_data.Add(data2);
			_data.Add(data3);
			_data.Add(data4);
		}

		public IEnumerator<object[]> GetEnumerator()
		{
			foreach (var dataStructHelperClass in _data)
			{
				yield return new object[]
				{
					dataStructHelperClass.Servers,
					dataStructHelperClass.Vms,
					dataStructHelperClass.ExpectedVms,
					dataStructHelperClass.ExpectedVms
				};
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private class DataStructHelperClass
		{
			internal List<Server> Servers;
			internal List<Vm> Vms;
			internal List<List<Vm>> ExpectedVms;
			internal List<double> ExpectedLoadPercentages;
		}
	}
}