using System.Web;
using System.Web.Optimization;

namespace Einstein.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var kendoversion = "2019.3.1023";

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //kendo scripts 
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/kendo/" + kendoversion +"/jquery.min.js",
                      "~/Scripts/kendo/" + kendoversion + "/jszip.min.js",
                      "~/Scripts/kendo/" + kendoversion + "/kendo.all.min.js",
                      "~/Scripts/kendo/" + kendoversion + "/kendo.aspnetmvc.min.js",
                      "~/Scripts/kendo/" + kendoversion + "/kendo.timezones.min.js",
                      //"~/Scripts/kendo/" + kendoversion + "/kendo.mobile.min.js",
                      "~/Scripts/kendo/" + kendoversion + "/cultures/kendo.culture.ru-RU.min.js",
                      "~/Scripts/kendo/" + kendoversion + "/messages/kendo.messages.ru-RU.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Site.css",
                       "~/Content/kendo/" + kendoversion + "/kendo.common.min.css",
                       "~/Content/kendo/" + kendoversion + "/kendo.dataviz.min.css",
                        "~/Content/kendo/" + kendoversion + "/kendo.silver.min.css",
                         "~/Content/kendo/" + kendoversion + "/kendo.mobile.common.min.css",
                 "~/Content/kendo/" + kendoversion + "/kendo.dataviz.silver.min.css"));
        }
    }
}
