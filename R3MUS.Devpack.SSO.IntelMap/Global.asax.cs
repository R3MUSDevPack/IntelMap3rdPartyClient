using R3MUS.Devpack.SSO.IntelMap.Helpers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace R3MUS.Devpack.SSO.IntelMap
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_PostAuthenticationRequest(object sender, EventArgs args)
        {
            if (SSOUserManager.SiteUser != null)
            {
                HttpContext.Current.User = SSOUserManager.SiteUser;
            }
        }
    }
}
