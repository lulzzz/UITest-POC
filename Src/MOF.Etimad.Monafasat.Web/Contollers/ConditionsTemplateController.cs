using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Controllers
{
   // Todo: Will come back to this file to make more enhancments for conditons template
   [Authorize]
   public class ConditionsTemplateController : BaseController
   {
      private readonly IApiClient _apiClient;
      private readonly IMemoryCache _cache;
      //private readonly IConfigurationRoot _configuration;

      public ConditionsTemplateController(IApiClient apiClient, IMemoryCache memoryCache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _apiClient = apiClient;
         _cache = memoryCache;
         // Todo: Check better way to injecet the confiugration.
         //_configuration = new ConfigurationBuilder()
         //         .SetBasePath(Directory.GetCurrentDirectory())
         //         .AddJsonFile("appsettings.json")
         //         .Build();
      }

      [HttpGet("ConditionsTemplate/Index/{tenderId}")]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> Index(string tenderId)
      {
         try
         {
            var model = await _apiClient.GetAsync<GetConditionTemplateModel>("Tender/GetConditionTemplate/" + tenderId, null);
            model.Certificates = await _cache.GetOrCreateAsync(CacheKeys.GetBookletCertificates, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes/*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromHours(minutes);
               return await _apiClient.GetListAsync<LookupModel>("Lookup/GetBookletCertificates", null);
            });

            return View(model);
         }
         catch (AuthorizationException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("Index", "Tender");
         }
      }
   }
}
