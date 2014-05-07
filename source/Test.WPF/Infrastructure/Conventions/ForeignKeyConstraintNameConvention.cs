namespace Test.Infrastructure.Conventions {
	using FluentNHibernate.Conventions;
	using FluentNHibernate.Conventions.Instances;
	class ForeignKeyConstraintNameConvention : IHasManyConvention {
		public void Apply(IOneToManyCollectionInstance instance) {
			instance.Key.ForeignKey(string.Format("FK_{0}_{1}", instance.Member.Name, instance.EntityType.Name));
		}
	}
}