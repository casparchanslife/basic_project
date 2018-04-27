using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Lib
{
    public class Global
    {
        public const string FORMAT_DATETIME_FULL_DATETIME_PATTERN = "dd/MM/yyyy HH:mm";
        public const string FORMAT_DATETIME_FULL_DATETIME_PATTERN2 = "yyyy-MM-dd HH:mm:ss";// 2017-06-01 00:00:00
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

        public const string FORMAT_SIEBEL_DATETIME_PATTERN = "MM/dd/yyyy HH:mm:ss";

        public const string FORMAT_MARS_DOB_DATETIME_PATTERN = "M/d/yyyy HH:mm:ss";

        public const string FORMAT_FULLCALENDAR_DATETIME_PATTERN = "yyyy-MM-dd";

        public const string FORMAT_PRODUCT_IMPORT_DATE_PATTERN = "d/M/yyyy";

        //jq grid
        public static int P_NUM_OF_GRID_ROWS = 50;

        public static string RECORD_STATUS_INACTIVE = "I";
        public static string RECORD_STATUS_ACTIVE = "A";
        public static string RECORD_STATUS_SENDING = "O";
        public static string RECORD_STATUS_PENDING = "P";
        public static string RECORD_STATUS_SENT = "S";
        public static string RECORD_STATUS_FAIL = "F";

        public static string RECORD_STATUS_DEACTIVATE = "D"; //For Deactivate Record

        public static string ROOT_DIRECTORY = "~/Files/";

        public static string DefaultFileNameByDownLoad = "Files";
        public static string DefaultCreateZip_Path = System.IO.Path.GetTempPath();

        public static string ACCOUNT_DEFAULT_LEVEL = "1";

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes) / 1024f;
        }

        public static int CommonPageSize = 10;

    }
}
