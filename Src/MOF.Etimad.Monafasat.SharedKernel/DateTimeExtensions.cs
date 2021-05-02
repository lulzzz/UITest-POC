using System;
using System.Globalization;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public static class DateTimeExtensions
    {
        static public string[] supportedFormats
        {
            get
            {
                return new string[] {
                                     "dd/MM/yyyy", "d/M/yyyy",
                                     "dd/M/yyyy", "d/MM/yyyy",
                                     "dd-MM-yyyy", "d-M-yyyy",
                                     "dd-M-yyyy", "d-MM-yyyy",
                                     "dd M yyyy", "d MM yyyy",
                                     "dd MM yyyy", "d M yyyy",
                                     "yyyy/MM/dd","yyyy/M/d",
                                     "yyyy-MM-dd", "yyyy-M-d",
                                     "yyyy MM dd", "yyyy M d"
                                 };
            }
        }

        #region Gregorian

        /// <summary>
        /// Check if the string provided is Gregorian date
        /// </summary>
        /// <param name="greg">date string</param>
        /// <returns>True if string can be casted as gregorian date</returns>
        public static bool IsGreg(this string greg)
        {
            string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
            "dd/MM/yyyy","d/M/yyyy",
            "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
            "yyyy-M-d","dd-MM-yyyy","d-M-yyyy",
            "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
            "yyyy M d","dd MM yyyy","d M yyyy",
            "dd M yyyy","d MM yyyy"};
            var enCul = new CultureInfo("en-GB");

            if (greg == null || greg.Length <= 0)
                return false;
            try
            {
                DateTime tempDate = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);

                if (tempDate.Year >= 1900 && tempDate.Year <= 2200)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the string provided is Gregorian date
        /// </summary>
        /// <param name="datetime">date string</param>
        /// <returns>True if string can be casted as gregorian date</returns>
        public static bool IsGregorianDate(this string datetime)
        {
            var cul = new CultureInfo("en-US");
            datetime = datetime.Trim();

            if (datetime.Length <= 0) return false;

            try
            {
                DateTime tempDate;

                if (DateTime.TryParseExact(datetime, supportedFormats, cul, DateTimeStyles.AllowWhiteSpaces, out tempDate))
                {
                    if (tempDate.IsSupportedDateTime(cul.Calendar)) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns DateTime string with specific format using GregorianCalendar
        /// </summary>
        /// <param name="dateTime">DateTime object</param>
        /// <param name="format">date time format to use</param>
        /// <returns></returns>
        public static string ToGregorianDate(this DateTime? dateTime, string format)
        {
            return dateTime.HasValue ? dateTime.Value.ToGregorianDate(format) : string.Empty;
        }

        /// <summary>
        /// Returns DateTime string with specific format using GregorianCalendar
        /// </summary>
        /// <param name="dateTime">DateTime object</param>
        /// <param name="format">date time format to use</param>
        /// <returns></returns>
        public static string ToGregorianDate(this DateTime dateTime, string format)
        {
            try
            {
                CultureInfo cul = new CultureInfo("en-US");
                cul.DateTimeFormat.Calendar = new System.Globalization.GregorianCalendar();
                return dateTime.ToString(format, cul);
            }
            catch { return String.Empty; }
        }

        /// <summary>
        /// Parse Gregorian date string with supported formats
        /// </summary>
        /// <param name="strDate">the date time string to parse</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ParseGregorianDate(this string strDate)
        {
            return strDate.ParseGregorianDate(supportedFormats);
        }

        /// <summary>
        /// Parse Gregorian date string with specific formats
        /// </summary>
        /// <param name="strDate">the date time string to parse</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ParseGregorianDate(this string strDate, params string[] formats)
        {
            if (string.IsNullOrWhiteSpace(strDate)) return null;

            DateTime result;
            CultureInfo cul;
            Calendar calendar = new System.Globalization.UmAlQuraCalendar();
            cul = new CultureInfo("ar-SA");
            cul.DateTimeFormat.Calendar = calendar;

            try
            {
                if (DateTime.TryParseExact(strDate, formats, cul, DateTimeStyles.AllowWhiteSpaces, out result))
                {
                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }


        #endregion

        #region Hijri

        public static string ToHijriDateWithFormat(this DateTime dateTime, string format)
        {
            string result = String.Empty;
            CultureInfo arCul = new CultureInfo("ar-SA");
            Calendar calendar;
            try
            {
                //check UmAlQura boundaries
                calendar = new System.Globalization.UmAlQuraCalendar();

                if (dateTime < calendar.MinSupportedDateTime || dateTime > calendar.MaxSupportedDateTime)
                    calendar = new System.Globalization.HijriCalendar();

                arCul.DateTimeFormat.Calendar = calendar;
                result = dateTime.ToString(format, arCul);
            }
            catch { result = String.Empty; }
            return result;
        }

        /// <summary>
        /// Check if the string provided is UmAlQura date
        /// </summary>
        /// <param name="greg">date string</param>
        /// <returns>True if string can be casted as UmAlQura date</returns>
        public static bool IsUmAlQuraDate(string datetime)
        {
            var cul = new CultureInfo("ar-SA");
            datetime = datetime.Trim();

            if (datetime.Length <= 0) return false;

            try
            {
                DateTime tempDate;

                if (DateTime.TryParseExact(datetime, supportedFormats, cul, DateTimeStyles.AllowWhiteSpaces, out tempDate))
                {
                    if (tempDate.IsSupportedDateTime(cul.Calendar)) return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string ToHijriDate(this DateTime? dateTime, string format)
        {
            return dateTime.HasValue ? dateTime.Value.ToHijriDate(format) : string.Empty;
        }

        public static string ToHijriDate(this DateTime dateTime, string format)
        {
            CultureInfo cul = new CultureInfo("ar-SA");
            Calendar calendar;
            try
            {
                //check with UmAlQura
                calendar = new System.Globalization.UmAlQuraCalendar();

                if (dateTime.IsSupportedDateTime(calendar))
                    calendar = new System.Globalization.HijriCalendar();

                cul.DateTimeFormat.Calendar = calendar;
                return dateTime.ToString(format, cul);
            }
            catch { return String.Empty; }
        }

        /// <summary>
        /// Parse hijri date string with supported formats
        /// </summary>
        /// <param name="strDate">the date time string to parse</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ParseHijriDate(this string strDate)
        {
            return strDate.ParseHijriDate(supportedFormats);
        }

        /// <summary>
        /// Parse hijri date string with specific formats
        /// </summary>
        /// <param name="strDate">the date time string to parse</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ParseHijriDate(this string strDate, params string[] formats)
        {
            if (string.IsNullOrWhiteSpace(strDate)) return null;
            DateTime result;
            CultureInfo cul;
            Calendar calendar = new System.Globalization.UmAlQuraCalendar();
            cul = new CultureInfo("ar-SA");
            cul.DateTimeFormat.Calendar = calendar;

            try
            {
                if (DateTime.TryParseExact(strDate, formats, cul, DateTimeStyles.AllowWhiteSpaces, out result))
                {
                    return result;
                }
                else
                {
                    //try to parse as hijri
                    cul.DateTimeFormat.Calendar = new HijriCalendar();
                    if (DateTime.TryParseExact(strDate, formats, cul, DateTimeStyles.AllowWhiteSpaces, out result))
                    {
                        return result;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        private static bool IsSupportedDateTime(this DateTime datetime, Calendar calendar)
        {
            if (datetime > calendar.MinSupportedDateTime && datetime <= calendar.MaxSupportedDateTime)
            {
                return true;
            }
            return false;
        }

        public static DateTime ToDateTime(this string datetime, char dateSpliter = '-', char timeSpliter = ':', char millisecondSpliter = ',')
        {
            try
            {

                DateTime result;
                CultureInfo cul;
                Calendar calendar = new System.Globalization.GregorianCalendar();
                cul = new CultureInfo("ar-SA");
                cul.DateTimeFormat.Calendar = calendar;
                datetime = datetime.Trim();
                datetime = datetime.Replace("  ", " ");
                string[] body = datetime.Split(' ');
                string[] date = body[0].Split(dateSpliter);
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                int hour = 0, minute = 0, second = 0, millisecond = 0;
                if (body.Length == 2)
                {
                    string[] tpart = body[1].Split(millisecondSpliter);
                    string[] time = tpart[0].Split(timeSpliter);
                    hour = int.Parse(time[0]);
                    minute = int.Parse(time[1]);
                    if (time.Length == 3) second = int.Parse(time[2]);
                    if (tpart.Length == 2) millisecond = int.Parse(tpart[1]);
                }
                return new DateTime(year, month, day, hour, minute, second, millisecond, calendar);
            }
            catch
            {
                return new DateTime();
            }
        }

    }
}
