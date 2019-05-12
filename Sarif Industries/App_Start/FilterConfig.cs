using System.Web;
using System.Web.Mvc;
using Sarif_Industries.Controllers;
namespace Sarif_Industries
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
