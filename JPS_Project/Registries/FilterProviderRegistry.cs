using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
using System.Web.Mvc;

namespace JPS_Project.Registries
{
    public class FilterProviderRegistry : FilterAttributeFilterProvider
    {
        private readonly Func<IContainer> _container;
        public FilterProviderRegistry(Func<IContainer> container)
        {
            _container = container;
        }
        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            var container = _container();
            foreach (var filter in filters)
            {
                container.BuildUp(filter.Instance);
                yield return filter;
            }
        }
    }
}