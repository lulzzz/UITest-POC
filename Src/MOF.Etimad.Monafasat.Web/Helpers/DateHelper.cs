using System;
using System.Globalization;

namespace MOF.Etimad.Monafasat.Web.Helpers
{
   public class DateHelper
   {
      public static DateTime? GetDate(string hijriOrGregDate)
      {
         HijriCalendar hijriCal = new HijriCalendar();
         GregorianCalendar gregCal = new GregorianCalendar();
         if (!string.IsNullOrEmpty(hijriOrGregDate))
         {
            string[] date = hijriOrGregDate.Split(',');

            if (date[3] == "H")
            {
               DateTime persianDate = hijriCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
               persianDate = DateTime.ParseExact(date[2] + "/" + date[1] + "/" + date[0], "yyyy/MM/dd", new CultureInfo("ar-SA")/*, hijriCal*/);
               return persianDate;
            }
            else
            {
               DateTime persianDate = gregCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
               persianDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), gregCal);
               return persianDate;
            }
         }
         else
            return null;
      }

      public static DateTime? GetSavingDate(string hijriOrGregDate, string time = "")
      {
         if (!string.IsNullOrEmpty(hijriOrGregDate))
         {
            string[] date = hijriOrGregDate.Split(',');
            if (date[3] == "H")
            {
               CultureInfo arProvider = CultureInfo.CreateSpecificCulture("ar-SA");
               Calendar hijriCalende = arProvider.Calendar;
               DateTime hijriDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), hijriCalende).AddDays(-1);
               if (!string.IsNullOrEmpty(time))
               {
                  TimeSpan ts = Convert.ToDateTime(time).TimeOfDay;
                  hijriDate = hijriDate + ts;
               }
               return hijriDate;
            }
            else
            {
               GregorianCalendar gregCal = new GregorianCalendar();
               DateTime gregDate = gregCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
               gregDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), gregCal);
               if (!string.IsNullOrEmpty(time))
               {
                  TimeSpan ts = Convert.ToDateTime(time).TimeOfDay;
                  gregDate = gregDate + ts;
               }
               return gregDate;
            }
         }
         else
            return null;
      }

      public static TimeSpan GetTimePart(string time)
      {
         TimeSpan ts = new TimeSpan();
         if (!string.IsNullOrEmpty(time))
         {
            ts = Convert.ToDateTime(time).TimeOfDay;
         }
         return ts;

      }



   }
}
