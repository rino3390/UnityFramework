using System.Collections.Generic;
using System.Linq;

namespace DDDCore.DDDCore
{
	public class AggregateRoot : Entity<string>
	{
		protected AggregateRoot(string id) : base(id) { }
		private readonly List<IEvent> _events = new List<IEvent>();

		protected void AddEvent(IEvent @event)
		{
			_events.Add(@event);
		}

		public void ClearEvents()
		{
			_events.Clear();
		}

		public T FindEvent<T>() where T : IEvent
		{
			var tEvent = _events.Find(e => e is T);
			return (T)tEvent;
		}

		public IEnumerable<T> FindEvents<T>() where T : IEvent
		{
			var events = _events.FindAll(e => e is T).Cast<T>();
			return events;
		}

		public List<IEvent> GetEvents()
		{
			return _events;
		}
	}
}