using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web
{
   public class MemoryCacheStore : ITicketStore
   {
      private const string KeyPrefix = "AuthSessionStore-";
      private IMemoryCache _cache;
      //private IConfiguration _IConfiguration;
      private readonly IHttpContextAccessor _httpContextAccessor;
      private readonly string _memoryCacheTimeOutMinutesValue;
      //private readonly IOptionsSnapshot<RootConfiguration> _rootConfiguration;
      public MemoryCacheStore(/*IOptionsSnapshot<RootConfiguration> rootConfigurationIConfiguration configuration, */IHttpContextAccessor httpContextAccessor, string memoryCacheTimeOutMinutesValue)
      {
         //_IConfiguration = configuration;
         _httpContextAccessor = httpContextAccessor;
         _cache = new MemoryCache(new MemoryCacheOptions());
         //_rootConfiguration = rootConfiguration;
         _memoryCacheTimeOutMinutesValue = memoryCacheTimeOutMinutesValue;
      }

      public async Task<string> StoreAsync(AuthenticationTicket ticket)
      {
         var guid = Guid.NewGuid();
         int expMinutesConfig = Convert.ToInt32(_memoryCacheTimeOutMinutesValue/*_rootConfiguration.Value.MemoryCacheTimeOutMinutesConfiguration.MemoryCacheTimeOutMinutesValue _IConfiguration.GetSection("MemoryCacheTimeOutMinutes:MemoryCacheTimeOutMinutes").Value*/);
         var options = new MemoryCacheEntryOptions();
         var expiresUtc = ticket.Properties.ExpiresUtc;
         if (expiresUtc.HasValue)
         {
            options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(expMinutesConfig));
         }
         options.SetSlidingExpiration(TimeSpan.FromMinutes(expMinutesConfig));
         var key = KeyPrefix + guid.ToString();
         _httpContextAccessor.HttpContext.Session.SetString("IdSession", key);
         await RenewAsync(key, ticket);
         return key;
      }

      public Task RenewAsync(string key, AuthenticationTicket ticket)
      {
         int expMinutesConfig = Convert.ToInt32(_memoryCacheTimeOutMinutesValue /*_rootConfiguration.Value.MemoryCacheTimeOutMinutesConfiguration.MemoryCacheTimeOutMinutesValue _IConfiguration.GetSection("MemoryCacheTimeOutMinutes:MemoryCacheTimeOutMinutes").Value*/);
         var options = new MemoryCacheEntryOptions();
         var expiresUtc = ticket.Properties.ExpiresUtc;
         if (expiresUtc.HasValue)
         {
            options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(expMinutesConfig));
         }
         options.SetSlidingExpiration(TimeSpan.FromMinutes(expMinutesConfig));

         _cache.Set(key, ticket, options);

         return Task.FromResult(0);
      }

      public Task<AuthenticationTicket> RetrieveAsync(string key)
      {
         AuthenticationTicket ticket;
         _cache.TryGetValue(key, out ticket);
         return Task.FromResult(ticket);
      }

      public Task RemoveAsync(string key)
      {
         _cache.Remove(key);
         return Task.FromResult(0);
      }
   }
}
