using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class ApiHttpClient : IApiClient
   {
      private readonly IHttpClientFactory _httpClientFactory;
      private readonly IHttpContextAccessor _httpContextAccessor;
      private readonly ILogger<ApiHttpClient> _logger;

      private int _timeout { get; set; }
      private bool _isTesting { get; set; }
      private string _accessToken { get; set; }

      public ApiHttpClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, ILogger<ApiHttpClient> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration)
      {
         _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
         _httpContextAccessor = httpContextAccessor;
         _logger = logger;
         _timeout = rootConfiguration.Value.HttpClientSettingConfiguration.Timeout;
         _isTesting = rootConfiguration.Value.Testing.IsTesting;
         _accessToken = rootConfiguration.Value.Testing.AccessToken;
      }

      public ApiHttpClient SetTimeout(int valueInSeconds)
      {
         _timeout = valueInSeconds;
         return this;
      }

      private async Task<HttpClient> GetHttpClientAsync()
      {
         var client = _httpClientFactory.CreateClient("MonafasatApi");
         client.DefaultRequestHeaders.Accept.Clear();
         var accessToken = _isTesting ? _accessToken : await _httpContextAccessor.HttpContext.GetTokenAsync("oidc", "access_token");
         var language = "ar-SA";
         if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Cookies.ContainsKey("language"))
         {
            language = _httpContextAccessor.HttpContext.Request.Cookies["language"];
            _httpContextAccessor.HttpContext.Response.Cookies.Append("language", language, new CookieOptions { Expires = DateTime.Today.AddYears(1) });
         }
         client.SetBearerToken(accessToken)
             .SetHeader(nameof(language), language)
             .SetHeader("ContentType", "application/json")
             .SetHeader("NewRoles", UrlHelper.ListToString(_httpContextAccessor.HttpContext.User.UserRoles(), ","))
             .SetHeader("BranchId", _httpContextAccessor.HttpContext.User.UserBranch())
             .SetHeader("CommitteeId", _httpContextAccessor.HttpContext.User.SelectedCommittee())
             .SetHeader("GovAgencyRoles", UrlHelper.ListToString(_httpContextAccessor.HttpContext.User.GetAllSemiGovAgencyUserRoles(), ","))
             .SetTimeout(_timeout);
         return client;
      }

      public async Task<T> GetAsync<T>(string segments, Dictionary<string, object> parameters)
      {
         var client = await GetHttpClientAsync();
         var result = await client.GetAsync<T>($"{segments}?{parameters?.ToQueryString()}", CheckErrorAsync);
         return result;
      }

      public async Task<List<T>> GetListAsync<T>(string segments, Dictionary<string, object> parameters)
      {
         var client = await GetHttpClientAsync();
         var result = await client.GetAsync<List<T>>($"{segments}?{parameters?.ToQueryString()}", CheckErrorAsync);
         return result;
      }

      public async Task<QueryResult<T>> GetQueryResultAsync<T>(string segments, Dictionary<string, object> parameters) where T : class
      {
         var client = await GetHttpClientAsync();
         var result = await client.GetAsync<QueryResult<T>>($"{segments}?{parameters?.ToQueryString()}", CheckErrorAsync);
         return result;
      }

      public async Task<string> GetStringAsync(string path, Dictionary<string, object> parameters)
      {
         var client = await GetHttpClientAsync();
         var result = await client.GetStringAsync($"{path}?{parameters?.ToQueryString()}", CheckErrorAsync);
         return result;
      }

      public async Task<T> PostAsync<T>(string segments, Dictionary<string, object> parameters, object model)
      {
         var client = await GetHttpClientAsync();
         var result = await client.PostAsync<T>($"{segments}?{parameters?.ToQueryString()}", model, CheckErrorAsync);
         return result;
      }

      public async Task<string> PostAsync(string segments, Dictionary<string, object> parameters, object model)
      {
         var client = await GetHttpClientAsync();
         var result = await client.PostAsync($"{segments}?{parameters?.ToQueryString()}", model, CheckErrorAsync);
         return result;
      }

      public async Task<T> PostAsync<T>(string segments, Dictionary<string, object> parameters, object model, string accessToken)
      {
         var client = await GetHttpClientAsync();
         client.SetBearerToken(accessToken);
         var result = await client.PostAsync<T>($"{segments}?{parameters?.ToQueryString()}", model, CheckErrorAsync);
         return result;
      }

      public async Task<T> PutAsync<T>(string segments, Dictionary<string, object> parameters, object model)
      {
         var client = await GetHttpClientAsync();
         var result = await client.PutAsync<T>($"{segments}?{parameters?.ToQueryString()}", model, CheckErrorAsync);
         return result;
      }

      public Task<T> DeleteAsync<T>(string segments, Dictionary<string, object> parameters, object model)
      {
         throw new NotImplementedException();
      }

      private async Task CheckErrorAsync(HttpResponseMessage responseMessage)
      {
         if (responseMessage.StatusCode != HttpStatusCode.OK)
         {
            _logger.LogError(responseMessage.StatusCode + "\n" + await responseMessage.Content.ReadAsStringAsync());
            HandleErrors<object>(responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync());
         }
      }

      private T HandleErrors<T>(HttpStatusCode? statusCode, string response)
      {
         _logger.LogError("Status Code : " + statusCode + ", " + response);
         if (statusCode == HttpStatusCode.Unauthorized || statusCode == HttpStatusCode.Forbidden)
            throw new AuthorizationException("Logout");
         if (statusCode == HttpStatusCode.MethodNotAllowed)
            throw new AuthorizationException();
         if (statusCode == HttpStatusCode.Conflict)
            throw new BusinessRuleException((JsonConvert.DeserializeObject<dynamic>(response)).error.ToString());
         //if (statusCode == HttpStatusCode.NoContent)
         //   return default;
         throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.UnexpectedError);
      }
   }
}
