using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pastebook
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapPageRoute("", "Pastebook/Friends", "~/Friends.cshtml");

            //http://stackoverflow.com/questions/7618037/asp-net-mvc-routing-by-string-id
            //routes.MapRoute(
            //    "Pastebook",
            //    "Pastebook/{username}",
            //    new { controller = "Pastebook", action = "UserProfile" },
            //    new { username = @"\w\.*" }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Pastebook",
            //    url: "{controller}/{action}/{username}",
            //    defaults: new { controller = "Pastebook", action = "Index", username = UrlParameter.Optional }
            //);
        }
    }
}
