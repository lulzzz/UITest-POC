using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.FunctionalTests.Helpers
{
    public static class ContentHelper
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static string ToQueryString(this Dictionary<string, object> source)
        {
            var keys = source.Where(kv => kv.Value != null && !((kv.Value.GetType().IsArray && ((Array)kv.Value).Length == 0) || (kv.Value is IList && ((IList)kv.Value).Count == 0))).Select(kv =>
            {
                var key = WebUtility.UrlEncode(kv.Key);
                var value = "";
                if (kv.Value.GetType().IsPrimitive || kv.Value.GetType() == typeof(string))
                    value = WebUtility.UrlEncode(kv.Value?.ToString());
                else if (kv.Value != null && kv.Value.GetType() == typeof(DateTime))
                    value = GetDateString(kv.Value);
                else if (kv.Value != null && kv.Value.GetType() == typeof(TimeSpan))
                    value = GetTimeString(kv.Value);
                else if (kv.Value != null && kv.Value.GetType() == typeof(Decimal))
                    value = GetDecimalString(kv.Value);
                else if (kv.Value != null && kv.Value is IList && kv.Value.GetType().IsGenericType)
                    return GetListString(kv.Key, kv.Value);
                else
                    value = WebUtility.UrlEncode(ToQueryStringNonPremitive(kv.Value));
                return $"{key}={value}";
            }).ToArray();

            return string.Join("&", keys);
        }

        private static string GetDateString(object parameterValue)
        {
            if (parameterValue.GetType() != typeof(DateTime))
                return parameterValue.ToString();
            return ((DateTime)parameterValue).ToString("MM/dd/yyyy", new CultureInfo("en-US"));
        }

        private static string GetListString(string parameterKey, object parameterValue)
        {
            var result = new List<string>();
            foreach (var item in ((IList)parameterValue))
                result.Add(GetBasicTypes(item));
            return string.Join("&", result
             .Select(x => string.Concat(
                 Uri.EscapeDataString(parameterKey), "=",
                 Uri.EscapeDataString(GetDateString(x)))));
        }

        private static string GetTimeString(object parameterValue)
        {
            return parameterValue.ToString();
        }
        private static string GetDecimalString(object parameterValue)
        {
            return parameterValue.ToString();
        }

        public static string ToQueryStringNonPremitive(object request, string separator = "&")
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

        private static string GetBasicTypes(object parameterValue)
        {
            if (parameterValue.GetType().IsPrimitive || parameterValue.GetType() == typeof(string))
                return WebUtility.UrlEncode(parameterValue?.ToString());
            else if (parameterValue != null && parameterValue.GetType() == typeof(DateTime))
                return GetDateString(parameterValue);
            else if (parameterValue != null && parameterValue.GetType() == typeof(TimeSpan))
                return GetTimeString(parameterValue);
            return "";
        }
    }
}
