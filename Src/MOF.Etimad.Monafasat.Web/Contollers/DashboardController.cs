using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class DashboardController : BaseController
   {
      private readonly IApiClient _ApiClient;
      private readonly IWebHostEnvironment _hostingEnvironment;

      public DashboardController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
      }

      [Authorize(RoleNames.DashboardPolicy)]
      public async Task<ActionResult> Index()
      {
         if (User.IsInRole(RoleNames.LocalContentOfficer))
            return RedirectToAction("Index", "LocalContentSettings");

         if (User.IsInRole(RoleNames.MandatoryListOfficer) || User.IsInRole(RoleNames.MandatoryListApprover))
            return RedirectToAction("Index", "MandatoryList");

         if (User.IsInRole(RoleNames.ProductManager) || User.IsInRole(RoleNames.ProductManagerDisplay))
            return RedirectToAction("EditAddedValue", "Tender");

         if (User.IsInRole(RoleNames.UnitSpecialistLevel1) || User.IsInRole(RoleNames.UnitSpecialistLevel2) || User.IsInRole(RoleNames.UnitSecretaryUser) || User.IsInRole(RoleNames.UnitManagerUser))
            return RedirectToAction(nameof(UnitUsersDashboardIndexAsync));
         var roles = ((ClaimsIdentity)User.Identity).Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
         if (roles.Count() == 0)
            return RedirectToAction("AllTendersForVisitor", "Tender");

         if (User.IsInRole(RoleNames.MonafasatAccountManager) || User.IsInRole(RoleNames.CustomerService) || User.IsInRole(RoleNames.FinancialSupervisor) || roles.Count() == 0)
            return RedirectToAction("Index", "Tender");
         if (User.IsInRole(RoleNames.UnAssingedUser))
         {
            return RedirectToAction("UnAssingedUser", "Home");
         }
         DashboardIndexModel model = new DashboardIndexModel();
         return View(model);
      }

      #region Unit 

      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<ActionResult> UnitUsersDashboardIndexAsync()
      {
         DashboardIndexModel model = new DashboardIndexModel();
         return View("/Views/Dashboard/Unit/UnitUsersDashboardIndexAsync.cshtml", model);
      }

      [HttpGet]
      [Authorize(RoleNames.UnitSpecialistsAndManagerUserPolicy)]
      public async Task<ActionResult> TendersWaitingTheUnitActionAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetQueryResultAsync<TenderWaitingTheUnitActionViewModel>("Dashboard/TendersWaitingTheUnitActionAsync", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      #endregion

      [Authorize(RoleNames.DashboardRejectedTendersPolicy)]
      [HttpGet]
      public async Task<ActionResult> RejectedTendersPagingAsync(int? PageNumber, string Sort)
      {
         QueryResult<RejectTenderViewModel> result = new QueryResult<RejectTenderViewModel>(new List<RejectTenderViewModel>(), 0, 0, 0);
         TempData["DashboardRejectedTenders"] = "DashboardRejectedTenders";
         if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersForDataEntry", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersOppeningSecretary))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersForOpeningStage", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersCheckSecretary))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersOfCheckAndAwardingStage", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersPurchaseSecretary) || User.IsInRole(RoleNames.OffersPurchaseManager))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersOfDirectPurchase", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.PreQualificationCommitteeSecretary))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersOfCheckQualificationStage", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.SecretaryGrievanceCommittee))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersOfGrievence", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.PrePlanningCreator))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedPreplaning", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.MonafasatBlockListSecritary))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetBlockNeedSecretaryRejected", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary))
         {
            result = await _ApiClient.GetQueryResultAsync<RejectTenderViewModel>("Dashboard/GetRejectedTendersForVROOpeningStage", new SearchCriteria() { PageNumber = PageNumber.GetValueOrDefault(1), Sort = Sort }.ToDictionary());
         }
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      [Authorize(RoleNames.DashboardPolicy)]
      [HttpGet]
      public async Task<ActionResult> RejectedTendersCountasync()
      {

         if (User.IsInRole(RoleNames.DataEntry))
         {
            await _ApiClient.GetAsync<int>("Dashboard/GetRejectedTendersCountForDataEntry", null);
         }
         else if (User.IsInRole(RoleNames.OffersOppeningSecretary))
         {
            await _ApiClient.GetAsync<int>("Dashboard/OppenedRejectedTendersCountPagingAsync", null);
         }
         else if (User.IsInRole(RoleNames.OffersCheckSecretary))
         {
            await _ApiClient.GetAsync<int>("Dashboard/GetRejectedTendersCountOfCheckAndAwardingStage", null);
         }
         else if (User.IsInRole(RoleNames.PrePlanningCreator))
         {
            await _ApiClient.GetAsync<int>("Dashboard/GetRejectedTendersCountPrePlaning", null);
         }
         else if (User.IsInRole(RoleNames.MonafasatBlockListSecritary))
         {
            await _ApiClient.GetAsync<int>("Dashboard/GetRejectedTendersCountPrePlaning", null);
         }
         return Json(1);
      }


      [Authorize(RoleNames.DataEntryAndAuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> JoiningRequestInvitationsPagingAsync(string Sort, int PageNumber = 1)
      {
         var result = await _ApiClient.GetQueryResultAsync<JoiningRequestInvitationsViewModel>("Dashboard/JoiningRequestInvitationsPagingAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }
      [Authorize(RoleNames.OffersOppeningSecretaryAndManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> OpeningNotificationsPagingAsync(string Sort, int PageNumber = 1)
      {
         var result = await _ApiClient.GetQueryResultAsync<OpeningNotificationsViewModel>("Dashboard/OpeningNotificationsPagingAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      [Authorize(RoleNames.TechnicalCommitteeUserPolicy)]
      [HttpGet]
      public async Task<ActionResult> LoadPendingEnquiriesDataAsync(string Sort, int PageNumber = 1)
      {
         var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Dashboard/GetPendingEnquiries", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }


      [Authorize(RoleNames.DashboardProcessNeedsActionPolicy)]
      [HttpGet]
      public async Task<ActionResult> ProcessNeedsActionPagingAsync(string Sort, int PageNumber = 1)
      {
         QueryResult<ProcessNeedsActionViewModel> model = new QueryResult<ProcessNeedsActionViewModel>(new List<ProcessNeedsActionViewModel>(), 0, 0, 0);
         if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.PurshaseSpecialist))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetDataEntrygProcessNeedsActionPagingAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.Auditer) || User.IsInRole(RoleNames.EtimadOfficer))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetAuditingProcessNeedsActionPagingAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersOppeningManager))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetConfirmOpeningProcessNeedsActionPagingAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersCheckManager))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetCheckingAndAwardingStageProcessNeedsAction", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.ApproveTenderAward))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetFinalAwardStageProcessNeedsAction", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersOpeningAndCheckManager))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetVROOpenCheckStageProcessNeedsAction", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersCheckSecretary))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetTendersNeedApprovalForOfferCheckSecretaryPaginAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersPurchaseManager))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetTendersNeedApprovalForDirectPurchasePaginAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.OffersPurchaseSecretary))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetTendersNeedApprovalForDirectPurchaseSecretaryPaginAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.ManagerGrievanceCommittee))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetProcessesNeedActionOfGrievence", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.PrePlanningAuditor))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetPrePlaningNeedApproval", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.MonafasatBlackListCommittee))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetBlockNeedManagerApproval", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.MonafasatBlockListSecritary))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetBlockNeedSecretaryApproval", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }
         else if (User.IsInRole(RoleNames.PreQualificationCommitteeManager))
         {
            model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetTendersNeedApprovalForPreQualificationCommitteeManagerAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());
         }

         return Json(new PaginationModel(model.Items, model.TotalCount, model.PageSize, model.PageNumber, null));
      }

      [Authorize(RoleNames.DashboardPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetTotalCountsAsync()
      {
         DashBoardTotalCount model = await _ApiClient.GetAsync<DashBoardTotalCount>("Dashboard/GetDashBoardCountsAsync", null);
         return Json(model);
      }
      [Authorize(RoleNames.AuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> PreQualificationProcessNeedsActionPagingAsync(string Sort, int PageNumber = 1)
      {
         QueryResult<ProcessNeedsActionViewModel> model = new QueryResult<ProcessNeedsActionViewModel>(new List<ProcessNeedsActionViewModel>(), 0, 0, 0);

         model = await _ApiClient.GetQueryResultAsync<ProcessNeedsActionViewModel>("Dashboard/GetAuditingPreQualificationProcessNeedsActionPagingAsync", new SearchCriteria() { PageNumber = PageNumber, Sort = Sort }.ToDictionary());

         return Json(new PaginationModel(model.Items, model.TotalCount, model.PageSize, model.PageNumber, null));
      }
      [Authorize(RoleNames.DataEntryPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetUnderEstablishedTendersAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetAsync<SalesSummaryViewModel>("Dashboard/SalesSummaryPagingAsync", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result.Sales.Items, result.Sales.TotalCount, result.Sales.PageSize, result.Sales.PageNumber, result.Total.ToString() + "," + result.PriceTotal));
      }

      [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> SalesSummaryPagingAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetAsync<SalesSummaryViewModel>("Dashboard/SalesSummaryPagingAsync", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result, result.Sales.TotalCount, result.Sales.PageSize, result.Sales.PageNumber, null));
      }

      [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> TendersSummaryPagingAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetAsync<TendersSummaryViewModel>("Dashboard/TendersSummaryPagingAsync", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result, result.Tenders.TotalCount, result.Tenders.PageSize, result.Tenders.PageNumber, null));
      }

      [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> DirectInvitationsSummaryPagingAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetAsync<DirectInvitationsSummaryViewModel>("Dashboard/DirectInvitationsSummaryPagingAsync", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result, result.DirectInvitations.TotalCount, result.DirectInvitations.PageSize, result.DirectInvitations.PageNumber, null));
      }

      [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
      [HttpGet]
      public async Task<ActionResult> SuppliersCountPagingAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetAsync<string>("Dashboard/SuppliersCountPagingAsync", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result, Convert.ToInt32(result), 10, 1, null));
      }

      [Authorize(RoleNames.DashboardPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetLastTenPurshaseAsync()
      {
         var result = await _ApiClient.GetAsync<List<LastTenPurshaseModel>>("Dashboard/GetLastTenPurshase", null);
         return Json(new PaginationModel(result, result.Count, 10, 1, null));
      }

      [Authorize(RoleNames.AuditerPolicy)]
      [HttpGet]
      public async Task<ActionResult> GetSuppliersEnquiryAsync(DashboardSearchCriteria dashboardSearchCriteriaModel)
      {
         var result = await _ApiClient.GetQueryResultAsync<SupplierEnquiryModel>("Dashboard/GetSuppliersEnquiry", dashboardSearchCriteriaModel.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }
   }
}
