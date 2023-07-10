using System.Collections.Generic;

namespace DDDCore.DDDCore
{
	public class Repository<T>: IRepository<T> where T: AggregateRoot
	{
		public int Count => entities.Count;
		public IEnumerable<string> Keys => entities.Keys;
		public IEnumerable<T> Values => entities.Values;
		protected readonly Dictionary<string, T> entities = new Dictionary<string, T>();

		public T this[string Id]
		{
			get => entities[Id];
			set => entities[Id] = value;
		}

		public void AddOrSet(T entity)
		{
			if( entity == null ) return;

			entities[entity.GetId()] = entity;
		}

		public void DeleteAll()
		{
			entities.Clear();
		}

		public void DeleteById(string Id)
		{
			if( entities.ContainsKey(Id) )
			{
				entities.Remove(Id);
			}
		}

		public T FindById(string Id)
		{
			return GetExistEntity(Id).value;
		}

		public (bool exist, T value) GetExistEntity(string Id)
		{
			return (IsContainsId(Id), IsContainsId(Id) ? entities[Id] : default);
		}

		private bool IsContainsId(string Id)
		{
			return entities.ContainsKey(Id);
		}
	}
}