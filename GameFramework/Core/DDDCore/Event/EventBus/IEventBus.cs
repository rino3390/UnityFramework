using MessagePipe;
using System;
using System.Collections.Generic;

namespace GameFramework.Core.Event
{
	public interface IEventBus
	{
		void Subscribe<TEvent>(Action<TEvent> callBack, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent;
		void Subscribe<TEvent>(Action<TEvent> callBack, Func<TEvent, bool> filter, params MessageHandlerFilter<IEvent>[] filters) where TEvent : IEvent;
		void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void Publish(IEvent @event);
		void Publish(List<IEvent> @event);
	}
}