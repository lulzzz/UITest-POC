using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Encryption;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MOF.Etimad.Monafasat.SharedKernal
{
    public static class Util
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Encrypt(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }


        public static string GetHigriDateWithFormat(DateTime? dateTime, string format)
        {
            string result = String.Empty;

            if (dateTime == null)
                return result;

            try
            {
                CultureInfo arCul = new CultureInfo("ar-SA");
                arCul.DateTimeFormat.Calendar = new System.Globalization.UmAlQuraCalendar();
                result = dateTime.Value.ToString(format, arCul);
            }
            catch { result = String.Empty; }
            return result;
        }

        public static string GetGregorianDateWithFormat(DateTime? dateTime, string format)
        {
            string result = String.Empty;

            if (dateTime == null)
                return result;

            try
            {
                CultureInfo geCul = new CultureInfo("en-US");
                geCul.DateTimeFormat.Calendar = new System.Globalization.GregorianCalendar();
                result = dateTime.Value.ToString(format, geCul);
            }
            catch { result = String.Empty; }
            return result;
        }

        public static string SendNotification(string NotificationMessage, string deviceToken)
        {
            try
            {

                string serverKey = "AAAACVjlf5c:APA91bGNBLbXo_iLAoh_N7wY6Pkjg0b5YCi86dsaGPyC0N71B0WR9fTCOFSEtX64a8qzNrzpBU-UQcQNrDjLPAef8AGYQsiNOpHJ7J3QYWJp7oBKM0ySORSlpbfNKkCoVaOW7jd1uwJf";
                string senderId = "40146141079";
                const string contentType = "application/json;  charset=utf-8";
                ServicePointManager.DefaultConnectionLimit = 1000;
                CookieContainer cookies = new CookieContainer();
                HttpWebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send") as HttpWebRequest;
                WebHeaderCollection headerCollection = webRequest.Headers;
                webRequest.Method = "POST";
                webRequest.Headers["Authorization"] = "Key=" + serverKey;
                webRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                string Data = "{\"to\":\"" + deviceToken + "\",\"data\": {\"message\": \"" + NotificationMessage + "\"},\"notification\": {\"text\": \"" + NotificationMessage + "\",\"sound\":true}}";
                webRequest.ContentType = contentType;
                webRequest.CookieContainer = cookies;
                webRequest.ContentLength = Data.Length;
                webRequest.SendChunked = true;
                webRequest.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                webRequest.Accept = "text/html,application/xhtml+xml,application/json,application/xml;q=0.9,*/*;q=0.8";
                webRequest.Referer = "https://accounts.craigslist.org";
                StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream());
                requestWriter.Write(Data);
                requestWriter.Flush();
                StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                responseReader.Close();
                webRequest.GetResponse().Close();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public static string Decrypt(object objEncrypted)
        {
            string txt = "";
            if (objEncrypted.GetType() == typeof(int))
                txt = ((int)objEncrypted).ToString();
            else if (objEncrypted.GetType() == typeof(decimal))
                txt = ((decimal)objEncrypted).ToString();
            else if (objEncrypted.GetType() == typeof(bool))
                txt = ((bool)objEncrypted).ToString();
            else if (objEncrypted.GetType() == typeof(string))
                txt = ((string)objEncrypted).ToString();
            if (!string.IsNullOrEmpty(txt))
                return StringEncrypter.UrlEncrypter.Decrypt((txt).Replace(' ', '+').Replace("*@@**", "/"));
            return "";
        }

        public static decimal DecryptNewDecimal(string idEncrypted)
        {
            if (string.IsNullOrEmpty(idEncrypted))
                return 0;
            else
                return Convert.ToDecimal(StringEncrypter.UrlEncrypter.Decrypt((idEncrypted).Replace(' ', '+').Replace("*@@**", "/")));
        }
        public static int Decrypt(string idEncrypted)
        {
            if (string.IsNullOrEmpty(idEncrypted))
                return 0;
            else
                return Convert.ToInt32(StringEncrypter.UrlEncrypter.Decrypt((idEncrypted).Replace("%20", "+").Replace(' ', '+').Replace("*@@**", "/")));
        }

        public static string Encrypt(int id)
        {
            return StringEncrypter.UrlEncrypter.Encrypt(Convert.ToString(id)).Replace("/", "*@@**").Replace("+", " ");
        }

        #region Image

        public static string ConvertImageURLToBase64(String url)
        {

            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return String.Empty;
                }
                StringBuilder _sb = new StringBuilder();

                Byte[] _byte = GetImage(url);

                _sb.Append(Convert.ToBase64String(_byte, 0, _byte.Length));

                return _sb.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        private static byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }

        public static string NameOfNumber(int number)
        {
            switch (number)
            {
                case 1:
                case 13:
                    return "الواحدة";
                case 2:
                case 14:
                    return "الثانية";
                case 3:
                case 15:
                    return "الثالثة";
                case 4:
                case 16:
                    return "الرابعة";
                case 5:
                case 17:
                    return "الخامسة";
                case 6:
                case 18:
                    return "السادسة";
                case 7:
                case 19:
                    return "السابعة";
                case 8:
                case 20:
                    return "الثامنة";
                case 9:
                case 21:
                    return "التاسعة";
                case 10:
                case 22:
                    return "العاشرة";
                case 11:
                case 23:
                    return "الحادية عشر";
                case 12:
                case 24:
                    return "الثانية عشر";
                default:
                    return "number is not 1 through 24";
            }

        }

        public static void DetermineTimesForDates(DateTime checkTimeOfDay, int timeMessage, RootConfigurations _rootConfiguration)
        {
            TimeSpan startTime = new TimeSpan(_rootConfiguration.OfferTimesConfiguration.StartOfferTime, 0, 0);
            TimeSpan endTime = new TimeSpan(_rootConfiguration.OfferTimesConfiguration.EndOfferTime, 0, 0);
            TimeSpan startTimeForVactionDays = new TimeSpan(_rootConfiguration.OfferTimesConfiguration.StartOfferVacationTime, 0, 0);
            TimeSpan endTimeForVactionDays = new TimeSpan(_rootConfiguration.OfferTimesConfiguration.EndOfferVacationTime, 0, 0);
            string hourNameForStartTime = NameOfNumber(_rootConfiguration.OfferTimesConfiguration.StartOfferTime);
            string hourNameForEndTime = NameOfNumber(_rootConfiguration.OfferTimesConfiguration.EndOfferTime);
            string vacationHourNameForStartTime = NameOfNumber(_rootConfiguration.OfferTimesConfiguration.StartOfferVacationTime);
            string vacationHourNameForEndTime = NameOfNumber(_rootConfiguration.OfferTimesConfiguration.EndOfferVacationTime);
            string BusinessRuleExceptionMessage = "";

            if (timeMessage == (int)Enums.TimeMessagesType.TimeOfferChecking)
            {
                BusinessRuleExceptionMessage = " فحص العروض";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.Qualification)
            {
                BusinessRuleExceptionMessage = "تقديم وثائق التأهيل";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.TimeOffersOpeningDate)
            {
                BusinessRuleExceptionMessage = "فتح العروض";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.LastOfferPresentationDate)
            {
                BusinessRuleExceptionMessage = "تقديم العروض";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.SamplesDeliveryTime)
            {
                BusinessRuleExceptionMessage = "تسليم العينات للمورد";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.OffersDeliveryTime)
            {
                BusinessRuleExceptionMessage = "تسليم العروض";
            }

            TimeSpan dailyTime = checkTimeOfDay.TimeOfDay;
            bool isVacationDayFordailyTime = (checkTimeOfDay.DayOfWeek.ToString() == "Saturday" || checkTimeOfDay.DayOfWeek.ToString() == "Friday") ? true : false;
            if (!(dailyTime >= startTime && dailyTime <= endTime) && !isVacationDayFordailyTime)
            {
                throw new BusinessRuleException($"الرجاء ادخال وقت {BusinessRuleExceptionMessage} ما بين الساعة {hourNameForStartTime} صباحا و {hourNameForEndTime} مساءً");
            }
            else if (!(dailyTime >= startTimeForVactionDays && dailyTime <= endTimeForVactionDays) && isVacationDayFordailyTime)
            {
                throw new BusinessRuleException($"الرجاء ادخال وقت {BusinessRuleExceptionMessage} ما بين الساعة {vacationHourNameForStartTime} صباحا و {vacationHourNameForEndTime} مساءً ليومي الجمعة والسبت");
            }
        }


        public static void DetermineTimesForDates(DateTime checkTimeOfDay, int timeMessage, int startOfferTime, int endOfferTime, int startOfferVacationTime, int endOfferVacationTime)
        {
            TimeSpan startTime = new TimeSpan(startOfferTime, 0, 0);
            TimeSpan endTime = new TimeSpan(endOfferTime, 0, 0);
            TimeSpan startTimeForVactionDays = new TimeSpan(startOfferVacationTime, 0, 0);
            TimeSpan endTimeForVactionDays = new TimeSpan(endOfferVacationTime, 0, 0);
            string hourNameForStartTime = NameOfNumber(startOfferTime);
            string hourNameForEndTime = NameOfNumber(endOfferTime);
            string vacationHourNameForStartTime = NameOfNumber(startOfferVacationTime);
            string vacationHourNameForEndTime = NameOfNumber(endOfferVacationTime);
            string BusinessRuleExceptionMessage = "";

            if (timeMessage == (int)Enums.TimeMessagesType.TimeOfferChecking)
            {
                BusinessRuleExceptionMessage = " فحص العروض";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.Qualification)
            {
                BusinessRuleExceptionMessage = "تقديم وثائق التأهيل";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.TimeOffersOpeningDate)
            {
                BusinessRuleExceptionMessage = "فتح العروض";
            }
            else if (timeMessage == (int)Enums.TimeMessagesType.LastOfferPresentationDate)
            {
                BusinessRuleExceptionMessage = "تقديم العروض";
            }

            TimeSpan dailyTime = checkTimeOfDay.TimeOfDay;
            bool isVacationDayFordailyTime = (checkTimeOfDay.DayOfWeek.ToString() == "Saturday" || checkTimeOfDay.DayOfWeek.ToString() == "Friday") ? true : false;
            if (!(dailyTime >= startTime && dailyTime <= endTime) && !isVacationDayFordailyTime)
            {
                throw new BusinessRuleException($"الرجاء ادخال وقت {BusinessRuleExceptionMessage} ما بين الساعة {hourNameForStartTime} صباحا و {hourNameForEndTime} مساءً");
            }
            else if (!(dailyTime >= startTimeForVactionDays && dailyTime <= endTimeForVactionDays) && isVacationDayFordailyTime)
            {
                throw new BusinessRuleException($"الرجاء ادخال وقت {BusinessRuleExceptionMessage} ما بين الساعة {vacationHourNameForStartTime} صباحا و {vacationHourNameForEndTime} مساءً ليومي الجمعة والسبت");
            }
        }

        #endregion
    }
}
