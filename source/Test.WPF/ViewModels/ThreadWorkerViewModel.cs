namespace Test.ViewModels {
	using Caliburn.Micro;
	using DbSessionQueue;
	using System;
	using System.Threading;
	using Test.Core.DataModel;
	public class ThreadWorkerViewModel : Screen {
		string _InstanceId;
		string _Message;
		Thread _WorkerThread;
		IEventAggregator _EventAggregator;

		public string InstanceId {
			get { return _InstanceId; }
		}
		public string Message {
			get { return _Message; }
			set {
				_Message = value;
				NotifyOfPropertyChange(() => Message);
			}
		}
		
		public ThreadWorkerViewModel(IEventAggregator eventAggregator) {
			_EventAggregator = eventAggregator;

			_WorkerThread = new Thread(new ThreadStart(Processor));
			_WorkerThread.SetApartmentState(ApartmentState.STA);
			_WorkerThread.IsBackground = true;

			_InstanceId = Guid.NewGuid().ToString();
		}
		protected override void OnActivate() {
			_WorkerThread.Start();
			base.OnActivate();
		}
		protected override void OnDeactivate(bool close) {
			_WorkerThread.Abort();
			base.OnDeactivate(close);
		}
		
		void Processor() {
			while (true) {
				var user = new User {
					FirstName = "Joe",
					LastName = _InstanceId,
					Age = 10,
					CreatedOnUTC = DateTime.UtcNow,
					UpdatedOnUTC = DateTime.UtcNow
				};
				
				_EventAggregator.PublishOnBackgroundThread(new SaveEntity<User>(user));
				Message = string.Format("Saved User at {0}", DateTimeOffset.Now);

				Thread.Sleep(TimeSpan.FromSeconds(1));
			}
		}
	}
}