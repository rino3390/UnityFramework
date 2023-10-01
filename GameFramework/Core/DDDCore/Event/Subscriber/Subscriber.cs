using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Threading.Tasks;

namespace GameFramework.Core.Event
{
	public class Subscriber
	{
		private readonly IEventBus eventBus;

		public Subscriber(IEventBus eventBus)
		{
			this.eventBus = eventBus;
		}

		public void Subscribe<TEvent>(Action<TEvent> eventHandler, Func<TEvent, bool> filter, params MessageHandlerFilter<IEvent>[] filters)
			where TEvent: IEvent
		{
			eventBus.Subscribe(eventHandler, filter, filters);
		}

		public void Subscribe<TEvent>(Action<TEvent> eventHandler, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			eventBus.Subscribe(eventHandler, _ => true, filters);
		}

		public void SubscribeAsync<TEvent>(Func<TEvent, UniTask> eventHandler, Func<TEvent, bool> filter, params AsyncMessageHandlerFilter<IEvent>[] filters)
			where TEvent: IEvent
		{
			eventBus.SubscribeAsync(eventHandler, filter, filters);
		}

		public void SubscribeAsync<TEvent>(Func<TEvent, UniTask> eventHandler, params AsyncMessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			eventBus.SubscribeAsync(eventHandler, _ => true, filters);
		}

		public void UnSubscribe<TEvent>(Action<TEvent> eventHandler) where TEvent: IEvent
		{
			eventBus.UnSubscribe(eventHandler);
		}
	}
}