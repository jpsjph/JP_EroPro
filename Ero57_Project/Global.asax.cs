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
using Ero57_Project.Controllers;
namespace Ero57_Project
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           var container= Bootstrapper.GetConfiguredContainer();
           container.RegisterType<AccountController>(new InjectionConstructor());
           container.RegisterType<ManageController>(new InjectionConstructor());
        }
    }
}
