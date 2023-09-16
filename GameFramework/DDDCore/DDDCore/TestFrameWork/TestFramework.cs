using GameFramework.DDDCore.Event.EventBus;
using GameFramework.DDDCore.Event.Publisher;
using GameFramework.DDDCore.Event.Subscriber;
using NSubstitute;
using NUnit.Framework;
using System;

namespace GameFramework.DDDCore.TestFrameWork
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