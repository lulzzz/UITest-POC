using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class NegotiationQueries : INegotiationQueries
    {
        private IAppDbContext _context;
        public NegotiationQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<NegotiationFirstSatgeSupplierOfferInfo> FindSupplierOfferDetails(int NegotiationId, int offerid)
        {

            var Query = await _context.NegotiationFirstStageSuppliers
                .Where(w => w.OfferId == offerid && w.NegotiationId == NegotiationId /*&& w.NegotiationSupplierStatusId == (int)Enums.enNegotiationFirstStageSupplierStatus.PendeingSupplierReply*/).Select(s =>
                              new NegotiationFirstSatgeSupplierOfferInfo
                              {
                                  ReductionLetter = new NegotiationAttachmentViewModel { FileNetReferenceId = s.NegotiationFirstStage.Attachments.FirstOrDefault().FileNetReferenceId, Name = s.NegotiationFirstStage.Attachments.FirstOrDefault().Name },
                                  StatusId = (Enums.enNegotiationSupplierStatus)s.NegotiationSupplierStatusId,
                                  StatusName = s.NegotiationSupplierStatus.Name,
                                  TenderIdString = Util.Encrypt(s.Offer.TenderId),
                                  NegotiationIdString = Util.Encrypt(s.NegotiationId),
                                  Days = ((int)Math.Floor((decimal)s.NegotiationFirstStage.SupplierReplyPeriodHours) / 24),
                                  Hours = s.NegotiationFirstStage.SupplierReplyPeriodHours % 24,
                                  RemainingDays = CalculateRemainingDays(s.NegotiationFirstStage.SupplierReplyPeriodHours, s.PeriodStartDateTime),
                                  RemainingHours = CalculateRemainingHours(s.NegotiationFirstStage.SupplierReplyPeriodHours, s.PeriodStartDateTime),
                                  RemaininMinutes = CalculateRemaininMinutes(s.NegotiationFirstStage.SupplierReplyPeriodHours, s.PeriodStartDateTime),
                                  OfferMainAmount = s.Offer.FinalPriceAfterDiscount.Value,
                                  NegotiationPercent = s.NegotiationFirstStage.DiscountAmount,
                                  OfferMainAmountAfterDiscount = s.Offer.FinalPriceAfterDiscount.Value
                                  - ((s.NegotiationFirstStage.DiscountAmount / 100) * s.Offer.FinalPriceAfterDiscount.Value),
                                  OfferMainAmountAfterDiscountText = new ConvertNumberToText(s.Offer.FinalPriceAfterDiscount.Value
                                  - ((s.NegotiationFirstStage.DiscountAmount / 100) * s.Offer.FinalPriceAfterDiscount.Value), new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic()
                              }).FirstOrDefaultAsync();
            return Query;

        }

        public async Task<NegotiationFirstSatgeSupplierOfferInfo> FindSupplierOfferDetailsForNegotiationFirstStage(int NegotiationId, int offerid)
        {

            var Query = await _context.NegotiationFirstStageSuppliers
                .Where(w => w.OfferId == offerid && w.NegotiationId == NegotiationId /*&& w.NegotiationSupplierStatusId == (int)Enums.enNegotiationFirstStageSupplierStatus.PendeingSupplierReply*/).Select(s =>
                              new NegotiationFirstSatgeSupplierOfferInfo
                              {
                                  ReductionLetter = new NegotiationAttachmentViewModel { FileNetReferenceId = s.NegotiationFirstStage.Attachments.FirstOrDefault().FileNetReferenceId, Name = s.NegotiationFirstStage.Attachments.FirstOrDefault().Name },
                                  StatusId = (Enums.enNegotiationSupplierStatus)s.NegotiationSupplierStatusId,
                                  StatusName = s.NegotiationSupplierStatus.Name,
                                  TenderIdString = Util.Encrypt(s.Offer.TenderId),
                                  NegotiationIdString = Util.Encrypt(s.NegotiationId),
                                  Days = ((int)Math.Floor((decimal)s.NegotiationFirstStage.SupplierReplyPeriodHours) / 24),
                                  Hours = s.NegotiationFirstStage.SupplierReplyPeriodHours % 24,
                                  RemainingDays = CalculateRemainingDays(s.NegotiationFirstStage.SupplierReplyPeriodHours, s.PeriodStartDateTime),
                                  RemainingHours = CalculateRemainingHours(s.NegotiationFirstStage.SupplierReplyPeriodHours, s.PeriodStartDateTime),
                                  RemaininMinutes = CalculateRemaininMinutes(s.NegotiationFirstStage.SupplierReplyPeriodHours, s.PeriodStartDateTime),
                                  OfferMainAmount = s.Offer.FinalPriceAfterDiscount.Value,
                                  NegotiationPercent = ((s.Offer.FinalPriceAfterDiscount - s.NegotiationFirstStage.DiscountAmount) / s.Offer.FinalPriceAfterDiscount) * 100,
                                  OfferMainAmountAfterDiscount = s.NegotiationFirstStage.DiscountAmount,
                                  OfferMainAmountAfterDiscountText = new ConvertNumberToText(s.NegotiationFirstStage.DiscountAmount, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic()
                              }).FirstOrDefaultAsync();
            return Query;

        }

        private static int CalculateRemainingDays(int SupplierReplyPeriodHours, DateTime? StartDate)
        {
            if (StartDate == null)
                return 0;
            TimeSpan RemainingDays = StartDate.Value.AddHours(SupplierReplyPeriodHours).Subtract(DateTime.Now);
            if (RemainingDays.Days <= 0)
                return 0;
            return int.Parse(RemainingDays.Days.ToString());


        }
        private static int CalculateRemainingHours(int SupplierReplyPeriodHours, DateTime? StartDate)
        {
            if (StartDate == null)
                return 0;

            TimeSpan RemainingHours = StartDate.Value.AddHours(SupplierReplyPeriodHours).Subtract(DateTime.Now);
            if (RemainingHours.Hours <= 0)
                return 0;
            return int.Parse(RemainingHours.Hours.ToString());

        }
        private static int CalculateRemaininMinutes(int SupplierReplyPeriodHours, DateTime? StartDate)
        {
            if (StartDate == null)
                return 0;
            TimeSpan RemaininMinutes = StartDate.Value.AddHours(SupplierReplyPeriodHours).Subtract(DateTime.Now);
            if (RemaininMinutes.Minutes <= 0)
                return 0;
            return int.Parse(RemaininMinutes.Minutes.ToString());

        }
        public async Task<SupplierTenderMainInfo> FindTenderDetails(int TenderId)
        {
            var Query = await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest)
              .Where(w => w.AgencyCommunicationRequest.TenderId == TenderId).Select(
              s =>
              new SupplierTenderMainInfo
              {
                  AgencyName = s.AgencyCommunicationRequest.Tender.Agency.NameArabic,
                  PurposeofTender = s.AgencyCommunicationRequest.Tender.Purpose,
                  ReferenceNumber = s.AgencyCommunicationRequest.Tender.ReferenceNumber,
                  TenderNumber = s.AgencyCommunicationRequest.Tender.TenderNumber,
                  RequestType = Resources.CommunicationRequest.DisplayInputs.NegotiationFirstStage,
                  TenderName = s.AgencyCommunicationRequest.Tender.TenderName,
                  TenderTypeName = s.AgencyCommunicationRequest.Tender.TenderType.NameAr

              }).FirstOrDefaultAsync();
            return Query;
        }
        public async Task<long> FindFirstQTId(int negotiationId, int formid)
        {
            var Query = await _context.NegotiationSupplierQuantityTables.Include(d => d.SupplierQuantityTable).ThenInclude(f => f.TenderQuantityTable)
                .Where(d => d.refNegotiationSecondStage == negotiationId && d.SupplierQuantityTable.TenderQuantityTable.FormId == formid && d.NegotiationQuantityItemJson != null)
                .Select(d => d.Id)
               .FirstOrDefaultAsync();
            return Query;
        }
        public async Task<SupplierTenderMainInfo> FindTenderDetailsForSecondNegotiation(int TenderId)
        {
            var Query = await _context.Tenders
              .Where(w => w.TenderId == TenderId && w.IsActive == true).Select(
              s =>
              new SupplierTenderMainInfo
              {
                  AgencyName = s.Agency.NameArabic,
                  PurposeofTender = s.Purpose,
                  ReferenceNumber = s.ReferenceNumber,
                  TenderNumber = s.TenderNumber,
                  RequestType = Resources.CommunicationRequest.DisplayInputs.NegotiationSecondStage,
                  TenderName = s.TenderName,
                  TenderTypeName = s.TenderType.NameAr

              }).FirstOrDefaultAsync();
            return Query;
        }
        public async Task<NegotiationFirstStage> FindWithAgencyRequestById(int NegotiationId)
        {
            var negotiation = await _context.NegotiationFirstStages.Where(d => d.NegotiationId == NegotiationId).Include(e => e.Attachments).Include(r => r.AgencyCommunicationRequest)
                .ThenInclude(t => t.Tender)
                .FirstOrDefaultAsync();
            return negotiation;
        }
        public async Task<NegotiationFirstStage> FindWithAgencyRequestAndSuppliersById(int NegotiationId)
        {
            var negotiation = await _context.NegotiationFirstStages
                .Where(d => d.NegotiationId == NegotiationId && d.IsActive == true)
                .Include(e => e.Attachments).Include(r => r.AgencyCommunicationRequest)
                .ThenInclude(t => t.Tender)
                .Include(t => t.NegotiationFirstStageSuppliers)
                .FirstOrDefaultAsync();
            return negotiation;
        }
        public async Task<NegotiationFirstStage> FindNegotiationWithAgencyRequestById(int negotiationId)
        {
            var negotiation = await _context.NegotiationFirstStages
                .Where(n => n.NegotiationId == negotiationId && n.IsActive == true)
                .Include(n => n.Attachments)
                .FirstOrDefaultAsync();

            var negotiationCommunicatoinRequest = await _context.AgencyCommunicationRequests
                .Where(c => c.AgencyRequestId == negotiation.AgencyRequestId).Include(c => c.Tender).FirstOrDefaultAsync();

            negotiation.SetNegotiationCommunicationRequest(negotiationCommunicatoinRequest);
            return negotiation;
        }

        public async Task<NegotiationFirstStage> FindWithSuppliersById(int NegotiationId)
        {
            return await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(d => d.Tender).Include(r => r.NegotiationFirstStageSuppliers).ThenInclude(w => w.Supplier)
                .Where(d => d.NegotiationId == NegotiationId)
                .FirstOrDefaultAsync();
        }
        public async Task<Supplier> GetSupplierInfoByCr(string cR)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.IsActive == true && x.SelectedCr == cR);
            return supplier;
        }
        public async Task<NegotiationFirstStage> GetNegotiationFirstStageWithAgency(int NegotiationId)
        {
            return await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).ThenInclude(d => d.Tender).Include(r => r.NegotiationFirstStageSuppliers)
                .Where(d => d.NegotiationId == NegotiationId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PendingAgencyRequestAlarm>> GetPendingNegotiations(string cR)
        {
            var result = await _context.NegotiationFirstStageSuppliers
                  .Where(d => d.Offer.CommericalRegisterNo == cR
                  && !d.IsReported && d.NegotiationSupplierStatusId == (int)Enums.enNegotiationSupplierStatus.PendeingSupplierReply)
                  .Select(s =>
               new PendingAgencyRequestAlarm
               {
                   NegotiationIdString = Util.Encrypt(s.NegotiationId),
                   RequestIdString = Util.Encrypt(s.NegotiationFirstStage.AgencyRequestId),
                   NegotiationTypeName = s.NegotiationFirstStage.NegotiationType.Name,
                   TenderIdString = Util.Encrypt(s.NegotiationFirstStage.AgencyCommunicationRequest.TenderId),
                   TenderName = s.NegotiationFirstStage.AgencyCommunicationRequest.Tender.TenderName,
                   AgencyRequestTypeId = s.NegotiationFirstStage.AgencyCommunicationRequest.AgencyRequestTypeId,
                   AgencyRequestTypeIdString = Util.Encrypt(s.NegotiationFirstStage.AgencyCommunicationRequest.AgencyRequestTypeId),
                   AgencyRequestTypeName = s.NegotiationFirstStage.AgencyCommunicationRequest.AgencyRequestType.Name,
                   RequestCreatedTime = s.CreatedAt
               }).ToListAsync();
            return result;

        }
        public async Task<NegotiationSecondStage> FindSecondStageNegotiationbyId(int NegotiationId)
        {
            return await _context.NegotiationSecondStages
                .Include(v => v.negotiationSupplierQuantitiestable).ThenInclude(d => d.NegotiationQuantityItemJson)//.ThenInclude(r=>r.NegotiationSupplierQuantityTableItems)
                .Include(e => e.AgencyCommunicationRequest).Include(e => e.NegotiationStatus)
                .Include(e => e.Offer).ThenInclude(t => t.Tender)
                .Where(d => d.NegotiationId == NegotiationId).FirstOrDefaultAsync();
        }
        public async
       Task<NegotiationSecondStage> FindNegotiationSecondStageByTenderId(int TenderId)
        {
            return await _context.NegotiationSecondStages.Include(d => d.Offer).Include(v => v.negotiationSupplierQuantitiestable).ThenInclude(d => d.NegotiationQuantityItemJson)
                .Where(d => d.Offer.TenderId == TenderId).FirstOrDefaultAsync();
        }
        public async Task<NegotiationSecondStage> FindSecondStageNegotiationWithTablesbyId(int NegotiationId)
        {
            return await _context.NegotiationSecondStages.Include(v => v.negotiationSupplierQuantitiestable).ThenInclude(d => d.NegotiationQuantityItemJson)
                .Where(d => d.NegotiationId == NegotiationId).FirstOrDefaultAsync();
        }

        public async Task<List<TenderQuantityItemDTO>> FindSecondStageNegotiationbyTQIId(int NegotiationId)
        {

            var data = await _context.NegotiationSupplierQuantityTables
                .Include(x => x.NegotiationQuantityItemJson).Include(s => s.NegotiationSecondStage).ThenInclude(x => x.Offer)
                .Where(n => n.refNegotiationSecondStage == NegotiationId).ToListAsync();
            var tables = data.Select(
                t => new QuantitiesTemplateModel
                {

                    QuantitiesItems = t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Select(i => new TenderQuantityItemDTO
                    {
                        Id = i.Id,
                        TableId = t.Id, //t.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.FirstOrDefault(a => a.Id == i.Id).Id,
                        TableName = t.Name, // string.IsNullOrEmpty(x.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Name) ? "اسم الجدول" :
                                            // x.TenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.TenderQuantityTableItems.Any(i => i.Id == q.Id)).Name,
                        ColumnId = i.ColumnId,
                        ItemNumber = i.ItemNumber,
                        TenderId = t.NegotiationSecondStage.Offer.TenderId,
                        TenderFormHeaderId = i.TenderFormHeaderId,
                        TemplateId = i.ActivityTemplateId,
                        Value = i.Value,
                        IsDefault = true,
                        ColumnName = ""
                    }).ToList(),
                }


                ).ToList();
            var result = tables.SelectMany(s => s.QuantitiesItems).ToList();

            return result;
        }

        public async Task<List<TableModel>> FindTableHtmlTemplatebyNegotiationId(int NegotiationId, int tenderid)
        {
            var tables = await _context.NegotiationSupplierQuantityTables.Include(a => a.NegotiationQuantityItemJson).Include(d => d.SupplierQuantityTable)
                .Where(w => w.refNegotiationSecondStage == NegotiationId && w.IsActive == true
                && w.SupplierQuantityTable.Offer.IsActive == true && w.SupplierQuantityTable.Offer.OfferStatusId == (int)Enums.OfferStatus.Received).ToListAsync();

            var tendertables = await _context.TenderQuantityTables
                .Where(w => w.TenderId == tenderid && w.IsActive == true).ToListAsync();


            return tables.Where(w => w.NegotiationQuantityItemJson != null && w.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Any())
                          .Select(s => new TableModel
                          {
                              FormId = tendertables.FirstOrDefault(d => d.Id == s.SupplierQuantityTable.TenederQuantityId).FormId,
                              TableId = s.Id.ToString(),
                              TableName = s.Name,
                              TenderId = tenderid
                          }).ToList();
        }
        public async Task<QuantitiesTemplateModel> FindNegotiationQuantityItemsForSecondNegotiation(int offerId)
        {
            var entitiess = await _context.NegotiationSecondStages
                .Where(t => t.IsActive == true && t.NegotiationId == offerId && t.Offer.IsActive == true && t.Offer.OfferStatusId == (int)Enums.OfferStatus.Received).AsTracking()
                .Select(x => new QuantitiesTemplateModel()
                {
                    TemplateYears = x.AgencyCommunicationRequest.Tender.TemplateYears,
                    TenderID = x.AgencyCommunicationRequest.TenderId,
                    OfferId = x.OfferId,
                    FormIds = x.AgencyCommunicationRequest.Tender.TenderQuantityTables.Where(t => t.IsActive == true && t.QuantitiyItemsJson.TenderQuantityTableItems != null).Select(f => f.FormId).Distinct().ToList(),
                    TenderCreatedByTypeId = x.AgencyCommunicationRequest.Tender.CreatedByTypeId,
                    TenderIdString = Util.Encrypt(x.AgencyCommunicationRequest.TenderId),
                    PreQualificationId = x.AgencyCommunicationRequest.Tender.PreQualificationId,
                    InvitationTypeId = x.AgencyCommunicationRequest.Tender.InvitationTypeId,
                    HasAlternativeOffer = x.AgencyCommunicationRequest.Tender.HasAlternativeOffer ?? false,
                    TenderName = x.AgencyCommunicationRequest.Tender.TenderName,
                    TenderStatusId = x.AgencyCommunicationRequest.Tender.TenderStatusId,
                    ReferenceNumber = x.AgencyCommunicationRequest.Tender.ReferenceNumber,
                    TenderNumber = x.AgencyCommunicationRequest.Tender.TenderNumber,
                    TenderTypeId = x.AgencyCommunicationRequest.Tender.TenderTypeId,
                    OfferPresentationWayId = x.AgencyCommunicationRequest.Tender.OfferPresentationWayId,
                })
                .FirstOrDefaultAsync();
            return entitiess;
        }

        public async Task<SecondStageNegotiationModelcs> FindNegotiationSecondbyId(int negotiationId)
        {

            var entity = await _context.NegotiationSecondStages
                .Where(d => d.IsActive == true && d.NegotiationId == negotiationId)
            .Select(e => new SecondStageNegotiationModelcs
            {
                CR = e.Offer.CommericalRegisterNo,
                Days = ((int)Math.Floor((decimal)e.SupplierReplyPeriodHours) / 24),
                Hours = e.SupplierReplyPeriodHours % 24,
                NegotiationId = e.NegotiationId,
                NegotiationIdString = Util.Encrypt(e.NegotiationId),
                RejectionReason = e.RejectionReason,
                StatusId = e.StatusId,
                tenderStatusId = e.AgencyCommunicationRequest.Tender.TenderStatusId,
                StatusName = e.NegotiationStatus.Name,
                RemainingDays = CalculateRemainingDays(e.SupplierReplyPeriodHours, e.PeriodStartDate),
                RemainingHours = CalculateRemainingHours(e.SupplierReplyPeriodHours, e.PeriodStartDate),
                RemaininMinutes = CalculateRemaininMinutes(e.SupplierReplyPeriodHours, e.PeriodStartDate),
                TenderIdString = Util.Encrypt(e.Offer.TenderId),
            }).FirstOrDefaultAsync();
            return entity;
        }
        public async Task<SecondStageNegotiationModelcs> FindNegotiationSecondbyId(int negotiationId, int userId)
        {

            var entity = await _context.NegotiationSecondStages
                .Where(d => d.IsActive == true && d.NegotiationId == negotiationId)
            .Select(e => new SecondStageNegotiationModelcs
            {
                CR = e.Offer.CommericalRegisterNo,
                Days = ((int)Math.Floor((decimal)e.SupplierReplyPeriodHours) / 24),
                Hours = e.SupplierReplyPeriodHours % 24,
                NegotiationId = e.NegotiationId,
                NegotiationIdString = Util.Encrypt(e.NegotiationId),
                RejectionReason = e.RejectionReason,
                StatusId = e.StatusId,
                tenderStatusId = e.AgencyCommunicationRequest.Tender.TenderStatusId,
                StatusName = e.NegotiationStatus.Name,
                RemainingDays = CalculateRemainingDays(e.SupplierReplyPeriodHours, e.PeriodStartDate),
                RemainingHours = CalculateRemainingHours(e.SupplierReplyPeriodHours, e.PeriodStartDate),
                RemaininMinutes = CalculateRemaininMinutes(e.SupplierReplyPeriodHours, e.PeriodStartDate),
                TenderIdString = Util.Encrypt(e.Offer.TenderId),
                IsUserHasAccessToLowBudgetTender = (e.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && e.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase == true && e.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == userId),
            }).FirstOrDefaultAsync();
            return entity;
        }


        public async Task<SecondStageNegotiationModelcs> FindNegotiationSecondStagebyId(int negotiationId)
        {
            var entity = await _context.NegotiationSecondStages
                .Where(d => d.NegotiationId == negotiationId)
            .Select(e => new SecondStageNegotiationModelcs
            {
                NegotiationId = e.NegotiationId,
                NegotiationIdString = Util.Encrypt(e.NegotiationId),
                RejectionReason = e.RejectionReason,
                StatusId = e.StatusId,
                StatusName = e.NegotiationStatus.Name,
                TenderIdString = Util.Encrypt(e.Offer.TenderId),
                CR = e.Offer.CommericalRegisterNo
            }).FirstOrDefaultAsync();
            return entity;
        }

        #region Negotiation second stage

        public async Task<int> GetNegotaitionTableQuantityItems(long tableId)
        {
            var table = await _context.NegotiationSupplierQuantityTables.Include(a => a.NegotiationQuantityItemJson).Where(a => a.Id == tableId).FirstOrDefaultAsync();
            if (table.NegotiationQuantityItemJson == null)
            {
                return 0;
            }
            var result = table.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.GroupBy(t => t.ItemNumber).Select(a => a.Count()).ToList();
            return result.Count == 0 ? 0 : result.Max(a => a);
        }
        public async Task<QueryResult<TenderQuantityItemDTO>> GetSupplierQTableItemsForNegotiation(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            Check.ArgumentNotNullOrEmpty(nameof(quantityTableSearchCretriaModel.OfferId), quantityTableSearchCretriaModel.OfferId.ToString());
            var table = await _context.NegotiationSupplierQuantityTables.Include(a => a.NegotiationQuantityItemJson).FirstOrDefaultAsync(t => t.refNegotiationSecondStage == quantityTableSearchCretriaModel.negotiationId
                                && t.Id == quantityTableSearchCretriaModel.QuantityTableId && t.IsActive == true);
            var offerEntity = await table.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems
                .OrderBy(d => d.ItemNumber)
                .Select(s => new TenderQuantityItemDTO
                {
                    ColumnId = s.ColumnId,
                    ItemNumber = s.ItemNumber,
                    ColumnName = "",
                    TableName = table.Name,
                    TableId = table.Id,
                    TemplateId = s.ActivityTemplateId,
                    TenderId = quantityTableSearchCretriaModel.TenderId,
                    TenderFormHeaderId = s.TenderFormHeaderId,
                    Value = s.Value,
                    IsDefault = true,
                    Id = s.Id
                }).ToQueryResult(quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * quantityTableSearchCretriaModel.CellsCount);
            return offerEntity;
        }

        public async Task<NegotiationSecondStage> GetNegotiationByQuantityTableId(int negotiationQuantityTableId)
        {
            var result = await _context.NegotiationSecondStages.Include(d => d.AgencyCommunicationRequest).Include(d => d.negotiationSupplierQuantitiestable).ThenInclude(d => d.NegotiationQuantityItemJson)
                .Where(i => i.IsActive == true && i.negotiationSupplierQuantitiestable.Any(s => s.Id == negotiationQuantityTableId))
                .FirstOrDefaultAsync();

            var tablesIDs = result.negotiationSupplierQuantitiestable.Where(s=>s.NegotiationQuantityItemJson != null && s.NegotiationQuantityItemJson.NegotiationSupplierQuantityTableItems.Any()).Select(t => t.Id);

            foreach (var id in tablesIDs)
            {
                var items = await _context.NegotiationQuantityTableItemsView.Where(x => x.NegoTableId == id).ToListAsync();



                result.negotiationSupplierQuantitiestable.FirstOrDefault(t => t.Id == id).NegotiationQuantityItemJson = new NegotiationQuantityItemJson()
                {
                    Id = items.FirstOrDefault().QTableJsonId,
                    NegotiationSupplierQuantityTableId = id,
                    NegotiationSupplierQuantityTableItems = items.Select(i => new NegotiationSupplierQuantityTableItem()
                    {
                        Id = i.Id,
                        ActivityTemplateId = i.ActivityTemplateId,
                        ColumnId = i.ColumnId,
                        ItemNumber = i.ItemNumber.Value,
                        TenderFormHeaderId = i.TenderFormHeaderId,
                        Value = i.Value
                    }).ToList()
                };
            }

            return result;
        }
        public async Task<Offer> FindOfferByNegotiationTableId(int negotiationQuantityTableId)
        {
            return await _context.NegotiationSupplierQuantityTables
                .Where(i => i.IsActive == true && i.Id == negotiationQuantityTableId)
                .Select(d => d.SupplierQuantityTable.Offer)
              .FirstOrDefaultAsync();
        }

        public async Task<Negotiation> FindNegotiationById(int NegotiationId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(NegotiationId), NegotiationId.ToString());
            var model = await _context.Negotiations.Include(a => a.AgencyCommunicationRequest).ThenInclude(t => t.Tender)
                .Where(p => p.NegotiationId == NegotiationId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<NegotiationSecondStage> FindNegotiationSecondStageById(int NegotiationId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(NegotiationId), NegotiationId.ToString());
            var model = await _context.NegotiationSecondStages.AsNoTracking().Include("negotiationSupplierQuantitiestable.NegotiationQuantityItemJson")
                .Include(a => a.Offer).ThenInclude(x => x.Supplier).Include(a => a.AgencyCommunicationRequest).ThenInclude(t => t.Tender)
                .Where(p => p.NegotiationId == NegotiationId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<NegotiationSecondStage> FindNegotiationSecondStageForSupplierById(int NegotiationId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(NegotiationId), NegotiationId.ToString());
            var model = await _context.NegotiationSecondStages
                .Where(p => p.NegotiationId == NegotiationId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<bool> IsTenderHasActiveNegotiationRequests(int tenderId)
        {
            var isTenderHasActiveNegotiationRequests = await _context.Negotiations
                .Where(n => n.AgencyCommunicationRequest.TenderId == tenderId && n.IsActive == true)
                .Where(n => n.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers || n.StatusId == (int)Enums.enNegotiationStatus.UnitSpecialestPendingApproved)
                .AnyAsync();

            return isTenderHasActiveNegotiationRequests;
        }
        public async Task<QuantitiesTemplateModel> GetNegotiationQuantityTableTemplateForNegotiation(int tenderid, int negoid)
        {
            var entitiess = await _context.Tenders
                .Where(t => t.IsActive == true && t.TenderId == tenderid).AsTracking()
                .Select(x => new QuantitiesTemplateModel()
                {
                    TemplateYears = x.TemplateYears,
                    TenderID = x.TenderId,
                    OfferId = negoid,
                    ActivityTemplates = x.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions.Select(t=>t.TemplateId.Value)).Distinct().ToList(),
                    TenderCreatedByTypeId = x.CreatedByTypeId,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    PreQualificationId = x.PreQualificationId,
                    InvitationTypeId = x.InvitationTypeId,
                    HasAlternativeOffer = false,
                    TenderName = x.TenderName,
                    TenderStatusId = x.TenderStatusId,
                    ReferenceNumber = x.ReferenceNumber,
                    TenderNumber = x.TenderNumber,
                    TenderTypeId = x.TenderTypeId,
                    OfferPresentationWayId = x.OfferPresentationWayId,

                })
                .FirstOrDefaultAsync();
            return entitiess;
        }

        #endregion Negotiation second stage
    }
}