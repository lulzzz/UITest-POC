using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : BaseController
    {
        private readonly IOfferAppService _offerService;
        private readonly IReportsService _reportsService;
        private readonly IMapper _mapper;
        public ReportsController(IOfferAppService offerService, IMapper mapper, IReportsService reportsService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _offerService = offerService;
            _mapper = mapper;
            _reportsService = reportsService;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("TenderValueToTypesReport")]
        public async Task<List<TenderValueToTypesModel>> TenderValueToTypesReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.GetTendersForTenderToTypesReport(searchCriteria);
            return tenders;
        }
        [HttpGet]
        [Route("GetFinancialYear")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<List<string>> GetFinancialYear()
        {
            var yers = await _reportsService.FinancialYear();
            return yers;
        }
        [HttpGet]
        [Route("GetTendersName")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<Dictionary<int, string>> GetTendersName()
        {
            var result = await _reportsService.GetTendersName();
            return result;
        }
        [Route("GetTenderStatuses")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<Dictionary<int, string>> GetTenderStatuses()
        {
            var result = await _reportsService.GetTenderStatuses();
            return result;
        }


        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("MessagesStatusReportAsync")]
        public async Task<QueryResult<MessagesModel>> MessagesStatusReportAsync(SearchCriteria searchCriteria)
        {
            var Notifications = await _reportsService.GetMessagesStatusReportAsync(searchCriteria);
            return Notifications;
        }


        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("TenderCountToTypesReport")]
        public async Task<List<TenderValueToTypesModel>> TenderCountToTypesReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.GetTendersCountForTenderToTypesReport(searchCriteria);
            return tenders;
        }



        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("TendersToAwardedSuppliersReport")]
        public async Task<List<TenderValueToTypesModel>> TendersToAwardedSuppliersReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            if (User.IsInRole(RoleNames.MonafasatAdmin) && searchCriteria.BranchId == 0)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.PleaseSelectBranch);
            }
            var tenders = await _reportsService.GetTenderForTenderToAwardedSuppliersReport(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("QualificationReportAsyncReport")]
        public async Task<QueryResult<QualificationModel>> QualificationReportAsyncReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.GetQualificationReport(searchCriteria);
            return tenders;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DownloadQualificationReport")]
        public async Task<byte[]> DownloadQualificationReport(TenderValueToTypeSearchCriteria searchCriteria)
        {

            searchCriteria.PageSize = 9999;
            var result = await _reportsService.GetQualificationReport(searchCriteria);

            var excel = PrepareTitelsExcelFile("QualificationReport", new string[]
            {
                Resources.QualificationResources.DisplayInputs.QualificationName,
                Resources.QualificationResources.DisplayInputs.PublishDate,
                Resources.QualificationResources.DisplayInputs.QualificationStatus,
                Resources.QualificationResources.DisplayInputs.QualificationType,

            });
            var worNumber = 2;
            foreach (var item in result.Items)
            {
                PrepareRowExcelFile(excel.Item2, new string[]
               {
                   item.QualificationName,
                   item.QualificationPublishDate.ToString(),
                   item.QualificationStatuse,
                   item.QualificationType,
               },
               worNumber); worNumber++;
            }
            PrepareRowExcelFile(excel.Item2, new string[]
            { Resources.SharedResources.DisplayInputs.TotalOperations, result.TotalCount.ToString() }, worNumber);




            return excel.Item1.GetAsByteArray();
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetAmountOfSavingsAsync")]
        public async Task<QueryResult<AmountOfSavingsViewModel>> GetAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var result = await _reportsService.GetAmountOfSavingsAsync(searchCriteria);
            return result;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTotalAmountOfSavingsAsync")]
        public async Task<TotalAmountOfSavingsViewModel> GetTotalAmountOfSavingsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var result = await _reportsService.GetTotalAmountOfSavingsAsync(searchCriteria);
            return result;
        }



        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DirectPurchaseReportAsyncReport")]
        public async Task<QueryResult<DirectPurchaseModel>> DirectPurchaseReportAsyncReport(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var tenders = await _reportsService.GetDirectPurchaseReport(searchCriteria);
            return tenders;
        }


        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("AgencyFileReport")]
        public async Task<QueryResult<AgencyFileViewModel>> AgencyFileReport(TenderValueToTypeSearchCriteria searchCriteria)
        {

            var tenders = await _reportsService.GetAgencyFileAsync(searchCriteria);
            return tenders;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DownloadAgencyFileReportAsync")]
        public async Task<byte[]> DownloadAgencyFileReport(TenderValueToTypeSearchCriteria searchCriteria)
        {


            searchCriteria.PageSize = 9999;
            var result = await _reportsService.GetAgencyFileAsync(searchCriteria);

            var excel = PrepareTitelsExcelFile("AgencyReport", new string[]
            {
                Resources.TenderResources.DisplayInputs.Agency,
                Resources.TenderResources.DisplayInputs.AgencyBranch,
                Resources.ReportResources.DisplayInputs.FirstTenderDate,
                Resources.ReportResources.DisplayInputs.LastTenderDate ,
                Resources.TenderResources.DisplayInputs.SummationOfGeneralTenders ,
                Resources.TenderResources.DisplayInputs.DirectPurchaseTendersCount  ,
                Resources.TenderResources.DisplayInputs.LimitedTender ,
                Resources.TenderResources.DisplayInputs.ReverseBidding ,
                Resources.TenderResources.DisplayInputs.FrameworkAgreement ,
                Resources.TenderResources.DisplayInputs.NumberOfPurchases ,
                Resources.ReportResources.DisplayInputs.TotalPurchasedValue

            });
            var worNumber = 2;
            foreach (var item in result.Items)
            {
                PrepareRowExcelFile(excel.Item2, new string[]
               {
                   item.AgencyName,
                   item.BranchName,
                   item.FirstTenderDate,
                   item.LastTenderDate,
                   item.TendersCount.ToString(),
                   item.DirectPurchaseCount.ToString(),
                   item.TenderLimitedValue.ToString(),
                   item.TenderReverseBiddingValue.ToString(),
                   item.TenderFrameworkAgreementValue.ToString(),
                   item.NumberOfPurchasedSuppliers.ToString(),
                   item.TotalPurchasedValue.ToString(),

               },
               worNumber); worNumber++;
            }
            PrepareRowExcelFile(excel.Item2, new string[]
            { Resources.SharedResources.DisplayInputs.TotalOperations, result.TotalCount.ToString() }, worNumber);




            return excel.Item1.GetAsByteArray();
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTendersReportList")]
        public async Task<QueryResult<BasicTenderModel>> GetTendersReportList(TenderSearchCriteriaModel criteria)
        {
            bool statusFlag = true;
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                statusFlag = false;
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.GetTendersReportList(tenderSearchCriteria);
            return _mapper.Map<QueryResult<BasicTenderModel>>(tenders, opt => opt.Items["statusFlag"] = statusFlag);

        }




        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DownloadTenderReport")]
        public async Task<byte[]> DownloadTenderReport(TenderSearchCriteriaModel criteria)
        {
            criteria.PageSize = 999;
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.GetTendersReportList(tenderSearchCriteria);
            var mapedTenders = _mapper.Map<QueryResult<BasicTenderModel>>(tenders, opt => opt.Items["statusFlag"] = true);
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");
                int rowNum = 1;
                //  for cell on row1 col1
                worksheet.Cells[rowNum, 1].Value = Resources.TenderResources.DisplayInputs.TenderName;
                worksheet.Cells[rowNum, 1].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 1].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 2].Value = Resources.TenderResources.DisplayInputs.SubmtionDate;
                worksheet.Cells[rowNum, 2].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 2].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 3].Value = Resources.TenderResources.DisplayInputs.TenderStatus;
                worksheet.Cells[rowNum, 3].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 3].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                if (mapedTenders.Items.Count() > 0)
                {
                    foreach (BasicTenderModel tenderItem in mapedTenders.Items)
                    {
                        rowNum++;
                        worksheet.Cells[rowNum, 1].Value = tenderItem.TenderName;
                        worksheet.Cells[rowNum, 2].Value = tenderItem.SubmitionDate.ToString();
                        worksheet.Cells[rowNum, 3].Value = tenderItem.TenderStatusName;
                    }
                }

                fileContents = package.GetAsByteArray();
            }

            return fileContents;

        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("Report_GetMostSuppliersHaveTendersAsync")]
        public async Task<QueryResult<MostSuppliersHaveTendersModel>> Report_GetMostSuppliersHaveTendersAsync(TenderValueToTypeSearchCriteria criteria)
        {
            var tenders = await _reportsService.ReportGetMostSuppliersHaveTendersListAsync(criteria);
            return tenders;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DailyNotificationsList")]
        public async Task<QueryResult<NotifyModel>> DailyNotificationsList(NotifySearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.GetDailyNotificationsList(searchCriteria);
            return tenders;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTendersSummaryAsync")]
        public async Task<QueryResult<TendersSummryReportViewModel>> GetTendersSummaryAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.GetTendersSummaryAsync(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DownloadTenderSummaryReport")]
        public async Task<byte[]> DownloadTenderSummaryReport(TenderValueToTypeSearchCriteria criteria)
        {
            criteria.PageSize = 999;
            var tenders = await _reportsService.GetTendersSummaryAsync(criteria);

            var mapedTenders = _mapper.Map<QueryResult<TendersSummryReportViewModel>>(tenders, opt => opt.Items["statusFlag"] = true);
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");
                int rowNum = 1;
                //  for cell on row1 col1
                worksheet.View.RightToLeft = true;
                worksheet.Cells[rowNum, 1].Value = Resources.TenderResources.DisplayInputs.Agency;
                worksheet.Cells[rowNum, 1].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 1].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 2].Value = Resources.TenderResources.DisplayInputs.AgencyBranch;
                worksheet.Cells[rowNum, 2].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 2].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 3].Value = Resources.TenderResources.DisplayInputs.SummationOfGeneralTenders;
                worksheet.Cells[rowNum, 3].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 3].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                ///////////////////////////////////////////////////////////////
                worksheet.Cells[rowNum, 4].Value = Resources.TenderResources.DisplayInputs.LimitedTender;
                worksheet.Cells[rowNum, 4].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 4].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 5].Value = Resources.TenderResources.DisplayInputs.ReverseBidding;
                worksheet.Cells[rowNum, 5].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 5].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 6].Value = Resources.TenderResources.DisplayInputs.FrameworkAgreement;
                worksheet.Cells[rowNum, 6].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 6].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 6].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 7].Value = Resources.TenderResources.DisplayInputs.SummationOfInvitationsAndDirectPurchaseTenders;
                worksheet.Cells[rowNum, 7].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 7].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 7].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 8].Value = Resources.TenderResources.DisplayInputs.NumberOfPurchases;
                worksheet.Cells[rowNum, 8].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 8].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 8].Style.Border.Top.Style = ExcelBorderStyle.Hair;
                ///////////////////////////////////////////





                worksheet.Cells[rowNum, 9].Value = Resources.TenderResources.DisplayInputs.SummationOfInvitationsAndDirectPurchaseTenders;
                worksheet.Cells[rowNum, 9].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 9].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 9].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 10].Value = Resources.TenderResources.DisplayInputs.NumberOfSuppliers;
                worksheet.Cells[rowNum, 10].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 10].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 10].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 11].Value = Resources.TenderResources.DisplayInputs.NumberOfSuppliersEnquiries;
                worksheet.Cells[rowNum, 11].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 11].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 11].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 12].Value = Resources.TenderResources.DisplayInputs.NumberOfOpenedOffers;
                worksheet.Cells[rowNum, 12].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 12].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 12].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 13].Value = Resources.TenderResources.DisplayInputs.NumberOfTechnicalChecked;
                worksheet.Cells[rowNum, 13].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 13].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 13].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 14].Value = Resources.TenderResources.DisplayInputs.NumberOfAwardded;
                worksheet.Cells[rowNum, 14].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 14].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 14].Style.Border.Top.Style = ExcelBorderStyle.Hair;


                /////////////////////////////////////
                worksheet.Cells[rowNum, 15].Value = Resources.TenderResources.DisplayInputs.TheElectronicOffersOnTenders;
                worksheet.Cells[rowNum, 15].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 15].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 15].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 16].Value = Resources.TenderResources.DisplayInputs.TheElectronicOffersOnDirectPurchase;
                worksheet.Cells[rowNum, 16].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 16].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 16].Style.Border.Top.Style = ExcelBorderStyle.Hair;


                if (mapedTenders.Items.Count() > 0)
                {
                    foreach (TendersSummryReportViewModel tenderItem in mapedTenders.Items)
                    {
                        rowNum++;
                        worksheet.Cells[rowNum, 1].Value = tenderItem.AgencyName;
                        worksheet.Cells[rowNum, 2].Value = tenderItem.BranchName;
                        worksheet.Cells[rowNum, 3].Value = tenderItem.TendersCount;
                        worksheet.Cells[rowNum, 4].Value = tenderItem.TenderLimitedValue;
                        worksheet.Cells[rowNum, 4].Value = tenderItem.TenderReverseBiddingValue;

                        worksheet.Cells[rowNum, 5].Value = tenderItem.TenderFrameworkAgreementValue;
                        worksheet.Cells[rowNum, 6].Value = tenderItem.DirectPurchaseCount;
                        worksheet.Cells[rowNum, 7].Value = tenderItem.NumberOfPurchasedSuppliers;
                        worksheet.Cells[rowNum, 8].Value = tenderItem.NumberOfOppenedOffers;
                        worksheet.Cells[rowNum, 9].Value = tenderItem.NumberTechnicalCheckedOfferd;
                        worksheet.Cells[rowNum, 9].Value = tenderItem.NumberAwardedOffered;
                        worksheet.Cells[rowNum, 9].Value = tenderItem.ElectronicOffersOnTenders;
                        worksheet.Cells[rowNum, 9].Value = tenderItem.ElectronicOffersOnDirectPurchase;
                    }
                }

                fileContents = package.GetAsByteArray();
            }

            return fileContents;

        }


        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTendersSummaryCountsAsync")]
        public async Task<TendersSummryReportCountsViewModel> GetTendersSummaryCountsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.GetTendersSummaryCountsAsync(searchCriteria);
            return result;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTendersPurshasesReportList")]
        public async Task<QueryResult<PurchaseModel>> GetTendersPurshasesReportList(TenderSearchCriteriaModel criteria)
        {
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.GetTendersPurshasesReportList(tenderSearchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTenderPurshasesReportTotalAmount")]
        public async Task<decimal> GetTenderPurshasesReportTotalAmount(TenderSearchCriteriaModel criteria)
        {
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var result = await _reportsService.GetTenderPurshasesReportTotalAmount(tenderSearchCriteria);
            return result;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DownloadTenderPurchaseReport")]
        public async Task<byte[]> DownloadTenderPurchaseReport(TenderSearchCriteriaModel criteria)
        {
            criteria.PageSize = 999;
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.GetTendersPurshasesReportList(tenderSearchCriteria);
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");
                int rowNum = 1;
                //  for cell on row1 col1
                worksheet.View.RightToLeft = true;
                worksheet.Cells[rowNum, 1].Value = Resources.TenderResources.DisplayInputs.TenderName;
                worksheet.Cells[rowNum, 1].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 1].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 2].Value = Resources.TenderResources.DisplayInputs.TenderPurshasingDate;
                worksheet.Cells[rowNum, 2].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 2].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 3].Value = Resources.TenderResources.DisplayInputs.CommercialName;
                worksheet.Cells[rowNum, 3].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 3].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 4].Value = Resources.TenderResources.DisplayInputs.CommercialNumber;
                worksheet.Cells[rowNum, 4].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 4].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 5].Value = Resources.TenderResources.DisplayInputs.ConditionsBookletValueRiyal;
                worksheet.Cells[rowNum, 5].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 5].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                if (tenders.Items.Count() > 0)
                {
                    foreach (PurchaseModel tenderItem in tenders.Items)
                    {
                        rowNum++;
                        worksheet.Cells[rowNum, 1].Value = tenderItem.TenderName;
                        worksheet.Cells[rowNum, 2].Value = tenderItem.PurshaseDate.ToString();
                        worksheet.Cells[rowNum, 3].Value = tenderItem.CommericalRegisterName;
                        worksheet.Cells[rowNum, 4].Value = tenderItem.CommericalRegisterNo;
                        worksheet.Cells[rowNum, 5].Value = tenderItem.ConditionsBookletPrice;
                    }
                }

                fileContents = package.GetAsByteArray();
            }

            return fileContents;

        }


        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("GetTendersCountReportList")]
        public async Task<QueryResult<TenderCountsModel>> GetTendersCountReportList(TenderSearchCriteriaModel criteria)
        {
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.GetTendersCountReportList(tenderSearchCriteria);
            int total = _reportsService.GetTendersCountReportsCount(tenderSearchCriteria);

            foreach (var item in tenders.Items)
            {
                item.Percent = string.Format("{0:0.00}", (item.Count / total) * 100); ;
            }

            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("FindSuppliersPurshaseReport")]
        public async Task<QueryResult<BasicTenderModel>> FindSuppliersPurshaseReport(TenderSearchCriteriaModel criteria)
        {
            bool statusFlag = true;
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                statusFlag = false;
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.FindSuppliersPurshaseReport(tenderSearchCriteria);
            QueryResult<BasicTenderModel> model = _mapper.Map<QueryResult<BasicTenderModel>>(tenders, opt => opt.Items["statusFlag"] = statusFlag);

            foreach (var item in model.Items)
            {
                var conditionBook = item.ConditionsBooklets.FirstOrDefault();
                var invitation = item.Invitations.FirstOrDefault();

                if (conditionBook != null)
                {
                    if (conditionBook.ConfirmPurchase)
                        item.InvitationStatus += Resources.TenderResources.DisplayInputs.Paid;
                    else
                        item.InvitationStatus += Resources.TenderResources.DisplayInputs.NotPaid;
                }

                if (invitation != null)
                {
                    item.InvitationStatus += invitation.InvitationStatus.NameAr;
                }

                if (string.IsNullOrEmpty(item.InvitationStatus))
                {
                    item.InvitationStatus = "-";
                }
            }

            return model;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DownloadSupplierPurchaseReportAsync")]
        public async Task<byte[]> DownloadSupplierPurchaseReport(TenderSearchCriteriaModel criteria)
        {
            criteria.PageSize = 999;
            bool statusFlag = true;
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                statusFlag = false;
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.FindSuppliersPurshaseReport(tenderSearchCriteria);
            QueryResult<BasicTenderModel> model = _mapper.Map<QueryResult<BasicTenderModel>>(tenders, opt => opt.Items["statusFlag"] = statusFlag);

            foreach (var item in model.Items)
            {
                var conditionBook = item.ConditionsBooklets.FirstOrDefault();
                var invitation = item.Invitations.FirstOrDefault();

                if (conditionBook != null)
                {
                    if (conditionBook.ConfirmPurchase)
                        item.InvitationStatus += Resources.TenderResources.DisplayInputs.Paid;
                    else
                        item.InvitationStatus += Resources.TenderResources.DisplayInputs.NotPaid;
                }

                if (invitation != null)
                {
                    item.InvitationStatus += invitation.InvitationStatus.NameAr;
                }

                if (string.IsNullOrEmpty(item.InvitationStatus))
                {
                    item.InvitationStatus = "-";
                }
            }

            // start casting report to Exel over byte arr
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {

                var worksheet = package.Workbook.Worksheets.Add("Report");
                worksheet.View.RightToLeft = true;

                int rowNum = 1;
                //  for cell on row1 col1
                worksheet.Cells[rowNum, 1].Value = Resources.TenderResources.DisplayInputs.TenderName;
                worksheet.Cells[rowNum, 1].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 1].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 1].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 2].Value = Resources.TenderResources.DisplayInputs.TenderNumber;
                worksheet.Cells[rowNum, 2].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 2].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 2].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 3].Value = Resources.TenderResources.DisplayInputs.TenderType;
                worksheet.Cells[rowNum, 3].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 3].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 3].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 4].Value = Resources.TenderResources.DisplayInputs.SubmtionDate;
                worksheet.Cells[rowNum, 4].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 4].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 4].Style.Border.Top.Style = ExcelBorderStyle.Hair;

                worksheet.Cells[rowNum, 5].Value = Resources.TenderResources.DisplayInputs.InvitationPurchaseStatus;
                worksheet.Cells[rowNum, 5].Style.Font.Size = 12;
                worksheet.Cells[rowNum, 5].Style.Font.Bold = true;
                worksheet.Cells[rowNum, 5].Style.Border.Top.Style = ExcelBorderStyle.Hair;


                if (tenders.Items.Count() > 0)
                {
                    foreach (BasicTenderModel tenderItem in model.Items)
                    {
                        rowNum++;
                        worksheet.Cells[rowNum, 1].Value = tenderItem.TenderName;
                        worksheet.Cells[rowNum, 2].Value = tenderItem.TenderNumber;
                        worksheet.Cells[rowNum, 3].Value = tenderItem.TenderTypeName;
                        worksheet.Cells[rowNum, 4].Value = tenderItem.SubmitionDate;
                        worksheet.Cells[rowNum, 5].Value = tenderItem.InvitationStatus;
                    }
                }

                fileContents = package.GetAsByteArray();
            }

            return fileContents;

        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportTendersToActivity")]
        public async Task<List<TenderValueToTypesModel>> ReportTendersToActivity(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportTendersToActivity(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportGetMostTendersActivitiesAsync")]
        public async Task<List<MostTendersActivitiesModel>> ReportGetMostTendersActivitiesAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportGetMostTendersActivitiesAsync(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("QualificationCountAsync")]
        public async Task<List<QualificationCountModel>> QualificationCountAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.QualificationCountAsync(searchCriteria);
            return tenders;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("QualificationCountListAsync")]
        public async Task<QueryResult<QualificationCountModel>> QualificationCountListAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.QualificationCountListAsync(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportGetMostSuppliersActivitiesAsync")]
        public async Task<List<MostTendersActivitiesModel>> ReportGetMostSuppliersActivitiesAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportGetMostSuppliersActivitiesAsync(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportGetMostSuppliersHaveTendersAsync")]
        public async Task<List<MostSuppliersHaveTendersModel>> ReportGetMostSuppliersHaveTendersAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportGetMostSuppliersHaveTendersAsync(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportGetTendersSalesAsync")]
        public async Task<List<TendersSalesModel>> ReportGetTendersSalesAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportGetTendersSalesAsync(searchCriteria);
            return tenders;
        }
        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportGetPublishedTendersAsync")]
        public async Task<List<TendersPublishingModel>> ReportGetPublishedTendersAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportGetPublishedTendersAsync(searchCriteria);
            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("ReportGetCountsStatisticsAsync")]
        public async Task<AgencyTenderStatisticsModel> ReportGetCountsStatisticsAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.ReportGetAgencyStatisticsAsync(searchCriteria);
            return tenders;
        }


        [HttpGet("ReportGetTendersCountReportAsync")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<List<TenderCountsModel>> ReportGetTendersCountReportAsync(TenderSearchCriteriaModel criteria)
        {
            var tenderSearchCriteria = _mapper.Map<TenderSearchCriteria>(criteria);
            var tenders = await _reportsService.ReportGetTendersCountReportAsync(tenderSearchCriteria);
            int total = _reportsService.GetTendersCountReportsCount(tenderSearchCriteria);

            foreach (var item in tenders)
            {
                item.Percent = string.Format("{0:0.00}", (item.Count / total) * 100); ;
            }

            return tenders;
        }

        [Authorize(RoleNames.ReportsPolicy)]
        [HttpGet("DirectInvitationsReport")]
        public async Task<List<DirectInvitationsReportModel>> DirectInvitationsReport(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var tenders = await _reportsService.DirectInvitationsReport(searchCriteria);
            return tenders;
        }

        #region TenderMonthlySalesReceipteReport

        [HttpGet("GetTenderSalesMonthlyCountsPerMonth")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<TenderSalesMonthlyCountsPerMonth> GetTenderSalesMonthlyCountsPerMonth(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.GetTenderSalesMonthlyCountsPerMonth(searchCriteria);
            return result;
        }

        [HttpGet("GetTenderSalesMonthlyRecipetReportPerAgency")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<QueryResult<TenderSalesMonthlyRecipetReportPerAgency>> GetTenderSalesMonthlyRecipetReportPerAgency(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.GetTenderSalesMonthlyRecipetReportPerAgency(searchCriteria);
            return result;
        }

        [HttpGet("GetAllTenderSalesMonthlyCountsPerMonth")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<TenderSalesMonthlyCountsPerMonth> GetAllTenderSalesMonthlyCountsPerMonth(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.GetAllTenderSalesMonthlyCountsPerMonth(searchCriteria);
            return result;
        }

        [HttpGet("GetTenderSalesMonthlyTenderDetails")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<QueryResult<TenderSalesMonthlyTenderDetails>> GetTenderSalesMonthlyTenderDetails(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.GetTenderSalesMonthlyTenderDetails(searchCriteria);
            return result;
        }
        [HttpGet("PerformanceReportAsync")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<QueryResult<PerformanceReportModel>> PerformanceReportAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.GetPerformanceReportAsync(searchCriteria);
            return result;
        }

        [HttpGet("ReportSpendingBySpendAgencyAsync")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendAgencyAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.ReportSpendingBySpendAgencyAsync(searchCriteria);
            return result;
        }
        [HttpGet("ReportSpendingBySpendCategoryAsync")]
        [Authorize(RoleNames.ReportsPolicy)]
        public async Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendCategoryAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.ReportSpendingBySpendCategoryAsync(searchCriteria);
            return result;
        }

        [HttpGet("ReportSpendingBySpendActivitiesAsync")]

        public async Task<QueryResult<ReportSpendingBySpendAgencyModel>> ReportSpendingBySpendActivitiesAsync(TenderValueToTypeSearchCriteria searchCriteria)
        {
            var result = await _reportsService.ReportSpendingBySpendActivitiesAsync(searchCriteria);
            return result;
        }
        #endregion TenderMonthlySalesReceipteReport


        #region DownloadHelper
        private Tuple<ExcelPackage, ExcelWorksheet> PrepareTitelsExcelFile(string worksheetName, string[] titels)
        {
            var package = new ExcelPackage();

            var worksheet = package.Workbook.Worksheets.Add(worksheetName);
            for (int i = 1; i <= titels.Length; i++)
            {
                worksheet.Cells[1, i].Value = titels[i - 1];
                worksheet.Cells[1, i].Style.Font.Size = 12;
                worksheet.Cells[1, i].Style.Font.Bold = true;
                worksheet.Cells[1, i].Style.Border.Top.Style = ExcelBorderStyle.Hair;
            }
            return new Tuple<ExcelPackage, ExcelWorksheet>(package, worksheet);

        }

        private void PrepareRowExcelFile(ExcelWorksheet worksheet, string[] values, int rowNumber)
        {
            for (int i = 0; i < values.Length; i++)
            {
                worksheet.Cells[rowNumber, i + 1].Value = values[i];
            }
        }
    }


    #endregion
}

