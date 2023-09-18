using GameFramework.DDDCore.Event.CoreEvent;
using System;
using System.Collections.Generic;

namespace GameFramework.DDDCore.Event.EventBus
{
	public interface IEventBus
	{
		void Subscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void Publish(IEvent @event);
		void Publish(List<IEvent> @event);
	}
}