using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{

   public class LookupController : BaseController
   {
      private IApiClient _ApiClient;
      public IMemoryCache _cache { get; }

      public LookupController(IApiClient ApiClient, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _cache = cache;
      }

      [Authorize(RoleNames.GetAllAgencyAnnouncementPolicy)]
      [HttpGet]
      public async Task<List<LookupModel>> GetAnnouncementStatusAsync()
      {
         var statusList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAnnounementStatus", null);
         return statusList;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAnnouncementStatusSupplierTemplatesLookup()
      {
         var statusList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAnnouncementStatusSupplierTemplatesLookup", null);
         return statusList;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAreasAsync()
      {
         List<LookupModel> areaList = await _cache.GetOrCreateAsync(CacheKeys.GetAreas, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
         });
         return areaList;
      }
      [HttpGet]
      public async Task<List<CountryModel>> GetCountriesync()
      {
         List<CountryModel> countryList = await _cache.GetOrCreateAsync(CacheKeys.GetCountries, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<CountryModel>("Lookup/GetCountries", null);
         });
         return countryList;
      }

      [HttpGet]
      public async Task<List<ConstructionWorkModel>> GetConstractionWorkAsync()
      {
         List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
         });
         return constructionWorks;
      }

      [HttpGet]
      public async Task<List<MaintenanceRunningWorkModel>> GetmaintenanceWorksAsync()
      {
         List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
         });
         return maintenanceWorks;
      }

      [HttpGet]
      public async Task<List<SelectListItem>> GetActivitiesAsync()
      {
         var activitiesList = await _ApiClient.GetListAsync<ActivityModel>("Lookup/GetActivities", null);
         var activitiesItems = new List<SelectListItem>();
         foreach (var item in activitiesList)
         {
            var group = new SelectListGroup { Name = item.Name };
            foreach (var sub in item.SubActivities)
            {
               activitiesItems.Add(new SelectListItem { Value = sub.ActivityId.ToString(), Text = sub.Name, Group = group });
            }
         }
         return activitiesItems;
      }

   }
}
