namespace DDDCore.DDDCore
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