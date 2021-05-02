using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Helpers;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class AnnouncementController : BaseController
   {
      private IApiClient _ApiClient;
      private readonly ILogger<AnnouncementController> _logger;

      public AnnouncementController(IApiClient ApiClient, IOptionsSnapshot<RootConfiguration> rootConfiguration, ILogger<AnnouncementController> logger) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
      }

      [Authorize(RoleNames.GetAnnouncementDetailsPolicy)]
      public async Task<ActionResult> GetAnnouncementDetails(string AnnouncementIdString)
      {
         try
         {
            AnnouncementDetailsModel announcementDetailsModel = await _ApiClient.GetAsync<AnnouncementDetailsModel>("Announcement/GetAnnouncementDetails/" + AnnouncementIdString, null);
            return View(announcementDetailsModel);
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

      #region Supplier

      [Authorize(RoleNames.GetAllAgencyAnnouncementPolicy)]
      public ActionResult AllAgencyAnnouncements()
      {
         return View();
      }

      [Authorize(RoleNames.GetAllAgencyAnnouncementPolicy)]
      public async Task<ActionResult> AllAgencyAnnouncementsPaging(AllAgencyAnnouncementSearchCriteria announcementCriteria)
      {
         try
         {
            announcementCriteria.FromPublishDate = DateHelper.GetDate(announcementCriteria.FromPublishDateString);
            announcementCriteria.ToPublishDate = DateHelper.GetDate(announcementCriteria.ToPublishDateString);
            var result = await _ApiClient.GetQueryResultAsync<AllAnouuncementsForAgencyModel>("Announcement/GetAllAgencyAnnouncements/", announcementCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, announcementCriteria.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.GetUnderOperationAgencyAnnouncementPolicy)]
      public ActionResult UnderOperationAgencyAnnouncements()
      {
         return View();
      }

      [Authorize(RoleNames.GetUnderOperationAgencyAnnouncementPolicy)]
      public async Task<ActionResult> UnderOperationAgencyAnnouncementsPaging(UnderOperationAgencyAnnouncementSearchCriteria announcementCriteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<UnderOperationAnouuncementsForAgencyModel>("Announcement/GetUnderOperationAgencyAnnouncements/", announcementCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, announcementCriteria.PageSize, result.PageNumber, null));
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

      #endregion

      #region Supplier

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> SupplierAnnouncements(string announcmentIdString)
      {
         return View();
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public ActionResult AllSupplierAnnouncements()
      {
         return View();
      }

      [AllowAnonymous]
      public ActionResult AllVisitorAnnouncements()
      {
         return View();
      }

      [AllowAnonymous]
      public async Task<ActionResult> SupplierAnnouncementDetails(string announcmentIdString)
      {
         try
         {
            //string AnnouncementIdStringg = Util.Encrypt(announcmentIdString);
            SupplierAnnouncementDetailsModel announcementDetailsModel = await _ApiClient.GetAsync<SupplierAnnouncementDetailsModel>("Announcement/GetSupplierAnnouncementDetails/" + announcmentIdString, null);
            return View(announcementDetailsModel);
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

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> SupplierAnnouncementsPaging(SupplierAnnouncementSearchCriteria announcementCriteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<SupplierGridAnnouncementModel>("Announcement/GetSupplierAnnouncements/", announcementCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, announcementCriteria.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> AllSupplierAnnouncementsPaging(AllSupplierAnnouncementSearchCriteria announcementCriteria)
      {
         try
         {
            announcementCriteria.FromLastJoinDate = DateHelper.GetDate(announcementCriteria.FromLastJoinDateString);
            announcementCriteria.ToLastJoinDate = DateHelper.GetDate(announcementCriteria.ToLastJoinDateString);
            var result = await _ApiClient.GetQueryResultAsync<AllAnouuncementsForSupplierVisitorModel>("Announcement/GetAllSupplierAnnouncements/", announcementCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, announcementCriteria.PageSize, result.PageNumber, null));
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

      [AllowAnonymous]
      public async Task<ActionResult> AllVisitorAnnouncementsPaging(AllSupplierAnnouncementSearchCriteria announcementCriteria)
      {
         try
         {
            announcementCriteria.FromLastJoinDate = DateHelper.GetDate(announcementCriteria.FromLastJoinDateString);
            announcementCriteria.ToLastJoinDate = DateHelper.GetDate(announcementCriteria.ToLastJoinDateString);
            var result = await _ApiClient.GetQueryResultAsync<AllAnouuncementsForSupplierVisitorModel>("Announcement/GetAllVisitorAnnouncements/", announcementCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, announcementCriteria.PageSize, result.PageNumber, null));
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
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> JoinAnnouncement(string announcmentIdString)
      {
         try
         {
            int announcmentId = Util.Decrypt(announcmentIdString);
            await _ApiClient.PostAsync("Announcement/JoinAnnouncement/" + announcmentId, null, null);
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
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> WithdrawJoinRequest(string announcmentIdString)
      {
         try
         {
            int announcmentId = Util.Decrypt(announcmentIdString);
            await _ApiClient.PostAsync("Announcement/WithdrawJoinRequest/" + announcmentId, null, null);
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

      #endregion

      #region Create-Announcement

      [HttpGet]
      [Authorize(Policy = PolicyNames.CreateAnnouncementPolicy)]
      public async Task<ActionResult> CreateAnnouncement(string announcementIdString)
      {
         try
         {
            CreateAnnouncementModel announcementModel = new CreateAnnouncementModel();
            if (string.IsNullOrEmpty(announcementIdString))
            {
               announcementModel.CreatedBy = User.FullName();
               announcementModel.AnnouncementPeriod = 10;
               return View(announcementModel);
            }
            var newAnnouncementModel = await _ApiClient.GetAsync<CreateAnnouncementModel>("Announcement/GetAnnouncementByIdForEdit/" + announcementIdString, null);
            return View(newAnnouncementModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AllAgencyAnnouncements));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AllAgencyAnnouncements));
         }

      }

      [HttpPost]
      [Authorize(Policy = PolicyNames.CreateAnnouncementPolicy)]

      public async Task<IActionResult> CreateAnnouncement(CreateAnnouncementModel announcementModel)
      {
         try
         {
            if (announcementModel.InsideKsa)
            {
               ModelState.Remove("CountriesIds");
               announcementModel.CountriesIds.Clear();
            }
            else
            {
               ModelState.Remove("AreasIds");
               announcementModel.AreasIds.Clear();
            }

            if (!ModelState.IsValid)
            {
               announcementModel.CreatedBy = User.FullName();
               return View(announcementModel);
            }
            await _ApiClient.PostAsync("Announcement/CreateNewAnnouncement", null, announcementModel);
            return RedirectToAction(nameof(AllAgencyAnnouncements));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            announcementModel.CreatedBy = User.FullName();
            return View(announcementModel);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }
      #endregion

      #region Announcement-Approval-Operation

      [HttpGet]
      public async Task<ActionResult> CreateVerificationCode(CreateVerificationCodeModel createVerification)
      {

         try
         {
            await _ApiClient.PostAsync("Announcement/CreateVerificationCode", null, createVerification);
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

      [HttpGet]
      [Authorize(Policy = PolicyNames.SendAnnouncementForApprovalPolicy)]
      public async Task<ActionResult> SendAnnouncementForApprovalAsync(string announcementIdString)
      {

         try
         {
            await _ApiClient.PostAsync("Announcement/SendAnnouncementForApproval/" + announcementIdString, null, null);
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

      [HttpGet]
      [Authorize(Policy = PolicyNames.ApproveAnnouncementPolicy)]
      public async Task<ActionResult> ApproveAnnouncementAsync(VerificationModel verificationModel)
      {

         try
         {
            var result = await _ApiClient.PostAsync<ApproveAnnouncemntModel>("Announcement/ApproveAnnouncement", null, verificationModel);
            return Ok(result);
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

      [HttpGet]
      [Authorize(Policy = PolicyNames.RejectApproveAnnouncementPolicy)]
      public async Task<ActionResult> RejectApproveAnnouncementAsync(RejectionReasonModel rejectionModel)
      {

         try
         {
           await _ApiClient.PostAsync("Announcement/RejectApproveAnnouncement", null, rejectionModel);
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

      [HttpGet]
      [Authorize(Policy = PolicyNames.ReopenAnnouncementPolicy)]
      public async Task<ActionResult> ReopenAnnouncementAsync(string announcementIdString)
      {

         try
         {
            await _ApiClient.PostAsync("Announcement/ReopenAnnouncement/" + announcementIdString, null, null);
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

      #endregion Announcement-Approval-Operation

      #region Delete-Announcement
      [Authorize(Policy = PolicyNames.DeleteAnnouncementPolicy)]
      public async Task<ActionResult> DeleteAnnouncement(string announcementIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Announcement/DeleteAnnouncement/" + announcementIdString, null, null);
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

      #endregion Delete-Announcement

      //[Authorize(RoleNames.GetAnnouncementDetailsPolicy)]
      [AllowAnonymous]
      public async Task<ActionResult> OpenAnnouncementDetailsReport(string announcementIdString)
      {
         try
         {
            SupplierAnnouncementDetailsModel result = await _ApiClient.GetAsync<SupplierAnnouncementDetailsModel>("Announcement/GetSupplierAnnouncementDetails/" + announcementIdString, null);

            return PartialView("Partials/_AnnouncementDetailsForPrint", result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #region Lookups
      [HttpGet]
      [AllowAnonymous]

      public async Task<List<TenderTypeModel>> GetTenderTypes()
      {
         if (User.UserIsVRO())
         {
            return new List<TenderTypeModel>();
         }
         var areaList = await _ApiClient.GetListAsync<TenderTypeModel>("Announcement/GetTenderTypes", null);
         return areaList;
      }
      #endregion Lookups
   }
}
