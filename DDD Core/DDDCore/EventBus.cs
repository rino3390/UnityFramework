using System;
using System.Collections.Generic;
using System.Linq;

namespace DDDCore.DDDCore
{
	public class EventBus: IEventBus
	{
		private readonly Dictionary<Type, List<Action<IEvent>>> _callBacks = new();

		public void Subscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent
		{
			var eventType = typeof(TEvent);

			if( !_callBacks.TryGetValue(eventType, out var callBacks) )
			{
				callBacks = new List<Action<IEvent>>();
				_callBacks[eventType] = callBacks;
			}

			callBacks.Add(c => callBack((TEvent)c));
		}

		public void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent
		{
			var eventType = typeof(TEvent);

			if( _callBacks.TryGetValue(eventType, out var callBacks) )
			{
				callBacks.Remove(c => callBack((TEvent)c));
			}
		}

		public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
		{
			var eventType = @event.GetType();

			if( _callBacks.TryGetValue(eventType, out var callBacks) )
			{
				foreach( var callBack in callBacks )
				{
					callBack.Invoke(@event);
				}
			}
		}

		public void Publish(AggregateRoot aggregateRoot)
		{
			aggregateRoot.GetEvents().Where(x=> x!=null).ToList().ForEach(Publish);
			aggregateRoot.ClearEvents();
		}
	}
}