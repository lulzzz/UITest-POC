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
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Web.Contollers
{

   public class AnnouncementSuppliersTemplateController : BaseController
   {
      private readonly IApiClient _ApiClient;
      private readonly ILogger<AnnouncementSuppliersTemplateController> _logger;

      public AnnouncementSuppliersTemplateController(IApiClient ApiClient, IOptionsSnapshot<RootConfiguration> rootConfiguration, ILogger<AnnouncementSuppliersTemplateController> logger) : base(rootConfiguration)
      {
         _ApiClient = ApiClient;
         _logger = logger;
      }

      public ActionResult Index()
      {
         return View();
      }

      [Authorize(RoleNames.GetAnnouncementSupplierTemplatePolicy)]
      public ActionResult AllAgencyAnnouncementSupplierTemplates()
      {
         return View();
      }


      [Authorize(RoleNames.GetAnnouncementSupplierTemplatePolicy)]
      public async Task<ActionResult> GetAllAnnouncementSupplierTemplates(AgencyAnnouncementSupplierTemplateSearchCriteria criteria)
      {
         try
         {
            criteria.FromPublishDate = DateHelper.GetDate(criteria.FromPublishDateString);
            criteria.ToPublishDate = DateHelper.GetDate(criteria.ToPublishDateString);
            var result = await _ApiClient.GetQueryResultAsync<AllAnnouncementSupplierTemplateAgencyModel>("AnnouncementSupplierTemplate/GetAllAnnouncementSupplierTemplates/", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, criteria.PageSize, result.PageNumber, null));
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


      //[Authorize(PolicyNames.GetAllAnnouncementSupplierTemplatesForSupplierPolicy)]
      [AllowAnonymous]
      public ActionResult AllSupplierAnnouncementSupplierTemplates()
      {
         return View();
      }

      //[Authorize(PolicyNames.GetAllAnnouncementSupplierTemplatesForSupplierPolicy)]
      [AllowAnonymous]
      public async Task<ActionResult> GetAllAnnouncementSupplierTemplatesForSupplier(AnnouncementSupplierTemplateSupplierSearchCriteriaModel criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<AnnouncementSupplierTemplateSupplierGridModel>("AnnouncementSupplierTemplate/GetAllAnnouncementSupplierTemplatesForSupplier/", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, criteria.PageSize, result.PageNumber, null));
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
      [Authorize(Policy = PolicyNames.CreateAnnouncementTemplatePolicy)]
      public async Task<ActionResult> CreateAnnouncementSuppliersTemplate(string announcementIdString)
      {
         try
         {
            CreateAnnouncementSupplierTemplateModel announcementModel = new CreateAnnouncementSupplierTemplateModel();
            if (string.IsNullOrEmpty(announcementIdString))
            {
               announcementModel.CreatedBy = User.FullName();
               announcementModel.AgencyName = User.UserAgencyName();
               return View(announcementModel);
            }
            var newAnnouncementModel = await _ApiClient.GetAsync<CreateAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/GetAnnouncementSupplierTemplateByIdForEdit/" + announcementIdString, null);
            await GetEditModeAttahmentsData(newAnnouncementModel);
            return View(newAnnouncementModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }

      }

      [HttpPost]
      [Authorize(Policy = PolicyNames.CreateAnnouncementTemplatePolicy)]
      public async Task<IActionResult> CreateAnnouncementSuppliersTemplate(CreateAnnouncementSupplierTemplateModel announcementModel)
      {
         try
         {
            announcementModel.BranchId = User.UserBranch();
            announcementModel.AgencyCode = User.UserAgency();

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
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               await GetEditModeAttahmentsData(announcementModel);
               announcementModel.CreatedBy = User.FullName();
               return View(announcementModel);
            }

            if (!string.IsNullOrEmpty(announcementModel.AnnouncementIdString))
            {
               announcementModel.AnnouncementId = Util.Decrypt(announcementModel.AnnouncementIdString);
            }
            if (announcementModel.TenderItemTypes != null && announcementModel.TenderItemTypes.Any())
            {
               foreach (var item in announcementModel.TenderItemTypes)
               {
                  announcementModel.TenderTypesId += item + ",";
               }
               announcementModel.TenderTypesId = announcementModel.TenderTypesId.Substring(0, announcementModel.TenderTypesId.Length - 1);
            }

            if (announcementModel.IsEffectiveList && announcementModel.EffectiveListDate.HasValue)
            {
               bool isVacationDayFordailyTime = (announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Saturday" || announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Friday");
               if (isVacationDayFordailyTime)
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.VacationDaysTime);
               }
               else
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.AllDaysTime);
               }
            }

            announcementModel.Attachments = await PreparePostedAttachmentsModel(announcementModel.AttachmentReference);
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = await _ApiClient.PostAsync<CreateAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/CreateNewAnnouncementSupplierTemplate", null, announcementModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);

            return RedirectToAction(nameof(AnnouncementSuppliersTemplateApproval), new { announcementSupplierTemplateString = Util.Encrypt(createAnnouncementSupplierTemplateModel.AnnouncementId) });
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
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
      }




      [Authorize(Policy = PolicyNames.CreateAnnouncementTemplatePolicy)]
      public async Task<ActionResult> SaveDraft(CreateAnnouncementSupplierTemplateModel announcementModel)
      {
         try
         {
            if (announcementModel.TenderItemTypes != null && announcementModel.TenderItemTypes.Any())
            {
               foreach (var item in announcementModel.TenderItemTypes)
               {
                  announcementModel.TenderTypesId += item + ",";
               }
               announcementModel.TenderTypesId = announcementModel.TenderTypesId.Substring(0, announcementModel.TenderTypesId.Length - 1);
            }

            if (announcementModel.IsEffectiveList && announcementModel.EffectiveListDate.HasValue)
            {
               bool isVacationDayFordailyTime = (announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Saturday" || announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Friday");
               if (isVacationDayFordailyTime)
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.VacationDaysTime);
               }
               else
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.AllDaysTime);
               }
            }

            announcementModel.Attachments = await PreparePostedAttachmentsModel(announcementModel.AttachmentReference);
            var obj = await _ApiClient.PostAsync<CreateAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/SaveDraft", null, announcementModel);
            return Json(obj);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
      }

      private async Task<List<TenderAttachmentModel>> PreparePostedAttachmentsModel(string attachmentReference)
      {
         List<string> attachmentReferences = new List<string>();
         if (!string.IsNullOrEmpty(attachmentReference))
         {
            string[] lst = attachmentReference.Split(',');
            foreach (var item in lst)
            {
               if (!string.IsNullOrEmpty(item))
               {
                  if (item.Contains("/GetFile?id="))
                     attachmentReferences.Add(item.Split("/GetFile?id=")[item.Split("/GetFile?id=").Length - 1]);
                  else
                     attachmentReferences.Add(item);
               }
            }
         }
         List<TenderAttachmentModel> tenderAttachments = new List<TenderAttachmentModel>();
         foreach (var item in attachmentReferences)
         {
            var arr = item.Split(":");
            TenderAttachmentModel tenderAttachment = new TenderAttachmentModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.TenderOtherFile, FileNetReferenceId = arr[0] };
            tenderAttachments.Add(tenderAttachment);
         }
         return tenderAttachments;
      }

      private async Task GetEditModeAttahmentsData(CreateAnnouncementSupplierTemplateModel model)
      {
         var list = model.Attachments;

         if (list != null)
         {
            model.AttachmentReference = "";
            foreach (var item in list)
            {
               model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
            }
            if (model.AttachmentReference.Length > 0)
               model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
         }
      }

      private async Task GetJoinRequestAttachments(AnnouncementTemplateMainDetailsModel model)
      {
         var list = model.Attachments;

         if (list != null)
         {
            model.AttachmentReference = "";
            foreach (var item in list)
            {
               model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
            }
            if (model.AttachmentReference.Length > 0)
               model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
         }
      }

      private async Task GetJoinRequestAttachmentsForReview(AnnouncementSuppliersTemplateJoinRequestsDetailsModel model)
      {
         var list = model.Attachments;

         if (list != null)
         {
            model.AttachmentReference = "";
            foreach (var item in list)
            {
               model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
            }
            if (model.AttachmentReference.Length > 0)
               model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
         }
      }

      private async Task GetEditModeAttahmentsDataForUpdate(UpdateAnnouncementSupplierTemplateModel model)
      {
         var list = model.Attachments;

         if (list != null)
         {
            model.AttachmentReference = "";
            foreach (var item in list)
            {
               model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
            }
            if (model.AttachmentReference.Length > 0)
               model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
         }
      }

      private void GetEditModeAttahmentsDataForExtend(ExtendAnnouncementSupplierTemplateModel model)
      {
         var list = model.Attachments;

         if (list != null)
         {
            model.AttachmentReference = "";
            foreach (var item in list)
            {
               model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
            }
            if (model.AttachmentReference.Length > 0)
               model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
         }
      }

      [Authorize(Policy = PolicyNames.ApproveAnnouncementTemplatePolicy)]
      public async Task<ActionResult> AnnouncementSuppliersTemplateApproval(string announcementSupplierTemplateString)
      {
         try
         {
            var announcementId = Util.Decrypt(announcementSupplierTemplateString);
            var result = await _ApiClient.GetAsync<ApproveAnnouncemntSupplierTemplateModel>("AnnouncementSupplierTemplate/AnnouncementSupplierTemplateForApproval/ " + announcementId, null);

            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return Ok();
      }


      [Authorize(Policy = PolicyNames.DetailsAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> AnnouncementSuppliersTemplateDetails(string AnnouncementId)
      {
         try
         {
            AnnouncementSuppliersTemplateDetailsModel model = new AnnouncementSuppliersTemplateDetailsModel();
            model.AnnouncementId = Util.Decrypt(AnnouncementId);
            model.AnnouncementIdString = AnnouncementId;
            return View(model);

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
      }


      [Authorize(Policy = PolicyNames.PrintAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> AnnouncementSuppliersTemplateJoinRequestsDetails(string AnnouncementId)
      {
         try
         {
            AnnouncementSuppliersTemplateJoinRequestsDetailsModel model = new AnnouncementSuppliersTemplateJoinRequestsDetailsModel();
            model = await _ApiClient.GetAsync<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>("AnnouncementSupplierTemplate/GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId/" + Util.Decrypt(AnnouncementId), null);
            return View(model);

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
      }


      [Authorize(RoleNames.GetAnnouncementSupplierTemplatePolicy)]
      public async Task<ActionResult> AnnouncementJoinRequestReview(string joinRequestIdString)
      {
         try
         {
            AnnouncementSuppliersTemplateJoinRequestsDetailsModel model = new AnnouncementSuppliersTemplateJoinRequestsDetailsModel();
            model.JoinRequestId = Util.Decrypt(joinRequestIdString);
            model = await _ApiClient.GetAsync<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>("AnnouncementSupplierTemplate/GetAnnouncementTemplateJoinRequestDetails/" + Util.Decrypt(joinRequestIdString), null);
            await GetJoinRequestAttachmentsForReview(model);
            return View(model);

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
      }


      [Authorize(RoleNames.GetAnnouncementSupplierTemplatePolicy)]
      public async Task<ActionResult> JoinRequestsPagingAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<JoinRequestModel>("AnnouncementSupplierTemplate/GetJoinRequestsByAnnouncementIdAsync", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      public async Task<ActionResult> ApprovedSuppliersJoinRequestsPagingAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<JoinRequestModel>("AnnouncementSupplierTemplate/GetApprovedSuppliersJoinRequestsByAnnouncementId", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      public async Task<ActionResult> AnnouncementSuppliersTemplateDetailsForSuppliers(string announcmentIdString)
      {
         try
         {
            AnnouncementTemplateMainDetailsModel model = new AnnouncementTemplateMainDetailsModel();
            model.AnnouncementId = Util.Decrypt(announcmentIdString);
            model = await _ApiClient.GetAsync<AnnouncementTemplateMainDetailsModel>("AnnouncementSupplierTemplate/GetAnnouncementTemplateDetailsForSuppliers/" + Util.Decrypt(announcmentIdString), null);
            await GetJoinRequestAttachments(model);
            return View(model);

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
      }
      public async Task<ActionResult> AddPublicListToAgencyAnnouncementLists(string announcmentIdString)
      {
         try
         {
            AddPublicListToAgencyAnnouncementListsModel model = new AddPublicListToAgencyAnnouncementListsModel();
            model.AnnouncementId = Util.Decrypt(announcmentIdString);
            model = await _ApiClient.GetAsync<AddPublicListToAgencyAnnouncementListsModel>("AnnouncementSupplierTemplate/GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists/" + Util.Decrypt(announcmentIdString), null);
            return View(model);

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
      }


      public IActionResult GetAnnouncementSuppliersTemplateDetailsViewComponent(string announcementId)
      {
         return ViewComponent("AnnouncementSuppliersTemplateDetail", new { id = Util.Decrypt(announcementId) });
      }

      [HttpGet]
      public async Task<ActionResult> CreateVerificationCode(CreateVerificationCodeModel createVerification)
      {

         try
         {
            var result = await _ApiClient.PostAsync("AnnouncementSupplierTemplate/CreateVerificationCode", null, createVerification);
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
      [Authorize(Policy = PolicyNames.ApproveAnnouncementTemplatePolicy)]
      public async Task<ActionResult> ApproveAnnouncementAsync(string announcementIdString, string verficationCode)
      {

         try
         {
            VerificationModel verificationModel = new VerificationModel() { IdString = announcementIdString, VerificationCode = verficationCode };
            var result = await _ApiClient.PostAsync<ApproveAnnouncemntSupplierTemplateModel>("AnnouncementSupplierTemplate/ApproveAnnouncement", null, verificationModel);
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







      [HttpPost]
      [Authorize(Policy = PolicyNames.CancelAnnouncementTemplatePolicy)]
      public async Task<ActionResult> ApproveAnnouncementCancelRequestAsync(string announcementIdString, string verficationCode, string cancelationReason)
      {
         AnnouncementSuppliersTemplateCancelModel cancelModel = new AnnouncementSuppliersTemplateCancelModel() { AnnouncementIdString = announcementIdString, VerificationCode = verficationCode, CancelationReason = cancelationReason };
         try
         {
            await _ApiClient.PostAsync<ApproveAnnouncemntSupplierTemplateModel>("AnnouncementSupplierTemplate/ApproveAnnouncementCancelRequestAsync", null, cancelModel);
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
      [Authorize(Policy = PolicyNames.UpdateAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> UpdateAnnouncementSuppliersTemplate(string announcementIdString)
      {
         try
         {
            UpdateAnnouncementSupplierTemplateModel announcementModel = new UpdateAnnouncementSupplierTemplateModel();
            if (string.IsNullOrEmpty(announcementIdString))
            {
               announcementModel.CreatedBy = User.FullName();
               announcementModel.AgencyName = User.UserAgencyName();
               return View(announcementModel);
            }
            var newAnnouncementModel = await _ApiClient.GetAsync<UpdateAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/GetAnnouncementSupplierTemplateByIdForEditApproval/" + announcementIdString, null);
            await GetEditModeAttahmentsDataForUpdate(newAnnouncementModel);
            return View(newAnnouncementModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }


      }

      [HttpPost]
      [Authorize(Policy = PolicyNames.UpdateAnnouncementSuppliersTemplatePolicy)]
      public async Task<IActionResult> UpdateAnnouncementSuppliersTemplate(UpdateAnnouncementSupplierTemplateModel announcementModel)
      {
         try
         {
            announcementModel.BranchId = User.UserBranch();
            announcementModel.AgencyCode = User.UserAgency();

            if (!ModelState.IsValid)
            {
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               await GetEditModeAttahmentsDataForUpdate(announcementModel);
               return View(announcementModel);
            }

            if (!string.IsNullOrEmpty(announcementModel.AnnouncementIdString))
            {
               announcementModel.AnnouncementId = Util.Decrypt(announcementModel.AnnouncementIdString);
            }
            if (announcementModel.IsEffectiveList && announcementModel.EffectiveListDate.HasValue)
            {
               bool isVacationDayFordailyTime = (announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Saturday" || announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Friday");
               if (isVacationDayFordailyTime)
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.VacationDaysTime);
               }
               else
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.AllDaysTime);
               }
            }
            announcementModel.Attachments = await PreparePostedAttachmentsModel(announcementModel.AttachmentReference);
            await _ApiClient.PostAsync<CreateAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/UpdateAnnouncementSupplierTemplateList", null, announcementModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
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
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
      }




      [HttpGet]
      [Authorize(Policy = PolicyNames.ExtendAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> ExtendAnnouncementSuppliersTemplate(string announcementIdString)
      {
         try
         {
            ExtendAnnouncementSupplierTemplateModel announcementModel = new ExtendAnnouncementSupplierTemplateModel();
            if (string.IsNullOrEmpty(announcementIdString))
            {
               announcementModel.CreatedBy = User.FullName();
               announcementModel.AgencyName = User.UserAgencyName();
               return View(announcementModel);
            }
            var newAnnouncementModel = await _ApiClient.GetAsync<ExtendAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/GetAnnouncementByIdForExtendAnnouncement/" + announcementIdString, null);
            GetEditModeAttahmentsDataForExtend(newAnnouncementModel);
            return View(newAnnouncementModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
      }



      [HttpPost]
      [Authorize(Policy = PolicyNames.ExtendAnnouncementSuppliersTemplatePolicy)]
      public async Task<IActionResult> ExtendAnnouncementSuppliersTemplate(ExtendAnnouncementSupplierTemplateModel announcementModel)
      {
         try
         {
            announcementModel.BranchId = User.UserBranch();
            announcementModel.AgencyCode = User.UserAgency();
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
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               GetEditModeAttahmentsDataForExtend(announcementModel);
               announcementModel.CreatedBy = User.FullName();
               return View(announcementModel);
            }

            if (!string.IsNullOrEmpty(announcementModel.AnnouncementIdString))
            {
               announcementModel.AnnouncementId = Util.Decrypt(announcementModel.AnnouncementIdString);
            }
            if (announcementModel.TenderItemTypes != null && announcementModel.TenderItemTypes.Any())
            {
               foreach (var item in announcementModel.TenderItemTypes)
               {
                  announcementModel.TenderTypesId += item + ",";
               }
               announcementModel.TenderTypesId = announcementModel.TenderTypesId.Substring(0, announcementModel.TenderTypesId.Length - 1);
            }

            if (announcementModel.IsEffectiveList && announcementModel.EffectiveListDate.HasValue)
            {
               bool isVacationDayFordailyTime = (announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Saturday" || announcementModel.EffectiveListDate.Value.DayOfWeek.ToString() == "Friday");
               if (isVacationDayFordailyTime)
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.VacationDaysTime);
               }
               else
               {
                  announcementModel.EffectiveListDate += DateHelper.GetTimePart(_rootConfiguration.WorkingEndTimeConfiguration.AllDaysTime);
               }
            }

            announcementModel.Attachments = await PreparePostedAttachmentsModel(announcementModel.AttachmentReference);
            await _ApiClient.PostAsync<ExtendAnnouncementSupplierTemplateModel>("AnnouncementSupplierTemplate/ExtendAnnouncementTemplate", null, announcementModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
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
            return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
         }
      }

      [HttpPost]
      public ActionResult checkExtendAnnouncementSupplierTemplateDate(ExtendAnnouncementSupplierTemplateModel model)
      {
         try
         {
            if (model.IsEffectiveList && model.EffectiveListDate < DateTime.Now)
            {
               return JsonErrorMessage(Resources.AnnouncementSupplierTemplateResources.ErrorMessages.EffectiveDateLessThanToday);
            }
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return JsonErrorMessage(ex.Message);
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return RedirectToAction(nameof(AllAgencyAnnouncementSupplierTemplates));
      }

      public IActionResult GetAnnouncementTemplateActivityAndAddressViewComponenet(string announcementIdStr)
      {
         return ViewComponent("AnnouncementTemplateActivityAndAddressDetails", new { announcementIdString = announcementIdStr });
      }


      public IActionResult GetAnnouncementDescriptionViewComponenet(string announcementIdStr)
      {
         return ViewComponent("AnnouncementDescription", new { idString = announcementIdStr });
      }


      public IActionResult GetAnnouncementTemplateAttachmentViewComponenet(string announcementIdStr)
      {
         return ViewComponent("AnnouncementDescription", new { idString = announcementIdStr });
      }
      public IActionResult GetAnnouncementTemplateJoinRequestViewComponenet(string announcementIdStr)
      {
         return ViewComponent("AnnouncementJoinRequestViewComponenet", new { idString = announcementIdStr });
      }

      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> SendJoinRequestToAnnouncment(AnnouncementTemplateMainDetailsModel announcementModel)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               return View(announcementModel);
            }

            announcementModel.Attachments = await PreparePostedAttachmentsModel(announcementModel.AttachmentReference);
            AnnouncementTemplateMainDetailsModel createAnnouncementSupplierTemplateModel = await _ApiClient.PostAsync<AnnouncementTemplateMainDetailsModel>("AnnouncementSupplierTemplate/SendJoinRequestToAnnouncment", null, announcementModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);

            return RedirectToAction(nameof(AnnouncementSuppliersTemplateDetailsForSuppliers), new { announcmentIdString = Util.Encrypt(announcementModel.AnnouncementId) });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AnnouncementSuppliersTemplateDetailsForSuppliers), new { announcmentIdString = Util.Encrypt(announcementModel.AnnouncementId) });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AnnouncementSuppliersTemplateDetailsForSuppliers), new { announcmentIdString = Util.Encrypt(announcementModel.AnnouncementId) });
         }
      }

      [HttpPost]
      public async Task<ActionResult> WithdrawFromAnnouncementTemplate(string joinRequestIdString)
      {
         try
         {
            int joinRequestId = Util.Decrypt(joinRequestIdString);
            await _ApiClient.PostAsync("AnnouncementSupplierTemplate/WithdrawFromAnnouncementTemplate/" + joinRequestId, null, null);
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
      public async Task<IActionResult> SaveJoinRequestResult(AnnouncementSuppliersTemplateJoinRequestsDetailsModel joinRequestModel)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               return View(joinRequestModel);
            }

            joinRequestModel = await _ApiClient.PostAsync<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>("AnnouncementSupplierTemplate/SaveJoinRequestResult", null, joinRequestModel);
            AddMessage(Resources.TenderResources.Messages.DataSaved);

            return RedirectToAction(nameof(AnnouncementSuppliersTemplateJoinRequestsDetails), new { AnnouncementId = joinRequestModel.AnnouncementIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AnnouncementSuppliersTemplateJoinRequestsDetails), new { AnnouncementId = joinRequestModel.AnnouncementIdString });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(AnnouncementSuppliersTemplateJoinRequestsDetails), new { AnnouncementId = joinRequestModel.AnnouncementIdString });
         }
      }

      public IActionResult GetTendersForAnnouncementTemplateViewComponenet(string announcementIdStr)
      {
         return ViewComponent("AnnouncementTendersDetail", new { announcementIdString = announcementIdStr });
      }


      // [Authorize]
      public IActionResult GetAnnouncementTemplateListViewComponenet(string announcementIdStr)
      {
         return ViewComponent("AnnouncementListDetail", new { announcementIdString = announcementIdStr });
      }

      public async Task<ActionResult> AnnouncementTendersPagingAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
      {
         try
         {
            criteria.announcementId = Util.Decrypt(criteria.announcementIdString);

            var result = await _ApiClient.GetQueryResultAsync<TenderAnnouncementSuppliersTemplateDetails>("AnnouncementSupplierTemplate/GetTendersByAnnouncementIdAsync", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(Policy = PolicyNames.DeleteAnnouncementTemplatePolicy)]
      public async Task<ActionResult> DeleteAnnouncementTemplate(string announcementIdString)
      {
         try
         {
            await _ApiClient.PostAsync("AnnouncementSupplierTemplate/DeleteAnnouncementTemplate/" + announcementIdString, null, null);
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

      // [Authorize(Policy = PolicyNames.DeleteAnnouncementTemplatePolicy)]
      public async Task<ActionResult> AddListToAgencyAnnouncementLists(string announcementIdString)
      {
         try
         {
            await _ApiClient.PostAsync("AnnouncementSupplierTemplate/AddListToAgencyAnnouncementLists/" + announcementIdString, null, null);
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

      public async Task<ActionResult> RemoveListFromAgencyAnnouncementLists(string announcementIdString)
      {
         try
         {
            await _ApiClient.PostAsync("AnnouncementSupplierTemplate/RemoveListFromAgencyAnnouncementLists/" + announcementIdString, null, null);
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



      public async Task<ActionResult> CancelAnnouncementSuppliersTemplate(string announcementIdString)
      {
         try
         {
            int announcementId = Util.Decrypt(announcementIdString);
            var result = await _ApiClient.GetAsync<AnnouncementSuppliersTemplateCancelModel>("AnnouncementSupplierTemplate/GetAnnouncementSupplierTemplateByIdForCancelation/ " + announcementId, null);
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
      }

      [HttpPost]
      [Authorize(Policy = PolicyNames.UpdateAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> FinalApproveAnnouncemntSupplierTemplate(AnnouncementFinalApprovalModel approvalModel)
      {
         try
         {
            await _ApiClient.PostAsync("AnnouncementSupplierTemplate/FinalApproveAnnouncemntSupplierTemplate/", null, approvalModel);
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
      [Authorize(Policy = PolicyNames.UpdateAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> DeleteSupplierFromAnnouncementTemplate(DeleteSupplierFromAnnouncementModel deleteModel)
      {
         try
         {
            await _ApiClient.PostAsync("AnnouncementSupplierTemplate/DeleteSupplierFromAnnouncementTemplate/", null, deleteModel);
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
      public async Task<ActionResult> GetJoinRequestsSuppliersByAnnouncementId(JoinRequestSuppliersSearchCriteria criteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<JoinRequestModel>("AnnouncementSupplierTemplate/GetJoinRequestsSuppliersByAnnouncementId/", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, criteria.PageSize, result.PageNumber, null));
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

      [Authorize(Policy = PolicyNames.DetailsAnnouncementSuppliersTemplatePolicy)]
      public async Task<ActionResult> AnnouncementBeneficiaryAgencyPagingAsync(AnnouncementSupplierTemplateBasicSearchCriteria criteria)
      {
         try
         {
            criteria.announcementId = Util.Decrypt(criteria.announcementIdString);
            var result = await _ApiClient.GetQueryResultAsync<LinkedAgenciesAnnouncementTemplateModel>("AnnouncementSupplierTemplate/GetBeneficiaryAgencyPagingAsync", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }



      [AllowAnonymous]
      public async Task<ActionResult> AnnouncementSuppliersTemplateJoinRequestsDetailsReport(string announcementIdString)
      {
         try
         {
            var announcementId = Util.Decrypt(announcementIdString);
            var result = await _ApiClient.GetAsync<AnnouncementTemplateDetailsForPrintModel>("AnnouncementSupplierTemplate/AnnouncementSuppliersTemplateJoinRequestsDetailsReport/" + announcementId, null);
            return PartialView("AnnouncementSuppliersTemplateJoinRequestsDetailsReport", result);
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

      [AllowAnonymous]
      public async Task<ActionResult> AnnouncementTemplateJoinRequestsDetailsReportForSupplier(string announcementIdString)
      {
         try
         {
            var announcementId = Util.Decrypt(announcementIdString);
            var result = await _ApiClient.GetAsync<AnnouncementTemplateDetailsForSupplierForPrintModel>("AnnouncementSupplierTemplate/GetAnnouncementTemplateJoinRequestsDetailsReportForSupplier/" + announcementId, null);
            return PartialView("AnnouncementTemplateJoinRequestsDetailsReportForSupplier", result);
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

   }

}
