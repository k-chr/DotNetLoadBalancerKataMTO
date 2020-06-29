using LoadBalancerKata;

namespace LoadBalancerKataTests
{
	public class VmBuilder : IBuilder<Vm>
	{
		private int _size;
		public Vm Build()
		{
			return new Vm(_size);
		}

		public static VmBuilder Vm()
		{
			return new VmBuilder();
		}

		public VmBuilder WithSize(int size)
		{
			_size = size;
			return this;
		}
	}
}