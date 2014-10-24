using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;
namespace JPS_Project.Registries
{
    public static class IocInitialize
    {
        public static IContainer Initialize()
        {
            return new Container(
                c =>
                {
                    c.AddRegistry(new StandardRegistry());
                    c.AddRegistry(new ControllerRegistry());
                    c.AddRegistry(new MVCRegistry());
                    c.AddRegistry(new TaskRegistry());
                });
        }
    }
}