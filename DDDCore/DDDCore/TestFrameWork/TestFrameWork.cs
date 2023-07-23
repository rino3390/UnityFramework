using DDDCore.Event.EventBus;
using DDDCore.Event.EventPublish;
using DDDCore.Event.EventSubscribe;
using NSubstitute;
using NUnit.Framework;
using System;

namespace DDDCore.TestFrameWork
{
	public class TestFrameWork
	{
		protected EventPublish eventPublish;
		protected EventSubscribe eventSubscribe;
		protected IEventBus eventBus;

		[SetUp]
		public virtual void Setup()
		{
			eventBus = Substitute.For<IEventBus>();
			eventPublish = new EventPublish(eventBus);
			eventSubscribe = new EventSubscribe(eventBus);
		}

		protected string NewGuid()
		{
			return Guid.NewGuid().ToString();
		}
	}
}