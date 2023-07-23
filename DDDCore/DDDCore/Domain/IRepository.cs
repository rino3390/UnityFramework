using System.Collections.Generic;

namespace DDDCore.Domain
{
	public interface IRepository<T, EntityT> where T: Entity<EntityT>
	{
		T this[EntityT Id] { get; set; }
		int Count { get; }
		IEnumerable<EntityT> Keys { get; }
		IEnumerable<T> Values { get; }

		void                  AddOrSet(T entity);
		void                  DeleteAll();
		void                  DeleteById(EntityT Id);
		T                     FindById(EntityT Id);
		(bool exist, T value) GetExistEntity(EntityT Id);
	}
}