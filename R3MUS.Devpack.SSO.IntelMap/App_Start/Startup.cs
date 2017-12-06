using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using R3MUS.Devpack.SSO.IntelMap.Helpers;
using System;
using System.Security.Claims;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(R3MUS.Devpack.SSO.IntelMap.App_Start.Startup))]
namespace R3MUS.Devpack.SSO.IntelMap.App_Start
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<SSOUserManager>(SSOUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/SSOLogin"),
                ExpireTimeSpan = TimeSpan.FromDays(1)
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}