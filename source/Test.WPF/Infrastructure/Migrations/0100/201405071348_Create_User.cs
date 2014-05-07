namespace Test.Infrastructure.Migrations._0100 {
	using FluentMigrator;
	[Migration(0100201405071348)]
	public class Create_User : Migration {
		public override void Up() {
			Create.Table("User")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity()

				.WithColumn("FirstName").AsString()
				.WithColumn("LastName").AsString()
				.WithColumn("Age").AsInt64()

				.WithColumn("CreatedOnUTC").AsDateTime()
				.WithColumn("UpdatedOnUTC").AsDateTime();
		}
		public override void Down() {
			Delete.Table("User");
		}
	}
}