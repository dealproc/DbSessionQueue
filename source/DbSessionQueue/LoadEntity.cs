namespace DbSessionQueue {
	using DbSessionQueue.Interfaces;
	using System;
	public class LoadEntity<TEntity> : ISessionCommand
		where TEntity : class, new() {
		object _Id;
		Action<TEntity> _WhenCompleted;
		public LoadEntity(object id, Action<TEntity> whenCompleted) {
			_Id = id;
			_WhenCompleted = whenCompleted;
		}
		public void Invoke(IDependencyResolver dependencyResolver) {
			using (var ctx = dependencyResolver.CreateContext()) {
				var repository = ctx.Resolve<IRepository<TEntity>>();
				_WhenCompleted.Invoke(repository.Get(_Id));
			}
		}
	}
}