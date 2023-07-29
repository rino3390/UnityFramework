using DDDCore.Event.EventBus;
using DDDCore.Event.EventPublish;
using DDDCore.Event.EventSubscribe;
using NSubstitute;
using NUnit.Framework;
using System;

namespace DDDCore.TestFramework
{
	public class TestFramework
	{
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

		protected string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}