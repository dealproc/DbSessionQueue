namespace DbSessionQueue {
	using Caliburn.Micro;
	using DbSessionQueue.Interfaces;
	using System;
	using System.Collections.Concurrent;
	using System.Threading;
	using System.Threading.Tasks;
	public class SessionQueue : PropertyChangedBase
		, IHandle<ISessionCommand>
		, IDisposable {
		bool _IsDisposed = false;
		volatile bool _Stop = false;
		readonly IDependencyResolver _DependencyResolver;
		readonly IEventAggregator _EventAggregator;
		readonly ConcurrentQueue<ISessionCommand> _CommandQueue;
		readonly Task _Worker;

		public SessionQueue(IDependencyResolver dependencyResolver,IEventAggregator eventAggregator) {
			_EventAggregator = eventAggregator;
			_CommandQueue = new ConcurrentQueue<ISessionCommand>();
			_Worker = new Task(HandleNextMessage);

			_EventAggregator.Subscribe(this);
		}
		private void HandleNextMessage() {
			while (!_Stop) {
				ISessionCommand cmd;
				if (_CommandQueue.TryDequeue(out cmd)) {
					cmd.Execute(_DependencyResolver);
					continue;
				}
				Task.Delay(TimeSpan.FromMilliseconds(10)).Wait();
			}
		}
		public long Count { get { return _CommandQueue.Count; } }
		public void Start() {
			_Worker.Start();
		}
		public void Stop() {
			_Stop = true;
		}
		public void Handle(ISessionCommand command) {
			_CommandQueue.Enqueue(command);
			NotifyOfPropertyChange(() => Count);
		}
		public void Dispose() {
			Dispose(_IsDisposed);
		}
		void Dispose(bool isDisposed) {
			if (!isDisposed) {
				GC.SuppressFinalize(this);
			}
			_IsDisposed = true;
		}
	}
}