//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Flurl;
//using Flurl.Http;
//using Newtonsoft.Json;
//using MOF.Etimad.Monafasat.SharedKernal;
//using MOF.Etimad.Monafasat.SharedKernel;
//using Microsoft.Extensions.Logging;
//using MOF.Etimad.Monafasat.ViewModel;
//using System.Web;
//using Flurl.Http.Configuration;

//namespace MOF.Etimad.Monafasat.Web.Infrastructure
//{
//   public class ApiClient : IApiClient
//    {
//      private readonly IHttpContextAccessor _httpContextAccessor;
//      private readonly ILogger<ApiClient> _logger;
//      public IConfiguration Configuration { get; }

//      public ApiClient(IHttpContextAccessor httpContextAccessor, ILogger<ApiClient> logger)
//      {
//         Configuration = ConfigurationHelper.GetConfiguration(Directory.GetCurrentDirectory());
//         _httpContextAccessor = httpContextAccessor;
//         _logger = logger;
//      }
//      public async Task<IFlurlRequest> GetFlurlRequestAsync(string accessToken = "")
//      {
//         var language = "ar-SA";
//         if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("language"))
//         {
//            language = _httpContextAccessor.HttpContext.Request.Cookies["language"];
//            _httpContextAccessor.HttpContext.Response.Cookies.Append("language", language, new CookieOptions { Expires = DateTime.Today.AddYears(1) });
//         }
//         var baseUrl = Configuration.GetSection("API:MonafasatAPI").Value;
//         if (string.IsNullOrEmpty(accessToken))
//            accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("oidc", "access_token");
//         FlurlHttp.ConfigureClient(baseUrl, cli => cli.Settings.HttpClientFactory = new UntrustedCertClientFactory());
//         return (FlurlRequest)baseUrl.WithOAuthBearerToken(accessToken)
//                        .AllowAnyHttpStatus()
//                        .WithTimeout(600)
//                        //.EnableCookies()
//                        .WithHeader(nameof(language), language)
//                        .WithHeader("ContentType", "application/json")
//                        .WithHeader("NewRoles", UrlHelper.ListToString(_httpContextAccessor.HttpContext.User.UserRoles(), ","))
//                        .WithHeader("BranchId", _httpContextAccessor.HttpContext.User.UserBranch())
//                        .WithHeader("CommitteeId", _httpContextAccessor.HttpContext.User.SelectedCommittee())
//                        .WithHeader("GovAgencyRoles", UrlHelper.ListToString(_httpContextAccessor.HttpContext.User.GetAllSemiGovAgencyUserRoles(), ","));
//      }
//      public async Task<string> GetStringAsync(string path, Dictionary<string, object> parameters, string accessToken = "")
//      {
//         var result = await (await GetFlurlRequestAsync(accessToken))
//                        .AppendPathSegments(path)
//                        .SetQueryParams(parameters)
//                        .GetAsync();
//         await CheckErrorAsync(result);
//         return await result.Content.ReadAsStringAsync();
//      }
//      private async Task<T> ExecuteGetAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         var result = await (await GetFlurlRequestAsync(accessToken))
//                        .AppendPathSegments(path)
//                        .SetQueryParams(parameters)
//                        .GetAsync();
//         await CheckErrorAsync(result);
//         return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
//      }
//      public async Task<T> GetAsync<T>(string path, Dictionary<string, object> parameters, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecuteGetAsync<T>, path, parameters, null, accessToken);
//      }
//      public async Task<List<T>> GetListAsync<T>(string path, Dictionary<string, object> parameters, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecuteGetAsync<List<T>>, path, parameters, null, accessToken);
//      }
//      public async Task<QueryResult<T>> GetQueryResultAsync<T>(string path, Dictionary<string, object> parameters, string accessToken = "") where T : class
//      {
//         return await ExecuteRequestAsync(ExecuteGetAsync<QueryResult<T>>, path, parameters, null, accessToken);
//      }
//      private async Task<T> ExecutePostAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         var result = await (await GetFlurlRequestAsync(accessToken))
//                        .AppendPathSegments(path)
//                        .SetQueryParams(parameters)
//                        .PostJsonAsync(model);
//         await CheckErrorAsync(result);
//         return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
//      }
//      public async Task<string> PostFileAsync(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecutePostFileAsync, path, parameters, model, accessToken);
//      }
//      private async Task<string> ExecutePostFileAsync(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         using (Stream stream = new MemoryStream(((UploadFileModel)model).FileContentField))
//         {
//            var result = await (await GetFlurlRequestAsync(accessToken))
//                              .AppendPathSegments(path)
//                              .SetQueryParams(parameters)
//                              .PostMultipartAsync(mp => mp.AddJson("json", model).AddFile("file", stream, "filename"));
//            await CheckErrorAsync(result);
//            return await result.Content.ReadAsStringAsync();
//         }
//      }
//      public async Task<T> PostAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecutePostAsync<T>, path, parameters, model, accessToken);
//      }
//      private async Task<string> ExecutePostAsync(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         var result = await (await GetFlurlRequestAsync(accessToken))
//                        .AppendPathSegments(path)
//                        .SetQueryParams(parameters)
//                        .PostJsonAsync(model);
//         await CheckErrorAsync(result);
//         return await result.Content.ReadAsStringAsync();
//      }
//      public async Task<string> PostAsync(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecutePostAsync, path, parameters, model, accessToken);
//      }
//      private async Task<T> ExecutePutAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         var result = await (await GetFlurlRequestAsync(accessToken))
//                        .AppendPathSegments(path)
//                        .SetQueryParams(parameters)
//                        .PutJsonAsync(model);
//         await CheckErrorAsync(result);
//         return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
//      }
//      public async Task<T> PutAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecutePutAsync<T>, path, parameters, model, accessToken);
//      }
//      private async Task<T> ExecuteDeleteAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         var result = await (await GetFlurlRequestAsync(accessToken))
//                        .AppendPathSegments(path)
//                        .SetQueryParams(parameters)
//                        .SendJsonAsync(HttpMethod.Delete, model);
//         await CheckErrorAsync(result);
//         return JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync());
//      }
//      public async Task<T> DeleteAsync<T>(string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         return await ExecuteRequestAsync(ExecutePutAsync<T>, path, parameters, model, accessToken);
//      }
//      private async Task<T> ExecuteRequestAsync<T>(Func<string, Dictionary<string, object>, object, string, Task<T>> methodName, string path, Dictionary<string, object> parameters, object model, string accessToken = "")
//      {
//         try
//         { return await methodName(path, parameters, model, accessToken); }
//         catch (FlurlHttpException ex)
//         {
//            _logger.LogError(ex.Message + "\n" + ex.StackTrace);
//            return HandleErrors<T>(ex.Call.HttpStatus, await ex.GetResponseStringAsync());
//         }
//      }
//      private async Task CheckErrorAsync(HttpResponseMessage responseMessage)
//      {
//         if (responseMessage.StatusCode != HttpStatusCode.OK)
//         {
//            _logger.LogError(responseMessage.StatusCode + "\n" + await responseMessage.Content.ReadAsStringAsync());
//            HandleErrors<object>(responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync());
//         }
//      }
//      private T HandleErrors<T>(HttpStatusCode? statusCode, string response)
//      {
//         _logger.LogError("Status Code : " + statusCode + ", " + response);
//         if (statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.Forbidden)
//            throw new AuthorizationException("Logout");
//         if (statusCode == HttpStatusCode.MethodNotAllowed)
//            throw new AuthorizationException();
//         if (statusCode == HttpStatusCode.Conflict)
//            throw new BusinessRuleException((JsonConvert.DeserializeObject<dynamic>(response)).error.ToString());
//         if (statusCode == HttpStatusCode.NoContent)
//            return default;
//         throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.UnexpectedError);
//      }
//   }
//   public class UntrustedCertClientFactory : DefaultHttpClientFactory
//   {
//      public override HttpMessageHandler CreateMessageHandler()
//      {
//         return new HttpClientHandler
//         {
//            ServerCertificateCustomValidationCallback = (a, b, c, d) => true
//         };
//      }
//   }

//}
