using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections.Generic;

namespace GameFramework.Core.Event
{
	public class EventBus: IEventBus
	{
		private readonly IPublisher<IEvent> publisher;
		private readonly ISubscriber<IEvent> subscriber;
		private readonly IAsyncPublisher<IEvent> asyncPublisher;
		private readonly IAsyncSubscriber<IEvent> asyncSubscriber;
		private readonly Dictionary<(Type, Action<IEvent>), IDisposable> disposables = new();

		public EventBus(IPublisher<IEvent> publisher,
						ISubscriber<IEvent> subscriber,
						IAsyncPublisher<IEvent> asyncPublisher,
						IAsyncSubscriber<IEvent> asyncSubscriber)
		{
			this.publisher = publisher;
			this.subscriber = subscriber;
			this.asyncPublisher = asyncPublisher;
			this.asyncSubscriber = asyncSubscriber;
		}

		public void Subscribe<TEvent>(Action<TEvent> callBack, Func<TEvent, bool> filter, params MessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			var bag = DisposableBag.CreateBuilder();

			subscriber.Subscribe(e => callBack((TEvent)e), e => e is TEvent @event && filter(@event), filters).AddTo(bag);

			if(disposables.TryGetValue(( typeof(TEvent), callBack as Action<IEvent> ), out var disposable)) return;

			disposable = bag.Build();
			disposables.Add(( typeof(TEvent), callBack as Action<IEvent> ), disposable);
		}

		public void SubscribeAsync<TEvent>(Func<TEvent, UniTask> callBack, Func<TEvent, bool> filter, params AsyncMessageHandlerFilter<IEvent>[] filters) where TEvent: IEvent
		{
			asyncSubscriber.Subscribe(async (target, _) => { await callBack((TEvent)target); }, e => e is TEvent @event && filter(@event), filters);
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

		public UniTask PublishAsync(IEvent @event)
		{
			return asyncPublisher.PublishAsync(@event, AsyncPublishStrategy.Parallel);
		}
	}
}