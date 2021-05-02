using System;
using System.Collections;
using System.Linq;
using System.Web;

namespace MOF.Etimad.Monafasat.Web
{
   public class PaginationModel
   {
      public object data { get; set; }


      public int totalCount { get; set; }
      public int pageSize { get; set; }

      public string queryString { get; set; }

      public int currentPage { get; set; }



      public PaginationModel(object data, int totalCount, int pageSize, int currentPage, string queryString)
      {
         this.data = data;
         this.totalCount = totalCount;
         this.pageSize = pageSize;
         this.currentPage = currentPage;
         this.queryString = queryString;
      }

   }
   public static class PaginationExtensionMethod
   {
      public static string GetQueryString(this object obj)
      {
         var properties = from p in obj.GetType().GetProperties()
                          where p.GetValue(obj, null) != null
                          select nameof(obj) + "." + p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

         return String.Join("&", properties.ToArray());
      }

      public static string ToQueryString(this object request, string separator = ",")
      {
         if (request == null)
            throw new ArgumentNullException("request");

         // Get all properties on the object
         var properties = request.GetType().GetProperties()
             .Where(x => x.CanRead)
             .Where(x => x.GetValue(request, null) != null)
             .ToDictionary(x => x.Name, x => x.GetValue(request, null));

         // Get names for all IEnumerable properties (excl. string)
         var propertyNames = properties
             .Where(x => !(x.Value is string) && x.Value is IEnumerable)
             .Select(x => x.Key)
             .ToList();

         // Concat all IEnumerable properties into a comma separated string
         foreach (var key in propertyNames)
         {
            var valueType = properties[key].GetType();
            var valueElemType = valueType.IsGenericType
                                    ? valueType.GetGenericArguments()[0]
                                    : valueType.GetElementType();
            if (valueElemType.IsPrimitive || valueElemType == typeof(string))
            {
               var enumerable = properties[key] as IEnumerable;
               properties[key] = string.Join(separator, enumerable.Cast<object>());
            }
         }

         // Concat all key/value pairs into a string separated by ampersand
         return string.Join("&", properties
             .Select(x => string.Concat(
                 request.GetType().Name + "." + Uri.EscapeDataString(x.Key), "=",
                 Uri.EscapeDataString(x.Value.ToString()))));
      }
   }
}
