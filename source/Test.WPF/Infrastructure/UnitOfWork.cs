namespace Test.Infrastructure {
	using DbSessionQueue.Interfaces;
	using NHibernate;
	using System;
	using System.Diagnostics;
	public class UnitOfWork : IUnitOfWork, IDisposable {
		bool _IsDisposed = false;
		string _Id;
		ISession _Session;
		ITransaction _Transaction;
		public ISession Session {
			get { return _Session; }
			protected set { _Session = value; }
		}
		public UnitOfWork(ISession session) {
			this.Session = session;
			this.Session.FlushMode = FlushMode.Commit;
			_Transaction = this.Session.BeginTransaction();
			_Id = Guid.NewGuid().ToString();
		}
		public void SaveChanges() {
			if (_Transaction == null) {
				throw new InvalidOperationException("UnitOfWork has already been saved.");
			}
			this.Session.Flush();
			this.Session.Transaction.Commit();

			// do we need to restart the session here?
			_Transaction = null;
		}
		public void Dispose() {
			Dispose(_IsDisposed);
		}
		private void Dispose(bool _IsDisposed) {
			if (!_IsDisposed) {

				if (_Transaction != null) {
					if (this.Session.Transaction.IsActive && !this.Session.Transaction.WasRolledBack) {
						this._Transaction.Rollback();
						Trace.WriteLine("Transaction aborted - " + _Id);
					}
					this.Session.Close();
				} else {
					Trace.WriteLine("UoW disposed - " + _Id);
					this.Session.Flush();
				}
				GC.SuppressFinalize(this);
			}
		}
	}
}