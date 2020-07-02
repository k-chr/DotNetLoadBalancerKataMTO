﻿using LoadBalancerKata;

namespace LoadBalancerKataTests
{
	public class ServerBuilder
	{
		private int _capacity;

		public ServerBuilder()
		{

		}

		public ServerBuilder WithCapacity(int capacity)
		{
			_capacity = capacity;
			return this;
		}

		public Server Build()
		{
			return new Server(_capacity);
		}
	}
}
