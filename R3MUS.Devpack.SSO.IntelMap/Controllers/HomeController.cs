using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using R3MUS.Devpack.SSO.IntelMap.Helpers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.SSO.IntelMap.Controllers
{
    public class HomeController : Controller
    {
        private readonly EveAuthenticationService _authenticationService;

        public HomeController()
        {
            _authenticationService = new EveAuthenticationService();
        }

        public ActionResult Index()
        {
            return View();
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