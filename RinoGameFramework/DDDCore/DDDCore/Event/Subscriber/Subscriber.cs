using RinoGameFramework.DDDCore.Event.CoreEvent;
using RinoGameFramework.DDDCore.Event.EventBus;
using System;

namespace RinoGameFramework.DDDCore.Event.Subscriber
{
	public class Subscriber
	{
		private readonly IEventBus eventBus;

		public Subscriber(IEventBus eventBus)
		{
			this.eventBus = eventBus;
		}

		public void Subscribe<TEvent>(Action<TEvent> eventHandler) where TEvent: IEvent
		{
			eventBus.Subscribe(eventHandler);
		}

		public void UnSubscribe<TEvent>(Action<TEvent> eventHandler) where TEvent: IEvent
		{
			eventBus.UnSubscribe(eventHandler);
		}
	}
}