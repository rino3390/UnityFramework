using System;

namespace DDDCore.DDDCore
{
	public interface IEventBus
	{
		void Subscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent;
		void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
		void Publish(AggregateRoot aggregateRoot);
	}
}