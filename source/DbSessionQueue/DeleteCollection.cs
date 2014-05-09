namespace DbSessionQueue {
	using DbSessionQueue.Interfaces;
	using System;
	using System.Collections.Generic;
	public class DeleteCollection<TEntity> : ISessionCommand
		where TEntity : class, new() {
		IEnumerable<TEntity> _ItemsToDelete;
		Action<IEnumerable<TEntity>> _WhenCompleted;
		public DeleteCollection(IEnumerable<TEntity> itemsToDelete, Action<IEnumerable<TEntity>> whenCompleted) {
			_ItemsToDelete = itemsToDelete;
			_WhenCompleted = whenCompleted;
		}
		public void Invoke(IDependencyResolver dependencyResolver) {
			using (var ctx = dependencyResolver.CreateContext()) {
				var repository = ctx.Resolve<IRepository<TEntity>>();
				foreach (var item in _ItemsToDelete) {
					repository.Delete(item);
				}
				_WhenCompleted.Invoke(_ItemsToDelete);
			}
		}
	}
}