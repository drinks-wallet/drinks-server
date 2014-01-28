using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drinks.Web.App_Start
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            var baseBundle = new ScriptBundle("~/Scripts/bundles/basescripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.signalR-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery-ui-{version}.custom.js",
                "~/Scripts/globalize.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap.validator.js",
                "~/Scripts/bootstrap.file-input.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/tools.js",
                "~/Scripts/bootstrap-switch.min.js",
                "~/Scripts/jquery.pnotify.js");
            bundles.Add(baseBundle);
            var baseStyles = new StyleBundle("~/Content/basestyles").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/jquery-ui-{version}.custom.css",
                "~/Content/jquery.pnotify.css",
                "~/Content/jquery.pnotify.icons.css",
                "~/Content/bootstrap-switch.css",
                "~/Content/font-awesome.css"
                );
            bundles.Add(baseStyles);
            var customStyles = new StyleBundle("~/Styles/custom").Include(
                "~/Styles/Site.less"
                );
            customStyles.Transforms.Add(new CssMinify());
            bundles.Add(customStyles);
        }
    }
}