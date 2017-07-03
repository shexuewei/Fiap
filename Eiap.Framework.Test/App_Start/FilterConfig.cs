using System.Web;
using System.Web.Mvc;

namespace Eiap.Framework.Test
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
