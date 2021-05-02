using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.ViewComponents.Tender
{

   [ViewComponent(Name = "OfferAttachmentsUpdate")]
   public class OfferAttachmentsUpdateComponent : ViewComponent
   {
      private IApiClient _ApiClient;
      private IMemoryCache _cache;
      //private readonly IConfigurationRoot _configuration;
      private readonly RootConfiguration _rootConfiguration;
      public OfferAttachmentsUpdateComponent(IApiClient ApiClient, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration)
      {
         _ApiClient = ApiClient;
         _cache = cache;
         //_configuration = new ConfigurationBuilder()
         //        .SetBasePath(Directory.GetCurrentDirectory())
         //        .AddJsonFile("appsettings.json")
         //        .Build();
         _rootConfiguration = rootConfiguration.Value;
      }

      public async Task<IViewComponentResult> InvokeAsync(string combinedId)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });
            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailModel model = new OfferDetailModel();
            model = await _ApiClient.GetAsync<OfferDetailModel>("Offer/GetOfferDetails/" + Util.Decrypt(combinedId), null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.CombinedIdString = combinedId;
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
      }

   }
}
