using System.Collections.Generic;

namespace DDDCore.DDDCore
{
	public interface IRepository<T> where T : AggregateRoot
	{
		T this[string Id] { get; set; }
		int Count => 0;
		IEnumerable<string> Keys => new List<string>();
		IEnumerable<T> Values => new List<T>();
		
		void                  AddOrSet(T entity);
		void                  DeleteAll();
		void                  DeleteById(string Id);
		T                     FindById(string Id);
		(bool exist, T value) GetExistEntity(string Id);
		
	}
}