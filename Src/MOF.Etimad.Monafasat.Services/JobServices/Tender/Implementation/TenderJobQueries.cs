using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class TenderJobQueries : ITenderJobQueries
    {
        private readonly IAppDbContext _context;
        private readonly RootConfigurations _rootConfiguration;

        public TenderJobQueries(IAppDbContext context, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _context = context;
            _rootConfiguration = rootConfiguration.Value;
        }
        public async Task<List<EmarketPlaceRequest>> GetAwardedSupplierQuantityTableItemsValue(List<int> offerIds)
        {
            var Columns = await _context.Offers.Where(o => o.IsActive == true && offerIds.Contains(o.OfferId)).SelectMany(f => f.SupplierTenderQuantityTables)
                .Select(x => new EmarketPlaceRequest
                {
                    FormId = x.TenderQuantityTable.FormId,
                    TableId = x.Id,
                }).ToListAsync();
            return Columns;
        }

        public async Task<SRMFrameworkAgreementManageModel> FillVendorProducts(List<int> offerIds, List<EmarketPlaceResponse> clotypes)
        {
            var tender = await _context.Tenders.Include(r => r.TenderAgreementAgencies)
                .FirstOrDefaultAsync(t => t.Offers.Any(o => o.OfferId == offerIds.FirstOrDefault()));

            var tenderAwardingDate = await _context.TenderHistories
                   .Where(h => h.TenderId == tender.TenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).OrderByDescending(w => w.TenderHistoryId)
                   .Select(c => c.CreatedAt)
                   .FirstOrDefaultAsync();

            var offersList = await _context.Offers
                            .Include(sq => sq.SupplierTenderQuantityTables)
                            .ThenInclude(si => si.QuantitiyItemsJson)
                            .Include(sq => sq.SupplierTenderQuantityTables)
                            .ThenInclude(si => si.TenderQuantityTable)
                            .Where(o => o.IsActive == true && offerIds.Contains(o.OfferId)).ToListAsync();

            var offer = offersList.FirstOrDefault(); // To Be Changed Inside Vendors List

            var result = new SRMFrameworkAgreementManageModel
            {
                ReferenceNumber = tender.ReferenceNumber,
                ContractName = tender.TenderName,
                ContractType = tender.AgreementTypeId == 0 ? SRMContractType.Open : SRMContractType.Close,
                ValidityInfo = new ValidityInfo
                {
                    NumOfDays = tender.AgreementDays.HasValue ? (int)tender.AgreementDays : 0,
                    NumOfMonths = tender.AgreementMonthes.HasValue ? (int)tender.AgreementMonthes : 0,
                    NumOfYears = tender.AgreementYears.HasValue ? (int)tender.AgreementYears : 0,
                },

                AgencyList = tender.TenderAgreementAgencies.Any() ? tender.TenderAgreementAgencies.Select(a => a.AgencyCode).ToList() : new List<string>(),
                AwardDt = DateTime.Now,
                CreatedBy = tender.AgencyCode,
                TotalAmt = (decimal)offer.FinalPriceAfterDiscount,
                Note = !string.IsNullOrEmpty(offer.JustificationOfRecommendation) ? offer.JustificationOfRecommendation : "",
                Currency = "SAR",
                ValidFrom = tenderAwardingDate,
                VendorsList = offersList.Select(o => new VendorList
                {
                    AwardedAmt = o.TotalOfferAwardingValue != null ? (decimal)o.TotalOfferAwardingValue : (decimal)o.PartialOfferAwardingValue,
                    PurchaseCurrency = "SAR",
                    VendorId = o.CommericalRegisterNo,
                    ProductList = o.SupplierTenderQuantityTables.Count > 0 ?
                    o.SupplierTenderQuantityTables.Select(s => s.QuantitiyItemsJson).SelectMany(p => CollectList(p.SupplierTenderQuantityTableItems, p.SupplierTenderQuantityTable.TenderQuantityTable.FormId, clotypes)).ToList()
                    :
                    new List<ProductList>()
                }).ToList()
            };
            return result;
        }

        private static List<ProductList> CollectList(List<SupplierTenderQuantityTableItem> tableItems, long formId, List<EmarketPlaceResponse> clotypes)
        {
            List<ProductList> productLists = new List<ProductList>();
            var grouppedData = tableItems.GroupBy(d => d.ItemNumber).Select(d => new { rowNumber = d.Key, data = d });


            foreach (var datarow in grouppedData)
            {
                ProductList product = new ProductList();
                product.DeliveryDurationInfo = new DeliveryDurationInfo
                {
                    NumOfDays = 5,
                    NumOfMonths = 5,
                    NumOfYears = 3
                };

                var datarowObj = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.ItemId)?.ColumnId) != null;
                var datarowObjValue = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.ItemId)?.ColumnId).Select(d => d.Value).FirstOrDefault();

                product.ProductId = datarowObj == true ? datarowObjValue : "";
                product.ProductName = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Name)?.ColumnId) != null
                    ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Name)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "";


                product.UnitPrice = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Price)?.ColumnId).Sum(s => decimal.Parse(string.IsNullOrEmpty(s.Value) ? "0" : s.Value));


                product.DiscountPercen = decimal.Parse(
                   (datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.DiscountPercentage)?.ColumnId) != null && !(string.IsNullOrEmpty(datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.DiscountPercentage)?.ColumnId).Select(d => d.Value).FirstOrDefault())))
                   ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.DiscountPercentage)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "0");


                product.VATAmt = decimal.Parse(
                    datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.VAT)?.ColumnId) != null
                    ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.VAT)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "0");


                product.Quantity = decimal.Parse(
                    datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Quantity)?.ColumnId) != null
                    ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Quantity)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "0");


                product.UnitOfMeasure = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Unit)?.ColumnId) != null
                   ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Unit)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "";


                product.Desc = datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Description)?.ColumnId) != null
                   ? datarow.data.Where(d => d.ColumnId == clotypes.FirstOrDefault(item => item.FormId == formId && item.ColumnTypeId == (int)Enums.ColumnValueEnum.Description)?.ColumnId).Select(d => d.Value).FirstOrDefault() : "";
                productLists.Add(product);
            }

            return productLists;
        }


        public async Task<List<Tender>> GetAllFinalAwardedTendersForEmarketPlace()
        {
            List<Tender> tenderList = new List<Tender>();
            int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
            int esclationPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.EscalationPeriod);
            List<Tender> tenders = await _context.Tenders
             .Include(x => x.Offers)
             .Include(x => x.TenderHistories)
             .Include(x => x.AgencyCommunicationRequests)
             .ThenInclude(x => x.PlaintRequests)
             .Where(x => x.IsActive == true && x.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement && x.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed && x.IsSendToEmarketPlace != true)
             .ToListAsync();
            tenders.ForEach(tender =>
            {
                if (tender.isTenderFinalAwarded(esclationPeriod, plaintReviewingPeriod))
                    tenderList.Add(tender);
            });
            return tenderList;
        }


        public async Task<List<Tender>> GetAllFinishedStoppingAwardingPeriodTenders()
        {
            List<Tender> tenderList = new List<Tender>();
            var tenders = await _context.Tenders.Include(t => t.TenderHistories)
                .Where(t => t.IsNotificationSentForStoppingPeriod != true && t.AwardingStoppingPeriod != null && t.IsActive == true && t.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).ToListAsync();
            tenders.ForEach(tender =>
            {
                var tenderAwardingDate = tender.TenderHistories.OrderByDescending(h => h.TenderHistoryId).FirstOrDefault(h => h.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed).CreatedAt;

                var result = DateTime.Now.Date >= tenderAwardingDate.AddDays(tender.AwardingStoppingPeriod.Value).Date;
                if (result)
                    tenderList.Add(tender);
            });
            return tenderList;
        }

        public async Task<List<BiddingRound>> FindFinishedBiddingRounds()
        {
            DateTime currentDateTime = DateTime.Now;
            return await _context.BiddingRounds
                .Where(b => b.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed || b.Tender.TenderStatusId == (int)Enums.TenderStatus.Bidding)
                .Where(t => t.IsActive == true &&
                t.StartDate < currentDateTime && t.EndDate <= currentDateTime &&
                (t.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started || t.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New)
                ).ToListAsync();
        }

        public async Task<List<BiddingRound>> FindPendingBiddingRounds()
        {
            DateTime currentDateTime = DateTime.Now;
            return await _context.BiddingRounds
                .Where(b => b.Tender.TenderStatusId == (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed || b.Tender.TenderStatusId == (int)Enums.TenderStatus.Bidding)
                .Include(b => b.Tender)
                .ThenInclude(b => b.Offers)
                .ThenInclude(b => b.Supplier)
                .ThenInclude(b => b.SupplierUserProfiles)
                .Include(b => b.Tender)
                .ThenInclude(b => b.TenderHistories)
                .Where(t => t.IsActive == true &&
                t.StartDate <= currentDateTime && t.EndDate >= currentDateTime &&
                t.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.New).ToListAsync();
        }

        public async Task<List<Tender>> FindTendersToOpenOffers(int id)
        {
            DateTime currentDateTime = DateTime.Now;
            return await _context.Tenders
                .Where(x => (x.OpeningNotificationSent == null || x.OpeningNotificationSent == false) && x.OffersOpeningCommitteeId.HasValue && x.IsActive == true)
                .Where(t => t.TenderStatusId == (int)Enums.TenderStatus.Approved)
                .WhereIf(id != 0, a => a.TenderId == id)
                .Where(t => t.OffersOpeningDate.HasValue && currentDateTime.AddDays(1) >= t.OffersOpeningDate.Value)
                .ToListAsync();

        }

        public async Task<List<GovAgency>> FindAgenciesByAgencyCodes(List<string> agencyCodes)
        {
            return await _context.GovAgencies.Where(a => a.IsActive == true && agencyCodes.Contains(a.AgencyCode)).ToListAsync();
        }

        public async Task<List<Tender>> FindTendersToCheckOffers()
        {
            DateTime currentDateTime = DateTime.Now;
            return await _context.Tenders
                .Where(x => x.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                .Where(x => (x.CheckingNotificationSent == null || x.CheckingNotificationSent == false) && x.IsActive == true)
                .Where(d => ((d.IsLowBudgetDirectPurchase == true) || d.DirectPurchaseCommitteeId.HasValue))
                .Where(t => t.TenderStatusId == (int)Enums.TenderStatus.Approved)
                .Where(t => t.OffersCheckingDate.HasValue && currentDateTime >= t.OffersCheckingDate.Value)
                .ToListAsync();
        }
    }
}
