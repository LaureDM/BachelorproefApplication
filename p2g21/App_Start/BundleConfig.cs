using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace p2g21.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include("~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap.jquery-2.1.0.js",
                "~/Scripts/bootstrap.jquery-2.1.0.intellisense.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate-vsdoc.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include("~/Content/bootstrap.min.css", "~/Content/bootstrap-theme.min.css"));
        }
    }
}