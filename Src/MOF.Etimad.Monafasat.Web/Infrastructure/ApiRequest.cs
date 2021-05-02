using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Infrastructure
{
   public class ApiRequest<T>
   {
      #region Properites
      public static string Url { get; set; }
      // private HttpClient _client;
      #endregion

      #region Constractor

      private IHttpContextAccessor _httpContextAccessor;
      private ControllerContext _httpContext;
      public RootConfiguration _configuration { get; }

      public ApiRequest(ControllerContext httpContext, IOptionsSnapshot<RootConfiguration> rootConfiguration)
      {
         _configuration = rootConfiguration.Value;
         _httpContext = httpContext;
      }
      public async Task<HttpClient> GetHttpClient(string baseAddress = "")
      {
         var clientHandler = new HttpClientHandler
         {
            UseCookies = true
         };
         var language = "ar-SA";
         var baseUri = new Uri(string.IsNullOrEmpty(baseAddress) ? _configuration.APIConfiguration.MonafasatAPI : baseAddress);
         if (_httpContext.HttpContext != null && _httpContext.HttpContext.Request.Cookies.ContainsKey("language"))
            language = _httpContext.HttpContext.Request.Cookies["language"];
         clientHandler.CookieContainer.Add(baseUri, new Cookie("language", language));
         var _client = new HttpClient(clientHandler);
         string accessToken = string.Empty;
         _httpContextAccessor = new HttpContextAccessor();
         var currentContext = _httpContextAccessor.HttpContext;

         //// get expires_at value
         //var expires_at = await currentContext.GetTokenAsync("expires_at");
         //// compare - make sure to use the exact date formats for comparison 
         //// (UTC, in this case)
         //if (string.IsNullOrWhiteSpace(expires_at)
         //    || ((DateTime.Parse(expires_at).AddSeconds(-60)).ToUniversalTime()
         //    < DateTime.UtcNow))
         //{
         //   accessToken = await RenewTokens();
         //}
         //else
         //{
         // get access token
         //accessToken = await _httpContext.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
         accessToken = await _httpContext.HttpContext.GetTokenAsync("oidc", "access_token");
         //}

         if (!string.IsNullOrWhiteSpace(accessToken))
         {
            _client.SetBearerToken(accessToken);
         }
         _client.BaseAddress = new Uri(string.IsNullOrEmpty(baseAddress) ? _configuration.APIConfiguration.MonafasatAPI : baseAddress);
         _client.DefaultRequestHeaders.Accept.Clear();
         _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
         return _client;
      }

      #endregion

      #region Methods
      // For Get And Delete
      public async Task<string> GetAsync(string path)
      {
         var clientApi = await GetHttpClient();

         HttpResponseMessage responseMessage = await clientApi.GetAsync(path);
         if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            throw new AuthorizationException("Logout");
         else if (responseMessage.IsSuccessStatusCode)
         {
            var responseData = await responseMessage.Content.ReadAsStringAsync();
            return responseData;
         }
         else if (responseMessage.StatusCode == HttpStatusCode.Conflict)
            throw new BusinessRuleException(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await responseMessage.Content.ReadAsStringAsync()).error.ToString());
         return "";
      }
      // For Add And Update
      public async Task<string> PostAsync(string path, T model)
      {
         var clientApi = await GetHttpClient();
         HttpResponseMessage response = await clientApi.PostAsJsonAsync(path, model);
         //response.EnsureSuccessStatusCode();
         if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new AuthorizationException("Logout");
         if (response.IsSuccessStatusCode)
         {
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
         }
         else if (response.StatusCode == HttpStatusCode.Conflict)
            throw new BusinessRuleException(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).error.ToString());
         return "";
      }

      public async Task<string> PostModelAsync<M>(string path, M model)
      {
         var clientApi = await GetHttpClient();
         HttpResponseMessage response = await clientApi.PostAsJsonAsync(path, model);
         //response.EnsureSuccessStatusCode();
         if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new AuthorizationException("Logout");
         if (response.IsSuccessStatusCode)
         {
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
         }
         else if (response.StatusCode == HttpStatusCode.Conflict)
            throw new BusinessRuleException(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).error.ToString());
         return "";
      }

      // For Add And Update
      public async Task<string> PostListAsync(string path, List<T> model)
      {
         var clientApi = await GetHttpClient();
         HttpResponseMessage response = await clientApi.PostAsJsonAsync(path, model);
         //response.EnsureSuccessStatusCode();
         if (response.IsSuccessStatusCode)
         {
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
         }
         else if (response.StatusCode == HttpStatusCode.Conflict)
            throw new BusinessRuleException(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).error.ToString());
         return "";
      }
      // For Add And Update
      public async Task<string> PostListAsyncWithPramAsync(string path, List<T> model, params string[] parameter)
      {
         for (int i = 0; i < parameter.Length; i += 2)
            path += "/" + parameter[i];
         var clientApi = await GetHttpClient();
         HttpResponseMessage response = await clientApi.PostAsJsonAsync(path, model);
         if (response.IsSuccessStatusCode)
         {
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
         }
         else if (response.StatusCode == HttpStatusCode.Conflict)
            throw new BusinessRuleException(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()).error.ToString());
         return "";
      }
      // IDP Get
      public async Task<string> GetIDPAsync(string path)
      {
         var clientApi = await GetHttpClient(_configuration.AuthorityConfiguration.AuthorityURL);
         HttpResponseMessage responseMessage = await clientApi.GetAsync(path);
         if (responseMessage.IsSuccessStatusCode)
         {
            var responseData = await responseMessage.Content.ReadAsStringAsync();
            return responseData;
         }
         return "Not Found";
      }
      #endregion

      private async Task<string> RenewTokens()
      {
         //try
         //{
         //   // get the current HttpContext to access the tokens
         //   var currentContext = _httpContextAccessor.HttpContext;
         //   // get the metadata
         //   var discoveryClient = new DiscoveryClient(_configuration.GetSection("Authority:AuthorityURL").Value);
         //   var metaDataResponse = await discoveryClient.GetAsync();
         //   // create a new token client to get new tokens
         //   var tokenClient = new TokenClient(metaDataResponse.TokenEndpoint,
         //       "3a89adb6-2d4f-4d03-a6e0-07e4bd5daf85", "secret");
         //   // get the saved refresh token
         //   var currentRefreshToken = await currentContext
         //       .GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
         //   if (string.IsNullOrEmpty(currentRefreshToken))
         //      return null;
         //   // refresh the tokens
         //   var tokenResult = await tokenClient.RequestRefreshTokenAsync(currentRefreshToken);
         //   if (!tokenResult.IsError)
         //   {
         //      // get auth info
         //      var authenticateInfo = await currentContext.AuthenticateAsync("Cookies");
         //      // create a new value for expires_at, and save it
         //      var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResult.ExpiresIn);
         //      authenticateInfo.Properties.UpdateTokenValue("expires_at",
         //          expiresAt.ToString("o", CultureInfo.InvariantCulture));
         //      authenticateInfo.Properties.UpdateTokenValue(
         //          OpenIdConnectParameterNames.AccessToken,
         //          tokenResult.AccessToken);
         //      authenticateInfo.Properties.UpdateTokenValue(
         //          OpenIdConnectParameterNames.RefreshToken,
         //          tokenResult.RefreshToken);




         //    // we're signing in again with the new values.  
         //    await currentContext.SignInAsync("Cookies",
         //          authenticateInfo.Principal, authenticateInfo.Properties);
         //      // return the new access token 
         //      return tokenResult.AccessToken;
         //   }
         //   else
         //   {
         //      throw new Exception("Problem encountered while refreshing tokens.",
         //          tokenResult.Exception);
         //   }
         //}
         //catch (Exception ex)
         //{
         //   return null;
         //}
         return null;
      }
   }
}
