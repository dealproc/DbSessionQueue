namespace Test.Infrastructure.Mappings {
	using FluentNHibernate.Automapping;
	using FluentNHibernate.Automapping.Alterations;
	using Test.Core.DataModel;
	public class UserMapOverride : IAutoMappingOverride<User> {
		public void Override(AutoMapping<User> mapping) {
		}
	}
}