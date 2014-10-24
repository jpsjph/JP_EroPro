using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Graph;
using Common.Services.Tasks;

namespace JPS_Project.Registries
{
    public class TaskRegistry : Registry
    {
        public TaskRegistry()
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(a => a.FullName.StartsWith("Common.Services.Tasks"));
                scan.AddAllTypesOf<IRunAtInit>();
                scan.AddAllTypesOf<IRunAtStartup>();
                scan.AddAllTypesOf<IRunOnRequest>();
                scan.AddAllTypesOf<IRunOnError>();
                scan.AddAllTypesOf<IRunAfterRequest>();
            });
        }
    }
}