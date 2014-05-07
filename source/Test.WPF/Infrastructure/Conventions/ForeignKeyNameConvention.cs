namespace Test.Infrastructure.Conventions {
	using FluentNHibernate.Conventions;
	using FluentNHibernate.Conventions.Instances;
	class ForeignKeyNameConvention : IHasManyConvention {
		public void Apply(IOneToManyCollectionInstance instance) {
			instance.Key.Column(instance.EntityType.Name + "Id");
		}
	}
}