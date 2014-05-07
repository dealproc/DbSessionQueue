namespace Test.ViewModels {
	using Caliburn.Micro;
	public class MessageBoxViewModel : Screen {
		string _Message;
		string _Title;
		public string Message {
			get { return _Message; }
			set {
				_Message = value;
				NotifyOfPropertyChange(() => this.Message);
			}
		}
		public string Title {
			get { return _Title; }
			set {
				_Title = value;
				NotifyOfPropertyChange(() => this.Title);
			}
		}
		public void Ok() {
			TryClose(true);
		}
		public void Cancel() {
			TryClose(false);
		}
	}
}