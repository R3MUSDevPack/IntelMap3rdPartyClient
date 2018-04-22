using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Owin;
using R3MUS.Devpack.SSO.IntelMap.Helpers;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;
using System.Web.Helpers;
using System.Web.Http;

[assembly: OwinStartup(typeof(R3MUS.Devpack.SSO.IntelMap.App_Start.Startup))]
namespace R3MUS.Devpack.SSO.IntelMap.App_Start
{
    public partial class Startup
    {
        public static IKernel Container { get; set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            Container = new StandardKernel();
            Container.Load(Assembly.GetExecutingAssembly());
            ConfigureBindings(Container);

            config.DependencyResolver = new NinjectResolver(Container);
            app.UseNinjectMiddleware(() => Container);

            ConfigureAuth(app);
            ConfigureSignalR(app);
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