using MessagePipe;
using System;

namespace GameFramework.Core.Event
{
	public class Subscriber
	{
		private readonly IEventBus eventBus;

		public Subscriber(IEventBus eventBus)
		{
			this.eventBus = eventBus;
		}

		public void Subscribe<TEvent>(Action<TEvent> eventHandler, Func<TEvent, bool> filter, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			eventBus.Subscribe(eventHandler, filter, filters);
		}

		public void Subscribe<TEvent>(Action<TEvent> eventHandler, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			eventBus.Subscribe(eventHandler, filters);
		}
		
		public void UnSubscribe<TEvent>(Action<TEvent> eventHandler) where TEvent: IEvent
		{
			eventBus.UnSubscribe(eventHandler);
		}
	}
}