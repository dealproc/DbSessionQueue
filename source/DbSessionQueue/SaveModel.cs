namespace DbSessionQueue {
	using DbSessionQueue.Interfaces;
	public class SaveModel<TEntity, TModel> : ISessionCommand
		where TEntity : class, new() {
		TModel _Model;
		public SaveModel(TModel model) {
			_Model = model;
		}
		public void Invoke(IDependencyResolver dependencyResolver) {
			TEntity savedEntity;
			using (var ctx = dependencyResolver.CreateContext()) {
				var repository = ctx.Resolve<IRepository<TEntity>>();
				var builder = ctx.Resolve<IBuilder<TEntity, TModel>>();
				var entity = builder.BuildEntity(_Model);
				savedEntity = repository.SaveOrUpdate(entity);
			}
			OnCompleted(savedEntity);
		}
		public virtual void OnCompleted(TEntity entity) { }
	}
}