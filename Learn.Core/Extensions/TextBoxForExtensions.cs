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
    public static class TextBoxForExtensions
    {
        #region BootstrapTextBoxFor
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.BootstrapTextBox(name, null);
        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapTextBox(name, value, htmlAttributes);
        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return htmlHelper.BootstrapTextBox(name, value, dict);
        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.BootstrapTextBox(name, value, null, htmlAttributes);
        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value, string format)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapTextBox(name, value, format, htmlAttributes);
        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value, string format, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return htmlHelper.BootstrapTextBox(name, value, format, dict);
        }
        public static MvcHtmlString BootstrapTextBox(this HtmlHelper htmlHelper, string name, object value, string format, IDictionary<string, object> htmlAttributes)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            string defaultClasses = "form-control";
            htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);

            return htmlHelper.TextBox(fullName, value, format, htmlAttributes);
        }
        #endregion

        #region BootstrapTextBoxFor
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapTextBoxFor(expression, htmlAttributes);
        }
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapTextBoxFor(expression, dict);
        }
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }
            return html.TextBoxFor(expression, metadata.DisplayFormatString, htmlAttributes);
        }
        #endregion

        #region BootstrapPasswordFor
        public static MvcHtmlString BootstrapPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapPasswordFor(expression, htmlAttributes);
        }
        public static MvcHtmlString BootstrapPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapPasswordFor(expression, dict);
        }
        public static MvcHtmlString BootstrapPasswordFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }
            return html.PasswordFor(expression, htmlAttributes);
        }
        #endregion

        #region DateTimePickerFor
        public static MvcHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.DateTimePickerFor(expression, htmlAttributes);
        }
        public static MvcHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.DateTimePickerFor(expression, dict);
        }
        public static MvcHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control input-date-time-picker";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }

            MvcHtmlString htmlstring = html.TextBoxFor(expression, htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group\"><span class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        public static MvcHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string format, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control input-date-time-picker";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }

            MvcHtmlString htmlstring = html.TextBoxFor(expression, format, htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group\"><span class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        #endregion

        #region DatePickerFor
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.DatePickerFor(expression, htmlAttributes);
        }
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.DatePickerFor(expression, dict);
        }
        public static MvcHtmlString DatePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control input-date-picker";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }

            MvcHtmlString htmlstring = html.TextBoxFor(expression, "{0:dd/MM/yyyy}", htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group\"><span class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        #endregion

        #region TimePicker
        public static MvcHtmlString TimePicker(this HtmlHelper html, string name)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TimePicker(name, htmlAttributes);
        }
        public static MvcHtmlString TimePicker(this HtmlHelper html, string name, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TimePicker(name, dict);
        }
        public static MvcHtmlString TimePicker(this HtmlHelper html, string name, IDictionary<string, object> htmlAttributes)
        {
            string fullName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }
            string defaultClasses = "form-control input-time-picker";
            htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);

            MvcHtmlString htmlstring = html.TextBox(name, null, htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group\"><span class=\"input-group-addon\"><i class=\"fa fa-clock-o\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        #endregion

        #region TimePickerFor
        public static MvcHtmlString TimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TimePickerFor(expression, htmlAttributes);
        }
        public static MvcHtmlString TimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TimePickerFor(expression, dict);
        }
        public static MvcHtmlString TimePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control input-time-picker";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }

            MvcHtmlString htmlstring = html.TextBoxFor(expression, htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group\"><span class=\"input-group-addon\"><i class=\"fa fa-clock-o\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        #endregion

        #region TimePicker24HoursFor
        public static MvcHtmlString TimePicker24HoursFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.TimePicker24HoursFor(expression, htmlAttributes);
        }
        public static MvcHtmlString TimePicker24HoursFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.TimePicker24HoursFor(expression, dict);
        }
        public static MvcHtmlString TimePicker24HoursFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control input-time-picker-24";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }

            MvcHtmlString htmlstring = html.TextBoxFor(expression, htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group\"><span class=\"input-group-addon\"><i class=\"fa fa-clock-o\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        #endregion

        #region ClockPickerFor
        public static MvcHtmlString ClockPickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.ClockPickerFor(expression, htmlAttributes);
        }
        public static MvcHtmlString ClockPickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.ClockPickerFor(expression, dict);
        }
        public static MvcHtmlString ClockPickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IDictionary<string, object> htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(labelText))
            {
                string defaultClasses = "form-control";
                htmlAttributes = MergeClasses(htmlAttributes, defaultClasses);
            }

            MvcHtmlString htmlstring = html.TextBoxFor(expression, htmlAttributes);
            htmlstring = new MvcHtmlString("<div class=\"input-group input-clock-picker\"><span class=\"input-group-addon\"><i class=\"fa fa-clock-o\"></i></span>" + htmlstring + "</div>");
            return htmlstring;
        }
        #endregion

        #region DateRangePickerFor
        public static MvcHtmlString DateRangePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> startExpression, Expression<Func<TModel, TValue>> endExpression)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.DateRangePickerFor(startExpression, htmlAttributes, endExpression, htmlAttributes);
        }
        public static MvcHtmlString DateRangePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> startExpression, object startHtmlAttributes,
                                                                                                     Expression<Func<TModel, TValue>> endExpression, object endHtmlAttributes)
        {
            var startDict = new RouteValueDictionary(startHtmlAttributes);
            var endDict = new RouteValueDictionary(endHtmlAttributes);
            return html.DateRangePickerFor(startExpression, startDict, endExpression, endDict);
        }
        public static MvcHtmlString DateRangePickerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> startExpression, IDictionary<string, object> startHtmlAttributes,
                                                                                                     Expression<Func<TModel, TValue>> endExpression, IDictionary<string, object> endHtmlAttributes)
        {
            ModelMetadata startMetadata = ModelMetadata.FromLambdaExpression(startExpression, html.ViewData);
            string startHtmlFieldName = ExpressionHelper.GetExpressionText(startExpression);
            string startLabelText = startMetadata.DisplayName ?? startMetadata.PropertyName ?? startHtmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(startLabelText))
            {
                string defaultClasses = "form-control";
                startHtmlAttributes = MergeClasses(startHtmlAttributes, defaultClasses);
            }

            ModelMetadata endMetadata = ModelMetadata.FromLambdaExpression(endExpression, html.ViewData);
            string endHtmlFieldName = ExpressionHelper.GetExpressionText(endExpression);
            string endLabelText = endMetadata.DisplayName ?? endMetadata.PropertyName ?? endHtmlFieldName.Split('.').Last();
            if (!String.IsNullOrEmpty(endLabelText))
            {
                string defaultClasses = "form-control";
                endHtmlAttributes = MergeClasses(endHtmlAttributes, defaultClasses);
            }

            MvcHtmlString startHtmlstring = html.TextBoxFor(startExpression, startHtmlAttributes);
            MvcHtmlString endHtmlstring = html.TextBoxFor(endExpression, endHtmlAttributes);
            MvcHtmlString htmlstring = new MvcHtmlString(
                "<div class=\"input-group input-daterange\">" +
                    startHtmlstring +
                    "<span class=\"input-group-addon\">to</span>" +
                    endHtmlstring +
                "</div>"
            );
            return htmlstring;
        }
        #endregion

        private static IDictionary<string, object> MergeClasses(IDictionary<string, object> htmlAttributes, string defaultClasses)
        {
            if (htmlAttributes == null)
            {
                htmlAttributes = new Dictionary<string, object>();
            }
            if (htmlAttributes.ContainsKey("class"))
            {
                htmlAttributes["class"] = htmlAttributes["class"] + " " + defaultClasses;
            }
            else
            {
                htmlAttributes.Add("class", defaultClasses);
            }
            return htmlAttributes;
        }
    }
}
