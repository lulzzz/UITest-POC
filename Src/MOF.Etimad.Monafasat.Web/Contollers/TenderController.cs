
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.AgencyBudget;
using MOF.Etimad.Monafasat.ViewModel.LocalContent;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using MOF.Etimad.Monafasat.ViewModel.Tender;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Helpers;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class TenderController : BaseController
   {
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly IApiClient _ApiClient;
      private readonly ILogger<TenderController> _logger;
      private IMemoryCache _cache;
      private readonly IHubContext<BiddingRoundHub> _hubContext;

      public TenderController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, ILogger<TenderController> logger, IMemoryCache cache, IHubContext<BiddingRoundHub> hubContext, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _cache = cache;
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
         _logger = logger;
         _hubContext = hubContext;
      }

      #region Tenders Grids

      [Authorize(RoleNames.IndexPolicy)]
      public ActionResult Index()
      {
         if (User.UserIsVRO())
            ViewBag.IsVro = true;

         IEnumerable<string> roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
         if (roles.Count() == 0)
            return RedirectToAction(nameof(AllTendersForVisitor));
         if (User.IsInRole(RoleNames.supplier))
            return RedirectToAction(nameof(AllSuppliersTenders));
         if (User.IsInRole(RoleNames.TechnicalCommitteeUser))
            return RedirectToAction("SupplierEnquiryList", "Enquiry");
         if (User.IsInRole(RoleNames.MonafasatBlackListCommittee) && roles.Count() == 1)
            return RedirectToAction(nameof(Index), "Block");
         return View();
      }

      [Authorize(RoleNames.IndexPolicy)]
      [HttpGet]
      public async Task<ActionResult> IndexPagingAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<AllTendersViewModel>("Tender/GetAllTendersForIndexPage/", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.IndexPolicy)]
      public ActionResult AppliedSuppliersList()
      {
         return View();
      }

      [Authorize(RoleNames.IndexPolicy)]
      [HttpGet]
      public async Task<ActionResult> AppliedSuppliersListPaging(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Tender/GetTendersForAppliedSuppliersReport/", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.IndexPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTendersToJoinCommittees(LinkTendersToCommitteeSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            tenderSearchCriteriaModel.PageSize = 100000;
            var result = await _ApiClient.GetQueryResultAsync<LinkTendersToCommitteeModel>("Tender/GetTendersToJoinCommittees/", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      private string GetTitle(string Type)
      {
         string title = "";
         if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer) && Type == Enum.GetName(typeof(StepNames), StepNames.DataEntry))//UnderStablishment
         {
            title = Resources.TenderResources.DisplayInputs.UnderstablishedStageTenders;
         }
         else if (User.IsInRole(RoleNames.OffersOppeningManager) || User.IsInRole(RoleNames.OffersOppeningSecretary) && Type == Enum.GetName(typeof(StepNames), StepNames.Open))
         {
            title = Resources.TenderResources.DisplayInputs.OpenStageTenders;
         }
         else if ((User.IsInRole(RoleNames.OffersCheckManager) || User.IsInRole(RoleNames.OffersCheckSecretary)) && Type == Enum.GetName(typeof(StepNames), StepNames.Check))
         {
            title = Resources.TenderResources.DisplayInputs.CheckStageTenders;
         }
         else if ((User.IsInRole(RoleNames.OffersCheckManager) || User.IsInRole(RoleNames.OffersCheckSecretary)) && Type == Enum.GetName(typeof(StepNames), StepNames.Award))
         {
            title = Resources.TenderResources.DisplayInputs.AwardStageTenders;
         }
         return title;
      }

      public List<int> GetStatusIDs(TenderSearchCriteriaModel tenderSearchCriteriaModel, string listType)
      {
         List<int> tenderStatusIds = new List<int>();
         if ((User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer)) && tenderSearchCriteriaModel.TenderTypeString == "DataEntry")
         {
            tenderStatusIds.Add((int)Enums.TenderStatus.UnderEstablishing);
            tenderStatusIds.Add((int)Enums.TenderStatus.Pending);
            tenderStatusIds.Add((int)Enums.TenderStatus.Established);
            tenderStatusIds.Add((int)Enums.TenderStatus.Approved);
         }
         else if ((User.IsInRole(RoleNames.OffersOppeningManager) || User.IsInRole(RoleNames.OffersOppeningSecretary)) && tenderSearchCriteriaModel.TenderTypeString == "Open")
         {
            tenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppening);
            tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedPending);
            tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
            tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedRejected);
         }
         else if ((User.IsInRole(RoleNames.OffersCheckManager) || User.IsInRole(RoleNames.OffersCheckSecretary)) && (tenderSearchCriteriaModel.TenderTypeString == "Check" || tenderSearchCriteriaModel.TenderTypeString == "Award"))
         {
            if (listType.Equals("Check"))
            {
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedPending);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedRejected);
            }
            else if (listType.Equals("Award"))
            {
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwarding);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedPending);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedConfirmed);
               tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedRejected);
            }
            else
               return null;
         }
         return tenderStatusIds;
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public ActionResult TenderAwardedIndex()
      {
         return View();
      }

      [HttpGet("Tender/GetSupplierInfoByCR/{supplierCR}")]
      public async Task<PartialViewResult> GetSupplierInfoByCR(string supplierCR)
      {
         var supplierInfo = await _ApiClient.GetAsync<SupplierFullDataViewModel>("Tender/GetSupplierInfoByCR/" + supplierCR, null);
         return PartialView("~/Views/Tender/Partials/_SupplierInfo.cshtml", supplierInfo);
      }

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpGet]
      public async Task<ActionResult> TenderAwardedIndexPagingAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckingAndAwardingModel>("Tender/FindAwardedTenderBySearchCriteria/", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      #endregion Tenders Grids

      #region Tender Staging

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public ActionResult TenderIndexUnderOperationsStage(string Type)
      {
         return View();
      }

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTendersForUnderOperationsStageIndexAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Tender/GetTendersForUnderOperationsStageIndex", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
      public ActionResult TenderIndexOpeningStage(string Type)
      {
         return View();
      }

      [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTendersForOpeningStageIndexAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckingAndAwardingModel>("Tender/GetTendersForOpeningStageIndex", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize]
      public ActionResult VROTendersCreatedByGovAgency()
      {
         return View();
      }

      [Authorize]
      [HttpGet]
      public async Task<ActionResult> GetVROTendersCreatedByGovAgency(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<VROTendersCreatedByGovAgencyModel>("Tender/GetVROTendersCreatedByGovAgency", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public ActionResult TenderIndexCheckingStage(string Type)
      {
         return View();
      }

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTendersForCheckingStageIndexAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckingAndAwardingModel>("Tender/GetTendersForCheckingStageIndex", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public ActionResult TenderIndexAwardingStage(string Type)
      {
         return View();
      }

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTendersForAwardingStageIndexAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckingAndAwardingModel>("Tender/GetTendersForAwardingStageIndex", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> RejectInitialAwardTenderOffer(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectInitialAwardTenderOffer", null, rejectionModel);
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

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpPost("Tender/SendRequestToApplayForTender")]
      public async Task<ActionResult> SendRequestToApplayForTenderPost(string tenderId)
      {
         try
         {
            var id = Util.Decrypt(tenderId);
            var result = await _ApiClient.PostAsync("Tender/SendRequestToApplayForTender", null, new BasicTenderModel { TenderId = id });
            return Ok(result);
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

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpGet("Tender/SendRequestToApplayForTender/{tenderId}")]
      public async Task<ActionResult> SendRequestToApplayForTenderGet([FromRoute] string tenderId)
      {
         try
         {
            var id = Util.Decrypt(tenderId);
            var tender = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Tender/GetByTenderIdInvitationDetails/" + id, null);
            return View(tender);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(AllSuppliersTenders));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(AllSuppliersTenders));
         }
      }

      public ActionResult ConditionsBookletTemplate()
      {
         return View();
      }

      public ActionResult Create()
      {
         return View();
      }

      [Authorize(RoleNames.CreateVerificationCodePolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateVerificationCode(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.PostAsync("Tender/CreateVerificationCode", null, new BasicTenderModel
            {
               TenderId = tenderId
            });
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

      #region Invitation Methods

      [Authorize(RoleNames.SupplierPolicy)]
      public ActionResult SupplierInvitationTenders()
      {
         return View();
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> SupplierInvitationTendersPagingAsync(TenderSearchCriteria tenderSearchCriteria)
      {
         try
         {
            tenderSearchCriteria.crNumber = User.SupplierNumber();
            var result = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Invitation/GetNewInvitationsByCRNo", tenderSearchCriteria.ToDictionary());
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

      [Authorize(RoleNames.SupplierPolicy)]
      public ActionResult SupplierTenders()
      {
         ViewBag.SupplierSubscriptionType = (int)SubscriptionType.Full;
         return View();
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public ActionResult AllSuppliersTenders()
      {
         ViewBag.SupplierSubscriptionType = (int)SubscriptionType.Full;
         return View();
      }

      [AllowAnonymous]
      public ActionResult AllTendersForVisitor()
      {
         IEnumerable<string> roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
         if (User.IsInRole(RoleNames.supplier))
            return RedirectToAction(nameof(AllSuppliersTenders));
         if (User.IsInRole(RoleNames.TechnicalCommitteeUser))
            return RedirectToAction("SupplierEnquiryList", "Enquiry");
         if (User.IsInRole(RoleNames.MonafasatBlackListCommittee) && roles.Count() == 1)
            return RedirectToAction(nameof(Index), "Block");
         if (roles.Count() > 0)
            return RedirectToAction("Index", "Dashboard");
         return View();
      }

      [HttpGet]
      public async Task<ActionResult> AgencyProjectBudget(string ProjectNumber, bool IsGfs)
      {
         string AgencyCode = User.UserAgency();
         var result = await _ApiClient.GetAsync<AgencyBudgetModel>("Tender/AgencyProjectBudget/" + ProjectNumber + "/" + IsGfs + "/" + AgencyCode + "", null);
         return Json(result);
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> AllTendersAsync()
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Invitation/GetAllTenders", null);
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

      [AllowAnonymous]

      //[Authorize]
      public async Task<ActionResult> AllSupplierTendersForVisitorAsync(TenderSearchCriteriaModel tenderSearch)
      {
         try
         {
            if (string.IsNullOrEmpty(tenderSearch.Sort))
            {
               tenderSearch.Sort = "SubmitionDate";
               tenderSearch.SortDirection = "DESC";
            }
            tenderSearch.FromLastOfferPresentationDate = tenderSearch.FromLastOfferPresentationDateString;
            tenderSearch.ToLastOfferPresentationDate = tenderSearch.ToLastOfferPresentationDateString;
            QueryResult<AllTendersForVisitorModel> result = await _ApiClient.GetQueryResultAsync<AllTendersForVisitorModel>("Invitation/GetAllSupplierTendersForVisitor", tenderSearch.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearch.PageSize, result.PageNumber, null));
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
      public async Task<ActionResult> AllSupplierTendersAsync(TenderSearchCriteriaModel tenderSearch)
      {
         try
         {
            if (string.IsNullOrEmpty(tenderSearch.Sort))
            {
               tenderSearch.Sort = "SubmitionDate";
               tenderSearch.SortDirection = "DESC";
            }
            tenderSearch.FromLastOfferPresentationDate = tenderSearch.FromLastOfferPresentationDateString;
            tenderSearch.ToLastOfferPresentationDate = tenderSearch.ToLastOfferPresentationDateString;
            var result = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Tender/GetAllSupplierTenders", tenderSearch.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearch.PageSize, result.PageNumber, null));
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
      public ActionResult UnSubscribedSuppliers()
      {
         return View();
      }

      [AllowAnonymous]
      public async Task<ActionResult> AllSubscribedSupplierTendersAsync(TenderSearchCriteriaModel tenderSearch)
      {
         try
         {
            if (string.IsNullOrEmpty(tenderSearch.Sort))
            {
               tenderSearch.Sort = "SubmitionDate";
               tenderSearch.SortDirection = "DESC";
            }
            var result = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Invitation/GetAllUnsubscribedSupplierTenders", tenderSearch.ToDictionary());
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

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> SupplierTendersAsync(TenderSearchCriteria tenderSearchCriteria)
      {
         try
         {
            var crNo = User.SupplierNumber();
            tenderSearchCriteria.cr = crNo.ToString();
            if (string.IsNullOrEmpty(tenderSearchCriteria.Sort))
            {
               tenderSearchCriteria.Sort = "SubmitionDate";
               tenderSearchCriteria.SortDirection = "DESC";
            }
            var result = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Invitation/GetSupplierTenders", tenderSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteria.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> SuppliersJoiningRequest(string tenderIdString)
      {
         try
         {
            TenderInformationModel tenderInformationModel = await _ApiClient.GetAsync<TenderInformationModel>("Tender/GetTenderByIdForJoinigRequests/" + Util.Decrypt(tenderIdString).ToString(), null);
            return View(tenderInformationModel);
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

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> SuppliersJoininqRequestPagingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Invitation/GetSuppliersJoiningRequestsByTenderId/" + tenderId, null);
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

      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetAllCities()
      {
         List<LookupModel> result = await _cache.GetOrCreateAsync(CacheKeys.GetCities, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetCities", null);
         });
         return result;
      }

      [HttpGet]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> TenderInvitationDetails(string tenderIdString)
      {
         try
         {
            var tender = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Tender/GetByTenderIdInvitationDetails/" + Util.Decrypt(tenderIdString), null);
            return View(tender);
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
      [Authorize(RoleNames.UpdateInvitationStatusPolicy)]
      public async Task<ActionResult> UpdateInvitationStatus(string invitationId, string statusId)
      {
         try
         {
            var invitation = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Invitation/UpdateInvitationStatus", new Dictionary<string, object> { { "invitationId", invitationId }, { "statusId", statusId } });
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
      [Authorize(RoleNames.UpdateInvitationStatusPolicy)]
      public async Task<ActionResult> AcceptInvitationWithFeesAsync(TenderFinantialCostModel costModel)
      {
         try
         {
            var invitation = await _ApiClient.PostAsync<TenderInvitationDetailsModel>("Invitation/AcceptInvitationWithFees", null, costModel);
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
      //[Authorize(RoleNames.supplier)]
      public async Task<ActionResult> JoinDirectPurchaseTenderAsync(string tenderIdstring)
      {
         try
         {
            var invitation = await _ApiClient.PostAsync("Invitation/JoinDirectPurchaseTender/" + tenderIdstring, null, null);
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
      [Authorize(RoleNames.UpdateInvitationStatusPolicy)]
      public async Task<ActionResult> ApproveJoiningRequestStatus(string invitationId, string statusId)
      {
         try
         {
            var invitation = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Invitation/ApproveJoiningRequestStatus", new Dictionary<string, object> { { "invitationId", Util.Decrypt(invitationId) }, { "statusId", statusId } });
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> RejectJoiningRequestStatus(string sInvitationId, string rejectionReason)
      {
         try
         {
            int invitationId = Util.Decrypt(sInvitationId);
            var invitation = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Invitation/RejectJoiningRequestStatus", new Dictionary<string, object> { { "invitationId", invitationId }, { "statusId", (int)InvitationStatus.Rejected }, { "rejectionReason", rejectionReason } });
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> SendTenderInvitations(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var tender = await _ApiClient.GetAsync<InvitationStepModel>("Tender/GetByTenderIdInvitationDetails/" + tenderId, null);
            return View(tender);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SendInvitationsAsync(List<InvitationCrModel> suppliers)
      {
         try
         {
            var tenderList = await _ApiClient.PostAsync("Tender/SendInvitations", null, suppliers);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SendInvitationsByEmailAsync(int tenderId, List<string> emails)
      {
         try
         {
            var emailslist = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Invitation/SendInvitationByEmail", new Dictionary<string, object> { { "tenderId", tenderId }, { "emails", emails } });
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> SendInvitationsBySmsAsync(int tenderId, List<string> mobileNoList)
      {
         try
         {
            var mobileList = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Invitation/SendInvitationBySms", new Dictionary<string, object> { { "tenderId", tenderId }, { "mobilNoList", mobileNoList } });
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetSuppliersBySearchCretria(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Invitation/GetAllSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetInvitedSuppliers(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetAllInvitedSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetAllInvitedSuppliersAndSendInvitationAgain(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            supplierSearchCretria.InvitationTenderId = Util.Decrypt(supplierSearchCretria.InvitationTenderIdString);
            var tenderList = await _ApiClient.PostAsync("Invitation/GetAllInvitedSuppliersAndSendInvitation", null, supplierSearchCretria);

            if (Convert.ToBoolean(tenderList))
            {
               return Json(new { status = "success", message = "تم إعادة الارسال بنجاح" });
            }
            else
            {
               return Json(new { status = "success", message = "لا يوجد دعوات لاعادة الارسال" });
            }

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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetInvitedUnRegisterSuppliers(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetAllInvitedUnRegisterSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> GetInvitedUnRegisterSuppliersForCreation(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Invitation/GetInvitedUnRegisterSuppliersForCreation", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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

      #region Data Entry Joining Requests

      [Authorize(RoleNames.JoiningRequestDetailsPolicy)]
      public async Task<ActionResult> JoiningRequestDetails(string invitationIdString)
      {
         try
         {
            int invitationId = Util.Decrypt(invitationIdString);
            var joiningRequest = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("Invitation/GetJoiningRequestByInvitationId/" + invitationId, null);
            ViewBag.SinvitationId = Util.Encrypt(invitationId);
            return View(joiningRequest);
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

      #endregion

      #endregion

      #region Create and update Tender

      #region Basic Tender Data step

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> BasicTenderData(string id, int? IsCommittee, string tenderTypeIdString, string announcementIdString)
      {
         try
         {
            int? tenderTypeId = !string.IsNullOrEmpty(tenderTypeIdString) ? Util.Decrypt(tenderTypeIdString) : (int?)null;
            int? announcementId = !string.IsNullOrEmpty(announcementIdString) ? Util.Decrypt(announcementIdString) : (int?)null;
            return View(await PrepareTenderCreationAsync(id, IsCommittee, tenderTypeId, announcementId));
         }
         catch (AuthorizationException ex)
         {
            AddError(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            throw ex;
         }
         catch (UnHandledAccessException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (BusinessRuleException ex)
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

      [HttpGet]
      [Authorize(Roles = RoleNames.DataEntry)]
      public async Task<ActionResult> BasicSecondStageData(string id)
      {
         try
         {
            return View(await PrepareBasicSecondStageTenderDataAsync(id));
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
      [Authorize(Roles = RoleNames.DataEntry)]
      public async Task<ActionResult> BasicSecondStageData(SecondStageBasicData model)
      {
         if (!string.IsNullOrEmpty(model.AgencyBudgetProjectNumber))
         {
            model.AgencyBudgetNumber = JsonConvert.DeserializeObject<List<AgencyBudgetNumberModel>>(model.AgencyBudgetProjectNumber);
         }
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            model = await PrepareBasicSecondStageTenderDataAsync(model.TenderIdString, model);
            return View(model);
         }
         try
         {
            model.TenderId = Util.Decrypt(model.TenderIdString);
            SecondStageBasicData tenderBasicDataModel = await _ApiClient.PostAsync<SecondStageBasicData>("Tender/CreateSeoncdStageBasic", null, model);
            return RedirectToAction(nameof(TenderDates), new { tenderId = Util.Encrypt(tenderBasicDataModel.TenderId) });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(TenderIndexUnderOperationsStage));
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
         model = await PrepareBasicSecondStageTenderDataAsync(model.TenderIdString, model);
         return View(model);
      }

      private async Task<SecondStageBasicData> PrepareBasicSecondStageTenderDataAsync(string id, SecondStageBasicData tenderBasicDataModel = null)
      {
         List<LookupModel> tenderTypes = await _cache.GetOrCreateAsync(CacheKeys.TenderTypes, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllTenderTypes", null);
         });
         if ((!string.IsNullOrWhiteSpace(id) && id != "0") && tenderBasicDataModel == null)
         {
            tenderBasicDataModel = await _ApiClient.GetAsync<SecondStageBasicData>("Tender/GetBasicTenderSecondStageDataById/" + Util.Decrypt(id).ToString(), null);
         }
         else if (tenderBasicDataModel == null)
         {
            tenderBasicDataModel = new SecondStageBasicData();
         }
         EditeCommitteesModel committeesModel = new EditeCommitteesModel();
         await FillTenderCommitteesAsync(committeesModel);
         tenderBasicDataModel.TechnicalCommittees = committeesModel.TechnicalCommittees;
         tenderBasicDataModel.OfferOpenningCommittees = committeesModel.OfferOpenningCommittees;
         tenderBasicDataModel.OfferCheckingCommittees = committeesModel.OfferCheckingCommittees;
         tenderBasicDataModel.IsAgancyHasTechnicalCommittee = committeesModel.IsAgancyHasTechnicalCommittee;
         tenderBasicDataModel.CreatedBy = string.IsNullOrEmpty(id) ? User.FullName() : tenderBasicDataModel.CreatedBy;
         tenderBasicDataModel.TenderTypes = tenderTypes;
         tenderBasicDataModel.TenderTypeId = (int)TenderType.SecondStageTender;
         tenderBasicDataModel.TenderTypeName = Resources.TenderResources.DisplayInputs.SecondStageTender;
         return tenderBasicDataModel;
      }

      private async Task<CreateTenderBasicDataModel> PrepareTenderCreationAsync(string id, int? IsCommittee, int? announcemenTenderTypeId, int? announcementId, CreateTenderBasicDataModel tenderBasicDataModel = null)
      {
         announcemenTenderTypeId = announcementId == null ? null : announcemenTenderTypeId;
         int tenderId = string.IsNullOrEmpty(id) ? 0 : Util.Decrypt(id);
         bool hasQualification = tenderBasicDataModel == null ? false : tenderBasicDataModel.HasQualification;

         var invitationTypeId = tenderBasicDataModel == null ? false : tenderBasicDataModel.InvitationCheck;
         var tenderTypeId = tenderBasicDataModel == null ? 0 : tenderBasicDataModel.TenderTypeId;

         if (tenderBasicDataModel == null)
         {
            tenderBasicDataModel = new CreateTenderBasicDataModel();
            tenderBasicDataModel = await _ApiClient.GetAsync<CreateTenderBasicDataModel>("Tender/GetBasicTenderDataById/" + tenderId, null);
         }
         if (!string.IsNullOrWhiteSpace(id) && tenderId != 0)
         {
            hasQualification = tenderBasicDataModel.HasQualification;
            tenderTypeId = tenderBasicDataModel.TenderTypeId;
            invitationTypeId = tenderBasicDataModel.InvitationTypeId == (int)Enums.InvitationType.Public ? true : false;
         }
         tenderTypeId = announcemenTenderTypeId != null ? (int)announcemenTenderTypeId : tenderTypeId;
         List<LookupModel> tenderTypes = await _ApiClient.GetListAsync<LookupModel>("Tender/GetTenderTypeLookups", null);
         tenderBasicDataModel.ReasonForPurchaseTenderType = await _cache.GetOrCreateAsync(CacheKeys.DirectPurchaseTenderReasons, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllDirectPurchaseTenderReasons", null);
         });
         tenderBasicDataModel.ReasonForLimitedTenderType = await _cache.GetOrCreateAsync(CacheKeys.LimitedTenderReasons, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllLimitedTenderReasons", null);
         });
         var preAnnouncementLookup = new LookupModel();
         if (announcementId != null)
         {
            preAnnouncementLookup = await _ApiClient.GetAsync<LookupModel>("Announcement/GetAnnouncementNameByAnnouncementId/" + announcementId, null);

            tenderBasicDataModel.TenderName = preAnnouncementLookup.Name;
         }
         if (!string.IsNullOrEmpty(tenderBasicDataModel.PurchaseMethodString))
            tenderBasicDataModel.PurchaseMethods = tenderBasicDataModel.PurchaseMethods == null ? new List<int>() : tenderBasicDataModel.PurchaseMethodString.Split(',').Select(int.Parse).ToList();
         tenderBasicDataModel.TenderTypes = tenderBasicDataModel.PurchaseMethods == null || tenderBasicDataModel.PurchaseMethods.Count == 0 ? tenderTypes : tenderTypes.Where(x => tenderBasicDataModel.PurchaseMethods.Contains(x.Id)).ToList();
         tenderBasicDataModel.IsAgencyRelatedByVRO = User.UserIsRelatedVRO();

         if (User.UserIsRelatedVRO() == false && tenderBasicDataModel.TenderTypes.Any(a => a.Id == (int)TenderType.NationalTransformationProjects))
            tenderBasicDataModel.TenderTypes.Remove(tenderBasicDataModel.TenderTypes.FirstOrDefault(a => a.Id == (int)TenderType.NationalTransformationProjects));

         tenderBasicDataModel.HasQualification = hasQualification;
         tenderBasicDataModel.TenderTypeId = tenderTypeId;
         tenderBasicDataModel.PreAnnouncementId = announcementId != null ? announcementId : tenderBasicDataModel.PreAnnouncementId;
         tenderBasicDataModel.ReasonForPurchaseTenderTypeId = announcemenTenderTypeId == null ? tenderBasicDataModel.ReasonForPurchaseTenderTypeId
            : (tenderTypeId == (int?)Enums.TenderType.NewDirectPurchase ? (int)Enums.ReasonForPurchaseTenderType.BusinessAndProcurementAreAvailableToOneContractorCSupplierAndHaveNoAcceptableAlternative : tenderBasicDataModel.ReasonForPurchaseTenderTypeId);
         tenderBasicDataModel.IsLinkedToAnnouncement = announcemenTenderTypeId == null ? tenderBasicDataModel.IsLinkedToAnnouncement
            : (tenderTypeId == (int?)Enums.TenderType.LimitedTender ? true : tenderBasicDataModel.IsLinkedToAnnouncement);
         tenderBasicDataModel.InvitationCheck = invitationTypeId;
         tenderBasicDataModel.TenderIdString = Util.Encrypt(tenderBasicDataModel.TenderId);
         tenderBasicDataModel.IsVRO = User.UserIsVRO();
         if (tenderBasicDataModel.CurrentDate.Subtract(tenderBasicDataModel.LastEnqueriesDate ?? DateTime.Now.Date).Days > 0)
            tenderBasicDataModel.CurrentGreaterEnquiryDate = true;
         if (IsCommittee != null && IsCommittee != 0)
            tenderBasicDataModel.IsCommitteeUpdate = true;

         return tenderBasicDataModel;
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> BasicTenderData(CreateTenderBasicDataModel model)
      {
         if (!string.IsNullOrEmpty(model.AgencyBudgetProjectNumber))
         {
            model.AgencyBudgetNumbers = JsonConvert.DeserializeObject<List<AgencyBudgetNumberModel>>(model.AgencyBudgetProjectNumber);
         }
         if (!string.IsNullOrEmpty(model.TenderIdString)) model.TenderId = Util.Decrypt(model.TenderIdString);
         if (ModelState[""] != null)
            ModelState[""].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            model = await PrepareTenderCreationAsync(model.TenderIdString, model.IsCommitteeUpdate.HasValue && model.IsCommitteeUpdate.Value ? (int?)1 : null, model.TenderTypeId, model.PreAnnouncementId, model);
            return View(model);
         }
         try
         {
            model.TenderId = Util.Decrypt(model.TenderIdString);
            // if agency related to vro and user select tender of type (مشروع التحول الوطنى) 
            // and route to (relation step) because (Dates Step) or all dates entered by مسول اعتماد فى VRO at tender approval
            if (User.UserIsRelatedVRO() && model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
               TenderRelationsModel tenderRelationsModel = await _ApiClient.PostAsync<TenderRelationsModel>("Tender/CreateVROTenderByRelatedAgency", null, model);
               return RedirectToAction(nameof(RelationsStep), new { tenderId = tenderRelationsModel.TenderIdString });
            }
            // if user select tender of another type, we follow the normal process of creation
            else
            {
               TenderDatesModel tenderDatesModel = await _ApiClient.PostAsync<TenderDatesModel>("Tender/Add", null, model);
               if (model.TenderStatusId == (int)TenderStatus.Approved)
               {
                  AddMessage(Resources.TenderResources.Messages.DataSaved);
                  return RedirectToAction(nameof(TenderIndexUnderOperationsStage));
               }
               else
               {
                  return RedirectToAction(nameof(TenderDates), new { tenderId = Util.Encrypt(tenderDatesModel.TenderId) });
               }
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
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         model = await PrepareTenderCreationAsync(model.TenderIdString, model.IsCommitteeUpdate.HasValue && model.IsCommitteeUpdate.Value ? (int?)1 : null, model.TenderTypeId, model.PreAnnouncementId, model);
         return View(model);
      }

      #endregion

      #region Tender Dates step

      private async Task<TenderDatesModel> PrepareTenderDatesAsync(TenderDatesModel tenderDatesModel)
      {
         tenderDatesModel.NeedInitialGuarantee = tenderDatesModel == null ? false : tenderDatesModel.NeedInitialGuarantee;
         tenderDatesModel.SupplierNeedSample = tenderDatesModel?.SupplierNeedSample == null ? false : (bool)tenderDatesModel.SupplierNeedSample; ;
         tenderDatesModel.Vacations = await _cache.GetOrCreateAsync(CacheKeys.VacationCache, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            return await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
         });
         tenderDatesModel.OffersOpeningAddressList = await _cache.GetOrCreateAsync(CacheKeys.AddressesCache + "_" + User.UserBranch(), async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<AddressModel>("Tender/GetOfferOpeningAddressesByBranchId/" + User.UserBranch(), null);
         });
         return tenderDatesModel;
      }

      private async Task<TenderDatesModel> PrepareTenderDatesWithLookUpsAsync(TenderDatesModel tenderDatesModel)
      {
         bool isNeedInitialGuarantee = tenderDatesModel == null ? false : tenderDatesModel.NeedInitialGuarantee;
         bool isSupplierNeedSample = tenderDatesModel?.SupplierNeedSample == null ? false : (bool)tenderDatesModel.SupplierNeedSample;
         tenderDatesModel.NeedInitialGuarantee = isNeedInitialGuarantee;
         tenderDatesModel.SupplierNeedSample = isSupplierNeedSample;
         tenderDatesModel.VersionId = tenderDatesModel.VersionId;
         tenderDatesModel.Vacations = await _cache.GetOrCreateAsync(CacheKeys.VacationCache, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            return await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
         });
         tenderDatesModel.OffersOpeningAddressList = await _cache.GetOrCreateAsync(CacheKeys.AddressesCache + "_" + User.UserBranch(), async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<AddressModel>("Tender/GetOfferOpeningAddressesByBranchId/" + User.UserBranch(), null);
         });
         return tenderDatesModel;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> TenderDates(string tenderId)
      {
         TenderDatesModel datesModel;
         try
         {
            datesModel = await _ApiClient.GetAsync<TenderDatesModel>("Tender/GetTenderDatesById/" + Util.Decrypt(tenderId), null);
            await PrepareTenderDatesAsync(datesModel);
            return View(datesModel);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));
         }
         catch (UnHandledAccessException ex)
         {
            throw ex;
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> TenderDates(TenderDatesModel model)
      {
         model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
         if (model.OffersOpeningDate != null)
         {
            model.OffersOpeningDate += DateHelper.GetTimePart(model.OffersOpeningTime);
         }
         model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
         if (model.DeliveryDate != null)
         {
            model.DeliveryDate += DateHelper.GetTimePart(model.DeliveryTime);
         }
         if (model.DeliveryDate != null && model.IsSampleAddresSameOffersAddress)
         {
            //model.OffersDeliveryDate += DateHelper.GetTimePart(model.DeliveryTime);
            model.OffersDeliveryDate = model.DeliveryDate;
         }
         if (model.OffersDeliveryDate != null && !model.IsSampleAddresSameOffersAddress)
         {
            model.OffersDeliveryDate += DateHelper.GetTimePart(model.OffersDeliveryTime);
         }
         if ((model.TenderTypeId == (int)TenderType.CurrentDirectPurchase && model.OffersOpeningAddressId == null) || model.TenderTypeId == (int)TenderType.NewDirectPurchase || model.TenderTypeId == (int)TenderType.FirstStageTender || model.TenderTypeId == (int)TenderType.Competition)
         {
            ModelState.Remove("OffersOpeningDate");
            ModelState.Remove("OffersOpeningTime");
         }
         if (model.TenderTypeId != (int)TenderType.NewDirectPurchase && model.TenderTypeId != (int)TenderType.FirstStageTender && model.TenderTypeId != (int)TenderType.Competition)// || model.TenderTypeId == (int)Enums.TenderType.NewTender)
         {
            ModelState.Remove("OffersCheckingDate");
            ModelState.Remove("OffersCheckingTime");
         }
         if (model.IsOldTender && (bool)model.SupplierNeedSample)
         {
            ModelState.Remove("BuildingName");
            ModelState.Remove("FloarNumber");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("DeliveryDate");
            ModelState.Remove("DeliveryTime");
         }
         if (model.IsOldTender || model.VersionId < (int)Enums.ActivityVersions.Sprint7Activities)
         {
            ModelState.Remove("AwardingExpectedDate");
            ModelState.Remove("StartingBusinessOrServicesDate");
            ModelState.Remove("OfferDeliveryAddress");
            ModelState.Remove("OfferBuildingName");
            ModelState.Remove("OfferFloarNumber");
            ModelState.Remove("OfferDepartmentName");
            ModelState.Remove("OffersDeliveryDate");
            ModelState.Remove("OffersDeliveryTime");
         }

         if (!model.NeedInitialGuarantee)
         {
            ModelState.Remove("InitialGuaranteeAddress");
            ModelState.Remove("InitialGuaranteePercentage");
         }
         if (!(bool)model.SupplierNeedSample)
         {
            ModelState.Remove("SamplesDeliveryAddress");
            ModelState.Remove("BuildingName");
            ModelState.Remove("FloarNumber");
            ModelState.Remove("DepartmentName");
            ModelState.Remove("DeliveryDate");
            ModelState.Remove("DeliveryTime");
         }
         if (!ModelState.IsValid)
         {
            model.TenderId = Util.Decrypt(model.TenderIdString);
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            await PrepareTenderDatesWithLookUpsAsync(model);
            return View(model);
         }
         try
         {
            if (!string.IsNullOrEmpty(model.OffersOpeningAddress))
               _cache.Remove(CacheKeys.AddressesCache + "_" + User.UserBranch());
            model.TenderId = Util.Decrypt(model.TenderIdString);
            TenderRelationsModel tenderRelationsModel = await _ApiClient.PostAsync<TenderRelationsModel>("Tender/UpdateTenderDates", null, model);
            if (tenderRelationsModel.TenderTypeId == (int)TenderType.SecondStageTender && tenderRelationsModel.TenderStatusId == (int)TenderStatus.UnderEstablishing)
            {
               return RedirectToAction(nameof(RelationsStep), new { tenderId = tenderRelationsModel.TenderIdString, secondStageTenderId = tenderRelationsModel.TenderIdString });
            }
            else
            {
               return RedirectToAction(nameof(RelationsStep), new { tenderId = model.TenderIdString });
            }
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            await PrepareTenderDatesWithLookUpsAsync(model);
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         await PrepareTenderDatesWithLookUpsAsync(model);
         return View(model);
      }

      #endregion

      #region Relations Data step

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> RelationsStep(string tenderId, string secondStageTenderId = null)
      {
         try
         {
            TenderRelationsModel response;
            response = await _ApiClient.GetAsync<TenderRelationsModel>("Tender/GetRelationsById/" + Util.Decrypt(tenderId), null);
            await FillTenderRelationsStepModelAsync(response, tenderId);
            if (secondStageTenderId != null)
               response.TenderIdString = secondStageTenderId;
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
         catch (UnHandledAccessException ex)
         {
            throw ex;
         }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      private async Task FillTenderRelationsStepModelAsync(TenderRelationsModel tenderRelationsModel, string id)
      {
         RelationsStepModels lookUps = await _cache.GetOrCreateAsync(CacheKeys.RelationsStepCache + tenderRelationsModel.ActivityVersionId, async entry =>
          {
             int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
             entry.SlidingExpiration = TimeSpan.FromHours(minutes);
             return await _ApiClient.GetAsync<RelationsStepModels>("Lookup/GetRelationsStepLookups/" + tenderRelationsModel.ActivityVersionId, null);
          });

         List<SelectListItem> worksItems = _cache.GetOrCreate(CacheKeys.WorkItemsCache, entry =>
         {
            var works = new List<SelectListItem>();
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            foreach (var item in lookUps.ConstructionWorks)
            {
               var group = new SelectListGroup { Name = item.Name };
               foreach (var sub in item.SubWorks)
                  works.Add(new SelectListItem { Value = sub.ConstructionWorkId.ToString(), Text = sub.Name, Group = group });
            }
            return works;
         });
         List<SelectListItem> activitiesItems = _cache.GetOrCreate(CacheKeys.ActivitiesCache + tenderRelationsModel.ActivityVersionId, entry =>
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
         ViewBag.ListOfConstructioWorks = worksItems;
         ViewBag.ListOfActivities = activitiesItems;
         tenderRelationsModel.Areas = lookUps.Areas;
         tenderRelationsModel.Countries = lookUps.Countries;
         tenderRelationsModel.ActivitiesWithYears = lookUps.ActivitiesWithYears;
         tenderRelationsModel.MaintenanceWorks = lookUps.RunningWorks;
         if (tenderRelationsModel.TenderAreaIDs.Count == 0 && tenderRelationsModel.TenderCountriesIDs.Count == 0)
            tenderRelationsModel.InsideKSA = true;
         else if (tenderRelationsModel.TenderAreaIDs.Count == 0 && tenderRelationsModel.TenderCountriesIDs.Count > 0)
            tenderRelationsModel.InsideKSA = false;
         tenderRelationsModel.TenderIdString = id;
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> RelationsStep(TenderRelationsModel model)
      {
         ModelState.Remove("TenderId");
         model.TenderId = Util.Decrypt(model.TenderIdString);
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            await FillTenderRelationsStepModelAsync(model, model.TenderIdString);
            return View(model);
         }
         try
         {
            bool.TryParse(model.InsideKSA.ToString(), out bool isInside);
            if (isInside)
            {
               model.Countries = null;
               model.TenderCountriesIDs = new List<int>();
               if (model.TenderAreaIDs.Count == 0)
               {
                  AddError(Resources.TenderResources.ErrorMessages.EnterTenderAreas);
                  throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderAreas);
               }
            }
            else
            {
               model.Areas = null;
               model.TenderAreaIDs = new List<int>();
               if (model.TenderCountriesIDs.Count == 0)
               {
                  AddError(Resources.TenderResources.ErrorMessages.EnterTenderCountries);
                  throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterTenderCountries);
               }
            }
            if (model.TenderTypeId == (int)Enums.TenderType.FirstStageTender || model.TenderTypeId == (int)Enums.TenderType.Competition || model.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
               await _ApiClient.PostAsync<QuantitiesTablesModel>("Tender/UpdateTenderRelationsWithoutQuantityTable", null, model);
               if (model.TenderTypeId == ((int)Enums.TenderType.CurrentDirectPurchase) || model.TenderTypeId == ((int)Enums.TenderType.CurrentTender) || model.TenderTypeId == ((int)Enums.TenderType.NationalTransformationProjects))
               {
                  return RedirectToAction(nameof(AttachmentsStep), new { id = model.TenderIdString });
               }
               else
               {
                  return RedirectToAction(nameof(UpdateConditionsTemplate), new { tenderIdString = model.TenderIdString });
               }
            }
            else
            {
               QuantitiesTemplateModel tables = await _ApiClient.PostAsync<QuantitiesTemplateModel>("Tender/UpdateTenderRelations", null, model);
               return RedirectToAction(nameof(QuantitiesTableStep), new { id = model.TenderIdString });
            }
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            await FillTenderRelationsStepModelAsync(model, model.TenderIdString);
            return View(model);
         }
         catch (Exception)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      #endregion

      #region Quantities Table step

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> QuantitiesTableStep(string id)
      {
         try
         {
            QuantityTableStepDTO tables;
            tables = await _ApiClient.GetAsync<QuantityTableStepDTO>("Tender/GetTenderQuantityItemsById/" + Util.Decrypt(id), null);
            if (tables.TenderTypeId == (int)TenderType.Competition || tables.TenderTypeId == (int)TenderType.FirstStageTender || tables.TenderTypeId == (int)TenderType.ReverseBidding)
            {
               throw new AuthorizationException();
            }
            return View(tables);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (UnHandledAccessException ex)
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> TenderQuantitiesTableUpdates(string id)
      {
         try
         {
            QuantityTableStepDTO tables;

            tables = await _ApiClient.GetAsync<QuantityTableStepDTO>("Tender/GetTenderQuantityItemsChangesById/" + Util.Decrypt(id), null);

            return View(tables);
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> GetEmptyForm(int formid, int tenderId, int templateYears, string tableName)
      {
         try
         {
            tableName = string.IsNullOrEmpty(tableName) ? "اسم الجدول" : tableName;
            string result = await _ApiClient.GetStringAsync("Tender/GetEmptyForm/" + formid + "/" + tenderId + "/" + templateYears + "/" + tableName, null);
            return Content(result);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> UpdateTableName(int tableid, int tenderId, string tableName)
      {
         try
         {
            string result = await _ApiClient.GetStringAsync("Tender/UpdateTableName/" + tableid + "/" + tenderId + "/" + tableName, null);
            return Content(result);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> UpdateTableChangesName(int tableid, int tenderId, string tableName)
      {
         try
         {
            string result = await _ApiClient.GetStringAsync("Tender/UpdateTableChangesName/" + tableid + "/" + tenderId + "/" + tableName, null);
            return Content(result);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> UpdateHasAlternative(int tenderId, bool hasAlternative)
      {
         try
         {
            string result = await _ApiClient.GetStringAsync("Tender/UpdateHasAlternative/" + tenderId + "/" + hasAlternative, null);
            return Content(result);
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
            return JsonErrorMessage(ex.Message);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task DeleteTable(int tableId, int tenderId)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/DeleteTable/" + tableId + "/" + tenderId, null);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task DeleteTableChange(int tableId, int tenderId, bool isNewTable)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/DeleteTableChange/" + tableId + "/" + tenderId + "/" + isNewTable, null);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task ChangeHasAlternativeOffer(int tenderId, bool hasAlternativeOffer)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/ChangeHasAlternativeOffer" + "/" + tenderId + "/" + hasAlternativeOffer, null);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task DeleteQuantityTableChangeRequest(int tenderId)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/DeleteQuantityTableChangeRequest/" + tenderId, null);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task DeleteExistingTableChange(long tableId, int tenderId)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/DeleteExistingTableChange/" + tableId + "/" + tenderId, null);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> SaveTenderQuantityItem(IFormCollection collection)
      {
         try
         {
            Dictionary<string, string> authorList = new Dictionary<string, string>();
            foreach (var item in collection.Keys)
            {
               authorList.Add(item, collection[item].ToString());
            }
            int tenderId = int.Parse(authorList["TenderId"]);
            ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Tender/ValidateQuantitiesTables", null, authorList);
            return Json(obj);
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
            return JsonErrorMessage(ex.Message);
         }

      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> DeleteTenderQuantityItem(IFormCollection collection)
      {
         Dictionary<string, string> authorList = new Dictionary<string, string>();
         foreach (var item in collection.Keys)
         {
            authorList.Add(item, collection[item].ToString());
         }
         int tenderId = int.Parse(authorList["TenderId"]);
         ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Tender/DeleteTenderQuantityItem", null, authorList);
         return Ok(obj);
      }

      [HttpPost]
      public async Task<ActionResult> SaveTenderQuantityItemChangeAsync(IFormCollection collection)
      {
         Dictionary<string, string> authorList = new Dictionary<string, string>();
         foreach (var item in collection.Keys)
         {
            authorList.Add(item, collection[item].ToString());
         }
         int tenderId = int.Parse(authorList["TenderId"]);
         ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Tender/ValidateQuantitiesTablesChanges", null, authorList);
         return Ok(obj);
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> DeleteTenderQuantityItemChangeAsync(IFormCollection collection)
      {
         Dictionary<string, string> authorList = new Dictionary<string, string>();
         foreach (var item in collection.Keys)
         {
            authorList.Add(item, collection[item].ToString());
         }
         int tenderId = int.Parse(authorList["TenderId"]);
         ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Tender/DeleteTenderQuantityItemChanges", null, authorList);
         return Ok(obj);
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> QuantitiesTableStep(FormCollection collection)
      {
         List<ValidationReturndTemplate> x = await _ApiClient.PostAsync<List<ValidationReturndTemplate>>("Tender/ValidateQuantitiesTables", null, collection);
         return View(x);
      }

      //[HttpPost]
      //[Authorize(RoleNames.DataEntryPolicy)]
      //public async Task<ActionResult> QuantitiesTableStepExxx(List<QuantityTableModel> model, string id, bool hasAlternativeOffer)
      //{
      //   if (!ModelState.IsValid)
      //   {
      //      AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
      //      var tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //      tables.TenderIdString = id;
      //      return View(tables);
      //   }
      //   try
      //   {
      //      model[0].TenderId = Util.Decrypt(id);
      //      await _ApiClient.PostAsync("Tender/SaveQuantitiesTables/" + hasAlternativeOffer, null, model);
      //      return View(new QuantitiesTablesModel { QuantitiesTables = model, TenderID = Util.Decrypt(id), TenderIdString = id });
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      return RedirectToAction(nameof(Index));
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //      return RedirectToAction(nameof(Index));
      //   }
      //}
      #endregion
      #region Quantities Table Templates

      //[HttpGet]
      //[Authorize(RoleNames.DataEntryPolicy)]
      //public async Task<ActionResult> QuantitiesTableTemplate(string id)
      //{
      //   try
      //   {
      //      QuantitiesTablesModel tables;
      //      //if (TempData["model"] != null)
      //      //{
      //      //   tables = TempData.Get<QuantitiesTablesModel>("model");
      //      //   TempData["model"] = null;
      //      //}
      //      //else
      //      //{
      //         tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //         tables.TenderIdString = id;
      //      //}

      //      return View(tables);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      return RedirectToAction(nameof(Index));
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //      return RedirectToAction(nameof(Index));
      //   }
      //}

      //[HttpPost]
      //[Authorize(RoleNames.DataEntryPolicy)]
      //public async Task<ActionResult> QuantitiesTableTemplate(List<QuantityTableModel> model, string id, bool hasAlternativeOffer)
      //{
      //   if (!ModelState.IsValid)
      //   {
      //      AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
      //      var tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //      tables.TenderIdString = id;
      //      return View(tables);
      //   }
      //   try
      //   {
      //      model[0].TenderId = Util.Decrypt(id);
      //      await _ApiClient.PostAsync("Tender/SaveQuantitiesTables/" + hasAlternativeOffer, null, model);
      //      return View(new QuantitiesTablesModel { QuantitiesTables = model, TenderID = Util.Decrypt(id), TenderIdString = id });
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      return RedirectToAction(nameof(Index));
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //      return RedirectToAction(nameof(Index));
      //   }
      //}
      #endregion
      #region Attachments Step
      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> AttachmentsStep(string id)
      {
         try
         {
            AttachmentsModel model = await GetAttahmentsStepModel(id);
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

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> TenderAttachmentsUpdates(AttachmentsModel model)
      {
         if (model.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase ||
             model.TenderTypeId == (int)Enums.TenderType.CurrentTender ||
             model.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
               return View(model);
            }
         }
         try
         {
            List<TenderAttachmentModel> tenderAttachments = await PreparePostedAttachmentsModel(model);
            await _ApiClient.PostAsync("Tender/SaveTenderAttachmentsUpdates/" + model.TenderID + "/" + model.TenderStatusId, null, tenderAttachments);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(TenderAttachmentsChangesApprovement), "Tender", new { tenderIdString = model.TenderIDString });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(TenderAttachmentsUpdates), new { id = model.TenderIDString });
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
         return View(model);
      }
      public async Task<AttachmentsModel> GetAttahmentsStepModel(string id)
      {
         AttachmentsModel model = await _ApiClient.GetAsync<AttachmentsModel>("Tender/GetAttachmentsEditStepByTenderId/" + Util.Decrypt(id), null);
         return model;
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> AttachmentsStep(AttachmentsModel model)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return View(model);
         }
         try
         {
            List<TenderAttachmentModel> tenderAttachments = await PreparePostedAttachmentsModel(model);
            tenderAttachments[0].TenderId = model.TenderID;
            BasicTenderInfoModel tenderInfoModel = await _ApiClient.PostAsync<BasicTenderInfoModel>("Tender/SaveTenderAttachments", null, tenderAttachments);
            if (tenderInfoModel.HasInvitaitons)
            {
               return RedirectToAction(nameof(SendInvitationsStep), new { tenderIdString = tenderInfoModel.TenderIdString });
            }
            return RedirectToAction(nameof(Index));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(model);
         }
         catch (UnHandledAccessException ex)
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

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> SendInvitationsStep(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var tender = await _ApiClient.GetAsync<InvitationStepModel>("Tender/GetByTenderIdInvitationDetails/" + tenderId, null);
            return View(tender);
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
      [Authorize(Roles = RoleNames.DataEntry)]
      public async Task<IActionResult> GetAllSuppliersBySearchCretriaForInvitationsAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Invitation/GetAllSuppliersBySearchCretriaForInvitations", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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
         if (tenderAttachments.Count() > 0)
         {
            tenderAttachments[0].TenderStatusId = model.TenderStatusId;
         }
         return tenderAttachments;
      }

      #endregion

      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> Delete(string TenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(TenderIdString);
            await _ApiClient.GetAsync<BasicTenderModel>("Tender/Delete/" + tenderId, null);
            return RedirectToAction(nameof(TenderIndexUnderOperationsStage));
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(TenderIndexUnderOperationsStage));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(TenderIndexUnderOperationsStage));
         }
      }

      [Authorize(RoleNames.DataEntryPolicy)]
      [HttpPost]
      public async Task<ActionResult> ConvertTenderInvitationToPublic(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ConvertTenderInvitationToPublic/" + tenderId, null, null);
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

      #region Extend Tender Dates

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> ExtendTenderDates(string tenderIdString)
      {
         try
         {
            TenderDatesModel response = await _ApiClient.GetAsync<TenderDatesModel>("Tender/GetTenderExtendDatesByTenderId/" + Util.Decrypt(tenderIdString), null);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> ExtendTenderDates(TenderDatesModel model)
      {
         if (model.LastOfferPresentationDate != null)
         {
            model.LastOfferPresentationDate += DateHelper.GetTimePart(model.LastOfferPresentationTime);
         }
         if (model.OffersOpeningDate != null)
         {
            model.OffersOpeningDate += DateHelper.GetTimePart(model.OffersOpeningTime);
         }
         if (model.OffersCheckingDate != null)
         {
            model.OffersCheckingDate += DateHelper.GetTimePart(model.OffersCheckingTime);
         }
         if (model.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || model.TenderTypeId == (int)Enums.TenderType.FirstStageTender || model.TenderTypeId == (int)Enums.TenderType.Competition)
         {
            ModelState.Remove("OffersOpeningDate");
            ModelState.Remove("OffersOpeningTime");
         }
         else
         {
            ModelState.Remove("OffersCheckingDate");
            ModelState.Remove("OffersCheckingTime");
         }

         ModelState.Remove("FinalGuaranteePercentage");
         ModelState.Remove("AwardingExpectedDate");
         ModelState.Remove("StartingBusinessOrServicesDate");
         ModelState.Remove("OffersOpeningAddressId");
         ModelState.Remove("DeliveryDate");
         ModelState.Remove("DeliveryTime");
         ModelState.Remove("BuildingName");
         ModelState.Remove("FloarNumber");
         ModelState.Remove("DepartmentName");
         ModelState.Remove("OfferDeliveryAddress");
         ModelState.Remove("OfferBuildingName");
         ModelState.Remove("OfferFloarNumber");
         ModelState.Remove("OfferDepartmentName");
         ModelState.Remove("OffersDeliveryDate");
         ModelState.Remove("OffersDeliveryTime");

         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            model = await _ApiClient.GetAsync<TenderDatesModel>("Tender/GetTenderExtendDatesByTenderId/" + Util.Decrypt(model.TenderIdString), null);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.Vacations = vacations;
            return View(model);
         }
         try
         {
            model.TenderId = Util.Decrypt(model.TenderIdString);
            await _ApiClient.PostAsync("Tender/UpdateTenderExtendDates", null, model);
            AddMessage(Resources.TenderResources.Messages.ExtendDatesSentSuccessfully);
            return RedirectToAction(nameof(TenderExtendDateApprovement), "Tender", new { tenderIdString = model.TenderIdString }, "d-2");
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            model = await _ApiClient.GetAsync<TenderDatesModel>("Tender/GetTenderExtendDatesByTenderId/" + Util.Decrypt(model.TenderIdString), null);
            List<VacationsDateModel> vacations = await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            model.Vacations = vacations;
            return View(model);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return View(model);
         }
      }
      #endregion

      #region Edit Areas-Committees-addresses of Tender 

      #region Edit Committees

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> EditCommittees(string id)
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
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<EditeCommitteesModel> GetTechincalAndOpenAndCheckCommittees()
      {
         EditeCommitteesModel committeesModel = new EditeCommitteesModel();
         //List<CommitteeModel> allCommittees = (await _cache.GetOrCreateAsync(CacheKeys.BasicStepCache + "_" + User.UserAgency(), async entry =>
         //{
         //   int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
         //   entry.SlidingExpiration = TimeSpan.FromHours(minutes);
         //   return await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetCommitteeByCommitteeTypeOnBranch", null);
         //})).ToList();

         List<CommitteeModel> allCommittees = await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetCommitteeByCommitteeTypeOnBranch", null);

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            committeesModel.BranchHasTechnicalCommittees = true;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            committeesModel.BranchHasTechnicalCommittees = false;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee))
         {
            committeesModel.OfferOpenningCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee).ToList();
         }
         else
         {
            committeesModel.OfferOpenningCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee))
         {
            committeesModel.OfferCheckingCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee).ToList();
         }
         else
         {
            committeesModel.OfferCheckingCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee).ToList();
         }
         committeesModel.IsAgancyHasTechnicalCommittee = committeesModel.TechnicalCommittees.Any() ? true : false;
         return committeesModel;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<EditeCommitteesModel> GetTechincalAndDirectPurchaseCommittees()
      {
         EditeCommitteesModel committeesModel = new EditeCommitteesModel();
         //List<CommitteeModel> allCommittees = (await _cache.GetOrCreateAsync(CacheKeys.PurchaseCommitteeCache + "_" + User.UserAgency(), async entry =>
         //{
         //   int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
         //   entry.SlidingExpiration = TimeSpan.FromHours(minutes);
         //   return await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetTechincalAndDirectPurchaseCommittees", null);
         //})).ToList();
         List<CommitteeModel> allCommittees = await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetTechincalAndDirectPurchaseCommittees", null);

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            committeesModel.BranchHasTechnicalCommittees = true;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            committeesModel.BranchHasTechnicalCommittees = false;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee))
         {
            committeesModel.DirectPurchaseCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee).ToList();
         }
         else
         {
            committeesModel.DirectPurchaseCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee).ToList();
         }
         committeesModel.IsAgancyHasTechnicalCommittee = committeesModel.TechnicalCommittees.Any() ? true : false;

         return committeesModel;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<CommitteeUserModel>> GetDirectPurchaseCommitteesMembers()
      {
         return await _ApiClient.GetAsync<List<CommitteeUserModel>>("Tender/GetDirectPurchaseCommitteesMembers", null);
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<EditeCommitteesModel> GetVROAndTechnicalCommittees()
      {
         EditeCommitteesModel committeesModel = new EditeCommitteesModel();
         //List<CommitteeModel> allCommittees = (await _cache.GetOrCreateAsync(CacheKeys.VROCommitteeCache + "_" + User.UserAgency(), async entry =>
         //{
         //   int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
         //   entry.SlidingExpiration = TimeSpan.FromHours(minutes);
         //   return await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetVROAndTechnicalCommittees", null);
         //})).ToList();

         List<CommitteeModel> allCommittees = await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetVROAndTechnicalCommittees", null);

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            committeesModel.BranchHasTechnicalCommittees = true;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            committeesModel.BranchHasTechnicalCommittees = false;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee))
         {
            committeesModel.VROCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
         }
         else
         {
            committeesModel.VROCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
         }
         committeesModel.IsAgancyHasTechnicalCommittee = committeesModel.TechnicalCommittees.Any() ? true : false;

         return committeesModel;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetSuccededPreQualifications()
      {
         List<LookupModel> lookups = await _ApiClient.GetListAsync<LookupModel>("Tender/GetSuccededPreQualifications", null);
         return lookups;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetFinishedAnnouncementHasOneSupplier(string tenderId)
      {
         List<LookupModel> lookups = await _ApiClient.GetListAsync<LookupModel>("Tender/GetFinishedAnnouncementHasOneSupplier/" + tenderId, null);
         return lookups;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetFinishedAnnouncementForLimitedTender(string tenderId)
      {
         List<LookupModel> lookups = await _ApiClient.GetListAsync<LookupModel>("Tender/GetFinishedAnnouncementForLimitedTender/" + tenderId, null);
         return lookups;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForLimitedTender(string tenderId, int selectedReasonId)
      {
         List<LookupModel> lookups = await _ApiClient.GetListAsync<LookupModel>("Tender/GetAnnouncementSupplierTemplateForLimitedTender/" + tenderId + "/" + selectedReasonId, null);
         return lookups;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetAnnouncementSupplierTemplateForDirectPurchaseTender()
      {
         List<LookupModel> lookups = await _ApiClient.GetListAsync<LookupModel>("Tender/GetAnnouncementSupplierTemplateForDirectPurchaseTender", null);
         return lookups;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<int> GetQuantityTableVersion()
      {
         var quantityTableVersionId = await _ApiClient.GetAsync<int>("Tender/GetQauntityTableVersionId", null);
         return quantityTableVersionId;
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<List<LookupModel>> GetTenderAgreementAgencies()
      {
         List<LookupModel> lookups = await _ApiClient.GetListAsync<LookupModel>("Tender/GetTenderAgreementAgenciesLookup", null);
         return lookups;
      }

      private async Task<EditeCommitteesModel> FillTenderCommitteesAsync(EditeCommitteesModel committeesModel)
      {
         List<CommitteeModel> allCommittees = (await _cache.GetOrCreateAsync(CacheKeys.BasicStepCache + "_" + User.UserAgency(), async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            return await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetCommitteeByCommitteeTypeOnBranch", null);
         })).ToList();

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            committeesModel.BranchHasTechnicalCommittees = true;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            committeesModel.BranchHasTechnicalCommittees = false;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee))
         {
            committeesModel.OfferOpenningCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee).ToList();
         }
         else
         {
            committeesModel.OfferOpenningCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee))
         {
            committeesModel.OfferCheckingCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee).ToList();
         }
         else
         {
            committeesModel.OfferCheckingCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee).ToList();
         }

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee))
         {
            committeesModel.VROCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
         }
         else
         {
            committeesModel.VROCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
         }

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee))
         {
            committeesModel.DirectPurchaseCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee).ToList();
         }
         else
         {
            committeesModel.DirectPurchaseCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee).ToList();
         }
         committeesModel.IsAgancyHasTechnicalCommittee = committeesModel.TechnicalCommittees.Count > 0 ? true : false;
         if (committeesModel.EstimatedValue <= (int)Enums.TenderBudget.LowBudget)
         {
            var agancyCode = allCommittees.Where(c => c.CommitteeId == (int)CommitteeType.PurchaseCommittee).Select(c => c.AgencyCode).FirstOrDefault();
            committeesModel.DirectPurchaseCommitteeMember = await _ApiClient.GetAsync<List<CommitteeUserModel>>("Tender/GetDirectPurchaseCommitteesMembers", null);
         }
         return committeesModel;
      }
      private async Task<EditeCommitteesModel> FillTenderCommitteesAsync(EditeCommitteesModel committeesModel, List<CommitteeModel> allCommittees)
      {
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
         {
            committeesModel.BranchHasTechnicalCommittees = true;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         else
         {
            committeesModel.BranchHasTechnicalCommittees = false;
            committeesModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee))
         {
            committeesModel.OfferOpenningCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee).ToList();
         }
         else
         {
            committeesModel.OfferOpenningCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.OpenOfferCommittee).ToList();
         }
         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee))
         {
            committeesModel.OfferCheckingCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee).ToList();
         }
         else
         {
            committeesModel.OfferCheckingCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.CheckOfferCommittee).ToList();
         }

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee))
         {
            committeesModel.VROCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
         }
         else
         {
            committeesModel.VROCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
         }

         if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee))
         {
            committeesModel.DirectPurchaseCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee).ToList();
         }
         else
         {
            committeesModel.DirectPurchaseCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.PurchaseCommittee).ToList();
         }
         committeesModel.IsAgancyHasTechnicalCommittee = committeesModel.TechnicalCommittees.Count > 0 ? true : false;
         return committeesModel;
      }
      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> EditCommittees(EditeCommitteesModel model)
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
            EditeCommitteesModel tenderCommitteeModel = await _ApiClient.PostAsync<EditeCommitteesModel>("Tender/EditCommittees", null, model);
            AddMessage(Resources.TenderResources.Messages.EditCommitteesSuccessfully);
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

      #region Edit Samples Delivery Address

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> EditSamplesDeliveryAddress(string id)
      {
         try
         {
            TenderSamplesAddressModel address = await _ApiClient.GetAsync<TenderSamplesAddressModel>("Tender/GetTenderSamplesAddress/" + Util.Decrypt(id).ToString(), null);
            address.TenderIdString = id;
            return View(address);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> EditSamplesDeliveryAddress(TenderSamplesAddressModel samplesAddressModel)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            samplesAddressModel = await _ApiClient.GetAsync<TenderSamplesAddressModel>("Tender/GetTenderSamplesAddress/" + Util.Decrypt(samplesAddressModel.TenderIdString).ToString(), null);
            return View(samplesAddressModel);
         }
         try
         {
            var tenderId = Util.Decrypt(samplesAddressModel.TenderIdString);
            TenderSamplesAddressModel tenderCommitteeModel = await _ApiClient.PostAsync<TenderSamplesAddressModel>("Tender/EditSamplesDeliveryAddress/" + tenderId + "/" + samplesAddressModel.SamplesDeliveryAddress, null, null);
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

      #region Edit Areas

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> EditAreas(string id)
      {
         try
         {
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> EditAreas(TenderAreasModel areasModel)
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

            List<CountryModel> countries = await _cache.GetOrCreateAsync(CacheKeys.GetCountries, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<CountryModel>("Lookup/GetCountries", null);
            });

            areasModel.Countries = countries;
            areasModel.Areas = areas;
            return View(areasModel);
         }
         try
         {
            areasModel.TenderId = Util.Decrypt(areasModel.TenderIdString);
            TenderAreasModel tenderAreasModel = await _ApiClient.PostAsync<TenderAreasModel>("Tender/EditAreas", null, areasModel);
            AddMessage(Resources.TenderResources.Messages.EditAreaSuccessfully);
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

      #endregion

      #region Tender Changes Requests

      //[HttpGet]
      //[Authorize(RoleNames.DataEntryPolicy)]
      //public async Task<ActionResult> QuantitiesTableUpdates(string id)
      //{
      //   try
      //   {
      //      var tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //      tables.TenderIdString = id;
      //      return View(tables);
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      var tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //      tables.TenderIdString = id;
      //      return View(tables);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //      return RedirectToAction(nameof(Index));
      //   }
      //}

      //[HttpGet]
      //[Authorize(RoleNames.DataEntryPolicy)]
      //public async Task<ActionResult> TenderQuantitiesTableUpdates(string id)
      //{
      //   try
      //   {
      //      var tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //      tables.TenderIdString = id;
      //      return View(tables);
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      var tables = await _ApiClient.GetAsync<QuantitiesTablesModel>("Tender/GetTenderQuantityTablesStepById/" + Util.Decrypt(id), null);
      //      tables.TenderIdString = id;
      //      return View(tables);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //      return RedirectToAction(nameof(Index));
      //   }
      //}

      //[HttpPost]
      //[Authorize(RoleNames.DataEntryPolicy)]
      //public async Task<ActionResult> QuantitiesTableUpdates(List<QuantityTableModel> model, int id)
      //{
      //   if (!ModelState.IsValid)
      //   {
      //      AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
      //      return View(model);
      //   }
      //   try
      //   {
      //      model[0].TenderId = id;

      //      await _ApiClient.PostAsync("Tender/SaveQuantitiesTablesUpdates", null, model);
      //      return Ok();
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      return JsonErrorMessage(ex.Message);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //}

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> TenderAttachmentsUpdates(string id)
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


      #endregion

      #region Display Tender Details
      //[AllowAnonymous]
      public async Task<ActionResult> Details(string STenderId)
      {
         try
         {
            if (User.IsInRole(RoleNames.supplier))
               return RedirectToAction(nameof(DetailsForSupplier), new { STenderId });
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer) || User.IsInRole(RoleNames.PurshaseSpecialist) || User.IsInRole(RoleNames.EtimadOfficer))
               return RedirectToAction(nameof(DetailsForUnderOperations), new { STenderId });
            if (User.IsInRole(RoleNames.UnitSpecialistLevel1) || User.IsInRole(RoleNames.UnitSpecialistLevel2) || User.IsInRole(RoleNames.UnitManagerUser) || User.IsInRole(RoleNames.UnitBusinessManagement))
               return RedirectToAction(nameof(TenderDetailsForUnitSecretary), new { tenderIdString = STenderId });
            if (User.UserRoles().Count > 0)
               return RedirectToAction(nameof(DetailsForAll), new { STenderId });
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsById/" + STenderId, null);
            tenderDetailsModel.SubscriptionTypeId = (int)SubscriptionType.Full;
            return View(tenderDetailsModel);
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
      [HttpGet]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> DetailsForSupplier(string STenderId)
      {
         try
         {
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsForSuppliers/" + STenderId, null);
            tenderDetailsModel.SubscriptionTypeId = (int)SubscriptionType.Full;
            tenderDetailsModel.HasNoTechnical = true;
            return View(tenderDetailsModel);
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
      [HttpGet]
      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> DetailsForUnderOperations(string STenderId)
      {
         try
         {
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsForUnit/" + STenderId, null);
            tenderDetailsModel.SubscriptionTypeId = (int)SubscriptionType.Full;
            List<VacationsDateModel> allVacation = await _cache.GetOrCreateAsync(CacheKeys.VacationCache, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
               entry.SlidingExpiration = TimeSpan.FromHours(minutes);
               return await _ApiClient.GetListAsync<VacationsDateModel>("Tender/FindAllVacationDates", null);
            });
            List<CommitteeModel> allCommittees = await _ApiClient.GetAsync<List<CommitteeModel>>("Tender/GetCommitteeByCommitteeTypeOnBranch", null);
            if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee))
            {
               tenderDetailsModel.BrancHasNoCommittees = false;
               tenderDetailsModel.TechnicalCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
            }
            else
            {
               tenderDetailsModel.BrancHasNoCommittees = true;
               tenderDetailsModel.TechnicalCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.TechincalCommittee).ToList();
            }
            if (allCommittees.Any(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee))
            {
               tenderDetailsModel.OpenCheckCommittees = allCommittees.Where(c => c.BranchIds.Contains(User.UserBranch()) && c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
            }
            else
            {
               tenderDetailsModel.OpenCheckCommittees = allCommittees.Where(c => c.CommitteeTypeId == (int)CommitteeType.VROCommittee).ToList();
            }
            if (tenderDetailsModel.TechnicalCommittees == null || tenderDetailsModel.TechnicalCommittees.Count() == 0)
            {
               tenderDetailsModel.HasNoTechnical = true;
            }
            tenderDetailsModel.Vacations = allVacation;
            return View(tenderDetailsModel);
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

      [HttpGet]
      [Authorize]
      public async Task<ActionResult> DetailsForAll(string STenderId)
      {
         try
         {
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsById/" + STenderId, null);
            tenderDetailsModel.SubscriptionTypeId = (int)SubscriptionType.Full;
            return View(tenderDetailsModel);
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
      [HttpGet]
      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<ActionResult> DetailsForUnit(string STenderId)
      {
         try
         {
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsById/" + STenderId, null);
            tenderDetailsModel.SubscriptionTypeId = (int)SubscriptionType.Full;
            return View(tenderDetailsModel);
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
      [AllowAnonymous]
      public async Task<ActionResult> DetailsForVisitor(string STenderId)
      {
         try
         {
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsForVisitor/" + STenderId, null);
            return View(tenderDetailsModel);
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

      #endregion

      #region Tender Details 

      [HttpGet]
      public async Task<ActionResult> GetTenderHistoryByUserIdAndTenderIdAndStatusId(int tenderIdString, int tenderStatusId)
      {
         try
         {
            var tenderHistory = await _ApiClient.GetAsync<TenderHistoryModel>("Tender/GetTenderHistoryByUserIdAndTenderIdAndStatusId/" + tenderIdString + "/" + tenderStatusId, null);
            return View(tenderHistory);
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
         catch (UnHandledAccessException ex)
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

      #endregion

      #region Update Tender Status

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> ReopenTenderCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenTenderChecking/" + tenderId, null, null);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> ReopenTenderAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Tender/ReopenTender/" + tenderId, null, null);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> SendTenderToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendTenderToApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
      public async Task<ActionResult> SendTenderToApproveOppenningAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendTenderToApproveOppenning/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SendTenderToApproveCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendTenderToApproveChecking/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> ReopenTenderAwardingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenTenderAwarding/" + tenderId, null, null);
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

      [Authorize(RoleNames.AuditerPolicy)]
      [HttpPost]
      public async Task<ActionResult> RejectTenderAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectTender", null, rejectionModel);
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
      public async Task<ActionResult> ApproveTenderCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderChecking/" + tenderId, null, null);
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
      [Authorize(RoleNames.ApproveTenderAwardPolicy)]
      public async Task<ActionResult> ApproveTenderAwardingWithVerificationAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderAwardingWithVerification/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.OffersOppeningManagerPolicy)]
      public async Task<ActionResult> ApproveTenderOppeningWithVerificationAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderOppening/" + tenderId + "/" + verificationCode, null, null);
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
      public async Task<ActionResult> ApproveTenderCheckingWithVerificationAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderCheckingWithVerification/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.AuditerPolicy)]
      public async Task<ActionResult> ApproveTenderWithInbudgetAsync(string tenderIdString, string verificationCode, bool iagree, bool competitionIsBudgeted)
      {
         try
         {
            //if (estimatedValue <= 0)
            //   throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.InvalidOfferValue);
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderWithInbudget/" + tenderId + "/" + verificationCode + "/" + iagree + "/" + competitionIsBudgeted, null, null);
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
      [Authorize(RoleNames.AuditerPolicy)]
      public async Task<ActionResult> ApproveTenderRelatedWithVROAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderRelatedWithVRO/" + tenderId, null, null);
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
      [Authorize(RoleNames.AuditerPolicy)]
      public async Task<ActionResult> ApproveTenderVROAsync(ApproveVROModel approveVROModel)
      {
         try
         {
            approveVROModel.OffersOpeningDate += DateHelper.GetTimePart(approveVROModel.OffersOpeningTime);
            approveVROModel.LastOfferPresentationDate += DateHelper.GetTimePart(approveVROModel.LastOfferPresentationTime);
            int tenderId = Util.Decrypt(approveVROModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderVRO", null, approveVROModel);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> SendUpdateQuantityTableToApproveAsync(int tenderId)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendUpdateQuantityTableToApprove/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.DataEntry + "," + RoleNames.PurshaseSpecialist)]
      public async Task<ActionResult> SendUpdateAttachmentsToApproveAsync(int tenderId)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendUpdateAttachmentsToApprove/" + tenderId + "/" + string.Empty, null, null);
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
      //[Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary)]

      public async Task<ActionResult> SendUpdateDatesToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendUpdateDatesToApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.AuditerPolicy)]
      public async Task<ActionResult> ApproveTenderUpdateWithVerificationAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Tender/ApproveTenderUpdateWithVerification/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> ApproveTenderExtendDatesChangeRequestAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderExtendDatesChangeRequest/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.AuditerPolicy)]
      public async Task<ActionResult> ApproveTenderQuantityTablesChangeRequestAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderQuantityTablesChangeRequest/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]

      public async Task<ActionResult> ApproveTenderAttachmentsChangeRequestAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderAttachmentsChangeRequest/" + tenderId + "/" + verificationCode, null, null);
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
      public async Task<string> ConvertNumberToText(Decimal estimatedValue)
      {
         ConvertNumberToText obj = new ConvertNumberToText(estimatedValue, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
         return obj.ConvertToArabic();
      }

      [HttpPost]
      [Authorize(RoleNames.AuditerPolicy)]
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
      //[Authorize(RoleNames.QualificationExtendDateApprovementPolicy)]
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]

      public async Task<ActionResult> RejectQualificationExtendDate(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync<BasicTenderModel>("Tender/RejectQualificationExtendDate", null, rejectionModel);
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
      [Authorize(RoleNames.AuditerPolicy)]
      public async Task<ActionResult> RejectTenderUpdateQuantityTableAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectTenderUpdateQuantityTable", null, rejectionModel);
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
      [Authorize(Roles = RoleNames.Auditer + "," + RoleNames.EtimadOfficer + "," + RoleNames.PreQualificationCommitteeManager)]
      public async Task<ActionResult> RejectTenderUpdateAttachmentAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectTenderUpdateAttachment", null, rejectionModel);
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
      //[Authorize(RoleNames.SupplierExtendOfferDatesPolicy)]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PurshaseSpecialist)]

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

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> CancelQuantityTableUpdateAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/CancelQuantityTableUpdate/" + tenderId, null, null);
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
      //[Authorize(RoleNames.DataEntryPolicy)]
      [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PurshaseSpecialist)]

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

      #region Cancel Tender

      [Authorize(RoleNames.CancelTenderPolicy)]
      public async Task<ActionResult> CancelTender(string STenderId)
      {
         try
         {
            int tenderId = Util.Decrypt(STenderId);
            TenderCanelationModel tenderCanelationModel = await _ApiClient.GetAsync<TenderCanelationModel>("Tender/GetTenderDataForCanelation/" + tenderId, null);
            tenderCanelationModel.CancelationReasons = await _cache.GetOrCreateAsync(CacheKeys.CancelationReasons, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllCancelationReasons", null);
            });
            return View(tenderCanelationModel);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAllBuyerSuppliers(string tenderIdString)
      {
         var areaList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllBuyerSuppliers/" + Util.Decrypt(tenderIdString), null);
         return areaList;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAllQuantityTableRowType()
      {
         var rowTypesList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllQuantityTableRowTypes", null);
         return rowTypesList;
      }

      //[HttpGet]
      //public async Task<List<LookupModel>> GetAllSuppliersHaveOffers(string tenderIdString)
      //{
      //   var areaList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllSuppliersHaveOffers/" + Util.Decrypt(tenderIdString), null);
      //   return areaList;
      //}

      //[Authorize(RoleNames.CancelTenderPolicy)]
      //public async Task<ActionResult> CancelTender(string STenderId)
      //{
      //   try
      //   {
      //      int tenderId = Util.Decrypt(STenderId);
      //      ViewBag.TenderId = tenderId;
      //      ViewBag.TenderIdString = STenderId;

      //      // this flag help manage reopen tender view
      //      ViewBag.hasHigherAuthority = false;
      //      if (User.IsInRole(RoleNames.Auditer) || User.IsInRole(RoleNames.OffersOppeningManager) || User.IsInRole(RoleNames.OffersCheckManager))
      //      {
      //         ViewBag.hasHigherAuthority = true;
      //      }
      //      // check if user can view and audit cancel request
      //      ViewBag.hasTenderCancelRequest = false;
      //      ViewBag.canAuditThisRequest = false;
      //      ViewBag.hasActiveTenderCancelRequest = false;
      //      var tenderActiveRevisionCancel = await _ApiClient.GetAsync<TenderChangeRequestModel>("Tender/GetTenderActiveCancelRequestsByTenderId/" + tenderId, null);
      //      ViewBag.hasRejectTenderCancelRequest = false;
      //      if (tenderActiveRevisionCancel != null)
      //      {
      //         if (User.IsInRole(RoleHelper.GetHigherAuthorityByRoleName(tenderActiveRevisionCancel.RequestedByRoleName)) && tenderActiveRevisionCancel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Pending)
      //         {
      //            ViewBag.canAuditThisRequest = true;
      //         }
      //         if (User.IsInRole(tenderActiveRevisionCancel.RequestedByRoleName))
      //         {
      //            ViewBag.CanReopenTender = true;
      //         }
      //         else
      //         {
      //            if (User.IsInRole(RoleNames.OffersOppeningSecretary) || User.IsInRole(RoleNames.OffersCheckSecretary))
      //            {

      //            }
      //         }
      //         ViewBag.tenderRevisionCancel = tenderActiveRevisionCancel;
      //         if (tenderActiveRevisionCancel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && tenderActiveRevisionCancel.RequestedByRoleName == User.UserRole())
      //         {
      //            ViewBag.hasRejectTenderCancelRequest = true;
      //         }
      //         else if (tenderActiveRevisionCancel.ChangeRequestStatusId == (int)Enums.ChangeStatusType.Rejected && tenderActiveRevisionCancel.RequestedByRoleName == User.UserRole())
      //         {
      //            ViewBag.hasActiveTenderCancelRequest = true;
      //         }
      //         else
      //            ViewBag.hasActiveTenderCancelRequest = false;
      //      }
      //      return View();
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      return NotFound();
      //   }
      //   catch (AuthorizationException ex)
      //   { throw ex; }
      //   catch
      //   {
      //      AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); return NotFound();
      //   }

      //}


      [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
      [HttpPost]
      public async Task<ActionResult> CreateCancelTenderRequestAsync(TenderRevisionCancelModel tenderRevisionCancelModel)
      {
         try
         {
            var tenderRevisionCancel = await _ApiClient.PostAsync<TenderChangeRequestModel>("Tender/CreateTenderCancelRequest", null, tenderRevisionCancelModel);
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
      public async Task<ActionResult> ApproveTenderCancelRequestAsync(TenderCancelModel cancelModel)// string tenderIdString, string verificationCode)
      {
         try
         {
            var tenderRevisionCancel = await _ApiClient.PostAsync<bool>("Tender/ApproveTenderCancelRequestAsync", null, cancelModel);// new TenderRevisionCancelModel { TenderIdString = tenderRevisionCancelModel.TenderIdString, CancelationReasonId = tenderRevisionCancelModel.CancelationReasonId, SupplierViolatorCRs = tenderRevisionCancelModel.SupplierViolatorCRs });
            //await _ApiClient.PostAsync<bool>("Tender/ApproveTenderCancelRequestAsync/" + tenderId + "/" + verificationCode, null, null);
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
            return Unauthorized();
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
      public async Task<ActionResult> RejectQualificationCancelRequestAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync<bool>("Tender/RejectQualificationCancelRequestAsync", null, rejectionModel);

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

      [HttpPost]
      [Authorize(RoleNames.CreateCancelTenderRequestPolicy)]
      public async Task<ActionResult> CancelTenderCancellationRequestAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/CancelTenderCancellationRequest/" + tenderId, null, null);
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

      #endregion

      #region Tender Offers Actions (Open)

      public async Task<ActionResult> OpenTenderOffers(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var tenderOffersModel = await _ApiClient.GetAsync<TenderOffersModel>("Tender/GetTenderOfferDetailsForOppeningStage/" + tenderId, null);
            return View(tenderOffersModel);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index));

         }
         catch (UnHandledAccessException ex)
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

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<ActionResult> PurshaseTender(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var tender = await _ApiClient.PostAsync<PurchaseModel>("Tender/PurshaseTender", null, tenderId);
            return Json(tender);
         }
         catch (BusinessRuleException ex)
         {
            //_logger.LogError(ex.Message + "\n" + ex.StackTrace);
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);

         }
      }

      [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
      public async Task<ActionResult> OpenTenderOffersPagingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
      {
         try
         {
            //int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<TenderOpenOfferModel>("Tender/GetOffersForOpenByTenderIdAsync/", tenderBasicSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [HttpPost]
      [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
      public async Task<ActionResult> OpenTenderOffersAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/OpenTenderOffer/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
      public async Task<ActionResult> ReopenTenderOffersAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenTenderOffer/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersOppeningManagerPolicy)]
      public async Task<ActionResult> RejectOpenTenderOffersAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectOpenTenderOffers", null, rejectionModel);
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

      #endregion Tender Offers Actions (Open)

      #region Tender Offers Actions (Check) 

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      public async Task<ActionResult> CheckTenderOffers(string tenderIdString)
      {
         TenderDetailsCheckingStageModel tender;
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            tender = await _ApiClient.GetAsync<TenderDetailsCheckingStageModel>("Tender/GetTenderOfferDetailsByTenderIdForCheckStage/" + tenderId, null);
            return View(tender);
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

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      public async Task<ActionResult> CheckTenderOffersPagingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckOfferModel>("Tender/GetOffersForCheckByTenderIdAsync", tenderBasicSearchCriteria.ToDictionary());
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

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> CheckVROTenderOffersPagingAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
      {
         try
         {

            //string tenderIdString
            int tenderId = Util.Decrypt(tenderBasicSearchCriteria.TenderIdString);
            tenderBasicSearchCriteria.TenderId = tenderId;
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckOfferModel>("Tender/GetVROOffersForCheckByTenderIdAsync", tenderBasicSearchCriteria.ToDictionary());
            //     var result = await _ApiClient.GetQueryResultAsync<TenderCheckOfferModel>("Tender/GetOffersForCheckByTenderIdAsync", tenderBasicSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items.OrderBy(o => o.OfferPrice), result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      public async Task<ActionResult> CheckTenderOffersForDirectPurchasePagingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);

            var result = await _ApiClient.GetQueryResultAsync<TenderCheckOfferModel>("Tender/CheckTenderOffersForDirectPurchasePagingAsync/ " + tenderId, null);
            return Json(new PaginationModel(result.Items.OrderBy(o => o.OfferPrice), result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> GetOffersForAwardingStageByTenderIdAsync(TenderBasicSearchCriteria tenderBasicSearchCriteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckOfferModel>("Tender/GetOffersForAwardingStageByTenderId", tenderBasicSearchCriteria.ToDictionary());
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

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> StartTenderCheckingOffersAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/StartTenderCheckingOffers/" + tenderId, null, null);
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
      public async Task<ActionResult> RejectCheckTenderOffersReportAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectCheckTenderOffersReport", null, rejectionModel);
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

      #endregion Tender Offers Actions (Check)

      #region Technical stage

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      public async Task<ActionResult> SendTenderToApproveTechnicalCheckingAsync([FromForm] string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendTenderToApproveTechnicalCheckingAsync/" + tenderId, null, null);
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
      [Authorize(Roles = RoleNames.OffersCheckManager)]
      public async Task<ActionResult> ApproveTendeTechnicalCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTendeTechnicalCheckingAsync/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(Roles = RoleNames.OffersCheckManager)]
      public async Task<ActionResult> RejectTendeTechnicalCheckingAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectTendeTechnicalCheckingAsync", null, rejectionModel);
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
      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      public async Task<ActionResult> ReOpenTendeTechnicalCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);

            await _ApiClient.PostAsync("Tender/ReOpenTendeTechnicalCheckingAsync/" + tenderId, null, null);

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

      #endregion Technical Stage

      #region Financail Stage


      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost]
      public async Task<ActionResult> MoveTenderToFinancialOffersChecking(string tenderIdString)
      {
         await _ApiClient.PostAsync("Tender/MoveTenderToFinancialOffersChecking/" + tenderIdString, null, null);
         return Ok();
      }


      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost]
      public async Task<ActionResult> SendTenderToApproveFinancailCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendTenderToApproveFinancailCheckingAsync/" + tenderId, null, null);
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

      [Authorize(RoleNames.EndOpenFinantialOffersStagePolicy)]
      [HttpPost]
      public async Task<ActionResult> EndOpenFinantialOffersStage(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/EndOpenFinantialOffersStage/" + tenderId, null, null);
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

      [Authorize(Roles = RoleNames.OffersCheckManager)]
      [HttpPost]
      public async Task<ActionResult> ApproveTendeFinancialCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTendeFinancialCheckingAsync/" + tenderId + "/" + verificationCode, null, null);
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

      [Authorize(Roles = RoleNames.OffersCheckManager)]
      [HttpPost]
      public async Task<ActionResult> RejectTendeFinancialCheckingAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectTendeFinancialCheckingAsync", null, rejectionModel);

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

      [Authorize(RoleNames.OffersOppeningManagerPolicy)]
      [HttpPost]
      public async Task<ActionResult> ApproveTendeFinancialOpeningAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTendeFinancialOpeningAsync/" + tenderId + "/" + verificationCode, null, null);
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

      [Authorize(RoleNames.OffersOppeningManagerPolicy)]
      [HttpPost]
      public async Task<ActionResult> RejectTendeFinancialOpeningAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectTendeFinancialOpeningAsync", null, rejectionModel);

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
      [Authorize(RoleNames.ReOpenTendeFinancialCheckingPolicy)]
      public async Task<ActionResult> ReOpenTendeFinancialCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);

            await _ApiClient.PostAsync("Tender/ReOpenTendeFinancialCheckingAsync/" + tenderId, null, null);

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
      [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
      public async Task<ActionResult> ReOpenTenderFinancialOpeningAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReOpenTenderFinancialOpeningAsync/" + tenderId, null, null);
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

      #endregion Technical Stage

      #region Tender Offers Actions (Award)

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> AwardTenderOffers(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var tender = await _ApiClient.GetAsync<TenderOffersModel>("Tender/GetTenderOffersDetailsForOpenAwarding/" + tenderId, null);
            return View(tender);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> AwardTenderOffersAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/AwardTenderOffers/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SendAwardTenderToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendAwardTenderToApprove/" + tenderId, null, null);
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
      public async Task<ActionResult> RejectAwardTenderOffersReportAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectAwardTenderOffersReport", null, rejectionModel);
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

      #endregion Tender Offers Actions (Award)

      #region Tender Awarding

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> TenderAwarding(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var tender = await _ApiClient.GetAsync<TenderOffersModel>("Tender/GetTenderOfferDetailsByTenderIdForAwarding/" + tenderId, null);
            return View(tender);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> SendAwardTenderToInitialApprove(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendAwardTenderToInitialApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> IsTenderHasPendingRequestsOrNoExeclusionReasons(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/IsTenderHasPendingRequestsOrNoExeclusionReasons/" + Util.Decrypt(tenderIdString), null, null);
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
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> IsSupplierPassedForTenderAwarding(string tenderIdString, string cr)
      {
         try
         {
            var result = await _ApiClient.GetAsync<int>("Tender/IsSupplierPassedForTenderAwarding/" + Util.Decrypt(tenderIdString) + '/' + cr, null);
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
      [HttpGet]
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> RemoveOfferAwardingValue(int offerId)
      {
         try
         {
            var result = await _ApiClient.GetAsync<bool>("Tender/RemoveOfferAwardingValue/" + offerId, null);
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

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> CheckSendTenderToApproveAwarding(SendToAwardingModel sendModel)
      {
         try
         {
            var result = await _ApiClient.PostAsync("Tender/CheckSendTenderToApproveAwarding", null, sendModel);

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

      [HttpPost]

      public async Task<ActionResult> EmarketPlace(List<int> offerIds)
      {
         try
         {
            var result = await _ApiClient.PostAsync("Tender/EmarketPlace", null, offerIds);

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

      [HttpPost]
      [Authorize(RoleNames.AwardingInitialApprovalPolicy)]
      public async Task<ActionResult> ApproveTenderInitialAwarding(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveTenderInitialAwarding/" + tenderId + "/" + verificationCode, null, null);
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

      #region Supplier Favourite Tenders

      [Authorize(RoleNames.SupplierPolicy)]
      public ActionResult SupplierFavouriteTenders()
      {
         ViewBag.SupplierSubscriptionType = (int)SubscriptionType.Full;
         return View();
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> GetSupplierFavouritTendersListAsync(TenderSearchCriteria tenderSearchCriteria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Tender/GetSupplierFavouritTendersList", tenderSearchCriteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteria.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }


      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> AddTenderToSupplierTendersFavouriteList(string TenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(TenderIdString);
            var result = await _ApiClient.GetAsync<bool>("Tender/AddTenderToSupplierTendersFavouriteList/" + tenderId, null);
            return Json(true);
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

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpPost]
      public async Task<ActionResult> DeleteTenderFromSupplierTendersFavouriteList(string TenderIdString)
      {
         try
         {

            int tenderId = Util.Decrypt(TenderIdString);
            await _ApiClient.PostAsync("Tender/DeleteTenderFromSupplierTendersFavouriteList", null, tenderId);
            return Json(true);
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

      #endregion

      #region Tenders Suppliers List

      [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
      public async Task<ActionResult> FavouriteSuppliersListAsync()
      {
         return View();
      }

      [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetActiveAllSuppliersAsync(SupplierSearchCretriaModel cretria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Tender/GetAllSuppliers", cretria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [Authorize(Roles = RoleNames.GetFavouriteSuppliersByListIdPolicy + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.CustomerService + "," + RoleNames.OffersOppeningManager + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.DataEntry + "," + RoleNames.PurshaseSpecialist + "," + RoleNames.EtimadOfficer)]
      [HttpGet]
      public async Task<ActionResult> GetFavouriteLists(FavouriteSupplierListModel cretria)
      {
         try
         {
            var result = await _ApiClient.GetListAsync<FavouriteSupplierListModel>("Tender/GetAgencyBranchFavouriteLists", cretria.ToDictionary());
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }
      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      [HttpGet]
      public async Task<ActionResult> NotificationStatusReport(string tenderId, string tenderName)
      {


         ViewBag.tenderId = tenderId;
         ViewBag.tenderName = tenderName;

         return View();

      }

      [Authorize(Roles = RoleNames.MonafasatAdmin)]
      [HttpGet]
      public async Task<ActionResult> NotificationStatusReportPagingAsync(TenderNotificationSearchCriteria cretria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderNotificationStatus>("Tender/NotificationStatusReport", cretria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }

      [HttpGet]
      public async Task<ActionResult> GetAllOperationCodes()
      {
         try
         {
            var result = await _ApiClient.GetListAsync<OperationCodeModel>("Account/GetOperationCode", null);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }


      [HttpGet]
      public async Task<ActionResult> GetAllNotificationStatus()
      {
         try
         {
            var result = await _ApiClient.GetListAsync<NotificationStatusModel>("Account/GetNotificationStatus", null);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }




      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> AddFavouriteSupplierList(FavouriteSupplierListModel cretria)
      {
         try
         {
            var result = await _ApiClient.PostAsync<FavouriteSupplierListModel>("Tender/AddFavouriteSupplierList", null, cretria);
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         { AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); }
         return Ok("error");
      }

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> DeleteFavouriteSupplierList(FavouriteSupplierListModel cretria)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/DeleteFavouriteSupplierList", cretria.ToDictionary());
            return Json(true);
         }
         catch (BusinessRuleException ex)
         { AddError(ex.Message); }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         { AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); }
         return Ok();
      }

      [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetAllFavouriteSuppliers(SupplierSearchCretriaModel cretria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Tender/GetFavouriteSuppliersByListId", cretria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
            // return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [Authorize(RoleNames.GetFavouriteSuppliersByListIdPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetFavouriteSuppliersByListId(SupplierSearchCretriaModel cretria)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<SupplierInvitationModel>("Tender/GetFavouriteSuppliersByListId", cretria.ToDictionary());
            return Json(result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }


      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      [HttpPost]
      public async Task<ActionResult> AddSupplierToFavouriteSuppliersListAsync(string CommericalRegisterNo, string CommericalRegisterName, int FavouriteSupplierListId, int OldFavouriteSupplierListId, string IsTransfer)
      {

         try
         {
            var cretria = new SupplierSearchCretriaModel
            {
               CommericalRegisterNo = CommericalRegisterNo,
               CommericalRegisterName = CommericalRegisterName,
               FavouriteSupplierListId = FavouriteSupplierListId,
               AgencyCode = User.UserAgency()
            };
            var result = await _ApiClient.PostAsync<bool>("Tender/AddSupplierToFavouriteSuppliersListAsync", null, cretria);

            if (result)
            {
               if (IsTransfer != null)
               {
                  var deleteCretria = new SupplierSearchCretriaModel
                  {
                     CommericalRegisterNo = CommericalRegisterNo,
                     CommericalRegisterName = CommericalRegisterName,
                     FavouriteSupplierListId = OldFavouriteSupplierListId
                  };
                  await _ApiClient.GetStringAsync("Tender/DeleteSupplierFromFavouriteList", deleteCretria.ToDictionary());
               }
            }

            return Json(new { status = "success", message = "تم الإضافة إلي القائمة بنجاح" });
         }
         catch (BusinessRuleException ex)
         {
            return Json(new { status = "error", message = ex.Message });

         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
      }

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> DeleteSupplierFromFavouriteList(SupplierSearchCretriaModel cretria)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/DeleteSupplierFromFavouriteList", cretria.ToDictionary());
            return Json(true);
         }
         catch (BusinessRuleException ex)
         { AddError(ex.Message); }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         { AddError(Resources.TenderResources.ErrorMessages.UnexpectedError); }
         return Ok();
      }

      #endregion


      [HttpGet]
      [Authorize(RoleNames.MonafasatAdminPolicy)]
      public async Task<ActionResult> GetTenderMovementsByTenderId(SimpleTenderSearchCriteria criteria)
      {
         var result = await _ApiClient.GetQueryResultAsync<TenderMovement>("Tender/GetTenderMovementsByTenderId", criteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, (int)PageSize.twenty, result.PageNumber, null));
      }

      [AllowAnonymous]
      public async Task<ActionResult> OpenTenderDetailsReportForVisitor(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<TenderDetialsReportModel>("Tender/OpenTenderDetailsReportForVisitor/" + tenderId, null);
            return PartialView("_OpenTenderDetailsReport", result);
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
      public async Task<ActionResult> OpenTenderDetailsReport(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<TenderDetialsReportModel>("Tender/OpenTenderDetailsReport/" + tenderId, null);
            return PartialView("_OpenTenderDetailsReport", result);
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

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> OpenTenderAdvDetailsReport(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<TenderDetialsReportModel>("Tender/OpenTenderAdvDetailsReport/" + tenderId, null);
            return PartialView("_OpenTenderAdvDetailsReport", result);
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

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SendTenderToApproveCheckingWithNewBiddingRound(BiddingDateTimeViewModel biddingDateTimeViewModel)
      {
         try
         {
            biddingDateTimeViewModel.BiddingStartDateTime = biddingDateTimeViewModel.StartTime != null ? biddingDateTimeViewModel.BiddingDate + DateHelper.GetTimePart(biddingDateTimeViewModel.StartTime) : DateTime.Now;
            biddingDateTimeViewModel.BiddingEndDateTime = biddingDateTimeViewModel.StartTime != null ? biddingDateTimeViewModel.BiddingDate + DateHelper.GetTimePart(biddingDateTimeViewModel.EndTime) : DateTime.Now;
            biddingDateTimeViewModel.BiddingDate = biddingDateTimeViewModel.StartTime != null ? biddingDateTimeViewModel.BiddingDate + DateHelper.GetTimePart(biddingDateTimeViewModel.StartTime) : DateTime.Now;
            biddingDateTimeViewModel.StartTime = biddingDateTimeViewModel.StartTime ?? "";
            biddingDateTimeViewModel.EndTime = biddingDateTimeViewModel.EndTime ?? "";
            await _ApiClient.PostAsync("Tender/SendTenderToApproveCheckingWithNewBiddingRound", null, biddingDateTimeViewModel);
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
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SaveCheckingDateTime(CheckingDateTimeViewModel checkingDateTimeViewModel)
      {
         try
         {
            if (checkingDateTimeViewModel.CheckingDateTime < DateTime.Now)
            {
               throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CheckingDateTimeLessThanNow);
            }
            await _ApiClient.PostAsync("Tender/SaveCheckingDateTime", null, checkingDateTimeViewModel);
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
      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      public async Task<ActionResult> SaveFinancialCheckingDateTime(CheckingDateTimeViewModel financialCheckingDateTimeViewModel)
      {
         try
         {
            if (financialCheckingDateTimeViewModel.CheckingDateTime < DateTime.Now)
            {
               throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CheckingDateTimeLessThanNow);
            }
            await _ApiClient.PostAsync("Tender/SaveFinancialCheckingDateTime", null, financialCheckingDateTimeViewModel);
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

      [Authorize(RoleNames.AwardingReportPolicy)]
      public async Task<ActionResult> AwardingReport(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<AwardingReportModel>("Tender/AwardingReport/" + tenderId, null);
            var baseUrl = _rootConfiguration.AuthorityConfiguration.AuthorityURL + "/";
            ViewBag.AgencyLogo = /*_configuration.GetSection("MonafasatURL:MonafasatURL").Value.ToString() + "/" +*/ User.AgencyLogo(baseUrl);
            return PartialView("_AwardingReport", result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }
      [Authorize(RoleNames.AwardingReportPolicy)]
      public async Task<ActionResult> SupplierAwardingReport(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<AwardingReportModel>("Tender/AwardingReport/" + tenderId, null);
            return PartialView("_SupplierAwardingReport", result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }

      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> PrintTenderReceiptReport(string tenderIdString, int SupplierId)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<AwardingReportModel>("Tender/PrintTenderReceiptReport?tenderId=" + tenderId + "&SupplierId=" + SupplierId, null);
            return PartialView("_PrintTenderReceiptReport", result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [HttpGet]
      [Authorize(RoleNames.OffersReportPolicy)]
      public async Task<ActionResult> OffersReport(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<OfferReportModel>("Tender/OffersReport/" + tenderId, null);

            return PartialView("_OffersReport", result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }

      [HttpGet]
      [Authorize(RoleNames.CountAndCloseAppliedOffersPolicy)]
      public async Task<ActionResult> CountAndCloseAppliedOffers(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetAsync<CountAndCloseAppliedOffersModel>("Tender/CountAndCloseAppliedOffers/" + tenderId, null);
            return PartialView("_CountAndCloseAppliedOffers", result);
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

      #region revisions

      [Authorize(RoleNames.TenderRevisionsPolicy)]
      public async Task<ActionResult> Revisions()
      {
         return View();
      }

      [Authorize(RoleNames.TenderRevisionsPolicy)]
      [HttpGet]
      public async Task<ActionResult> RevisionDatesPagingAsync(TenderRevisionSearchCriteria criteria)
      {
         try
         {
            List<int> tenderStatusIds = new List<int>();
            await FillDatesAsync(criteria);
            var result = await _ApiClient.GetQueryResultAsync<TenderExtendDateModel>("Tender/GetTenderRevisionDateBySearchCriteria", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }
      private async Task FillDatesAsync(TenderRevisionSearchCriteria criteria)
      {
         criteria.RequestDateFrom = criteria.RequestDateFromS;
         criteria.RequestDateTo = criteria.RequestDateToS;
         criteria.ResponseDateFrom = criteria.ResponseDateFromS;
         criteria.ResponseDateTo = criteria.ResponseDateToS;
      }

      [Authorize(RoleNames.TenderRevisionsPolicy)]
      [HttpGet]
      public async Task<ActionResult> CancelRequestsPagingAsync(TenderRevisionSearchCriteria criteria)
      {
         try
         {
            List<int> tenderStatusIds = new List<int>();
            await FillDatesAsync(criteria);
            var result = await _ApiClient.GetQueryResultAsync<TenderRevisionCancelModel>("Tender/GetTenderRevisionCancelBySearchCriteria", criteria.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
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
         catch
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      #endregion

      #region Subscription

      [HttpGet]
      [Authorize(RoleNames.GetRelatedTendersByActivitiesPolicy)]
      public async Task<ActionResult> GetRelatedTendersByActivities(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetListAsync<BasicTenderModel>("Tender/GetRelatedTendersByActivities/" + tenderId, null);
            return PartialView("_CountAndCloseAppliedOffers", result);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }

      }

      #endregion

      #region Lookups

      [HttpGet]
      public async Task<List<GovAgencyModel>> GetAllAgenciesAsync()
      {
         var result = await _ApiClient.GetListAsync<GovAgencyModel>("Lookup/GetALlAgencies/", null);
         return result;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAllBranchesByAgencyCode()
      {
         string agency = string.Empty;
         if (!(User.IsInRole(RoleNames.MonafasatAccountManager) || User.IsInRole(RoleNames.CustomerService)))
            agency = User.UserAgency();
         List<LookupModel> result;
         if (string.IsNullOrEmpty(agency))
            result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllBranches", null);
         else
            result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllBranchesByAgencyCode/" + agency, null);
         return result;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAllBranchesNotAssignedToCommittee(int committeeId, int committeeType)
      {
         var result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllBranchesNotAssignedToCommittee/" + committeeId + "/" + committeeType, null);
         return result;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetStatusAsync()
      {
         var statusList = await _ApiClient.GetListAsync<LookupModel>("Tender/GetStatus", null);
         return statusList;
      }
      [HttpGet]
      public async Task<List<LookupModel>> GetApprovedStatuses()
      {
         var statusList = await _ApiClient.GetListAsync<LookupModel>("Tender/GetApprovedStatuses", null);
         return statusList;
      }

      [HttpGet]
      public async Task<List<LookupModel>> GetAreasAsync()
      {
         //var areaList = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
         //return areaList;
         List<LookupModel> areaList = await _cache.GetOrCreateAsync(CacheKeys.GetAreas, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAreas", null);
         });
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
         var dataEntriesList = await _ApiClient.GetListAsync<UserLookupModel>("Lookup/GetAllDataEntryUsers", null);
         return dataEntriesList;
      }

      [HttpGet]
      public async Task<List<UserLookupModel>> GetAllAuditorUsersAsync()
      {
         var auditorsList = await _ApiClient.GetListAsync<UserLookupModel>("Lookup/GetAllAuditorUsers", null);
         return auditorsList;
      }

      //[AllowAnonymous]
      [Authorize]
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
         List<ActivityModel> activitiesList = await _cache.GetOrCreateAsync(CacheKeys.GetActivities, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<ActivityModel>("Lookup/GetActivities", null);
         });
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

      [AllowAnonymous]
      [HttpGet]
      public async Task<List<SelectListItem>> GetAllSpendingCategories()
      {
         List<LookupModel> categories = await _cache.GetOrCreateAsync(CacheKeys.SpendingCategories, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetAllSpendingCategories", null);
         });

         var categoriesList = new List<SelectListItem>();
         foreach (var item in categories)
         {

            categoriesList.Add(new SelectListItem { Value = item.Id.ToString(), Text = item.Name });

         }
         return categoriesList;
      }

      [AllowAnonymous]
      [HttpGet]
      public async Task<List<SelectListItem>> GetMainActivitiesAsync()
      {
         List<ActivityModel> activitiesList = await _cache.GetOrCreateAsync(CacheKeys.GetMainActivities, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
            return await _ApiClient.GetListAsync<ActivityModel>("Lookup/GetActivities", null);
         });
         var activitiesItems = new List<SelectListItem>();
         foreach (var item in activitiesList)
         {
            activitiesItems.Add(new SelectListItem { Value = item.ActivityId.ToString(), Text = item.Name });
         }
         return activitiesItems;
      }
      [AllowAnonymous]
      [HttpGet]
      public async Task<List<SelectListItem>> GetSubActivitiesAsync(int mainAcivityId)
      {
         var activitiesList = await _ApiClient.GetListAsync<ActivityModel>("Lookup/GetActivities", null);
         var activitiesItems = new List<SelectListItem>();
         activitiesList = activitiesList.Where(a => a.ActivityId == mainAcivityId).ToList();
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

      [HttpGet]
      public async Task<ActionResult> GetFinancialYear(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         var result = await _ApiClient.GetListAsync<string>("Tender/GetFinancialYear", null);
         return Json(result);
      }

      #endregion

      #region [Supplier Info]
      [HttpGet("Tender/ValidateMCICRAndGetInfo/{CR}")]
      public async Task<ActionResult> ValidateMCICRAndGetInfo(string CR)
      {
         try
         {

            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/ValidateMCICRAndGetInfo/" + CR, null);


            return Json(new { result });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }


         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
            //return NotFound();
         }

      }

      [HttpGet("Tender/GetCOCSubscriptionBySijilNumber/{LicenseNumber}/{CityCode}")]
      public async Task<ActionResult> GetCOCSubscriptionBySijilNumber(string LicenseNumber, string CityCode)
      {
         try
         {

            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/GetCOCSubscriptionBySijilNumber/" + LicenseNumber + "/" + CityCode, null);


            return Json(new { result });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
            //return NotFound();
         }

      }

      [HttpGet("Tender/ZakatCertificateInquiryByCR/{CR}")]
      public async Task<ActionResult> ZakatCertificateInquiryByCR(string CR)
      {
         try
         {

            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/ZakatCertificateInquiryByCR/" + CR, null);


            return Json(new { result });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
            //return NotFound();
         }

      }




      [HttpGet("Tender/LocalContentGetEtasblishmentTypeByCR/{CR}")]
      public async Task<ActionResult> LocalContentGetEtasblishmentTypeByCR(string CR)
      {
         try
         {
            var result = await _ApiClient.GetAsync<LocalContentViewModel>("Lookup/CheckEstablishmentType/" + CR, null);
            return Json(new { result });
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




      [HttpGet("Tender/ContractorDetailsInquiry/{partyNumberId}")]
      public async Task<ActionResult> ContractorDetailsInquiry(string partyNumberId)
      {
         try
         {
            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/ContractorDetailsInquiry/" + partyNumberId, null);
            return Json(new { result });
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


      [HttpGet("Tender/LicenseStatusInquiry/{LicenseNumber}")]
      public async Task<ActionResult> LicenseStatusInquiry(string LicenseNumber)
      {
         try
         {

            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/LicenseStatusInquiry/" + LicenseNumber, null);
            return Json(new { result });
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

      [HttpGet("Tender/GOSICertificateInquiry/{GOSIId}")]
      public async Task<ActionResult> GOSICertificateInquiry(string GOSIId)
      {
         try
         {

            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/GOSICertificateInquiry/" + GOSIId, null);


            return Json(new { result });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (Exception ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }

      }

      [HttpGet("Tender/NitaqatInformationInquiry/{EstablishmentLaborOfficeId}/{EstablishmentSequenceNumber}")]
      public async Task<ActionResult> NitaqatInformationInquiry(string EstablishmentLaborOfficeId, string EstablishmentSequenceNumber)
      {
         try
         {
            var result = await _ApiClient.GetAsync<SupplierInfoStatusModel>("Tender/NitaqatInformationInquiry/" + EstablishmentLaborOfficeId + "/" + EstablishmentSequenceNumber, null);
            return Json(new { result });
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
            //return NotFound();
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
            //return NotFound();
         }

      }
      #endregion


      #region Call Details View Components

      public IActionResult GetBasicStepViewComponenet(string tenderIdStr)
      {
         return ViewComponent("BasicDetails", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetTenderDatesViewComponenet(string tenderIdStr)
      {
         return ViewComponent("TenderDatesWithoutChanges", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetTenderLocalContentValuesViewComponenet(string tenderIdStr)
      {
         return ViewComponent("TenderLocalContentValues", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetTenderLocalContentDetailsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("TenderLocalContentDetails", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetInvitationsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("InvitationsViewComponent", new { tenderIdString = tenderIdStr });
      }

      [Authorize]
      public IActionResult GetCommunicationRequestsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("CommunicationRequestsViewComponenet", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetTenderDetailsForPlaintViewComponenet(string tenderIdString)
      {
         return ViewComponent("TenderDetailsForPlaint", new { tenderIdString = tenderIdString });
      }
      public IActionResult GetTenderDatesChangesViewComponenet(string tenderIdStr)
      {
         return ViewComponent("TenderDates", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetQualificationDatesChangesViewComponenet(string tenderIdString)
      {
         return ViewComponent("QualificationDates", new { tenderIdString = tenderIdString });
      }
      public IActionResult GetRelationsDetailsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("ActivityDetails", new { tenderIdString = tenderIdStr });
      }
      public IActionResult GetAttachmentsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("AttachmentsWithoutChanges", new { tenderId = Util.Decrypt(tenderIdStr) });
      }
      public IActionResult GetAttachmentsChangesViewComponenet(string tenderIdStr)
      {
         return ViewComponent("Attachments", new { tenderId = Util.Decrypt(tenderIdStr) });
      }

      [Authorize]
      public IActionResult GetQuantitiesTableViewComponenet(string tenderIdStr)
      {
         return ViewComponent("QuantitiesTableWithoutChanges", new { tenderId = Util.Decrypt(tenderIdStr) });
      }
      public IActionResult GetQuantitiesTableChangesViewComponenet(string tenderIdStr)
      {
         return ViewComponent("QuantitiesTable", new { tenderId = Util.Decrypt(tenderIdStr) });
      }
      public IActionResult GetEnquiryViewComponenet(string tenderIdStr, bool canAddEnquiry)
      {
         return ViewComponent("Enquiry", new { tenderId = Util.Decrypt(tenderIdStr), canAddEnquiry = canAddEnquiry });
      }
      public IActionResult GetAwardingViewComponenet(string tenderIdStr)
      {
         return ViewComponent("AwardingDetails", new { tenderId = Util.Decrypt(tenderIdStr) });
      }
      public IActionResult GetCheckingResultsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("CheckingResults", new { tenderId = Util.Decrypt(tenderIdStr) });
      }
      public IActionResult GetTenderNewsViewComponenet(string tenderIdStr)
      {
         return ViewComponent("TenderNews", new { tenderId = Util.Decrypt(tenderIdStr) });
      }
      public IActionResult GetPurchaseBookViewComponenet(string tenderIdStr, string tenderStatusIdString)
      {
         return ViewComponent("PurchaseBookViewComponent", new { tenderId = Util.Decrypt(tenderIdStr), StatusId = tenderStatusIdString });
      }
      public IActionResult GetInvitationBillDetailsViewComponenet(string tenderIdStr, string tenderStatusIdString)
      {
         return ViewComponent("InvitationBillDetailsViewComponenet", new { tenderId = Util.Decrypt(tenderIdStr), StatusId = tenderStatusIdString });
      }
      #endregion

      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> TenderExtendDateApprovement(string tenderIdString)
      {
         return View("TenderExtendDateApprovement", tenderIdString);
      }


      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> TenderQuantityTableChangesApprovement(string tenderIdString)
      {
         //return View("TenderQuantityTableChangesApprovement", tenderIdString);
         try
         {
            QuantityTableStepDTO tables;
            //if (TempData["model"] != null)
            //{
            //   tables = TempData.Get<QuantityTableStepDTO>("model");
            //   TempData["model"] = null;
            //}
            //else
            //{
            tables = await _ApiClient.GetAsync<QuantityTableStepDTO>("Tender/GetReadOnlyTenderQuantityItemsChangesById/" + Util.Decrypt(tenderIdString), null);
            //}
            return View(tables);
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
      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      public async Task<ActionResult> TenderAttachmentsChangesApprovement(string tenderIdString)
      {
         return View("TenderAttachmentsChangesApprovement", tenderIdString);
      }

      #region Unit Stage

      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public ActionResult TenderIndexUnitStage(string Type)
      {
         return View("/Views/Tender/Unit/TenderIndexUnitStage.cshtml");
      }

      [HttpGet]
      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<ActionResult> GetTendersForUnitStageIndexAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Tender/GetTendersForUnitStageIndexAsync", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<ActionResult> TenderDetailsForUnitSecretary(string tenderIdString)
      {
         try
         {
            TenderDetailsModel tenderDetailsModel = await _ApiClient.GetAsync<TenderDetailsModel>("Tender/GetMainTenderDetailsForUnit/" + tenderIdString, null);
            if (tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.UnderUnitReviewLevelOne || tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.UnderUnitReviewLevelTwo)
            {
               ViewBag.TenderModificationsOptions = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetUnitModificationsTypes", null);
               if (User.IsInRole(RoleNames.UnitSpecialistLevel1))
               {
                  ViewBag.UnitSecretryLevelTwo = await _ApiClient.GetListAsync<LookupModel>("Tender/GetAllUnitSecretryLevelTwo/" + RoleNames.UnitSpecialistLevel2, null);
               }
            }
            if (User.IsInRole(RoleNames.UnitBusinessManagement))
            {
               if (tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.UnderUnitReviewLevelOne || tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.WaitingUnitSecretaryReview)
               {
                  ViewBag.UnitSecretryLevelTwo = await _ApiClient.GetListAsync<LookupModel>("Tender/GetAllUnitSecretryLevelTwo/" + RoleNames.UnitSpecialistLevel1, null);
               }
               else if (tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.UnderUnitReviewLevelTwo || tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.TenderTransferdToLevelTwo)
               {
                  ViewBag.UnitSecretryLevelTwo = await _ApiClient.GetListAsync<LookupModel>("Tender/GetAllUnitSecretryLevelTwo/" + RoleNames.UnitSpecialistLevel2, null);
               }
               else if (tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.WaitingManagerApprove || tenderDetailsModel.TenderUnitStatusId == (int)TenderUnitStatus.UnderManagerReviewing)
               {
                  ViewBag.UnitSecretryLevelTwo = await _ApiClient.GetListAsync<LookupModel>("Tender/GetAllUnitSecretryLevelTwo/" + RoleNames.UnitManagerUser, null);
               }
            }
            return View("/Views/Tender/Unit/DetailsForUnit.cshtml", tenderDetailsModel);
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

      #region LevelOne

      [HttpPost]
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> OpenTenderByUnitSecretaryAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/OpenTenderByUnitSecretaryAsync/" + tenderIdString, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> SendTenderByUnitSecretaryForModificationAsync(ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel)
      {
         try
         {
            returnTenderToDataEntryFromUnitFormodificationsModel.files = new List<ExtendOffersValidityAttachementViewModel>();
            if (!string.IsNullOrEmpty(returnTenderToDataEntryFromUnitFormodificationsModel.filesString))
            {
               var files = returnTenderToDataEntryFromUnitFormodificationsModel.filesString.Split(',');
               foreach (var file in files)
               {
                  if (!string.IsNullOrEmpty(file))
                     returnTenderToDataEntryFromUnitFormodificationsModel.files.Add(PrepareExtendOfferValidityAttachmentForInsert(file));
               }
            }
            await _ApiClient.PostAsync("Tender/SendTenderByUnitSecretaryForModificationAsync", null, returnTenderToDataEntryFromUnitFormodificationsModel);
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
      private static ExtendOffersValidityAttachementViewModel PrepareExtendOfferValidityAttachmentForInsert(string guaranteeReferenceId)
      {
         List<string> attachmentReferences = new List<string>();
         if (guaranteeReferenceId != null)
         {
            if (!string.IsNullOrEmpty(guaranteeReferenceId))
            {
               if (guaranteeReferenceId.Contains("/GetFile/"))
                  attachmentReferences.Add(guaranteeReferenceId.Split("/GetFile/")[guaranteeReferenceId.Split("/GetFile/").Length - 1]);
               else
                  attachmentReferences.Add(guaranteeReferenceId);
            }
            var arr = guaranteeReferenceId.Split(":");
            ExtendOffersValidityAttachementViewModel extendOffersValidityAttachement = new ExtendOffersValidityAttachementViewModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.InitialGuarantee, FileNetReferenceId = arr[0] };
            return extendOffersValidityAttachement;
         }
         return new ExtendOffersValidityAttachementViewModel();
      }

      [HttpPost]
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> ApproveTenderByUnitSecretaryAsync(string tenderIdString, bool IWouldLikeToAttendTheommitte)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ApproveTenderByUnitSecretaryAsync/" + tenderIdString + "/" + IWouldLikeToAttendTheommitte, null, null);
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
      [Authorize(Roles = RoleNames.UnitSpecialistLevel1 + "," + RoleNames.UnitSpecialistLevel2)]
      public async Task<ActionResult> UpdateTenderLocalContentValues(UpdateTenderNationalProductValuesViewModel viewModel)
      {

         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            return RedirectToAction(nameof(Details), new { STenderId = viewModel.TenderIdString });
         }

         try
         {
            await _ApiClient.PostAsync("Tender/UpdateTenderLocalContentValues/", null, viewModel);


            AddMessage(Resources.SharedResources.Messages.DataSaved);
            return RedirectToAction(nameof(Details), new { STenderId = viewModel.TenderIdString });
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
             return RedirectToAction(nameof(Details), new { STenderId = viewModel.TenderIdString });
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
            //_logger.LogError(ex.ToString(), ex);
            //return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [HttpPost]
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> RejectTenderByUnitSecretaryAsync(string tenderIdString, string comment)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/RejectTenderByUnitSecretaryAsync/" + tenderIdString + "/" + comment, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> SendToLevelTwoFromUnitSecretaryLevelOneAsync(string tenderIdString, string userName, string notes)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendToLevelTwoFromUnitSecretaryLevelOneAsync/" + tenderIdString + "/" + userName, null, notes);
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
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> SendToApproveFromUnitSecretaryToUnitMangerAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendToApproveFromUnitSecretaryToUnitMangerAsync/" + tenderIdString, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel1Policy)]
      public async Task<ActionResult> ReOpenTenderByUnitSecertaryAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ReOpenTenderByUnitSecertaryAsync/" + tenderIdString, null, null);
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

      #endregion LevelOne

      #region LevelTwo

      [HttpPost]
      [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
      public async Task<ActionResult> OpenTenderByUnitSecretaryLevelTwoAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/OpenTenderByUnitSecretaryLevelTwoAsync/" + tenderIdString, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
      public async Task<ActionResult> SendTenderByUnitSecretaryLevelTwoForModificationAsync(ReturnTenderToDataEntryFromUnitFormodificationsModel returnTenderToDataEntryFromUnitFormodificationsModel)
      {
         try
         {
            returnTenderToDataEntryFromUnitFormodificationsModel.files = new List<ExtendOffersValidityAttachementViewModel>();
            if (!string.IsNullOrEmpty(returnTenderToDataEntryFromUnitFormodificationsModel.filesString))
            {
               var files = returnTenderToDataEntryFromUnitFormodificationsModel.filesString.Split(',');
               foreach (var file in files)
               {
                  if (!string.IsNullOrEmpty(file))
                     returnTenderToDataEntryFromUnitFormodificationsModel.files.Add(PrepareExtendOfferValidityAttachmentForInsert(file));
               }
            }
            await _ApiClient.PostAsync("Tender/SendTenderByUnitSecretaryLevelTwoForModificationAsync", null, returnTenderToDataEntryFromUnitFormodificationsModel);
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
      [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
      public async Task<ActionResult> RejectTenderByUnitSecretaryLevelTwoAsync(string tenderIdString, string comment)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/RejectTenderByUnitSecretaryLevelTwoAsync/" + tenderIdString + "/" + comment, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
      public async Task<ActionResult> ApproveTenderByUnitSecretaryLevelTwoAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ApproveTenderByUnitSecretaryLevelTwoAsync/" + tenderIdString, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
      public async Task<ActionResult> SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendToApproveFromUnitSecretaryLevelTwoToUnitMangerAsync/" + tenderIdString, null, null);
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
      [Authorize(RoleNames.UnitSpecialistLevel2Policy)]
      public async Task<ActionResult> ReOpenTenderByUnitSecertaryLevelAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ReOpenTenderByUnitSecertaryLevelAsync/" + tenderIdString, null, null);
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

      #endregion LevelTwo

      #region Unit Manager

      [HttpPost]
      [Authorize(RoleNames.UnitManagerUserPolicy)]
      public async Task<ActionResult> ReviewTenderByUnitManagerAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ReviewTenderByUnitManager/" + tenderIdString, null, null);
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
      [Authorize(RoleNames.UnitManagerUserPolicy)]
      public async Task<ActionResult> ApproveTenderByUnitManagerAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ApproveTenderByUnitManagerAsync/" + tenderIdString + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.UnitManagerUserPolicy)]
      public async Task<ActionResult> RejectTenderByUnitManagerAsync(string tenderIdString, string comment)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/RejectTenderByUnitManagerAsync/" + tenderIdString + "/" + comment, null, null);
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

      #endregion Unit Manager

      [HttpPost]
      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<ActionResult> SendToNewUserUnitBusinessManagementAsync(string tenderIdString, string userName)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendToNewUserUnitBusinessManagementAsync/" + tenderIdString + "/" + userName, null, null);
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

      #region Direct Purchase checking

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      public ActionResult TenderIndexAwardingStageForDirectPurchase(string Type)
      {
         return View();
      }

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTendersForAwardingStageIndexForDirectPurchaseAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Tender/GetTendersForAwardingStageIndex", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.ApproveTenderAward)]
      public ActionResult TenderIndexCheckingDirectPuchaseStage(string Type)
      {
         return View("~/Views/Tender/DirectPurchase/TenderIndexCheckingDirectPuchaseStage.cshtml");
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.ApproveTenderAward)]
      public async Task<ActionResult> GetTendersForCheckingDirectPuchaseStageAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<TenderCheckingAndAwardingModel>("Tender/GetTendersForCheckingDirectPuchaseStageAsync", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      public async Task<ActionResult> CheckDirectPurchaseOffers(string tenderIdString)
      {
         TenderOffersModel tender;
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            tender = await _ApiClient.GetAsync<TenderOffersModel>("Tender/GetDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage/" + tenderId, null);
            return View("/Views/Tender/DirectPurchase/CheckDirectPurchaseOffers.cshtml", tender);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      public async Task<ActionResult> StartTenderDirectPurchaseOffersCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/StartDirectPurchaseTenderCheckingOffers/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> SendDirectPurchaseTenderOffersCheckingToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendDirectPurchaseTenderOffersCheckingToApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      public async Task<ActionResult> ApproveDirectPurchaseTenderOffersCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveDirectPurchaseTenderOffersChecking/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> RejectDirectPurchaseTenderOffersReportAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectDirectPurchaseTenderOffersChecking", null, rejectionModel);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> ReopenDirectPurchaseTenderOffersCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenDirectPurchaseTenderOffersChecking/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> SendDirectPurchaseTenderOffersTechnicalCheckingToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendDirectPurchaseTenderOffersTechnicalCheckingToApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> ApproveDirectPurchaseTenderOffersTechnicalCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveDirectPurchaseTenderOffersTechnicalChecking/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> RejectDirectPurchaseTenderOffersTechnicalCheckingAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectDirectPurchaseTenderOffersTechnicalChecking", null, rejectionModel);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> ReopenDirectPurchaseTenderOffersTechnicalCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenDirectPurchaseTenderOffersTechnicalChecking/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> SendDirectPurchaseTenderOffersFinanceCheckingToApproveAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/SendDirectPurchaseTenderOffersFinanceCheckingToApprove/" + tenderId, null, null);
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
      [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> ApproveDirectPurchaseTenderOffersFinanceCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ApproveDirectPurchaseTenderOffersFinanceChecking/" + tenderId + "/" + verificationCode, null, null);
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
      [Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> RejectDirectPurchaseTenderOffersFinanceCheckingAsync(RejectionModel rejectionModel)
      {
         try
         {
            rejectionModel.TenderId = Util.Decrypt(rejectionModel.TenderIdString);
            await _ApiClient.PostAsync("Tender/RejectDirectPurchaseTenderOffersFinanceChecking", null, rejectionModel);
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
      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> ReopenDirectPurchaseTenderOffersFinancialCheckingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            await _ApiClient.PostAsync("Tender/ReopenDirectPurchaseTenderOffersFinancialChecking/" + tenderId, null, null);
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

      #endregion Direct Purchase checking

      #region Round Bidding 

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.supplier)]
      public async Task<ActionResult> TenderBiddingViewAsync(string tenderIdString)
      {
         try
         {
            ViewBag.tenderIdString = tenderIdString;
            var result = await _ApiClient.GetAsync<BiddingTenderDetailsModel>("Tender/GetBiddingRoundDetailsByBiddingRoundId/" + tenderIdString, null);
            return View(result);
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

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.supplier)]
      [HttpGet]
      public async Task<ActionResult> RoundBiddingPagingAsync(string tenderIdString)
      {
         try
         {
            var result = await _ApiClient.GetAsync<BiddingTenderDetailsModel>("Tender/GetBiddingRoundOffersByBiddingRoundId/" + tenderIdString, null);
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

      [Authorize(Roles = RoleNames.supplier)]
      [HttpPost]
      public async Task<ActionResult> AddOfferBid([FromForm] string tenderIdString, [FromForm] string biddingRoundIdString, [FromForm] decimal biddingValue)
      {
         try
         {
            if (string.IsNullOrEmpty(tenderIdString) || string.IsNullOrEmpty(biddingRoundIdString))
               AddError(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            else
            {
               await _ApiClient.PostAsync<BiddingTenderDetailsModel>("Tender/AddOfferBid/" + tenderIdString + "/" + biddingRoundIdString + "/" + biddingValue, null, null);
               await _hubContext.Clients.All.SendAsync($"Notify_BiddingRound_{tenderIdString}_{biddingRoundIdString}", $"Home page loaded at: {DateTime.Now}");
            }
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            _logger.LogError(ex.ToString(), ex);
         }
         return RedirectToAction(nameof(TenderBiddingViewAsync), new { tenderIdString });
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost]
      public async Task<ActionResult> EndTenderPedding(string tenderIdString, string biddingRoundIdString, string verificationCode)
      {
         try
         {
            await _ApiClient.PostAsync<BiddingTenderDetailsModel>("Tender/EndTenderPedding/" + tenderIdString + "/" + biddingRoundIdString + "/" + verificationCode, null, null);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
         }
         return RedirectToAction(nameof(TenderBiddingViewAsync), new { tenderIdString });
      }

      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost]
      public async Task<ActionResult> StartNewRound(BiddingDateTimeViewModel biddingDateTimeViewModel/*, string verificationCode*/)
      {
         try
         {
            biddingDateTimeViewModel.BiddingStartDateTime = biddingDateTimeViewModel.BiddingDate + DateHelper.GetTimePart(biddingDateTimeViewModel.StartTime);
            biddingDateTimeViewModel.BiddingEndDateTime = biddingDateTimeViewModel.BiddingDate + DateHelper.GetTimePart(biddingDateTimeViewModel.EndTime);
            await _ApiClient.PostAsync<BiddingTenderDetailsModel>("Tender/StartNewRound/"/* + verificationCode*/, null, biddingDateTimeViewModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
         }
         return RedirectToAction(nameof(TenderBiddingViewAsync), new { biddingDateTimeViewModel.TenderIdString });
      }

      #endregion

      #region VRO
      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      public ActionResult VROTendersIndexCheckingAndOpeningStage(string Type)
      {
         return View();
      }

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetVROTendersIndexCheckingAndOpeningStageAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<VROTenderCheckingAndOpeningModel>("Tender/GetVROTendersIndexCheckingAndOpeningStageIndex", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, tenderSearchCriteriaModel.PageSize, result.PageNumber, null));
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

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      public async Task<ActionResult> VROTenderChecking(string tenderIdString)
      {
         try
         {
            VROTenderOffersModel tender = await _ApiClient.GetAsync<VROTenderOffersModel>("Tender/FindVROTenderOfferDetailsByTenderIdForCheckStage/" + tenderIdString, null);
            return View("/Views/Tender/VRO/VROTenderChecking.cshtml", tender);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      [HttpPost]
      //[Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> StartVROTenderOffersCheckingAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/StartVROTenderOffersCheckingAsync/" + tenderIdString, null, null);
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

      #region Technical

      [HttpPost]
      //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> SendVROTenderOffersTechnicalCheckingToApproveAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendVROTenderOffersTechnicalCheckingToApproveAsync/" + tenderIdString, null, null);
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
      //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> ReopenVROTenderOffersTechnicalCheckingAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ReopenVROTenderOffersTechnicalCheckingAsync/" + tenderIdString, null, null);
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
      //[Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> ApproveVROTenderOffersTechnicalCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ApproveVROTenderOffersTechnicalCheckingAsync/" + tenderIdString + "/" + verificationCode, null, null);
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
      public async Task<ActionResult> RejectVROTenderOffersTechnicalCheckingAsync(RejectionModel rejectionModel)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/RejectVROTenderOffersTechnicalCheckingAsync", null, rejectionModel);
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

      #endregion Technical

      #region Financial

      [HttpPost]
      //[Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> StartVROTenderOffersFinancialCheckingAsync(string tenderIdString, decimal estimatedValue = 0.00m)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/StartVROTenderOffersFinancialCheckingAsync/" + tenderIdString + '/' + estimatedValue, null, null);
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
      //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> SendVROTenderOffersFinanceCheckingToApproveAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/SendVROTenderOffersFinanceCheckingToApproveAsync/" + tenderIdString, null, null);
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
      //[Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      public async Task<ActionResult> ReopenVROTenderOffersFinancialCheckingAsync(string tenderIdString)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ReopenVROTenderOffersFinancialCheckingAsync/" + tenderIdString, null, null);
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
      //[Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> ApproveVROTenderOffersFinanceCheckingAsync(string tenderIdString, string verificationCode)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/ApproveVROTenderOffersFinanceCheckingAsync/" + tenderIdString + "/" + verificationCode, null, null);
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
      //[Authorize(RoleNames.OffersPurchaseManagerPolicy)]
      public async Task<ActionResult> RejectVROTenderOffersFinanceCheckingAsync(RejectionModel rejectionModel)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/RejectVROTenderOffersFinanceCheckingAsync", null, rejectionModel);
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

      #endregion Financial

      #endregion VRO

      #region Negotiation For Unit Specialist


      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public ActionResult TenderNegotiation()
      {
         return View();
      }

      [HttpGet]
      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<IActionResult> GetAllTenderhasNegotiationbySearchCretriaForUnitUsersAsync(NegotiationOnTenderSearchCriteriaModel negotiationOnTender)
      {
         try
         {
            var user = User.UserRole();
            var tenders = await _ApiClient.GetQueryResultAsync<NegotiationOnTenderModel>("Tender/GetAllTenderhasNegotiationbySearchCretriaForUnitUsers", negotiationOnTender.ToDictionary());
            return Json(new PaginationModel(tenders.Items, tenders.TotalCount, tenders.PageSize, tenders.PageNumber, null));
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

      #region UnRegisterd-Suppliers-Invitation
      [HttpGet]
      public async Task<ActionResult> CheckForCrNumberExistAsync(string crNumber)
      {
         try
         {
            var result = await _ApiClient.GetAsync<UnRegisteredSuppliersInvitationsModel>("Tender/CheckForCrNumberExist/" + crNumber, null);
            return Json(result.CrName);
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
      public async Task<ActionResult> SendInvitationsForUnRegisteredSupplierAsync(UnRegisteredSuppliersInvitationsModel invitationsModel)
      {
         try
         {
            var result = await _ApiClient.PostAsync("Tender/SendInvitationsForUnRegisteredSupplier", null, invitationsModel);
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
      public async Task<ActionResult> RemoveUnRegisterSupplier(int tenderId, string crNumber)
      {
         try
         {
            await _ApiClient.GetStringAsync("Tender/RemoveUnRegisterSupplier/" + tenderId + "/" + crNumber, null);
            return Json(new { status = "success", message = Resources.BlockResources.Messages.DataRemoved });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            return Json(new { status = "error", message = ex.Message });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            return Json(new { status = "error", message = Resources.TenderResources.ErrorMessages.UnexpectedError });
         }
      }
      //public async Task<ActionResult> SendInvitationsForForeignSuppliersAsync(UnRegisteredSuppliersInvitationsModel invitationsModel)
      //{
      //   try
      //   {
      //      var result = await _ApiClient.PostAsync("SendInvitationsForForeignSuppliers", null, invitationsModel);
      //      return Ok();
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      return JsonErrorMessage(ex.Message);
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //}
      //public async Task<ActionResult> SendInvitationsForOtherdSuppliersAsync(UnRegisteredSuppliersInvitationsModel invitationsModel)
      //{
      //   try
      //   {
      //      var result = await _ApiClient.PostAsync("SendInvitationsForOtherdSuppliers", null, invitationsModel);
      //      return Ok();
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      return JsonErrorMessage(ex.Message);
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
      //   }
      //}

      #endregion

      #region ConditinoTemplateAttachmentTemplate
      [HttpGet("Tender/UpdateConditionsTemplate/{tenderIdString}")]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> UpdateConditionsTemplate(string tenderIdString)
      {
         try
         {
            var model = await _ApiClient.GetAsync<GetConditionTemplateModel>("Tender/GetConditionTemplate/" + tenderIdString, null);
            model.Certificates = await _cache.GetOrCreateAsync(CacheKeys.GetBookletCertificates, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromHours(minutes);
               return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetBookletCertificates", null);
            });


            model.LocalContentMechanism = await _cache.GetOrCreateAsync(CacheKeys.GetLocalContentMechanism, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes);
               entry.SlidingExpiration = TimeSpan.FromHours(minutes);
               return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetLocalContentMechanism", null);
            });

            return View("/Views/Tender/ConditionTemplate/UpdateConditionsTemplate.cshtml", model);
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
         catch (Exception e)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index));
         }
      }

      private async Task<GetConditionTemplateModel> PrepareEditingDataAsync(string id, GetConditionTemplateModel model = null)
      {

         model = new GetConditionTemplateModel();
         if (!string.IsNullOrEmpty(id))
         {
            model = await _ApiClient.GetAsync<GetConditionTemplateModel>("Tender/GetTenderConditionsTemplateById/" + id, null);
         }
         model.Certificates = await _cache.GetOrCreateAsync(CacheKeys.GetBookletCertificates, async entry =>
         {
            int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
            entry.SlidingExpiration = TimeSpan.FromHours(minutes);
            return await _ApiClient.GetListAsync<LookupModel>("Lookup/GetBookletCertificates", null);
         });
         return model;
      }

      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> UpdateConditionsTemplateAsync(GetConditionTemplateModel model)
      {
         if (!ModelState.IsValid)
         {
            AddError(Resources.TenderResources.ErrorMessages.ModelDataError);
            var pageModel = await PrepareEditingDataAsync(model.EncryptedTenderId, model);
            return View(pageModel);
         }
         try
         {
            List<string> attachmentReferences = new List<string>();
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
            List<TenderAttachmentModel> tenderAttachments = new List<TenderAttachmentModel>();
            foreach (var item in attachmentReferences)
            {
               var arr = item.Split(":");
               TenderAttachmentModel tenderAttachment = new TenderAttachmentModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.TenderOtherFile, FileNetReferenceId = arr[0] };
               tenderAttachments.Add(tenderAttachment);
            }
            model.TenderAttachments = tenderAttachments;
            await _ApiClient.PostAsync<PrePlanningModel>("Tender/UpdateConditionsTemplate", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return RedirectToAction(nameof(AttachmentsStep), new { id = Util.Encrypt(model.EncryptedTenderId) });
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
         var FormModel = await PrepareEditingDataAsync(model.EncryptedTenderId, model);
         return View(FormModel);
      }


      [HttpPost]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> AddEditIntroductionTemplateAsync(EditConditionTemplateSecondSectionModel model)
      {

         try
         {
            if (!ModelState.IsValid)
            {
               throw new BusinessRuleException(ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage).FirstOrDefault());
            }
            await _ApiClient.PostAsync<PrePlanningModel>("Tender/AddEditIntroductionTemplate", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> AddEditPreparingOffersTemplateAsync(EditConditionTemplateThridSectionModel model)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               throw new BusinessRuleException(ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage).FirstOrDefault());
            }
            await _ApiClient.PostAsync<PrePlanningModel>("Tender/AddEditPreparingOffersTemplate", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> AddEditTechnicalDeclerationsTemplateAsync(EditConditionTemplateSeventhSectionModel model)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               throw new BusinessRuleException(ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage).FirstOrDefault());
            }
            await _ApiClient.PostAsync<PrePlanningModel>("Tender/AddEditTechnicalDeclerationsTemplate", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> AddEditDescriptionAndConditionsTemplateAsync(EditConditionTemplateEighthSectionModel model)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               throw new BusinessRuleException(ModelState.Values
                              .SelectMany(v => v.Errors)
                              .Select(e => e.ErrorMessage).FirstOrDefault());
            }
            List<string> attachmentReferences = new List<string>();
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
            List<TenderAttachmentModel> tenderAttachments = new List<TenderAttachmentModel>();
            foreach (var item in attachmentReferences)
            {
               var arr = item.Split(":");
               TenderAttachmentModel tenderAttachment = new TenderAttachmentModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.TenderOtherFile, FileNetReferenceId = arr[0] };
               tenderAttachments.Add(tenderAttachment);
            }
            model.TenderAttachments = tenderAttachments;
            await _ApiClient.PostAsync<PrePlanningModel>("Tender/AddEditDescriptionAndConditionsTemplate", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
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
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<ActionResult> AddEditLocalContenteAsync(LocalContentModel model)
      {

         try
         {
            List<string> attachmentReferences = new List<string>();
            if (model.StudyMinimumTargetFileNetReferenceId != null)
            {
               string[] lst = model.StudyMinimumTargetFileNetReferenceId.Split(',');
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
            List<TenderAttachmentModel> tenderAttachments = new List<TenderAttachmentModel>();
            foreach (var item in attachmentReferences)
            {
               var arr = item.Split(":");
               TenderAttachmentModel tenderAttachment = new TenderAttachmentModel() { Name = arr[1], AttachmentTypeId = (int)AttachmentType.LocalContent, FileNetReferenceId = arr[0] };
               tenderAttachments.Add(tenderAttachment);
            }
            model.TenderAttachments = tenderAttachments;

            await _ApiClient.PostAsync<PrePlanningModel>("Tender/AddEditLocalContentTemplate", null, model);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
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


      [AllowAnonymous]
      public async Task<IActionResult> ConditionsTemplateHtml(string STenderId)
      {
         GetConditionTemplateModel tenderDetailsModel = new GetConditionTemplateModel();
         QuantitiesTemplateModel quantitiesTablesModel = new QuantitiesTemplateModel();
         try
         {

            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(async () =>
            {
               tenderDetailsModel = await _ApiClient.GetAsync<GetConditionTemplateModel>("Tender/GetTenderConditionsTemplateById/" + STenderId, null);
            }));
            tasks.Add(Task.Run(async () =>
            {
               quantitiesTablesModel = await _ApiClient.GetAsync<QuantitiesTemplateModel>("Tender/GetTenderQuantityItemsDetailsById/" + Util.Decrypt(STenderId), null);
            }));

            await Task.WhenAll(tasks);

            tenderDetailsModel.QuantityTables = quantitiesTablesModel.grid;

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return View("/Views/Tender/ConditionTemplate/ConditionsTemplateHtml.cshtml", tenderDetailsModel);
      }

      public async Task<IActionResult> PrintConditionsTemplateHtml(string STenderId)
      {
         GetConditionTemplateModel tenderDetailsModel = new GetConditionTemplateModel();
         TemplateQuantityTableModel quantitiesTablesModel = new TemplateQuantityTableModel();
         try
         {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(async () =>
            {
               tenderDetailsModel = await _ApiClient.GetAsync<GetConditionTemplateModel>("Tender/GetTenderConditionsTemplateById/" + STenderId, null);
               tenderDetailsModel.AgencyName = User.UserAgencyName();
            }));
            tasks.Add(Task.Run(async () =>
            {
               quantitiesTablesModel = await _ApiClient.GetAsync<TemplateQuantityTableModel>("Tender/GetTenderQuantityItemsForPrint/" + Util.Decrypt(STenderId), null);
            }));
            await Task.WhenAll(tasks);
            tenderDetailsModel.RegulationsDate = _rootConfiguration.ConditionsTemplateConfiguration.RegulationsDate /*_configuration.GetSection("ConditionsTemplate:RegulationsDate").Value.ToString()*/;
            tenderDetailsModel.TenderConditionsTemplatePriceText = new ConvertNumberToText(tenderDetailsModel.TenderConditionsTemplatePriceNumbers ?? 0, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic();
            tenderDetailsModel.QuantityTables = new List<string> { quantitiesTablesModel.Tables };
            tenderDetailsModel.DescriptionQuantityTables = quantitiesTablesModel.DescritionTables;

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return View("/Views/Tender/ConditionTemplate/ConditionsTemplateHtml.cshtml", tenderDetailsModel);
      }


      public async Task<IActionResult> PrintConditionsTemplateHtmlWithVersion(string STenderId)
      {
         GetConditionTemplateModel tenderDetailsModel = new GetConditionTemplateModel();
         TemplateQuantityTableModel quantitiesTablesModel = new TemplateQuantityTableModel();
         try
         {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(async () =>
            {
               tenderDetailsModel = await _ApiClient.GetAsync<GetConditionTemplateModel>("Tender/GetTenderConditionsTemplateById/" + STenderId, null);
               tenderDetailsModel.AgencyName = User.UserAgencyName();
            }));
            tasks.Add(Task.Run(async () =>
            {
               quantitiesTablesModel = await _ApiClient.GetAsync<TemplateQuantityTableModel>("Tender/GetTenderQuantityItemsForPrint/" + Util.Decrypt(STenderId), null);
            }));
            await Task.WhenAll(tasks);
            tenderDetailsModel.RegulationsDate = _rootConfiguration.ConditionsTemplateConfiguration.RegulationsDate /*_configuration.GetSection("ConditionsTemplate:RegulationsDate").Value.ToString()*/;
            tenderDetailsModel.TenderConditionsTemplatePriceText = new ConvertNumberToText(tenderDetailsModel.TenderConditionsTemplatePriceNumbers ?? 0, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic();
            tenderDetailsModel.QuantityTables = new List<string> { quantitiesTablesModel.Tables };
            tenderDetailsModel.DescriptionQuantityTables = quantitiesTablesModel.DescritionTables;

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
         return View("/Views/Tender/ConditionTemplate/ConditionsTemplateHtmlVersion4.cshtml", tenderDetailsModel);
      }


      #endregion

      [HttpGet]
      public async Task<IActionResult> GetTenderTableQuantityItemsAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Tender/GetTenderTableQuantityItems", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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

      [HttpGet]
      public async Task<IActionResult> GetTenderTableQuantityItemsChangesAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Tender/GetTenderTableQuantityItemsChanges", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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
      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> AddNewTable(int formid, int tenderId, int templateYears, string tableName)
      {
         try
         {
            return Content((await _ApiClient.GetAsync<long>("Tender/AddNewTable/" + formid + "/" + tenderId + "/" + templateYears + "/" + tableName, null)).ToString());
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
      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> AddNewTableChanges(int formid, int tenderId, int templateYears, string tableName)
      {
         try
         {
            return Content((await _ApiClient.GetAsync<long>("Tender/AddNewTableChange/" + formid + "/" + tenderId + "/" + templateYears + "/" + tableName, null)).ToString());
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch
         {
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError); //return NotFound();
         }
      }
      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> ExportTenderTableQuantityItemsAsync(int tableId, bool isNewChange)
      {
         try
         {
            var tableContent = await _ApiClient.GetListAsync<TenderQuantityItemDTO>("Tender/ExportTenderTableQuantityItems/" + tableId + "/" + isNewChange, null);
            return GenerateExcell(tableContent);
         }
         catch (BusinessRuleException ex)
         {
            return JsonErrorMessage(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message + "\n" + ex.StackTrace);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      [Authorize(RoleNames.DataEntryPolicy)]
      public async Task<IActionResult> ExportTenderTableQuantityItemsAsync_New(int tableId, bool isNewChange, int tenderId, int yearsCount, int formId, bool isEmpty = false)
      {
         try
         {
            var stageid = 1;
            List<TenderQuantityItemDTO> tableContent = new List<TenderQuantityItemDTO>();
            if (!isEmpty)
            {
               tableContent = await _ApiClient.GetListAsync<TenderQuantityItemDTO>("Tender/ExportTenderTableQuantityItems/" + tableId + "/" + isNewChange, null);

            }
            var excelHeader = await _ApiClient.GetAsync<ExcelHeader>($"Tender/GenerateQuantityTableTemplateExcel_New/{stageid}/{formId}/{yearsCount}", null);

            var ddd = GenerateExcell(excelHeader, tableContent, yearsCount);
            return ddd;
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


      private IActionResult GenerateExcell(ExcelHeader excelHeader, List<TenderQuantityItemDTO> tableContent, int yearsCount)
      {
         var yearWord = new List<string> { "السنة الاولى", "السنة الثانية", "السنة الثالثة", "السنة الرابعة", "السنة الخامسة", "السنة السادسة", "السنة السابعة", "السنة الثامنة", "السنة التاسعة", "السنة العاشرة" };
         var notHeaderedColumns = excelHeader.excelHeaderCells.Where(e => !e.isHeaderRepeated && !e.isDescription && e.columnTypeId != (int)Enums.ColumnTypeEnum.IsMandatoryList).OrderBy(d => d.columnOrder).ToList();
         var HeaderedColumns = excelHeader.excelHeaderCells.Where(e => e.isHeaderRepeated && e.columnTypeId != (int)Enums.ColumnTypeEnum.IsMandatoryList).OrderBy(d => d.columnOrder).ToList();
         var DescriptionColumns = excelHeader.excelHeaderCells.Where(e => e.isDescription && e.columnTypeId != (int)Enums.ColumnTypeEnum.IsMandatoryList).OrderBy(d => d.columnOrder).ToList();


         byte[] fileContents;
         int rowIndex = 1;
         int colIndex = 1;
         int startrow = 1;
         Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#29ad6f");
         Color bordercolor = System.Drawing.ColorTranslator.FromHtml("#157e4d");

         using (var package = new ExcelPackage())
         {
            if (String.IsNullOrEmpty(excelHeader.activityName))
            {
               excelHeader.activityName = excelHeader.formName;
            }
            var worksheet = package.Workbook.Worksheets.Add(excelHeader.activityName);
            worksheet.View.RightToLeft = true;
            if (HeaderedColumns.Any() || DescriptionColumns.Any())
            {
               startrow = 2;
               worksheet.InsertRow(1, 1);

            }

            if (DescriptionColumns.Any())
            {
               worksheet.Cells[1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount) + 1].Value = "المواصفات";
               worksheet.Cells[1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount + 1),
                  1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount) + DescriptionColumns.Count()].Merge = true;
               Color colFromHex1 = System.Drawing.ColorTranslator.FromHtml("#46cc76");
               worksheet.Cells[1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount + 1),
                           1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount) + DescriptionColumns.Count()].Style.Fill.PatternType = ExcelFillStyle.Solid;


               worksheet.Cells[1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount + 1),
               1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount) + DescriptionColumns.Count()].Style.Fill.BackgroundColor.SetColor(colFromHex);
               worksheet.Cells[1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount + 1),
               1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount) + DescriptionColumns.Count()].Style.Font.Color.SetColor(Color.White);

            }
            if (HeaderedColumns.Any())
            {
               Color colFromHex2 = System.Drawing.ColorTranslator.FromHtml("#46cc76");
               worksheet.Cells[1, 1
             ,
             1, notHeaderedColumns.Count()].Style.Fill.PatternType = ExcelFillStyle.Solid;
               worksheet.Cells[1, 1
                ,
                1, notHeaderedColumns.Count()].Style.Fill.BackgroundColor.SetColor(colFromHex2);
               for (int j = 0; j < yearsCount; j++)
               {
                  var start = notHeaderedColumns.Count() + (HeaderedColumns.Count() * (j) + 1);
                  var end = notHeaderedColumns.Count() + (HeaderedColumns.Count() * (j + 1));
                  worksheet.Cells[1, notHeaderedColumns.Count() + (HeaderedColumns.Count() * j) + 1].Value = yearWord[j];

                  worksheet.Cells[1, start
                     ,
                     1, end].Merge = true;

                  worksheet.Cells[1, start
                   ,
                   1, end].Style.Fill.PatternType = ExcelFillStyle.Solid;
                  worksheet.Cells[1, start
                   ,
                   1, end].Style.Fill.BackgroundColor.SetColor(colFromHex2);
                  worksheet.Cells[1, start
                   ,
                   1, end].Style.Font.Color.SetColor(Color.White);
               }
            }
            for (int i = 1; i <= notHeaderedColumns.Count(); i++)
            {
               worksheet.Cells[1, i, startrow, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
               worksheet.Cells[1, i, startrow, i].Style.Fill.BackgroundColor.SetColor(colFromHex);
               worksheet.Cells[1, i, startrow, i].Style.Font.Color.SetColor(Color.White);
               worksheet.Cells[1, i, startrow, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[1, i, startrow, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[1, i, startrow, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[1, i, startrow, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[1, i, startrow, i].Style.Border.Top.Color.SetColor(bordercolor);
               worksheet.Cells[1, i, startrow, i].Style.Border.Bottom.Color.SetColor(bordercolor);
               worksheet.Cells[1, i, startrow, i].Style.Border.Right.Color.SetColor(bordercolor);
               worksheet.Cells[1, i, startrow, i].Style.Border.Left.Color.SetColor(bordercolor);
               worksheet.Cells[1, i, startrow, i].Merge = true;
               worksheet.Cells[1, i, startrow, i].Value = notHeaderedColumns[i - 1].ColumnName;
            }
            for (int j = 1; j <= yearsCount; j++)
            {


               for (int i = ((j - 1) * HeaderedColumns.Count()) + notHeaderedColumns.Count(), headeredindex = 0; i < (HeaderedColumns.Count() + ((j - 1) * HeaderedColumns.Count()) + notHeaderedColumns.Count()); i++, headeredindex++)
               {

                  worksheet.Cells[startrow, i + 1].Value = HeaderedColumns[headeredindex].ColumnName;
                  worksheet.Cells[startrow, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                  worksheet.Cells[startrow, i + 1].Style.Fill.BackgroundColor.SetColor(colFromHex);
                  worksheet.Cells[startrow, i + 1].Style.Font.Color.SetColor(Color.White);

                  worksheet.Cells[startrow, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                  worksheet.Cells[startrow, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                  worksheet.Cells[startrow, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                  worksheet.Cells[startrow, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                  worksheet.Cells[startrow, i + 1].Style.Border.Top.Color.SetColor(bordercolor);
                  worksheet.Cells[startrow, i + 1].Style.Border.Bottom.Color.SetColor(bordercolor);
                  worksheet.Cells[startrow, i + 1].Style.Border.Right.Color.SetColor(bordercolor);
                  worksheet.Cells[startrow, i + 1].Style.Border.Left.Color.SetColor(bordercolor);


               }
            }
            for (int a = notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount) + 1, headeredindex = 0; a <= DescriptionColumns.Count() + notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount); a++, headeredindex++)
            {
               worksheet.Cells[startrow, a].Value = DescriptionColumns[headeredindex].ColumnName;
               worksheet.Cells[startrow, a].Style.Fill.PatternType = ExcelFillStyle.Solid;
               worksheet.Cells[startrow, a].Style.Fill.BackgroundColor.SetColor(colFromHex);
               worksheet.Cells[startrow, a].Style.Font.Color.SetColor(Color.White);

               worksheet.Cells[startrow, a].Style.Border.Top.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[startrow, a].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[startrow, a].Style.Border.Left.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[startrow, a].Style.Border.Right.Style = ExcelBorderStyle.Thin;
               worksheet.Cells[startrow, a].Style.Border.Top.Color.SetColor(bordercolor);
               worksheet.Cells[startrow, a].Style.Border.Bottom.Color.SetColor(bordercolor);
               worksheet.Cells[startrow, a].Style.Border.Right.Color.SetColor(bordercolor);
               worksheet.Cells[startrow, a].Style.Border.Left.Color.SetColor(bordercolor);

            }


            #region [FILL DATA]

            var rows = tableContent.Select(d => d.ItemNumber).OrderBy(d => d).Distinct().ToList();
            startrow = startrow + 1;
            var _startrow = startrow + 1;
            for (int _row = 0; _row < rows.Count(); _row++)
            {

               _startrow = startrow + _row;
               var qtDataCells = tableContent.Where(d => d.ItemNumber == rows[_row]).ToList();
               for (int i = 1; i <= notHeaderedColumns.Count(); i++)
               {

                  worksheet.Cells[_startrow, i].Value = qtDataCells.FirstOrDefault(w => w.ColumnId == notHeaderedColumns[i - 1].columnId).Value;
               }


               for (int j = 1; j <= yearsCount; j++)
               {


                  for (int i = ((j - 1) * HeaderedColumns.Count()) + notHeaderedColumns.Count(), headeredindex = 0; i < (HeaderedColumns.Count() + ((j - 1) * HeaderedColumns.Count()) + notHeaderedColumns.Count()); i++, headeredindex++)
                  {


                     worksheet.Cells[_startrow, i + 1].Value = qtDataCells.FirstOrDefault(w => w.ColumnId == HeaderedColumns[headeredindex].columnId && w.TenderFormHeaderId == j).Value;
                  }
               }





               for (int a = notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount), headeredindex = 0; a < DescriptionColumns.Count() + notHeaderedColumns.Count() + (HeaderedColumns.Count() * yearsCount); a++, headeredindex++)
               {
                  worksheet.Cells[_startrow, a + 1].Value = qtDataCells.FirstOrDefault(w => w.ColumnId == DescriptionColumns[headeredindex].columnId).Value;// DescriptionColumns[headeredindex].ColumnName;
               }

            }
            #endregion
            fileContents = package.GetAsByteArray();
         }
         return File(
             fileContents: fileContents,
             contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
             fileDownloadName: "" + excelHeader.activityName + " - " + excelHeader.formName + ".xlsx"
         );
      }


      private IActionResult GenerateExcell(List<TenderQuantityItemDTO> tableContent)
      {
         if (tableContent.Count == 0)
         {
            AddError("الجدول لا تحتوى على اصناف");
            return Redirect(Request.Headers["Referer"].ToString());
         }
         var yearWord = new List<string> { "السنة الاولى", "السنة الثانية", "السنة الثالثة", "السنة الرابعة", "السنة الخامسة", "السنة السادسة", "السنة السابعة", "السنة الثامنة", "السنة التاسعة", "السنة العاشرة" };
         tableContent = tableContent.Where(s => s.ColumnName != "مرفقات").OrderBy(a => a.ItemNumber).ThenBy(a => a.ColumnId).ThenBy(a => a.TenderFormHeaderId).ToList();
         var cellCount = tableContent.Where(s => s.ColumnName != "مرفقات").GroupBy(t => t.ItemNumber).Select(a => a.Count()).ToList().Max();
         byte[] fileContents;
         int rowIndex = 1;
         int colIndex = 1;
         using (var package = new ExcelPackage())
         {
            var worksheet = package.Workbook.Worksheets.Add("Sheet1");
            worksheet.View.RightToLeft = true;
            if (tableContent.Any(a => a.TenderFormHeaderId > 0))
            {
               for (var i = 1; i <= cellCount; i++)
               {
                  if (tableContent[i - 1].ColumnName != "مرفقات" && tableContent[i - 1].TenderFormHeaderId > 0)
                  {
                     worksheet.Cells[1, i].Value = yearWord[(int)tableContent[i - 1].TenderFormHeaderId.Value - 1];
                     worksheet.Cells[1, i].Style.Font.Size = 12;
                     worksheet.Cells[1, i].Style.Font.Bold = true;
                     worksheet.Cells[1, i].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                  }
               }
               rowIndex++;
            }
            for (var i = 1; i <= cellCount; i++)
            {
               if (tableContent[i - 1].ColumnName != "مرفقات")
               {
                  if (tableContent.Any(a => a.TenderFormHeaderId > 0) && tableContent[i - 1].TenderFormHeaderId == 0)
                  {
                     worksheet.Cells[rowIndex - 1, i, rowIndex, i].Merge = true;
                     worksheet.Cells[rowIndex - 1, i, rowIndex, i].Value = tableContent[i - 1].ColumnName;
                     worksheet.Cells[rowIndex - 1, i, rowIndex, i].Style.Font.Size = 12;
                     worksheet.Cells[rowIndex - 1, i, rowIndex, i].Style.Font.Bold = true;
                     worksheet.Cells[rowIndex - 1, i, rowIndex, i].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                  }
                  else
                  {
                     worksheet.Cells[rowIndex, i].Value = tableContent[i - 1].ColumnName;
                     worksheet.Cells[rowIndex, i].Style.Font.Size = 12;
                     worksheet.Cells[rowIndex, i].Style.Font.Bold = true;
                     worksheet.Cells[rowIndex, i].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                  }
               }
            }
            rowIndex++;
            foreach (var item in tableContent)
            {
               if (tableContent[colIndex - 1].ColumnName != "مرفقات")
               {
                  worksheet.Cells[rowIndex, colIndex].Value = item.Value;
                  worksheet.Cells[rowIndex, colIndex].Style.Font.Size = 12;
                  worksheet.Cells[rowIndex, colIndex].Style.Font.Bold = true;
                  worksheet.Cells[rowIndex, colIndex].Style.Border.Top.Style = ExcelBorderStyle.Hair;
               }
               colIndex++;
               if (colIndex > cellCount)
               {
                  rowIndex++;
                  colIndex = 1;
               }
            }
            fileContents = package.GetAsByteArray();
         }
         if (fileContents == null || fileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
             fileContents: fileContents,
             contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
             fileDownloadName: "Exported_Quantity_Table.xlsx"
         );
      }
      [HttpPost]
      public IActionResult GetTenderQuantityTableViewComponent(int tenderId, int tableId, int formId, bool isReadOnly)
      {
         return ViewComponent("TenderQuantityTable", new { tenderId, tableId = tableId.ToString(), formId, isReadOnly });
      }
      [HttpPost]
      public IActionResult GetTenderQuantityTableChangesViewComponent(int tenderId, int tableId, int formId, bool isReadOnly, bool isNewTable, bool isTableDeleted)
      {
         return ViewComponent("TenderQuantityTableChanges", new { tenderId, tableId = tableId.ToString(), formId, isReadOnly, isNewTable, isTableDeleted });
      }
      [HttpPost]
      public async Task<IActionResult> ImportTenderTableQuantityItemsAsync(long tableId, int formId, string tableName, int tenderId, int years, bool isNewTable)
      {
         try
         {
            IFormFile file = Request.Form.Files[0];
            string folderName = "Upload";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            if (!Directory.Exists(newPath))
            {
               Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
               using (var memoryStream = new MemoryStream())
               {
                  await file.CopyToAsync(memoryStream);
                  byte[] buffer = new byte[memoryStream.Length];
                  buffer = memoryStream.ToArray();
                  UploadTableExcelModel model = new UploadTableExcelModel
                  {
                     FileNameField = file.Name,
                     FileContentField = buffer,
                     FileLengthField = buffer.Length.ToString(),
                     TableId = tableId,
                     FormId = formId,
                     TableName = tableName,
                     TenderId = tenderId,
                     Years = years,
                     IsNewTable = isNewTable
                  };
                  await _ApiClient.PostAsync("Tender/ImportTenderTableQuantityItemsAsync", null, model);
               }
            }
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
      public async Task RemoveAttachmentChangeByIdAsync(int attachmentId)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/RemoveAttachmentChangeById/" + attachmentId, null, null);
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
         }
         catch (AuthorizationException ex)
         { throw ex; }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message + "\n" + ex.StackTrace);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.ViewAddedValuePolicy)]
      public async Task<IActionResult> EditAddedValue()
      {
         try
         {
            var tenderTypes = await _ApiClient.GetListAsync<TenderTypeWithAddedValueModel>("Tender/GetAllTenderTypesAddedValue/", null);
            return View(tenderTypes);
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
      [Authorize(RoleNames.EditAddedValuePolicy)]
      public async Task<IActionResult> EditAddedValue(List<TenderTypeWithAddedValueModel> tenderTypes)
      {
         try
         {
            await _ApiClient.PostAsync("Tender/UpdateTenderTypesAddedValues", null, tenderTypes);
            AddMessage(Resources.TenderResources.Messages.DataSaved);
            return View(tenderTypes);
         }
         catch (AuthorizationException ex)
         {
            return View(tenderTypes);
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return View(tenderTypes);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [HttpGet]
      public async Task<IActionResult> GenerateQuantityTableTemplateExcel_New(int stageId, int formId, int yearsCount)
      {
         var data = await _ApiClient.GetAsync<ExcelHeader>($"Tender/GenerateQuantityTableTemplateExcel_New/{stageId}/{formId}/{yearsCount}", null);

         return null;
      }


      [HttpGet]
      public async Task<IActionResult> GenerateQuantityTableTemplateExcel(int stageId, int formId, int yearsCount)
      {
         var data = await _ApiClient.GetAsync<QuantityTableModelForExcel>($"Tender/GenerateQuantityTableTemplateExcel/{stageId}/{formId}/{yearsCount}", null);
         var stream = GetDataStream(data);
         var memoryStream = new MemoryStream(stream.ToArray());
         return new FileStreamResult(memoryStream, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"))
         {
            FileDownloadName = "form_" + data.FormName + ".xlsx"
         };

      }

      private static MemoryStream GetDataStream(QuantityTableModelForExcel quantity)
      {
         var pck = CreateExcelPackage(quantity);
         var stream = new MemoryStream();
         // processing the stream.
         pck.SaveAs(stream);
         return stream;
      }
      private static ExcelPackage CreateExcelPackage(QuantityTableModelForExcel quantityTableModel)
      {
         var package = new ExcelPackage();
         ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(quantityTableModel.FormName);
         worksheet.View.RightToLeft = true;
         int Index = 1;
         int startColumn = Index;
         if (quantityTableModel.HeadrsCount > 1)
         {

            foreach (var column in quantityTableModel.NotrepeatedCols.OrderBy(d => d.DisplayOrder).ToList())
            {
               worksheet.Cells[1, Index, 2, Index].Merge = true;
               worksheet.Cells[1, Index, 2, Index].Value = column.ColumnName;
               Index++;
            }
            foreach (var header in quantityTableModel.TenderFormHeaders)
            {
               startColumn = Index;

               foreach (var column in quantityTableModel.RepeatedCols.OrderBy(d => d.DisplayOrder).ToList())
               {
                  worksheet.Cells[2, Index].Value = column.ColumnName;
                  Index++;
               }
               worksheet.Cells[1, startColumn, 1, Index - 1].Merge = true;
               worksheet.Cells[1, startColumn, 1, Index - 1].Value = header.Name;
            }

            foreach (var column in quantityTableModel.DescriptionCols.OrderBy(d => d.DisplayOrder).ToList())
            {
               worksheet.Cells[1, Index, 2, Index].Merge = true;
               worksheet.Cells[1, Index, 2, Index].Value = column.ColumnName;
               Index++;
            }
         }
         else
         {
            foreach (var column in quantityTableModel.Columns.OrderBy(d => d.DisplayOrder).ToList())
            {
               worksheet.Cells[1, Index].Value = column.ColumnName;
               Index++;
            }
         }
         package = ConvertToCsv(package);
         return package;
      }
      public static ExcelPackage ConvertToCsv(ExcelPackage package)
      {
         var worksheet = package.Workbook.Worksheets[0];
         worksheet.View.RightToLeft = true;
         var maxColumnNumber = worksheet.Dimension.End.Column;
         var currentRow = new List<string>(maxColumnNumber);
         var totalRowCount = worksheet.Dimension.End.Row;
         var currentRowNum = 1;
         return package;
      }

      [HttpGet]
      public async Task<IActionResult> GetAllMandatoryListProductsForExport()
      {
         var result = await _ApiClient.GetListAsync<MandatoryListForExportModel>("Tender/GetAllMandatoryListProductsForExport", null);
         var stream = new MemoryStream();
         using (var package = new ExcelPackage(stream))
         {
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.LoadFromCollection(result, true);
            workSheet.Cells[1, 1].Value = "Division Name and Number(English)";
            workSheet.Cells[1, 2].Value = "اسم ورقم القطاع الرئيسي(عربي)";
            workSheet.Cells[1, 3].Value = " CSI CODE الرمز الإنشائي  الذي ينتمي له المنتج/ البند";
            workSheet.Cells[1, 4].Value = "Product / Item Name(English)";
            workSheet.Cells[1, 5].Value = "اسم المنتج / البند(عربي)";
            workSheet.Cells[1, 6].Value = "Product / Item Definition(English)";
            workSheet.Cells[1, 7].Value = "تعريف المنتج / البند(عربي)";
            workSheet.Cells[1, 8].Value = "Price Ceiling   السقف السعري ";
            int row = 2;
            for (int i = 0; i < result.Count; i++)
            {
               workSheet.Cells[row, 1].Value = result[i].DivisionNameEn;
               workSheet.Cells[row, 2].Value = result[i].DivisionNameAr;
               workSheet.Cells[row, 3].Value = result[i].CSICode;
               workSheet.Cells[row, 4].Value = result[i].NameEn;
               workSheet.Cells[row, 5].Value = result[i].NameAr;
               workSheet.Cells[row, 6].Value = result[i].DescriptionEn;
               workSheet.Cells[row, 7].Value = result[i].DescriptionAr;
               workSheet.Cells[row, 8].Value = result[i].Price;
               row++;
            }
            for (int x = 1; x < 9; x++)
            {
               workSheet.Cells[1, x].Merge = true;
               workSheet.Cells[1, x].Style.Font.Size = 12;
               workSheet.Cells[1, x].Style.Font.Bold = true;
               workSheet.Cells[1, x].Style.Border.Top.Style = ExcelBorderStyle.Hair;
               workSheet.Cells[1, x].Style.WrapText = true;
               workSheet.DefaultColWidth = 20;
            }
            workSheet.View.RightToLeft = true;
            workSheet.Row(1).Height = 15 * 3;
            workSheet.Cells.AutoFitColumns();
            package.Save();
         }

         stream.Position = 0;
         string excelName = "MandatoryList" + ".xlsx";
         return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
      }

      public async Task<bool> CheckIfActivityCabHasTawreed(ActivityVersionModel activityVersionModel)
      {
         try
         {
            var check = await _ApiClient.GetAsync<bool>("Tender/CheckIfActivityCabHasTawreed", activityVersionModel.ToDictionary());
            return check;
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
   }
}

