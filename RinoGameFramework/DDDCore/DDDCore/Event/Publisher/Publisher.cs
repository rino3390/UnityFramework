using RinoGameFramework.DDDCore.Event.CoreEvent;
using RinoGameFramework.DDDCore.Event.EventBus;
using System.Collections.Generic;

namespace RinoGameFramework.DDDCore.Event.Publisher
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