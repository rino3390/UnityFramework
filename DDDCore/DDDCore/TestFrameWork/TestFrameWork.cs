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
		protected Publisher eventPublish;
		protected Subscriber Subscriber;
		protected IEventBus eventBus;

		[SetUp]
		public virtual void Setup()
		{
			eventBus = Substitute.For<IEventBus>();
			eventPublish = new Publisher(eventBus);
			Subscriber = new Subscriber(eventBus);
		}

		protected string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}