using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap.Graph;

namespace JPS_Project.Registries
{
    public class ControllerRegistry:Registry
    {
        public ControllerRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.With(new ControllerConvention());
            });
        }
    }
}
