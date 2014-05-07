namespace Test.Infrastructure.Conventions {
	using FluentNHibernate.Conventions;
	using FluentNHibernate.Conventions.AcceptanceCriteria;
	using FluentNHibernate.Conventions.Inspections;
	using FluentNHibernate.Conventions.Instances;
	class CreatedAtPropertyAccessConvention : IPropertyConvention, IPropertyConventionAcceptance {
		public void Accept(IAcceptanceCriteria<IPropertyInspector> criteria) {
			criteria.Expect(x => x.Property.Name.Equals("CreatedAt"));
		}

		public void Apply(IPropertyInstance instance) {
			instance.Access.ReadOnlyPropertyThroughCamelCaseField(CamelCasePrefix.Underscore);
		}
	}
}