using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{

   public class LocalContentSettingsController : BaseController
   {
      private readonly IApiClient _apiClient;
      private readonly ILogger<LocalContentSettingsController> _logger;


      public LocalContentSettingsController(IApiClient apiClient, ILogger<LocalContentSettingsController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _apiClient = apiClient;
         _logger = logger;
      }
      //[Authorize(RoleNames.LocalContentOfficer)]
      public async Task<IActionResult> Index()
      {
         var viewModel = await _apiClient.GetAsync<LocalContentSettingsViewModel>("LocalContentSettings/Find", null);
         return View(viewModel);
      }

      //[Authorize(RoleNames.LocalContentOfficer)]
      [HttpPost]
      public async Task<IActionResult> Index(LocalContentSettingsViewModel viewModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(viewModel);
         }

         try
         {
            await _apiClient.PostAsync("LocalContentSettings/Update", null, viewModel);

            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(viewModel);
         }
         catch (AuthorizationException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(ex.Message);
            return View(viewModel);
         }
      }

   }
}
