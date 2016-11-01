using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pastebook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //http://stackoverflow.com/questions/19315742/after-logout-if-browser-back-button-press-then-it-go-back-last-screen
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError() as HttpException;

            if (exception != null)
            {
                int errorCode = exception.GetHttpCode();

                if (errorCode == 404)
                {
                    Response.Redirect("~/Error/Error404");
                }

                else
                {
                    Response.Redirect("~/Error/Error500");
                }
            }
        }
    }
}
