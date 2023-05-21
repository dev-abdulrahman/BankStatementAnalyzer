using System.Web.Optimization;

namespace BankStatementAnalyzer.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Content/theme/assets/vendor_components/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Content/theme/assets/vendor_components/popper/dist/popper.min.js",
                "~/Content/theme/assets/vendor_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Content/theme/assets/vendor_components/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/theme/assets/vendor_components/fastclick/lib/fastclick.js",
                "~/Content/theme/js/template.js",
               "~/Content/theme/js/demo.js",
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/jquery.toast.js",
                "~/Scripts/bootstrap-datepicker.min.js",
                "~/Scripts/c3.min.js",
                "~/Scripts/d3.min.js",
                 "~/Scripts/sweetalert.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/loginScripts").Include(
               "~/Content/theme/assets/vendor_components/popper/dist/popper.min.js",
               "~/Content/theme/assets/vendor_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Scripts/jquery.toast.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/DatatableCheckbox").Include(
                       "~/Scripts/dataTables.checkboxes.min.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/ImageUplodify").Include(
                      "~/Scripts/imageuploadify.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/Select2").Include(
                     "~/Scripts/select2.min.js"
                    ));

            bundles.Add(new StyleBundle("~/Content/Select2").Include(
          "~/Content/select2.min.css"));

            bundles.Add(new StyleBundle("~/Content/DatatableCheckbox").Include(
          "~/Content/dataTables.checkboxes.css"));

            bundles.Add(new StyleBundle("~/content/ImageUplodify").Include(
                "~/Content/imageuploadify.min.css"
                ));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/theme/assets/vendor_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/theme/css/bootstrap-extend.css",
                      "~/Content/theme/css/master_style.css",
                      "~/Content/theme/css/skins/_all-skins.css",
                      "~/Content/jquery.dataTables.min.css",
                      "~/Content/theme/css/font-awesome.css",
                      "~/Content/custom-style.css",
                      "~/Content/jquery.toast.css",
                      "~/Content/bootstrap-datepicker.min.css",
                      "~/Content/c3.min.css",
                      "~/Content/sweetalert.css"
                      ));
        }
    }
}