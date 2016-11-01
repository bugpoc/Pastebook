using System.Web;
using System.Web.Optimization;

namespace Pastebook
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/pastebook").Include(
                      "~/Scripts/pastebooklayout.js"));

            bundles.Add(new StyleBundle("~/Content/gentelella").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/custom.css",
                     "~/Content/style.css"));


        }
    }
}
