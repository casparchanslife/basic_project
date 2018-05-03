using Learn.CMS.App_Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Learn.CMS.Extensions
{
    public static class HTMLHelperExtensions
    {
        public static HtmlString GetLocalizedString(this HtmlHelper htmlHelper, string resourceKey, string culture = null, string region = null)
        {
            return new HtmlString(Resources.GetResoureByKey(resourceKey, culture, region));
        }
    }
}
