namespace Test.Infrastructure.Migrations {

	using FluentMigrator.Runner.Announcers;
	using FluentMigrator.Runner.Initialization;

	using System;

	using Test.Core.Infrastructure.Migrations;

	public class MigrationManager {
		public static bool Migrate() {
			var ctx = new RunnerContext(new NullAnnouncer()) {
				ApplicationContext = string.Empty,
				Database = "sqlite",
				Connection = Constants.CONNECTION_STRING,
				Target = "Test"
			};

			try {
				var executor = new TaskExecutor(ctx);
				executor.Execute();
			} catch (Exception ex) {
				return false;
			}

			return true;
		}
	}
}