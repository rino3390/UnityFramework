using GameFramework.Core.Event;
using MessagePipe;
using NSubstitute;
using NUnit.Framework;

namespace GameFramework.Core.Tests
{
	[TestFixture]
	public class EventBusTest
	{
		private ISubscriber<IEvent> subscriber;
		private IPublisher<IEvent> publisher;
		private EventBus eventBus;

		[SetUp]
		public void Setup()
		{
			// subscriber = Substitute.For<ISubscriber<IEvent>>();
			// publisher = Substitute.For<IPublisher<IEvent>>();
			// eventBus = new EventBus(publisher, subscriber);
		}

		[Test]
		public void Subscribe_Should_Call_Subscriber()
		{
			// subscriber.Received(1).Subscribe();
		}
	}
}