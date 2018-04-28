using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;

namespace Learn.Lib.Extensions
{
    public static class LabelForExtensions
    {
        #region BootstrapLabelFor
        public static MvcHtmlString BootstrapLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapLabelFor(expression, htmlAttributes);
        }
        public static MvcHtmlString BootstrapLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapLabelFor(expression, dict);
        }
        public static MvcHtmlString BootstrapLabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            List<string> defaultClasses = new List<string>() { "control-label" }; // new List<string>() { "control-label", "col-sm-3", "col-md-3", "col-lg-2" };
            List<string> overridableClasses = new List<string>(); // new List<string>() { "col-lg-", "col-md-", "col-sm-", "col-xs-" };
            if (htmlAttributes.ContainsKey("class"))
            {
                List<string> classValues = htmlAttributes["class"].ToString().Split(' ').ToList();
                foreach (string item in classValues)
                {
                    foreach (string overridableClass in overridableClasses)
                    {
                        if (item.Contains(overridableClass))
                        {
                            string unwantedDefaultClass = defaultClasses.FirstOrDefault(o => o.Contains(overridableClass));
                            if (!String.IsNullOrEmpty(unwantedDefaultClass))
                            {
                                defaultClasses.Remove(unwantedDefaultClass);
                            }
                        }
                    }
                }

                classValues.AddRange(defaultClasses);
                htmlAttributes["class"] = String.Join(" ", classValues);
            }
            else
            {
                htmlAttributes.Add("class", String.Join(" ", defaultClasses));
            }
            return html.LabelFor(expression, htmlAttributes);
        }
        #endregion
    }
}