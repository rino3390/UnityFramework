using GameFramework.DDDCore.Event.CoreEvent;
using GameFramework.RinoUtility.MessagePipeFilter;
using System;
using System.Collections.Generic;

namespace GameFramework.DDDCore.Event.EventBus
{
	public interface IEventBus
	{
		void Subscribe<TEvent,TValue>(Action<TEvent> callBack,params WhereFilter<TValue>[] filter) where TEvent : IEvent;
		void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void Publish(IEvent @event);
		void Publish(List<IEvent> @event);
	}
}