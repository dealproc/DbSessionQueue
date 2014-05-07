namespace DbSessionQueue.Interfaces {
	public interface ISessionCommand {
		void Execute(IDependencyResolver dependencyResolver);
	}
}