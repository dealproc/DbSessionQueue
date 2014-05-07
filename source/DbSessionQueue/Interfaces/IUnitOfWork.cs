namespace DbSessionQueue.Interfaces {
	using System;
	public interface IUnitOfWork : IDisposable {
		void SaveChanges();
	}
}