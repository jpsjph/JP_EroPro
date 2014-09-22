using Common.Services.Tasks;
using Core.Concrete;
using Core.Infrastructure;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using System.Web.Mvc;
using Unity.Mvc4;

namespace DataLayer
{
    /// <summary>
    /// Unity Start
    /// </summary>
    public static class Bootstrapper
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDataConnectionString, DataConnectionString>(new PerResolveLifetimeManager());
            var connectionString = container.Resolve<IDataConnectionString>();
            container.RegisterType<IDataContext, DataAccessContext>();
            var dataContext = container.Resolve<IDataContext>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType(typeof(ILogService), typeof(LogService), new InjectionConstructor(connectionString.ConnectionString, container.Resolve<IEmailService>()));
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new InjectionConstructor(dataContext));
            container.RegisterType(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>), new InjectionConstructor(dataContext));
            container.RegisterType<IPersistenceService, PersistenceService>(new InjectionConstructor(dataContext));

            container.RegisterTypes(AllClasses.FromLoadedAssemblies()
                                              .Where(type => typeof(IRunAtInit).IsInstanceOfType(type)),
                                                              WithMappings.FromAllInterfaces,
                                                              WithName.TypeName,
                                                              WithLifetime.Transient);
            container.RegisterTypes(AllClasses.FromLoadedAssemblies()
                                              .Where(type => typeof(IRunAtStartup).IsInstanceOfType(type)),
                                                              WithMappings.FromAllInterfaces,
                                                              WithName.TypeName,
                                                              WithLifetime.Transient);
            container.RegisterTypes(AllClasses.FromLoadedAssemblies()
                                              .Where(type => typeof(IRunOnError).IsInstanceOfType(type)),
                                                              WithMappings.FromAllInterfaces,
                                                              WithName.TypeName,
                                                              WithLifetime.Transient);
            container.RegisterTypes(AllClasses.FromLoadedAssemblies()
                                              .Where(type => typeof(IRunOnRequest).IsInstanceOfType(type)),
                                                              WithMappings.FromAllInterfaces,
                                                              WithName.TypeName,
                                                              WithLifetime.Transient);

            container.RegisterTypes(AllClasses.FromLoadedAssemblies()
                                              .Where(type => typeof(IRunAfterRequest).IsInstanceOfType(type)),
                                                              WithMappings.FromAllInterfaces,
                                                              WithName.TypeName,
                                                              WithLifetime.Transient);

        }
    }
}