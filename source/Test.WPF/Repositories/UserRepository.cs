namespace Test.WPF.Repositories {
	using Test.Core.Repositories.Interfaces;
	using Test.WPF.Infrastructure;
	public class UserRepository : Repository<Test.Core.DataModel.User>, IUserRepository {
		public UserRepository(UnitOfWork uow) : base(uow) { }
	}
}