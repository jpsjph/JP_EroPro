using System.Web;
using System.Web.Optimization;

namespace Ero57_Project
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
             "~/Scripts/Kendo/kendo.all.min.js",
             "~/Scripts/Kendo/kendo.timezones.min.js",
              "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/Kendo/css").Include(
          "~/Content/Kendo/kendo.common-bootstrap.min.css",
          "~/Content/Kendo/kendo.common.min.css",
          "~/Content/Kendo/kendo.bootstrap.min.css",          
          "~/Content/Kendo/kendo.default.min.css" ,
          "~/Content/Kendo/kendo.dataviz.min.css",
          "~/Content/Kendo/kendo.dataviz.default.min.css")); 
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
