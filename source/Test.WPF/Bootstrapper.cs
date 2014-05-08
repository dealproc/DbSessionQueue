﻿namespace Test {
	using Autofac;

	using Caliburn.Micro;
	
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Windows;
	
	public class Bootstrapper : BootstrapperBase {
		IContainer _Container;
		public Bootstrapper() {
			Start();
		}
		protected override void OnStartup(object sender, StartupEventArgs e) {
			if (!Infrastructure.Migrations.MigrationManager.Migrate()) {
				MessageBox.Show("Could not migrate database.");
				Environment.Exit(-1);
			}
			base.OnStartup(sender, e);
			DisplayRootViewFor<ViewModels.ShellViewModel>();
		}
		protected override void Configure() {
			var currentAssembly = Assembly.GetExecutingAssembly();
			var builder = new ContainerBuilder();

			// Caliburn Micro support.
			builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
			builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();

			// Session Queue support
			builder.RegisterType<Infrastructure.IoC.AutofacDbSessionQueueDependencyResolver>()
				.As<DbSessionQueue.Interfaces.IDependencyResolver>();

			builder.RegisterType<DbSessionQueue.SessionQueue>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterAssemblyModules(currentAssembly);

			builder.RegisterAssemblyTypes(currentAssembly)
				.Where(t => t.Name.EndsWith("Repository"))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(currentAssembly)
				.Where(t => t.Name.EndsWith("ViewModel") && !t.Name.Contains("Worker"))
				.AsSelf()
				.InstancePerLifetimeScope();

			builder.RegisterType<ViewModels.ThreadWorkerViewModel>()
				.AsSelf()
				.InstancePerDependency();

			_Container = builder.Build();
		}
		protected override object GetInstance(System.Type service, string key) {
			object instance;
			if (string.IsNullOrEmpty(key)) {
				if (_Container.TryResolve(service, out instance)) {
					return instance;
				}
			} else {
				if (_Container.TryResolveNamed(key, service, out instance)) {
					return instance;
				}
			}
			throw new Exception(string.Format("Could not locate any instances of service {0} with key '{1}'.", service.Name, key));
		}
		protected override IEnumerable<object> GetAllInstances(System.Type service) {
			IEnumerable<object> result = _Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
			return result;
		}
		protected override void BuildUp(object instance) {
			_Container.InjectProperties(instance);
		}
	}
}