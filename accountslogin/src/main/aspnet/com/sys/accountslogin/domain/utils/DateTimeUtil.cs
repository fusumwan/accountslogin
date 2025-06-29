
using System;
using System.Globalization;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils
{

    public static class DateTimeUtil
    {
        public static string AdjustLocalDateStr(string value, string pattern)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            if (pattern == "yyyy-MM-dd HH:mm:ss" && value.Length == 10)
            {
                value += " 00:00:00";
            }

            return value;
        }

        public static DateTime? ObjectToDate(object value, string pattern)
        {
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                var formatter = new DateTimeFormatInfo { ShortDatePattern = pattern, FullDateTimePattern = pattern };
                if (DateTime.TryParseExact(value.ToString(), pattern, formatter, DateTimeStyles.None, out var date))
                {
                    return date;
                }
            }
            return null;
        }

        public static DateTime? ObjectToLocalDateTime(object value, string pattern)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                var adjustedValue = AdjustLocalDateStr(value.ToString(), pattern);
                var formatter = new DateTimeFormatInfo { FullDateTimePattern = pattern };
                if (DateTime.TryParseExact(adjustedValue, pattern, formatter, DateTimeStyles.None, out var localDateTime))
                {
                    return localDateTime;
                }
            }
            return null;
        }

        public static DateTime? ObjectToLocalDate(object value, string pattern)
        {
            return ObjectToLocalDateTime(value, pattern)?.Date;
        }

        public static DateTime? StringToDate(string value, string pattern)
        {
            return ObjectToDate(value, pattern);
        }

        public static DateTime? StringToLocalDate(string value, string pattern)
        {
            return ObjectToLocalDate(value, pattern);
        }

        public static DateTime? StringToLocalDateTime(string value, string pattern)
        {
            return ObjectToLocalDateTime(value, pattern);
        }

        public static string DateToObject(DateTime? value, string pattern)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(pattern);
            }
            return null;
        }

        public static string DateToString(DateTime? value, string pattern)
        {
            return DateToObject(value, pattern);
        }

        public static string LocalDateToObject(DateTime? value, string pattern)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(pattern);
            }
            return null;
        }

        public static string LocalDateToString(DateTime? value, string pattern)
        {
            return LocalDateToObject(value, pattern);
        }

        public static string LocalDateTimeToObject(DateTime? value, string pattern)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(pattern);
            }
            return null;
        }

        public static string LocalDateTimeToString(DateTime? value, string pattern)
        {
            return LocalDateTimeToObject(value, pattern);
        }

        public static DateTime? LocatDateToDate(DateTime? value)
        {
            // Since DateTime in C# already contains the date in the correct format.
            return value;
        }

        public static DateTime? DateToLocalDate(DateTime value)
        {
            // Return the date part of a DateTime
            return value.Date;
        }
    }


}
