using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookReview {
  public class MvcApplication : System.Web.HttpApplication {
    protected void Application_Start() {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
    protected void Application_End() {
      // When the application is shutting down.
    }

    protected void Session_Start() {
      // User connects to site for the first time.
      Session["DateNow"] = DateTime.Now;
    }

    protected void Session_End() {
      // Session Timeout (20 minutes) or Browser Close.
      //Session.Abandon(); // This is what calls Session_End().... infinite loop waiting to happen.
    }
  }

}
