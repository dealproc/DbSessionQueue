namespace DbSessionQueue.Interfaces {
	public interface ISessionCommand {
		void Invoke(IDependencyResolver dependencyResolver);
	}
}