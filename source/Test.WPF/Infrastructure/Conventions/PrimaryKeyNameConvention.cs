namespace Test.Infrastructure.Conventions {
	using FluentNHibernate.Conventions;
	using FluentNHibernate.Conventions.Instances;
	class PrimaryKeyNameConvention : IIdConvention {
		public void Apply(IIdentityInstance instance) {
			instance.Column("Id");
		}
	}
}