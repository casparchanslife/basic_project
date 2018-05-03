using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace Learn.Core.Localization.Constraints
{
    public class RegionConstraint : IRouteConstraint
    {
        private readonly string pattern;
        private readonly string defaultRegion;

        public RegionConstraint(string pattern, string defaultRegion = null)
        {
            this.pattern = pattern;
            this.defaultRegion = defaultRegion;
        }

        public bool Match(
            HttpContextBase httpContext,
            Route route,
            string parameterName,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (routeDirection == RouteDirection.UrlGeneration && defaultRegion != null && this.defaultRegion.Equals(values[parameterName]))
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