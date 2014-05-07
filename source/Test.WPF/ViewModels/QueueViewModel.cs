namespace Test.ViewModels {
	using Caliburn.Micro;
	using DbSessionQueue;
	public class QueueViewModel : Screen {
		SessionQueue _Queue;
		public SessionQueue Queue {
			get { return _Queue; }
			set {
				_Queue = value;
				NotifyOfPropertyChange(() => Queue);
			}
		}
		public QueueViewModel(SessionQueue queue) {
			Queue = queue;
			Queue.Start();
		}
	}
}