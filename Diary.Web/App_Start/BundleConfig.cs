using System.Web;
using System.Web.Optimization;

namespace Diary.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
                
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/datepicker-init.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajax-img-loader").Include(
                   "~/Scripts/img-loader.js"));

            //bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
            //    "~/Scripts/datepicker-init.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/main.css",
                      "~/Content/bootstrap-datepicker3.css"));
        }
    }
}
