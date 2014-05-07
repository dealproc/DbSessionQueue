namespace Test.Infrastructure.IoC {
	using Autofac;

	using DbSessionQueue.Interfaces;
	
	using System;
	using System.Collections.Generic;
	
	public class AutofacDbSessionQueueDependencyResolver : IDependencyResolver {
		bool _IsDisposed = false;
		ILifetimeScope _LifetimeScope;
		public AutofacDbSessionQueueDependencyResolver(ILifetimeScope lifetimeScope) {
			_LifetimeScope = lifetimeScope;
		}

		public IDependencyResolver CreateContext() {
			return new AutofacDbSessionQueueDependencyResolver(_LifetimeScope.BeginLifetimeScope());
		}

		public TService Resolve<TService>() {
			return _LifetimeScope.Resolve<TService>();
		}

		public TService Resolve<TService>(string key) {
			return _LifetimeScope.ResolveNamed<TService>(key);
		}

		public object GetService(Type serviceType) {
			return _LifetimeScope.Resolve(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType) {
			var enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
			var instance = _LifetimeScope.Resolve(enumerableServiceType);
			return (IEnumerable<object>)instance;
		}

		public void Dispose() {
			Dispose(_IsDisposed);
		}
		void Dispose(bool isDisposed) {
			if (!isDisposed) {
				if (_LifetimeScope != null) {
					_LifetimeScope.Dispose();
					_LifetimeScope = null;
				}
			}
			_IsDisposed = true;
		}
	}
}