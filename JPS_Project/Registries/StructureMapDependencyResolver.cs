using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace JPS_Project.Registries
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly Func<IContainer> _container;
        public StructureMapDependencyResolver(Func<IContainer> container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;
            return serviceType.IsAbstract || serviceType.IsInterface 
                ? _container().TryGetInstance(serviceType) 
                : _container().GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container().GetAllInstances(serviceType).Cast<object>();
        }
    }
}