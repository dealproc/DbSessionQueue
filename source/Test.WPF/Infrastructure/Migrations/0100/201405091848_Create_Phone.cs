namespace Test.Infrastructure.Migrations._0100 {
	using FluentMigrator;
	[Migration(0100201405091848)]
	public class Create_Phone : Migration {
		public override void Up() {
			Create.Table("Phone")
				.WithColumn("Id").AsInt32().PrimaryKey().Identity()

				.WithColumn("UserId").AsInt32()
				.WithColumn("Number").AsString()
				.WithColumn("IsActive").AsBoolean()

				.WithColumn("CreatedOnUTC").AsDateTime()
				.WithColumn("UpdatedOnUTC").AsDateTime();
		}
		public override void Down() {
			Delete.Table("Phone");
		}
	}
}