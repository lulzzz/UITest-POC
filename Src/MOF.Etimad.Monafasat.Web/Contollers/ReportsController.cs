using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class ReportsController : BaseController
   {
      private readonly IWebHostEnvironment _hostingEnvironment;
      private IApiClient _ApiClient;

      private readonly ILogger<BlockController> _logger;

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetFinancialYear()
      {
         var result = await _ApiClient.GetListAsync<string>("Reports/GetFinancialYear", null);
         return Json(result);
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTenderStatuses()
      {
         var result = await _ApiClient.GetAsync<Dictionary<int, string>>("Reports/GetTenderStatuses", null);
         return Json(result);
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTendersName()
      {
         var result = await _ApiClient.GetAsync<Dictionary<int, string>>("Reports/GetTendersName", null);
         return Json(result);
      }

      public ReportsController(IWebHostEnvironment hostingEnvironment, IApiClient apiClient, ILogger<BlockController> logger, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = apiClient;
         _logger = logger;
      }

      [Authorize(RoleNames.ReportsPolicy)]
      [HttpGet]
      public async Task<ActionResult> TenderValuesToTypesReport()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<List<TenderValueToTypesModel>> TenderValuesToTypesReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         //var result = await TenderProxy.TenderValueToTypesReport(searchCriteria);
         var result = await _ApiClient.GetListAsync<TenderValueToTypesModel>("Reports/TenderValueToTypesReport", searchCriteria.ToDictionary());
         return result;
      }
      [Authorize(RoleNames.ReportsPolicy)]
      [HttpGet]
      public async Task<ActionResult> TenderCountToTypesReport()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<List<TenderValueToTypesModel>> TenderCountToTypesReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         //var result = await TenderProxy.TenderCountToTypesReport(searchCriteria);
         var result = await _ApiClient.GetListAsync<TenderValueToTypesModel>("Reports/TenderCountToTypesReport", searchCriteria.ToDictionary());
         return result;
      }
      [Authorize(RoleNames.ReportsPolicy)]
      [HttpGet]
      public async Task<ActionResult> TendersToAwardedSuppliersReport()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TendersToAwardedSuppliersReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            var result = await _ApiClient.GetListAsync<TenderValueToTypesModel>("Reports/TendersToAwardedSuppliersReport", searchCriteria.ToDictionary());
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
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public ActionResult QualificationReport()
      {

         return View();


      }


      [Authorize(RoleNames.ReportsPolicy)]
      public ActionResult QualificationCount()
      {

         return View();


      }
      [Authorize(RoleNames.ReportsPolicy)]
      public ActionResult QualificationReportCountList()
      {

         return View();


      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> QualificationCountAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {


         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetListAsync<QualificationCountModel>("Reports/QualificationCountAsync", tenderSearchCriteria.ToDictionary());

         return Json(result);


      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> QualificationCountListAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {


         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetQueryResultAsync<QualificationCountModel>("Reports/QualificationCountListAsync", tenderSearchCriteria.ToDictionary());

         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));


      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> QualificationReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<QualificationModel>("Reports/QualificationReportAsyncReport", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DownloadQualificationReport(TenderValueToTypeSearchCriteria searchCriteria)
      {

         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Reports/DownloadQualificationReport", searchCriteria.ToDictionary());

         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.QualificationResources.DisplayInputs.Qualification + ".xlsx"
         );


      }



      [HttpGet]
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> AmountOfSavingsReport()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> AmountOfSavingsReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         //var result = await TenderProxy.DailyNotificationsList(searchCriteria);
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         var result = await _ApiClient.GetQueryResultAsync<AmountOfSavingsViewModel>("Reports/GetAmountOfSavingsAsync", searchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TotalAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         //var result = await TenderProxy.DailyNotificationsList(searchCriteria);
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         var result = await _ApiClient.GetAsync<TotalAmountOfSavingsViewModel>("Reports/GetTotalAmountOfSavingsAsync", searchCriteria.ToDictionary());
         return Json(result);
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public ActionResult DirectPurchaseReport()
      {

         return View();


      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DirectPurchaseReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<DirectPurchaseModel>("Reports/DirectPurchaseReportAsyncReport", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }







      [Authorize(RoleNames.ReportsPolicy)]
      [HttpGet]
      public async Task<ActionResult> ReportTendersToActivity()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<List<TenderValueToTypesModel>> ReportTendersToActivityAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         //var result = await TenderProxy.ReportTendersToActivity(searchCriteria);
         var result = await _ApiClient.GetListAsync<TenderValueToTypesModel>("Reports/ReportTendersToActivity", searchCriteria.ToDictionary());
         return result;
      }

      public DateTime? GetDate(string hijriOrGregDate)
      {
         HijriCalendar hijriCal = new HijriCalendar();
         GregorianCalendar gregCal = new GregorianCalendar();
         if (!string.IsNullOrEmpty(hijriOrGregDate))
         {
            string[] date = hijriOrGregDate.Split(',');

            //var lastEnqueriesDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), persianCal);
            if (date[3] == "H")
            {
               DateTime persianDate = hijriCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
               persianDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), hijriCal);
               return persianDate;
            }
            else
            {
               DateTime persianDate = gregCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
               persianDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), gregCal);
               return persianDate;
            }
         }
         else
            return null;
      }


      #region Reports 
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderReportPagingAsync(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;
         var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Reports/GetTendersReportList", tenderSearchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<IActionResult> DownloadTenderReport(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;

         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Reports/DownloadTenderReport", tenderSearchCriteria.ToDictionary());

         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.TenderResources.DisplayInputs.TenderReport + ".xlsx"
         );

      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<IActionResult> DownloadTenderPurchaseReport(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;

         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Reports/DownloadTenderPurchaseReport", tenderSearchCriteria.ToDictionary());

         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.SharedResources.DisplayInputs.ReportPuarshases + ".xlsx"
         );

      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderReport()
      {
         return View();
      }


      [HttpGet]
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> MostSuppliersHaveTendersReport()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> MostSuppliersHaveTendersReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         //var result = await TenderProxy.MostSuppliersHaveTenderReport(searchCriteria);
         var result = await _ApiClient.GetQueryResultAsync<MostSuppliersHaveTendersModel>("Reports/Report_GetMostSuppliersHaveTendersAsync", searchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));

      }


      [HttpGet]
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DailyNotificationsListReport()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DailyNotificationsListReportAsync(NotifySearchCriteria searchCriteria)
      {
         //var result = await TenderProxy.DailyNotificationsList(searchCriteria);
         var result = await _ApiClient.GetQueryResultAsync<NotifyModel>("Reports/DailyNotificationsList", searchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }




      [HttpGet]
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TendersSummaryReport()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTendersSummaryAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         //var result = await TenderProxy.DailyNotificationsList(searchCriteria);
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         var result = await _ApiClient.GetQueryResultAsync<TendersSummryReportViewModel>("Reports/GetTendersSummaryAsync", searchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }
      [HttpGet]
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> AgencyFileReport()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> AgencyFileReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         //var result = await TenderProxy.DailyNotificationsList(searchCriteria);
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         var result = await _ApiClient.GetQueryResultAsync<AgencyFileViewModel>("Reports/AgencyFileReport", searchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DownloadAgencyFileReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;

         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Reports/DownloadAgencyFileReportAsync", searchCriteria.ToDictionary());

         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.ReportResources.DisplayInputs.AgencyFileReport + ".xlsx"
         );
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<IActionResult> DownloadTenderSummaryReport(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         //tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         //tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;

         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Reports/DownloadTenderSummaryReport", tenderSearchCriteria.ToDictionary());

         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.TenderResources.DisplayInputs.TendersSummryReport + ".xlsx"
         );

      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTendersSummaryCountsAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         //var result = await TenderProxy.DailyNotificationsList(searchCriteria);
         searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
         searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
         var result = await _ApiClient.GetAsync<TendersSummryReportCountsViewModel>("Reports/GetTendersSummaryCountsAsync", searchCriteria.ToDictionary());
         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderPurshasesReportPagingAsync(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;
         var result = await _ApiClient.GetQueryResultAsync<PurchaseModel>("Reports/GetTendersPurshasesReportList", tenderSearchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderPurshasesReportTotalAmount(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;
         var result = await _ApiClient.GetAsync<decimal>("Reports/GetTenderPurshasesReportTotalAmount", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderPurshasesReport()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderCountsReportPagingAsync(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;

         //var result = await TenderProxy.GetTendersCountReportList(tenderSearchCriteria);
         var result = await _ApiClient.GetQueryResultAsync<TenderCountsModel>("Reports/GetTendersCountReportList", tenderSearchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> TenderCountsReport()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> SupplierPurshasesReportPagingAsync([FromQuery] TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;

         //var result = await TenderProxy.FindSuppliersPurshaseReport(tenderSearchCriteria);
         var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Reports/FindSuppliersPurshaseReport", tenderSearchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<IActionResult> DownloadSupplierPurchaseReport(TenderSearchCriteriaModel tenderSearchCriteria)
      {

         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;
         var downloadFileContents = await _ApiClient.GetAsync<byte[]>("Reports/DownloadSupplierPurchaseReportAsync", tenderSearchCriteria.ToDictionary());

         if (downloadFileContents == null || downloadFileContents.Length == 0)
         {
            return NotFound();
         }
         return File(
            fileContents: downloadFileContents,
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: Resources.SharedResources.DisplayInputs.PurshasesReport + ".xlsx"
         );
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> SupplierPurshasesReport()
      {
         try
         {
            QueryResult<SupplierModel> suppliers = await _ApiClient.GetQueryResultAsync<SupplierModel>("Invitation/GetAllSuppliersData", null);

            ViewBag.ListOfSuppliers = suppliers;

            var worksItems = new List<SelectListItem>();
            foreach (var item in suppliers.Items)
            {
               worksItems.Add(new SelectListItem { Value = item.CommericalRegisterNo.ToString(), Text = item.CommericalRegisterName });
            }

            ViewBag.ListOfSuppliers = worksItems;

            return View();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("Index", "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("Index", "Tender");
         }
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportMostTendersActivitiesAsync()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportGetMostTendersActivitiesAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         //if (tenderSearchCriteria.AgencyCode == null | tenderSearchCriteria.AgencyCode <= 0)
         //   tenderSearchCriteria.AgencyCode = Convert.ToInt32(User.UserAgency());
         //var result = TenderProxy.ReportGetMostTendersActivitiesAsync(tenderSearchCriteria);
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetListAsync<MostTendersActivitiesModel>("Reports/ReportGetMostTendersActivitiesAsync", tenderSearchCriteria.ToDictionary());

         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public ActionResult MessagesStatusReport(TenderValueToTypeSearchCriteria tenderSearchCriteria)

      {


         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> MessagesStatusReportAsync(SearchCriteria searchCriteria)
      {

         // var result = await _ApiClient.GetListAsync<MessagesModel>("Reports/MessagesStatusReportAsync", searchCriteria.ToDictionary());

         var result = await _ApiClient.GetQueryResultAsync<MessagesModel>("Reports/MessagesStatusReportAsync", searchCriteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));



      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportMostSuppliersActivitiesAsync()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportGetMostSuppliersActivitiesAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         //if (tenderSearchCriteria.AgencyCode == null | tenderSearchCriteria.AgencyCode <= 0)
         //   tenderSearchCriteria.AgencyCode = Convert.ToInt32(User.UserAgency());
         //var result = TenderProxy.ReportGetMostSuppliersActivitiesAsync(tenderSearchCriteria);
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetListAsync<MostTendersActivitiesModel>("Reports/ReportGetMostSuppliersActivitiesAsync", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportMostSuppliersHaveTendersAsync()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportGetMostSuppliersHaveTendersAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         //if (tenderSearchCriteria.AgencyCode == null | tenderSearchCriteria.AgencyCode <= 0)
         //   tenderSearchCriteria.AgencyCode = Convert.ToInt32(User.UserAgency());
         //var result = TenderProxy.ReportGetMostSuppliersHaveTendersAsync(tenderSearchCriteria);
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetListAsync<MostSuppliersHaveTendersModel>("Reports/ReportGetMostSuppliersHaveTendersAsync", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportTendersSalesAsync()
      {
         //var tenders = await TenderProxy.GetMyTenderAsync();
         var tenders = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Tender/GetTendersByLogedUser", null);
         ViewBag.Tenders = tenders.Items.Select(t => new SelectListItem { Text = t.TenderName, Value = t.TenderId.ToString() }).ToList();
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportGetTendersSalesAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         //if (tenderSearchCriteria.AgencyCode == null | tenderSearchCriteria.AgencyCode <= 0)
         //   tenderSearchCriteria.AgencyCode = Convert.ToInt32(User.UserAgency());
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;

         var result = await _ApiClient.GetListAsync<TendersSalesModel>("Reports/ReportGetTendersSalesAsync", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportPublishedTendersAsync()
      {
         return View();
      }

      [Authorize(RoleNames.AdminAndAccountManagerPolicy)]
      public async Task<ActionResult> ReportGetPublishedTendersAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         //if (tenderSearchCriteria.AgencyCode == null | tenderSearchCriteria.AgencyCode <= 0)
         //   tenderSearchCriteria.AgencyCode = Convert.ToInt32(User.UserAgency());
         //var result = await TenderProxy.ReportGetPublishedTendersAsync(tenderSearchCriteria);
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetListAsync<TendersPublishingModel>("Reports/ReportGetPublishedTendersAsync", tenderSearchCriteria.ToDictionary());

         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportTenderCountsReportAsync()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportCountTenderCountsReportAsync(TenderSearchCriteriaModel tenderSearchCriteria)
      {
         tenderSearchCriteria.FromLastOfferPresentationDate = tenderSearchCriteria.FromLastOfferPresentationDateString;
         tenderSearchCriteria.ToLastOfferPresentationDate = tenderSearchCriteria.ToLastOfferPresentationDateString;

         var result = await _ApiClient.GetListAsync<TenderCountsModel>("Reports/ReportGetTendersCountReportAsync", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> AgencyStatisticsReportPagingAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;

         //AgencyTenderStatisticsModel result = await TenderProxy.FindAgencyStatisticsReport(tenderSearchCriteria);
         AgencyTenderStatisticsModel result = await _ApiClient.GetAsync<AgencyTenderStatisticsModel>("Reports/ReportGetCountsStatisticsAsync", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportAgencyStatistics()
      {
         return View();
      }
      #endregion Reports

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DirectInvitationsReport()
      {
         return View();
      }
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> DirectInvitationsReportAsync(TenderValueToTypeSearchCriteria tenderSearchCriteria)
      {
         tenderSearchCriteria.FromCreatedDate = tenderSearchCriteria.FromCreatedDateString;
         tenderSearchCriteria.ToCreatedDate = tenderSearchCriteria.ToCreatedDateString;
         var result = await _ApiClient.GetListAsync<DirectInvitationsReportModel>("Reports/DirectInvitationsReport", tenderSearchCriteria.ToDictionary());
         return Json(result);
      }
      [HttpGet]
      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetBranchByAgency(string AgencyCode)
      {
         try
         {
            if (String.IsNullOrEmpty(AgencyCode) || AgencyCode == "undefined")
            {
               AgencyCode = "0";
            }
            var roles = await _ApiClient.GetListAsync<BranchModel>("Branch/GetBranchByAgency/" + AgencyCode, null);

            return Json(roles);

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
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      #region TenderMonthlySalesReceipteReport

      [Authorize(RoleNames.ReportsPolicy)]
      public ActionResult TenderSalesMonthlyReport()
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTenderSalesMonthlyCountsPerMonthAsync(TenderValueToTypeSearchCriteria searchCriteria)
      { //
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetAsync<TenderSalesMonthlyCountsPerMonth>("Reports/GetTenderSalesMonthlyCountsPerMonth", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTenderSalesMonthlyRecipetReportPerAgencyAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<TenderSalesMonthlyRecipetReportPerAgency>("Reports/GetTenderSalesMonthlyRecipetReportPerAgency", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetAllTenderSalesMonthlyCountsPerMonthAsync(TenderValueToTypeSearchCriteria searchCriteria)
      { //
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetAsync<TenderSalesMonthlyCountsPerMonth>("Reports/GetAllTenderSalesMonthlyCountsPerMonth", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> GetTenderSalesMonthlyTenderDetailsAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<TenderSalesMonthlyTenderDetails>("Reports/GetTenderSalesMonthlyTenderDetails", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> PerformanceReport(TenderValueToTypeSearchCriteria searchCriteria)
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> PerformanceReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<PerformanceReportModel>("Reports/PerformanceReportAsync", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportSpendingBySpendAgency(TenderValueToTypeSearchCriteria searchCriteria)
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportSpendingBySpendAgencyAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<ReportSpendingBySpendAgencyModel>("Reports/ReportSpendingBySpendAgencyAsync", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }


      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportSpendingBySpendCategory(TenderValueToTypeSearchCriteria searchCriteria)
      {
         return View();
      }

      [Authorize(RoleNames.ReportsPolicy)]
      public async Task<ActionResult> ReportSpendingBySpendCategoryAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<ReportSpendingBySpendAgencyModel>("Reports/ReportSpendingBySpendCategoryAsync", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }




      public async Task<ActionResult> ReportSpendingBySpendActivities(TenderValueToTypeSearchCriteria searchCriteria)
      {
         return View();
      }


      public async Task<ActionResult> ReportSpendingBySpendActivitiesAsync(TenderValueToTypeSearchCriteria searchCriteria)
      {
         try
         {
            searchCriteria.FromCreatedDate = searchCriteria.FromCreatedDateString;
            searchCriteria.ToCreatedDate = searchCriteria.ToCreatedDateString;
            var result = await _ApiClient.GetQueryResultAsync<ReportSpendingBySpendAgencyModel>("Reports/ReportSpendingBySpendActivitiesAsync", searchCriteria.ToDictionary());
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
            //_logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }
      #endregion TenderMonthlySalesReceipteReport

   }
}

