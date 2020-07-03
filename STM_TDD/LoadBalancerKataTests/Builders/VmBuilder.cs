using LoadBalancerKata;

namespace LoadBalancerKataTests.Builders
{
	public class VmBuilder : IBuilder<Vm>
	{
		private int _size;

		private VmBuilder()
		{
		}

		public Vm Build() => new Vm(_size);

		public static VmBuilder Vm() => new VmBuilder();

		public VmBuilder WithSize(int size)
		{
			_size = size;
			return this;
		}
	}
}