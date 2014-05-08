namespace Test.ViewModels {
	using Caliburn.Micro;
	public class ShellViewModel : Screen {
		IWindowManager _WindowManager;
		QueueViewModel _Queue;
		ThreadListViewModel _ThreadList;
		RepositoryReadViewModel _ReadFromRepository;

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
		public RepositoryReadViewModel ReadFromRepository {
			get { return _ReadFromRepository; }
			set {
				_ReadFromRepository = value;
				NotifyOfPropertyChange(() => ReadFromRepository);
			}
		}
		public ShellViewModel(IWindowManager windowManager, QueueViewModel queue, ThreadListViewModel threadList, RepositoryReadViewModel readFromRepository) {
			_WindowManager = windowManager;
			Queue = queue;
			ThreadList = threadList;
			ReadFromRepository = readFromRepository;

			Queue.ActivateWith(this);
			ThreadList.ActivateWith(this);
			ReadFromRepository.ActivateWith(this);
		}
	}
}