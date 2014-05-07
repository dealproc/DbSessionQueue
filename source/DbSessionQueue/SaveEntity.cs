namespace DbSessionQueue {
	using DbSessionQueue.Interfaces;
	public class SaveEntity<TEntity> : ISessionCommand
		where TEntity : class, new() {
		TEntity _Entity;
		public SaveEntity(TEntity entity) {
			_Entity = entity;
		}
		public void Execute(IDependencyResolver dependencyResolver) {
			TEntity savedEntity;
			using (var ctx = dependencyResolver.CreateContext()) {
				var repository = ctx.Resolve<IRepository<TEntity>>();
				savedEntity = repository.SaveOrUpdate(_Entity);
			}
			OnCompleted(savedEntity);
		}
		public virtual void OnCompleted(TEntity entity) { }
	}
}