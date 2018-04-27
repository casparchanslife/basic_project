using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Learn.Lib
{
    public static class Common
    {

        public static string _UploadImagePath = ConfigurationManager.AppSettings["UploadImagePath"];

        public const string FORMAT_DATETIME_FULL_DATETIME_PATTERN = "dd/MM/yyyy HH:mm";
        public const string FORMAT_DATETIME_LONG_DATE_PATTERN = "dd/MM/yyyy (ddd)";
        public const string FORMAT_DATETIME_SHORT_DATE_PATTERN = "dd/MM/yyyy";
        public const string FORMAT_DATETIME_TIME_12_PATTERN = "hh:mm tt";
        public const string FORMAT_DATETIME_TIME_24_PATTERN = "HH:mm";

        public const string FORMAT_DATETIMEPICKER_PARSE = "DD/MM/YYYY HH:mm";
        public const string FORMAT_DATEPICKER_PARSE = "DD/MM/YYYY";
        public const string FORMAT_TIMEPICKER_12_PARSE = "hh:mm A";
        public const string FORMAT_TIMEPICKER_24_PARSE = "HH:mm";

        public const string FORMAT_TIMESPAN_24_PARSE = @"hh\:mm";

        public const string FORMAT_DATETIMEPICKER_Api = "dd/MM/yyyy HH:mm:ss";// 16/05/2012 09:17:00

        public const string FORMAT_REPORT_DATETIMEPICKER_EN = "dd-MMM-yy";
        public const string FORMAT_REPORT_DATETIMEPICKER = "dd/MM/yyyy HH:mm:ss.ttt";

        public const string FORMAT_ESLITEAPI = "yyyyMMdd";

        public const string FORMAT_CardInquiries = "yyyy/MM/dd";

        public const string FORMAT_Contact = "yyyy/MM/dd HH:mm:ss";

        public const string STATUS_ACTIVE = "A";

        public const string STATUS_INACTIVE = "A";

    }
}
