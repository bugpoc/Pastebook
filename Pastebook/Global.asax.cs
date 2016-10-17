using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pastebook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //    routes.MapRoute(
        //        "Test",                                           // Route name
        //        "Pastebook/{username}",                            // URL with parameters
        //        new { controller = "Pastebook", action = "Index" }  // Parameter defaults
        //    );
        //    routes.MapRoute(
        //        "Default",                                              // Route name
        //        "{controller}/{action}/{id}",                           // URL with parameters
        //        new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
        //    );
        //}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
    }
}
