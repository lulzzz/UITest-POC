using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.Reports;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Contollers
{
   public class OfferController : BaseController
   {
      private readonly IWebHostEnvironment _hostingEnvironment;
      private readonly int _pageSize = 10;
      //private static readonly string _attachmentDownloadPath = "/Offer/Download?";
      private IApiClient _ApiClient;
      private IMemoryCache _cache;
      //private readonly IConfigurationRoot _configuration;

      private readonly ILogger<OfferController> _logger;
      public OfferController(IWebHostEnvironment hostingEnvironment, IApiClient ApiClient, ILogger<OfferController> logger, IMemoryCache cache, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _hostingEnvironment = hostingEnvironment;
         _ApiClient = ApiClient;
         _logger = logger;
         _cache = cache;
         //_configuration = new ConfigurationBuilder()
         //          .SetBasePath(Directory.GetCurrentDirectory())
         //          .AddJsonFile("appsettings.json")
         //          .Build();
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> Index()
      {
         return View();
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> IndexPagingAsync(TenderSearchCriteriaModel tenderSearchCriteriaModel)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<BasicTenderModel>("Tender/GetTendersBySearchCriteria", tenderSearchCriteriaModel.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpGet("offer/OfferSummary/{OfferIdString}")]
      public async Task<ActionResult> OfferSummary(string OfferIdString)
      {
         try
         {


            OfferSummaryStatusModel statusModel = await _ApiClient.GetAsync<OfferSummaryStatusModel>("Offer/GetOfferStatusForOfferSummary/" + Util.Decrypt(OfferIdString), null);

            if (TempData["ValidationSummary"] != null)
            {
               var errorMessages = TempData["ValidationSummary"] as IEnumerable<string>;
               statusModel.IValidationSummary = errorMessages;
            }
            return View(statusModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);

            return RedirectToAction("SupplierTenders", "Tender");
         }

      }

      [Authorize(RoleNames.SupplierPolicy)]
      [HttpGet("Offer/SendOfferToApprove/{OfferIdString}")]
      public async Task<IActionResult> SendOfferToApprove(string OfferIdString)
      {
         try
         {
            var result = await _ApiClient.PostAsync<OfferSummaryStatusModel>("Offer/SendOfferToApprove", null, Util.Decrypt(OfferIdString));
            if (result.StatusId != (int)Enums.OfferStatus.Received)
            {

               AddError(" لم يتم إرسال العرض ");
               TempData["ValidationSummary"] = result.ValidationSummary;
               return RedirectToAction(nameof(OfferSummary), "Offer", new { OfferIdString });

            }
            AddMessage("تم إرسال العرض ");
            return RedirectToAction("SupplierTenders", "Tender");

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(OfferSummary), "Offer", new { OfferStringId = OfferIdString });
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(OfferSummary), "Offer", new { OfferStringId = OfferIdString });
         }
      }

      public IActionResult GetOfferAttachmentsUpdateComponent(string OfferId, string CombinedId)
      {
         return ViewComponent("OfferAttachmentsUpdate", new { offerId = OfferId, combinedId = CombinedId });
      }

      #region New Apply Offer

      [HttpPost("offer/AddSuppliertoCombinedList")]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> AddSuppliertoCombinedList(CombinedSupplierInsertModel model)
      {
         try
         {
            //AddSuppliertoCombinedList
            //AddCombinedToOffer
            var response = await _ApiClient.PostAsync<AjaxResponse<string>>("offer/AddSuppliertoCombinedList", null, model);
            return Json(response);
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
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> GetSuppliersForCombinedAsync(SupplierSearchCretriaModel model)
      {
         try
         {

            var result = await _ApiClient.GetQueryResultAsync<SupplierCombinedModelView>("Offer/GetAllSuppliersBySearchCretriaForCombined/", model.ToDictionary());

            return Json(new PaginationModel(result.Items, result.TotalCount, model.PageSize, model.PageNumber, null));
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      //[Authorize(RoleNames.SupplierPolicy)]
      //[HttpPost("Offer/SaveTable")]
      //public async Task<ActionResult> SaveTable(SaveTableModel table)
      //{
      //   try
      //   {
      //      //foreach (SaveitemModel sa in table.QuantityTableItem) { if (sa.Discount == null || sa.Discount == 0) sa.Discount = decimal.Parse("0.0"); }
      //      if (!ModelState.IsValid)
      //      {
      //         return JsonErrorMessage(Resources.OfferResources.ErrorMessages.EnterPrice);
      //      }
      //      table.TenderId = Util.Decrypt(table.TenderId).ToString();
      //      var result = await _ApiClient.PostAsync<OfferModel>("offer/SaveTable", null, table);
      //      return Json(result);
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

      //[Authorize(RoleNames.ApplyOffersPolicy)]
      //[HttpGet("Offer/ApplyOffer/{tenderId}/{offerid?}")]
      //public async Task<ActionResult> ApplyOffer(string tenderId, string offerid)
      //{
      //   try
      //   {
      //      OfferModel offer;
      //      var _tenderId = Util.Decrypt(tenderId);
      //      ViewBag.tenderId = _tenderId;
      //      ViewBag.tenderIdEncrypt = tenderId;
      //      offer = await _ApiClient.GetAsync<OfferModel>("Offer/ApplyTenderOffer/" + _tenderId + "/" + offerid, null);
      //      return View(offer);
      //   }
      //   catch (AuthorizationException ex)
      //   {
      //      throw ex;
      //   }
      //   catch (BusinessRuleException ex)
      //   {
      //      AddError(ex.Message);
      //      return RedirectToAction("SupplierTenders", "Tender");
      //   }
      //   catch (Exception ex)
      //   {
      //      _logger.LogError(ex.ToString(), ex);
      //      return RedirectToAction("SupplierTenders", "Tender");
      //   }
      //}

      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> GetCombinedSuppliersByOfferId(CombinedSearchCretriaModel model)
      {
         try
         {

            var result = await _ApiClient.GetQueryResultAsync<SupplierCombinedModelView>("Offer/GetCombinedSuppliersByOfferId/", model.ToDictionary());

            return Json(new PaginationModel(result.Items, result.TotalCount, model.PageSize, model.PageNumber, null));
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }
      #endregion

      //[Authorize(RoleNames.ApplyOffersPolicy)]
      [Authorize]
      [HttpGet("Offer/OpenOffers/{offerid}/{redirectPage}")]
      public async Task<ActionResult> OpenOffers(string offerid, int redirectPage = 0)
      {
         try
         {
            TempData["redirectPage"] = redirectPage;
            OpenOfferModel model = await _ApiClient.GetAsync<OpenOfferModel>("Offer/GetOpenOfferData/" + Util.Decrypt(offerid), null);
            return View(model);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      [HttpGet("Offer/OpenOffersDetailsForAwarding/{offerid}/{redirectPage}")]
      public async Task<ActionResult> OpenOffersDetailsForAwarding(string offerid, int redirectPage = 0)
      {
         try
         {
            TempData["redirectPage"] = redirectPage;
            OpenOfferModel model = await _ApiClient.GetAsync<OpenOfferModel>("Offer/OpenOffersDetailsForAwarding/" + Util.Decrypt(offerid), null);
            return View(model);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      public IActionResult GetCombinedSuppliersViewComponenet(string offerIdString)
      {
         return ViewComponent("CombinedSuppliersComponent", new { offerIdString = offerIdString });
      }


      [Authorize(RoleNames.ApplyOffersPolicy)]
      [HttpPost]
      public async Task<ActionResult> ApplyOfferOpenings(OfferModel model)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferModel offer;
            var _tenderId = Util.Decrypt(model.TenderId);
            ViewBag.tenderId = _tenderId;
            ViewBag.tenderIdEncrypt = model.TenderId;
            var _offerId = Util.Decrypt(model.OfferId);
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/GeOfferByTenderIdAndOfferIdForOpening/" + _tenderId + "/" + _offerId, null);
            return View(offer);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
      [HttpPost("Offer/SaveOpeningOfferFinancialDetails")]
      public async Task<ActionResult> SaveOpeningOfferFinancialDetails(SaveOpeningOfferFinancialDetails tables, OfferDetailModel model)
      {
         try
         {
            var obj = new object();
            if (!string.IsNullOrEmpty(model.CombinedIdString))
            {
               obj = await _ApiClient.PostAsync<Task<object>>("Offer/AddOfferDetails", null, model);
            }
            //var result = await _ApiClient.PostAsync<OfferModel>("offer/SaveOpeningOfferFinancialDetails", null, tables);
            var result = new OfferModel();
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

      [HttpPost("Offer/SaveDiscountNotes")]
      public async Task<ActionResult> SaveDiscountNotes(DiscountNotesModel model)
      {
         try
         {
            OfferModel result = await _ApiClient.PostAsync<OfferModel>("Offer/SaveDiscountNotes", null, model);
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

      [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
      [HttpPost("Offer/SaveOpeningStageTables")]
      public async Task<ActionResult> SaveOpeningStagTables(List<SaveTableModel> tables)
      {
         try
         {
            int offerId = tables[0].OfferId;
            var result = await _ApiClient.PostAsync<OfferModel>("offer/SaveOpeningStagTables", null, tables);
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
      private static SupplierAttachmentModel FillSupplierAttachments(string attachmentFilePath, string root, Enums.AttachmentType type)
      {
         SupplierAttachmentModel attachment = null;
         if (!string.IsNullOrEmpty(attachmentFilePath))
         {
            var arr = attachmentFilePath.Split(":");
            attachment = new SupplierAttachmentModel() { FileName = arr[1], FileNetReferenceId = arr[0], type = type };
         }
         return attachment;
      }


      [Authorize(RoleNames.SupplierPolicy)]
      [HttpPost("Offer/SendOffer")]
      public async Task<IActionResult> SendOffer([FromForm] int offerId)
      {
         try
         {
            var result = await _ApiClient.PostAsync("Offer/SendOffer", null, offerId);
            bool res = true;
            return Json(res);
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
      private static SupplierAttachmentModel FillSupplierAttachments(string attachmentFilePath, string root)
      {
         SupplierAttachmentModel attachment = null;
         if (!string.IsNullOrEmpty(attachmentFilePath))
         {
            var arr = attachmentFilePath.Split(":");
            attachment = new SupplierAttachmentModel() { FileName = arr[1], FileNetReferenceId = arr[0] };
         }
         return attachment;
      }

      [Authorize(RoleNames.SuppliersReportPolicy)]
      public async Task<IActionResult> SuppliersReport(string tenderIdString)
      {
         var result = await _ApiClient.GetAsync<TenderDetailsForAppliedSuppliersReportModel>("Tender/GetTenderDetailsForAppliedSuppliersReport/" + tenderIdString, null);
         return View(result);
      }

      [Authorize(RoleNames.SuppliersReportPolicy)]
      public async Task<ActionResult> GetSuppliersReportByTenderIdAsync(string tenderIdString)
      {
         try
         {
            var tenderId = Util.Decrypt(tenderIdString);
            QueryResult<AppliedSuppliersReportModel> tenderModel = await _ApiClient.GetQueryResultAsync<AppliedSuppliersReportModel>("Offer/GetSuppliersReportByTenderId/" + tenderId, null);
            return Json(new PaginationModel(tenderModel.Items, tenderModel.TotalCount, _pageSize, tenderModel.PageNumber, null));
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
      [Authorize(RoleNames.SuppliersReportPolicy)]
      public async Task<ActionResult> GetSuppliersReportByTenderIdAsync__(string tenderIdString, int pageNumber = 1)
      {
         try
         {
            var tenderModel = await _ApiClient.GetQueryResultAsync<AppliedSuppliersReportDetailsModel>("Offer/GetSuppliersReportByTenderId__/" + tenderIdString + "/" + pageNumber, null);
            return Json(new PaginationModel(tenderModel.Items, tenderModel.TotalCount, 6, tenderModel.PageNumber, null));
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


      [Authorize(RoleNames.SuppliersReportPolicy)]
      public async Task<ActionResult> ExportAppliedSuppliersReportAsync(string tenderIdString)
      {
         try
         {
            var result = await _ApiClient.GetListAsync<AppliedSuppliersReportDetailsModel>("Offer/ExportAppliedSuppliersReport/" + tenderIdString, null);

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
               var workSheet = package.Workbook.Worksheets.Add("Sheet1");
               //workSheet.Cells.LoadFromCollection(result, true);
               // workSheet.Cells.LoadFromCollection(result, true);
               workSheet.Cells[1, 1].Value = "الاسم التجاري للشركة";
               workSheet.Cells[1, 2].Value = "السجل التجاري للشركة";
               workSheet.Cells[1, 3].Value = "تاريخ إرسال الدعوه";
               workSheet.Cells[1, 4].Value = "تاريخ الشراء";
               workSheet.Cells[1, 5].Value = "حالة الدعوة";
               workSheet.Cells[1, 6].Value = "حالة الشراء";
               workSheet.Cells[1, 7].Value = "حالة العرض";
               workSheet.Cells[1, 8].Value = "تاريخ قبول الدعوة";
               workSheet.Cells[1, 9].Value = "تاريخ رفض الدعوة";
               workSheet.Cells[1, 10].Value = "تاريخ سحب الدعوة";
               workSheet.Cells[1, 11].Value = "تاريخ سحب العرض";
               int row = 2;
               for (int i = 0; i < result.Count; i++)
               {
                  workSheet.Cells[row, 1].Value = result[i].CompanyName;
                  workSheet.Cells[row, 2].Value = result[i].CommericalRegisterNo;
                  workSheet.Cells[row, 3].Value = result[i].InvitationSendingDate.ToGregorianDate("dd/MM/yyyy");
                  workSheet.Cells[row, 4].Value = result[i].PurchaseDate.ToGregorianDate("dd/MM/yyyy");
                  workSheet.Cells[row, 5].Value = result[i].InvitationStatusName;
                  workSheet.Cells[row, 6].Value = result[i].PurchaseStatusName;
                  workSheet.Cells[row, 7].Value = result[i].OfferStatusName;
                  workSheet.Cells[row, 8].Value = result[i].InvitationAcceptanceDate.ToGregorianDate("dd/MM/yyyy");
                  workSheet.Cells[row, 9].Value = result[i].InvitationRejectionDate.ToGregorianDate("dd/MM/yyyy");
                  workSheet.Cells[row, 10].Value = result[i].InvitationWithdrawalDate.ToGregorianDate("dd/MM/yyyy");
                  workSheet.Cells[row, 11].Value = result[i].OfferWithdrawalDate.ToGregorianDate("dd/MM/yyyy");
                  row++;
               }
               for (int x = 1; x < 12; x++)
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
            string excelName = "Suppliers Report" + ".xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

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
      [HttpGet]
      public async Task<ActionResult> OffersFilesUpload(string id)
      {
         try
         {
            ApplyOfferModel model = new ApplyOfferModel { OfferId = id };
            return View(model);
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

      [Authorize(RoleNames.OffersOppeningSecretaryPolicy)]
      [HttpPost("Offer/OffersFilesUpload")]
      public async Task<ActionResult> OffersFilesUpload(OfferAttachmentsModel model)
      {
         try
         {
            #region old code
            //HijriCalendar hijriCal = new HijriCalendar();
            //GregorianCalendar gregCal = new GregorianCalendar();
            //foreach (SupplierBankGuaranteeModel attachment in model.BankGuaranteeFiles)
            //{
            //   if (!string.IsNullOrEmpty(attachment.GuaranteeEndDateString))
            //   {
            //      string[] date = attachment.GuaranteeEndDateString.Split(',');
            //      if (date[3] == "H")
            //      {
            //         DateTime persianDate = hijriCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
            //         attachment.GuaranteeEndDate = persianDate;
            //      }
            //      else
            //      {
            //         DateTime persianDate = gregCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
            //         persianDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), gregCal);
            //         attachment.GuaranteeEndDate = persianDate;
            //      }
            //   }

            //   if (!string.IsNullOrEmpty(attachment.GuaranteeStartDateString))
            //   {
            //      string[] date = attachment.GuaranteeStartDateString.Split(',');
            //      if (date[3] == "H")
            //      {
            //         DateTime persianDate = hijriCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
            //         attachment.GuaranteeStartDate = persianDate;
            //      }
            //      else
            //      {
            //         DateTime persianDate = gregCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
            //         persianDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), gregCal);
            //         attachment.GuaranteeStartDate = persianDate;
            //      }
            //   }
            //}
            //if (!string.IsNullOrEmpty(model.OfferLetterDateString))
            //{
            //   string[] date = model.OfferLetterDateString.Split(',');
            //   if (date[3] == "H")
            //   {
            //      DateTime persianDate = hijriCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
            //      model.OfferLetterDate = persianDate;
            //   }
            //   else
            //   {
            //      DateTime persianDate = gregCal.ToDateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0, 0);
            //      persianDate = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), gregCal);
            //      model.OfferLetterDate = persianDate;
            //   }
            //}
            #endregion
            model.OfferID = Util.Decrypt(model.OfferIDString);
            var obj = await _ApiClient.PostAsync<Task<bool>>("Offer/AddOfferAttachmentsDetails", null, model);
            bool res = true;
            return Json(res);
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
      public async Task<ActionResult> SaveOffersClassification(SupplierSpecificationModel obj)
      {
         try
         {
            List<string> attachmentReferences = new List<string>();
            SupplierSpecificationModel attachment = new SupplierSpecificationModel();
            if (!string.IsNullOrEmpty(Request.Form["ConstructionWorkId"]))
               attachment.ConstructionWorkId = int.Parse(Request.Form["ConstructionWorkId"]);
            if (!string.IsNullOrEmpty(Request.Form["MaintenanceRunningWorkId"]))
               attachment.MaintenanceRunningWorkId = int.Parse(Request.Form["MaintenanceRunningWorkId"]);
            if (!string.IsNullOrEmpty(Request.Form["Degree"]))
               attachment.Degree = int.Parse(Request.Form["Degree"]);
            attachment.IsForApplier = bool.Parse(Request.Form["IsForApplier"]);
            attachment.OfferId = 29;
            await _ApiClient.PostAsync("Offer/AddClassification", null, attachment);
            string message = "success";
            return Json(message);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      [HttpGet("Offer/OfferChecking/{tenderId}/{offerid?}")]
      public async Task<ActionResult> OfferChecking(string tenderId, string offerId)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferModel offer;
            var _tenderId = Util.Decrypt(tenderId);
            ViewBag.tenderId = _tenderId;
            ViewBag.tenderIdEncrypt = tenderId;
            var _offerId = Util.Decrypt(offerId);
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/GetOfferDetailsById/" + _tenderId + "/" + _offerId, null);
            return View(offer);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
      }

      [HttpPost]
      public IActionResult GetOfferQuantityTableViewComponent(int tenderId, int offerId, int tableId, int formId, bool isReadOnly)
      {
         return ViewComponent("OfferQuantityTable", new { tenderId, offerId, tableId = tableId.ToString(), formId, isReadOnly });
      }

      [HttpPost]
      public IActionResult GetApplyOfferQuantityTableViewComponent(int tenderId, int offerId, int tableId, int formId, bool isReadOnly)
      {
         return ViewComponent("ApplyOfferQuantityTable", new { tenderId, offerId, tableId = tableId.ToString(), formId, isReadOnly });
      }
      [HttpPost]
      public IActionResult GetQuantityTableOpenDetailsViewComponent(int tenderId, int offerId, int tableId, int formId, bool isReadOnly)
      {
         return ViewComponent("QuantityTableOpenDetails", new { tenderId, offerId, tableId = tableId.ToString(), formId, isReadOnly });
      }
      [HttpPost]
      public IActionResult GetApplyOfferQuantityTableStepViewComponent(string offerIdString)
      {
         return ViewComponent("ApplyOfferQuantityTableStep", new { offerIdString });
      }

      [HttpPost]
      public IActionResult GetVROOfferQuantityTableViewComponent(int tenderId, int offerId, int tableId, int formId, bool isReadOnly)
      {
         return ViewComponent("VROOfferQuantityTable", new { tenderId, offerId, tableId = tableId.ToString(), formId, isReadOnly });
      }

      [HttpGet]
      public async Task<IActionResult> GetOfferTableQuantityItemsAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetOfferTableQuantityItems", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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
      public async Task<IActionResult> GetApplyOfferTableQuantityItemsAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetApplyOfferTableQuantityItems", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }



      [HttpPost]
      public async Task<ActionResult> SaveCheckingQuantityItem(IFormCollection collection)
      {
         try
         {
            Dictionary<string, object> authorList = new Dictionary<string, object>();
            foreach (var item in collection.Keys)
            {
               authorList.Add(item, collection[item].ToString());
            }
            var quantityTableItems = await _ApiClient.PostAsync<QueryResult<TableModel>>("Offer/ValidateandSaveCheckingQuantitiesTable", null, authorList);
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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
      public async Task<ActionResult> SaveVROCheckingQuantityItem(IFormCollection collection)
      {

         try
         {

            //string TenderId = "";
            Dictionary<string, string> AuthorList = new Dictionary<string, string>();
            foreach (var item in collection.Keys)
            {
               AuthorList.Add(item, collection[item].ToString());
            }
            //int tenderId = 21;
            //AuthorList["TenderId"] = tenderId.ToString();
            string html = await _ApiClient.PostAsync("Offer/ValidateandSaveVROCheckingQuantitiesTable", null, AuthorList);

            return Content(html, "text/html");
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
      [HttpGet("Offer/OfferCheckingOneFile/{offerid?}")]
      public async Task<ActionResult> OfferCheckingOneFile(string offerId)
      {
         try
         {
            var offer = await _ApiClient.GetAsync<OfferCheckingDetailsModel>("Offer/GetOfferDetailsById/" + Util.Decrypt(offerId), null);
            return View(offer);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
      }

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      [HttpPost]
      public async Task<ActionResult> OfferCheckingOneFile(OfferCheckingDetailsModel offerModel)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.OfferResources.ErrorMessages.ModelDataError);
               var offers = await _ApiClient.GetAsync<OfferCheckingDetailsModel>("Offer/GetOfferDetailsById/" + offerModel.OfferId, null);
               return View(offers);
            }
            var offer = await _ApiClient.PostAsync<OfferCheckingDetailsModel>("Offer/OfferCheckingStatus/", null, offerModel);
            var tenderId = Util.Encrypt(offer.TenderId);
            return RedirectToAction("CheckTenderOffers", "Tender", new { tenderIdString = tenderId });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            var offers = await _ApiClient.GetAsync<OfferCheckingDetailsModel>("Offer/GetOfferDetailsById/" + offerModel.OfferId, null);
            return View(offers);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
      }

      [Authorize(Roles = RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager)]
      [HttpGet("Offer/OfferDetail/{CombinedId}")]
      public async Task<ActionResult> OfferDetail(string combinedId)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }


            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes/*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });
            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes/*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailModel model = await _ApiClient.GetAsync<OfferDetailModel>("Offer/GetOfferDetails/" + Util.Decrypt(combinedId), null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.CombinedIdString = combinedId;
            return View(model);
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

      [Authorize(Roles = RoleNames.OffersOppeningSecretary)]
      [HttpPost("Offer/OfferDetail")]
      public async Task<ActionResult> OfferDetail(OfferDetailModel model)
      {
         try
         {
            var obj = await _ApiClient.PostAsync<Task<object>>("Offer/AddOfferDetails", null, model);
            bool res = true;
            return Json(res);
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

      public async Task<ActionResult> OfferAwarding(int offerId)
      {
         try
         {
            var offer = await _ApiClient.GetAsync<OfferModel>("Offer/GetOfferById/" + offerId, null);
            return View(offer);
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

      public async Task<ActionResult> OfferOpeningReport(string tenderIdString)
      {
         ViewBag.tenderIdString = tenderIdString;
         return View();
      }

      public async Task<ActionResult> OfferOpeningReportAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var offers = await _ApiClient.GetQueryResultAsync<OfferFinancialDetailsModel>("Offer/OffersOpeningReport/" + tenderId, null);
            return Json(new PaginationModel(offers.Items, offers.TotalCount, _pageSize, offers.PageNumber, null));
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

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> TenderAwardingPagingAsync(string tenderIdString, string crsuppliers)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            if (!string.IsNullOrEmpty(crsuppliers))
            {
               crsuppliers = crsuppliers.Substring(0, crsuppliers.Length - 1);
            }
            var result = await _ApiClient.GetQueryResultAsync<OfferFinancialDetailsModel>("Offer/GetOffersForAwardingByTenderId/" + tenderId, new Dictionary<string, object> { { nameof(crsuppliers), crsuppliers } });
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

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> ActualTenderAwardingPagingAsync(string tenderIdString)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetQueryResultAsync<OfferFinancialDetailsModel>("Offer/GetAwardedOffersByTenderId/" + tenderId, null);
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
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> SaveOfferAwardingValuesAsync(OfferAwardingModel offerAwardingObj)
      {
         try
         {
            await _ApiClient.PostAsync("Offer/SaveOfferAwardingValues", null, offerAwardingObj);
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

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      [HttpGet("Offer/OfferCheckingDetailsForAwarding/{tenderId}/{offerid?}")]
      public async Task<ActionResult> OfferCheckingDetailsForAwarding(string tenderId, string offerid)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferModel offer;
            var _tenderId = Util.Decrypt(tenderId);
            ViewBag.tenderId = _tenderId;
            ViewBag.tenderIdEncrypt = tenderId;
            var _offerId = Util.Decrypt(offerid);
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/OfferCheckingDetailsForAwarding/" + _tenderId + "/" + _offerId, null);
            return View("~/Views/Offer/OfferCheckingDetailsForAwarding.cshtml", offer);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      #region Direct Purchase Checking Stage

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      [HttpPost("Offer/SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial")]
      public async Task<ActionResult> SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial(OfferDetailModel model, CheckOfferResultModel checkingResult)
      {
         try
         {
            var result = await _ApiClient.PostAsync<OfferModel>("Offer/SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial", null, new OfferCheckingContainer
            {
               model = model,
               checkingResult = checkingResult
            });
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

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      [HttpPost("Offer/SaveDirectPurchaseAttachmentsChecking")]
      public async Task<ActionResult> SaveDirectPurchaseAttachmentsChecking(OfferDetailModel model)
      {
         try
         {
            var obj = await _ApiClient.PostAsync<Task<object>>("Offer/SaveDirectPurchaseAttachmentsChecking", null, model);
            bool res = true;
            return Json(res);
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

      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      [HttpPost("Offer/SaveOneFileDirectPurchaseChecking")]
      public async Task<ActionResult> SaveOneFileDirectPurchaseChecking(CheckOfferResultModel checkingResult)
      {
         try
         {
            await _ApiClient.PostAsync<Task<object>>("Offer/SaveOneFileDirectPurchaseChecking", null, checkingResult);
            return Ok();
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

      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      [HttpPost("Offer/SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial")]
      public async Task<ActionResult> SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial(OfferCheckingContainer container)
      {
         try
         {
            await _ApiClient.PostAsync<Task<object>>("Offer/SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial", null, container);
            return Ok();
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

      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      [HttpPost("Offer/SaveTwoFileFinancialCheckingDataAndAttachemntsDetial")]
      public async Task<ActionResult> SaveTwoFileFinancialCheckingDataAndAttachemntsDetial(OfferCheckingContainer container)
      {
         try
         {
            await _ApiClient.PostAsync<Task<object>>("Offer/SaveTwoFileFinancialCheckingDataAndAttachemntsDetial", null, container);
            return Ok();
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

      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      [HttpPost("Offer/SaveTwoFilesTechnicalDirectPurchaseChecking")]
      public async Task<ActionResult> SaveTwoFilesTechnicalDirectPurchaseChecking(CheckOfferResultModel checkingResult)
      {
         try
         {
            await _ApiClient.PostAsync<Task<object>>("Offer/SaveTwoFilesTechnicalDirectPurchaseChecking", null, checkingResult);
            return Ok();
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

      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      [HttpPost("Offer/SaveTwoFilesFinancialDirectPurchaseChecking")]
      public async Task<ActionResult> SaveTwoFilesFinancialDirectPurchaseChecking(CheckOfferResultModel checkingResult)
      {
         try
         {
            await _ApiClient.PostAsync<Task<object>>("Offer/SaveTwoFilesFinancialDirectPurchaseChecking", null, checkingResult);
            return Ok();
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

      [Route("Offer/GetCoastsTablForDirectPurchaseAsync")]
      public async Task<IActionResult> GetCoastsTablForDirectPurchaseAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetAsync<TableModel>("Offer/GetCoastsTablForDirectPurchaseAsync", quantityTableSearchCretriaModel.ToDictionary());
            return Json(quantityTableItems.TableHtml);
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

      [Route("Offer/GetCoastsTablForApplyOfferAsync")]
      public async Task<IActionResult> GetCoastsTablForApplyOfferAsync([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetAsync<TableModel>("Offer/GetCoastsTablForApplyOfferAsync", quantityTableSearchCretriaModel.ToDictionary());
            return Json(quantityTableItems.TableHtml);
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


      [Route("Offer/GetBayanTableAsync")]
      public async Task<IActionResult> GetBayanTableAsync([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetBayanTableAsync", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
            //return Json(quantityTableItems);
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

      [Route("Offer/GetBayanTableReadOnlyAsync")]
      public async Task<IActionResult> GetBayanTableReadOnlyAsync([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetBayanTableReadOnlyAsync", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
            //return Json(quantityTableItems);
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
      public async Task<ActionResult> SaveBayanQuantityItem(IFormCollection collection)
      {
         //string TenderId = "";
         Dictionary<string, string> authorList = new Dictionary<string, string>();
         foreach (var item in collection.Keys)
         {
            authorList.Add(item, collection[item].ToString());
         }
         ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Offer/ValidateBayanQuantitiesTables", null, authorList);
         //return RedirectToAction(nameof(QuantitiesTableStep), new { id = Util.Encrypt(tenderId) });
         //return View(collection);
         return Ok(obj);
      }

      [HttpPost]
      public async Task<ActionResult> DeleteBayanQuantityItem(IFormCollection collection)
      {
         Dictionary<string, string> authorList = new Dictionary<string, string>();
         foreach (var item in collection.Keys)
         {
            authorList.Add(item, collection[item].ToString());
         }

         ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Offer/DeleteBayanQuantityItem", null, authorList);
         return Ok(obj);
      }

      [Route("offer/GetOfferTableQuantityItemsForDirectPurchaseAsync")]
      public async Task<IActionResult> GetOfferTableQuantityItemsForDirectPurchaseAsync([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetOfferTableQuantityItemsForDirectPurchase", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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

      [Route("offer/GetApplyOfferTableQuantityItems")]
      public async Task<IActionResult> GetApplyOfferTableQuantityItems([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetApplyOfferTableQuantityItems", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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



      [Authorize(RoleNames.OffersPurchaseSecretaryAndManagerPolicy)]
      [HttpGet("Offer/DirectPurchaseOfferChecking/{tenderId}/{offerid?}")]
      public async Task<ActionResult> DirectPurchaseOfferChecking(string tenderId, string offerid)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferModel offer;
            var _tenderId = Util.Decrypt(tenderId);
            ViewBag.tenderId = _tenderId;
            ViewBag.tenderIdEncrypt = tenderId;
            var _offerId = Util.Decrypt(offerid);
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/GeOfferByTenderIdAndOfferIdForDirectPurchase/" + _tenderId + "/" + _offerId, null);
            return View("~/Views/Offer/DirectPurchase/DirectPurchaseOfferChecking.cshtml", offer);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }
      [Authorize]
      [HttpGet("Offer/CombinedSuppliersDetailsDirectPurchase/{offeridString}/{tenderIdString}/{CombinedIdString}/{CombinedOwner}/{tenderTypeId}/{tenderStatusId}/{offerPresentationWayId}")]
      public async Task<ActionResult> CombinedSuppliersDetailsDirectPurchase(string offeridString, string tenderIdString, string CombinedIdString, bool CombinedOwner, string tenderTypeId, string tenderStatusId, string offerPresentationWayId)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferDetailModel model = new OfferDetailModel();
            model.OfferIdString = offeridString;
            model.TenderIDString = tenderIdString;
            model.CombinedOwner = CombinedOwner;
            model.TenderTypeId = Convert.ToInt32(tenderTypeId);
            model.TenderStatusId = Convert.ToInt32(tenderStatusId);
            model.OfferPresentationWayId = Convert.ToInt32(offerPresentationWayId);
            if (CombinedIdString != "null")
            {
               model.CombinedIdString = CombinedIdString;
            }
            return View("~/Views/Offer/DirectPurchase/CombinedSuppliersDetailsDirectPurchase.cshtml", model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }

      public async Task<ActionResult> GetCombinedSuppliersDirectPurchaseGridAsync(string offerid)
      {
         try
         {
            // get all data from api at once 
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("Offer/GetCombinedSuppliers/" + Util.Decrypt(offerid), null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      [HttpGet("Offer/ViewOfferDirectPurchaseDetails/{offerId}")]
      public async Task<ActionResult> ViewOfferDirectPurchaseDetails(string offerId)
      {
         try
         {
            // get all data from api at once 
            CheckOfferModel model = await _ApiClient.GetAsync<CheckOfferModel>("Offer/GetOpenOfferDataForCHeckStage/" + Util.Decrypt(offerId), null);
            if (User.IsInRole(RoleNames.OffersPurchaseManager) || User.IsInRole(RoleNames.OffersCheckManager) || model.TenderBasicInfo.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
               model.IsReadOnly = true;
            }
            return View("~/Views/Offer/DirectPurchase/ViewDirectPurchaseOfferDetails.cshtml", model);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("Offer/DirectPurchaseCheckingOneFile/{CombinedId}")]
      public async Task<ActionResult> DirectPurchaseCheckingOneFile(string combinedId)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes/*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });
            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForDirectPurchaseTenderChecking/" + Util.Decrypt(combinedId), null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.CombinedIdString = combinedId;
            return View("~/Views/Offer/DirectPurchase/DirectPurchaseCheckingOneFile.cshtml", model);
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

      [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("Offer/DirectPurchaseTechnicalChecking/{CombinedId}")]
      public async Task<ActionResult> DirectPurchaseTechnicalChecking(string combinedId)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });
            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForDirectPurchaseTenderChecking/" + Util.Decrypt(combinedId), null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.CombinedIdString = combinedId;
            return View("~/Views/Offer/DirectPurchase/DirectPurchaseTechnicalChecking.cshtml", model);
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

      [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
      [HttpGet("Offer/DirectPurchaseFinancialChecking/{CombinedId}")]
      public async Task<ActionResult> DirectPurchaseFinancialChecking(string combinedId)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });
            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForDirectPurchaseTenderFinancialChecking/" + Util.Decrypt(combinedId), null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.CombinedIdString = combinedId;
            return View("~/Views/Offer/DirectPurchase/DirectPurchaseFinancialChecking.cshtml", model);
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

      #endregion Direct Purchase Checking Stage

      [HttpGet]
      [Authorize(RoleNames.OpenOffersReportPolicy)]
      public async Task<ActionResult> OpenOffersReport(string tenderIdString, bool isRegistry)
      {
         try
         {
            int tenderId = Util.Decrypt(tenderIdString);
            var result = await _ApiClient.GetListAsync<OfferDeatilsReportForTenderModel>("Offer/OfferDeatilsReportForTender/" + tenderId, null);
            if (User.IsInRole(RoleNames.supplier) && result.Count == 0)
               return RedirectToAction("AccessDenied", "Authorization");
            if (User.IsInRole(RoleNames.supplier))
               return PartialView("_OpenOffersSupplierReport", result);
            if (isRegistry == true)
               return PartialView("_OpenOffersReportRegistry", result);
            else
            {
               if (User.IsInRole(RoleNames.OffersCheckManager) || User.IsInRole(RoleNames.OffersCheckSecretary))
                  return PartialView("_OpenOffersReportForCheckingCommittee", result);
               else
                  return PartialView("_OpenOffersReportForAllAgency", result);
            }
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> WithdrawOfferAsync(string offerId)
      {
         try
         {
            //int offerId = Util.Decrypt(offerIdString);
            var result = await _ApiClient.PostAsync("Offer/WithdrawOffer/" + offerId, null, null);
            return RedirectToAction("SupplierTenders", "Tender");
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

      //public IActionResult GetApplyOfferComponent(string tenderIdstr, string offerIdStr)
      //{
      //   var offer = ViewComponent("ApplyOfferComponent", new { tenderId = tenderIdstr, offerId = offerIdStr });
      //   return offer;
      //}

      #region United suppliers

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      [HttpGet("Offer/CombinedSuppliersCheckingStage/{offeridString}/{tenderIdString}/{CombinedIdString?}")]
      public async Task<ActionResult> CombinedSuppliersCheckingStage(string offeridString, string tenderIdString, string CombinedIdString = null)
      {
         try
         {
            OfferDetailModel model = new OfferDetailModel();
            model = await _ApiClient.GetAsync<OfferDetailModel>("Offer/GetTenderStatus/" + Util.Decrypt(tenderIdString), null);
            model.OfferIdString = offeridString;
            model.TenderIDString = tenderIdString;
            model.CombinedIdString = CombinedIdString;
            return View(model);
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

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      [HttpGet("Offer/GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync/{offerid}")]
      public async Task<ActionResult> GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(string offerid)
      {
         try
         {
            // get all data from api at once 
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("Offer/GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync/" + Util.Decrypt(offerid), null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      #endregion United suppliers

      #region TwoFiles checking

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
      [HttpGet("Offer/TenderTowFilesCombinedSuppliersCheckingStage/{offerIdString}")]
      public async Task<ActionResult> TenderTowFilesCombinedSuppliersCheckingStage(string offerIdString)
      {
         try
         {
            ViewBag.offerIdString = offerIdString;
            return View();
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

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
      [HttpGet("Offer/GetCombinedSuppliersForTwoFilesCombinedSuppliersCheckingStageAsync/{offerid}")]
      public async Task<ActionResult> GetCombinedSuppliersForTwoFilesCombinedSuppliersCheckingStageAsync(string offerid)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("Offer/GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync/" + Util.Decrypt(offerid), null);
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

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
      [HttpGet("Offer/TenderTowFilesTechnicalChecking/{offerIdString}")]
      public async Task<ActionResult> TenderTowFilesTechnicalChecking(string offerIdString)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });
            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForTenderChecking/" + offerIdString, null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.OfferIdString = offerIdString;
            //model.CombinedIdString = combinedId;
            return View(model);
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

      [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
      [HttpPost("Offer/SaveOfferCheckingOneFile")]
      public async Task<ActionResult> SaveOfferCheckingOneFile(SaveOpeningOfferFinancialDetails tables, OfferDetailModel model, CheckOfferResultModel checkingResult)
      {
         try
         {
            var result = await _ApiClient.PostAsync<OfferModel>("Offer/SaveOfferCheckingOneFile", null, new OfferCheckingContainer
            {
               model = model,
               checkingResult = checkingResult
            });
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

      [Authorize(RoleNames.AddFinantialCheckingResultPolicy)]
      [HttpPost("Offer/AddFinantialCheckingResult")]
      public async Task<ActionResult> AddFinantialCheckingResult(CheckOfferResultModel checkingResult)
      {
         try
         {
            var result = await _ApiClient.PostAsync<OfferModel>("Offer/AddFinantialCheckingResult", null, checkingResult);
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

      [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningSecretary)]
      [HttpPost("Offer/SaveCombinedOfferCheckingAttachments")]
      public async Task<ActionResult> SaveCombinedOfferCheckingAttachments(OfferDetailModel model)
      {
         try
         {
            var result = await _ApiClient.PostAsync<OfferModel>("Offer/AddCheckingFinancialDetails", null, model);
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
      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost("Offer/TenderTowFilesTechnicalChecking")]
      public async Task<ActionResult> TenderTowFilesTechnicalChecking(OfferDetailsForCheckingTwoFilesModel model)
      {
         try
         {
            var obj = await _ApiClient.PostAsync<Task<object>>("Offer/AddTechnicalOfferResultForTwoFilesTender", null, model);
            return PartialView("~/Views/Offer/CheckOffer/Partials/_TwoFilesOffersTechnicalChecking.cshtml", model);
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

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
      [HttpGet("Offer/TenderTowFilesFinancialCheck")]
      public async Task<ActionResult> TenderTowFilesFinancialCheck(string offerIdString)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);

            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });

            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForTenderChecking/" + offerIdString, null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.OfferIdString = offerIdString;
            return View(model);
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

      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost("Offer/TenderTowFilesFinancialCheck")]
      public async Task<ActionResult> TenderTowFilesFinancialCheck(TenderTowFilesFinancialDetailsCheck model)
      {
         try
         {
            var obj = await _ApiClient.PostAsync<Task<object>>("Offer/AddTwoFilesFinancialCheck", null, model);
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

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
      [HttpGet("Offer/TenderTowFilesFinancialDetails")]
      public async Task<ActionResult> TenderTowFilesFinancialDetails(string offerIdString)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);

            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });

            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForTenderChecking/" + offerIdString, null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.OfferIdString = offerIdString;
            return View(model);
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

      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost("Offer/TenderTowFilesFinancialDetails")]
      public async Task<ActionResult> TenderTowFilesFinancialDetails(TenderTowFilesFinancialDetailsDetails tenderTowFilesFinancialDetailsCheck)
      {
         try
         {
            var result = await _ApiClient.PostAsync<Task<object>>("Offer/AddTwoFilesFinancialDetails", null, tenderTowFilesFinancialDetailsCheck);
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

      [Authorize(Roles = RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
      [HttpGet("Offer/TenderTowFilesEvaluateFinancialDetails/{offerIdString}")]
      public async Task<ActionResult> TenderTowFilesEvaluateFinancialDetails(string offerIdString)
      {
         try
         {
            List<BankModel> banks = await _ApiClient.GetListAsync<BankModel>("Lookup/GetBanks", null);
            //List<MaintenanceRunningWorkModel> maintenanceWorks = await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);

            List<MaintenanceRunningWorkModel> maintenanceWorks = await _cache.GetOrCreateAsync(CacheKeys.GetMaintenanceRunningWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes /*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<MaintenanceRunningWorkModel>("Lookup/GetMaintenanceRunningWorks", null);
            });

            //List<ConstructionWorkModel> constructionWorks = await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            List<ConstructionWorkModel> constructionWorks = await _cache.GetOrCreateAsync(CacheKeys.GetConstructionWorks, async entry =>
            {
               int minutes = int.Parse(_rootConfiguration.CachingConfiguration.CachingMinutes/*_configuration.GetSection("Chaching:CachingMinutes").Value*/);
               entry.SlidingExpiration = TimeSpan.FromMinutes(minutes);
               return await _ApiClient.GetListAsync<ConstructionWorkModel>("Lookup/GetConstructionWorks", null);
            });
            OfferDetailsForCheckingModel model = await _ApiClient.GetAsync<OfferDetailsForCheckingModel>("Offer/GetOfferDetailsForTenderChecking/" + offerIdString, null);
            model.MaintenanceRunningWorks = maintenanceWorks;
            model.ConstructionWorks = constructionWorks;
            model.Banks = banks;
            model.OfferIdString = offerIdString;
            return View(model);
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

      [Authorize(Roles = RoleNames.OffersCheckSecretary)]
      [HttpPost("Offer/TenderTowFilesEvaluateFinancialDetails")]
      public async Task<ActionResult> TenderTowFilesEvaluateFinancialDetails(OfferDetailsForCheckingModel model)
      {
         try
         {
            var result = await _ApiClient.PostAsync<Task<object>>("Offer/AddTwoFilesFinancialCheck", null, model);
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

      #endregion TwoFiles checking

      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> SaveAlternativeItem(SupplierQuantityTableItemModel AlternativeItem)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               return JsonErrorMessage(Resources.TenderResources.ErrorMessages.ModelDataError);
            }

            var itemResponseModel = await _ApiClient.PostAsync<AlternativeItemResponseModel>("Offer/SaveAlternativeItem", null, AlternativeItem);
            return Json(itemResponseModel);
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
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> DeleteAlternativeItem2(int AlternativeItemId)
      {
         try
         {

            var itemResponseModel = await _ApiClient.PostAsync<AlternativeItemResponseModel>("Offer/DeleteAlternativeItem", null, (AlternativeItemId));
            return Json(itemResponseModel);
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

      //[HttpGet("Offer/GetQuantititesTablesWithAlternativesByOfferId/{offeridString}")]
      //public async Task<ActionResult> GetQuantititesTablesWithAlternativesByOfferId(string offeridString)
      //{
      //   try
      //   {
      //      //var quantities =  new List<AlternativeSupplierQuantityTableForCheckingModel>{ new AlternativeSupplierQuantityTableForCheckingModel
      //      //               {
      //      //               AlternativeSupplierQuantityTableItemForCheckingModels = new List<AlternativeSupplierQuantityTableItemForCheckingModel>
      //      //            {
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ Id = 1 , Name = "nawaf", Price = 1000, hasAlternative = false },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ Id = 2 , Name = "nawaf", Price = 1000, hasAlternative = false },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ IdString = "3", Id = 3 , Name = "parent", Price = 500, hasAlternative = true },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ Id = 4 , Name = "child", Price = 500, isAlternative = true, OriginalItemId = "3" },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ IdString = "5", Id = 5 , Name = "parent50", Price = 500, hasAlternative = true },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ Id = 6 , Name = "child50", Price = 500, isAlternative = true, OriginalItemId = "5" },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ IdString = "7", Id = 7 , Name = "parent55", Price = 500, hasAlternative = true },
      //      //               new AlternativeSupplierQuantityTableItemForCheckingModel{ Id = 8 , Name = "child55", Price = 500, isAlternative = true, OriginalItemId = "7" }
      //      //            },
      //      //               TableQuantityName = "qishtah",
      //      //               OpeningTotalPrice = 1000,
      //      //               TableQuantityId = 1
      //      //            } };


      //      int offerId = Util.Decrypt(offeridString);

      //      var quantities = await _ApiClient.GetListAsync<AlternativeSupplierQuantityTableForCheckingModel>("Offer/GetQuantitiesbyOfferId/" + offerId, null);

      //      return PartialView("~/Views/Offer/CheckOffer/Partials/_QuantityTablesAndAlternativeOffers.cshtml", quantities);
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

      [HttpPost("Offer/SaveAlternativeChoices")]
      public async Task<ActionResult> SaveAlternativeChoices(AlternativeOfferCheckingChoiceSavingModel choices)
      {
         try
         {
            var quantities = await _ApiClient.PostAsync<AlternativeSupplierQuantityTableForCheckingModel>("Offer/SaveAlternativeChoices", null, choices);
            return PartialView("~/Views/Offer/CheckOffer/Partials/_QuantityTablesAndAlternativeOffers.cshtml", quantities);
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

      #region VROCheking

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      public async Task<ActionResult> VROOffersTechnicalChecking(string offerIdString)
      {
         try
         {
            var offer = await _ApiClient.GetAsync<VROOffersTechnicalCheckingViewModel>("Offer/GetVROOfferDetailsById/" + offerIdString, null);
            return View("~/Views/Offer/VRO/VROOffersTechnicalChecking.cshtml", offer);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
      }

      [HttpGet]
      public async Task<IActionResult> GetOfferTableQuantityItemsForVROAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetOfferTableQuantityItemsVRO", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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

      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      [HttpGet("Offer/ViewVROOfferDetails/{offerId}")]
      public async Task<ActionResult> ViewVROOfferDetails(string offerId)
      {
         try
         {
            // get all data from api at once 
            CheckOfferModel model = await _ApiClient.GetAsync<CheckOfferModel>("Offer/GetOpenOfferDataForCHeckStage/" + Util.Decrypt(offerId), null);
            if (User.IsInRole(RoleNames.OffersOpeningAndCheckManager) || model.TenderBasicInfo.TenderStatusId != (int)Enums.TenderStatus.VROOffersFinancialChecking)
            {
               model.IsReadOnly = true;
            }
            return View("~/Views/Offer/VRO/ViewVROOfferDetails.cshtml", model);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      [HttpPost]
      public async Task<ActionResult> VROOffersTechnicalChecking(VROOffersTechnicalCheckingViewModel offerModel)
      {
         try
         {
            if (!ModelState.IsValid)
            {
               AddError(Resources.OfferResources.ErrorMessages.ModelDataError);
               var offers = await _ApiClient.GetAsync<VROOffersTechnicalCheckingViewModel>("Offer/GetVROOfferDetailsById?offerIdString=" + offerModel.OfferIdString, null);
               return View(offers);
            }
            var offer = await _ApiClient.PostAsync<VROOffersTechnicalCheckingViewModel>("Offer/VROOffersTechnicalChecking", null, offerModel);
            return RedirectToAction("VROTenderChecking", "Tender", new { tenderIdString = offer.TenderIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            var offers = await _ApiClient.GetAsync<VROOffersTechnicalCheckingViewModel>("Offer/GetVROOfferDetailsById/" + offerModel.OfferIdString, null);
            return View("~/Views/Offer/VRO/VROOffersTechnicalChecking.cshtml", offers);
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            var offers = await _ApiClient.GetAsync<VROOffersTechnicalCheckingViewModel>("Offer/GetVROOfferDetailsById/" + offerModel.OfferIdString, null);
            return View("~/Views/Offer/VRO/VROOffersTechnicalChecking.cshtml", offers);
         }
      }

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      [HttpGet("Offer/VROOfferFinantialChecking/{tenderId}/{offerid?}")]
      public async Task<ActionResult> VROOfferFinantialChecking(string tenderId, string offerId)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            VROFinantialCheckingModel offer;
            var _tenderId = Util.Decrypt(tenderId);
            ViewBag.tenderId = _tenderId;
            ViewBag.tenderIdEncrypt = tenderId;
            var _offerId = Util.Decrypt(offerId);
            offer = await _ApiClient.GetAsync<VROFinantialCheckingModel>("Offer/GetOfferFinancialCheckingDetailsById/" + _tenderId + "/" + _offerId, null);
            return View("~/Views/Offer/VRO/VROOfferFinantialChecking.cshtml", offer);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message, ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("TenderIndexCheckingStage", "Tender");
         }
      }

      [Authorize(Roles = RoleNames.OffersOpeningAndCheckSecretary)]
      [HttpPost("Offer/VROFinancialChecking")]
      public async Task<ActionResult> VROFinancialChecking(OfferDetailsForCheckingModel model)
      {
         try
         {
            await _ApiClient.PostAsync<Task<object>>("Offer/AddFinantialOfferAttachmentsInVRO", null, model);
            return Json(true);
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

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      [HttpGet("Offer/VROCombinedSuppliersDetails/{offeridString}/{tenderIdString}/{CombinedIdString}/{CombinedOwner}/{tenderTypeId}")]
      public async Task<ActionResult> VROCombinedSuppliersDetails(string offeridString, string tenderIdString, string CombinedIdString, bool CombinedOwner, string tenderTypeId)
      {
         try
         {
            if (Request.Headers["Referer"] != "")
            {
               ViewData["Reffer"] = Request.Headers["Referer"].ToString();
            }
            OfferDetailModel model = new OfferDetailModel();
            model.OfferIdString = offeridString;
            model.TenderIDString = tenderIdString;
            model.CombinedOwner = CombinedOwner;
            model.TenderTypeId = Convert.ToInt32(tenderTypeId);
            if (CombinedIdString != "null")
            {
               model.CombinedIdString = CombinedIdString;
            }
            return View("~/Views/Offer/VRO/VROCombinedSuppliersDetails.cshtml", model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }

      [Authorize(RoleNames.VROOpenAndCheckingViewPolicy)]
      public async Task<ActionResult> GetCombinedSuppliersVROGridAsync(string offerid)
      {
         try
         {
            // get all data from api at once 
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("Offer/GetCombinedSuppliers/" + Util.Decrypt(offerid), null);
            return Json(new PaginationModel(result.Items, result.TotalCount, _pageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }

      [Authorize(RoleNames.OffersPurchaseSecretaryPolicy)]
      [HttpPost("Offer/SaveCombinedTechnicalDetailsVRO")]
      public async Task<ActionResult> SaveCombinedTechnicalDetailsVRO(OfferDetailModel model)
      {
         try
         {
            var obj = await _ApiClient.PostAsync<Task<object>>("Offer/SaveCombinedTechnicalDetailsDirectPurchase", null, model);
            bool res = true;
            return Json(res);
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

      #endregion VROCheking

      [HttpGet("Offer/OfferDetailsForAcceptingCombined/{combinedIdString}")]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> OfferDetailsForAcceptingCombined(string combinedIdString)
      {
         //   int offerId = Util.Decrypt(offerIdString);
         //   var result = await _ApiClient.GetAsync<CombinedInvitationDetailsModel>("Offer/GetCombinedInvitationDetails/" + offerId, null);
         //   return View(result);
         try
         {
            // get all data from api at once 
            OfferDetailsForAcceptingSolidarityInviationsModel model = await _ApiClient.GetAsync<OfferDetailsForAcceptingSolidarityInviationsModel>("Offer/GetOfferDetailsByCombinedIdForSupplier/" + Util.Decrypt(combinedIdString), null);
            return View(model);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> CombinedInvitationsForSupplier()
      {
         return View();
      }

      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> GetAllCombinedInvitationForSupplierAsync(CombinedSearchCriteria criteria)
      {
         try
         {

            var result = await _ApiClient.GetQueryResultAsync<CombinedListModel>("Offer/GetAllCombinedInvitationForSupplier/", criteria.ToDictionary());

            return Json(new PaginationModel(result.Items, result.TotalCount, criteria.PageSize, criteria.PageNumber, null));
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> CombinedInvitationDetails(string offerIdString)
      {
         try
         {
            int offerId = Util.Decrypt(offerIdString);
            var result = await _ApiClient.GetAsync<CombinedInvitationDetailsModel>("Offer/GetCombinedInvitationDetails/" + offerId, null);
            return View(result);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(CombinedInvitationsForSupplier));
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.ToString(), ex);
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> AcceptCombinedInvitation(string combinedIdString)
      {

         try
         {
            int combinedId = Util.Decrypt(combinedIdString);
            await _ApiClient.PostAsync("Offer/AcceptCombinedInvitation/" + combinedId, null, null);
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
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> RejectCombinedInvitation(string combinedIdString)
      {
         try
         {
            int combinedId = Util.Decrypt(combinedIdString);
            await _ApiClient.PostAsync("Offer/RejectCombinedInvitation/" + combinedId, null, null);
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

      #region Offer New

      public async Task<ActionResult> OfferSolidarity(string offeridString)
      {

         try
         {

            return View();
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }


      }

      public async Task<ActionResult> OfferFilesAsync(string offeridString)
      {

         try
         {
            OfferFileVModel offerFileVModel = await _ApiClient.GetAsync<OfferFileVModel>("Offer/OfferFilesAsync/" + Util.Decrypt(offeridString), null);
            return View(offerFileVModel);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }


      }

      [HttpPost]
      public async Task<ActionResult> OfferFilesAsync(OfferFileVModel offerFileVModel, Enums.enSubmitActionType ActionType)
      {

         try
         {
            offerFileVModel.SolidarityFiles = CreateSupplierAttachments(offerFileVModel.SolidarityrFilesReferenceIds, Enums.AttachmentType.SupplierCombinedAttachment);
            offerFileVModel.TechnicalFiles = CreateSupplierAttachments(offerFileVModel.TechnicalFilesReferenceIds, Enums.AttachmentType.SupplierTechnicalProposalAttachment);
            offerFileVModel.FinancialFiles = CreateSupplierAttachments(offerFileVModel.FinancialFilesReferenceId, Enums.AttachmentType.SupplierFinancialProposalAttachment);
            offerFileVModel.TechnicalandFinancialFiles = CreateSupplierAttachments(offerFileVModel.TechnicalandFinancialFilesReferenceIds, Enums.AttachmentType.SupplierTechnicalAndFinancialProposalAttachment);
            OfferFileVModel offerFileVM = await _ApiClient.PostAsync<OfferFileVModel>("Offer/PostOfferFilesAsync", null, offerFileVModel);
            if (ActionType == Enums.enSubmitActionType.SaveOnly)
            {
               return RedirectToAction(nameof(OfferFilesAsync), "Offer", new { offeridString = offerFileVModel.offerIdString });

            }

            if (offerFileVM.tenderType == Enums.TenderType.Competition || offerFileVM.tenderType == Enums.TenderType.FirstStageTender || offerFileVM.tenderType == Enums.TenderType.ReverseBidding)
            {

               if (offerFileVM.isSolidarity)
               {
                  return RedirectToAction(nameof(OfferSolidarityAsync), "Offer", new { OfferIdString = offerFileVModel.offerIdString });

               }
               else
               {

                  return RedirectToAction(nameof(OfferSummary), "Offer", new { OfferIdString = offerFileVModel.offerIdString });

               }

            }

            return RedirectToAction(nameof(ApplyOfferQuantityTablesStep), "Offer", new { OfferIdString = offerFileVModel.offerIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }


      }

      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> OfferMainAsync(string offeridString, string tenderIdString)
      {

         try
         {
            int offerid = string.IsNullOrEmpty(offeridString) ? 0 : Util.Decrypt(offeridString);
            int tenderId = string.IsNullOrEmpty(tenderIdString) ? 0 : Util.Decrypt(tenderIdString);
            OfferMainVModel offerFileVModel = await _ApiClient.GetAsync<OfferMainVModel>("Offer/GetOfferMain/" + offerid + "/" + tenderId, null);
            if (offerFileVModel.hasOffer)
            {
               return RedirectToAction(nameof(OfferMainAsync), new { offeridString = offerFileVModel.offerIdString, tenderIdString = offerFileVModel.tenderIdString });
            }
            if (!string.IsNullOrEmpty(offerFileVModel.SolidarityMessage))
            {
               AddWarnning(offerFileVModel.SolidarityMessage);


            }
            return View(offerFileVModel);

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }


      }

      [Authorize(Roles = RoleNames.supplier)]
      private static List<SupplierAttachmentModel> CreateSupplierAttachments(string attachmentFilePath, Enums.AttachmentType type)
      {
         SupplierAttachmentModel attachment = null;
         List<SupplierAttachmentModel> supplierAttachmentModels = new List<SupplierAttachmentModel>();
         if (!string.IsNullOrEmpty(attachmentFilePath))
         {
            var attachmentFilePaths = attachmentFilePath.Split(',');

            foreach (var path in attachmentFilePaths)
            {
               if (String.IsNullOrEmpty(path))
               {
                  continue;
               }

               if (path.StartsWith(','))
               {
                  path.TrimStart(',');
               }
               var arr = path.Split(":");
               var _name = RemoveSpecialChars(arr[1]);
               attachment = new SupplierAttachmentModel() { FileName = _name, FileNetReferenceId = arr[0], type = type };
               supplierAttachmentModels.Add(attachment);

            }
         }
         return supplierAttachmentModels;
      }

      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> OfferMainAsync(OfferMainVModel mainVModel, string submitButton)
      {

         try
         {

            OfferMainVModel offerFileVModel = await _ApiClient.PostAsync<OfferMainVModel>("Offer/PostOfferMain", null, mainVModel);

            if (submitButton == "Next")
            {
               return RedirectToAction("OfferFilesAsync", new { offeridString = offerFileVModel.offerIdString });
            }
            return RedirectToAction("OfferMainAsync", new { offeridString = offerFileVModel.offerIdString, tenderIdString = offerFileVModel.tenderIdString });
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }


      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> ApplyOfferQuantityTablesAsync(string offerIdString)
      {
         try
         {

            int offerid = string.IsNullOrEmpty(offerIdString) ? 0 : Util.Decrypt(offerIdString);
            OfferQuantityTableModel offerFileVModel = await _ApiClient.GetAsync<OfferQuantityTableModel>("Offer/GetApplyOfferQuantityTableModelAsync/" + offerid, null);
            return View(offerFileVModel);

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }
      }
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> ApplyOfferQuantityTablesStep(string offerIdString)
      {
         try
         {
            int offerid = string.IsNullOrEmpty(offerIdString) ? 0 : Util.Decrypt(offerIdString);
            OfferQuantityTableModel offerFileVModel = await _ApiClient.GetAsync<OfferQuantityTableModel>("Offer/ApplyOfferQuantityTablesStep/" + offerid, null);
            return View(offerFileVModel);

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }
      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> OfferQuantityTablesAsync(string offerIdString)
      {
         try
         {

            int offerid = string.IsNullOrEmpty(offerIdString) ? 0 : Util.Decrypt(offerIdString);
            OfferQuantityTableModel offerFileVModel = await _ApiClient.GetAsync<OfferQuantityTableModel>("Offer/GetOfferQuantityTableModelAsync/" + offerid, null);
            return View(offerFileVModel);

         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction("SupplierTenders", "Tender");
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            return RedirectToAction("SupplierTenders", "Tender");
         }




      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<PartialViewResult> GetOfferAllFiles(string OfferIdString)
      {


         int offerid = Util.Decrypt(OfferIdString);
         SupplierAttachmentPartialVModel offerFileVModels = await _ApiClient.GetAsync<SupplierAttachmentPartialVModel>("Offer/GetOfferFiles/" + offerid, null);
         return PartialView("~/Views/Offer/Partial/ApplyOffer/_ApplyOfferFiles.cshtml", offerFileVModels);

      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> OfferSolidarityAsync(string OfferIdString)
      {
         try
         {

            int offerId = Util.Decrypt(OfferIdString);
            OfferSolidarityModel offersolidaritymodel = await _ApiClient.GetAsync<OfferSolidarityModel>("Offer/GetOfferSolidarity/" + offerId, null);
            return View(offersolidaritymodel);
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
         return View();

      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> GetQuantityTablesReadOnlyComponent(string OfferIdString)
      {
         return ViewComponent("QuantityTablesReadOnlyComponent", OfferIdString);

      }

      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]

      public async Task<ActionResult> GetQuantityTablesReadOnlyComponentForAwarding(string OfferIdString)
      {
         return ViewComponent("QuantityTablesReadOnlyComponent", OfferIdString);

      }

      public async Task<ActionResult> GetQuantityTablesGlobalReadOnlyComponent(string OfferIdString)
      {
         return ViewComponent("QuantityTablesReadOnlyComponent", OfferIdString);

      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> GetQuantityTablesComponent(string OfferIdString)
      {
         return ViewComponent("QuantityTablesComponent", OfferIdString);

      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> GetApplyOfferQuantityTablesComponent(string OfferIdString)
      {
         return ViewComponent("ApplyOfferQuantityTablesComponent", OfferIdString);

      }

      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> DeleteAttachment(int attachmentId)
      {
         try
         {
            var res = await _ApiClient.GetAsync<bool>("Offer/DeleteAttachment/" + attachmentId, null);
            return Ok(res);
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
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> SaveSupplierQuantityItem(IFormCollection collection)
      {

         try
         {

            Dictionary<string, string> authorList = new Dictionary<string, string>();

            var tasks = collection.Select(async item =>
            {
               authorList.Add(item.Key, item.Value);
            });

            await Task.WhenAll(tasks);

            //int tenderId = 21;
            //AuthorList["TenderId"] = tenderId.ToString();
            string html = await _ApiClient.PostAsync("Offer/ValidateandSaveQuantitiesTable", null, authorList);

            return Content(html, "text/html");
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
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> ValidateSupplierAlternativeItem(IFormCollection collection)
      {

         try
         {

            //string TenderId = "";
            Dictionary<string, string> AuthorList = new Dictionary<string, string>();
            foreach (var item in collection.Keys)
            {
               AuthorList.Add(item, collection[item].ToString());
            }
            //int tenderId = 21;
            //AuthorList["TenderId"] = tenderId.ToString();
            ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Offer/ValidateSupplierAlternativeItem", null, AuthorList);

            return Ok(obj);
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
      public async Task<List<LookupModel>> GetAllCities()
      {
         var result = await _ApiClient.GetListAsync<LookupModel>("Lookup/GetCities", null);
         return result;
      }

      [HttpGet]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> GetInvitedSuppliers(SolidaritySearchCriteria supplierSearchCretria)
      {
         try
         {
            //      supplierSearchCretria.PageSize = 1000;
            supplierSearchCretria.Sort = "CreatedAt";
            var suppliers = await _ApiClient.GetQueryResultAsync<SolidarityInvitedRegisteredSupplierModel>("Offer/GetAllInvitedSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> GetInvitedUnregisteredSuppliers(SolidarityUnregisteredSearchCriteria supplierSearchCretria)
      {
         try
         {
            supplierSearchCretria.Sort = "CreatedAt";

            var suppliers = await _ApiClient.GetQueryResultAsync<SolidarityInvitedUnRegisteredSupplierModel>("Offer/GetInvitedUnregisteredSuppliers", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, suppliers.PageSize, suppliers.PageNumber, null));
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
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> GetAllSuppliersBySearchCretriaForInvitationAsync(SupplierSearchCretriaModel supplierSearchCretria)
      {
         try
         {
            var suppliers = await _ApiClient.GetQueryResultAsync<InvitationCrModel>("Offer/GetAllSuppliersBySearchCretriaForInvitation", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> GetAllSuppliersBySearchCretriaForSolidarityAsync(SolidaritySearchCriteria supplierSearchCretria)
      {
         try
         {
            supplierSearchCretria.PageSize = 10;
            var suppliers = await _ApiClient.GetQueryResultAsync<SolidarityInvitedRegisteredSupplierModel>("Offer/GetAllSuppliersBySearchCretriaForSolidarity", supplierSearchCretria.ToDictionary());
            return Json(new PaginationModel(suppliers.Items, suppliers.TotalCount, _pageSize, suppliers.PageNumber, null));
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

      // Send Invitations For Registered Suppliers
      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> SendInvitationsAsync(List<SolidarityInvitedRegisteredSupplierModel> suppliers)
      {
         try
         {
            var tenderList = await _ApiClient.PostAsync("Offer/SendInvitations", null, suppliers);
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

      // Send Invitations For Registered Suppliers
      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<JsonResult> AddSolidaritySupplier(SolidarityInvitationModel solidarity)
      {//SendInvitations
         try
         {
            var tenderList = await _ApiClient.PostAsync("Offer/AddSolidaritySupplier", null, solidarity);
            return Json(new { success = true });
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
      public async Task<JsonResult> RemoveSolidaritySupplier(SolidarityRemoveInvitationModel model)
      {//SendInvitations
         try
         {
            var tenderList = await _ApiClient.PostAsync("Offer/RemoveSolidaritySupplier", null, model);
            return Json(new { success = true });
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

      // Send Invitations For UnRegistered Suppliers By Email Address
      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> SendInvitationsByEmailAsync(int tenderId, List<string> emails)
      {
         try
         {
            var emailslist = await _ApiClient.GetQueryResultAsync<TenderInvitationDetailsModel>("Offer/SendInvitationByEmail", new Dictionary<string, object> { { "tenderId", tenderId }, { "emails", emails } });
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

      // Send Invitations For UnRegistered Suppliers By SMS
      [HttpPost]
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<IActionResult> SendInvitationsBySmsAsync(int tenderId, List<string> mobileNoList)
      {
         try
         {
            var mobileList = await _ApiClient.GetAsync<TenderInvitationDetailsModel>("offer/SendInvitationBySms", new Dictionary<string, object> { { "tenderId", tenderId }, { "mobilNoList", mobileNoList } });
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



      [Authorize(RoleNames.CheckTenderOffersPolicy)]
      public async Task<ActionResult> GetOfferQuantityTableForAwarding(string offerIdString, string tenderIdString)
      {
         return ViewComponent("OfferQuantityTableForAwarding", new { offerIdStr = offerIdString, tenderIdStr = tenderIdString });
      }
      [HttpGet]
      public async Task<ActionResult> GetEmptyForm(string offerIdString, string tenderIdString)
      {
         try
         {
            string result = await _ApiClient.GetStringAsync("Offer/GetEmptyForm/" + Util.Decrypt(offerIdString) + "/" + Util.Decrypt(tenderIdString), null);
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

      [HttpGet]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> GetAllCombinedSuppliersAsync(CombinedSearchCretriaModel criteria)
      {
         var result = await _ApiClient.GetQueryResultAsync<CombinedSuppliersListModel>("Offer/GetAllCombinedSuppliers/", criteria.ToDictionary());
         return Json(new PaginationModel(result.Items, result.TotalCount, criteria.PageSize, criteria.PageNumber, null));
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.supplier)]
      public async Task<ActionResult> DeleteAlternativeItem(string TableId, string ItemNumber, string FormId)
      {
         try
         {

            int QTableId = int.Parse(TableId);
            int QItemNumber = int.Parse(ItemNumber);
            ///" + QTableId + "/" + QItemNumber
            var itemResponseModel = await _ApiClient.GetAsync<AlternativeItemResponseModel>("Offer/DeleteAlternativeItem/" + QTableId + "/" + QItemNumber, null);
            return Ok(new { tableId = "Table_" + TableId + "_" + FormId });
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

      #region OpenOffers

      [Authorize(RoleNames.ApplyOffersPolicy)]
      [HttpGet("Offer/ApplyOfferOpening/{tenderId}/{offerid?}")]
      public async Task<ActionResult> ApplyOfferOpening(string tenderId, string offerid)
      {
         try
         {
            OfferModel offer;
            var _tenderId = Util.Decrypt(tenderId);
            ViewBag.tenderId = _tenderId;
            ViewBag.tenderIdEncrypt = tenderId;
            var _offerId = Util.Decrypt(offerid);
            offer = await _ApiClient.GetAsync<OfferModel>("Offer/GeOfferByTenderIdAndOfferIdForOpening/" + _tenderId + "/" + _offerId, null);
            if (offer.TenderTypeId == (int)Enums.TenderType.Competition || offer.TenderTypeId == (int)Enums.TenderType.ReverseBidding || offer.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
               if (offer.IsSolidarity)
               {
                  return RedirectToAction("CombinedSuppliersDetails", "Offer", new { tenderIdString = offer.TenderIdString, offeridString = offer.OfferIdString });
               }
            }
            return View(offer);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      public async Task<ActionResult> GetCombinedSuppliersAsync(CombinedSupplierModel model)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("Offer/GetCombinedSupplierByOfferid/", model.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, model.PageSize, model.PageNumber, null));
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }
      }

      public async Task<ActionResult> GetCombinedSuppliersGridAsync(CombinedSupplierModel model)
      {
         try
         {
            var result = await _ApiClient.GetQueryResultAsync<CombinedSupplierModel>("Offer/GetCombinedSuppliers/", model.ToDictionary());
            return Json(new PaginationModel(result.Items, result.TotalCount, result.PageSize, result.PageNumber, null));
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }

      }

      [HttpGet("Offer/FinancialOfferDetailsN")]
      public async Task<ActionResult> FinancialOfferDetailsN(string offerIdString, string PreviusAction = "", bool isNegotiation = false)
      {
         try
         {

            OfferDetailsDisplayModel offer = await _ApiClient.GetAsync<OfferDetailsDisplayModel>("Offer/GetFinancialOfferDetailsForDisplay/" + offerIdString, null);
            offer.PreviusAction = PreviusAction;
            offer.IsNegotiation = isNegotiation;

            //if (offer.TenderTypeId == (int)Enums.TenderType.Competition || offer.TenderTypeId == (int)Enums.TenderType.ReverseBidding || offer.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            //{
            //   if (offer.IsSolidarity)
            //   {
            //      return RedirectToAction("CombinedSuppliersDetails", "Offer", new { tenderIdString = offer.TenderIdString, offeridString = offer.OfferIdString });
            //   }
            //}

            return View("FinancialOfferDetails", offer);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      public async Task<ActionResult> FinancialOfferDetails(string offerIdString, string previusAction = null)
      {
         try
         {
            OfferDetailsDisplayModel offer = await _ApiClient.GetAsync<OfferDetailsDisplayModel>("Offer/GetFinancialOfferDetailsForDisplay/" + offerIdString, null);
            offer.IsNegotiation = false;
            offer.PreviusAction = previusAction;
            return View(offer);
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
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), "Tender");
         }

      }

      [Authorize(RoleNames.GetOpeningQuantityTablesComponentPolicy)]
      public async Task<ActionResult> GetOpeningQuantityTablesComponent(string offerIdString, bool allowEdit)
      {
         return ViewComponent("OpeningQuantityTablesComponent", new { OfferIdString = offerIdString, AllowEdit = allowEdit });
      }

      public async Task<ActionResult> GetQuantityTablesDisplayComponent(string offerIdString, bool allowEdit, bool isNegotiation = false)
      {
         return ViewComponent("QuantityTablesDisplayComponent", new { OfferIdString = offerIdString, AllowEdit = allowEdit, isNegotiation = isNegotiation });
      }

      [HttpGet("Offer/CombinedSuppliersDetails/{offeridString}/{tenderIdString}/{CombinedIdString?}")]
      public async Task<ActionResult> CombinedSuppliersDetails(string offeridString, string tenderIdString, string CombinedIdString = null)
      {
         try
         {
            OfferDetailModel model = new OfferDetailModel();
            model = await _ApiClient.GetAsync<OfferDetailModel>("Offer/GetTenderStatus/" + Util.Decrypt(tenderIdString), null);
            model.OfferIdString = offeridString;
            model.TenderIDString = tenderIdString;
            model.CombinedIdString = CombinedIdString;
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
      }

      [HttpGet("Offer/CombinedSuppliersDetailsReadOnly/{offeridString}/{tenderIdString}/{CombinedIdString?}")]
      public async Task<ActionResult> CombinedSuppliersDetailsReadOnly(string offeridString, string tenderIdString, string CombinedIdString = null)
      {
         try
         {
            OfferDetailModel model = new OfferDetailModel();
            model = await _ApiClient.GetAsync<OfferDetailModel>("Offer/GetTenderStatus/" + Util.Decrypt(tenderIdString), null);
            model.OfferIdString = offeridString;
            model.TenderIDString = tenderIdString;
            model.CombinedIdString = CombinedIdString;
            return View(model);
         }
         catch (AuthorizationException ex)
         {
            throw ex;
         }
         catch (BusinessRuleException ex)
         {
            AddError(ex.Message);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
         catch (Exception ex)
         {
            AddError(Resources.TenderResources.ErrorMessages.UnexpectedError);
            _logger.LogError(ex.ToString(), ex);
            return RedirectToAction(nameof(Index), nameof(TenderController).Substring(0, nameof(TenderController).LastIndexOf("Controller")));
         }
      }

      //[HttpPost]
      //[Authorize(Roles = RoleNames.OffersOppeningSecretary)]
      public async Task<ActionResult> SaveOfferQuantityItem(IFormCollection collection)
      {
         //string TenderId = "";
         Dictionary<string, string> AuthorList = new Dictionary<string, string>();
         foreach (var item in collection.Keys)
         {
            AuthorList.Add(item, collection[item].ToString());
         }
         ValidationReturndTemplate obj = await _ApiClient.PostAsync<ValidationReturndTemplate>("Offer/ValidateQuantitiesTables", null, AuthorList);
         return Ok(obj);
      }

      [HttpPost]
      [Authorize(Roles = RoleNames.OffersOppeningSecretary)]
      public async Task<ActionResult> SaveOpeningQuantityItem(IFormCollection collection)
      {
         try
         {
            Dictionary<string, string> AuthorList = new Dictionary<string, string>();
            foreach (var item in collection.Keys)
            {
               AuthorList.Add(item, collection[item].ToString());
            }
            string html = await _ApiClient.PostAsync("Offer/ValidateandSaveQuantitiesTableForOpening", null, AuthorList);
            return Content(html, "text/html");
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
      public async Task<ActionResult> GetOfferMainComponent(int TenderId)
      {
         return ViewComponent("BasicOfferTenderInfo", TenderId);

      }
      [Authorize(RoleNames.SupplierPolicy)]
      public async Task<ActionResult> GetOfferSummaryComponent(string offerId)
      {
         return ViewComponent("OfferSummary", offerId);

      }

      [HttpPost]
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
      public async Task<ActionResult> AddExclusionReasonAsync(ExclusionReasonAwardingModel exclusionReasonAwardingObj)
      {
         try
         {

            await _ApiClient.PostAsync("Offer/AddExclusionReason", null, exclusionReasonAwardingObj);
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
      public async Task<ActionResult> RemoveExclusionReasonAsync(ExclusionReasonAwardingModel exclusionReasonAwardingObj)
      {
         try
         {
            await _ApiClient.PostAsync("Offer/RemoveExclusionReason", null, exclusionReasonAwardingObj);
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


      #region Open Stage QT Details

      [Route("offer/GetTableQuantityItemsOpenDetails")]
      public async Task<IActionResult> GetTableQuantityItemsOpenDetails([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetTableQuantityItemsOpenDetails", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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
      public async Task<IActionResult> GetTableQuantityItemsOpenDetailsAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetQueryResultAsync<TableModel>("Offer/GetTableQuantityItemsOpenDetails", quantityTableSearchCretriaModel.ToDictionary());
            return Json(new PaginationModel(quantityTableItems.Items, quantityTableItems.TotalCount, quantityTableItems.PageSize, quantityTableItems.PageNumber, null));
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
            _logger.LogError(ex.ToString(), ex);
            return JsonErrorMessage(Resources.TenderResources.ErrorMessages.UnexpectedError);
         }
      }

      [Route("Offer/GetCoastsTablForOpenDetailsAsync")]
      public async Task<IActionResult> GetCoastsTablForOpenDetailsAsync([FromQuery] QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
      {
         try
         {
            var quantityTableItems = await _ApiClient.GetAsync<TableModel>("Offer/GetCoastsTablForOpenDetails", quantityTableSearchCretriaModel.ToDictionary());
            return Json(quantityTableItems.TableHtml);
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

      [Route("Offer/GetCanIgnoreMandatoryValidationColumnsId")]
      public async Task<List<int>> GetCanIgnoreMandatoryValidationColumnsId()
      {
         try
         {
            var columnsIds = await _ApiClient.GetAsync<List<int>>("Offer/GetCanIgnoreMandatoryValidationColumnsId", null);
            return columnsIds;
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
      #endregion

      [HttpGet("Offer/UpdateIfCorporationSmallOrMedium/{offerIdString}")]
      public async Task<ActionResult> UpdateIfCorporationSmallOrMedium(string offerIdString)
      {
         try
         {
            var result = await _ApiClient.GetStringAsync("Offer/UpdateIfCorporationSmallOrMedium/" + offerIdString, null);
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
      [HttpGet("Offer/UpdateIsCorporatioIncludedInMoneyMarket/{offerIdString}")]
      public async Task<ActionResult> UpdateIsCorporatioIncludedInMoneyMarket(string offerIdString)
      {
         try
         {
            var result = await _ApiClient.GetStringAsync("Offer/UpdateIsCorporatioIncludedInMoneyMarket/" + offerIdString, null);
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
      [HttpGet("Offer/UpdateLocalContentPercentage/{offerIdString}")]
      public async Task<ActionResult> UpdateLocalContentPercentage(string offerIdString)
      {
         try
         {
            var result = await _ApiClient.GetStringAsync("Offer/UpdateLocalContentPercentage/" + offerIdString, null);
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
      [HttpGet("Offer/UpdateLocalContentDesiredPercentage/{offerIdString}")]
      public async Task<ActionResult> UpdateLocalContentDesiredPercentage(string offerIdString)
      {
         try
         {
            var result = await _ApiClient.GetStringAsync("Offer/UpdateLocalContentDesiredPercentage/" + offerIdString, null);
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

      [HttpGet("Offer/CalculateOffersPreferences/{tenderStringId}")]
      [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]

      public async Task<ActionResult> CalculateOffersPreferences(string tenderStringId)
      {
         try
         {
            var result = await _ApiClient.GetStringAsync("Offer/CalculateOffersPreferences/" + tenderStringId, null);
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


   }
}

