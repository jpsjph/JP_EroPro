using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap.Graph;

namespace JPS_Project.Registries
{
   public class StandardRegistry:Registry
    {
       public StandardRegistry()
       {
           Scan(scan =>
           {
               scan.TheCallingAssembly();
               scan.WithDefaultConventions();
           });
       }   
    }
}
