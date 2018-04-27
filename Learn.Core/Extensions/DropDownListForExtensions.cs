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
    public static class DropDownListForExtensions
    {
        // MVC 5.2
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name)
        {
            return htmlHelper.BootstrapDropDownList(name, string.Empty);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapDropDownList(name, selectList, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return htmlHelper.BootstrapDropDownList(name, selectList, dict);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.BootstrapDropDownList(name, selectList, String.Empty, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, string optionLabel)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapDropDownList(name, null, optionLabel, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapDropDownList(name, selectList, optionLabel, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return htmlHelper.BootstrapDropDownList(name, selectList, optionLabel, dict);
        }
        public static MvcHtmlString BootstrapDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes, string selectedValue = null)
        {
            string fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("name");
            }

            // If we got a null SelectList, try to use ViewData to get the list of item
            if (selectList == null)
            {
                selectList = htmlHelper.GetSelectList(fullName);
            }

            string defaultClasses = "form-control";
            if (htmlAttributes != null)
            {
                if (htmlAttributes.ContainsKey("class"))
                {
                    htmlAttributes["class"] = htmlAttributes["class"] + " " + defaultClasses;
                }
                else
                {
                    htmlAttributes.Add("class", defaultClasses);
                }
            }
            else
            {
                htmlAttributes = new RouteValueDictionary();
                htmlAttributes.Add("class", defaultClasses);
            }

            MvcHtmlString htmlString = null;
            if (selectList == null)
            {
                htmlString = htmlHelper.DropDownList(name, selectList, htmlAttributes);
            }
            else
            {
                htmlString = htmlHelper.DropDownList(name, selectList, optionLabel, htmlAttributes);
            }
            return htmlString;

            // Old Logic: Build all code by ourselves
            /*
            TagBuilder selectTagBuilder = new TagBuilder("select");
            if (htmlAttributes.ContainsKey("id"))
            {
                selectTagBuilder.Attributes.Add("id", (string)htmlAttributes["id"]);
            }
            else
            {
                selectTagBuilder.Attributes.Add("id", fullName.Replace('.', '_').Replace('[', '_').Replace(']', '_').Replace(' ', '_'));
            }
            selectTagBuilder.Attributes.Add("name", fullName);
            selectTagBuilder.MergeAttributes<string, object>(htmlAttributes);
            string wrapStart = selectTagBuilder.ToString(TagRenderMode.StartTag);
            string wrapClose = selectTagBuilder.ToString(TagRenderMode.EndTag);

            string options = String.Empty;
            if (selectList != null)
            {
                MvcHtmlString ddlHtml = htmlHelper.DropDownList(name, selectList, optionLabel, htmlAttributes);
                // Remove <select></select> tags from the code rendered by original html helper
                options = System.Text.RegularExpressions.Regex.Replace(ddlHtml.ToString(), "(?i)</?select[^>]*>", "");
            }

            // Older Logic
            if (selectList != null)
            {
                StringBuilder optionsSb = new StringBuilder();

                TagBuilder tagBuilder = new TagBuilder("option");
                // Make optionLabel the first item that gets rendered.
                if (optionLabel != null)
                {
                    tagBuilder.MergeAttribute("value", String.Empty);
                    tagBuilder.InnerHtml = optionLabel;
                    optionsSb.Append(tagBuilder.ToString(TagRenderMode.Normal));
                }

                foreach (var item in selectList)
                {
                    tagBuilder = new TagBuilder("option");
                    tagBuilder.MergeAttribute("value", item.Value.ToString());
                    if (item.Selected || selectedValue == item.Value)
                    {
                        tagBuilder.MergeAttribute("selected", "selected", true);
                    }
                    if (item.Disabled) { tagBuilder.MergeAttribute("disabled", "disabled", true); }
                    tagBuilder.InnerHtml = item.Text;

                    optionsSb.Append(tagBuilder.ToString(TagRenderMode.Normal));
                }
                options = optionsSb.ToString();
            }
            // Older logic end

            StringBuilder sb = new StringBuilder();
            sb.Append(wrapStart);
            sb.Append(options);
            sb.Append(wrapClose);

            return new MvcHtmlString(sb.ToString());
            //*/
        }

        #region BootstrapDropDownListFor
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapDropDownListFor(expression, selectList, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return htmlHelper.BootstrapDropDownListFor(expression, selectList, dict);
        }
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.BootstrapDropDownListFor(expression, selectList, null, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return htmlHelper.BootstrapDropDownListFor(expression, selectList, optionLabel, htmlAttributes);
        }
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return htmlHelper.BootstrapDropDownListFor(expression, selectList, optionLabel, dict);
        }
        public static MvcHtmlString BootstrapDropDownListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string selectedValue = (metadata.Model == null) ? "" : metadata.Model.ToString();

            /* // Handle Unobtrusive Validation Attributes
            IDictionary<string, object> validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(htmlFieldName);
            if (htmlAttributes == null)
                htmlAttributes = validationAttributes;
            else
                htmlAttributes = htmlAttributes.Concat(validationAttributes).ToDictionary(k => k.Key, v => v.Value);

            ModelState modelState;
            if (htmlHelper.ViewData.ModelState.TryGetValue(htmlFieldName, out modelState))
            {
                if (modelState.Errors.Count > 0)
                {
                    string s = HtmlHelper.ValidationInputCssClassName;
                }
            }

            var list = selectList.Select(o => new SelectListItem()
            {
                Value = o.Value,
                Text = o.Text,
                Selected = o.Selected
            }).AsEnumerable();

            var ss = htmlHelper.DropDownListFor(expression, list, optionLabel);

            return ss; 
            //*/
            return htmlHelper.BootstrapDropDownList(htmlFieldName, selectList, optionLabel, htmlAttributes, selectedValue);
        }
        #endregion

        private static SelectList GetSelectList(this HtmlHelper htmlHelper, string name)
        {
            object o = null;
            if (htmlHelper.ViewData != null)
            {
                o = htmlHelper.ViewData.Eval(name);
            }
            SelectList selectList = new SelectList(new List<SelectListItem>());
            if (o == null)
            {
                selectList = new SelectList(new List<SelectListItem>());
                //throw new InvalidOperationException("Missing Select Data");
            }
            else
            {
                selectList = o as SelectList;
                if (selectList == null)
                {
                    selectList = new SelectList(new List<SelectListItem>());
                    //throw new InvalidOperationException("Wrong Select Data Type");
                }
            }
            return selectList;
        }
    }
}