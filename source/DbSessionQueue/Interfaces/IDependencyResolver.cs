namespace DbSessionQueue.Interfaces {
	using System;
	using System.Collections.Generic;
	public interface IDependencyResolver : IDisposable {
		IDependencyResolver CreateContext();
		TService Resolve<TService>();
		TService Resolve<TService>(string key);
		object GetService(System.Type serviceType);
		IEnumerable<object> GetServices(System.Type serviceType);
	}
}