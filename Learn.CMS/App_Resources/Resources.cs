using System;
using System.IO;
using System.Threading;

namespace Learn.CMS.App_Resources
{
    using Learn.Core.Localization;
    using System.Web;

    /// <summary>
    /// http://afana.me/archive/2013/11/01/aspnet-mvc-internationalization-store-strings-in-database-or-xml.aspx/
    /// </summary>
    public class Resources
    {
        private static IResourceProvider resourceProvider = new XmlResourceProvider(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Resources\Resources.xml"));

        public static string GetResoureByKey(string resourceKey, string culture = null, string region = null)
        {
            if (culture == null)
            {
                culture = Thread.CurrentThread.CurrentUICulture.Name;
            }
            if (region == null)
            {
                var routeData = HttpContext.Current.Request.RequestContext.RouteData;
                region = (routeData.Values["region"] ?? Area.TW).ToString().ToUpper();
            }
            return (string)resourceProvider.GetResource(resourceKey, culture, region);
        }

        public static string RequiredErrorMessage
        {
            get
            {
                return GetResoureByKey("RequiredErrorMessage");
            }
        }
    }
}