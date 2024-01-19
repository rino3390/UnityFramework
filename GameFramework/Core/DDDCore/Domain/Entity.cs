namespace GameFramework.Core.Domain
{
	public class Entity
	{
		public string ID { get; }

		protected Entity(string id)
		{
			ID = id;
		}
	}
}