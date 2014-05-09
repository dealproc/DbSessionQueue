namespace Test.Core.DataModel {
	using System.Collections.Generic;
	public class User : DataModelBase {
		string _FirstName;
		string _LastName;
		int _Age;
		IList<Phone> _PhoneNumbers;

		public virtual string FirstName {
			get { return _FirstName; }
			set {
				_FirstName = value;
				FirePropertyChanged("FirstName");
			}
		}
		public virtual string LastName {
			get { return _LastName; }
			set {
				_LastName = value;
				FirePropertyChanged("LastName");
			}
		}
		public virtual int Age {
			get { return _Age; }
			set {
				_Age = value;
				FirePropertyChanged("Age");
			}
		}
		public virtual IList<Phone> PhoneNumbers {
			get { return _PhoneNumbers; }
			set {
				_PhoneNumbers = value;
				FirePropertyChanged("PhoneNumbers");
			}
		}
		public User() {
			PhoneNumbers = new List<Phone>();
		}
	}
}
