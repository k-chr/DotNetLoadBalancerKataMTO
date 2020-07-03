using System.Collections;
using System.Collections.Generic;
using LoadBalancerKata;
using static LoadBalancerKataTests.Builders.ServerBuilder;
using static LoadBalancerKataTests.Builders.VmBuilder;
using static LoadBalancerKataTests.ServerLoadBalancerTestBase;

namespace LoadBalancerKataTests.ParametrizedTests.ClassData
{
	public class ManyServersManyVmsTestData : IEnumerable<object[]>
	{
		private readonly List<DataStructHelperClass> _data = new List<DataStructHelperClass>();

		public ManyServersManyVmsTestData()
		{
			var (vm1, vm2, vm3, vm4, vm5) = (A(Vm().WithSize(2)), A(Vm().WithSize(1)), A(Vm().WithSize(8)),
				A(Vm().WithSize(4)), A(Vm().WithSize(5)));
			var (vm6, vm7, vm8, vm9, vm10) = (A(Vm().WithSize(3)), A(Vm().WithSize(7)), A(Vm().WithSize(6)),
				A(Vm().WithSize(5)), A(Vm().WithSize(2)));
			var (vm11, vm12, vm13, vm14, vm15) = (A(Vm().WithSize(1)), A(Vm().WithSize(6)), A(Vm().WithSize(1)),
				A(Vm().WithSize(3)), A(Vm().WithSize(4)));
			var (vm16, vm17, vm18, vm19, vm20) = (A(Vm().WithSize(5)), A(Vm().WithSize(9)), A(Vm().WithSize(7)),
				A(Vm().WithSize(11)), A(Vm().WithSize(8)));

			var (s1, s2, s3) = (A(Server().WithCapacity(10)), A(Server().WithCapacity(8)),
				A(Server().WithCapacity(20)));
			var (s4, s5) = (A(Server().WithCapacity(20)), A(Server().WithCapacity(16)));
			var (s6, s7, s8, s9) = (A(Server().WithCapacity(2)), A(Server().WithCapacity(8)),
				A(Server().WithCapacity(8)), A(Server().WithCapacity(11)));
			var (s10, s11, s12, s13, s14) = (A(Server().WithCapacity(20)), A(Server().WithCapacity(4)),
				A(Server().WithCapacity(8)), A(Server().WithCapacity(8)), A(Server().WithCapacity(11)));
			var data4 = new DataStructHelperClass
			{
				Servers = new List<Server> {s10, s11, s12, s13, s14},
				Vms = new List<Vm> {vm16, vm17, vm18, vm19, vm20},
				ExpectedLoadPercentages = new List<double> {80.0, 0.0, 87.5, 100.0, 81.81818181 },
				ExpectedVms = new List<ICollection<Vm>>
				{
					new List<Vm> {vm16, vm19},
					new List<Vm>(),
					new List<Vm> {vm18},
					new List<Vm> {vm20},
					new List<Vm> {vm17}
				}
			};
			var data1 = new DataStructHelperClass
			{
				Servers = new List<Server> {s1, s2, s3},
				Vms = new List<Vm> {vm1, vm2, vm3, vm4, vm5},
				ExpectedLoadPercentages = new List<double> {70.0, 62.5, 40.0},
				ExpectedVms = new List<ICollection<Vm>>
				{
					new List<Vm> {vm1, vm5},
					new List<Vm> {vm2, vm4},
					new List<Vm> {vm3}
				}
			};

			var data2 = new DataStructHelperClass
			{
				Servers = new List<Server> {s4, s5},
				Vms = new List<Vm> {vm6, vm7, vm8, vm9, vm10},
				ExpectedLoadPercentages = new List<double> {55.0, 75.0},
				ExpectedVms = new List<ICollection<Vm>>
				{
					new List<Vm> {vm6, vm8, vm10},
					new List<Vm> {vm7, vm9}
				}
			};

			var data3 = new DataStructHelperClass
			{
				Servers = new List<Server> {s6, s7, s8, s9},
				Vms = new List<Vm> {vm11, vm12, vm13, vm14, vm15},
				ExpectedLoadPercentages = new List<double> {50.0, 75.0, 62.5, 27.27272727},
				ExpectedVms = new List<ICollection<Vm>>
				{
					new List<Vm> {vm11},
					new List<Vm> {vm12},
					new List<Vm> {vm13, vm15},
					new List<Vm> {vm14}
				}
			};


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
					dataStructHelperClass.ExpectedLoadPercentages
				};
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		private class DataStructHelperClass
		{
			internal List<Server> Servers;
			internal List<Vm> Vms;
			internal List<ICollection<Vm>> ExpectedVms;
			internal List<double> ExpectedLoadPercentages;
		}
	}
}