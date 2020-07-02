namespace LoadBalancerKata
{
	public class Vm
	{
		public int Size { get; }

		public Vm(in int size) => Size = size;

		public override string ToString() => $"Virtual Machine of Size {Size}";
	}
}