using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Learn.Lib.Extensions
{
    /*
    public static class CheckBoxListExtensions
    {
        #region -- CheckBoxListVertical --
        /// <summary>
        /// Checks the box list vertical.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="listInfo">The list info.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="columnNumber">The column number.</param>
        /// <returns></returns>
        public static string CheckBoxListVertical(this HtmlHelper htmlHelper,
            string name,
            List<CheckBoxListInfo> listInfo,
            IDictionary<string, object> htmlAttributes,
            int columnNumber = 1)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("必須給這些CheckBoxList一個Tag Name", "name");
            }
            if (listInfo == null)
            {
                throw new ArgumentNullException("listInfo", "必須要給List<CheckBoxListInfo> listInfo");
            }
            if (listInfo.Count < 1)
            {
                throw new ArgumentException("List<CheckBoxListInfo> listInfo 至少要有一組資料", "listInfo");
            }

            int dataCount = listInfo.Count;

            // calculate number of rows
            int rows = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataCount) / Convert.ToDecimal(columnNumber)));
            if (dataCount <= columnNumber || dataCount - columnNumber == 1)
            {
                rows = dataCount;
            }

            TagBuilder wrapBuilder = new TagBuilder("div");
            wrapBuilder.MergeAttribute("style", "float: left; light-height: 25px; padding-right: 5px;");

            string wrapStart = wrapBuilder.ToString(TagRenderMode.StartTag);
            string wrapClose = string.Concat(wrapBuilder.ToString(TagRenderMode.EndTag), " <div style=\"clear:both;\"></div>");
            string wrapBreak = string.Concat("</div>", wrapBuilder.ToString(TagRenderMode.StartTag));

            StringBuilder sb = new StringBuilder();
            sb.Append(wrapStart);

            int lineNumber = 0;

            foreach (CheckBoxListInfo info in listInfo)
            {
                TagBuilder builder = new TagBuilder("input");
                if (info.IsChecked)
                {
                    builder.MergeAttribute("checked", "checked");
                }
                builder.MergeAttributes<string, object>(htmlAttributes);
                builder.MergeAttribute("type", "checkbox");
                builder.MergeAttribute("value", info.Value);
                builder.MergeAttribute("name", name);
                sb.Append(builder.ToString(TagRenderMode.Normal));

                TagBuilder labelBuilder = new TagBuilder("label");
                labelBuilder.MergeAttribute("for", name);
                labelBuilder.InnerHtml = info.DisplayText;
                sb.Append(labelBuilder.ToString(TagRenderMode.Normal));

                lineNumber++;

                if (lineNumber.Equals(rows))
                {
                    sb.Append(wrapBreak);
                    lineNumber = 0;
                }
                else
                {
                    sb.Append("<br/>");
                }
            }
            sb.Append(wrapClose);
            return MvcHtmlString.Create(sb.ToString()).ToString();
        }
        #endregion

        #region -- CheckBoxList (Horizonal, Vertical) --
        /// <summary>
        /// Checks the box list.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="listInfo">The list info.</param>
        /// <param name="htmlAttributes">The HTML attributes.</param>
        /// <param name="position">The position.</param>
        /// <param name="number">Position.Horizontal則表示每個Row的顯示個數, Position.Vertical則表示要顯示幾個Column</param>
        /// <returns></returns>
        public static string CheckBoxList(this HtmlHelper htmlHelper,
            string name,
            List<CheckBoxListInfo> listInfo,
            IDictionary<string, object> htmlAttributes,
            Position position = Position.Horizontal,
            int number = 0)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("必須給這些CheckBoxList一個Tag Name", "name");
            }
            if (listInfo == null)
            {
                throw new ArgumentNullException("listInfo", "必須要給List<CheckBoxListInfo> listInfo");
            }
            if (listInfo.Count < 1)
            {
                throw new ArgumentException("List<CheckBoxListInfo> listInfo 至少要有一組資料", "listInfo");
            }

            StringBuilder sb = new StringBuilder();
            int lineNumber = 0;

            switch (position)
            {
                case Position.Horizontal:

                    foreach (CheckBoxListInfo info in listInfo)
                    {
                        lineNumber++;
                        sb.Append(CreateCheckBoxItem(info, name, htmlAttributes));

                        if (number == 0 || (lineNumber % number == 0))
                        {
                            sb.Append("<br />");
                        }
                    }
                    break;

                case Position.Vertical:

                    int dataCount = listInfo.Count;

                    // 計算最大顯示的列數(rows)
                    int rows = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataCount) / Convert.ToDecimal(number)));
                    if (dataCount <= number || dataCount - number == 1)
                    {
                        rows = dataCount;
                    }

                    TagBuilder wrapBuilder = new TagBuilder("div");
                    wrapBuilder.MergeAttribute("style", "float: left; light-height: 25px; padding-right: 5px;");

                    string wrapStart = wrapBuilder.ToString(TagRenderMode.StartTag);
                    string wrapClose = string.Concat(wrapBuilder.ToString(TagRenderMode.EndTag), " <div style=\"clear:both;\"></div>");
                    string wrapBreak = string.Concat("</div>", wrapBuilder.ToString(TagRenderMode.StartTag));

                    sb.Append(wrapStart);

                    foreach (CheckBoxListInfo info in listInfo)
                    {
                        lineNumber++;
                        sb.Append(CreateCheckBoxItem(info, name, htmlAttributes));

                        if (lineNumber.Equals(rows))
                        {
                            sb.Append(wrapBreak);
                            lineNumber = 0;
                        }
                        else
                        {
                            sb.Append("<br/>");
                        }
                    }
                    sb.Append(wrapClose);
                    break;
            }

            return MvcHtmlString.Create(sb.ToString()).ToString();
        }

        internal static string CreateCheckBoxItem(CheckBoxListInfo info, string name, IDictionary<string, object> htmlAttributes)
        {
            StringBuilder sb = new StringBuilder();

            TagBuilder builder = new TagBuilder("input");
            if (info.IsChecked)
            {
                builder.MergeAttribute("checked", "checked");
            }
            builder.MergeAttributes<string, object>(htmlAttributes);
            builder.MergeAttribute("type", "checkbox");
            builder.MergeAttribute("value", info.Value);
            builder.MergeAttribute("name", name);
            sb.Append(builder.ToString(TagRenderMode.Normal));

            TagBuilder labelBuilder = new TagBuilder("label");
            labelBuilder.MergeAttribute("for", name);
            labelBuilder.InnerHtml = info.DisplayText;
            sb.Append(labelBuilder.ToString(TagRenderMode.Normal));

            return sb.ToString();
        }
        #endregion
    }
    public enum Position
    {
        Horizontal,
        Vertical
    }
    //*/

    public static class CheckBoxForExtensions
    {
        #region BootstrapCheckBox
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapCheckBox(name, false);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, bool isChecked)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapCheckBox(name, isChecked, htmlAttributes);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBox(name, false, dict);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapCheckBox(name, false, htmlAttributes);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, bool isChecked, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBox(name, isChecked, dict);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapCheckBox(name, String.Empty, isChecked, htmlAttributes);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, string text, bool isChecked, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBox(name, String.Empty, isChecked, dict);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, string text, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapCheckBox(name, String.Empty, text, isChecked, htmlAttributes);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, object value, string text, bool isChecked, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBox(name, value, text, isChecked, dict);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, object value, string text, bool isChecked, IDictionary<string, object> htmlAttributes)
        {
            return html.BootstrapCheckBox(name, String.Empty, text, isChecked, false, htmlAttributes);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, object value, string text, bool isChecked, bool isDisabled, object htmlAttributes)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBox(name, value, text, isChecked, isDisabled, dict);
        }
        public static MvcHtmlString BootstrapCheckBox(this HtmlHelper html, string name, object value, string text, bool isChecked, bool isDisabled, IDictionary<string, object> htmlAttributes)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name");
            }

            string wrapStart = @"<div class=""checkbox i-checks""><label>";
            string wrapClose = @"</label></div>";

            TagBuilder tagBuilder = new TagBuilder("input");
            tagBuilder.Attributes.Add("id", name.Replace('.', '_'));
            tagBuilder.Attributes.Add("name", name);
            tagBuilder.MergeAttributes<string, object>(htmlAttributes);
            tagBuilder.MergeAttribute("type", "checkbox");
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


        public static MvcHtmlString BootstrapCheckBoxFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, bool isDisplayText = true)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapCheckBoxFor(expression, htmlAttributes, isDisplayText);
        }
        public static MvcHtmlString BootstrapCheckBoxFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, object htmlAttributes, bool isDisplayText = true)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBoxFor(expression, dict, isDisplayText);
        }
        public static MvcHtmlString BootstrapCheckBoxFor<TModel>(this HtmlHelper<TModel> html, Expression<Func<TModel, bool>> expression, IDictionary<string, object> htmlAttributes, bool isDisplayText = true)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string indexedName = htmlFieldName;
            string wrapStart = @"<div class=""checkbox i-checks"">";
            string wrapClose = @"</div>";

            var checkbox = html.CheckBoxFor(expression, htmlAttributes);
            var label = String.Format("<label for=\"{0}\"><strong>{1}</strong></label>", htmlFieldName, metadata.DisplayName); // html.Label(indexedName);
            
            StringBuilder sb = new StringBuilder();
            sb.Append(wrapStart);
            sb.Append(checkbox.ToString());
            if (isDisplayText) { sb.Append(label); }
            sb.Append(wrapClose);

            return new MvcHtmlString(sb.ToString());
        }
        private static MvcHtmlString BootstrapCheckBoxFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object value, string text, bool isChecked, bool isDisabled, IDictionary<string, object> htmlAttributes, int index)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);

            string indexedName = htmlFieldName + "[" + index + "]";
            string indexedId = htmlFieldName + "_" + index;
            string wrapStart = @"<div class=""checkbox i-checks"">";
            string wrapClose = @"</div>";

            var hiddenInput = html.Hidden(indexedName + ".Value", value, new { id = indexedId + "__Value" });
            var checkbox = html.CheckBox(indexedName + ".Selected", isChecked, new { id = indexedId + "__Selected" });
            var label = html.Label(indexedId + "__Selected", text);

            StringBuilder sb = new StringBuilder();
            sb.Append(wrapStart);
            sb.Append(hiddenInput.ToString());
            sb.Append(checkbox.ToString());
            sb.Append(label.ToString());
            sb.Append(wrapClose);

            return new MvcHtmlString(sb.ToString());
        }

        #region BootstrapCheckBoxList
        public static MvcHtmlString BootstrapCheckBoxList(this HtmlHelper html, string name, IEnumerable<SelectListItem> items, string selectedValue = null, int columnNumber = 1)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapCheckBoxList(name, items, htmlAttributes, selectedValue, columnNumber);
        }
        public static MvcHtmlString BootstrapCheckBoxList(this HtmlHelper html, string name, IEnumerable<SelectListItem> items, object htmlAttributes, string selectedValue = null, int columnNumber = 1)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBoxList(name, items, dict, selectedValue, columnNumber);
        }
        /// <param name="columnNumber">
        ///     The column number, for display the checkbox list in vertical format
        ///     Cannot be greater than the size of item list
        /// </param>
        public static MvcHtmlString BootstrapCheckBoxList(this HtmlHelper html, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> htmlAttributes, string selectedValue = null, int columnNumber = 1)
        {
            StringBuilder sb = new StringBuilder();

            if (columnNumber <= 1)
            {
                foreach (var item in items)
                {
                    if (item.Value == selectedValue)
                    {
                        item.Selected = true;
                    }
                    sb.Append(html.BootstrapCheckBox(name, item.Value, item.Text, item.Selected, item.Disabled, htmlAttributes));
                }
            }
            else
            {
                int dataCount = items.Count();
                if (columnNumber > 12) { columnNumber = 1; }

                // calculate max. number of rows
                int rows = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataCount) / Convert.ToDecimal(columnNumber)));
                if (dataCount <= columnNumber || dataCount - columnNumber == 1)
                {
                    rows = dataCount;
                }
                // calculate optimum column number
                columnNumber = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(dataCount) / Convert.ToDecimal(rows)));

                TagBuilder rowWrapBuilder = new TagBuilder("div");
                rowWrapBuilder.MergeAttribute("class", "row");
                string rowWrapStart = rowWrapBuilder.ToString(TagRenderMode.StartTag);
                string rowWrapEnd = rowWrapBuilder.ToString(TagRenderMode.EndTag);

                // calculate bootstrap column class size
                int colMdSize = Convert.ToInt32(Math.Floor(Convert.ToDecimal(12) / Convert.ToDecimal(columnNumber)));
                int colSmSize = 6, colXsSize = 12;
                switch (columnNumber)
                {
                    case 2: colMdSize = 6; break;
                    case 3: colMdSize = 4; break;
                    case 4: colMdSize = 3; colSmSize = 4; colXsSize = 6; break;
                    case 5: colMdSize = 3; colSmSize = 4; colXsSize = 6; break;
                    case 6: colMdSize = 2; colSmSize = 3; colXsSize = 4; break;
                    case 7: colMdSize = 2; colSmSize = 3; colXsSize = 4; break;
                    case 8: colMdSize = 2; colSmSize = 3; colXsSize = 4; break;
                    case 9: colMdSize = 2; colSmSize = 3; colXsSize = 4; break;
                    case 10: colMdSize = 2; colSmSize = 3; colXsSize = 4; break;
                    case 11: colMdSize = 2; colSmSize = 3; colXsSize = 4; break;
                    case 12: colMdSize = 1; colSmSize = 2; colXsSize = 3; break;
                }

                TagBuilder colWrapBuilder = new TagBuilder("div");
                colWrapBuilder.MergeAttribute("class", "col-md-" + colMdSize + " col-sm-" + colSmSize + " col-xs-" + colXsSize);
                string colWrapStart = colWrapBuilder.ToString(TagRenderMode.StartTag);
                string colWrapEnd = colWrapBuilder.ToString(TagRenderMode.EndTag);

                // Create html
                sb.Append(rowWrapStart);

                for (int i = 0; i < columnNumber; i++)
                {
                    sb.Append(colWrapStart);
                    sb.Append(html.BootstrapCheckBoxList(name, items.Skip(i * rows).Take(rows), htmlAttributes));
                    sb.Append(colWrapEnd);
                }

                sb.Append(rowWrapEnd);
            }

            return new MvcHtmlString(sb.ToString());
        }
        #endregion

        #region BootstrapCheckBoxListFor
        public static MvcHtmlString BootstrapCheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, int columnNumber = 1)
        {
            var htmlAttributes = new Dictionary<string, object>();
            return html.BootstrapCheckBoxListFor(expression, selectList, htmlAttributes, columnNumber);
        }
        public static MvcHtmlString BootstrapCheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, int columnNumber = 1)
        {
            var dict = new RouteValueDictionary(htmlAttributes);
            return html.BootstrapCheckBoxListFor(expression, selectList, dict, columnNumber);
        }
        public static MvcHtmlString BootstrapCheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes, int columnNumber = 1)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string selectedValue = (metadata.Model == null) ? "" : metadata.Model.ToString();

            return html.BootstrapCheckBoxList(htmlFieldName, selectList, htmlAttributes, selectedValue, columnNumber);
            /*
            StringBuilder sb = new StringBuilder();

            var list = selectList.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(html.BootstrapCheckBoxFor(expression, list[i].Value, list[i].Text, list[i].Selected, list[i].Disabled, htmlAttributes, i));
            }
            return new MvcHtmlString(sb.ToString());
            //*/
        }
        #endregion

        #region BootstrapCheckBoxSelectAll
        public static MvcHtmlString BootstrapCheckBoxSelectAll(this HtmlHelper html, string name, string text, IDictionary<string, object> htmlAttributes)
        {
            StringBuilder sb = new StringBuilder();
            string selectAllName = "select-all-" + name;
            MvcHtmlString checkboxHtml = html.BootstrapCheckBox(selectAllName, text, false, htmlAttributes);

            sb.AppendLine(checkboxHtml.ToString());
            sb.AppendLine(@"<script type=""text/javascript"">");
            sb.AppendLine(@"    $('input[type=checkbox][name=" + selectAllName + "]').on('ifChecked', function (event) {");
            sb.AppendLine(@"        $('input[type=checkbox][name=" + name + "]:enabled').iCheck('check');");
            sb.AppendLine(@"    });");
            sb.AppendLine(@"    $('input[type=checkbox][name=" + selectAllName + "]').on('ifUnchecked', function (event) {");
            sb.AppendLine(@"        $('input[type=checkbox][name=" + name + "]:enabled').iCheck('uncheck');");
            sb.AppendLine(@"    });");
            sb.AppendLine(@"</script>");

            return new MvcHtmlString(sb.ToString());
        }
        public static MvcHtmlString BootstrapCheckBoxSelectAllFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string text, IDictionary<string, object> htmlAttributes)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string selectedValue = (metadata.Model == null) ? "" : metadata.Model.ToString();

            return html.BootstrapCheckBoxSelectAll(htmlFieldName, text, htmlAttributes);
        }
        #endregion
    }
}
