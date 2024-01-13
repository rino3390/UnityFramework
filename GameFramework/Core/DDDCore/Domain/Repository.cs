using System.Collections.Generic;

namespace GameFramework.Core.Domain
{
	public class Repository<TEntity>: IRepository<TEntity> where TEntity: Entity
	{
		public int Count => entities.Count;
		public IEnumerable<string> Keys => entities.Keys;
		public IEnumerable<TEntity> Values => entities.Values;
		protected readonly Dictionary<string, TEntity> entities = new();

		public TEntity this[string Id]
		{
			get => GetExistEntity(Id).value;
			set => entities[Id] = value;
		}

		public void Save(TEntity entity)
		{
			if(entity == null) return;

			entities[entity.GetId()] = entity;
		}

		public void DeleteAll()
		{
			entities.Clear();
		}

		public void DeleteById(string Id)
		{
			if(entities.ContainsKey(Id))
			{
				entities.Remove(Id);
			}
		}

		public (bool exist, TEntity value) GetExistEntity(string Id)
		{
			return (ContainsId(Id), ContainsId(Id) ? entities[Id] : default);
		}

		private bool ContainsId(string Id)
		{
			return entities.ContainsKey(Id);
		}
	}
}