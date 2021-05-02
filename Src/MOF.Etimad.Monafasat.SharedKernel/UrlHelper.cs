using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MOF.Etimad.Monafasat.SharedKernal
{
    public static class UrlHelper
    {
        public static string ToQueryString(this object request, string separator = "&")
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
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(GetDateString(x.Value)))));
        }
        private static string GetDateString(object parameterValue)
        {
            if (parameterValue.GetType() != typeof(DateTime))
                return parameterValue.ToString();
            return ((DateTime)parameterValue).ToString("MM/dd/yyyy", new CultureInfo("en-US"));

        }
        public static string ListToQuerstring<T>(this List<T> request, string listName)
        {
            string result = "";
            foreach (var item in request)
            {
                result += "&" + listName + "=" + item;
            }
            return result;
        }
        public static string ListToString(List<string> request, string seprator)
        {
            string result = "";
            foreach (var item in request)
            {
                result += item + seprator;
            }
            return result;
        }
    }

}
