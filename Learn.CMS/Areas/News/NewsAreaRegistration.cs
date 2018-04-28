using System.Web.Mvc;

namespace Learn.CMS.Areas.News
{
    public class NewsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "News";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "News",
                "News/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Learn.CMS.Areas.News.Controllers" }
            );
        }
    }
}