using GameFramework.DDDCore.Event.CoreEvent;
using GameFramework.DDDCore.Event.EventBus;
using GameFramework.RinoUtility.MessagePipeFilter;
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

		public void Subscribe<TEvent, TValue>(Action<TEvent> eventHandler, params WhereFilter<TValue>[] filter) where TEvent: IEvent
		{
			eventBus.Subscribe(eventHandler, filter);
		}

		public void UnSubscribe<TEvent>(Action<TEvent> eventHandler) where TEvent: IEvent
		{
			eventBus.UnSubscribe(eventHandler);
		}
	}
}