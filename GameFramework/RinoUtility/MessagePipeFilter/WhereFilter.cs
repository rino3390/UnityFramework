using MessagePipe;
using System;

namespace GameFramework.RinoUtility.MessagePipeFilter
{
	public class WhereFilter<TValue>: MessageHandlerFilter<TValue>
	{
		private readonly Func<TValue, bool> _predicate;

		public WhereFilter(Func<TValue, bool> predicate)
		{
			_predicate = predicate;
		}

		public override void Handle(TValue message, Action<TValue> next)
		{
			if(!_predicate(message)) return;

			next(message);
		}
	}
}