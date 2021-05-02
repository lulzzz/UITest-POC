using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Helpers;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class QualificationController : BaseController
   {
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly int _pageSize = 10;
      //private static readonly string _attachmentDownloadPath = "/TenderQualification/Download?";
      private IApiClient _ApiClient;
      private readonly ILogger<QualificationController> _logger;
      private IMemoryCache _cache;
      //private readonly IConfigurationRoot _configuration;
      public QualificationController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, ILogger<QualificationController> logger, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
         _logger = logger;
         _cache = cache;
         //_configuration = new ConfigurationBuilder()
         //         .SetBasePath(Directory.GetCurrentDirectory())
         //         .AddJsonFile("appsettings.json")
         //         .Build();
      }

      #region [Supplier Forms]

      #region [Views]
      /// <summary>
      /// User Story [7267,7270] 
      /// Dispaly Qualification Details
      /// </summary>
      /// <returns></returns>
      public async Task<ActionResult> PreQualificationDetails(string QualificationId)
      {
         try
         {
            QualificationStatusModel model = new QualificationStatusModel();
            model.tenderId = Util.Decrypt(QualificationId);
            model = await _ApiClient.GetAsync<QualificationStatusModel>("Qualification/GetPrequalificationDetailsData/" + Util.Decrypt(QualificationId), null);
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
      [HttpGet]
      public async Task<List<LookupModel>> GetAllBranchesByAgencyCode()
      {

         var result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllBranchesByAgencyCode/" + User.UserAgency(), null);
         return result;
      }

      [AllowAnonymous]
      public async Task<ActionResult> OpenQualificationDetailsReport(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<TenderDetialsReportModel>("Qualification/OpenQualificationDetailsReport/" + tenderId, null);
            return PartialView("_OpenQualificationDetailsReport", result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [HttpGet]
      [Authorize(RoleNames.QualificationSecretaryAndManagerPolicy)]
      public async Task<ActionResult> QualificationOffersRegistryReport(string qualificationIdString)
      {
         try
         {
            int qualificationId = Util.Decrypt(qualificationIdString);
            var result = await _ApiClient.GetAsync<RegistryReportForQualificationModel>("Qualification/QualificationOffersRegistryReport/" + qualificationId, null);
            return PartialView("_QualificationOffersRegistryReport", result);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), "Qualification");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Qualification");
         }
      }

      /// <summary>
      /// Go View To Apply Supplier Document
      ///  User Story [7267,7270] 
      /// </summary>
      /// <param name="qualificationId"></param>
      /// <returns></returns>
      [HttpGet("Qualification/PreQualificationVisitorDetails/{qualificationId}")]
      public async Task<IActionResult> PreQualificationVisitorDetails(string qualificationId)
      {
         try
         {
            PreQualificationApplyDocumentsModel result = null;
            //var preQualificationApplyDocumentsModel = new PreQualificationApplyDocumentsModel { SupplierPreQualificationDocumentIdString = qualificationId };
            if (result == null)
            {
               result = new PreQualificationApplyDocumentsModel
               {
                  PreQualificationResult = 0,
                  PreQualificationIdString = qualificationId
               };
            }
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
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);

         }
         return RedirectToAction(nameof(Index));
      }

      /// <summary>
      /// Go View To Apply Supplier Document
      ///  User Story [7267,7270] 
      /// </summary>
      /// <param name="qualificationId"></param>
      /// <returns></returns>
      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet]
      public async Task<IActionResult> PreQualificationSupplierDetails(string qualificationId)
      {
         try
         {
            PreQualificationApplyDocumentsModel result = await _ApiClient.GetAsync<PreQualificationApplyDocumentsModel>("Qualification/GetSupplierDocuments/" + Util.Decrypt(qualificationId), null);
            //var preQualificationApplyDocumentsModel = new PreQualificationApplyDocumentsModel { SupplierPreQualificationDocumentIdString = qualificationId };
            if (result == null)
            {
               result = new PreQualificationApplyDocumentsModel
               {
                  PreQualificationResult = 0,
                  PreQualificationIdString = qualificationId
               };
            }
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
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);

         }
         return RedirectToAction(nameof(Index));
      }

      /// <summary>
      /// Go View To Apply Supplier Document
      ///  User Story [7267,7270] 
      /// </summary>
      /// <param name="qualificationId"></param>
      /// <returns></returns>
      [Authorize(Roles = RoleNames.supplier)]
      [HttpGet]
      public async Task<IActionResult> ApplyPreQualificationDocument(string qualificationId, bool editMode)
      {
         try
         {

            var result = await _ApiClient.GetAsync<PreQualificationApplyDocumentsModel>("Qualification/CheckSupplierDocuments/" + Util.Decrypt(qualificationId) + "/" + editMode, null);
            result.EditMode = editMode;
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
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);

         }
         return RedirectToAction(nameof(Index));
      }

      #endregion

      #region [Data Actions]

      /// User Story[7267, 7270]
      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<IActionResult> ApplyPreQualificationDocument(PreQualificationApplyDocumentsModel model)
      {
         try
         {
            if (model.QualificationStatusId == (int)Enums.QualificationStatus.Draft)
            {
               ModelState.Remove("lstQualificationSupplierTechDataModel[3].QualificationLookupId");
               ModelState.Remove("lstQualificationSupplierTechDataModel[4].QualificationLookupId");
               ModelState.Remove("lstQualificationSupplierTechDataModel[10].QualificationLookupId");
            }
            if (!ModelState.IsValid)
            {
               var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);

               var errors = ModelState.Select(x => x.Value.Errors)
                         .Where(y => y.Count > 0)
                         .ToList();
               throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ModelDataError);
            }
            model.preQualificationSupplierAttachmentModels = await PreparePostedSupplierAttachmentsModel(model.AttachmentRefrences);
            model.InsuranceAttachmentModels = await PrepareQualificationAttachmentModels(model.FileReferenceForInsurance);
            model.QualityAssuranceStandardsAttachmentModels = await PrepareQualificationAttachmentModels(model.FileReferenceForQualityAssuranceStandards);
            model.EnvironmentalHealthSafetyStandardsAttachmentModels = await PrepareQualificationAttachmentModels(model.FileReferenceForEnvironmentalHealthSafetyStandards);
            model.lstQualificationSupplierDataModel = model.lstQualificationSupplierTechDataModel.Concat(model.lstQualificationSupplierFinancialDataModel).ToList();
            var details = await _ApiClient.PostAsync<PreQualificationApplyDocumentsModel>("Qualification/ApplyDocsforSupplier", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction("Index");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(ApplyPreQualificationDocument), new { qualificationId = model.PreQualificationIdString });
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return View(model);
      }

      #endregion

      #region [Private Methods]

      /// <summary>
      ///  User Story [7267,7270]
      /// </summary>
      /// <param name="attachmentReference"></param>
      /// <returns></returns>
      private async Task<List<PreQualificationSupplierAttachmentModel>> PreparePostedSupplierAttachmentsModel(string attachmentReference)
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
         List<PreQualificationSupplierAttachmentModel> tenderAttachments = new List<PreQualificationSupplierAttachmentModel>();
         foreach (var item in attachmentReferences)
         {
            var arr = item.Split(":");
            PreQualificationSupplierAttachmentModel tenderAttachment = new PreQualificationSupplierAttachmentModel() { FileName = arr[1], FileNetReferenceId = arr[0] };
            tenderAttachments.Add(tenderAttachment);
         }
         return tenderAttachments;
      }

      private async Task<List<QualificationAttachmentModel>> PrepareQualificationAttachmentModels(string attachmentReference)
      {
         List<string> attachmentReferences = new List<string>();
         if (!string.IsNullOrEmpty(attachmentReference) && attachmentReference != "0")
         {
            string[] lst = attachmentReference.Split(',');
            foreach (var item in lst)
            {
               if (!string.IsNullOrEmpty(item))
               {
                  if (item.Contains("/GetFile/"))
                     attachmentReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
                  else
                     attachmentReferences.Add(item);
               }
            }
         }
         List<QualificationAttachmentModel> tenderAttachments = new List<QualificationAttachmentModel>();
         foreach (var item in attachmentReferences)
         {
            var arr = item.Split(":");
            QualificationAttachmentModel tenderAttachment = new QualificationAttachmentModel() { FileName = arr[1], FileNetReferenceId = arr[0] };
            tenderAttachments.Add(tenderAttachment);
         }
         return tenderAttachments;
      }

      #endregion

      #endregion

      #region Qualifications Grids

      //User Story 12869 - for supplier (7591 - 7592 - 7593)
      [Authorize(RoleNames.QualificationIndexPolicy)]
      public async Task<ActionResult> Index()
      {
         try
         {
            if (User.IsInRole(RoleNames.supplier))
               return RedirectToAction(nameof(AllSuppliersPreQualifications));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
         return View();
      }

      //User Story 12869
      [Authorize(RoleNames.QualificationIndexPolicy)]
      [HttpGet]
      public async Task<ActionResult> IndexPagingAsync(PreQualificationSearchCriteriaModel preQualificationSearchCriteriaModel)
      {
         QueryResult<PreQualificationCardModel> result;
         try
         {
            result = await _ApiClient.GetQueryResultAsync<PreQualificationCardModel>("Qualification/GetPreQualificationsBySearchCriteria/", preQualificationSearchCriteriaModel.ToDictionary());
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
         return Json(new PaginationModel(result.Items, result.TotalCount, preQualificationSearchCriteriaModel.PageSize, result.PageNumber, null));
      }

      [HttpGet]
      public async Task<ActionResult> IndexPagingForSupplierProject(PreQualificationSearchCriteriaModel preQualificationSearchCriteriaModel)
      {
         QueryResult<QualificationSupplierProjectModel> result;
         try
         {
            result = await _ApiClient.GetQueryResultAsync<QualificationSupplierProjectModel>("Qualification/GetSupplierProjectbySearchCriteria/", preQualificationSearchCriteriaModel.ToDictionary());
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }
         return Json(new PaginationModel(result.Items, result.TotalCount, preQualificationSearchCriteriaModel.PageSize, result.PageNumber, null));
      }

      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.CustomerService + "," + RoleNames.supplier + "," + RoleNames.TechnicalCommitteeUser
         + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.FinancialSupervisor)]
      [HttpGet]
      public async Task<ActionResult> GetFinancialYear(PreQualificationSearchCriteriaModel qualificationSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetListAsync<string>("Tender/GetFinancialYear", null);
            return Json(result);
         }
         catch (Exception ex)
         {
            return NotFound();
         }

      }

      //User Story 44 - 45 - 46
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager)]
      public async Task<ActionResult> QualificationStageIndex(string Type)
      {
         ViewBag.Title = GetTitle(Type);
         if (!String.IsNullOrEmpty(Type))
         {
            ViewBag.Type = Type;
            TempData["ListType"] = Type;
         }
         return View();
      }

      //User Story 12869
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer)]
      public async Task<ActionResult> PreQualificationIndexUnderOperationsStage(string Type)
      {
         return View();
      }

      //User Story 12869
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> PreQualificationIndexCheckingStage(string Type)
      {
         return View();
      }

      //User Story 12869
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
      [HttpGet]
      public async Task<ActionResult> GetPreQualificationsForCheckingStageIndexAsync(PreQualificationSearchCriteriaModel preQualificationSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<PreQualificationCardModel>("Qualification/GetPreQualificationForCheckingStageIndex", preQualificationSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, preQualificationSearchCriteriaModel.PageSize, result.PageNumber, null));
         }

         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      //User Story 12869
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer)]
      [HttpGet]
      public async Task<ActionResult> GetPreQualificationForUnderOperationsStageIndexAsync(PreQualificationSearchCriteriaModel preQualificationSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<PreQualificationCardModel>("Qualification/GetPreQualificationForUnderOperationsStageIndex", preQualificationSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, preQualificationSearchCriteriaModel.PageSize, result.PageNumber, null));
         }

         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      private string GetTitle(string Type)
      {
         string title = "";
         if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer) && Type == Enum.GetName(typeof(StepNames), StepNames.DataEntry))//UnderStablishment
         {
            title = Resources.TenderResources.DisplayInputs.UnderstablishedStageTenders;
         }
         else if ((User.IsInRole(RoleNames.OffersCheckManager) || User.IsInRole(RoleNames.OffersCheckSecretary)) && Type == Enum.GetName(typeof(StepNames), StepNames.Check))
         {
            title = Resources.TenderResources.DisplayInputs.CheckStageTenders;
         }
         return title;
      }

      #endregion Qualifications Grids

      #region Supplier Grids

      //[AllowAnonymous]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> AllSuppliersPreQualifications()
      {
         ViewBag.SupplierSubscriptionType = (int)Enums.SubscriptionType.Full;
         return View();
      }

      public async Task<ActionResult> QualificationsForVisitor()
      {
         ViewBag.SupplierSubscriptionType = (int)Enums.SubscriptionType.Full;
         return View();
      }

      // [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> AllPreQualificationsAsync(PreQualificationSearchCriteriaModel preQualificationSearch)
      {
         try
         {
            preQualificationSearch.FromLastOfferPresentationDate = DateHelper.GetDate(preQualificationSearch.FromLastOfferPresentationDateString);
            preQualificationSearch.ToLastOfferPresentationDate = DateHelper.GetDate(preQualificationSearch.ToLastOfferPresentationDateString);
            var result = await _ApiClient.GetQueryResultAsync<PreQualificationCardModel>("Qualification/GetQualificationsListForVisitor/", preQualificationSearch.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, preQualificationSearch.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      //[ResponseCache(Duration = 60, VaryByHeader = "User-Agent")]
      public async Task<ActionResult> AllSupplierPreQualificationsAsync(PreQualificationSearchCriteriaModel preQualificationSearch)
      {
         try
         {
            preQualificationSearch.FromLastOfferPresentationDate = DateHelper.GetDate(preQualificationSearch.FromLastOfferPresentationDateString);
            preQualificationSearch.ToLastOfferPresentationDate = DateHelper.GetDate(preQualificationSearch.ToLastOfferPresentationDateString);
            var result = await _ApiClient.GetQueryResultAsync<PreQualificationCardModel>("Qualification/GetSupplierQualificationsList/", preQualificationSearch.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, preQualificationSearch.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> GetSupplierFavouritPreQualificationsListAsync(PreQualificationSearchCriteriaModel tenderSearchCriteria)
      {
         try
         {
            //var result = await _ApiClient.GetAsync<PreQualificationBasicDetailsModel>("Qualification/GetPreQualificationDetailsForChecking/ " + tenderSearchCriteria.TenderId, null);
            //return View(result);
            var result = await _ApiClient.GetQueryResultAsync<PreQualificationCardModel>("Qualification/GetSupplierFavouritQualificationsList", tenderSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteria.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      #endregion Supplier Grids

      #region Check PreQualification Actions
      // Get Supplier PreQualifications Requests For Checking Stage
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> CheckPreQualification(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<PreQualificationBasicDetailsModel>("Qualification/GetPreQualificationDetailsForChecking/ " + tenderId, null);
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      // Get Supplier PreQualifications Requests For Checking Stage
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> GetPreQualificationsRequestsForCheckingAsync(QualificationIdWithSearchCriteria qualificationSearch)
      {
         try
         {
            //int tenderId = Util.Decrypt(tenderIdString);

            var result = await _ApiClient.GetQueryResultAsync<PreQualificationResultForChecking>("Qualification/GetPreQualificationsRequestsForChecking", qualificationSearch.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> PreQualificationChecking(string PreQualificationIdString, string SupplierId)
      {
         var preQualificationChecking = await _ApiClient.GetAsync<SupplierPreQualificationDocumentModel>("Qualification/GetsupplierPreQualificationDocumentById/" + Util.Decrypt(PreQualificationIdString) + "/" + SupplierId, null);

         return View(preQualificationChecking);
      }

      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.DataEntry)]
      public async Task<ActionResult> PrequalificationTechnicalExamination(string PreQualificationIdString)
      {
         var model = await _ApiClient.GetAsync<PrequalificationTechnicalExaminationModel>("Qualification/GetPrequalificationTechnicalExamination/" + Util.Decrypt(PreQualificationIdString), null);
         return View(model);
      }

      /// <summary>
      /// Update supplier Pre Qualification State (Matched - not matched)
      ///  User Story [7376] 
      /// </summary>
      /// <param name="qualificationId"></param>
      /// <returns></returns>
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
      [HttpPost]
      public async Task<ActionResult> PreQualificationChecking(SupplierPreQualificationDocumentModel SupplierPreQualificationDocumentModel)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.OfferResources.ErrorMessages.ModelDataError);
               return View(SupplierPreQualificationDocumentModel);
            }
            var offer = await _ApiClient.PostAsync<SupplierPreQualificationDocumentModel>("Qualification/PreQualificationCheckingStatus/", null, SupplierPreQualificationDocumentModel);
            var tenderId = Util.Encrypt(SupplierPreQualificationDocumentModel.PreQualificationId);
            return RedirectToAction("CheckPreQualification", "Qualification", new { tenderIdString = tenderId });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("PreQualificationChecking", "Qualification", new { SupplierPreQualificationDocumentIdString = Util.Encrypt(SupplierPreQualificationDocumentModel.SupplierPreQualificationDocumentId) });
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      [HttpPost]
      public async Task<ActionResult> ChooseQualificationResult(ChooseQualificationResultModel chooseQualificationResultModel)
      {
         try
         {
            await _ApiClient.PostAsync("Qualification/ChooseQualificationResult", null, chooseQualificationResultModel);
            return Ok();
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
            return JsonErrorMessage(ex.Message);
         }
      }

      #endregion

      #region Lookups

      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersPurchaseSecretary
         + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager
         + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.CustomerService + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.supplier + "," + RoleNames.EtimadOfficer
         + "," + RoleNames.PurshaseSpecialist + "," + RoleNames.FinancialSupervisor)]
      [HttpGet]
      public async Task<List<LookupModel>> GetStatusAsync()
      {
         //var statusList = await TenderProxy.GetStatusAsync();
         var statusList = await _ApiClient.GetListAsync<LookupModel>("Qualification/GetStatus", null);
         return statusList;
      }

      [HttpGet]
      public async Task<List<TenderTypeModel>> GetTenderTypesModified()
      {
         if (User.UserIsVRO())
         {
            return new List<TenderTypeModel>();
         }
         var list = new List<TenderTypeModel>();
         var areaList = await _ApiClient.GetListAsync<TenderTypeModel>("Lookup/GetTenderTypes", null);
         areaList.RemoveAll(w => w.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || w.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || w.TenderTypeId == (int)Enums.TenderType.NewTender || w.TenderTypeId == (int)Enums.TenderType.CurrentTender);
         list.Add(new TenderTypeModel { TenderTypeId = (int)Enums.TenderType.GeneralTernderTwoTypes, TenderTypeName = Resources.TenderResources.DisplayInputs.GeneralTender });
         list.Add(new TenderTypeModel { TenderTypeId = (int)Enums.TenderType.DirectPurchaseTowTypes, TenderTypeName = Resources.TenderResources.DisplayInputs.DirectPurshase });
         list.AddRange(areaList);
         return list;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAreasAsync()
      {
         //var areaList = await LookupProxy.GetAreasAsync();
         //var areaList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
         List<LookupModel> areaList = await _cache.GetOrCreateAsync(CacheKeys.GetAreas, async entry =>
        {
           int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes/*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
           entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
           return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
        });
         return areaList;

      }

      [HttpGet]
      public async Task<List<TenderTypeModel>> GetTenderTypes()
      {
         if (User.UserIsVRO())
         {
            return new List<TenderTypeModel>();
         }
         var areaList = await _ApiClient.GetListAsync<TenderTypeModel>("Lookup/GetTenderTypes", null);
         return areaList;
      }

      [HttpGet]
      public async Task<List<ActivityModel>> GetMainTenderActivitiesAsync()
      {
         var activityList = await _ApiClient.GetListAsync<ActivityModel>("Lookup/GetMainActivities", null);
         return activityList;
      }

      [HttpGet]
      public async Task<List<UserLookupModel>> GetAllDataEntryUsersAsync()
      {
         List<int> roles = new List<int>();
         roles.Add((int)UserRole.NewMonafasat_DataEntry);
         roles.Add((int)UserRole.NewMonafasat_OffersCheckSecretary);
         roles.Add((int)UserRole.NewMonafasat_SecretaryDirtectPurshasingCommittee);

         var dataEntriesList = await _ApiClient.GetListAsync<UserLookupModel>("Lookup/GetUserBasedOnlistOfRoleIds", new Dictionary<string, object> { { "lstOfRoles", roles } });
         //var dataEntriesList = await _ApiClient.GetListAsync<UserLookupModel>("Lookup/GetAllDataEntryUsers", null);
         return dataEntriesList;
      }

      [HttpGet]
      public async Task<List<UserLookupModel>> GetAllAuditorUsersAsync()
      {
         List<int> roles = new List<int>();
         roles.Add((int)UserRole.NewMonafasat_Auditer);
         roles.Add((int)UserRole.NewMonafasat_OffersCheckManager);
         roles.Add((int)UserRole.NewMonafasat_ManagerDirtectPurshasingCommittee);

         var auditorsList = await _ApiClient.GetListAsync<UserLookupModel>("Lookup/GetUserBasedOnlistOfRoleIds", new Dictionary<string, object> { { "lstOfRoles", roles } });
         return auditorsList;
      }

      [HttpGet]
      public async Task<List<CountryModel>> GetCountriesync()
      {
         List<CountryModel> countryList = await _cache.GetOrCreateAsync(CacheKeys.GetCountries, async entry =>
        {
           int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
           entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
           return await _ApiClient.GetListAsync<CountryModel>("Lookup/GetCountries", null);
        });
         return countryList;
      }

      [AllowAnonymous]
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

      public async Task<List<GovAgencyModel>> GetAllAgenciesAsync()
      {
         try
         {
            var result = await _ApiClient.GetListAsync<GovAgencyModel>("Lookup/GetALlAgencies/", null);

            return result;
         }
         catch (Exception)
         {
            return null;
         }
      }

      #endregion

      #region Create-Pre-Qualification

      [HttpGet]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> SavePreQualification(string id)
      {
         try
         {
            PreQualificationSavingModel response = new PreQualificationSavingModel();
            if (!string.IsNullOrEmpty(id))
            {
               response = await _ApiClient.GetAsync<PreQualificationSavingModel>("Qualification/GetPreQualificationEditingData/" + Util.Decrypt(id), null);
               response.TenderIdString = id;
               await GetEditModeAttahmentsData(response);
            }
            response.ActivityVersionId = (int)Enums.ActivityVersions.OldActivities;
            await FillQualificationDropDownsModel(response);
            return View(response);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> SaveDraft(PreQualificationSavingModel model)
      {
         try
         {
            model.Attachments = await PreparePostedAttachmentsModel(model.AttachmentReference);
            var obj = await _ApiClient.PostAsync<PreQualificationSavingModel>("Qualification/SaveDraft", null, model);
            return Json(obj);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      [HttpPost]
      public async Task<ActionResult> checkQualificationDate(PreQualificationSavingModel model)
      {
         try
         {
            if (model.LastEnqueriesDate < DateTime.Now || model.LastOfferPresentationDate < DateTime.Now || model.OffersCheckingDate < DateTime.Now)
            {
               return JsonErrorMessage(Resources.SharedResources.ErrorMessages.DateLessThanToday);
            }

            if (model.LastOfferPresentationDate >= model.OffersCheckingDate)
            {
               return JsonErrorMessage(Resources.QualificationResources.ErrorMessages.InvalidOffersCheckingDate);
            }

            if (model.LastOfferPresentationDate <= model.LastEnqueriesDate)
            {
               return JsonErrorMessage(Resources.QualificationResources.ErrorMessages.InvalidLastEnqueriesDate);
            }


            if (model.LastOfferPresentationDate != null)
            {
               model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
               int startOfferTime = _rootConfiguration.OfferTimesConfiguration.StartOfferTime;
               int endOfferTime = _rootConfiguration.OfferTimesConfiguration.EndOfferTime;
               int startOfferVacationTime = _rootConfiguration.OfferTimesConfiguration.StartOfferVacationTime;
               int endOfferVacationTime = _rootConfiguration.OfferTimesConfiguration.EndOfferVacationTime;
               Util.DetermineTimesForDates(model.LastOfferPresentationDate.Value, (int)Enums.TimeMessagesType.Qualification, startOfferTime, endOfferTime, startOfferVacationTime, endOfferVacationTime);
            }

         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return RedirectToAction(nameof(Index), "Qualification");
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> SavePreQualification(PreQualificationSavingModel model)
      {
         if (User.UserRole() == RoleNames.PreQualificationCommitteeSecretary)
         {
            ModelState.Remove("TenderName");
            ModelState.Remove("TechnicalOrganizationId");
         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            await GetEditModeAttahmentsData(model);
            await FillQualificationDropDownsModel(model);
            return View(model);
         }
         try
         {
            if (!string.IsNullOrEmpty(model.TenderIdString))
            {
               model.TenderId = Util.Decrypt(model.TenderIdString);
            }
            model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
            model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
            model.TenderId = Util.Decrypt(model.TenderIdString);
            model.Attachments = await PreparePostedAttachmentsModel(model.AttachmentReference);
            PreQualificationSavingModel tenderBasicDataModel = await _ApiClient.PostAsync<PreQualificationSavingModel>("Qualification/CreatePreQualifcation", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(PreQualificationApproval), new { qualificationIdString = Util.Encrypt(tenderBasicDataModel.TenderId) });
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
         model.AttachmentReference = "";
         await GetEditModeAttahmentsData(model);

         await FillQualificationDropDownsModel(model);
         return View(model);
      }

      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary)]
      public async Task<ActionResult> Delete(string TenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(TenderIdString);
            await _ApiClient.GetAsync<BasicTenderModel>("Tender/Delete/" + tenderId, null);
            return RedirectToAction(nameof(Index));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));

         }
         // return Ok();
      }

      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> DeletePostQualification(string TenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(TenderIdString);
            await _ApiClient.GetAsync<BasicTenderModel>("Tender/DeletePostQualification/" + tenderId, null);
            return RedirectToAction(nameof(Index));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));

         }
         // return Ok();
      }

      private async Task FillQualificationDropDownsModel(PreQualificationSavingModel model)
      {
         RelationsStepModels lookUps = await _cache.GetOrCreateAsync(CacheKeys.RelationsStepCache + model.ActivityVersionId, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            return await _ApiClient.GetAsync<RelationsStepModels>("Lookup/GetRelationsStepLookups/" + model.ActivityVersionId, null);
         });
         List<VacationsDateModel> allVacation = await _cache.GetOrCreateAsync(CacheKeys.VacationCache, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
         });
         model.Vacations = allVacation;
         model.Areas = lookUps.Areas;
         model.Countries = lookUps.Countries;
         model.MaintenanceWorks = lookUps.RunningWorks;
         model.ActivitiesWithYears = lookUps.ActivitiesWithYears;


         List<CommitteeModel> allCommittees = (await _cache.GetOrCreateAsync(CacheKeys.BasicStepCache + "_" + User.UserAgency(), async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetCommitteeByCommitteeTypeOnBranch", null);
         })).ToList();

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            model.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            model.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }

         model.IsAgancyHasTechnicalCommittee = model.TechnicalCommittees.Count > 0 ? true : false;

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PreQualificationCommittee))
         {
            model.PreQualificationCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PreQualificationCommittee).ToList();
         }
         else
         {
            model.PreQualificationCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.PreQualificationCommittee).ToList();
         }

         List<SelectListItem> worksItems = _cache.GetOrCreate(CacheKeys.WorkItemsCache, entry =>
         {
            var works = new List<SelectListItem>();
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            foreach (var item in lookUps.ConstructionWorks)
            {
               var group = new SelectListGroup { Name = item.Name };
               foreach (var sub in item.SubWorks)
                  works.Add(new SelectListItem { Value = sub.ConstructionWorkId.ToString(), Text = sub.Name, Group = group });
            }
            return works;
         });

         //List<SelectListItem> activitiesItems = _cache.GetOrCreate(CacheKeys.ActivitiesCache, entry =>
         //{
         //   var activities = new List<SelectListItem>();
         //   int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
         //   entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
         //   foreach (var item in lookUps.Activities)
         //   {
         //      var group = new SelectListGroup { Name = item.Name };
         //      foreach (var sub in item.SubActivities)
         //      {
         //         activities.Add(new SelectListItem { Value = sub.ActivityId.ToString(), Text = sub.Name, Group = group });
         //      }
         //   }
         //   return activities;
         //});
         List<SelectListItem> activitiesItems = _cache.GetOrCreate(CacheKeys.ActivitiesCache + model.ActivityVersionId, entry =>
         {
            var activities = new List<SelectListItem>();
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            foreach (var item in lookUps.Activities)
            {
               var group = new SelectListGroup { Name = item.Name };
               foreach (var sub in item.SubActivities)
               {
                  activities.Add(new SelectListItem { Value = sub.ActivityId.ToString(), Text = sub.Name, Group = group });
               }
            }
            return activities;
         });
         ViewBag.ListOfActivities = activitiesItems;
         ViewBag.ListOfConstructioWorks = worksItems;
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

      private async Task GetEditModeAttahmentsData(PreQualificationSavingModel model)
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
            //if (list.Count() == 0)
            //   model.TenderStatusId = (int)Enums.TenderStatus.Established;
         }
      }

      #endregion

      #region PreQualification-Apply-Docus

      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> AddQualificationToSupplierFavouriteListAsync(string TenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(TenderIdString);
            var result = await _ApiClient.GetAsync<bool>("Qualification/AddQualificationToSupplierFavouriteList/" + tenderId, null);
            return Json(true);
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

      #endregion

      #region Qualification-Approval-Actions

      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> PreQualificationApproval(string qualificationIdString)
      {
         try
         {
            var preQualificationId = Util.Decrypt(qualificationIdString);
            var result = await _ApiClient.GetAsync<PreQulaificationApprovalModel>("Qualification/GetPreQualificationForApproval/ " + preQualificationId, null);
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

      [HttpPost]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> SendPreQualificationToApproveAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/SendPreQualificationToApprove/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> SendQualificationToCommitteeApproveAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/SendQualificationToCommitteeApprove/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> ApproveQualificationFromQualificationSecritaryAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ApproveQualificationFromQualificationSecritary/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> ApprvePreQualificationAsync(string tenderIdString, string verficationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ApprvePreQualification/" + tenderId + "/" + verficationCode, null, null);
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
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> ApprovePreQualificationFromCommitteeManagerAsync(string tenderIdString, string verficationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ApprovePreQualificationFromCommitteeManager/" + tenderId + "/" + verficationCode, null, null);
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
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> RejectApprvePreQualificationAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Qualification/RejectApprvePreQualification", null, rejectionModel);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> RejectApprovePreQualificationFromCommitteeManagerAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Qualification/RejectApprovePreQualificationFromCommitteeManager", null, rejectionModel);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary)]
      public async Task<ActionResult> ReopenPreQualificationAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("ddd");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ReopenPreQualification/" + tenderId, null, null);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> ReopenPreQualificationFromCommitteeSecrtaryAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("ddd");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ReopenPreQualificationFromCommitteeSecrtary/" + tenderId, null, null);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #endregion

      #region PreQualification-Checking

      //User Story Number: 7378//
      [HttpPost]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersPurchaseManager + "" + RoleNames.OffersCheckManager)]
      public async Task<ActionResult> StartPreQualificationCheckingOffersAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/StartPreQualificationCheckingOffersAsync" + tenderId, null, null);
            return Ok();
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      //User Story Number: 7378//
      [HttpPost]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> SendPreQualificationToApproveCheckingAsync(string tenderIdString)
      {
         try
         {
            string role = User.UserRole();
            int id = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/SendPreQualificationToApproveChecking/" + id, null, null);
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

      //User Story Number: 7383//
      [HttpPost]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> ApprovePreQualificationCheckingWithVerificationAsync(string tenderIdString, string verficationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ApprovePreQualificationCheckingWithVerification/" + tenderId + "/" + verficationCode, null, null);
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

      //User Story Number: 7383//
      [HttpGet]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> RejectCheckPreQualificationOffersReportAsync(RejectionModel model)
      {
         try
         {
            model.TenderId = Util.Decrypt(model.TenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Qualification/RejectCheckPreQualificationOffersReportAsync", null, model);
            return Ok();
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      //User Story Number: 7378//
      [HttpPost]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> ReopenPreQualificationCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Qualification" + "/ReopenPreQualificationChecking/" + tenderId, null, null);
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

      #region Creat-Post-Qualification

      [HttpGet]
      //[Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> CreatePostQualification(string PostQualificationTenderIdString, string postQualificationIdString, List<string> commercialNumbers)
      {
         try
         {
            PostQualificationApplyDocumentsModel response = new PostQualificationApplyDocumentsModel();
            string link = "";
            if (commercialNumbers.Count() == 0)
            {
               link = "Qualification/GetQualificationDataToEditPostQualification/" + postQualificationIdString;
            }
            else
            {
               string strcrs = string.Join(",", commercialNumbers.ToArray());
               link = "Qualification/getQualificationDataToCreatePostQualification/" + strcrs + "/" + PostQualificationTenderIdString + "/" + (string.IsNullOrEmpty(postQualificationIdString) ? "0" : postQualificationIdString);
            }
            response = await _ApiClient.GetAsync<PostQualificationApplyDocumentsModel>(link, null);
          //  response.ActivityVersionId = await _ApiClient.GetAsync<int>("Qualification/GetCurrentActivityVersion/" + Util.Decrypt(PostQualificationTenderIdString), null);

            if (commercialNumbers.Any() && !response.CommercialNumbers.Any())
            {
               response.CommercialNumbers = commercialNumbers;
               response.LastEnqueriesDate = null;
               response.LastOfferPresentationDate = null;
               response.OffersCheckingDate = null;
            }
            else
            {
               response.postQualificationIdString = postQualificationIdString;
            }
            await GetEditModeAttahmentsData(response);
            response.CreatedBy = string.IsNullOrEmpty(PostQualificationTenderIdString) ? User.UserName() : response.CreatedBy;
            response.ActivityVersionId = (int)Enums.ActivityVersions.OldActivities;
            await FillQualificationDropDownsModel(response);
            return View(response);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("TenderAwarding", "Tender", new { tenderIdString = PostQualificationTenderIdString });

         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("TenderAwarding", "Tender", new { tenderIdString = PostQualificationTenderIdString });
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("TenderAwarding", "Tender", new { tenderIdString = PostQualificationTenderIdString });

         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> CreatePostQualification(PostQualificationApplyDocumentsModel model)
      {
         if (!ModelState.IsValid)
         {
            var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            await FillQualificationDropDownsModel(model);
            return View(model);
         }
         try
         {
            if (!string.IsNullOrEmpty(model.TenderIdString))
            {
               model.TenderId = Util.Decrypt(model.TenderIdString);
            }
            if (model.LastOfferPresentationDate != null)
            {
               model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
            }
            if (model.OffersCheckingDate != null)
            {
               model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
            }
            model.TenderId = Util.Decrypt(model.TenderIdString);
            model.PostQualificationTenderId = Util.Decrypt(model.PostQualificationTenderIdString);
            model.Attachments = await PreparePostedAttachmentsModel(model.AttachmentReference);
            PreQualificationSavingModel tenderBasicDataModel = await _ApiClient.PostAsync<PreQualificationSavingModel>("Qualification/CreatePostQualification", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            if (tenderBasicDataModel.TenderStatusId == (int)TenderStatus.UnderEstablishing || tenderBasicDataModel.TenderStatusId == (int)TenderStatus.Established || tenderBasicDataModel.TenderStatusId == (int)TenderStatus.QualificationUnderEstablishingFromCommittee)
            {
               return RedirectToAction(nameof(PreQualificationApproval), new { qualificationIdString = Util.Encrypt(tenderBasicDataModel.TenderId) });
            }
            else
            {
               await FillQualificationDropDownsModel(model);
               return View(model);
            }
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
         model.AttachmentReference = "";
         await GetEditModeAttahmentsData(model);

         await FillQualificationDropDownsModel(model);
         return View(model);
      }

      #endregion

      #region Apply-Post-Qualification-Docuoments

      //public async Task<IActionResult> ApplyPostQualificationDocument(string qualificationId, bool editMode)
      //{
      //   try
      //   {
      //      PreQualificationApplyDocumentsModel result = await _ApiClient.GetAsync<PreQualificationApplyDocumentsModel>("Qualification/GetPostQualificationSuppierDocument/" + Util.Decrypt(qualificationId), null);
      //      result.EditMode = editMode;
      //      //if (result == null)
      //      //{
      //      //   result = new PreQualificationApplyDocumentsModel
      //      //   {
      //      //      PreQualificationResult = 0,
      //      //      PreQualificationIdString = qualificationId
      //      //   };
      //      //}
      //      return View(result);
      //   }

      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //   }

      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (Exception ex)
      //   {
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);

      //   }
      //   return RedirectToAction(nameof(Index));
      //}
      //[Authorize(RoleNames.SupplierPolicy)]
      //[HttpPost]
      //public async Task<IActionResult> ApplyPostQualificationDocument(PreQualificationApplyDocumentsModel model)
      //{
      //   try
      //   {
      //      if (model.QualificationTypeId == (int)Enums.PreQualificationType.Small)
      //      {
      //         ModelState.Remove("FileReferenceForInsurance");
      //         ModelState.Remove("FileReferenceForQualityAssuranceStandards");
      //         ModelState.Remove("FileReferenceForEnvironmentalHealthSafetyStandards");
      //      }

      //      if (!ModelState.IsValid)
      //      {
      //         var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);

      //         var errors = ModelState.Select(x => x.Value.Errors)
      //                   .Where(y => y.Count > 0)
      //                   .ToList();
      //         throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ModelDataError);
      //      }

      //      model.preQualificationSupplierAttachmentModels = await PreparePostedSupplierAttachmentsModel(model.AttachmentRefrences);
      //      model.InsuranceAttachmentModels = await PrepareQualificationAttachmentModels(model.FileReferenceForInsurance);
      //      model.QualityAssuranceStandardsAttachmentModels = await PrepareQualificationAttachmentModels(model.FileReferenceForQualityAssuranceStandards);
      //      model.EnvironmentalHealthSafetyStandardsAttachmentModels = await PrepareQualificationAttachmentModels(model.FileReferenceForEnvironmentalHealthSafetyStandards);
      //      model.lstQualificationSupplierDataModel = model.lstQualificationSupplierTechDataModel.Concat(model.lstQualificationSupplierFinancialDataModel).ToList();

      //      var details = await _ApiClient.PostAsync<PreQualificationApplyDocumentsModel>("Qualification/ApplyPostQualificationDocsforSupplier", null, model);
      //      AddMessage(Resources.TenderResources.Messages.DataSaved);
      //      return RedirectToAction(nameof(Index));
      //   }
      //   catch (AuthorizationException ex)
      //   {

      //      throw ex;
      //   }
      //   catch (UnHandledAccessException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //   }
      //   catch (Exception ex)
      //   {
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //   return View(model);
      //}

      #endregion

      #region Check-Post-Qualification

      public async Task<ActionResult> CheckPostQualification(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<PreQualificationBasicDetailsModel>("Qualification/GetPostQualificationDetailsByIdForChecking/ " + tenderId, null);
            return View(result);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> PostQualificationChecking(string SupplierPreQualificationDocumentIdString, string SupplierId)
      {
         var preQualificationChecking = await _ApiClient.GetAsync<SupplierPreQualificationDocumentModel>("Qualification/GetsupplierPreQualificationDocumentById/" + Util.Decrypt(SupplierPreQualificationDocumentIdString) + "/" + SupplierId, null);
         return View(preQualificationChecking);
      }

      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      [HttpPost]
      public async Task<ActionResult> PostQualificationChecking(SupplierPreQualificationDocumentModel SupplierPreQualificationDocumentModel)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.OfferResources.ErrorMessages.ModelDataError);
               return View(SupplierPreQualificationDocumentModel);
            }
            var offer = await _ApiClient.PostAsync<SupplierPreQualificationDocumentModel>("Qualification/PostQualificationChecking/", null, SupplierPreQualificationDocumentModel);
            var tenderId = Util.Encrypt(SupplierPreQualificationDocumentModel.PreQualificationId);
            return RedirectToAction("CheckPostQualification", "Qualification", new { tenderIdString = tenderId });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(SupplierPreQualificationDocumentModel);
            //AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return View(SupplierPreQualificationDocumentModel);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SendPostQualificationToApproveCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/SendPostQualificationToApproveChecking/" + tenderId, null, null);
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

      //User Story Number: 7383//
      [HttpPost]
      [Authorize(RoleNames.OffersCheckManagerPolicy)]
      public async Task<ActionResult> ApprovePostQualificationCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ApprovePostQualificationChecking/" + tenderId, null, null);
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

      //User Story Number: 7383//
      [HttpPost]
      [Authorize(RoleNames.OffersCheckManagerPolicy)]
      public async Task<ActionResult> RejectCheckPostQualificationOffersReportAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Qualification/RejectCheckPostQualificationOffersReportAsync", null, rejectionModel);
            return Ok();
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }
      }

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> ReopenPostQualificationCheckingAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("ddd");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ReopenPostQualificationChecking/" + tenderId, null, null);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #endregion

      #region Create Verification Code

      //User Story Number: 7383//
      [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
      [HttpPost]
      public async Task<ActionResult> CreateVerificationCode(string tenderIdString)
      {
         try
         {

            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.PostAsync("Qualification/CreateVerificationCode/" + tenderId, null, null);

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

      #region Edit Committees

      [HttpGet]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> EditCommitteesAsync(string id)
      {
         try
         {

            EditeCommitteesModel committeesModel = await _ApiClient.GetAsync<EditeCommitteesModel>("Tender/GetTenderCommitteesByTenderId/" + Util.Decrypt(id).ToString(), null);
            await FillTenderCommitteesAsync(committeesModel);
            committeesModel.TenderIdString = id;
            return View(committeesModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(PreQualificationIndexUnderOperationsStage));
         }
      }

      private async Task<EditeCommitteesModel> FillTenderCommitteesAsync(EditeCommitteesModel committeesModel)
      {
         List<CommitteeModel> allCommittees = (await _cache.GetOrCreateAsync(CacheKeys.BasicStepCache + "_" + User.UserAgency(), async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetCommitteeByCommitteeTypeOnBranch", null);
         })).ToList();


         if (allCommittees.Any(c => c.BranchIds.Contains(committeesModel.BranchId) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(committeesModel.BranchId) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(committeesModel.BranchId) && c.CommitteeTypeId == (int)CommitteeType.PreQualificationCommittee))
         {
            committeesModel.PreQualificationCommittees = allCommittees.Where(c => c.BranchIds.Contains(committeesModel.BranchId) && c.CommitteeTypeId == (int)CommitteeType.PreQualificationCommittee).ToList();
         }
         else
         {
            committeesModel.PreQualificationCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.PreQualificationCommittee).ToList();
         }

         return committeesModel;

      }

      public IActionResult GetAttachmentsChangesViewComponenet(string tenderIdStr)
      {
         return ViewComponent("QualificationAttachments", new { tenderId = Util.Decrypt(tenderIdStr) });
      }

      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> QualificationAttachmentsChangesApprovement(string tenderIdString)
      {
         return View("QualificationAttachmentsChangesApprovement", tenderIdString);
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> EditCommitteesAsync(EditeCommitteesModel model)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            EditeCommitteesModel committeesModel = await _ApiClient.GetAsync<EditeCommitteesModel>("Tender/GetTenderCommitteesByTenderId/" + Util.Decrypt(model.TenderIdString), null);
            committeesModel = await FillTenderCommitteesAsync(committeesModel);
            return View(committeesModel);
         }
         try
         {
            model.TenderId = Util.Decrypt(model.TenderIdString);
            EditeCommitteesModel tenderCommitteeModel = await _ApiClient.PostAsync<EditeCommitteesModel>("Qualification/EditPreQualificationCommittes", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(EditCommitteesAsync), new { id = model.TenderIdString });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      #endregion

      #region Edit Areas

      [HttpGet]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> EditAreasAsync(string id)
      {
         try
         {
            //TODO: check weather the quilifcation qrea is inside or outside sa
            TenderAreasModel areasModel = await _ApiClient.GetAsync<TenderAreasModel>("Tender/GetTenderToEditAreas/" + Util.Decrypt(id).ToString(), null);
            List<LookupModel> areas = await _cache.GetOrCreateAsync(CacheKeys.GetAreas, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
            });
            List<CountryModel> countries = await _cache.GetOrCreateAsync(CacheKeys.GetCountries, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<CountryModel>("Lookup/GetCountries", null);
            });

            areasModel.Areas = areas;
            areasModel.Countries = countries;
            areasModel.TenderIdString = id;
            return View(areasModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> EditAreasAsync(TenderAreasModel areasModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            areasModel = await _ApiClient.GetAsync<TenderAreasModel>("Tender/GetTenderToEditAreas/" + Util.Decrypt(areasModel.TenderIdString).ToString(), null);
            //List<LookupModel> areas = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
            List<LookupModel> areas = await _cache.GetOrCreateAsync(CacheKeys.GetAreas, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
            });
            areasModel.Areas = areas;
            return View(areasModel);
         }
         try
         {
            areasModel.TenderId = Util.Decrypt(areasModel.TenderIdString);
            TenderAreasModel tenderAreasModel = (areasModel.InsideKSA.Value ? await _ApiClient.PostAsync<TenderAreasModel>("Tender/EditAreas", null, areasModel) : await _ApiClient.PostAsync<TenderAreasModel>("Tender/EditCountries", null, areasModel));
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      #endregion

      #region Edit attachment

      [HttpGet]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> QualificationAttachmentsUpdates(string id)
      {
         try
         {
            AttachmentsModel model = await GetAttahmentsStepModel(id);
            return View(model);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }

      }

      private async Task<AttachmentsModel> GetAttahmentsStepModel(string id)
      {
         AttachmentsModel model = await _ApiClient.GetAsync<AttachmentsModel>("Tender/GetAttachmentsEditStepByTenderId/" + Util.Decrypt(id), null);
         return model;
         //var list = await _ApiClient.GetListAsync<TenderAttachmentModel>("Tender/GetAttachmentsStepByTenderId/" + Util.Decrypt(id), null);
         //AttachmentsModel model = new AttachmentsModel() { AttachmentReference = "", BookletReference = "" };
         //TenderOffersStepModel bModel = await _ApiClient.GetAsync<TenderOffersStepModel>("Tender/GetOfferDetailsById/" + Util.Decrypt(id), null);
         //foreach (var item in list)
         //{
         //   if (item.AttachmentTypeId == (int)Enums.AttachmentType.TenderBookletAttachment)
         //      model.BookletReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
         //   else
         //      //model.AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.Name;
         //      model.AttachmentReference += ',' + item.FileNetReferenceId + ":" + item.Name;
         //}
         //model.TenderTypeId = bModel.TenderTypeId;
         //if (model.AttachmentReference.Length > 0)
         //   model.AttachmentReference = model.AttachmentReference.Remove(0, 1);
         //if (model.BookletReference.Length > 0)
         //   model.BookletReference = model.BookletReference.Remove(0, 1);
         //model.TenderIDString = id;
         //if (list != null)
         //   if (list.Count() == 0)
         //      model.TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
         //   else
         //      model.TenderStatusId = list[0].TenderStatusId;
         //return model;
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<IActionResult> QualificationAttachmentsUpdates(AttachmentsModel model)
      {
         ModelState.Remove("BookletReference");
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(model);
         }
         try
         {
            List<TenderAttachmentModel> tenderAttachments = await PreparePostedAttachmentsModel(model);
            tenderAttachments[0].TenderId = model.TenderID;
            //await _ApiClient.PostAsync("Tender/SaveTenderAttachmentsUpdates?tenderId=1&tenderStatusId=2", null, tenderAttachments);
            await _ApiClient.PostAsync("Tender/SaveTenderAttachmentsUpdates/" + model.TenderID + "/" + model.TenderStatusId, null, tenderAttachments);

            AddMessage(Resources.TenderResources.Messages.DataSaved);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(QualificationAttachmentsChangesApprovement), "Qualification", new { tenderIdString = model.TenderIDString });
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
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return RedirectToAction(nameof(QualificationAttachmentsUpdates), "Qualification", new { id = model.TenderIDString });
         //return View(model);
      }

      private async Task<List<TenderAttachmentModel>> PreparePostedAttachmentsModel(AttachmentsModel model)
      {
         model.TenderID = Util.Decrypt(model.TenderIDString);
         List<string> attachmentReferences = new List<string>();
         List<string> bookletReferences = new List<string>();
         if (model.AttachmentReference != null)
         {
            string[] lst = model.AttachmentReference.Split(',');
            foreach (var item in lst)
            {
               if (!string.IsNullOrEmpty(item))
               {
                  if (item.Contains("/GetFile/"))
                     attachmentReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
                  else
                     attachmentReferences.Add(item);
               }
            }
         }
         if (model.BookletReference != null)
         {
            string[] lst = model.BookletReference.Split(',');
            foreach (var item in lst)
            {
               if (!string.IsNullOrEmpty(item))
               {
                  if (item.Contains("/GetFile/"))
                     bookletReferences.Add(item.Split("/GetFile/")[item.Split("/GetFile/").Length - 1]);
                  else
                     bookletReferences.Add(item);
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
         foreach (var item in bookletReferences)
         {
            var arr = item.Split(":");
            TenderAttachmentModel tenderAttachment = new TenderAttachmentModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.TenderBookletAttachment, FileNetReferenceId = arr[0] };
            tenderAttachments.Add(tenderAttachment);
         }

         if (tenderAttachments.Count != 0)
         {
            tenderAttachments[0].TenderStatusId = model.TenderStatusId;
         }
         else
         {
            throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.UploadOneFileAtleast);
         }
         return tenderAttachments;
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> ApproveTenderAttachmentsChangeRequestAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderAttachmentsChangeRequest/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.Auditer)]
      public async Task<ActionResult> RejectTenderUpdateAttachmentAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Tender/RejectTenderUpdateAttachmentAsync", null, rejectionModel);
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
      [Authorize(Roles = RoleNames.DataEntry)]
      public async Task<ActionResult> CancelAttachmentsUpdateAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/CancelAttachmentsUpdate/" + tenderId, null, null);
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

      #region Extend Dates

      [HttpGet]
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> ExtendQualificationDatesAsync(string tenderIdString)
      {
         try
         {
            PreQualificationSavingModel response = await _ApiClient.GetAsync<PreQualificationSavingModel>("Qualification/GetQualifiacrionDatesByQualificationId/" + Util.Decrypt(tenderIdString), null);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            response.TenderIdString = tenderIdString;
            response.Vacations = vacations;
            return View(response);
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
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> ExtendQualificationDates(PreQualificationSavingModel model)
      {
         ModelState.Remove("AttachmentReference");
         ModelState.Remove("TenderName");
         ModelState.Remove("TechnicalOrganizationId");
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.Vacations = vacations;
            return View(nameof(ExtendQualificationDatesAsync), model);
         }
         try
         {
            if (model.LastOfferPresentationDate != null)
            {
               model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
            }
            if (model.OffersCheckingDate != null)
            {
               model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
            }
            model.TenderTypeId = (int)Enums.TenderType.PreQualification;
            model.TenderId = Util.Decrypt(model.TenderIdString);
            await _ApiClient.PostAsync("Tender/UpdateTenderExtendDates", null, model);

            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(QualificationExtendDateApprovement), "Qualification", new { tenderIdString = model.TenderIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.Vacations = vacations;
            return View(nameof(ExtendQualificationDatesAsync), model);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            model.Vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            return View(nameof(ExtendQualificationDatesAsync), model);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> SendUpdateDatesToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Tender/SendUpdateDatesToApprove/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.Auditer)]
      public async Task<ActionResult> ApproveTenderExtendDatesChangeRequestAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderExtendDatesChangeRequest/" + tenderId + "/" + "", null, null);
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
      [Authorize(Roles = RoleNames.Auditer)]
      public async Task<ActionResult> RejectTenderExtendDateAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);

            await _ApiClient.PostAsync<BasicTenderModel>("Tender/RejectTenderExtendDate", null, rejectionModel);
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
      [Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
      public async Task<ActionResult> CancelTenderExtendDatesAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/CancelTenderExtendDates/" + tenderId, null, null);
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

      #region cancel qualification

      [Authorize(Roles = RoleNames.supplier + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeSecretary + ","
         + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
      public async Task<ActionResult> CancelQualification(string STenderId, bool reOpen)
      {
         try
         {
            int tenderId = Util.Decrypt(STenderId);

            TenderCanelationModel tenderCanelationModel = await _ApiClient.GetAsync<TenderCanelationModel>("Tender/GetQualificationDataForCanelation/" + tenderId + "/" + reOpen, null);
            tenderCanelationModel.CancelationReasons = new List<LookupModel>(); //await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllCancelationReasons", null);
            return View(tenderCanelationModel);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
         }

      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAllSuppliersHasPrequalifications(string tenderIdString)
      {
         var areaList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllSuppliersHasPrequalifications/" + Util.Decrypt(tenderIdString), null);
         return areaList;
      }

      [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateCancelQualificationRequestAsync(TenderRevisionCancelModel tenderRevisionCancelModel)
      {
         try
         {
            var tenderRevisionCancel = await _ApiClient.PostAsync<TenderChangeRequestModel>("Tender/CreateQualificationCancelRequest", null, tenderRevisionCancelModel);// new TenderRevisionCancelModel { TenderIdString = tenderRevisionCancelModel.TenderIdString, CancelationReasonId = tenderRevisionCancelModel.CancelationReasonId, SupplierViolatorCRs = tenderRevisionCancelModel.SupplierViolatorCRs });
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

      [Authorize(RoleNames.ApproveOrRejectTenderCancelRequestPolicy)]
      [HttpPost]
      public async Task<ActionResult> ApproveQualificationCancelRequestAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync<bool>("Tender/ApproveQualificationCancelRequestAsync/" + tenderId + "/" + verificationCode, null, null);

            //await _ApiClient.PostAsync<bool>("Tender/ApproveQualificationCancelRequestAsync", new Dictionary<string, object> { { "id", Id } }, null);
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

      [Authorize(RoleNames.ApproveOrRejectTenderCancelRequestPolicy)]
      [HttpPost]
      public async Task<ActionResult> RejectTenderCancelRequestAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync<bool>("Tender/RejectTenderCancelRequestAsync", null, rejectionModel);
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
      [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
      public async Task<ActionResult> ReopenTenderAfterCancelAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenTenderAfterCancel/" + tenderId, null, null);
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

      #region Post-Qualification Approval Actions

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> PostQualificationApproval(string qualificationIdString)
      {
         try
         {
            var preQualificationId = Util.Decrypt(qualificationIdString);
            var result = await _ApiClient.GetAsync<PreQulaificationApprovalModel>("Qualification/GetPostQualificationForApproval/ " + preQualificationId, null);

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

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SendPostQualificationToApproveAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/SendPostQualificationToApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersCheckManagerPolicy)]
      public async Task<ActionResult> ApprvePostQualificationAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ApprvePostQualification/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.OffersCheckManagerPolicy)]
      public async Task<ActionResult> RejectApprvePostQualificationAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Qualification/RejectApprvePostQualification", null, rejectionModel);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> ReopenPostQualificationAsync(string tenderIdString)
      {
         try
         {
            //throw new Exception("ddd");
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Qualification/ReopenPostQualification/" + tenderId, null, null);
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      #endregion

      [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary)]
      public async Task<ActionResult> QualificationExtendDateApprovement(string tenderIdString)
      {
         return View("QualificationExtendDateApprovement", tenderIdString);
      }

      #region PreQualificationConfiguration

      [HttpPost]
      public IActionResult QualificationConfigurationType(int type, string obj)
      {
         var data = JsonConvert.DeserializeObject<PreQualificationSavingModel>(obj);
         if (type == (int)PreQualificationType.Small)
         {
            if (data.QualificationSmallEvaluation == null)
            {
               data.QualificationSmallEvaluation = new QualificationSmallEvaluation();
            }
            return PartialView("~/Views/Qualification/Partials/_SmallConfigQualification.cshtml", data);
         }
         else if (type == (int)PreQualificationType.Medium)
         {
            if (data.QualificationMediumEvaluation == null)
            {
               data.QualificationMediumEvaluation = new QualificationMediumEvaluation();
            }
            return PartialView("~/Views/Qualification/Partials/_MediumConfigQualification.cshtml", data);
         }
         else if (type == (int)PreQualificationType.Large)
         {
            if (data.QualificationLargeEvaluation == null)
            {
               data.QualificationLargeEvaluation = new QualificationLargeEvaluation();
            }
            return PartialView("~/Views/Qualification/Partials/_LargeConfigQualification.cshtml", data);
         }
         else
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return NotFound();
         }

      }

      #endregion

      #region ViewComponentAjaxCall

      public IActionResult GetQualificationSupplierIDMInfoViewComponenet(string SupplierId)
      {
         return ViewComponent("QualificationSupplierIDMInfo", new { SupplierId = SupplierId });
      }

      public IActionResult GetPreQualificationPartialDetailViewComponenet(string qualificationId)
      {
         return ViewComponent("PreQualificationPartialDetail", new { qualificationId = qualificationId });
      }

      public IActionResult GetQualificationDetailsViewComponent(string qualificationId)
      {
         return ViewComponent("PreQualificationDetail", new { id = Util.Decrypt(qualificationId) });
      }

      public IActionResult GetConfigQualificationDetailsViewComponent(string qualificationId, int qualificationTypeId)
      {

         if (qualificationTypeId == (int)Enums.PreQualificationType.Small)
         {
            return ViewComponent("SmallConfigQualificationDetails", new { qualificationId = Util.Decrypt(qualificationId) });
         }
         else if (qualificationTypeId == (int)Enums.PreQualificationType.Medium)
         {
            return ViewComponent("MediumConfigQualificationDetails", new { qualificationId = Util.Decrypt(qualificationId) });
         }
         else if (qualificationTypeId == (int)Enums.PreQualificationType.Large)
         {
            return ViewComponent("LargeConfigQualificationDetails", new { qualificationId = Util.Decrypt(qualificationId) });
         }
         return ViewComponent(string.Empty);
      }

      #endregion

      [HttpGet]
      public JsonResult RenewSessionTime()
      {

         var sessionid = HttpContext.Session.Id;
         return Json(true);
      }

   }
}
