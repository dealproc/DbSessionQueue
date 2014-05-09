namespace Test.ViewModels {
	using Autofac;
	using Caliburn.Micro;
	public class ThreadListViewModel : Conductor<ThreadWorkerViewModel>.Collection.AllActive {
		ILifetimeScope _LifetimeScope;
		public ThreadListViewModel(ILifetimeScope lifetimeScope) {
			_LifetimeScope = lifetimeScope;
		}
		protected override void OnActivate() {
			base.OnActivate();
			for (var i = 0; i < 100; i++) {
				AddThread();
			}
		}
		public void AddThread() {
			var thread = _LifetimeScope.Resolve<ThreadWorkerViewModel>();
			thread.DeactivateWith(this);
			Items.Add(thread);
			ActivateItem(thread);
		}
		public void RemoveThread(ThreadWorkerViewModel viewModel) {
			DeactivateItem(viewModel, true);
			Items.Remove(viewModel);
		}
	}
}