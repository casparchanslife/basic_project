using Learn.Core.Filters;
using System.Web;
using System.Web.Mvc;

namespace Learn.CMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CultureFilter(defaultCulture: "tc"));
            filters.Add(new HandleErrorAttribute());
        }
    }
}
