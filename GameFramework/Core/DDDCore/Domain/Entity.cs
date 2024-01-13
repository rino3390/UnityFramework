namespace GameFramework.Core.Domain
{
	public class Entity
	{
		private readonly string Id;

		protected Entity(string id)
		{
			Id = id;
		}

		public string GetId() => Id;
	}
}