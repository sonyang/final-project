using System.Web;
using System.Web.Optimization;

namespace BookReview {
  public class BundleConfig {
    // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
    public static void RegisterBundles(BundleCollection bundles) {
      BundleTable.EnableOptimizations = true;

      bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
      bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
      bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));
      bundles.Add(new ScriptBundle("~/bundles/site").Include("~/Scripts/site.js"));

      bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/font-awesome.min.css", "~/Content/site.css"));
    }
  }
}
