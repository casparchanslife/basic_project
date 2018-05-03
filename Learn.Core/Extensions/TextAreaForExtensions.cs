using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Learn.Core.Extensions
{
    public static class TextAreaForExtensions
    {
        #region BootstrapTextAreaFor
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapTextAreaFor(expression, htmlAttributes);
        }
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapTextAreaFor(expression, dict);
        }
        public static MvcHtmlString BootstrapTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                if (htmlAttributes == null)
                {
                    htmlAttributes = new Dictionary<string, object>();
                }
                string defaultClasses = "form-control";
                if (htmlAttributes.ContainsKey("class"))
                {
                    htmlAttributes["class"] = htmlAttributes["class"] + " " + defaultClasses;
                }
                else
                {
                    htmlAttributes.Add("class", defaultClasses);
                }
            }
            return html.TextAreaFor(expression, htmlAttributes);
        }
        #endregion

        #region CKEditorTextAreaFor
        public static MvcHtmlString CKEditorTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.CKEditorTextAreaFor(expression, htmlAttributes);
        }
        public static MvcHtmlString CKEditorTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.CKEditorTextAreaFor(expression, dict);
        }
        public static MvcHtmlString CKEditorTextAreaFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                if (htmlAttributes == null)
                {
                    htmlAttributes = new Dictionary<string, object>();
                }
                string defaultClasses = "form-control ckeditor";
                if (htmlAttributes.ContainsKey("class"))
                {
                    htmlAttributes["class"] = htmlAttributes["class"] + " " + defaultClasses;
                }
                else
                {
                    htmlAttributes.Add("class", defaultClasses);
                }
            }
            return html.TextAreaFor(expression, htmlAttributes);
        }
        #endregion
    }
}
