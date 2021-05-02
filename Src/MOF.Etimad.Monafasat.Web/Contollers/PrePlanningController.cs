using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class PrePlanningController : BaseController
   {
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly IApiClient _ApiClient;
      private readonly ILogger<PrePlanningController> _logger;
      private readonly IMemoryCache _cache;
      public PrePlanningController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, ILogger<PrePlanningController> logger, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
         _logger = logger;
         _cache = cache;
      }

      private async Task<PrePlanningModel> PrepareEditingDataAsync(string id, PrePlanningModel model = null)
      {
         model = model ?? new PrePlanningModel();
         if (!string.IsNullOrEmpty(id))
         {
            model = await _ApiClient.GetAsync<PrePlanningModel>("PrePlanning/GetPrePlanningModelById/" + Util.Decrypt(id), null);
            if (model.StatusId == (int)Enums.PrePlanningStatus.Pending || model.StatusId == (int)Enums.PrePlanningStatus.Rejected)
            {
               throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);
            }
         }
         else
         {
            model = await _ApiClient.GetAsync<PrePlanningModel>("PrePlanning/SetPrePlanningLookUps", null);
         }
         return model;
      }
      private async Task FillLookUpsAsync(PrePlanningModel model, string id)
      {
         PrePlanningLookUpsModels lookUps = await _cache.GetOrCreateAsync(CacheKeys.PrePlanningCache/* + "_" + HttpContext.Session.Id*/, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetAsync<PrePlanningLookUpsModels>("Lookup/GetPrePlanningLookups", null);
         });
         List<SelectListItem> activitiesItems = new List<SelectListItem>();
         foreach (var item in lookUps.ProjectTypes)
         {
            var group = new SelectListGroup { Name = item.Name };
            foreach (var sub in item.SubActivities)
            {
               activitiesItems.Add(new SelectListItem { Value = sub.ActivityId.ToString(), Text = sub.Name, Group = group });
            }
         }
         List<LookupModel> yearQuarter = new List<LookupModel>(lookUps.YearQuarters); ;
         model.ProjectTypesList = activitiesItems;
         model.YearQuarters = yearQuarter;
         model.Areas = lookUps.Areas;
         model.Countries = lookUps.Countries;
         model.EncyptedPrePlanningId = id;

         if (DateTime.Now.Month == 4 || DateTime.Now.Month == 5 || DateTime.Now.Month == 6)
         {
            model.YearQuarters.Remove(model.YearQuarters[0]);
         }
         else if (DateTime.Now.Month == 7 || DateTime.Now.Month == 8 || DateTime.Now.Month == 9)
         {
            model.YearQuarters.Remove(model.YearQuarters[0]);
            model.YearQuarters.Remove(model.YearQuarters[0]);
         }
         else if (DateTime.Now.Month == 10 || DateTime.Now.Month == 11 || DateTime.Now.Month == 12)
         {
            model.YearQuarters.Remove(model.YearQuarters[0]);
            model.YearQuarters.Remove(model.YearQuarters[0]);
            model.YearQuarters.Remove(model.YearQuarters[0]);
         }

      }


      public async Task<PrePlanningLookUpsModels> GetLookUpsAsync()
      {
         PrePlanningLookUpsModels lookUps = await _cache.GetOrCreateAsync(CacheKeys.PrePlanningCache/* + "_" + HttpContext.Session.Id*/, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetAsync<PrePlanningLookUpsModels>("Lookup/GetPrePlanningLookups", null);
         });
         List<SelectListItem> activitiesItems = new List<SelectListItem>();
         foreach (var item in lookUps.ProjectTypes)
         {
            var group = new SelectListGroup { Name = item.Name };
            foreach (var sub in item.SubActivities)
            {
               activitiesItems.Add(new SelectListItem { Value = sub.ActivityId.ToString(), Text = sub.Name, Group = group });
            }
         }
         lookUps.ProjectTypesList = activitiesItems;
         return lookUps;
      }
      [HttpGet]
      [Authorize(RoleNames.PrePlanningCreationPolicy)]
      public async Task<ActionResult> AddEditPrePlanning(string id)
      {
         try
         {
            PrePlanningModel model = await PrepareEditingDataAsync(id);
            await FillLookUpsAsync(model, id);
            return View(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            PrePlanningModel model = await PrepareEditingDataAsync(id);
            await FillLookUpsAsync(model, id);
            return View(model);
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
         catch (Exception e)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      [Authorize(RoleNames.PrePlanningCreationPolicy)]
      public async Task<ActionResult> AddEditPrePlanning(PrePlanningModel model)
      {

         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            await FillLookUpsAsync(model, model.EncyptedPrePlanningId);
            return View(model);
         }
         try
         {
            if (!string.IsNullOrEmpty(model.EncyptedPrePlanningId))
            {
               model.PrePlanningId = Util.Decrypt(model.EncyptedPrePlanningId);
            }
            PrePlanningModel prePlanningModel = await _ApiClient.PostAsync<PrePlanningModel>("PrePlanning/Add", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction("Details", new { id = Util.Encrypt(prePlanningModel.PrePlanningId) });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         var myModel = PrepareEditingDataAsync(model.EncyptedPrePlanningId, model);

         await FillLookUpsAsync(model, model.EncyptedPrePlanningId);
         return View(model);
      }

      [Authorize(RoleNames.PrePlanningIndexPolicy)]
      public ActionResult Index()
      {
         return View();
      }

      [Authorize(RoleNames.PrePlanningIndexPolicy)]
      public async Task<IActionResult> IndexPagingAsync(PrePlanningSearchCriteria criteria)
      {
         try
         {

            var committees = await _ApiClient.GetQueryResultAsync<PrePlanningModel>("PrePlanning/FindPrePlanningBySearchCriteria", criteria.ToDictionary());
            return Json(new PaginationModel(committees.Items, committees.TotalCount, committees.PageSize, committees.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.PrePlanningIndexPolicy)]
      public async Task<ActionResult> Details(string id)
      {
         try
         {
            var model = await _ApiClient.GetAsync<PrePlanningModel>("PrePlanning/GetPrePlanningDetailsById/" + Util.Decrypt(id), null);

            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }
      [Authorize(RoleNames.PrePlanningIndexPolicy)]
      public async Task<ActionResult> Deactivate(string id)
      {
         try
         {
            await _ApiClient.GetAsync<int>("PrePlanning/Deactivate/" + Util.Decrypt(id), null);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }


      [HttpPost]
      [Authorize(RoleNames.PrePlanningCreationPolicy)]
      public async Task<ActionResult> SendToApproveAsync(string idString)
      {
         try
         {
            int id = Util.Decrypt(idString);
            await _ApiClient.PostAsync("PrePlanning/SendToApprove/" + id, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [Authorize(RoleNames.PrePlanningAuditingPolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateVerificationCode(string IdString)
      {
         try
         {
            var result = await _ApiClient.PostAsync("PrePlanning/CreateVerificationCode/" + Util.Decrypt(IdString), null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [Authorize(RoleNames.PrePlanningAuditingPolicy)]
      [HttpPost]
      public async Task<ActionResult> RejectAsync(string IdString, string rejectionReason)
      {
         try
         {
            int id = Util.Decrypt(IdString);
            await _ApiClient.PostAsync("PrePlanning/Reject/" + id.ToString(), null, rejectionReason);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [HttpPost]
      [Authorize(RoleNames.PrePlanningAuditingPolicy)]
      public async Task<ActionResult> ApproveAsync(string IdString, string verificationCode)
      {
         try
         {
            int id = Util.Decrypt(IdString);
            await _ApiClient.PostAsync("PrePlanning/Approve/" + id + "/" + verificationCode, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.PrePlanningCreationPolicy)]
      public async Task<ActionResult> ReopenAsync(string IdString)
      {
         try
         {
            int id = Util.Decrypt(IdString);
            await _ApiClient.PostAsync("PrePlanning/ReOpen/" + id, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.PrePlanningCreationPolicy)]
      public async Task<ActionResult> ReOpenForCancelation(string IdString)
      {
         try
         {
            int id = Util.Decrypt(IdString);
            await _ApiClient.PostAsync("PrePlanning/ReOpenForCancelation/" + id, null, null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

   }
}
