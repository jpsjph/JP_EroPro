using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Graph;
using StructureMap.Pipeline;
using  StructureMap.TypeRules;
using StructureMap.Configuration.DSL;
using System.Web.Mvc;

namespace JPS_Project.Registries
{
    public class ControllerConvention: IRegistrationConvention
    {

        public void Process(Type type,Registry registry)
        {
            if (type.CanBeCastTo(typeof(Controller)) && !type.IsAbstract)
            {
                registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
            }
        }
    }
}
