using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Learn.Lib.Extensions
{
    public static class HTMLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string area = null, string controller = null, List<string> controllers = null, string action = null, List<string> actions = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            string currentArea = (string)html.ViewContext.RouteData.DataTokens["area"];

            if (String.IsNullOrEmpty(area))
                area = currentArea;

            if (String.IsNullOrEmpty(controller) && controllers == null)
                controller = currentController;

            if (String.IsNullOrEmpty(action) && actions == null)
                action = currentAction;

            return area == currentArea
                   && (controller == currentController || (controllers != null && controllers.Contains(currentController)))
                   && (action == currentAction || (actions != null && actions.Contains(currentAction))) ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}
