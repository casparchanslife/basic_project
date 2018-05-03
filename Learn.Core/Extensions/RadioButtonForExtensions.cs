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
    public static class RadioButtonForExtensions
    {
        #region BootstrapRadioButton
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapRadioButton(name, value, false);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, bool isChecked)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapRadioButton(name, value, isChecked, htmlAttributes);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButton(name, value, false, dict);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapRadioButton(name, value, false, htmlAttributes);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, bool isChecked, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButton(name, value, isChecked, dict);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapRadioButton(name, value, String.Empty, isChecked, htmlAttributes);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, string text, bool isChecked, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButton(name, value, text, isChecked, dict);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, string text, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapRadioButton(name, String.Empty, text, isChecked, false, htmlAttributes);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, string text, bool isChecked, bool isDisabled, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButton(name, value, text, isChecked, isDisabled, dict);
        }
        public static MvcHtmlString BootstrapRadioButton(this HtmlHelper html, string name, object value, string text, bool isChecked, bool isDisabled, IDictionary<string, object> htmlAttributes)
        {
            string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            //<div class="radio i-checks"><label><input type="radio" name="@name">@text</label></div>
            string wrapStart = @"<div class=""radio i-checks""><label>";
            string wrapClose = @"</label></div>";

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes.Add("id", fullName.Replace('.', '_'));
            tagBuilder.Attributes.Add("name", fullName);
            tagBuilder.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder.MergeAttribute("type", "radio");
            if (value != null) { tagBuilder.MergeAttribute("value", value.ToString()); }
            if (isChecked) { tagBuilder.MergeAttribute("checked", "checked", true); }
            if (isDisabled) { tagBuilder.MergeAttribute("disabled", "disabled", true); }
            tagBuilder.InnerHtml = text;

            StringBuilder sb = new StringBuilder();
            sb.Append(wrapStart);
            sb.Append(tagBuilder.ToString(TagRenderMode.Normal));
            sb.Append(wrapClose);

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region BootstrapRadioButtonFor
        public static MvcHtmlString BootstrapRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object value, string text)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapRadioButtonFor(expression, value, text, htmlAttributes);
        }
        public static MvcHtmlString BootstrapRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object value, string text, IDictionary<string, object> htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButtonFor(expression, value, text, dict);
        }
        public static MvcHtmlString BootstrapRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object value, string text, object htmlAttributes)
        {
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            return html.BootstrapRadioButton(htmlFieldName, value, text, false, htmlAttributes);
        }
        #endregion

        #region BootstrapRadioButtonList
        public static MvcHtmlString BootstrapRadioButtonList(this HtmlHelper html, string name, IEnumerable<SelectListItem> items, string selectedValue = null)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapRadioButtonList(name, items, htmlAttributes, selectedValue);
        }
        public static MvcHtmlString BootstrapRadioButtonList(this HtmlHelper html, string name, IEnumerable<SelectListItem> items, object htmlAttributes, string selectedValue = null)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButtonList(name, items, dict, selectedValue);
        }
        public static MvcHtmlString BootstrapRadioButtonList(this HtmlHelper html, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> htmlAttributes, string selectedValue = null)
        {
            var output = new StringBuilder();

            foreach (var item in items)
            {
                if (item.Value == selectedValue)
                {
                    item.Selected = true;
                }
                output.Append(html.BootstrapRadioButton(name, item.Value, item.Text, item.Selected, item.Disabled, htmlAttributes));
            }

            return new MvcHtmlString(output.ToString());
        }
        #endregion

        #region BootstrapRadioButtonListFor
        public static MvcHtmlString BootstrapRadioButtonListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapRadioButtonListFor(expression, selectList, htmlAttributes);
        }
        public static MvcHtmlString BootstrapRadioButtonListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapRadioButtonListFor(expression, selectList, dict);
        }
        public static MvcHtmlString BootstrapRadioButtonListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string selectedValue = (metadata.Model == null) ? "" : metadata.Model.ToString();

            return html.BootstrapRadioButtonList(htmlFieldName, selectList, htmlAttributes, selectedValue);
        }
        #endregion
    }
}
