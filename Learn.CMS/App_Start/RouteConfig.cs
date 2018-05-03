using Learn.Core.Localization.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Learn.CMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
               name: "Default2",
               url: "{region}/{culture}/",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, region = "hk", culture = "tc" },
               constraints: new { region = new RegionConstraint(pattern: "tw|hk|cn"), culture = new CultureConstraint(pattern: "tc|en|sc") }
            );

            routes.MapRoute(
                name: "RegionWithCulture",
                url: "{region}/{culture}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, region = "hk", culture = "tc" },
                constraints: new { region = new RegionConstraint(pattern: "tw|hk|cn"), culture = new CultureConstraint(pattern: "tc|en|sc"), id = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, region = "hk", culture = "tc" }
            );
        }
    }
}
