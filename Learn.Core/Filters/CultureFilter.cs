using Learn.Log;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Web.Mvc;

namespace Learn.Core.Filters
{
    public class CultureFilter : IAuthorizationFilter
    {
        private readonly string defaultCulture;
        private readonly string defaultRegion;

        public CultureFilter(string defaultCulture)
        {
            this.defaultCulture = defaultCulture;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var values = filterContext.RouteData.Values;
            string culture = (string)values["culture"] ?? this.defaultCulture;
            CultureInfo ci;
            switch (culture)
            {
                case "sc":
                    ci = new CultureInfo("zh-CHS");
                    break;
                case "en":
                    ci = new CultureInfo("en-US");
                    break;
                case "tc":
                default:
                    ci = new CultureInfo("zh-CHT");
                    break;
            }

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}