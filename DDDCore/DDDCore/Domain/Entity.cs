using DDDCore.Event;
using DDDCore.Event.CoreEvent;
using System.Collections.Generic;

namespace DDDCore.Domain
{
	public class Entity<T>
	{
		private readonly T Id;

		protected Entity(T id)
		{
			Id = id;
		}

		public T GetId() => Id;
	}
}