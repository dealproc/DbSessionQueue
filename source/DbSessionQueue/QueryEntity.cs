namespace DbSessionQueue {
	using DbSessionQueue.Interfaces;
	using System;
	using System.Linq.Expressions;

	class QueryEntity<TEntity> : ISessionCommand
		where TEntity : class, new() {
		Expression<Func<TEntity,bool>> _Expression;
		Action<TEntity> _WhenCompleted;
		public QueryEntity(Expression<Func<TEntity, bool>> expression, Action<TEntity> whenCompleted) {
			_Expression = expression;
			_WhenCompleted = whenCompleted;
		}
		public void Invoke(IDependencyResolver dependencyResolver) {
			using (var ctx = dependencyResolver.CreateContext()) {
				var repository = ctx.Resolve<IRepository<TEntity>>();
				_WhenCompleted.Invoke(repository.GetFirst(_Expression));
			};
		}
	}
}