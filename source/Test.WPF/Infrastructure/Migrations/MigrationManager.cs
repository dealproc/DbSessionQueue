namespace Test.WPF.Infrastructure.Migrations {
	using Test.Core.Infrastructure.Migrations;
	public class MigrationManager : IMigrationManager {
		public bool Migrate() {
			return true;
		}
	}
}