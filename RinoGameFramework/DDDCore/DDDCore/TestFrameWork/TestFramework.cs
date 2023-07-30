using NSubstitute;
using NUnit.Framework;
using RinoGameFramework.DDDCore.Event.EventBus;
using RinoGameFramework.DDDCore.Event.Publisher;
using RinoGameFramework.DDDCore.Event.Subscriber;
using System;

namespace RinoGameFramework.DDDCore.TestFrameWork
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