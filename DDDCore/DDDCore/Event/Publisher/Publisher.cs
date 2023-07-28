using DDDCore.Event.CoreEvent;
using DDDCore.Event.EventBus;
using System.Collections.Generic;

namespace DDDCore.Event.EventPublish
{
	public class Publisher
	{
		private readonly IEventBus eventBus;
		public Publisher(IEventBus eventBus)
		{
			this.eventBus = eventBus;
		}
		
		public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
		{
			eventBus.Publish(@event);
		}
		public void Publish<TEvent>(List<TEvent> events) where TEvent : IEvent
		{
			eventBus.Publish(events);
		}
	}
}