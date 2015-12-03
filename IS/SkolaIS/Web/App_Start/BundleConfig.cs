using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/base").Include(
                "~/Scripts/jquery*",
                "~/Scripts/bootstrap.js"
            ));

            bundles.Add(new StyleBundle("~/Style/base").Include(
                "~/Style/bootstrap.css",
                "~/Style/bootstrap-theme.css",
                "~/Style/style.css"
            ));
        }
    }
}