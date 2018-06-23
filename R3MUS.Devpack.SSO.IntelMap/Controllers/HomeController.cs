using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using R3MUS.Devpack.SSO.IntelMap.Helpers;
using R3MUS.Devpack.SSO.IntelMap.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.SSO.IntelMap.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEveAuthenticationService _authenticationService;

        public HomeController(IEveAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ActionResult GankIntel()
        {
            var ident = _authenticationService.GenerateIdentity();
            ident.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.GroupSid,
                "Gank-Intel"));
            var authManager = HttpContext.GetOwinContext().Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(
                new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false },
                ident
                );

            return RedirectToAction("Index", "Intel");
        }

        public ActionResult SSOLogin()
        {
            return _authenticationService.SSOLogin();
        }

        public ActionResult SSOReturn()
        {
            try
            {
                var context = HttpContext.GetOwinContext();
                var userManager = context.GetUserManager<SSOUserManager>();
                var authManager = context.Authentication;
                var authToken = _authenticationService.GetAuthToken(Request.Url);

                var ident = _authenticationService.GenerateIdentity(authToken);
                userManager.FindByIdAsync(ident.GetUserId());
                ident.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.GroupSid,
                    SSOUserManager.SiteUser.GroupName));

                authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(
                    new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = false },
                    ident
                    );

                return RedirectToAction("Index", "Intel");
            }
            catch (Exception ex)
            {
                return RedirectToAction("SSOLogin");
            }
        }
    }
}