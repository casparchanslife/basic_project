using Learn.Core.DataEnum;
using Learn.Core.Services;
using System.Web;
using System.Web.Mvc;
using System.Linq;
namespace Learn.Core.Attributes
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public string functionID { get; set; }
        public string accessLevel { get; set; }
        public IFunctionRoleService functionRoleService { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }
            else
            {
                var accessRightList = functionRoleService.GetAuthenticatedUserRolesAccessRight();

                foreach (var accessLevel in accessLevel.Split(','))
                {
                    switch (accessLevel)
                    {
                        case AccessLevelType.Read:
                            return accessRightList.Count(o => o.function_id == functionID && o.can_read) > 0;
                        case AccessLevelType.Insert:
                            return accessRightList.Count(o => o.function_id == functionID && o.can_insert) > 0;
                        case AccessLevelType.Update:
                            return accessRightList.Count(o => o.function_id == functionID && o.can_update) > 0;
                        case AccessLevelType.Delete:
                            return accessRightList.Count(o => o.function_id == functionID && o.can_delete) > 0;
                    }
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("Unauthorized", "Home", new { Area = "" }));
            }
            else
            {
                filterContext.Result = new RedirectResult(urlHelper.Action("Login", "Account", new { returnUrl = HttpContext.Current.Request.Url.AbsoluteUri, Area = "Admin" }));
            }
        }

    }
}
