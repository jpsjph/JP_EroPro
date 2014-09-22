using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Common.Services.Tasks;
using Ero57_Project.Controllers;
namespace Ero57_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {

        public IUnityContainer Container
        {
            get { return (IUnityContainer)HttpContext.Current.Items["_Container"]; }
            set { HttpContext.Current.Items["_Container"] = value; }
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Container = Container ?? Bootstrapper.GetConfiguredContainer();
            Container.RegisterType<AccountController>(new InjectionConstructor());
            Container.RegisterType<ManageController>(new InjectionConstructor());
            foreach (var task in Container.ResolveAll<IRunAtInit>())
            {
                task.Execute();
            }
            foreach (var task in Container.ResolveAll<IRunAtStartup>())
            {
                task.Execute();
            }
        }

        public void Application_BeginRequest()
        {
            if (Container == null)
            {
                Container = Bootstrapper.GetConfiguredContainer();
                Container.RegisterType<AccountController>(new InjectionConstructor());
                Container.RegisterType<ManageController>(new InjectionConstructor());
            }
            foreach (var task in Container.ResolveAll<IRunOnRequest>())
            {
                task.Execute();
            }
        }

        public void Application_Error()
        {
            foreach (var task in Container.ResolveAll<IRunOnError>())
            {
                task.Execute();
            }
        }
        public void Application_EndRequest()
        {
            try
            {
                foreach (var task in Container.ResolveAll<IRunAfterRequest>())
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
    }
}
