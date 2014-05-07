//namespace Test.Infrastructure.Conventions {
//	using FluentNHibernate.Conventions;
//	using FluentNHibernate.Conventions.AcceptanceCriteria;
//	using FluentNHibernate.Conventions.Inspections;
//	using FluentNHibernate.Conventions.Instances;
//	public class SchemaConvention : IClassConvention, IClassConventionAcceptance {
//		public void Accept(IAcceptanceCriteria<IClassInspector> criteria) {
//			criteria
//				.Expect(x => x.Schema, Is.Not.Set)
//				.Expect(x => x.EntityType.Namespace.StartsWith("Adapt.POS.Workstation.DataModel"));
//		}

//		public void Apply(IClassInstance instance) {
//			instance.Schema("POS");
//		}
//	}
//}