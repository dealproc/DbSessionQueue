namespace Test.ViewModels {
	using Autofac;
	using Caliburn.Micro;
	public class ThreadListViewModel : Conductor<ThreadWorkerViewModel>.Collection.AllActive {
		ILifetimeScope _LifetimeScope;
		public ThreadListViewModel(ILifetimeScope lifetimeScope) {
			_LifetimeScope = lifetimeScope;
		}
		public void AddThread() {
			Items.Add(_LifetimeScope.Resolve<ThreadWorkerViewModel>());
		}
		public void RemoveThread(ThreadWorkerViewModel viewModel) {
			DeactivateItem(viewModel, true);
			Items.Remove(viewModel);
		}
	}
}