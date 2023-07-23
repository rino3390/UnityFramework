using System.Collections.Generic;

namespace DDDCore.Domain
{
	public class Repository<TEntity, TID>: IRepository<TEntity, TID> where TEntity: Entity<TID>
	{
		public int Count => entities.Count;
		public IEnumerable<TID> Keys => entities.Keys;
		public IEnumerable<TEntity> Values => entities.Values;
		protected readonly Dictionary<TID, TEntity> entities = new Dictionary<TID, TEntity>();

		public TEntity this[TID Id]
		{
			get => FindById(Id);
			set => entities[Id] = value;
		}

		public void AddOrSet(TEntity entity)
		{
			if(entity == null) return;

			entities[entity.GetId()] = entity;
		}

		public void DeleteAll()
		{
			entities.Clear();
		}

		public void DeleteById(TID Id)
		{
			if(entities.ContainsKey(Id))
			{
				entities.Remove(Id);
			}
		}

		public TEntity FindById(TID Id)
		{
			return GetExistEntity(Id).value;
		}

		public (bool exist, TEntity value) GetExistEntity(TID Id)
		{
			return (IsContainsId(Id), IsContainsId(Id) ? entities[Id] : default);
		}

		private bool IsContainsId(TID Id)
		{
			return entities.ContainsKey(Id);
		}
	}
}