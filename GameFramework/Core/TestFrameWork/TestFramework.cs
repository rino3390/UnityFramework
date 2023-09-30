using GameFramework.Core.Event;
using NSubstitute;
using NUnit.Framework;
using System;
using Zenject;

namespace GameFramework.Core
{
	public class TestFramework
	{
		protected DiContainer container;
		protected Publisher publisher;
		protected Subscriber subscriber;
		protected IEventBus eventBus;

		[SetUp]
		public virtual void Setup()
		{
			eventBus = Substitute.For<IEventBus>();
			publisher = new Publisher(eventBus);
			subscriber = new Subscriber(eventBus);
		}

		protected Scenario Scenario()
		{
			return new Scenario();
		}
		protected string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}