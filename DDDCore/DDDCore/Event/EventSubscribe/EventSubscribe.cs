using DDDCore.Event.CoreEvent;
using DDDCore.Event.EventBus;
using System;

namespace DDDCore.Event.EventSubscribe
{
	public class EventSubscribe
	{
		private readonly IEventBus eventBus;

		public EventSubscribe(IEventBus eventBus)
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