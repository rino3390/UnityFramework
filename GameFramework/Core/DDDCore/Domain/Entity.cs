namespace GameFramework.Core.Domain
{
	public class Entity
	{
		public string Id { get; }

		protected Entity(string id)
		{
			Id = id;
		}
	}
}