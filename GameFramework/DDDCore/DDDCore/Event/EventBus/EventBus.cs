using GameFramework.DDDCore.Event.CoreEvent;
using System;
using System.Collections.Generic;

namespace GameFramework.DDDCore.Event.EventBus
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

			callBacks.Add(e => callBack((TEvent)e));
		}

		public void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent : IEvent
		{
			var eventType = typeof(TEvent);

			if( _callBacks.TryGetValue(eventType, out var callBacks) )
			{
				callBacks.Remove(e => callBack((TEvent)e));
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
		public void Publish<TEvent>(List<TEvent> events) where TEvent : IEvent
		{
			foreach( var @event in events )
			{
				Publish(@event);
			}
		}
	}
}