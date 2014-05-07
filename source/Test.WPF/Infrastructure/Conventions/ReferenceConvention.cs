namespace Test.Infrastructure.Conventions {
	using FluentNHibernate.Conventions;
	using FluentNHibernate.Conventions.Instances;
	class ReferenceConvention : IReferenceConvention {
		public void Apply(IManyToOneInstance instance) {
			instance.Column(instance.Property.PropertyType.Name + "Id");
		}
	}
}