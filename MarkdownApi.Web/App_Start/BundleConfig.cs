using System.Web;
using System.Web.Optimization;

namespace MarkdownApi.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                "~/Scripts/lib/jquery-{version}.js",
                "~/Scripts/lib/bootstrap.js",
                "~/Scripts/lib/respond.js",
                "~/Scripts/lib/toastr.js",
                "~/Scripts/lib/prettify.js",
                "~/Scripts/lib/lang-*",
                "~/Scripts/lib/showdown.js",
                "~/Scripts/lib/showdown-prettify.js",
                "~/Scripts/lib/angular.js",
                "~/Scripts/app/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/core/services/*.js",
                "~/Scripts/app/core/*.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/toastr.css",
                "~/Content/prettify.css",
                "~/Content/prettify-themes/sons-of-obsidian.css",
                "~/Content/site.css"));
        }
    }
}
