using R3MUS.Devpack.SSO.IntelMap.Helpers;
using R3MUS.Devpack.SSO.IntelMap.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace R3MUS.Devpack.SSO.IntelMap.Controllers
{
    [Authorize]
    public class IntelController : Controller
    {
        // GET: Intel
        public ActionResult Index()
        {
            var path = Server.MapPath("~/Maps");
            var dInfo = new System.IO.DirectoryInfo(path);
            var maps = new Dictionary<string, string>();
            dInfo.GetFiles().ToList().ForEach(f => maps.Add(f.Name.Replace(".svg", ""), f.Name.Replace(".svg", "")));

            if (System.IO.Directory.Exists(string.Concat(path, "/", SSOUserManager.SiteUser.GroupName)))
            {
                dInfo = new System.IO.DirectoryInfo(string.Concat(path, "/", SSOUserManager.SiteUser.GroupName));
                dInfo.GetFiles().ToList().ForEach(f => maps[f.Name.Replace(".svg", "")] = string.Concat(SSOUserManager.SiteUser.GroupName, "/", f.Name.Replace(".svg", "")));
            }

            var viewModel = new MapFiles();
            maps.OrderBy(o => o.Key).ForEach(f => viewModel.Add(f.Key, f.Value));
            viewModel.InitialMap = maps["Deklein"];

            //ViewBag.InitialMap = "Deklein";
            return View(viewModel);
        }
    }
}