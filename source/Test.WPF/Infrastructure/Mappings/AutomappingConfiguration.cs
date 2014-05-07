namespace Test.Infrastructure.Mappings {
	using FluentNHibernate.Automapping;
	using System;
	using System.Linq;
	using Test.Core.DataModel;
	public class AutomappingConfiguration : DefaultAutomappingConfiguration {
		public override bool ShouldMap(Type type) {
			return !string.IsNullOrWhiteSpace(type.Namespace) &&
				type.Namespace.StartsWith("Test.Core.DataModel") &&
				type.GetInterfaces().Any(x => x == typeof(IEntity));
		}
		public override bool IsDiscriminated(Type type) {
			return true;
		}
		//public override bool IsComponent(Type type) {
		//	return type == typeof(Address);
		//}
	}
}