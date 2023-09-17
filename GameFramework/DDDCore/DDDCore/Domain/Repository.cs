using System.Collections.Generic;

namespace GameFramework.DDDCore.Domain
{
	public class Repository<TEntity, TID>: IRepository<TEntity, TID> where TEntity: Entity<TID>
	{
		public int Count => entities.Count;
		public IEnumerable<TID> Keys => entities.Keys;
		public IEnumerable<TEntity> Values => entities.Values;
		protected readonly Dictionary<TID, TEntity> entities = new Dictionary<TID, TEntity>();

		public TEntity this[TID Id]
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

		public void DeleteById(TID Id)
		{
			if(entities.ContainsKey(Id))
			{
				entities.Remove(Id);
			}
		}

		public (bool exist, TEntity value) GetExistEntity(TID Id)
		{
			return (ContainsId(Id), ContainsId(Id) ? entities[Id] : default);
		}

		private bool ContainsId(TID Id)
		{
			return entities.ContainsKey(Id);
		}
	}
}