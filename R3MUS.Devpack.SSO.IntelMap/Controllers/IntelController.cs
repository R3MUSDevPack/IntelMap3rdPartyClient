using System.Web.Mvc;

namespace R3MUS.Devpack.SSO.IntelMap.Controllers
{
    [Authorize]
    public class IntelController : Controller
    {
        // GET: Intel
        public ActionResult Index()
        {
            ViewBag.InitialMap = "Deklein";
            return View();
        }
    }
}