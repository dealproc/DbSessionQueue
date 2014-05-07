namespace Test.Core.DataModel {
	using System;
	using System.ComponentModel;
	public abstract class DataModelBase : IEntity, INotifyPropertyChanged {
		int _Id;
		DateTime _CreatedOnUTC;
		DateTime _UpdatedOnUTC;

		public virtual int Id {
			get { return _Id; }
			set {
				_Id = value;
				FirePropertyChanged("Id");
			}
		}
		public virtual DateTime CreatedOnUTC {
			get { return _CreatedOnUTC; }
			set {
				_CreatedOnUTC = value;
				FirePropertyChanged("CreatedOnUTC");
			}
		}
		public virtual DateTime UpdatedOnUTC {
			get { return _UpdatedOnUTC; }
			set {
				_UpdatedOnUTC = value;
				FirePropertyChanged("UpdatedOnUTC");
			}
		}
		public virtual event PropertyChangedEventHandler PropertyChanged;
		protected virtual void FirePropertyChanged(string p) {
			var h = PropertyChanged;
			if (h != null) {
				h(this, new PropertyChangedEventArgs(p));
			}
		}
	}
}