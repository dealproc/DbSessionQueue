namespace Test.Infrastructure.IoC {

	using Autofac;

	using DbSessionQueue.Interfaces;
	
	using FluentNHibernate.Automapping;
	using FluentNHibernate.Cfg;
	using FluentNHibernate.Cfg.Db;
	
	using NHibernate;
	using NHibernate.Dialect;
	using NHibernate.Driver;
	
	using System;
	
	using Test.Infrastructure.Conventions;
	using Test.Infrastructure.Mappings;
	using Test.Repositories;
	
	using Configuration = NHibernate.Cfg.Configuration;
	using Module = Autofac.Module;
	
	public class NHibernateConfigModule : Module {
		protected override void Load(ContainerBuilder builder) {
			if (builder == null) {
				throw new ArgumentNullException("builder");
			}

			var cfg = BuildConfiguration();

			if (cfg == null) {
				RegisterComponents(builder, null, null);
			} else {
				var sessionFactory = BuildSessionFactory(cfg);
				RegisterComponents(builder, cfg, sessionFactory);
			}
		}

		private Configuration BuildConfiguration() {
			var rawConfig = new NHibernate.Cfg.Configuration();

			var configuration = Fluently.Configure(rawConfig)
				.Database(
					SQLiteConfiguration.Standard
					.Driver<SQLite20Driver>()
					.Dialect<SQLiteDialect>()
					.ConnectionString(Constants.CONNECTION_STRING)
				)
				.ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.ReleaseConnections, "on_close"))
				.ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.ShowSql, "false"))
				.ExposeConfiguration(c => c.SetProperty(NHibernate.Cfg.Environment.UseSecondLevelCache, "true"))
				.Mappings(m => {
					m.AutoMappings.Add(
						AutoMap
							.AssemblyOf<Test.Core.DataModel.User>(new AutomappingConfiguration())
							.UseOverridesFromAssemblyOf<Infrastructure.Mappings.UserMapOverride>()
							.Conventions.AddFromAssemblyOf<TableNameConvention>()
					);
				})
				.BuildConfiguration();

			return configuration;
		}

		private ISessionFactory BuildSessionFactory(Configuration config) {
			var sessionFactory = config.BuildSessionFactory();
			if (sessionFactory == null) {
				throw new Exception("Cannot build NHibernate Session Factory");
			}
			return sessionFactory;
		}

		private void RegisterComponents(ContainerBuilder builder, Configuration config, ISessionFactory sessionFactory) {
			builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
			builder.RegisterInstance(config).As<Configuration>().SingleInstance();
			builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
			builder.Register(x => {
				var session = x.Resolve<ISessionFactory>().OpenSession();
				return session;
			}).As<ISession>();
			builder.RegisterType<UnitOfWork>().InstancePerLifetimeScope();
		}
	}
}