using MessagePipe;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Core.Event
{
	public class EventBus: IEventBus
	{
		private readonly IPublisher<IEvent> publisher;
		private readonly ISubscriber<IEvent> subscriber;
		private readonly Dictionary<(Type, Action<IEvent>), IDisposable> disposables = new();

		public EventBus(IPublisher<IEvent> publisher, ISubscriber<IEvent> subscriber)
		{
			this.publisher = publisher;
			this.subscriber = subscriber;
		}

		public void Subscribe<TEvent>(Action<TEvent> callBack, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			Subscribe(callBack, _ => true, filters);
		}

		public void Subscribe<TEvent>(Action<TEvent> callBack, Func<TEvent, bool> filter, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			var bag = DisposableBag.CreateBuilder();

			subscriber.Subscribe(e => callBack((TEvent)e), e => e is TEvent @event && filter(@event), filters).AddTo(bag);
			
			if(disposables.TryGetValue(( typeof(TEvent), callBack as Action<IEvent> ), out var disposable)) return;
			disposable = bag.Build();
			disposables.Add(( typeof(TEvent), callBack as Action<IEvent> ), disposable);
		}

		public void UnSubscribe<TEvent>(Action<TEvent> callBack) where TEvent: IEvent
		{
			if(!disposables.TryGetValue(( typeof(TEvent), callBack as Action<IEvent> ), out var disposable)) return;

			disposable.Dispose();
			disposables.Remove(( typeof(TEvent), callBack as Action<IEvent> ));
		}

		public void Publish(IEvent @event)
		{
			publisher.Publish(@event);
		}

		public void Publish(List<IEvent> @event)
		{
			foreach(var e in @event)
			{
				publisher.Publish(e);
			}
		}
	}
}