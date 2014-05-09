namespace DbSessionQueue {
	using DbSessionQueue.Interfaces;
	using System;
	public class DeleteEntity<TEntity> : ISessionCommand
		where TEntity : class, new() {
		TEntity _ItemToDelete;
		Action<TEntity> _WhenCompleted;
		public DeleteEntity(TEntity itemToDelete, Action<TEntity> whenCompleted) {
			_ItemToDelete = itemToDelete;
			_WhenCompleted = whenCompleted;
		}
		public void Invoke(IDependencyResolver dependencyResolver) {
			using (var ctx = dependencyResolver.CreateContext()) {
				var repository = ctx.Resolve<IRepository<TEntity>>();
				repository.Delete(_ItemToDelete);
			}
			_WhenCompleted.Invoke(_ItemToDelete);
		}
	}
}
