namespace DbSessionQueue.Interfaces {
	public interface IBuilder<TEntity, TModel> {
		TEntity BuildEntity(TModel model, ModelSource source = ModelSource.Local, bool CreateWhenNotFound = true);
		TModel BuildModel(TEntity entity);
	}
}