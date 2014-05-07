namespace Test.WPF.Repositories {
	using DbSessionQueue.Interfaces;
	using NHibernate.Linq;
	using NHibernate.Criterion;
	using NHibernate.Criterion.Lambda;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Test.Core.DataModel;
	using Test.WPF.Infrastructure;
	using System.Diagnostics;
	public class Repository<T> : IRepository<T> where T : DataModelBase, new() {
		UnitOfWork _UnitOfWork;
		public Repository(UnitOfWork unitOfWork) {
			_UnitOfWork = unitOfWork;
		}
		public T Get(int id) {
			return _UnitOfWork.Session.Load<T>(id);
		}
		public T GetFirst(System.Linq.Expressions.Expression<Func<T, bool>> condition) {
			return _UnitOfWork.Session.Query<T>().FirstOrDefault(condition);
		}
		public T Single(System.Linq.Expressions.Expression<Func<T, bool>> condition) {
			return _UnitOfWork.Session.Query<T>().Single<T>(condition);
		}
		public T SingleOrDefault(System.Linq.Expressions.Expression<Func<T, bool>> condition) {
			return _UnitOfWork.Session.Query<T>().SingleOrDefault<T>(condition);
		}
		public IQueryable<T> GetBy(System.Linq.Expressions.Expression<Func<T, bool>> condition) {
			return _UnitOfWork.Session.Query<T>().Where(condition);
		}
		public IEnumerable<T> GetList(Func<QueryOverProjectionBuilder<T>, QueryOverProjectionBuilder<T>> condition) {
			return _UnitOfWork.Session.QueryOver<T>().SelectList(condition).List<T>();
		}
		public IQueryable<T> GetAll() {
			return _UnitOfWork.Session.Query<T>();
		}
		public IEnumerable<T> GetByIDs(IEnumerable<int> ids) {
			return _UnitOfWork.Session.CreateCriteria(typeof(T)).Add(Restrictions.In("Id", ids.ToArray())).List<T>();
		}
		public virtual T SaveOrUpdate(T entity) {
			try {
				entity.UpdatedOnUTC = DateTime.UtcNow;
				_UnitOfWork.Session.SaveOrUpdate(entity);
				_UnitOfWork.SaveChanges();
			} catch (Exception ex) {
				Trace.WriteLine(ex.Message);
				throw;
			}
			return entity;
		}
		public IEnumerable<T> SaveOrUpdate(IEnumerable<T> entities) {
			try {
				entities.ForEach(entity => {
					entity.UpdatedOnUTC = DateTime.UtcNow;
					_UnitOfWork.Session.SaveOrUpdate(entity);
				});
				_UnitOfWork.SaveChanges();
			} catch (Exception ex) {
				Trace.WriteLine(ex.Message);
				throw;
			}
			return entities;
		}
		public void Delete(T item) {
			try {
				_UnitOfWork.Session.Delete(item);
				_UnitOfWork.Session.Flush();
				_UnitOfWork.Session.Evict(item);
				_UnitOfWork.SaveChanges();
			} catch (Exception ex) {
				Trace.WriteLine(ex.Message);
				throw;
			}
		}
	}
}