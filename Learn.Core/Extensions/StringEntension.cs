using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Learn.Lib.Extensions
{
    public static class StringEntension
    {
        public static string Multiply(this string source, int multiplier)
        {
            /*
            StringBuilder sb = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }
            return sb.ToString();
            */
            return String.Concat(Enumerable.Repeat(source, multiplier));
        }
        /// <summary>
        /// To get the property name from the passed in lambda
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/28569628/4684232"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static MvcHtmlString PropertyNameFor<T>(Expression<Func<T, object>> property) where T : class
        {
            //MemberExpression body = GetMemberExpression(property);// (MemberExpression)property.Body;
            //return MvcHtmlString.Create(body.Member.Name);
            string propertyPlainName = PropertyPlainNameFor(property);
            return MvcHtmlString.Create(propertyPlainName);
        }

        /// <summary>
        /// To get the property name from the passed in lambda
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/28569628/4684232"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string PropertyPlainNameFor<T>(Expression<Func<T, object>> property) where T : class
        {
            MemberExpression body = GetMemberExpression(property);// (MemberExpression)property.Body;
            return body.Member.Name;
        }

        /// <summary>
        /// To get the DisplayAttribute from the passed in lambda
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/28569628/4684232"/>
        /// <see cref="https://stackoverflow.com/a/32808219/4684232"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayNameFor<T>(Expression<Func<T, object>> property) where T : class
        {
            return MvcHtmlString.Create(DisplayNameForText(property));
        }

        /// <summary>
        /// To get the DisplayAttribute from the passed in lambda
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/28569628/4684232"/>
        /// <see cref="https://stackoverflow.com/a/32808219/4684232"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string DisplayNameForText<T>(Expression<Func<T, object>> property) where T : class
        {
            MemberExpression body = GetMemberExpression(property);// (MemberExpression)property.Body;
            var dd = body.Member.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
            if (dd != null && !String.IsNullOrWhiteSpace(dd.Name))
            {
                return dd.Name;
            }
            else
            {
                return body.Member.Name;
            }
        }

        /// <summary>
        /// Ensure MemberExpression is retrieved even when the input is UnaryExpression
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/12975480/4684232"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> exp)
        {
            var member = exp.Body as MemberExpression;
            var unary = exp.Body as UnaryExpression;
            return member ?? (unary != null ? unary.Operand as MemberExpression : null);
        }
    }
}
