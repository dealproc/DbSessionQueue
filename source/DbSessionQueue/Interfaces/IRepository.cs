namespace DbSessionQueue.Interfaces {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	public interface IRepository<T> where T : class, new() {
		T Get(object id);
		T GetFirst(Expression<Func<T, bool>> condition);
		IQueryable<T> GetBy(Expression<Func<T, bool>> condition);
		IQueryable<T> GetAll();
		IEnumerable<T> GetByIDs(IEnumerable<int> ids);
		T SaveOrUpdate(T entity);
		IEnumerable<T> SaveOrUpdate(IEnumerable<T> entities);
		void Delete(T item);
		T Single(Expression<Func<T, bool>> condition);
		T SingleOrDefault(Expression<Func<T, bool>> condition);
	}
}