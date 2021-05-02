using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public static class HttpClientExtensions
   {
      public static HttpClient SetBasicAuthentication(this HttpClient httpClient, string username, string password)
      {
         var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
         httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
         return httpClient;
      }

      public static HttpClient SetBearerToken(this HttpClient httpClient, string token)
      {
         httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
         return httpClient;
      }

      public static HttpClient SetHeader(this HttpClient httpClient, string name, object value)
      {
         httpClient.DefaultRequestHeaders.Add(name, value.ToString());
         return httpClient;
      }

      public static HttpClient SetTimeout(this HttpClient httpClient, int valueInSeconds)
      {
         httpClient.Timeout = TimeSpan.FromSeconds(valueInSeconds);
         return httpClient;
      }

      public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url, Func<HttpResponseMessage, Task> CheckErrorAsync)
      {
         var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
         await CheckErrorAsync(response);
         // var data = await response.Content.ReadAsStringAsync();
         //return JsonConvert.DeserializeObject<T>(data);

         using (var stream = await response.Content.ReadAsStreamAsync())
         {
            using (var streamReader = new StreamReader(stream))
            {
               using (var reader = new JsonTextReader(streamReader))
               {
                  var serializer = new JsonSerializer();

                  return serializer.Deserialize<T>(reader);
               }
            }
         }

      }

      public static async Task<string> GetStringAsync(this HttpClient httpClient, string url, Func<HttpResponseMessage, Task> CheckErrorAsync)
      {
         var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
         await CheckErrorAsync(response);
         //  return   await response.Content.ReadAsStringAsync();

         using (var stream = await response.Content.ReadAsStreamAsync())
         {
            using (var streamReader = new StreamReader(stream))
            {
               return await streamReader.ReadToEndAsync();
            }
         }
      }

      public static async Task<T> PostAsync<T>(this HttpClient httpClient, string url, object content, Func<HttpResponseMessage, Task> CheckErrorAsync)
      {
         var data = await httpClient.PostAsync(url, content, CheckErrorAsync);
         return JsonConvert.DeserializeObject<T>(data);
      }

      public static async Task<string> PostAsync(this HttpClient httpClient, string url, object content, Func<HttpResponseMessage, Task> CheckErrorAsync)
      {
         var response = await httpClient.PostAsync(url, CreateHttpContent(content));
         await CheckErrorAsync(response);
         //return await response.Content.ReadAsStringAsync();


         using (var stream = await response.Content.ReadAsStreamAsync())
         {
            using (var streamReader = new StreamReader(stream))
            {
               return await streamReader.ReadToEndAsync();
            }
         }

      }

      public static async Task<T> PutAsync<T>(this HttpClient httpClient, string url, object content, Func<HttpResponseMessage, Task> CheckErrorAsync)
      {
         var response = await httpClient.PutAsync(url, CreateHttpContent(content));
         await CheckErrorAsync(response);
         // var data = await response.Content.ReadAsStringAsync();
         // return JsonConvert.DeserializeObject<T>(data);

         using (var stream = await response.Content.ReadAsStreamAsync())
         {
            using (var streamReader = new StreamReader(stream))
            {
               using (var reader = new JsonTextReader(streamReader))
               {
                  var serializer = new JsonSerializer();

                  return serializer.Deserialize<T>(reader);
               }
            }
         }
      }

      private static HttpContent CreateHttpContent<T>(T content)
      {
         var settings = new JsonSerializerSettings
         {
            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
         };
         var json = JsonConvert.SerializeObject(content, settings);
         return new StringContent(json, Encoding.UTF8, "application/json");
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

      public static string ToQueryStringNonPremitive(this object request, string separator = "&")
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

      private static string GetDateString(object parameterValue)
      {
         if (parameterValue.GetType() != typeof(DateTime))
            return parameterValue.ToString();
         return ((DateTime)parameterValue).ToString("MM/dd/yyyy", new CultureInfo("en-US"));
      }

      private static string GetTimeString(object parameterValue)
      {
         return parameterValue.ToString();
      }
      private static string GetDecimalString(object parameterValue)
      {
         return parameterValue.ToString();
      }
   }

   [DataContract]
   public class ResponseMessage
   {
      [DataMember(Name = "IsSuccess")]
      public bool IsSuccess { get; set; }

      [DataMember(Name = "ReturnMessage")]
      public string ReturnMessage { get; set; }
   }

   [DataContract]
   public class ResponseMessage<T> : ResponseMessage
   {
      [DataMember(Name = "Data")]
      public T Data { get; set; }
   }


   [DataContract]
   public class ErrorMessage
   {
      [DataMember(Name = "error")]
      public string Error { get; set; }
   }
}
