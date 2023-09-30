using System;
using System.Collections.Generic;

namespace GameFramework.Core.Event
{
	public interface IEventBus
	{
		void Subscribe<TEvent>(Action<TEvent> callBack,params Func<TEvent, bool>[] filter) where TEvent : IEvent;
		void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void Publish(IEvent @event);
		void Publish(List<IEvent> @event);
	}
}