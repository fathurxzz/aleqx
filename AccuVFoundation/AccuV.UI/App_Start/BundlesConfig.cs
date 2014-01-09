using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace AccuV
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/jquery.blockUI.min.js",
            "~/Scripts/libs/kendo/kendo.all.min.js",
            "~/Scripts/controllers/shared.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/ko").Include(
                "~/Scripts/knockout-3.0.0.js",
                "~/Scripts/knockout.mapping-latest.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new Bundle("~/css/all").Include(
                "~/Content/kendo/kendo.common.min.css",
                "~/Content/kendo/kendo.silver.min.css",
                "~/Content/App.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
          "~/Content/bootstrap.css",
          "~/Content/site.css"));
        }
    }
}