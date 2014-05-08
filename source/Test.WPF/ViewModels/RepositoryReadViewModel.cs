namespace Test.ViewModels {
	using Autofac;
	using Caliburn.Micro;
	using System;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading;
	using Test.Core.Repositories.Interfaces;
	public class RepositoryReadViewModel : Screen {
		ILifetimeScope _LifetimeScope;
		Thread _WorkerThread;
		long _ItemsInRepository;

		public long ItemsInRepository {
			get { return _ItemsInRepository; }
			set {
				_ItemsInRepository = value;
				NotifyOfPropertyChange(() => ItemsInRepository);
			}
		}

		public RepositoryReadViewModel(ILifetimeScope lifetimeScope) {
			_LifetimeScope = lifetimeScope;
			_WorkerThread = new Thread(new ThreadStart(QueryQueue));
			_WorkerThread.SetApartmentState(ApartmentState.STA);
			_WorkerThread.IsBackground = true;
		}

		protected override void OnActivate() {
			_WorkerThread.Start();
		}
		protected override void OnDeactivate(bool close) {
			_WorkerThread.Abort();
			base.OnDeactivate(close);
		}

		void QueryQueue() {
			Trace.WriteLine("Reading from repository");
			while (true) {
				using (var scope = _LifetimeScope.BeginLifetimeScope()) {
					var repository = scope.Resolve<IUserRepository>();
					ItemsInRepository = repository.GetAll().LongCount();
				}
				Thread.Sleep(TimeSpan.FromMilliseconds(250));
			}
		}
	}
}