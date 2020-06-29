namespace LoadBalancerKataTests
{
	public interface IBuilder<out T>
	{
		T Build();
	}
}