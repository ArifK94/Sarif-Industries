using System.Web;
using System.Web.Optimization;

namespace Sarif_Industries
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Content/vendor/jquery/jquery-3.2.1.min.js",
                "~/Content/vendor/animsition/js/animsition.min.js",
                "~/Content/vendor/bootstrap/js/popper.js",
                "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                "~/Content/vendor/select2/select2.min.js",
                "~/Content/vendor/slick/slick.min.js",
                "~/Scripts/slick-custom.js",
                "~/Content/vendor/sweetalert/sweetalert.min.js",
                "~/Content/vendor/noui/nouislider.min.js",
                "~/Content/vendor/countdowntime/countdowntime.js",
                "~/Content/vendor/lightbox2/js/lightbox.min.js",
                "~/Content/vendor/daterangepicker/moment.min.js",
                "~/Content/vendor/daterangepicker/daterangepicker.js",
                "~/Scripts/Cart.js",
                "~/Scripts/main.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                      "~/fonts/font-awesome-4.7.0/css/font-awesome.min.css",
                      "~/fonts/themify/themify-icons.css",
                      "~/fonts/Linearicons-Free-v1.0.0/icon-font.min.css",
                      "~/fonts/elegant-font/html-css/style.css",
                      "~/Content/vendor/animate/animate.css",
                      "~/Content/vendor/css-hamburgers/hamburgers.min.css",
                      "~/Content/vendor/animsition/css/animsition.min.css",
                      "~/Content/vendor/select2/select2.min.css",
                      "~/Content/vendor/daterangepicker/daterangepicker.css",
                      "~/Content/vendor/slick/slick.css",
                      "~/Content/vendor/lightbox2/css/lightbox.min.css",
                      "~/Content/vendor/noui/nouislider.min.css",
                      "~/Content/util.css",
                      "~/Content/main.css",
                      "~/Content/product_shop.css"
                      ));
        }
    }
}
