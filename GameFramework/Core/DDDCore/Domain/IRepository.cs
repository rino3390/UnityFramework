using System.Collections.Generic;

namespace GameFramework.Core.Domain
{
	public interface IRepository<TEntity> where TEntity: Entity
	{
		TEntity this[string Id] { get; }
		int Count { get; }
		IEnumerable<string> Keys { get; }
		IEnumerable<TEntity> Values { get; }

		void Save(TEntity entity);
		void DeleteAll();
		void DeleteById(string Id);
		bool GetExistEntity(string Id, out TEntity value);
	}
}