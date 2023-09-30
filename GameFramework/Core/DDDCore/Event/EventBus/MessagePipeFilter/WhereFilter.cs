using MessagePipe;
using System;

namespace GameFramework.Core.Event
{
	public class WhereFilter: MessageHandlerFilter<IEvent>
	{
		private readonly Func<IEvent, bool> _predicate;

		public WhereFilter(Func<IEvent, bool> predicate)
		{
			_predicate = predicate;
		}

		public override void Handle(IEvent message, Action<IEvent> next)
		{
			if(!_predicate(message)) return;

			next(message);
		}
	}
}