namespace Test {
	using Autofac;

	using Caliburn.Micro;
	
	using System;
	using System.Collections.Generic;
	
	public class Bootstrapper : BootstrapperBase {
		IContainer _Container;
		protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
			DisplayRootViewFor<object>();
		}
		protected override void Configure() {
			base.Configure();
		}
		protected override object GetInstance(System.Type service, string key) {
			object instance;
			if (string.IsNullOrEmpty(key)) {
				if (_Container.TryResolve(service, out instance)) {
					return instance;
				}
			} else {
				if (_Container.TryResolveNamed(key, service, out instance)) {
					return instance;
				}
			}
			throw new Exception(string.Format("Could not locate any instances of service {0} with key '{1}'.", service.Name, key));
		}
		protected override IEnumerable<object> GetAllInstances(System.Type service) {
			IEnumerable<object> result = _Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
			return result;
		}
		protected override void BuildUp(object instance) {
			_Container.InjectProperties(instance);
		}
	}
}