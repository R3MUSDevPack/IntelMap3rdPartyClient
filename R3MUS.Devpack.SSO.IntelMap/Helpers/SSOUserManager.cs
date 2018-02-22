using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using R3MUS.Devpack.SSO.IntelMap.Extensions;
using R3MUS.Devpack.SSO.IntelMap.Models;
using System;
using System.Threading.Tasks;
using System.Web;

namespace R3MUS.Devpack.SSO.IntelMap.Helpers
{
    public class SSOUserManager : UserManager<SSOApplicationUser>
    {
        public static SSOApplicationUser SiteUser
        {
            get { return (SSOApplicationUser)HttpContext.Current.Session["SiteUser"]; }
            set { HttpContext.Current.Session["SiteUser"] = value; }
        }

        public SSOUserManager(DummyUserStore<SSOApplicationUser> store)
            : base(store)
        {
        }

        public static SSOUserManager Create(IdentityFactoryOptions<SSOUserManager> options, IOwinContext context)
        {
            return new SSOUserManager(new DummyUserStore<SSOApplicationUser>());
        }

        public override Task<SSOApplicationUser> FindByIdAsync(string userId)
        {
            var toon = new ESI.Models.Character.Detail(Convert.ToInt64(userId));

            var siteUser = new SSOApplicationUser()
            {
                Id = userId,
                UserName = toon.Name,
                CorporationId = (long)toon.CorporationId
            };
            siteUser.GenerateUser();
            
            if (HttpContext.Current != null)
            {
                SiteUser = siteUser;
            }

            return Task.FromResult(siteUser);
        }
    }
}