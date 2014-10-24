using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Services.Tasks;
using JPS_Project.Registries;
using StructureMap;
namespace JPS_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {     

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = IocInitialize.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(() => Container ?? container));
            using (var cont = container.GetNestedContainer())
            {
                foreach (var task in cont.GetAllInstances<IRunAtInit>())
                {
                    task.Execute();
                } foreach (var task in cont.GetAllInstances<IRunAtStartup>())
                {
                    task.Execute();
                }
            }
        }

        public void Application_BeginRequest()
        {
            Container = IocInitialize.Initialize().GetNestedContainer();            
            foreach (var task in Container.GetAllInstances<IRunOnRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.GetAllInstances<IRunOnError>())
            {
                task.Execute();
            }
        }

        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in Container.GetAllInstances<IRunAfterRequest>())
                {
                    task.Execute();
                }
            }
            finally
            {
                Container.Dispose();
                Container = null;
            }
        }

        public IContainer Container
        {
            get
            {
                return (IContainer)HttpContext.Current.Items["_Container"];
            }
            set
            {
                HttpContext.Current.Items["_Container"] = value;
            }
        }
    }
}
