using GameFramework.DDDCore.Event.CoreEvent;
using GameFramework.DDDCore.Event.EventBus;
using System.Collections.Generic;

namespace GameFramework.DDDCore.Event.Publisher
{
	public class Publisher
	{
		private readonly IEventBus eventBus;
		public Publisher(IEventBus eventBus)
		{
			this.eventBus = eventBus;
		}
		
		public void Publish(IEvent @event) 
		{
			eventBus.Publish(@event);
		}
		public void Publish(List<IEvent> events)
		{
			eventBus.Publish(events);
		}
	}
}