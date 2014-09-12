using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Core.Infrastructure;
using Core.Concrete;
using System;

namespace DataLayer
{
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
            container.RegisterType<IDataContext, DataContext>(new InjectionConstructor(connectionString.ConnectionString));
            var dataContext = container.Resolve<IDataContext>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new InjectionConstructor( dataContext));
            container.RegisterType<IPersistenceService, PersistenceService>(new InjectionConstructor(dataContext));
        }
    }
}