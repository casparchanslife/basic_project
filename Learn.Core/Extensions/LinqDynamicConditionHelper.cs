using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Data.SqlTypes;
using Eslite.Lib.Helpers.Grid;

namespace Learn.Core.Extensions
{
    // http://vbcity.com/blogs/rock/archive/2012/11/23/leveraging-jqgrid-searches-and-dynamic-linq-on-asp-net-mvc-solutions.aspx
    public static class LinqDynamicConditionHelper
    {
        static string FORMAT_DATETIME_FULL_DATETIME_PATTERN = Common.FORMAT_DATETIME_FULL_DATETIME_PATTERN; // "yyyy-MM-dd HH:mm";
        static string FORMAT_DATETIME_SHORT_DATE_PATTERN = Common.FORMAT_DATETIME_SHORT_DATE_PATTERN; // "yyyy-MM-dd";
        static string FORMAT_DATETIME_TIME_24_PATTERN = Common.FORMAT_DATETIME_TIME_24_PATTERN; // "HH:mm";
        static CultureInfo provider = CultureInfo.InvariantCulture;

        private static Dictionary<string, string> WhereOperation = new Dictionary<string, string>{
            { "eq", "{1} = {2}({0})" },
            { "ne", "{1} != {2}({0})" },
            { "lt", "{1} <  {2}({0})" },
            { "le", "{1} <= {2}({0})" },
            { "gt", "{1} >  {2}({0})" },
            { "ge", "{1} >= {2}({0})" },
            { "bw", "{1}.StartsWith({2}({0}))" },
            { "bn", "!{1}.StartsWith({2}({0}))" },
            { "in", "" },
            { "ni", "" },
            { "ew", "{1}.EndsWith({2}({0}))" },
            { "en", "!{1}.EndsWith({2}({0}))" },
            { "cn", "{1}.Contains({2}({0}))" },
            { "nc", "!{1}.Contains({2}({0}))" },
            { "nu", "{1} == null" },
            { "nn", "{1} != null" },
            { "lom", "{2} >= {3}({0}) and {2} <= {3}({1})" },
        };

        private static Dictionary<string, string> ValidOperators = new Dictionary<string, string>{
            { "Object"   , "" }, 
            { "Boolean"  , "eq:cn:" }, 
            { "Char"     , "" }, 
            { "String"   , "eq:ne:lt:le:gt:ge:bw:bn:cn:nc:" }, 
            { "SByte"    , "" }, 
            { "Byte"     , "eq:ne:lt:le:gt:ge:" },
            { "Int16"    , "eq:ne:lt:le:gt:ge:" }, 
            { "UInt16"   , "" }, 
            { "Int32"    , "eq:ne:lt:le:gt:ge:cn:" }, 
            { "UInt32"   , "" }, 
            { "Int64"    , "eq:ne:lt:le:gt:ge:" }, 
            { "UInt64"   , "" }, 
            { "Decimal"  , "eq:ne:lt:le:gt:ge:" }, 
            { "Single"   , "eq:ne:lt:le:gt:ge:" }, 
            { "Double"   , "eq:ne:lt:le:gt:ge:" }, 
            //{ "DateTime" , "eq:ne:lt:le:gt:ge:bw:ew" }, 
            { "DateTime" , "eq:ne:lt:le:gt:ge:" }, 
            { "TimeSpan" , "eq:ne:lt:le:gt:ge:" },  //"" 
            { "Guid"     , "" },
            { "Enum"     , "eq:ne:cn:nc" }
        };

        public static string GetCondition<T>(Filter filter)
        {
            StringBuilder sb = new StringBuilder();
            string rtRule = string.Empty;
            if (filter == null) return null;

            foreach (Rule rule in filter.rules)
            {
                rtRule = GetCondition<T>(rule);
                if (rtRule.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(filter.groupOp.ToLower() == "and" ? " && " : " || ");
                    }
                    sb.Append(rtRule);
                }
            }
            return sb.ToString();
        }

        public static string GetCondition<T>(Rule rule)
        {
            if (rule.data == "%")
            {
                // returns an empty string when the data is ‘%’
                return "";
            }
            else
            {
                // initializing variables
                Type myType = null;
                string myTypeName = string.Empty;
                string myTypeRawName = string.Empty;

                //William
                PropertyInfo myPropInfo = typeof(T).GetProperty(rule.field.Split('.')[0]);

                int index = 0;
                // navigating fields hierarchy
                foreach (string wrkField in rule.field.Split('.'))
                {
                    if (index > 0)
                    {
                        myPropInfo = myPropInfo.PropertyType.GetProperty(wrkField);
                    }
                    index++;
                }
                // property type and its name
                myType = myPropInfo.PropertyType;
                myTypeName = myPropInfo.PropertyType.Name;
                myTypeRawName = myTypeName;
                // handling ‘nullable’ fields
                if (myTypeName.ToLower() == "nullable`1")
                {
                    myType = Nullable.GetUnderlyingType(myPropInfo.PropertyType);
                    //myTypeName = myType.Name + "?";
                    myTypeName = myType.Name;
                    myTypeRawName = myType.Name;
                }
                // Phoebe 2015-10-14: handling 'Enum' fields
                if (myType.IsEnum)
                {
                    myTypeRawName = "Enum";
                }

                string expression = String.Empty;
                // creating the condition expression
                if (ValidOperators[myTypeRawName].Contains(rule.op + ":"))
                {
                    if (myType.Name.ToLower().Equals("string"))
                    {
                        expression = String.Format(WhereOperation[rule.op],
                                                   GetFormattedData(myType, rule.data) + ".ToUpper()",
                                                   rule.field + ".ToUpper()",
                                                   myTypeName);
                    }
                    else if (myType.IsEnum)
                    {
                        expression = String.Format(WhereOperation[rule.op],
                                                  GetFormattedData(myType, rule.data) + ".ToUpper()",
                                                  rule.field + ".ToString()" + ".ToUpper()",
                                                  "String");
                    }
                    else if (rule.op.Equals("eq") && myType.Name.ToLower().Equals("datetime"))
                    {
                        var originalDate = rule.data.Trim().Split(' ')[0];

                        var startDate = originalDate + " 00:00";
                        var endDate = originalDate + " 23:59";
                        expression = String.Format(WhereOperation["lom"],
                           GetFormattedData(myType, startDate),
                           GetFormattedData(myType, endDate),
                           rule.field,
                           myTypeName);
                    }
                    else
                    {
                        expression = String.Format(WhereOperation[rule.op],
                                                   GetFormattedData(myType, rule.data),
                                                   rule.field,
                                                   myTypeName);
                    }
                }
                return expression;
            }
        }
        private static string GetFormattedData(Type type, string value)
        {
            if (type.IsEnum)
            {
                return value = @"""" + value + @"""";
            }
            switch (type.Name.ToLower())
            {
                case "string":
                    value = @"""" + value + @"""";
                    break;
                case "datetime":
                    if (value.Length > 10)
                    {
                        DateTime dt = DateTime.ParseExact(value, FORMAT_DATETIME_FULL_DATETIME_PATTERN, provider);
                        value = dt.Year.ToString() + "," +
                            dt.Month.ToString() + "," +
                            dt.Day.ToString() + "," +
                            dt.Hour.ToString() + "," +
                            dt.Minute.ToString() + "," +
                            "0";
                    }
                    else if (value.Length > 5)
                    {
                        DateTime dt = DateTime.ParseExact(value, FORMAT_DATETIME_SHORT_DATE_PATTERN, provider);
                        value = dt.Year.ToString() + "," +
                            dt.Month.ToString() + "," +
                            dt.Day.ToString() + "," +
                            "0" + "," +
                            "0" + "," +
                            "0";
                    }
                    else
                    {
                        DateTime dt = DateTime.ParseExact(value, FORMAT_DATETIME_TIME_24_PATTERN, provider);
                        value = SqlDateTime.MinValue.Value.Year.ToString() + "," +
                            SqlDateTime.MinValue.Value.Month.ToString() + "," +
                            SqlDateTime.MinValue.Value.Day.ToString() + "," +
                            dt.Hour.ToString() + "," +
                            dt.Minute.ToString() + "," +
                            "0";
                    }
                    break;
                case "timespan":
                    if (value.Length > 5)
                    {
                        TimeSpan ts = TimeSpan.ParseExact(value, "G", provider);
                        value = ts.Hours.ToString() + "," +
                            ts.Minutes.ToString() + "," +
                            ts.Seconds.ToString();
                    }
                    else
                    {
                        TimeSpan ts = TimeSpan.ParseExact(value, "g", provider);
                        value = ts.Hours.ToString() + "," +
                            ts.Minutes.ToString() + "," +
                            "0";
                    }
                    break;
                //case "boolean":
                //    bool bl = false;
                //    Boolean.TryParse(value, out bl);
                //    value = @"""" + bl.ToString() + @"""";
                //    break;
            }
            return value;
        }
    }
}
