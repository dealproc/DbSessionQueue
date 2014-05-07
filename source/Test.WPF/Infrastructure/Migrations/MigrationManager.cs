namespace Test.Infrastructure.Migrations {

	using FluentMigrator.Runner.Announcers;
	using FluentMigrator.Runner.Initialization;
	using System;
	using System.IO;
	using Test.Core.Infrastructure.Migrations;

	public class MigrationManager {
		public static bool Migrate() {
			var folderInfo = new DirectoryInfo(Constants.DATABASE_FILE);
			var folder = Path.GetDirectoryName(Constants.DATABASE_FILE);
			if (!Directory.Exists(folder)) {
				Directory.CreateDirectory(folder);
			}

			var ctx = new RunnerContext(new NullAnnouncer()) {
				ApplicationContext = string.Empty,
				Database = "sqlite",
				Connection = Constants.CONNECTION_STRING_CREATE,
				Target = "Test.WPF"
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