namespace Test.Core.DataModel {
	public class Phone : DataModelBase {
		User _User;
		string _Number;
		bool _IsActive;
		public virtual User User {
			get { return _User; }
			set {
				_User = value;
				FirePropertyChanged("User");
			}
		}
		public virtual string Number {
			get { return _Number; }
			set {
				_Number = value;
				FirePropertyChanged("Number");
			}
		}
		public virtual bool IsActive {
			get { return _IsActive; }
			set {
				_IsActive = value;
				FirePropertyChanged("IsActive");
			}
		}
	}
}