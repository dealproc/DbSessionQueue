namespace Test.ViewModels {
	using Caliburn.Micro;
	public class ShellViewModel : Screen {
		IWindowManager _WindowManager;
		QueueViewModel _Queue;
		ThreadListViewModel _ThreadList;

		public QueueViewModel Queue {
			get { return _Queue; }
			set {
				_Queue = value;
				NotifyOfPropertyChange(() => Queue);
			}
		}
		public ThreadListViewModel ThreadList {
			get { return _ThreadList; }
			set {
				_ThreadList = value;
				NotifyOfPropertyChange(() => ThreadList);
			}
		}
		public ShellViewModel(IWindowManager windowManager, QueueViewModel queue, ThreadListViewModel threadList) {
			_WindowManager = windowManager;
			Queue = queue;
			ThreadList = threadList;
		}
	}
}