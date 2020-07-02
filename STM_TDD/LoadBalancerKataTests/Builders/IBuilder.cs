namespace LoadBalancerKataTests.Builders
{
	public interface IBuilder<out T>
	{
		T Build();
	}
}