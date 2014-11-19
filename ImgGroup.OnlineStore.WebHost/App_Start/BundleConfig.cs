using System.Web;
using System.Web.Optimization;

namespace ImgGroup.OnlineStore.WebHost
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app/shoppingCart.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/2014.2.716/kendo.core.min.js",
                      "~/Scripts/kendo/2014.2.716/kendo.ui.core.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/ko").Include(
                      "~/Scripts/knockout-{version}.js",
                      "~/Scripts/knockout.mapping-latest.js",
                      "~/Scripts/knockout-kendo.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",                      
                      "~/Content/site.css",
                      "~/Content/kendo/2014.2.716/kendo.common.min.css",
                      "~/Content/kendo/2014.2.716/kendo.bootstrap.min.css",
                      "~/Content/toastr.min.css"
                      //"~/Content/kendo/2014.2.716/kendo.common.core.min.css",
                      //"~/Content/kendo/2014.2.716/kendo.common-bootstrap.min.css",
                      //"~/Content/kendo/2014.2.716/kendo.common-bootstrap.core.min.css"));
                      ));
        }
    }
}
