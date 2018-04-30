using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Learn.Lib.Attributes
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public string FunctionID { get; set; }
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return _CommonRepository.GetUserAccessRight(AccessLevel, FunctionID);
            return base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("Unauthorized", "Home"));                
            }
            else
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Account", new {  returnUrl = HttpContext.Current.Request.Url.AbsoluteUri  }));
            }                        
        }

    }
}
