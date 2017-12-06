using System.Web;
using System.Web.Mvc;

namespace R3MUS.Devpack.SSO.IntelMap
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
