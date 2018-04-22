using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Ninject;
using R3MUS.Devpack.SSO.IntelMap.App_Start;
using R3MUS.Devpack.SSO.IntelMap.Extensions;
using R3MUS.Devpack.SSO.IntelMap.Models;
using R3MUS.Devpack.SSO.IntelMap.Services;
using System;
using System.Threading.Tasks;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Helpers
{
    public class SSOUserManager : UserManager<SSOApplicationUser>
    {
        [Inject]
        public ISSOUserService SSOUserService { get; set; }

        public static SSOApplicationUser SiteUser
        {
            get { return (SSOApplicationUser)HttpContext.Current.Session["SiteUser"]; }
            set { HttpContext.Current.Session["SiteUser"] = value; }
        }

        public SSOUserManager(DummyUserStore<SSOApplicationUser> store)
            : base(store)
        {
            Startup.Container.Inject(this);
        }

        public static SSOUserManager Create()
        {
            return new SSOUserManager(new DummyUserStore<SSOApplicationUser>());
        }
        
        public override Task<SSOApplicationUser> FindByIdAsync(string userId)
        {
            var siteUser = SSOUserService.CreateUser(userId);
            
            if (HttpContext.Current != null)
            {
                SiteUser = siteUser;
            }

            return Task.FromResult(siteUser);
        }
    }
}