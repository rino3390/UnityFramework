using System.Collections.Generic;

namespace RinoGameFramework.DDDCore.Domain
{
	public interface IRepository<TEntity, EntityId> where TEntity: Entity<EntityId>
	{
		TEntity this[EntityId Id] { get; set; }
		int Count { get; }
		IEnumerable<EntityId> Keys { get; }
		IEnumerable<TEntity> Values { get; }

		void                        Save(TEntity entity);
		void                        DeleteAll();
		void                        DeleteById(EntityId Id);
		(bool exist, TEntity value) GetExistEntity(EntityId Id);
	}
}