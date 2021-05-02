using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class EnquiryController : BaseController
   {
      private IApiClient _ApiClient;
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly int _pageSize = (int)Enums.PageSize.Ten;
      private readonly ILogger<EnquiryController> _logger;


      public EnquiryController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, ILogger<EnquiryController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
         _logger = logger;
      }

      #region Enquiry

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> AddEnquiry(string id)
      {
         int tenderId = Util.Decrypt(id);
         ViewBag.tenderId = tenderId;
         return View();
      }

      [Authorize(RoleNames.AuditerAndTechnicalPolicy)]
      public async Task<ActionResult> SupplierEnquiryList()
      {
         return View();
      }

      [Authorize(RoleNames.AuditerAndTechnicalPolicy)]
      public async Task<ActionResult> EnquirySupplierListPagingAsync(TenderSearchCriteriaModel criteria)
      {
         try
         {
            if (string.IsNullOrEmpty(criteria.Sort))
            {
               criteria.Sort = "SubmitionDate";
            }
            var result = await _ApiClient.GetQueryResultAsync<TenderInformationModel>("Tender/GetAllTendersHasEnquiry", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(SupplierEnquiryList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(SupplierEnquiryList));
         }

      }

      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> SendEnquiry(EnquiryModel enquiryObj)
      {
         try
         {
            var result = await _ApiClient.PostAsync<EnquiryModel>("Enquiry/SendEnquiry", null, enquiryObj);
            return Json(result);
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

      [Authorize(RoleNames.SupplierEnquiriesOnTenderPolicy)]
      public async Task<ActionResult> SupplierEnquiriesOnTender(string id)
      {
         try
         {
            TenderInformationModel tenderInformationModel = await _ApiClient.GetAsync<TenderInformationModel>("Tender/GetTenderByIdForEnquiry/" + Util.Decrypt(id).ToString(), null);
            return View(tenderInformationModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("AllSuppliersTenders", "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(SupplierEnquiryList));
         }
      }

      #endregion

      #region Enquiry Reply

      [HttpGet]
      public async Task<ActionResult> GetAllSuppliersEnquiriesByTenderId(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<EnquiryModel>("Enquiry/GetAllSuppliersEnquiriesByTenderId/" + tenderId.ToString(), null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(SupplierEnquiryList));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(SupplierEnquiryList));
         }
      }

      [HttpGet]
      public async Task<ActionResult> GetAllEnquiryRepliesByTenderIdSerach(SimpleSearchCretriaModel criteria)
      {
         try
         {
            criteria.TenderId = Util.Decrypt(criteria.TenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<EnquiryModel>("Enquiry/GetAllEnquiryRepliesByTenderId", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
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
      public async Task<ActionResult> GetAllEnquiryRepliesByEnquiryId(SimpleSearchCretriaModel criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<EnquiryReplyModel>("Enquiry/GetAllEnquiryRepliesByEnquiryId", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.TechnicalCommitteeUserPolicy)]
      [HttpGet]
      public async Task<ActionResult> EnquiryDetails(string enquiryIdString)
      {
         try
         {
            EnquiryModel enquiryModel = await _ApiClient.GetAsync<EnquiryModel>("Enquiry/GetEnquiryById/" + Util.Decrypt(enquiryIdString), null);
            return View(enquiryModel);
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
      public async Task<ActionResult> GetEnquiryReplyByReplyId(string enquiryReplyIdString)
      {
         try
         {
            int enquiryReplyId = Util.Decrypt(enquiryReplyIdString);
            EnquiryReplyModel enquiryReplyModel = await _ApiClient.GetAsync<EnquiryReplyModel>("Enquiry/GetEnquiryReplyByReplyId/" + enquiryReplyId, null);
            return Ok(enquiryReplyModel);
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
      public async Task<ActionResult> AddEnquiryReply(EnquiryReplyModel enquiryReplyObj)
      {
         var result = new EnquiryReplyModel();
         try
         {
            result = await _ApiClient.PostAsync<EnquiryReplyModel>("Enquiry/AddEnquiryReply", null, enquiryReplyObj);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      public async Task<ActionResult> AddEnquiryComment(EnquiryReplyModel enquiryReplyObj)
      {
         var result = new EnquiryReplyModel();
         try
         {
            result = await _ApiClient.PostAsync<EnquiryReplyModel>("Enquiry/AddEnquiryComment", null, enquiryReplyObj);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      public async Task<ActionResult> EditEnquiryReply(EnquiryReplyModel enquiryReplyObj)
      {
         var result = new EnquiryReplyModel();
         try
         {
            result = await _ApiClient.PostAsync<EnquiryReplyModel>("Enquiry/EditEnquiryReply", null, enquiryReplyObj);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      public async Task<ActionResult> ApproveEnquiryReply(string enquiryReplyIdString)
      {
         var result = new EnquiryReplyModel();
         try
         {
            int enquiryReplyId = Util.Decrypt(enquiryReplyIdString);
            result = await _ApiClient.GetAsync<EnquiryReplyModel>("Enquiry/ApproveEnquiryReply/" + enquiryReplyId, null);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      public async Task<ActionResult> Delete(string enquiryReplyIdString)
      {
         try
         {
            int enquiryReplyId = Util.Decrypt(enquiryReplyIdString);
            await _ApiClient.GetAsync<object>("Enquiry/Delete/" + enquiryReplyId.ToString(), null);
            return Ok();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(EnquiryDetails));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(EnquiryDetails));
         }
      }

      #endregion

      #region Join Technical Committee and internal comments

      [HttpGet]
      public async Task<ActionResult> JoinTechnicalCommittee(string enquiryIdString)
      {
         try
         {

            int enquiryId = Util.Decrypt(enquiryIdString);

            JoinTechnicalCommitteeModel joinTechnicalCommitteeModel = await _ApiClient.GetAsync<JoinTechnicalCommitteeModel>("Enquiry/GetJoinTechnicalCommitteeByEnquiryId/" + enquiryId, null);
            EnquiryModel enquiryModel = await _ApiClient.GetAsync<EnquiryModel>("Enquiry/GetEnquiryById/" + enquiryId, null);
            var technicalCommitteeList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetTechnicalCommitteeListOnBranchTender/" + enquiryModel.TenderId, null);

            if (joinTechnicalCommitteeModel.JoinTechnicalCommitteeId == 0)
            {
               joinTechnicalCommitteeModel.EnquiryIdString = enquiryIdString;
               joinTechnicalCommitteeModel.EnquiryId = enquiryId;
               joinTechnicalCommitteeModel.TenderId = enquiryModel.TenderId;
               joinTechnicalCommitteeModel.EnquiryName = enquiryModel.EnquiryName;
               joinTechnicalCommitteeModel.EnquirySendingDate = enquiryModel.EnquirySendingDate;
               joinTechnicalCommitteeModel.CommitteeList = technicalCommitteeList;
            }
            else
            {
               joinTechnicalCommitteeModel.CommitteeList = technicalCommitteeList;
               joinTechnicalCommitteeModel.EnquiryName = enquiryModel.EnquiryName;
               joinTechnicalCommitteeModel.TenderId = enquiryModel.TenderId;
               joinTechnicalCommitteeModel.EnquirySendingDate = enquiryModel.EnquirySendingDate;
            }
            return View(joinTechnicalCommitteeModel);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(EnquiryDetails));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(EnquiryDetails), new { enquiryIdString = enquiryIdString });
         }
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetTechnicalCommitteeList()
      {
         var technicalCommitteeList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetTechnicalCommitteeList", null);
         return technicalCommitteeList;
      }
      [HttpGet]
      public async Task<List<LookupModel>> GetTechnicalCommitteeListOnBranch()
      {
         var technicalCommitteeList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetTechnicalCommitteeListOnBranch", null);
         return technicalCommitteeList;
      }

      [HttpGet]
      public async Task<ActionResult> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<JoinTechnicalCommitteeModel>("Enquiry/GetInvitedCommitesByEnquiryId", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
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
      public async Task<ActionResult> RemoveInvitedCommitteeAsync(int committeeId, int replyId, int enquiryId)
      {
         try
         {
            await _ApiClient.PostAsync("Enquiry/RemoveInvitedCommittee", new Dictionary<string, object> { { nameof(committeeId), committeeId }, { nameof(replyId), replyId }, { nameof(enquiryId), enquiryId } }, null);
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
      public async Task<ActionResult> SendInvitationsToJoinCommittee(JoinTechnicalCommitteeModel joinTechnicalCommitteeModel)
      {
         JoinTechnicalCommitteeModel result = new JoinTechnicalCommitteeModel();
         try
         {
            result = await _ApiClient.PostAsync<JoinTechnicalCommitteeModel>("Enquiry/SendInvitationsToJoinCommittee", null, joinTechnicalCommitteeModel);
            return Json(result);
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

   }
}

