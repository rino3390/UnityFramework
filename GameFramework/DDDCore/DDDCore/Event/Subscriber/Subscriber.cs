using GameFramework.DDDCore.Event.CoreEvent;
using GameFramework.DDDCore.Event.EventBus;
using System;

namespace GameFramework.DDDCore.Event.Subscriber
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