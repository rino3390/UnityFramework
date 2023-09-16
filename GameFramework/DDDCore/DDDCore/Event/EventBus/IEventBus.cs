using GameFramework.DDDCore.Event.CoreEvent;
using System;
using System.Collections.Generic;

namespace GameFramework.DDDCore.Event.EventBus
{
	public interface IEventBus
	{
		void Subscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
		void Publish<TEvent>(List<TEvent> @event) where TEvent : IEvent;
	}
}