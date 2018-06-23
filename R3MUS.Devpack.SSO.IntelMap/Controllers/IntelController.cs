using R3MUS.Devpack.SSO.IntelMap.Helpers;
using R3MUS.Devpack.SSO.IntelMap.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace R3MUS.Devpack.SSO.IntelMap.Controllers
{
    //[Authorize]
    public class IntelController : Controller
    {
        // GET: Intel
        public ActionResult Index()
        {
            try
            {
                var path = Server.MapPath("~/Maps");
                var dInfo = new System.IO.DirectoryInfo(path);
                var maps = new Dictionary<string, string>();
                dInfo.GetFiles().ToList().ForEach(f => maps.Add(f.Name.Replace(".svg", "").Replace("_", " "), f.Name.Replace(".svg", "")));

                var viewModel = new MapFiles();

                if (SSOUserManager.SiteUser != null && System.IO.Directory.Exists(string.Concat(path, "/", SSOUserManager.SiteUser.GroupName)))
                {
                    dInfo = new System.IO.DirectoryInfo(string.Concat(path, "/", SSOUserManager.SiteUser.GroupName));
                    dInfo.GetFiles().ToList().ForEach(f =>
                    {
                        maps[f.Name.Replace(".svg", "").Replace("_", " ")] = string.Concat(SSOUserManager.SiteUser.GroupName, "/", f.Name.Replace(".svg", ""));
                    });
                }

                maps.OrderBy(o => o.Key).ForEach(f => viewModel.Add(f.Key.Replace("_", " "), f.Value)); ;

                if (SSOUserManager.SiteUser == null)
                {
                    viewModel.InitialMap = maps["The Citadel"];
                }
                else
                {
                    viewModel.InitialMap = maps[SSOUserManager.SiteUser.DefaultRegion];
                }

                return View(viewModel);
            }
            catch
            {
                return RedirectToAction("SSOLogin", "Home");
            }
        }
    }
}