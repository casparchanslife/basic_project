using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Learn.Core.Localization.Constraints
{
    public class CultureConstraint : IRouteConstraint
    {
        private readonly string pattern;
        private readonly string defaultCulture;

        public CultureConstraint(string pattern, string defaultCulture = null)
        {
            this.pattern = pattern;
            this.defaultCulture = defaultCulture;
        }

        public bool Match(
            HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration && defaultCulture != null && this.defaultCulture.Equals(values[parameterName]))
            {
                return false;
            }
            else
            {
                return Regex.IsMatch((string)values[parameterName], "^" + pattern + "$");
            }
        }
    }
}