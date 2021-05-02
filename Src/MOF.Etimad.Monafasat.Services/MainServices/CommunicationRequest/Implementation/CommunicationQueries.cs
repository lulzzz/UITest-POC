using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Negotiations;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class CommunicationQueries : ICommunicationQueries
    {
        private readonly IAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RootConfigurations _rootConfiguration;

        public CommunicationQueries(IAppDbContext context, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _rootConfiguration = rootConfiguration.Value;
        }

        #region Tender Communication Request
        public async Task<QueryResult<CommunicationRequestGrid>> GetSuppliersCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria)
        {
            var userRole = _httpContextAccessor.HttpContext.User.UserRole();
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            var result = await _context.AgencyCommunicationRequests.Include(d => d.SupplierExtendOfferDatesRequest)
                 .Where(t => t.TenderId == criteria.TenderId && t.IsActive == true)
                 .Where(t => t.RequestedByRoleName == RoleNames.supplier || t.RequestedByRoleName == RoleNames.OffersCheckSecretary || t.RequestedByRoleName == RoleNames.OffersPurchaseManager || t.RequestedByRoleName == RoleNames.OffersPurchaseSecretary)
                 .Where(a => a.AgencyRequestTypeId != (int)Enums.AgencyCommunicationRequestType.Negotiation && a.AgencyRequestTypeId != (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy)
                                  .WhereIf(_httpContextAccessor.HttpContext.User.UserRole() == RoleNames.Auditer, a => a.AgencyRequestTypeId != (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates)
                 .WhereIf(!string.IsNullOrEmpty(criteria.CR), r => r.SupplierExtendOfferDatesRequest.CR.Equals(criteria.CR) || r.Enquiries.Any() || r.PlaintRequests.Select(d => d.Offer).Any(d => d.CommericalRegisterNo == criteria.CR))
                                  .SortBy(criteria.Sort, criteria.SortDirection)
                 .Select(x => new CommunicationRequestGrid
                 {
                     AgencyRequestId = x.AgencyRequestId,
                     AgencyRequestIdString = Util.Encrypt(x.AgencyRequestId),
                     AgencyRequestTypeId = x.AgencyRequestTypeId,
                     canApproveExtendOfferPresentation =
                     (userRole == RoleNames.PreQualificationCommitteeSecretary && x.Tender.OffersPreQualificationCommittee.CommitteeUsers.Any(c => c.UserProfileId == userId) && (x.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification))
                     ||
                     (userRole == RoleNames.DataEntry && (x.Tender.TenderTypeId != (int)Enums.TenderType.PreQualification && x.Tender.TenderTypeId != (int)Enums.TenderType.PostQualification)),
                     AgencyRequestType = ((x.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification) && x.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.SupplierOfferExtendDates) ? "طلب تأجيل رفع وثائق التاهيل" : x.AgencyRequestType.Name,
                     CreatedAt = x.CreatedAt,
                     StatusId = x.StatusId,
                     Status = x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected ? "تم الرد (بالرفض) " : (x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.TenderExtendedDates && (x.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || x.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification)) ? "تم تمديد توايخ تقديم وثائق التأهيل" : x.Status.Name,
                     ExtevdOfferSupplierStatusId = x.ExtendOffersValidity.ExtendOffersValiditySuppliers.Where(r => r.SupplierCR == criteria.CR).FirstOrDefault().SupplierExtendOfferValidityStatusId
                 }).OrderBy(t => t.CreatedAt).ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return result;
        }

        public async Task<QueryResult<CommunicationRequestGrid>> GetAgencyCommunicationRequestsGridAsync(SimpleTenderSearchCriteria criteria)
        {
            int? offerId = null;
            Offer offer = null;
            if (!string.IsNullOrEmpty(criteria.CR))
            {
                offer = await _context.Offers.FirstOrDefaultAsync(w => w.CommericalRegisterNo == criteria.CR && w.TenderId == criteria.TenderId);
                offerId = offer?.OfferId;
            }
            List<int?> underProcessingStatuses = new List<int?>() { (int)Enums.SupplierExtendOffersValidityStatus.Sent, (int)Enums.SupplierExtendOffersValidityStatus.UnderProcessing };
            List<int?> AcceptedStatuses = new List<int?>() { (int)Enums.SupplierExtendOffersValidityStatus.Accepted, (int)Enums.SupplierExtendOffersValidityStatus.AcceptedInitially };
            #region test

            var test = await _context.AgencyCommunicationRequests.Where(w => w.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy)
                     .Where(t => t.TenderId == criteria.TenderId && t.IsActive == true)
                     .WhereIf(!string.IsNullOrEmpty(criteria.CR), t => t.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(d => d.SupplierCR == criteria.CR))
                     .WhereIf(!string.IsNullOrEmpty(criteria.CR), t => t.StatusId != (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate && t.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Pending)
                     .Where(a => criteria.currentUserRole == RoleNames.supplier.ToString() || criteria.currentUserRole == RoleNames.OffersCheckManager.ToString() || criteria.currentUserRole == RoleNames.OffersCheckSecretary.ToString()
                     || criteria.currentUserRole == RoleNames.OffersPurchaseManager.ToString() || criteria.currentUserRole == RoleNames.OffersPurchaseSecretary.ToString()
                     || criteria.currentUserRole == RoleNames.PreQualificationCommitteeManager.ToString() || criteria.currentUserRole == RoleNames.PreQualificationCommitteeSecretary.ToString() || criteria.currentUserRole == RoleNames.OffersPurchaseManager.ToString())
                     .WhereIf(criteria.SelectedCommittee != 0, x => (x.Tender.OffersCheckingCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.userId))
                   || (x.Tender.DirectPurchaseCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.userId))
                   || (x.Tender.IsLowBudgetDirectPurchase.HasValue && x.Tender.IsLowBudgetDirectPurchase.Value && x.Tender.DirectPurchaseMemberId == criteria.userId)
                   || (x.Tender.OffersPreQualificationCommittee.CommitteeUsers.Any(c => c.UserProfileId == criteria.userId)))
                     .Select(x => new CommunicationRequestGrid
                     {
                         TenderIdString = Util.Encrypt(criteria.TenderId),
                         NegotiationRequests = x.Negotiations.Select(d => new NegotiationRequest { enNegotiationType = (Enums.enNegotiationType)d.NegotiationTypeId, Id = Util.Encrypt(d.NegotiationId), RequestId = Util.Encrypt(x.AgencyRequestId) }).ToList(),
                         AgencyRequestId = x.AgencyRequestId,
                         AgencyRequestIdString = Util.Encrypt(x.AgencyRequestId),
                         AgencyRequestTypeId = x.AgencyRequestTypeId,
                         AgencyRequestType = x.AgencyRequestType.Name,
                         ExtevdOfferSupplierStatusId = x.ExtendOffersValidity.ExtendOffersValiditySuppliers.FirstOrDefault(r => r.SupplierCR == criteria.CR).SupplierExtendOfferValidityStatusId,
                         StatusId = x.StatusId,
                         Status = x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Finished ? "منتهية" : string.IsNullOrEmpty(criteria.CR)
                         ? (x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected ? "تم رفض إعتماد الطلب"
                         : (x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate ? "تحت التحديث "
                         : (x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved ? "تم إرسال الطلب" : "بإنتظار اعتماد الطلب  ")))
                         : x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved
                         && underProcessingStatuses.Contains(x.ExtendOffersValidity.ExtendOffersValiditySuppliers.Where(r => r.SupplierCR == criteria.CR).FirstOrDefault().SupplierExtendOfferValidityStatusId)
                         ? "تحت المراجعة" :
                         x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved && AcceptedStatuses.Contains(x.ExtendOffersValidity.ExtendOffersValiditySuppliers
                         .FirstOrDefault(r => r.SupplierCR == criteria.CR).SupplierExtendOfferValidityStatusId) ? "تم الرد بالموافقة"
                         : x.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved && x.ExtendOffersValidity.ExtendOffersValiditySuppliers
                         .FirstOrDefault(r => r.SupplierCR == criteria.CR).SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected ? "تم الرد بالرفض" : x.Status.Name
                      ,
                     }).ToListAsync();

            var q1 = await _context.NegotiationFirstStages.Include(d => d.NegotiationFirstStageSuppliers).Where(w => w.AgencyCommunicationRequest.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation
        )
             .Where(t => t.AgencyCommunicationRequest.TenderId == criteria.TenderId
             && t.IsActive == true
             && (
             t.AgencyCommunicationRequest.RequestedByRoleName == RoleNames.OffersCheckSecretary ||
             t.AgencyCommunicationRequest.RequestedByRoleName == RoleNames.OffersPurchaseSecretary ||
             t.AgencyCommunicationRequest.RequestedByRoleName == RoleNames.OffersPurchaseManager ||
             t.AgencyCommunicationRequest.RequestedByRoleName == RoleNames.OffersCheckManager ||
             t.AgencyCommunicationRequest.RequestedByRoleName == RoleNames.supplier))
             .Where(f => criteria.currentUserRole == RoleNames.supplier || criteria.currentUserRole == RoleNames.OffersCheckSecretary || criteria.currentUserRole == RoleNames.OffersCheckManager
             || ((criteria.currentUserRole == RoleNames.OffersPurchaseSecretary || criteria.currentUserRole == RoleNames.OffersPurchaseManager)
             && !(f.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && f.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value))
             || (f.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && f.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value && f.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == criteria.userId))

             .WhereIf(criteria.currentUserRole == RoleNames.OffersCheckManager || criteria.currentUserRole == RoleNames.OffersPurchaseManager,
             d => (d.StatusId != (int)Enums.enNegotiationStatus.UnderUpdate && d.StatusId != (int)Enums.enNegotiationStatus.New)
             || (d.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && d.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value && d.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == criteria.userId)
             )
             .WhereIf(criteria.currentUserRole == RoleNames.supplier && !string.IsNullOrEmpty(criteria.CR),
             t => (t.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers
             || t.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed
             || t.StatusId == (int)Enums.enNegotiationStatus.SupplierNotAgreed)
             && t.NegotiationFirstStageSuppliers.Any(d => (d.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.PendeingSupplierReply || d.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.Agree || d.NegotiationSupplierStatusId == (int)enNegotiationSupplierStatus.DisAgree) && d.SupplierCR == criteria.CR)
             )
             .Select(x => new CommunicationRequestGrid
             {
                 TenderIdString = Util.Encrypt(criteria.TenderId),
                 AgencyRequestId = x.NegotiationId,
                 AgencyRequestIdString = Util.Encrypt(x.NegotiationId),
                 AgencyRequestTypeId = x.NegotiationTypeId,
                 AgencyRequestType = x.NegotiationType.Name,
                 StatusId = x.StatusId,
                 Status = x.NegotiationStatus.Name,
                 IsNewNegotiation = x.IsNewNegotiation,
             }).ToListAsync();

            var q2 = await _context.NegotiationSecondStages.Include(d => d.Offer).Where(w => w.AgencyCommunicationRequest.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation)

                .Where(t => t.AgencyCommunicationRequest.TenderId == criteria.TenderId && t.IsActive == true)
                .WhereIf(criteria.currentUserRole == RoleNames.supplier && !string.IsNullOrEmpty(criteria.CR), r => r.Offer.CommericalRegisterNo == criteria.CR &&
                        (r.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers || r.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || r.StatusId == (int)Enums.enNegotiationStatus.SupplierNotAgreed))
                .WhereIf(criteria.currentUserRole == RoleNames.OffersCheckManager || criteria.currentUserRole == RoleNames.OffersPurchaseManager,
                r => ((int)enNegotiationStatus.UnderUpdate != r.StatusId && (int)enNegotiationStatus.New != r.StatusId)
                || (r.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && r.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.Value && r.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == criteria.userId))
                .WhereIf(criteria.currentUserRole == RoleNames.UnitSecretaryUser || criteria.currentUserRole == RoleNames.UnitSpecialistLevel1 || criteria.currentUserRole == RoleNames.UnitSpecialistLevel2, r => (int)enNegotiationStatus.New != r.StatusId)
                .SortBy(criteria.Sort, criteria.SortDirection)
                .Select(x => new CommunicationRequestGrid
                {
                    TenderIdString = Util.Encrypt(criteria.TenderId),
                    AgencyRequestId = x.NegotiationId,
                    AgencyRequestIdString = Util.Encrypt(x.NegotiationId),
                    AgencyRequestTypeId = x.NegotiationTypeId,
                    AgencyRequestType = x.NegotiationType.Name,
                    StatusId = x.StatusId,
                    Status = x.NegotiationStatus.Name,
                }).ToListAsync();
            #endregion
            var ee = test.Concat(q1);
            var final = ee.Concat(q2);
            var datas = await final.ToQueryResult(criteria.PageNumber, criteria.PageSize);
            return datas;
        }

        #endregion

        #region Extend Offers Validity
        public async Task<ExtendOffersValidityModel> GetExtendOffersValidity(int agencyRequestId, int userId)
        {

            var endTime = _rootConfiguration.OfferTimesConfiguration.EndOfferTime - 12;
            var endTimeForVactionDays = _rootConfiguration.OfferTimesConfiguration.EndOfferVacationTime - 12;
            var result = await _context.AgencyCommunicationRequests
            .Where(x => x.AgencyRequestId == agencyRequestId)
            .Where(x => x.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy)
            .Where(x => x.IsActive == true)
            .Select(t => new ExtendOffersValidityModel
            {
                IsTenderHasExecludedReasonOrAwardingValue = t.Tender.Offers.Any(o => o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true && (!string.IsNullOrEmpty(o.ExclusionReason) || o.PartialOfferAwardingValue != null || o.TotalOfferAwardingValue != null)),
                AgencyRequestId = t.AgencyRequestId,
                ExtendOfferValidityId = t.ExtendOffersValidity.ExtendOffersValidityId,
                AgencyRequestIdString = Util.Encrypt(t.AgencyRequestId),
                TenderId = t.TenderId,
                TenderTypeId = t.Tender.TenderTypeId,
                RejectionReason = t.RejectionReason,
                StatusId = t.Tender.TenderStatusId,
                TenderIdString = Util.Encrypt(t.TenderId),
                AgencyId = t.Tender.AgencyCode,
                AgencyRequestStatusId = t.StatusId,
                IsLowBudgetDirectPurchase = t.Tender.IsLowBudgetDirectPurchase,
                DirectPurchaseMemberId = t.Tender.DirectPurchaseMemberId,
                ExtendOffersValiditySavingModel = new ExtendOffersValiditySavingModel
                {
                    TenderRequestsCount = t.Tender.AgencyCommunicationRequests.Count,
                    AgencyRequestId = t.AgencyRequestId,
                    AgencyRequestIdString = Util.Encrypt(t.AgencyRequestId),
                    ExtendOffersReason = t.ExtendOffersValidity.ExtendOffersReason,
                    ExtendOffersValidityId = t.ExtendOffersValidity.ExtendOffersValidityId,
                    NewOffersExpiryDate = t.ExtendOffersValidity.NewOffersExpiryDate,
                    NewOffersExpiryDateString = t.ExtendOffersValidity.NewOffersExpiryDate.ToGregorianDate("dd/MM/yyyy") ?? "",
                    NewOffersExpiryDateHijriString = t.ExtendOffersValidity.NewOffersExpiryDate.ToHijriDateWithFormat("dd/MM/yyyy") ?? "",
                    OffersDuration = t.ExtendOffersValidity.OffersDuration,
                    ReplyReceivingDurationDays = t.ExtendOffersValidity.ReplyReceivingDurationDays,
                    AgencyRequestStatusId = t.StatusId,
                    TenderIdString = Util.Encrypt(t.TenderId),
                    IsLowBudgetDirectPurchase = t.Tender.IsLowBudgetDirectPurchase,
                    DirectPurchaseMemberId = t.Tender.DirectPurchaseMemberId,
                    ReplyReceivingDurationTime = (t.ExtendOffersValidity.NewOffersExpiryDate.DayOfWeek.ToString() == "Saturday" || t.ExtendOffersValidity.NewOffersExpiryDate.DayOfWeek.ToString() == "Friday") ? endTimeForVactionDays + ":00 PM" : endTime + ":00 PM",
                },
                TenderBasicInfoModel = new TenderBasicInfoModel
                {
                    TenderStatusId = t.Tender.TenderStatusId,
                    TenderRefrenceNumber = t.Tender.ReferenceNumber,
                    TenderName = t.Tender.TenderName,
                    TenderNumber = t.Tender.TenderNumber,
                    EstimatedValue = t.Tender.EstimatedValue,
                    EstimatedValueText = new ConvertNumberToText(t.Tender.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic(),
                    OffersNumber = t.ExtendOffersValidity.ExtendOffersValiditySuppliers.Count(a => a.IsActive == true),
                    OfferPresentationWay = t.Tender.OfferPresentationWayId,
                },
            }).FirstOrDefaultAsync();
            return result;
        }
        public async Task<TenderBasicInfoModel> GetTenderBasicInfoModel(int tenderId)
        {
            return await _context.Tenders
                .Where(x => x.TenderId == tenderId)
                .Select(t => new TenderBasicInfoModel
                {
                    TenderStatusId = t.TenderStatusId,
                    TenderRefrenceNumber = t.ReferenceNumber,
                    TenderName = t.TenderName,
                    TenderNumber = t.TenderNumber,
                    EstimatedValue = t.EstimatedValue,
                    EstimatedValueText = new ConvertNumberToText(t.EstimatedValue.Value, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic(),
                    OffersNumber = t.Offers.Count(a => a.IsActive == true),
                    OfferPresentationWay = t.OfferPresentationWayId,
                }).FirstOrDefaultAsync();
        }

        public async Task<ExtendOffersValidity> GetExtendOffersValidityForAddInitialGuaranteeById(int extendOfferValidityId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(extendOfferValidityId), extendOfferValidityId.ToString());
            return await _context.ExtendOffersValiditys.Include(a => a.AgencyCommunicationRequest)
                .Include(e => e.ExtendOffersValiditySuppliers)
                .Where(e => e.IsActive == true && e.ExtendOffersValidityId == extendOfferValidityId).FirstOrDefaultAsync();
        }
        public async Task<ExtendOffersValiditySupplier> GetExtendOffersValiditySupplierById(int extendOfferValidityId, string supplierCr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(extendOfferValidityId), extendOfferValidityId.ToString());
            return await _context.ExtendOffersValiditySuppliers.Include(e => e.ExtendOffersValidity).ThenInclude(e => e.AgencyCommunicationRequest)
                .Where(e => e.IsActive == true && e.ExtendOffersValidityId == extendOfferValidityId && e.SupplierCR == supplierCr).FirstOrDefaultAsync();
        }

        private async Task<List<string>> GetSuppliersFailedInPostqualification(int tenderId)
        {
            var result = await _context.Tenders
                .Where(t => t.PostQualificationTenderId == tenderId && t.TenderStatusId == (int)Enums.TenderStatus.DocumentCheckConfirmed)
                .Where(t => t.QualificationFinalResults.Any(q => q.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed))
                .SelectMany(q => q.QualificationFinalResults)
                .Where(s => s.QualificationLookupId == (int)Enums.QualificationResultLookup.Failed)
                .Select(s => s.SupplierSelectedCr).ToListAsync();
            return result;
        }

        public async Task<QueryResult<ExtendOffersGridModel>> GetOffersForExtendOfferValidityCreation(int tenderId)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            var failedSuppliers = await GetSuppliersFailedInPostqualification(tenderId);
            int _versionid = (int)Enums.QuantityTableVersion.Version2;
            var offers = await _context.Offers
                .Include(o => o.Supplier)
                .Include(o => o.negotiations)
                .ThenInclude(o => o.NegotiationStatus)
                .Include(o => o.Combined)
             .Where(o => o.TenderId == tenderId && o.IsActive == true)
             .WhereIf(failedSuppliers.Any(), o => !failedSuppliers.Contains(o.CommericalRegisterNo)).ToQueryResult(1, 1000);

            var tender = await _context.Tenders.Where(t => t.TenderId == tenderId)
               .Include(o => o.Invitations)
               .ThenInclude(o => o.InvitationStatus)
               .Include(o => o.ConditionsBooklets)
               .ThenInclude(o => o.BillInfo)

               .FirstOrDefaultAsync();

            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();
            tender.SetTenderHistories(tenderHistories);
            offers.Items.ToList().ForEach(o => o.Tender = tender);
            var result = await offers.Items.Where(o => o.Tender.IsNewAwarding(newAwardingReleaseDate)
              ? (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
             && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
             :
             (o.OfferStatusId == (int)Enums.OfferStatus.Received && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer))
                .OrderBy(x => x.Tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2 ? x.OfferWeightAfterCalcNPA : x.FinalPriceAfterDiscount)

             .Select(offer => new ExtendOffersGridModel
             {
                 isTawreed = offer.Tender.QuantityTableVersionId == _versionid,
                 CommericalRegisterName = offer.Supplier.SelectedCrName,
                 CommericalRegisterNo = offer.CommericalRegisterNo,
                 InvitationTypeName = offer.Tender.Invitations.Count > 0 && offer.Tender.ConditionsBooklets.Count == 0 ?
                                      offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr :
                                      offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                 OfferIdString = Util.Encrypt(offer.OfferId),
                 status = offer.negotiations.FirstOrDefault(r => r.OfferId == offer.OfferId && (r.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers || r.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || r.StatusId == (int)Enums.enNegotiationStatus.SupplierNotAgreed)) != null ? offer.negotiations.FirstOrDefault(r => r.OfferId == offer.OfferId && (r.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers || r.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || r.StatusId == (int)Enums.enNegotiationStatus.SupplierNotAgreed)).NegotiationStatus.Name : "لم يتم الارسال ",
                 InvitationPurchaseStatus = offer.Tender.Invitations.Count > 0 && offer.Tender.ConditionsBooklets.Count == 0 ?
                           offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr :
                           offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                 OfferId = offer.OfferId,

                 OfferPrice = offer.FinalPriceAfterDiscount,
                 OfferPriceNP = offer.OfferWeightAfterCalcNPA,
                 CombinedId = offer.Combined.Count() == 1 ? offer.Combined.Where(a => a.OfferId == offer.OfferId).Select(x => x.Id).FirstOrDefault() : 0,
                 CombinedCount = offer.Combined.Count(),
                 CombinedIdString = offer.Combined.Count() == 1 ? Util.Encrypt(offer.Combined.Where(a => a.OfferId == offer.OfferId).Select(x => x.Id).FirstOrDefault()) : "",
                 OfferAcceptanceStatus = "",
                 OfferTechnicalEvaluationStatus = offer.OfferTechnicalEvaluationStatusId.Value == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                 TenderTypeId = offer.Tender.TenderTypeId,
                 OfferPresentationWay = offer.Tender.OfferPresentationWayId,
                 GuaranteeAttachment = new CommunicationAttachmentModel()
             }).ToQueryResult(1, 1000);
            return result;

        }


        public async Task<QueryResult<ExtendOffersGridModel>> GetTenderOffersPagingAsync(int tenderId, int extendOfferValidityId)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();
            int _versionid = (int)Enums.QuantityTableVersion.Version2;
            var offers = await _context.Offers
                .Include(o => o.Supplier)
                .Include(o => o.negotiations)
                .ThenInclude(o => o.NegotiationStatus)
                .Include(o => o.Combined)
             .Where(t => t.TenderId == tenderId && t.IsActive == true)
             .Where(o => o.ExtendOffersValiditySupplier.ExtendOffersValidityId == extendOfferValidityId && o.ExtendOffersValiditySupplier.IsActive == true)
              .Where(t => t.OfferStatusId == (int)Enums.OfferStatus.Excluded || t.OfferStatusId == (int)Enums.OfferStatus.Received).ToQueryResult(1, 1000);
            var tender = await _context.Tenders.Where(t => t.TenderId == tenderId)
                .Include(o => o.Invitations)
                .ThenInclude(o => o.InvitationStatus)
                .Include(o => o.ConditionsBooklets)
                .ThenInclude(o => o.BillInfo)
                .FirstOrDefaultAsync();

            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();
            tender.SetTenderHistories(tenderHistories);
            offers.Items.ToList().ForEach(o => o.Tender = tender);
            var extendOffersValiditySupplier = await _context.ExtendOffersValiditySuppliers.Where(e => offers.Items.Select(o => o.OfferId).Contains(e.OfferId) && e.ExtendOffersValidityId == extendOfferValidityId)
                        .Include(o => o.ExtendOffersValidity)
                        .ThenInclude(o => o.AgencyCommunicationRequest)
                        .Include(o => o.SupplierExtendOffersValidityStatus)
                        .Include(o => o.ExtendOffersValidityAttachment).ToListAsync();

            offers.Items.ToList().ForEach(o => o.ExtendOffersValiditySupplier = extendOffersValiditySupplier.OrderByDescending(e => e.ExtendOffersValidityId).FirstOrDefault(e => e.OfferId == o.OfferId));
            var result = await offers.Items
                  .Where(o => o.Tender.IsNewAwarding(newAwardingReleaseDate)
                 ? (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer
                && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                :
                (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer))
                .OrderBy(x => x.Tender.QuantityTableVersionId == (int)Enums.QuantityTableVersion.Version2 ? x.OfferWeightAfterCalcNPA : x.FinalPriceAfterDiscount)
             .Select(offer => new ExtendOffersGridModel
             {
                 isTawreed = offer.Tender.QuantityTableVersionId == _versionid,
                 CommericalRegisterName = offer.Supplier.SelectedCrName,
                 CommericalRegisterNo = offer.CommericalRegisterNo,
                 InvitationTypeName = offer.Tender.Invitations.Count > 0 && offer.Tender.ConditionsBooklets.Count == 0 ?
                                      offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr :
                                      offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                 OfferIdString = Util.Encrypt(offer.OfferId),
                 status = offer.negotiations.FirstOrDefault(r => r.OfferId == offer.OfferId && (r.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers || r.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || r.StatusId == (int)Enums.enNegotiationStatus.SupplierNotAgreed)) != null ? offer.negotiations.FirstOrDefault(r => r.OfferId == offer.OfferId && (r.StatusId == (int)Enums.enNegotiationStatus.SentToSuppliers || r.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || r.StatusId == (int)Enums.enNegotiationStatus.SupplierNotAgreed)).NegotiationStatus.Name : "لم يتم الارسال ",
                 InvitationPurchaseStatus = offer.Tender.Invitations.Count > 0 && offer.Tender.ConditionsBooklets.Count == 0 ?
                           offer.Tender.Invitations.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo).InvitationStatus.NameAr :
                           offer.Tender.ConditionsBooklets.FirstOrDefault(x => x.IsActive == true && x.CommericalRegisterNo == offer.CommericalRegisterNo && x.BillInfo != null).BillInfo.BillStatusId == (int)Enums.BillStatus.Paid ? Resources.TenderResources.DisplayInputs.Purchased : Resources.TenderResources.DisplayInputs.NotPurchased,
                 OfferId = offer.OfferId,

                 OfferPrice = offer.FinalPriceAfterDiscount,
                 OfferPriceNP = offer.OfferWeightAfterCalcNPA,
                 CombinedId = offer.Combined.Count() == 1 ? offer.Combined.Where(a => a.OfferId == offer.OfferId).Select(x => x.Id).FirstOrDefault() : 0,
                 CombinedCount = offer.Combined.Count(),
                 CombinedIdString = offer.Combined.Count() == 1 ? Util.Encrypt(offer.Combined.Where(a => a.OfferId == offer.OfferId).Select(x => x.Id).FirstOrDefault()) : "",
                 OfferAcceptanceStatus = offer.ExtendOffersValiditySupplier == null ? "" : (offer.ExtendOffersValiditySupplier.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Sent && offer.ExtendOffersValiditySupplier.ExtendOffersValidity.AgencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate ? "" : offer.ExtendOffersValiditySupplier.SupplierExtendOffersValidityStatus.Name),
                 OfferTechnicalEvaluationStatus = offer.OfferTechnicalEvaluationStatusId.Value == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                 TenderTypeId = offer.Tender.TenderTypeId,
                 OfferPresentationWay = offer.Tender.OfferPresentationWayId,
                 GuaranteeAttachment = offer.ExtendOffersValiditySupplier == null ?
                new CommunicationAttachmentModel() :
                offer.ExtendOffersValiditySupplier.ExtendOffersValidityAttachment == null ? new CommunicationAttachmentModel() :
                new CommunicationAttachmentModel
                {
                    AttachmentTypeId = offer.ExtendOffersValiditySupplier.ExtendOffersValidityAttachment.AttachmentTypeId,
                    Name = offer.ExtendOffersValiditySupplier.ExtendOffersValidityAttachment.Name,
                    FileNetReferenceId = offer.ExtendOffersValiditySupplier.ExtendOffersValidityAttachment.FileNetReferenceId
                }
             }).ToQueryResult(1, 1000);
            return result;

        }

        public async Task<OfferNegotiationFileModel> GetOfferFilesbyId(int offerid)
        {
            var Query = await _context.Offers
                     .Where(x => x.OfferId == offerid)

                     .Select(d => new OfferNegotiationFileModel
                     {
                         CombinedSuppliers = d.Combined.Where(f => f.StatusId == (f.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.Existed ? f.StatusId : (int)Enums.SupplierSolidarityStatus.Approved)).Select(w =>
                                        new SupplierCombinedModel
                                        {
                                            CommericalName =

                                             w.Supplier.SelectedCrName
                                         ,
                                            CommericalRegisterNo = w.CRNumber,
                                            SolidarityTypeName = DefineSolidarityTypeName(w)


                                        }).ToList(),
                         offerIdString = Util.Encrypt(offerid),
                         tenderTypeId = d.Tender.TenderTypeId,
                         offerAttachments = d.Attachment.Select(s => new OfferAttachmentModel
                         {
                             FileName = s.FileName,
                             FileNetId = s.FileNetReferenceId,
                             FiletypeName = DefineAttachmentTypeName(s)
                         }




                         ).ToList()
                     }).FirstOrDefaultAsync();

            return Query;
        }
        private static string DefineSolidarityTypeName(OfferSolidarity s)
        {


            switch (s.SolidarityTypeId)
            {
                case (int)UnRegisteredSuppliersInvitationType.Existed:
                    return "مورد غير مسجل يملك سجل";
                case (int)UnRegisteredSuppliersInvitationType.HasCR:
                    return "مورد غير مسجل - يملك سجل فى بلد المنشأ";
                case (int)UnRegisteredSuppliersInvitationType.HasLicience:
                    return "مورد غير مسجل - لدية رخصة مزاولة مهنة";
                case (int)UnRegisteredSuppliersInvitationType.Foriegn:
                    return "مورد غير مسجل - أجنبي";
                case (int)UnRegisteredSuppliersInvitationType.SolidarityLeader:
                    return "قائد التضامن";


                default:
                    return "متضامن";
            }

        }


        private static string DefineAttachmentTypeName(SupplierAttachment s)
        {

            if (s is SupplierOriginalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.OfferAttachment;
            }
            else if (s is SupplierTechnicalProposalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.TechnicalFile;

            }
            else if (s is SupplierFinancialProposalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.FinancialOffer;
            }
            else if (s is SupplierFinancialandTechnicalProposalAttachment)
            {
                return Resources.OfferResources.DisplayInputs.TechnicalFinancialFiles;

            }
            else if (s is SupplierCombinedAttachment)
            {
                return Resources.OfferResources.DisplayInputs.CombinationLetter;

            }
            else
            {
                return "";
            }
        }


        public async Task<ExtendOffersDisplayFilesModel> GetOfferToExtendbyId(int offerid, int userId)
        {
            var Query = await _context.Offers
                        .Where(x => x.OfferId == offerid)
                                                                                                                        .Select(off => new ExtendOffersDisplayFilesModel
                                                                                                                        {
                                                                                                                            offerAttachmentModels = off.Attachment.Select(s => new OfferAttachmentModel
                                                                                                                            {
                                                                                                                                FileName = s.FileName,
                                                                                                                                FileNetId = s.FileNetReferenceId,
                                                                                                                                OfferPresentationWayId = off.Tender.OfferPresentationWayId,
                                                                                                                                FiletypeName = DefineAttachmentTypeName(s)
                                                                                                                            }).ToList(),
                                                                                                                            OfferIdString = Util.Encrypt(off.OfferId),
                                                                                                                        }).FirstOrDefaultAsync();
            return Query;
        }
        public async Task<OfferDetailModel> GetOfferDetails(int combinedSupplierId)
        {
            var res = await _context.SupplierCombinedDetails
                .Where(d => d.CombinedId == combinedSupplierId)
                .Select(SCD => new OfferDetailModel
                {
                    SupplierName = ((OfferSolidarity)SCD.Combined).Supplier.SelectedCrName,
                    CombinedDetailId = SCD.CombinedDetailId,
                    CombinedId = SCD.CombinedId,
                    TenderIDString = Util.Encrypt(SCD.Combined.Offer.Tender.TenderId),
                    TenderStatusId = SCD.Combined.Offer.Tender.TenderStatusId,
                    CombinationLetter = SCD.Combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault(),
                    OfferIdString = Util.Encrypt(SCD.Combined.Offer.OfferId),
                    OfferId = SCD.Combined.Offer.OfferId,
                    CombinDetailsIDString = Util.Encrypt(SCD.CombinedDetailId),
                    CombinedOwner = SCD.Combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                    IsChamberJoiningAttached = SCD.IsChamberJoiningAttached,
                    IsChamberJoiningValid = SCD.IsChamberJoiningValid,
                    IsCommercialRegisterAttached = SCD.IsCommercialRegisterAttached,
                    IsCommercialRegisterValid = SCD.IsCommercialRegisterValid,
                    IsOfferCopyAttached = SCD.Combined.Offer.IsOfferCopyAttached,
                    IsOfferLetterAttached = SCD.Combined.Offer.IsOfferLetterAttached,
                    OfferLetterNumber = SCD.Combined.Offer.OfferLetterNumber,
                    OfferLetterDate = SCD.Combined.Offer.OfferLetterDate,
                    OfferLetterDateDisplay = (SCD.Combined.Offer.OfferLetterDate.HasValue) ? (SCD.Combined.Offer.OfferLetterDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                    IsPurchaseBillAttached = SCD.Combined.Offer.IsPurchaseBillAttached,
                    IsSaudizationAttached = SCD.IsSaudizationAttached,
                    IsSaudizationValidDate = SCD.IsSaudizationValidDate,
                    IsSocialInsuranceAttached = SCD.IsSocialInsuranceAttached,
                    IsSocialInsuranceValidDate = SCD.IsSocialInsuranceValidDate,
                    IsVisitationAttached = SCD.Combined.Offer.IsVisitationAttached,
                    IsZakatAttached = SCD.IsZakatAttached,
                    IsZakatValidDate = SCD.IsZakatValidDate,
                    IsOpened = SCD.Combined.Offer.IsOpened,
                    IsBankGuaranteeAttached = SCD.Combined.Offer.IsBankGuaranteeAttached.Value,
                    IsSpecificationAttached = SCD.IsSpecificationAttached.Value,
                    IsSpecificationValidDate = SCD.IsSpecificationValidDate.Value,
                    OfferPresentationWayId = SCD.Combined.Offer.Tender.OfferPresentationWayId.Value,
                    BankGuaranteeFiles = SCD.Combined.Offer.BankGuaranteeDetails
                                .Select(x => new SupplierBankGuaranteeModel
                                {
                                    IsBankGuaranteeValid = x.IsBankGuaranteeValid,
                                    IsBankGuaranteeValidString = x.IsBankGuaranteeValid.HasValue ? (x.IsBankGuaranteeValid.Value ? Resources.OfferResources.DisplayInputs.Valid : Resources.OfferResources.DisplayInputs.Invalid) : "",
                                    GuaranteeNumber = x.GuaranteeNumber,
                                    BankId = x.BankId,
                                    BankName = x.Bank.NameAr,
                                    Amount = x.Amount,
                                    GuaranteeStartDate = x.GuaranteeStartDate.Value.Date,
                                    GuaranteeEndDate = x.GuaranteeEndDate.Value.Date,
                                    GuaranteeStartDateDisplay = (x.GuaranteeStartDate.HasValue) ? (x.GuaranteeStartDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeEndDateDisplay = (x.GuaranteeEndDate.HasValue) ? (x.GuaranteeEndDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture)) : "",
                                    GuaranteeDays = x.GuaranteeDays
                                }).ToList()
                                ,
                    SpecificationsFiles = SCD.SpecificationDetails.Select(x => new SupplierSpecificationModel()
                    {
                        IsForApplier = x.IsForApplier,
                        IsForApplierString = x.IsForApplier.HasValue ? (x.IsForApplier.Value ? Resources.OfferResources.DisplayInputs.OfferApplier : Resources.OfferResources.DisplayInputs.Solidarity) : "",
                        MaintenanceRunningWorkId = x.MaintenanceRunningWorkId,
                        MaintenanceRunningWorkString = x.MaintenanceRunningWork == null ? "" : x.MaintenanceRunningWork.NameAr,
                        ConstructionWorkId = x.ConstructionWorkId,
                        ConstructionWorkString = x.ConstructionWork == null ? "" : x.ConstructionWork.NameAr,
                        Degree = x.Degree,
                    }).ToList()
                }).FirstOrDefaultAsync();

            return res;
        }
        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(int offerid, int userId)
        {
            var Query = await _context.OfferSolidarities
                               .Where(x => x.OfferId == offerid)
                               .Where(x => x.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader)
                                                                                                                                                           .Select(comb => new CombinedSupplierModel
                                                                                                                                                           {
                                                                                                                                                               CombinedIdString = Util.Encrypt(comb.Id),
                                                                                                                                                               CombinedId = comb.Id,
                                                                                                                                                               IsOwner = comb.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                                                                                                                                                               roleName = DefineSolidarityTypeName(comb),
                                                                                                                                                               SupplierName = (comb.Supplier.SelectedCrName),
                                                                                                                                                               SupplierCr = (comb.CRNumber),
                                                                                                                                                               OfferId = offerid,
                                                                                                                                                           }).ToQueryResult();
            return Query;
        }
        public async Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerid)
        {
            var result = await _context.Offers.Include(d => d.Combined)
                           .Where(x => x.OfferId == offerid)
                           .SelectMany(d => d.Combined)
                           .Select(y => new CombinedSupplierModel
                           {
                               IsOwner = y.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader,
                               roleName = y.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader ? Resources.OfferResources.DisplayInputs.SolidarityLeader : Resources.OfferResources.DisplayInputs.Solidarity,
                               OfferIdString = Util.Encrypt(y.OfferId),
                               TenderIdString = Util.Encrypt(y.Offer.TenderId),

                           }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestById(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(t => t.Tender)
                .Include(e => e.ExtendOffersValidity)
                 .Where(p => p.AgencyRequestId == requestId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestByIdForSendToApproval(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(t => t.Tender)
                .ThenInclude(t => t.Offers)
                .Include(e => e.ExtendOffersValidity)
                 .Where(p => p.AgencyRequestId == requestId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestByIdForApproval(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest communicationRequest = await _context.AgencyCommunicationRequests
                .Include(t => t.Tender)
                .Include(e => e.ExtendOffersValidity)
                .ThenInclude(e => e.ExtendOffersValiditySuppliers)
                 .Where(p => p.AgencyRequestId == requestId && p.IsActive == true).FirstOrDefaultAsync();

            var offers = await _context.Offers.Where(o => o.TenderId == communicationRequest.TenderId && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true).ToListAsync();
            communicationRequest.Tender.SetTenderReceivedOffers(offers);
            return communicationRequest;
        }
        public async Task<AgencyCommunicationRequest> FindAgencyCommunicationRequestByIdForDelete(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(t => t.Tender)
                .Include(e => e.ExtendOffersValidity)
                .ThenInclude(e => e.ExtendOffersValiditySuppliers)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestById(int requestId)
        {
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }

        public async Task<List<Offer>> GetOffersToSendExtendOfferValidityByTenderId(int tenderId)
        {
            DateTime newAwardingReleaseDate = _rootConfiguration.NewAwarding.ReleaseDate.ToDateTime();

            var tender = await _context.Tenders.Where(t => t.TenderId == tenderId && t.IsActive == true).FirstOrDefaultAsync();
            var tenderHistories = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.OffersAwarding).ToListAsync();

            tender.SetTenderHistories(tenderHistories);

            var offers = await _context.Offers.Include(s => s.Supplier)
                .Where(o => o.TenderId == tenderId && o.OfferStatusId == (int)Enums.OfferStatus.Received && o.IsActive == true)
                .ToListAsync();

            offers.ForEach(o => o.Tender = tender);


            var result = offers.Where(o => o.Tender.IsNewAwarding(newAwardingReleaseDate)
           ? (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
          : (o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)).ToList();

            return result;
        }

        public async Task<Tender> GetTenderByAgencyRequestId(int agencyRequestId)
        {
            var result = (await _context.AgencyCommunicationRequests.Include(t => t.Tender).Where(x => x.AgencyRequestId == agencyRequestId).FirstOrDefaultAsync()).Tender;

            return result;
        }
        public async Task<SupplierExtendOffersValidityViewModel> GetSupplierExtendOffersValidityForSupplierViewModel(int agencyCommunicationRequestId, string Cr)
        {
            var result = await _context.ExtendOffersValiditySuppliers
                .Where(s => s.IsActive == true && s.ExtendOffersValidity.AgencyCommunicationRequestId == agencyCommunicationRequestId && s.SupplierCR == Cr)
                .Select(ss => new SupplierExtendOffersValidityViewModel
                {
                    AgencyRequestStatusId = ss.ExtendOffersValidity.AgencyCommunicationRequest.StatusId,
                    SupplierExtendOffersValidityId = Util.Encrypt(ss.Id),
                    ExtendOffersValidityIdString = Util.Encrypt(ss.ExtendOffersValidityId),
                    TenderIdString = Util.Encrypt(ss.ExtendOffersValidity.AgencyCommunicationRequest.TenderId),
                    AgencyName = ss.ExtendOffersValidity.AgencyCommunicationRequest.Tender.Agency.NameArabic,
                    ExtendOffersReason = ss.ExtendOffersValidity.ExtendOffersReason,
                    NewOffersExpiryDate = ss.ExtendOffersValidity.NewOffersExpiryDate,
                    OffersValidityDays = ss.ExtendOffersValidity.OffersDuration,
                    TenderName = ss.ExtendOffersValidity.AgencyCommunicationRequest.Tender.TenderName,
                    TenderPurpose = ss.ExtendOffersValidity.AgencyCommunicationRequest.Tender.Purpose,
                    TenderReferenceNumber = ss.ExtendOffersValidity.AgencyCommunicationRequest.Tender.ReferenceNumber,
                    TenderTypeName = ss.ExtendOffersValidity.AgencyCommunicationRequest.Tender.TenderType.NameAr,
                    SupplierExtendOffersValidityStatusId = ss.SupplierExtendOfferValidityStatusId != null ? (int)ss.SupplierExtendOfferValidityStatusId : 0,
                    IsFileUploaded = _context.ExtendOffersValidityAttachments.Where(r => r.ExtendOffersValiditySupplierId == ss.Id).FirstOrDefault() != null ? true : false
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ExtendOffersValidityAttachementViewModel> GetExtendOffersValidityAttachmentModel(int extendOffersValiditySupplierId)
        {
            var result = await _context.ExtendOffersValidityAttachments
                .Where(s => s.IsActive == true && s.ExtendOffersValiditySupplierId == extendOffersValiditySupplierId)
                .Select(a => new ExtendOffersValidityAttachementViewModel
                {
                    FileNetReferenceId = a.FileNetReferenceId,
                    Name = a.Name
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<ExtendOffersValiditySupplier> GetSupplierExtendOffersValidityForSupplier(int agencyCommunicationRequestId, string Cr)
        {
            var result = await _context.ExtendOffersValiditySuppliers
                .Where(e => e.IsActive == true && e.ExtendOffersValidity.AgencyCommunicationRequestId == agencyCommunicationRequestId && e.SupplierCR == Cr)
                .FirstOrDefaultAsync();

            return result;
        }
        public async Task<ExtendOffersValiditySupplier> GetSupplierExtendOffersValidityForSupplierByExtendOffersValiditySupplierId(int extendOffersValiditySupplierId, string Cr)
        {
            return await _context.ExtendOffersValiditySuppliers
                .Where(e => e.IsActive == true && e.Id == extendOffersValiditySupplierId && e.SupplierCR == Cr)
                .FirstOrDefaultAsync();
        }
        public async Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestForApproval(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(a => a.PlaintRequests)
                .Include(a => a.EscalationAcceptanceStatus)
                .Include(a => a.PlaintAcceptanceStatus)
                .Include(a => a.Tender)
                .ThenInclude(a => a.Offers)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true).FirstOrDefaultAsync();
            return model;
        }
        public async Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestForReject(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(a => a.Tender)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true)
                .FirstOrDefaultAsync();
            return model;
        }

        public async Task<int> GetExtendOffersValidityDaysByTenderId(int tenderId)
        {
            var totalOffersDuration = await _context.ExtendOffersValiditys
                .Where(t => t.IsActive == true && t.AgencyCommunicationRequest.StatusId != (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate && t.AgencyCommunicationRequest.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Rejected && t.AgencyCommunicationRequest.TenderId == tenderId && t.AgencyCommunicationRequest.IsActive == true)
                .Select(x => x.OffersDuration).SumAsync();
            return totalOffersDuration;
        }

        public async Task<TenderCommunicationRequestModel> GetTenderHasValidExtendOffersValidity(int tenderId)
        {
            var tender = await _context.Tenders
                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                   .Select(t => new TenderCommunicationRequestModel
                   {

                       TenderHasExtendOffersValidity = t.AgencyCommunicationRequests.Any(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy && a.IsActive == true
                         &&
                        ((a.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished
                        && (!a.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(s => s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected
                           || s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Accepted))
                        && a.ExtendOffersValidity.NewOffersExpiryDate > DateTime.Now)
                         ||
                          (a.ExtendOffersValidity.NewOffersExpiryDate > DateTime.Now &&
                          !a.ExtendOffersValidity.ExtendOffersValiditySuppliers.Any(s => s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected
                              || s.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Accepted))
                      ))

                   }).FirstOrDefaultAsync();

            return tender;
        }
        #endregion

        #region Plaint

        public async Task<TenderPlaintDatailsModel> FindTenderForPlaintRequestById(int tenderId, string selectedCR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            TenderPlaintDatailsModel tender = await _context.Tenders.Include(h => h.AgencyCommunicationRequests).ThenInclude(p => p.PlaintRequests)
                .Include(o => o.Offers/*.Where(of => of.CommericalRegisterNo == selectedCR)*/).ThenInclude(s => s.Tender).Include(s => s.Status)
                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                .Select(t => new TenderPlaintDatailsModel
                {
                    TenderId = t.TenderId,
                    TenderStatusId = t.TenderStatusId,
                    TenderNumber = t.TenderNumber,
                    ReferenceNumber = t.ReferenceNumber,

                    Notes = t.AgencyCommunicationRequests.Select(e => e.PlaintRequests.Where(r => r.PlainRequestId == e.AgencyRequestId).FirstOrDefault().Notes).FirstOrDefault(),
                    OfferRejectionReason = t.Offers.Where(r => r.CommericalRegisterNo == selectedCR).Select(r => r.RejectionReason + (!string.IsNullOrEmpty(r.FinantialRejectionReason) ? " , " + r.FinantialRejectionReason : "")).FirstOrDefault(),
                    TechnicalEvaluation = t.Offers.Where(r => r.CommericalRegisterNo == selectedCR).Select(r => r.OfferTechnicalEvaluationStatusId).FirstOrDefault() != null && t.Offers.Where(r => r.CommericalRegisterNo == selectedCR).Select(r => r.OfferTechnicalEvaluationStatusId).FirstOrDefault() == 1
                    ? "مقبول" : "مرفوض",

                    TenderName = t.TenderName,
                    TenderAwardingType = t.TenderAwardingType,
                    AcceptedAnnouncementDate = t.AcceptedAnnouncementDate.HasValue ? t.AcceptedAnnouncementDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture) : "",
                    AwardedSupplierList = t.Offers.Where(o => o.IsActive == true && (o.PartialOfferAwardingValue.HasValue || o.TotalOfferAwardingValue.HasValue)).Select(o => new LookupModel
                    {
                        Name = o.Supplier.SelectedCrName,
                        //Value = o.Tender.TenderTypeId != (int)Enums.TenderType.Competition ? o.FinalPriceAfterDiscount.ToString() : (o.TotalOfferAwardingValue.HasValue ? o.TotalOfferAwardingValue.Value.ToString() : o.PartialOfferAwardingValue.Value.ToString())
                        Value = o.TotalOfferAwardingValue.HasValue ? o.TotalOfferAwardingValue.Value.ToString() : o.PartialOfferAwardingValue.Value.ToString()
                    }).ToList(),
                    TenderAwardingTypeName = t.TenderAwardingType.HasValue ? t.TenderAwardingType.Value ? "كلي" : "جزئى" : "",
                    FullAmount = t.Offers.Where(o => o.IsActive == true && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer
                && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(o => o.TotalOfferAwardingValue).Sum(),
                    PartialAmount = t.Offers.Where(o => o.IsActive == true && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer
                && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(o => o.PartialOfferAwardingValue).Sum(),
                    AwardedSuppliers = t.Offers.Where(o => o.IsActive == true && (o.PartialOfferAwardingValue.HasValue || o.TotalOfferAwardingValue.HasValue)).Select(o => o.Supplier.SelectedCrName).ToList(),
                    PlaintsNumber = t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).Count() == 0 ? 0 : t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint)
                    .FirstOrDefault().PlaintRequests.Where(p => p.IsActive == true).Count(),
                    EscalatedPlaintsNumber = t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).Count() == 0 ? 0 : t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint)
                    .FirstOrDefault().PlaintRequests.Where(p => p.IsEscalation && p.IsActive == true).Count(),
                    TenderIdString = Util.Encrypt(t.TenderId)
                }).FirstOrDefaultAsync();
            return tender;
        }
        public async Task<TenderPlaintDatailsModel> FindTenderForEscalationRequestById(int tenderId, string selectedCR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            TenderPlaintDatailsModel model = await _context.Tenders.Include(h => h.AgencyCommunicationRequests).ThenInclude(p => p.PlaintRequests)
                .Include(o => o.Offers).Include(s => s.Status)
                .Where(t => t.IsActive == true && t.TenderId == tenderId)
                .Select(t => new TenderPlaintDatailsModel
                {
                    AgencyName = t.Agency.NameArabic,
                    TenderTypeId = t.TenderTypeId,
                    TenderId = t.TenderId,
                    TenderStatusId = t.TenderStatusId,
                    TenderNumber = t.ReferenceNumber,
                    ReferenceNumber = t.ReferenceNumber,
                    AcceptedAnnouncementDate = t.SubmitionDate.HasValue == true ? t.SubmitionDate.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture) : "",
                    Histories = t.TenderHistories,
                    TenderName = t.TenderName,
                    TenderAwardingType = t.TenderAwardingType,
                    TenderReferenceNumber = t.ReferenceNumber,
                    TenderAwardingTypeName = t.TenderAwardingType.HasValue ? t.TenderAwardingType.Value ? "كلي" : "مبدئى" : "",
                    FullAmount = t.Offers.Where(o => o.IsActive == true && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer
                        && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(o => o.TotalOfferAwardingValue).Sum(),
                    PartialAmount = t.Offers.Where(o => o.IsActive == true && o.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer
                     && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer).Select(o => o.PartialOfferAwardingValue).Sum(),
                    AwardedSuppliers = t.Offers.Where(o => o.IsActive == true && (o.PartialOfferAwardingValue != null || o.TotalOfferAwardingValue != null)).Select(o => o.Supplier.SelectedCrName).ToList(),
                    PlaintsNumber = t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).Count() == 0 ? 0 : t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint)
                    .FirstOrDefault().PlaintRequests.Where(p => p.IsActive == true).Count(),
                    EscalatedPlaintsNumber = t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).Count() == 0 ? 0 : t.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint)
                    .FirstOrDefault().PlaintRequests.Where(p => p.IsEscalation && p.IsActive == true).Count(),
                    TenderIdString = Util.Encrypt(t.TenderId),
                    ContainsSupply = t.QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1 && t.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials)),
                    IsTenderContainsTawreedTables = t.IsTenderContainsTawreedTables,
                    NationalProductPercentage = t.NationalProductPercentage,
                    AwardedSupplierList = t.Offers.Where(o => o.IsActive == true && (o.PartialOfferAwardingValue.HasValue || o.TotalOfferAwardingValue.HasValue)).Select(o => new LookupModel
                    {
                        Name = o.Supplier.SelectedCrName,
                        Value = o.Tender.TenderTypeId != (int)Enums.TenderType.Competition ? o.FinalPriceAfterDiscount.ToString() : (o.TotalOfferAwardingValue.HasValue ? o.TotalOfferAwardingValue.Value.ToString() : o.PartialOfferAwardingValue.Value.ToString())
                    }).ToList(),
                    DeliveryDate = t.DeliveryDate,
                    SamplesDeliveryAddress = Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress + ":" + t.SamplesDeliveryAddress == null ? "" : t.SamplesDeliveryAddress
                + (t.BuildingName != null ? ", " + Resources.TenderResources.DisplayInputs.BuildingName + ":" + t.BuildingName : "")
                + (t.FloarNumber != null ? ", " + Resources.TenderResources.DisplayInputs.FloarNumber + ":" + t.FloarNumber : "")
                + (t.DepartmentName != null ? ", " + Resources.TenderResources.DisplayInputs.DepartmentName + ":" + t.DepartmentName : ""),
                    OffersOpeningAddress = t.OffersOpeningAddress.AddressName,
                    AwardingStoppingPeriod = t.AwardingStoppingPeriod,
                    SupplierNeedSample = t.SupplierNeedSample,
                    IsSampleAddresSameOffersAddress = (bool)t.TenderAddress.IsSampleAddresSameOffersAddress,
                    OffersDeliveryDate = t.TenderDates.OffersDeliveryDate,
                    OfferDeliveryAddress = t.TenderAddress.OffersDeliveryAddress,
                    OfferBuildingName = t.TenderAddress.OfferBuildingName,
                    OfferFloarNumber = t.TenderAddress.OfferFloarNumber,
                    OfferDepartmentName = t.TenderAddress.OfferDepartmentName,
                    MaxTimeToAnswerQuestions = t.TenderConditionsTemplate.MaxTimeToAnswerQuestions,
                    ParticipationConfirmationLetterDate = t.TenderDates.ParticipationConfirmationLetterDate,
                    FinalGuaranteePercentage = t.AwardingDiscountPercentage,
                    AwardingExpectedDate = t.TenderDates.AwardingExpectedDate,
                    StartingBusinessOrServicesDate = t.TenderDates.StartingBusinessOrServicesDate
                }).FirstOrDefaultAsync();
            if (model.IsSampleAddresSameOffersAddress)
            {
                model.OfferDeliveryAddress = model.SamplesDeliveryAddress;
            }
            else
            {
                model.OfferDeliveryAddress = Resources.TenderResources.DisplayInputs.SamplesDeliveryAddress + ":" + model.OfferDeliveryAddress == null ? "" : model.OfferDeliveryAddress
                + (model.OfferBuildingName != null ? ", " + Resources.TenderResources.DisplayInputs.BuildingName + ":" + model.OfferBuildingName : "")
                + (model.OfferFloarNumber != null ? ", " + Resources.TenderResources.DisplayInputs.FloarNumber + ":" + model.OfferFloarNumber : "")
                + (model.OfferDepartmentName != null ? ", " + Resources.TenderResources.DisplayInputs.DepartmentName + ":" + model.OfferDepartmentName : "");
            }
            model.VersionId = await _context.TenderVersions.
                Where(a => a.TenderId == tenderId && a.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity)
                .Select(a => a.VersionId).FirstOrDefaultAsync();
            if (model.VersionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                model.StartingSendingEnquiries = await _context.TenderHistories.Where(h => h.TenderId == tenderId && h.StatusId == (int)Enums.TenderStatus.Approved).OrderByDescending(h => h.TenderHistoryId).Select(x => x.CreatedAt).FirstOrDefaultAsync();
            }
            //model.AwardedSupplierList = await _context.Offers.Where(o => o.TenderId == tenderId && o.IsActive == true && (o.PartialOfferAwardingValue.HasValue || o.TotalOfferAwardingValue.HasValue)).Select(o => new LookupModel
            //{
            //    Name = o.Supplier.SelectedCrName,
            //    Value = o.Tender.TenderTypeId != (int)Enums.TenderType.Competition ? o.FinalPriceAfterDiscount.ToString() : (o.TotalOfferAwardingValue.HasValue ? o.TotalOfferAwardingValue.Value.ToString() : o.PartialOfferAwardingValue.Value.ToString())
            //}).ToList();
            return model;
        }
        public async Task<Tender> FindTenderWithPlaintRequestByTenderId(int tenderId, string selectedCR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var entities = await _context.Tenders
                .Include(o => o.Offers).ThenInclude(p => p.PlaintRequests).
                Include(t => t.TenderHistories)
                .Include(h => h.AgencyCommunicationRequests)
                .Where(t => t.TenderId == tenderId)
                .Where(t =>
                t.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed
                )
                .FirstOrDefaultAsync();
            return entities;
        }

        public async Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintRequestByTenderIdAndCR(int tenderId, string selectedCR)
        {

            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            TenderPlaintCommunicationRequestModel model = await _context.Offers
            .Include(p => p.PlaintRequests)
            .ThenInclude(p => p.Attachments)
            .Where(t => t.TenderId == tenderId && t.IsActive == true && t.CommericalRegisterNo.Equals(selectedCR) && t.IsActive == true
            && (t.Tender.TenderAwardingHistories.Max(th => th.AwardingIndex) == (int)Enums.AwardingHistoryIndex.One
            ||
            (
            t.Tender.TenderAwardingHistories.Max(th => th.AwardingIndex) == (int)Enums.AwardingHistoryIndex.Two
            && t.Tender.TenderAwardingHistories.FirstOrDefault(th => th.AwardingIndex == (int)Enums.AwardingHistoryIndex.One) != null
            )
            )
            )
            .Select(o => new TenderPlaintCommunicationRequestModel
            {
                EncryptedTenderId = Util.Encrypt(o.TenderId),
                EncryptedOfferId = Util.Encrypt(o.OfferId),
                TenderStatusId = o.Tender.TenderStatusId,
                OfferStatusId = o.OfferStatusId,
                AwardingStoppingPeriod = o.Tender.AwardingStoppingPeriod,
                PlaintRequestId = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().PlainRequestId : 0,
                PlaintReason = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().PlaintReason : "",
                PlaintStatusId = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().AgencyCommunicationRequest.PlaintAcceptanceStatusId : 0,
                PlaintStatusName = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().AgencyCommunicationRequest.PlaintAcceptanceStatus.Name : "",
                CommunicationRequestId = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().AgencyCommunicationRequestId : 0,
                StatusId = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().AgencyCommunicationRequest.StatusId : 0,
                StatusName = o.PlaintRequests.FirstOrDefault() != null ? o.PlaintRequests.FirstOrDefault().AgencyCommunicationRequest.Status.Name : "",
                tenderHistory = o.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                AttachmentList = o.PlaintRequests.Any() ?
                    o.PlaintRequests.FirstOrDefault().Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.PlainRequest).Select(s => new CommunicationAttachmentModel
                    {
                        AttachmentTypeId = s.AttachmentTypeId,
                        Name = s.Name,
                        FileNetReferenceId = s.FileNetReferenceId
                    }).ToList()
                    : new List<CommunicationAttachmentModel>()
            }).FirstOrDefaultAsync();

            return model;
        }
        public async Task<PlaintRequest> FindPlaintRequestToEscalateById(int plaintId, string selectedCR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(plaintId), plaintId.ToString());
            PlaintRequest model = await _context.PlaintRequests
                .Include(p => p.AgencyCommunicationRequest)
                .ThenInclude(a => a.Tender)
                .Include(p => p.Attachments)
                .Include(p => p.Offer)
                .Where
                (
                        p => p.IsActive == true && p.PlainRequestId == plaintId && p.Offer.CommericalRegisterNo.Equals(selectedCR)
                        && !p.IsEscalation && (p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.Rejected || p.AgencyCommunicationRequest.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Approved)
                                        )
                .FirstOrDefaultAsync();
            return model;
        }
        public async Task<TenderPlaintCommunicationRequestModel> FindSupplierPlaintDetailsByPlaintIdAndCR(int agencyRequestId, string selectedCR)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyRequestId), agencyRequestId.ToString());
            TenderPlaintCommunicationRequestModel model = await _context.PlaintRequests

                .Where
                (
                        p => p.IsActive == true && p.AgencyCommunicationRequestId == agencyRequestId /*p.PlainRequestId == plaintId*/ && p.Offer.CommericalRegisterNo.Equals(selectedCR)
                )
                .Select(p => new TenderPlaintCommunicationRequestModel
                {
                    SupplierCR = p.Offer.Supplier.SelectedCr,
                    SupplierName = p.Offer.Supplier.SelectedCrName,
                    RequestDate = p.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    AgenctName = p.Offer.Tender.Agency.NameArabic,
                    RejectionDate = p.AgencyCommunicationRequest.UpdatedAt.HasValue ? p.AgencyCommunicationRequest.UpdatedAt.Value.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture) : "",
                    EscalationStatusId = p.AgencyCommunicationRequest.EscalationAcceptanceStatusId,
                    EscalationStatus = p.AgencyCommunicationRequest.EscalationAcceptanceStatus == null ? "" : p.AgencyCommunicationRequest.EscalationAcceptanceStatus.Name,
                    EncryptedTenderId = Util.Encrypt(p.AgencyCommunicationRequest.Tender.TenderId),
                    EncryptedOfferId = Util.Encrypt(p.OfferId),
                    TenderStatusId = p.AgencyCommunicationRequest.Tender.TenderStatusId,
                    StatusName = p.AgencyCommunicationRequest.Status.Name,
                    ProcedureId = p.AgencyCommunicationRequest.TenderPlaintRequestProcedureId,
                    ProcedureName = p.AgencyCommunicationRequest.TenderPlaintRequestProcedureId.HasValue ? p.AgencyCommunicationRequest.TenderPlaintRequestProcedure.Name : "",
                    Details = p.AgencyCommunicationRequest.Details,
                    RejectionReason = p.Notes,
                    IsEscalation = p.IsEscalation,
                    LastUpdateDate = p.AgencyCommunicationRequest.UpdatedAt,
                    ParentRejectionReason = p.AgencyCommunicationRequest.RejectionReason,
                    OfferStatusId = p.Offer.OfferStatusId,
                    AwardingStoppingPeriod = p.AgencyCommunicationRequest.Tender.AwardingStoppingPeriod,
                    PlaintRequestId = p.PlainRequestId,
                    EncryptedPlaintRequestId = Util.Encrypt(p.PlainRequestId),
                    PlaintReason = p.PlaintReason,
                    TenderId = p.Offer.TenderId,
                    PlaintStatusId = p.AgencyCommunicationRequest.PlaintAcceptanceStatusId,
                    PlaintStatusName = p.AgencyCommunicationRequest.PlaintAcceptanceStatus == null ? Resources.SharedResources.DisplayInputs.NoResponse : p.AgencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved ? p.AgencyCommunicationRequest.PlaintAcceptanceStatus.Name : Resources.SharedResources.DisplayInputs.NoResponse,
                    StatusId = p.AgencyCommunicationRequest.StatusId,
                    CommunicationRequestId = p.AgencyCommunicationRequest.AgencyRequestId,
                    tenderHistory = p.AgencyCommunicationRequest.Tender.TenderHistories.OrderByDescending(d => d.TenderHistoryId).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),

                    AttachmentList =
                p.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.PlainRequest).Select(s => new CommunicationAttachmentModel
                {
                    AttachmentTypeId = s.AttachmentTypeId,
                    Name = s.Name,
                    FileNetReferenceId = s.FileNetReferenceId
                }).ToList(),
                    EscalationAttachments =
                p.EscalationRequest.PlaintRequest.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.Escalation)
                .Select(b => new CommunicationAttachmentModel
                {
                    AttachmentTypeId = b.AttachmentTypeId,
                    Name = b.Name,
                    FileNetReferenceId = b.FileNetReferenceId
                }).FirstOrDefault()

                }
                ).FirstOrDefaultAsync();

            return model;
        }

        public async Task<PlaintRequest> FindPlaintById(int plaintId, string agencyId, int committeeId)
        {
            PlaintRequest result = await _context.PlaintRequests
                .Where(p => p.PlainRequestId == plaintId && p.IsActive == true && p.AgencyCommunicationRequest.Tender.AgencyCode == agencyId
                                && (p.AgencyCommunicationRequest.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ? p.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId : p.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId)
                )
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<EscalationRequest> FindEscalationByPlaintId(int plaintId, string agencyId)
        {
            EscalationRequest result = await _context.EscalationRequests
                .Where(e => e.PlaintRequest.PlainRequestId == plaintId && e.IsActive == true /*&& e.PlaintRequest.AgencyCommunicationRequest.Tender.AgencyCode == agencyId*/
                )

                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<AgencyCommunicationRequest> FindCommunicationRequestByIdForPlaint(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());

            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(a => a.Tender)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true)
                .FirstOrDefaultAsync();
            return model;
        }
        public async Task<AgencyCommunicationRequest> FindCommunicationRequestByIdForApprovePlaint(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());

            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(a => a.Tender)
                .ThenInclude(a => a.Offers)
                .Include(a => a.PlaintNotification)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true)
                .FirstOrDefaultAsync();
            return model;
        }
        public async Task<AgencyCommunicationRequest> FindCommunicationRequestByIdForRejectPlaint(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());

            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests
                .Include(a => a.Tender)
                .Include(a => a.PlaintNotification)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true)
                .FirstOrDefaultAsync();
            return model;
        }

        public async Task<List<PlaintRequest>> GetAllPlaintRequestsByRequestId(int id)
        {
            return await _context.PlaintRequests
                .Include(a => a.Offer)
                .Where(a => a.AgencyCommunicationRequestId == id && a.IsActive == true).ToListAsync();
        }

        public async Task<SupplierExtendOfferDatesRequest> FindCommunicationRequestByTenderIdAndCr(int tenderId, string Cr)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            return await _context.SupplierExtendOfferDatesRequests.Where(r => r.CR == Cr && r.AgencyCommunicationRequest.TenderId == tenderId && r.IsActive == true).FirstOrDefaultAsync();
        }
        public async Task<AgencyCommunicationRequest> FindEscalationRequestById(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            AgencyCommunicationRequest model = await _context.AgencyCommunicationRequests.Include(a => a.PlaintRequests).ThenInclude(p => p.EscalationRequest).Include(a => a.Tender).ThenInclude(t => t.Offers)
                .Where(p => p.AgencyRequestId == requestId && p.IsActive == true
               )
                .FirstOrDefaultAsync();
            return model;
        }
        public async Task<PlaintRequestModel> FindPlaintRequestModelById(int plaintId, string agencyCode, int committeeId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(plaintId), plaintId.ToString());
            PlaintRequestModel model = await _context.PlaintRequests
                .Include(p => p.Offer)
                .ThenInclude(o => o.Tender)
                .ThenInclude(t => t.TenderHistories)
                .Include(p => p.AgencyCommunicationRequest)
                .Include(p => p.Attachments)
                .Where(p => p.IsActive == true && p.PlainRequestId == plaintId)
                .WhereIf(!string.IsNullOrEmpty(agencyCode), p => p.AgencyCommunicationRequest.Tender.AgencyCode == agencyCode)

                .Select(p => new PlaintRequestModel
                {
                    Notes = p.Notes,
                    PlaintReason = p.PlaintReason,
                    PlaintStatusId = p.AgencyCommunicationRequest.PlaintAcceptanceStatusId,
                    PlaintStatusName = p.AgencyCommunicationRequest.PlaintAcceptanceStatus.Name,
                    StatusId = p.AgencyCommunicationRequest.StatusId,
                    StatusName = p.AgencyCommunicationRequest.Status.Name,
                    PlainRequestId = Util.Encrypt(p.PlainRequestId),
                    RejectionReason = p.AgencyCommunicationRequest.RejectionReason,
                    CreationDate = p.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    SelectedCrName = p.Offer.Supplier.SelectedCrName,
                    CanTakeAction = (p.AgencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent
                    || p.AgencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected
                    ) && p.AgencyCommunicationRequest.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ?
                    (p.AgencyCommunicationRequest.Tender.OffersCheckingCommitteeId == committeeId) : (p.AgencyCommunicationRequest.Tender.DirectPurchaseCommitteeId == committeeId),
                    AgencyCommunicationRequestId = p.AgencyCommunicationRequestId,
                    EncryptedAgencyCommunicationRequestId = Util.Encrypt(p.AgencyCommunicationRequestId),
                    TenderId = Util.Encrypt(p.AgencyCommunicationRequest.Tender.TenderId),
                    AwardingStoppingPeriod = p.AgencyCommunicationRequest.Tender.AwardingStoppingPeriod,
                    tenderHistory = p.AgencyCommunicationRequest.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                    AttachmentList = p.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.PlainRequest).Select(s => new CommunicationAttachmentModel
                    {
                        AttachmentTypeId = s.AttachmentTypeId,
                        Name = s.Name,
                        FileNetReferenceId = s.FileNetReferenceId
                    }).ToList(),
                }
                ).FirstOrDefaultAsync();
            return model;
        }

        public async Task<PlaintRequestModel> FindEscalationRequestModelById(int plaintId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(plaintId), plaintId.ToString());
            PlaintRequestModel model = await _context.PlaintRequests
                .Include(p => p.Offer)
                .ThenInclude(o => o.Tender)
                .ThenInclude(t => t.TenderHistories)
                .Include(p => p.AgencyCommunicationRequest)
                .Include(p => p.Attachments)
                .Where(p => p.PlainRequestId == plaintId && p.IsActive == true && p.IsEscalation == true && p.AgencyCommunicationRequest.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed)
                .Select(p => new PlaintRequestModel
                {
                    EncryptedAgencyCommunicationRequestId = Util.Encrypt(p.AgencyCommunicationRequestId),
                    EncryptedPlaintId = Util.Encrypt(p.PlainRequestId),
                    ApprovalDate = p.AgencyCommunicationRequest.PlaintNotification.ApprovalDate,
                    Notes = p.Notes,
                    EscalationNotes = p.EscalationRequest.EscalationNotes,
                    EscalationRejectionReason = p.AgencyCommunicationRequest.EscalationRejectionReason,
                    EscalationStatusId = p.AgencyCommunicationRequest.EscalationAcceptanceStatusId,
                    EscalationStatusName = p.AgencyCommunicationRequest.EscalationAcceptanceStatus.Name,
                    PlaintReason = p.PlaintReason,
                    PlaintStatusId = p.AgencyCommunicationRequest.PlaintAcceptanceStatusId,
                    PlaintStatusName = p.AgencyCommunicationRequest.PlaintAcceptanceStatus.Name,
                    PlainRequestId = Util.Encrypt(p.PlainRequestId),
                    RejectionReason = p.AgencyCommunicationRequest.RejectionReason,
                    CreationDate = p.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    EscalationDate = p.EscalationRequest.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    SelectedCrName = p.Offer.Supplier.SelectedCrName,
                    CanTakeAction = (p.AgencyCommunicationRequest.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent || p.AgencyCommunicationRequest.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected),
                    AgencyCommunicationRequestId = p.AgencyCommunicationRequestId,
                    TenderId = Util.Encrypt(p.AgencyCommunicationRequest.Tender.TenderId),
                    AwardingStoppingPeriod = p.AgencyCommunicationRequest.Tender.AwardingStoppingPeriod,
                    tenderHistory = p.AgencyCommunicationRequest.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                    AttachmentList = p.Attachments == null ?
                new List<CommunicationAttachmentModel>() :
                p.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.PlainRequest).Select(s => new CommunicationAttachmentModel
                {
                    AttachmentTypeId = s.AttachmentTypeId,
                    Name = s.Name,
                    FileNetReferenceId = s.FileNetReferenceId
                }).ToList(),
                    EscalationAttachments = p.EscalationRequest.PlaintRequest.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.Escalation).Select(a => new CommunicationAttachmentModel
                    {
                        AttachmentTypeId = a.AttachmentTypeId,
                        Name = a.Name,
                        FileNetReferenceId = a.FileNetReferenceId
                    }).FirstOrDefault()
                }
                ).FirstOrDefaultAsync();
            return model;
        }
        public async Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsById(int Id, string agencyId, int committeeId, bool isEscalation)
        {
            Check.ArgumentNotNullOrEmpty(nameof(Id), Id.ToString());
            TenderPLaintCommunicationModel model = _context.AgencyCommunicationRequests
                .Where(a => a.IsActive == true && a.AgencyRequestId == Id)
                .WhereIf(!isEscalation, a => a.Tender.AgencyCode == agencyId)
               .Select(a => new TenderPLaintCommunicationModel
               {
                   CommunicationRequestId = a.AgencyRequestId,
                   StatusId = a.StatusId,
                   EscalationStatusId = a.StatusId,
                   RejectionReason = a.RejectionReason,
                   procedureId = a.TenderPlaintRequestProcedureId,
                   procedureName = a.TenderPlaintRequestProcedure != null ? a.TenderPlaintRequestProcedure.Name : "",
                   CreationDate = a.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                   CanManagerTakeAction = !isEscalation ? ((a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending && a.IsActive == true
                    && (a.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ? a.Tender.OffersCheckingCommitteeId == committeeId :
                    a.Tender.DirectPurchaseCommitteeId == committeeId)))
                    : a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending,

                   CanSecretaryTakeAction = !isEscalation ? (a.PlaintRequests.Where(p => (p.AgencyCommunicationRequest.PlaintAcceptanceStatusId == (int)Enums.AgencyPlaintStatus.New) && p.IsActive == true
                     && a.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ? a.Tender.OffersCheckingCommitteeId == committeeId : a.Tender.DirectPurchaseCommitteeId == committeeId).Count() == 0 && (a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.UnderUpdate || a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected) ? true : false)
                    : a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Approved || a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.RejectedByPlaintManager,
                   HasPlaints = a.PlaintRequests.Where(p => p.IsActive == true).Any() ? true : false,
                   EncryptedTenderId = Util.Encrypt(a.Tender.TenderId),
                   AwardingStoppingPeriod = a.Tender.AwardingStoppingPeriod,
                   tenderHistory = a.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                   TenderStatusId = a.Tender.TenderStatusId
               }
            ).FirstOrDefault();
            return model;
        }

        public async Task<TenderEscalatedPLaintModel> FindAgencyCommunicationEscalatedPalintsByTenderId(int requestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(requestId), requestId.ToString());
            TenderEscalatedPLaintModel model = await _context.AgencyCommunicationRequests
                .Where(a => a.IsActive == true && a.AgencyRequestId == requestId && a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint
                )
                .Select(a => new TenderEscalatedPLaintModel
                {
                    ApprovalDate = a.PlaintNotification.ApprovalDate,
                    CommunicationRequestId = a.AgencyRequestId,
                    EncryptedCommunicationRequestId = Util.Encrypt(a.AgencyRequestId),
                    StatusId = a.StatusId,
                    AcceptanceStatusId = a.EscalationAcceptanceStatusId,
                    StatusName = a.Status.Name,
                    EscalationStatusId = a.EscalationStatusId,
                    EscalationStatusName = a.EscalationStatus == null ? "" : a.EscalationStatus.Name,
                    RejectionReason = a.EscalationRejectionReason,
                    procedureId = a.TenderPlaintRequestProcedureId,
                    procedureName = a.TenderPlaintRequestProcedure != null ? a.TenderPlaintRequestProcedure.Name : "",
                    Details = a.Details,
                    CreationDate = a.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    AwardingStoppingPeriod = a.Tender.AwardingStoppingPeriod,
                    tenderHistory = a.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                    CanSecretaryTakeAction = a.PlaintRequests.Any(p => p.IsEscalation && p.IsActive == true)
                    && (a.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent || a.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected),

                    CanManagerTakeAction = (a.EscalationStatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending && a.IsActive == true),
                    HasEscalatedPlaints = a.PlaintRequests.Any(p => p.IsEscalation),
                    EncryptedTenderId = Util.Encrypt(a.Tender.TenderId),
                    TenderStatusId = a.Tender.TenderStatusId
                }
            ).FirstOrDefaultAsync();
            return model;
        }
        public async Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByRequestId(int agencyCommunicationId, string agencyId, int committeeId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyCommunicationId), agencyCommunicationId.ToString());
            TenderPLaintCommunicationModel model = _context.AgencyCommunicationRequests
                .Where(a => a.IsActive == true && a.AgencyRequestId == agencyCommunicationId && a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint && a.Tender.AgencyCode == agencyId)
                .Select(a => new TenderPLaintCommunicationModel
                {
                    PlaintsNoNotes = a.PlaintRequests.Where(p => string.IsNullOrEmpty(p.Notes)).Count(),
                    TenderId = Util.Encrypt(a.TenderId),
                    CommunicationRequestId = a.AgencyRequestId,
                    StatusId = a.StatusId,
                    StatusName = a.Status.Name,
                    plaintStatusId = a.PlaintAcceptanceStatusId,
                    PlaintStatusName = a.PlaintAcceptanceStatus.Name,
                    RejectionReason = a.RejectionReason,
                    procedureId = a.TenderPlaintRequestProcedureId,
                    procedureName = a.TenderPlaintRequestProcedure != null ? a.TenderPlaintRequestProcedure.Name : "",
                    CreationDate = a.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    CanManagerTakeAction = (a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.UnderRevision && a.IsActive == true
                    && (a.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ?
                    a.Tender.OffersCheckingCommitteeId == committeeId
                    : a.Tender.DirectPurchaseCommitteeId == committeeId)
                    ),
                    AwardingStoppingPeriod = a.Tender.AwardingStoppingPeriod,
                    tenderHistory = a.Tender.TenderHistories.OrderBy(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                    CanSecretaryTakeAction = (a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent || a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                    && (a.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ?
                    a.Tender.OffersCheckingCommitteeId == committeeId
                    : a.Tender.DirectPurchaseCommitteeId == committeeId
                    ),
                    Details = a.Tender.OffersCheckingCommitteeId.ToString(),
                    HasPlaints = a.PlaintRequests.Where(p => p.IsActive == true).Count() > 0 ? true : false,
                    EncryptedTenderId = Util.Encrypt(a.Tender.TenderId),
                    TenderStatusId = a.Tender.TenderStatusId
                }
            ).FirstOrDefault();
            return model;
        }
        public async Task<TenderPLaintCommunicationModel> FindAgencyCommunicationPalintRequestsByTenderId(int agencyRequestId, string agencyId, int committeeId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyRequestId), agencyRequestId.ToString());
            TenderPLaintCommunicationModel model = await _context.AgencyCommunicationRequests
                .Where(a => a.IsActive == true && a.AgencyRequestId == agencyRequestId && a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint)
                .WhereIf(!string.IsNullOrEmpty(agencyId), t => t.Tender.AgencyCode == agencyId)

                .OrderByDescending(a => a.AgencyRequestId)
                .Select(a => new TenderPLaintCommunicationModel
                {
                    TenderId = Util.Encrypt(a.TenderId),
                    CommunicationRequestId = a.AgencyRequestId,
                    EncryptedCommunicationRequestId = Util.Encrypt(a.AgencyRequestId),
                    StatusId = a.StatusId,
                    StatusName = a.Status.Name,
                    plaintStatusId = a.PlaintAcceptanceStatusId,
                    PlaintStatusName = a.PlaintAcceptanceStatus.Name,

                    RejectionReason = a.RejectionReason,
                    procedureId = a.TenderPlaintRequestProcedureId,
                    procedureName = a.TenderPlaintRequestProcedure != null ? a.TenderPlaintRequestProcedure.Name : "",
                    CreationDate = a.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    CanManagerTakeAction = (a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Pending && a.IsActive == true
                    && (a.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ?
                    a.Tender.OffersCheckingCommitteeId == committeeId : a.Tender.DirectPurchaseCommitteeId == committeeId)
                    ),
                    AwardingStoppingPeriod = a.Tender.AwardingStoppingPeriod,
                    tenderHistory = a.Tender.TenderHistories.OrderByDescending(a => a.CreatedAt).FirstOrDefault(x => x.StatusId == (int)Enums.TenderStatus.OffersAwardedConfirmed),
                    CanSecretaryTakeAction = (a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.RequestSent || a.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected)
                    && (a.Tender.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase ?
                    a.Tender.OffersCheckingCommitteeId == committeeId : a.Tender.DirectPurchaseCommitteeId == committeeId
                    ),
                    Details = a.Details,
                    HasPlaints = a.PlaintRequests.Any(p => p.IsActive == true),
                    EncryptedTenderId = Util.Encrypt(a.Tender.TenderId),
                    TenderStatusId = a.Tender.TenderStatusId
                }
            ).FirstOrDefaultAsync();
            return model;
        }
        public async Task<QueryResult<TenderPlaintCheckingModel>> FindTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria)
        {
            Check.ArgumentNotNullOrEmpty(nameof(plaintSearchCriteria.TenderId), plaintSearchCriteria.TenderId.ToString());
            QueryResult<TenderPlaintCheckingModel> model = await _context.PlaintRequests
                .Where(p => p.IsActive == true && p.AgencyCommunicationRequest.AgencyRequestId == Util.Decrypt(plaintSearchCriteria.AgencyRequestId))
                .OrderByDescending(a => a.AgencyCommunicationRequestId)
                .Select(p => new TenderPlaintCheckingModel
                {
                    PlainRequestId = Util.Encrypt(p.PlainRequestId),
                    PlaintReason = p.PlaintReason,
                    SelectedCrName = p.Offer.Supplier.SelectedCrName,
                    CommericalRegisterNo = p.Offer.Supplier.SelectedCr,
                    Notes = p.Notes,
                    CreationDate = p.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    PlaintStatusId = p.AgencyCommunicationRequest.PlaintAcceptanceStatusId,
                    PlaintStatusName = p.AgencyCommunicationRequest.PlaintAcceptanceStatus == null ? "" : p.AgencyCommunicationRequest.PlaintAcceptanceStatus.Name,
                    AttachmentList = p.Attachments.Where(a => a.IsActive == true && a.AttachmentTypeId == (int)Enums.AttachmentType.PlainRequest).Select(s => new CommunicationAttachmentModel
                    {
                        AttachmentTypeId = s.AttachmentTypeId,
                        Name = s.Name,
                        FileNetReferenceId = s.FileNetReferenceId
                    }).ToList(),
                    EncryptedTenderId = Util.Encrypt(p.AgencyCommunicationRequest.Tender.TenderId),
                    TenderStatusId = p.AgencyCommunicationRequest.Tender.TenderStatusId,
                    AwardingStoppingPeriod = p.AgencyCommunicationRequest.Tender.AwardingStoppingPeriod,
                    CommunicationRequestId = p.AgencyCommunicationRequestId,
                    StatusId = p.AgencyCommunicationRequest.StatusId,
                    StatusName = p.AgencyCommunicationRequest.StatusId == (int)Enums.AgencyCommunicationRequestStatus.Rejected ? "تم رفض الإعتماد" : p.AgencyCommunicationRequest.Status.Name
                                        ,
                    AgencyRejectionReason = p.Notes
                }
                    ).ToQueryResult(plaintSearchCriteria.PageNumber, plaintSearchCriteria.PageSize);
            return model;
        }
        public async Task<QueryResult<TenderPlaintCheckingModel>> FindEscalatedTenderPlaintRequestsByTenderId(PlaintSearchCriteria plaintSearchCriteria)
        {
            Check.ArgumentNotNullOrEmpty(nameof(plaintSearchCriteria.AgencyRequestId), plaintSearchCriteria.AgencyRequestId.ToString());
            QueryResult<TenderPlaintCheckingModel> model = await _context.PlaintRequests
                //.Include(a => a.AgencyCommunicationRequest).ThenInclude(a => a.RejectionHistories)
                .Where(p => p.IsActive == true && p.IsEscalation && p.AgencyCommunicationRequest.AgencyRequestId == Util.Decrypt(plaintSearchCriteria.AgencyRequestId))
                .Select(p => new TenderPlaintCheckingModel
                {
                    ID = p.PlainRequestId,
                    IsEscalation = p.IsEscalation,
                    PlainRequestId = Util.Encrypt(p.PlainRequestId),
                    PlaintReason = p.PlaintReason,
                    SelectedCr = p.Offer.CommericalRegisterNo,
                    CreationDate = p.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    EscalationDate = p.EscalationRequest == null ? "" : p.EscalationRequest.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    PlaintStatusId = p.AgencyCommunicationRequest.PlaintAcceptanceStatusId,
                    AttachmentList = new List<CommunicationAttachmentModel>(),
                    EscalationAttachments = null,
                    EncryptedTenderId = Util.Encrypt(p.AgencyCommunicationRequest.Tender.TenderId),
                    tenderHistory = p.AgencyCommunicationRequest.RejectionHistories.Where(a => a.RequestsRejectionTypeId == (int)Enums.RequestRejectionType.Plaint && a.StatusId == (int)Enums.AgencyPlaintStatus.Rejected).OrderByDescending(a => a.CreatedAt).FirstOrDefault(),
                    AgencyRejectionDate = "",
                    AgencyRejectionReason = p.Notes,
                    EscalationStatusId = p.AgencyCommunicationRequest.EscalationStatusId,
                    EscalationAcceptanceStatusId = p.AgencyCommunicationRequest.EscalationAcceptanceStatusId
                }
                    ).ToQueryResult(plaintSearchCriteria.PageNumber, plaintSearchCriteria.PageSize);

            var EscalationAcceptanceStatusLookup = await _context.PlaintStatus.Select(s => new LookupModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            var EscalationStatusLookup = await _context.AgencyCommunicationRequestStatuses.Select(s => new LookupModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
            var PlaintRequestIds = model.Items.Select(c => c.ID).ToList();
            var attachmentsEntity = await _context.AgencyCommunicationAttachments.ToListAsync();
            var attachments = attachmentsEntity.Where(a => a.IsActive == true && PlaintRequestIds.Contains(a.PlaintRequestId)).Select(s => new CommunicationAttachmentModel
            {
                PlaintRequestId = s.PlaintRequestId,
                AttachmentTypeId = s.AttachmentTypeId,
                Name = s.Name,
                FileNetReferenceId = s.FileNetReferenceId
            }).ToList();

            var CRs = model.Items.Select(c => c.SelectedCr).ToList();
            var supplierList = await _context.Suppliers.Where(s => s.IsActive == true && CRs.Contains(s.SelectedCr)).ToListAsync();
            foreach (var item in model.Items)
            {
                RejectionHistory obj = item.tenderHistory as RejectionHistory;
                if (obj != null)
                    item.AgencyRejectionDate = obj.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);

                item.EscalationStatusName = EscalationAcceptanceStatusLookup.Where(x => x.Id == item.EscalationAcceptanceStatusId).Select(x => x.Name).FirstOrDefault();
                item.StatusName = EscalationStatusLookup.Where(x => x.Id == item.EscalationStatusId).Select(x => x.Name).FirstOrDefault();
                item.AttachmentList = attachments.Where(a => a.PlaintRequestId == item.ID && a.AttachmentTypeId == (int)Enums.AttachmentType.PlainRequest).ToList();
                item.EscalationAttachments = attachments.Where(a => a.PlaintRequestId == item.ID && a.AttachmentTypeId == (int)Enums.AttachmentType.Escalation).FirstOrDefault();
                item.SelectedCrName = supplierList.Where(s => s.SelectedCr == item.SelectedCr).Select(S => S.SelectedCrName).FirstOrDefault();
            }

            return model;
        }

        public async Task<NegotiationFirstStage> GetLastNegotiationFirstStageWithSupplierByTenderId(int tenderId)
        {
            var CommunicationRequest = await _context.NegotiationFirstStages
                .Include(s => s.NegotiationFirstStageSuppliers)
                .Include(s => s.AgencyCommunicationRequest)
                .Include(s => s.Attachments)
                .Where(s => s.AgencyCommunicationRequest.TenderId == tenderId && s.IsActive == true).OrderByDescending(a => a.NegotiationId).FirstOrDefaultAsync();
            return CommunicationRequest;
        }

        public async Task<NegotiationFirstStage> GetLastAgreedNegotiationFirstStageByTenderId(int tenderId)
        {
            var CommunicationRequest = await _context.NegotiationFirstStages
                .Where(s => s.AgencyCommunicationRequest.TenderId == tenderId && s.IsActive == true && (s.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreed || s.StatusId == (int)Enums.enNegotiationStatus.SupplierAgreedWithExtraDiscount))
                .OrderByDescending(a => a.NegotiationId).FirstOrDefaultAsync();
            return CommunicationRequest;
        }

        public async Task<NegotiationSecondStage> GetNegotiationSecondStageTenderId(int TenderId)
        {
            var CommunicationRequest = await _context.NegotiationSecondStages
                .Include(d => d.AgencyCommunicationRequest)
                .Include(h => h.NegotiationStatus)
                .Where(s => s.IsActive == true && s.AgencyCommunicationRequest.TenderId == TenderId)
                .Select(s => s).FirstOrDefaultAsync();
            return CommunicationRequest;
        }

        public async Task<QueryResult<EscalatedTendersModel>> GetEscalatedTenders(EscalationSearchCriteria searchCriteria)
        {
            var result = await _context.Tenders
                .Where(x => x.IsActive == true)
                .WhereIf(searchCriteria.TenderTypeId != 0, c => c.TenderTypeId == searchCriteria.TenderTypeId)
                .WhereIf(!String.IsNullOrWhiteSpace(searchCriteria.ReferenceNumber), c => c.ReferenceNumber == searchCriteria.ReferenceNumber)
                .WhereIf(!String.IsNullOrWhiteSpace(searchCriteria.TenderNumber), c => c.TenderNumber == searchCriteria.TenderNumber)
                .WhereIf(!String.IsNullOrWhiteSpace(searchCriteria.TenderName), c => c.TenderName.Contains(searchCriteria.TenderName))
                .Where(x => x.AgencyCommunicationRequests.Any(a => a.PlaintRequests.Any(p => p.IsEscalation)))
                .SortBy(searchCriteria.Sort, searchCriteria.SortDirection)
                .Select(x => new EscalatedTendersModel
                {
                    TenderName = x.TenderName,
                    TenderNumber = x.TenderNumber,
                    ReferenceNumber = x.ReferenceNumber,
                    TenderId = x.TenderId,
                    AgencyName = x.Agency.NameArabic,
                    BranchName = x.Branch.BranchName,
                    EscalationNumber = x.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).OrderByDescending(a => a.AgencyRequestId).FirstOrDefault().PlaintRequests.Where(p => p.IsEscalation == true).Count(),
                    AgencyRequestId = x.AgencyCommunicationRequests.Where(a => a.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Plaint).OrderByDescending(a => a.AgencyRequestId).FirstOrDefault().AgencyRequestId,
                    TenderStatusId = x.TenderStatusId,
                    TenderStatusIdString = Util.Encrypt(x.TenderStatusId),
                    TenderStatusName = x.Status.NameAr,
                    TenderIdString = Util.Encrypt(x.TenderId),
                    TenderTypeId = x.TenderTypeId,
                    TenderTypeName = x.TenderType.NameAr,
                    CondetionalBookletPrice = x.ConditionsBookletPrice,
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    ApprovedBy = x.TenderHistories.Where(a => a.StatusId == (int)Enums.TenderStatus.Approved).FirstOrDefault().CreatedBy,
                    SubmitionDate = x.SubmitionDate,
                })
                .ToQueryResult(searchCriteria.PageNumber, searchCriteria.PageSize);
            return result;
        }

        #endregion

        #region Extend offer presentation dates request
        public async Task<SupplierExtendOfferDatesAgencyRequestModel> FindSupplierExtendOfferDatesRequestById(int id)
        {
            Check.ArgumentNotNullOrEmpty(nameof(id), id.ToString());
            var extendOfferDatesModel = (await _context.SupplierExtendOfferDatesRequests
                .Where(s => s.IsActive == true)
                .Where(s => s.AgencyCommunicationRequestId == id)
                .Select(a => new SupplierExtendOfferDatesAgencyRequestModel()
                {
                    CreatedAt = a.CreatedAt,
                    TenderTypeId = a.AgencyCommunicationRequest.Tender.TenderTypeId,
                    CreatedAtString = a.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    SupplierExtendOfferDatesRequestId = a.SupplierExtendOfferDatesRequestId,
                    canApproveExtendOfferPresentation =
                     (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.PreQualificationCommitteeSecretary) && a.AgencyCommunicationRequest.Tender.OffersPreQualificationCommittee.CommitteeUsers.Any(c => c.UserProfileId == _httpContextAccessor.HttpContext.User.UserId())
                     && (a.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.PreQualification || a.AgencyCommunicationRequest.Tender.TenderTypeId == (int)Enums.TenderType.PostQualification))
                     ||
                     (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.DataEntry)
                     && (a.AgencyCommunicationRequest.Tender.TenderTypeId != (int)Enums.TenderType.PreQualification || a.AgencyCommunicationRequest.Tender.TenderTypeId != (int)Enums.TenderType.PostQualification)),
                    TenderId = a.AgencyCommunicationRequest.TenderId,
                    TenderIdString = Util.Encrypt(a.AgencyCommunicationRequest.TenderId),
                    AgencyRequestId = a.AgencyCommunicationRequest.AgencyRequestId,
                    AgencyRequestTypeId = a.AgencyCommunicationRequest.AgencyRequestTypeId,
                    CreatedBy = a.CreatedBy,
                    ExtendOfferDatesRequestReason = a.ExtendOfferDatesRequestReason,
                    ExtendOfferDatesRequestedDate = a.ExtendOfferDatesRequestedDate.HasValue ? a.ExtendOfferDatesRequestedDate.Value.ToString("dd/MM/yyyy HH:mm tt", System.Threading.Thread.CurrentThread.CurrentCulture) : "",
                    RejectionReason = a.AgencyCommunicationRequest.RejectionReason,
                    IsReported = a.AgencyCommunicationRequest.IsReported,
                    StatusId = a.AgencyCommunicationRequest.StatusId
                }).FirstOrDefaultAsync());
            return extendOfferDatesModel;
        }
        public async Task<List<SupplierExtendOfferDatesAgencyRequestModel>> FindSupplierExtendOfferDatesRequestsByTenderId(int tenderId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            var extendOfferDatesModel = (await _context.SupplierExtendOfferDatesRequests
                .Where(s => s.IsActive == true)
                .Where(s => s.AgencyCommunicationRequest.TenderId == tenderId)
                .Select(a => new SupplierExtendOfferDatesAgencyRequestModel()
                {
                    CreatedAt = a.CreatedAt,
                    CreatedAtString = a.CreatedAt.ToString("dd/MM/yyyy", System.Threading.Thread.CurrentThread.CurrentCulture),
                    SupplierExtendOfferDatesRequestId = a.SupplierExtendOfferDatesRequestId,
                    TenderId = a.AgencyCommunicationRequest.TenderId,
                    AgencyRequestId = a.AgencyCommunicationRequest.AgencyRequestId,
                    AgencyRequestTypeId = a.AgencyCommunicationRequest.AgencyRequestTypeId,
                    CreatedBy = a.CreatedBy,
                    ExtendOfferDatesRequestReason = a.ExtendOfferDatesRequestReason,
                    ExtendOfferDatesRequestedDate = a.ExtendOfferDatesRequestedDate.HasValue ? a.ExtendOfferDatesRequestedDate.Value.ToString("dd/MM/yyyy HH:mm tt tt", System.Threading.Thread.CurrentThread.CurrentCulture) : "",
                    RejectionReason = a.AgencyCommunicationRequest.RejectionReason,
                    IsReported = a.AgencyCommunicationRequest.IsReported,
                    StatusId = a.AgencyCommunicationRequest.StatusId
                }).ToListAsync());
            return extendOfferDatesModel;
        }

        public async Task<AgencyCommunicationRequest> GetAgencyCommunicationRequestForSupplierExtendDatesRequest(int agencyRequestId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(agencyRequestId), agencyRequestId.ToString());
            AgencyCommunicationRequest agencyCommunicationRequest =
                await _context.AgencyCommunicationRequests.Where(s => s.IsActive == true && s.AgencyRequestId == agencyRequestId)
                .Include(s => s.SupplierExtendOfferDatesRequest)
                .Include(t => t.Tender)
                .FirstOrDefaultAsync();
            return agencyCommunicationRequest;
        }
        #endregion

        #region Negotiation
        public async Task<NegotiationAgencyFirstStageEditModel> GetNegotiationDatabyId(int tenderId, string CR)
        {
            var CommunicationRequest = await _context.AgencyCommunicationRequests.Include("Negotiations.NegotiationStatus").Include(h => h.Status).Where(s => s.IsActive == true && s.TenderId == tenderId && s.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation).FirstOrDefaultAsync();
            if (!(CommunicationRequest is null) && CommunicationRequest.Negotiations.Where(f => f.NegotiationTypeId == (int)Enums.enNegotiationType.FirstStage).Any())
            {
                var FirstStageNegotiationObj = CommunicationRequest.Negotiations.Where(f => f.NegotiationTypeId == (int)Enums.enNegotiationType.FirstStage).FirstOrDefault();

                var Query = await _context.NegotiationFirstStages.Include(d => d.AgencyCommunicationRequest).Where(w => w.NegotiationId == FirstStageNegotiationObj.NegotiationId).Select(s =>
                             new NegotiationAgencyFirstStageEditModel
                             {
                                 RejectionReason = s.RejectionReason,
                                 Days = ((int)Math.Floor((decimal)s.SupplierReplyPeriodHours) / 24),
                                 Hours = s.SupplierReplyPeriodHours % 24,
                                 NeagotiationReasonText = s.NegotiationReason.Name,
                                 NegotiationReasonId = s.NegotiationReasonId.HasValue ? s.NegotiationReasonId.Value : 0,
                                 NegotiationIdString = Util.Encrypt(s.NegotiationId),
                                 ReductionPercent = s.DiscountAmount,
                                 StatusId = s.StatusId,
                                 ProjectNumber = s.ProjectNumber,
                                 negotiationFirstStageViewModel = new NegotiationAgencyFirstStageViewModel { StatusName = s.NegotiationStatus.Name, RejectionReason = s.RejectionReason, attachmentViewModel = new NegotiationAttachmentViewModel { FileNetReferenceId = s.Attachments.FirstOrDefault().FileNetReferenceId, Name = s.Attachments.FirstOrDefault().Name } },
                                 TenderIdString = Util.Encrypt(tenderId),
                                 EnOperationType = enOperationType.Update
                             }
                                           ).FirstOrDefaultAsync();
                return Query;

            }
            else
                return new NegotiationAgencyFirstStageEditModel { EnOperationType = Enums.enOperationType.Add, IsEditAllowed = true };
        }

        public async Task<CreateNegotiationFirstStageDataModel> GetNegotiationDataForFirstStagebyId(int negotiationFirstStageId)
        {
            var userId = _httpContextAccessor.HttpContext.User.UserId();
            if (!string.IsNullOrEmpty(negotiationFirstStageId.ToString()) && negotiationFirstStageId != 0)
            {
                var Query = await _context.NegotiationFirstStages.Where(w => w.NegotiationId == negotiationFirstStageId).Select(s =>
                             new CreateNegotiationFirstStageDataModel
                             {
                                 RejectionReason = s.RejectionReason,
                                 Days = ((int)Math.Floor((decimal)s.SupplierReplyPeriodHours) / 24),
                                 Hours = s.SupplierReplyPeriodHours % 24,
                                 NeagotiationReasonText = s.NegotiationReason.Name,
                                 NegotiationReasonId = s.NegotiationReasonId.HasValue ? s.NegotiationReasonId.Value : 0,
                                 NegotiationIdString = Util.Encrypt(s.NegotiationId),
                                 DesiredOffersAmount = s.DiscountAmount,
                                 StatusId = s.StatusId,
                                 ProjectNumber = s.ProjectNumber,
                                 LowestOfferValue = s.AgencyCommunicationRequest.Tender.Offers.Where(t => t.OfferStatusId == (int)Enums.OfferStatus.Received && t.IsActive == true && t.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer && t.OfferAcceptanceStatusId == (int)Enums.OfferAcceptanceStatusId.AcceptedOffer)
                    .OrderBy(s => s.FinalPriceAfterDiscount).Select(o => o.FinalPriceAfterDiscount).FirstOrDefault(),
                                 negotiationFirstStageViewModel = new NegotiationAgencyFirstStageViewModel { StatusName = s.NegotiationStatus.Name, RejectionReason = s.RejectionReason, attachmentViewModel = new NegotiationAttachmentViewModel { FileNetReferenceId = s.Attachments.FirstOrDefault().FileNetReferenceId, Name = s.Attachments.FirstOrDefault().Name } },
                                 TenderIdString = Util.Encrypt(s.AgencyCommunicationRequest.TenderId),
                                 EnOperationType = enOperationType.Update,
                                 IsUserHasAccessToLowBudgetTender = (s.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase.HasValue && s.AgencyCommunicationRequest.Tender.IsLowBudgetDirectPurchase == true && s.AgencyCommunicationRequest.Tender.DirectPurchaseMemberId == userId)

                             }).FirstOrDefaultAsync();
                return Query;

            }
            else
                return new CreateNegotiationFirstStageDataModel { EnOperationType = Enums.enOperationType.Add, IsEditAllowed = true };
        }


        public async Task<bool> IsFirstStageNegotiationExist(int tenderId)
        {
            var result = await _context.AgencyCommunicationRequests
                  .Where(d => d.IsActive == true && d.TenderId == tenderId && d.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation
                  && d.Negotiations.Any(f => f.IsActive == true && f.NegotiationTypeId == (int)Enums.enNegotiationType.FirstStage)).AnyAsync();
            return result;
        }


        public async Task<NegotiationAgencyFirstStageEditModel> GetNegotiationFirstStageDatabyId(int negotiationFirstStageId)
        {
            var CommunicationRequest = await _context.NegotiationFirstStages.Include(d => d.Attachments).Include(d => d.AgencyCommunicationRequest).Include(h => h.NegotiationStatus).Where(s => s.NegotiationId == negotiationFirstStageId)
         .Select(s => new NegotiationAgencyFirstStageEditModel
         {
             Days = ((int)Math.Floor((decimal)s.SupplierReplyPeriodHours) / 24),
             Hours = s.SupplierReplyPeriodHours % 24,
             NeagotiationReasonText = s.NegotiationReason.Name,
             NegotiationReasonId = s.NegotiationReasonId.Value,
             NegotiationIdString = Util.Encrypt(s.NegotiationId),
             ReductionPercent = s.DiscountAmount,
             StatusId = s.StatusId,
             ProjectNumber = s.ProjectNumber,
             negotiationFirstStageViewModel = new NegotiationAgencyFirstStageViewModel { StatusName = s.NegotiationStatus.Name, RejectionReason = s.RejectionReason, attachmentViewModel = new NegotiationAttachmentViewModel { FileNetReferenceId = s.Attachments.FirstOrDefault().FileNetReferenceId, Name = s.Attachments.FirstOrDefault().Name } },
             TenderIdString = Util.Encrypt(s.AgencyCommunicationRequest.TenderId),
             EnOperationType = enOperationType.Update
         }).FirstOrDefaultAsync();
            return CommunicationRequest;
        }



        #endregion

        public async Task<List<string>> FindSuppliersThatRejectExtendOfferValidity(int tenderId)
        {
            return await _context.ExtendOffersValiditySuppliers
                .Where(e => e.Offer.TenderId == tenderId && e.IsActive == true)
                .Where(e => e.ExtendOffersValidity.AgencyCommunicationRequest.IsActive == true && e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Rejected)
                .Select(e => e.SupplierCR).ToListAsync();
        }
        public async Task<List<string>> FindSuppliersThatNotResponseToExtendOfferValidity(int tenderId)
        {
            var suppliers = await _context.ExtendOffersValiditySuppliers
                .Where(e => e.Offer.TenderId == tenderId && e.IsActive == true)
                .Where(e => e.ExtendOffersValidity.AgencyCommunicationRequest.IsActive == true && e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.Sent && DateTime.Now.Date > e.ExtendOffersValidity.NewOffersExpiryDate)
                .Select(e => e.SupplierCR).ToListAsync();
            return suppliers;
        }
        public async Task<List<string>> FindSuppliersThatAcceptInitialyExtendOfferValidity(int tenderId)
        {
            var suppliers = await _context.ExtendOffersValiditySuppliers
                .Where(e => e.Offer.TenderId == tenderId && e.IsActive == true)
                .Where(e => e.ExtendOffersValidity.AgencyCommunicationRequest.IsActive == true && e.SupplierExtendOfferValidityStatusId == (int)Enums.SupplierExtendOffersValidityStatus.AcceptedInitially
                && DateTime.Now.Date > e.ExtendOffersValidity.NewOffersExpiryDate)
                .Select(e => e.SupplierCR).ToListAsync();
            return suppliers;
        }

        public async Task<bool> CanAddPostqualificationIfTenderHasExtendOfferValidity(int tenderId)
        {
            var result = await _context.ExtendOffersValiditys
               .Where(e => e.AgencyCommunicationRequest.TenderId == tenderId && e.IsActive == true && e.AgencyCommunicationRequest.IsActive == true)
               .Where(e => e.AgencyCommunicationRequest.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
               .AnyAsync();
            return result;
        }

        public async Task<List<AgencyCommunicationRequest>> GetCommunicationRequestWithNegotiationAndExtendOffersValdityByTenderId(int tenderId)
        {
            List<AgencyCommunicationRequest> communicationRequests = await _context.AgencyCommunicationRequests
                .Include(s => s.Negotiations)
                .Include(s => s.ExtendOffersValidity)
                .Where(x => x.IsActive == true && x.TenderId == tenderId && (x.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.Negotiation || x.AgencyRequestTypeId == (int)Enums.AgencyCommunicationRequestType.ExtendOfferValidtiy)).ToListAsync();
            return communicationRequests;
        }


        public async Task<List<ExtendOffersValiditySupplier>> FindExtendOfferValidtySupplier(int extendOfferValidityId)
        {
            List<ExtendOffersValiditySupplier> extendOffersValiditySuppliers = await _context.ExtendOffersValiditySuppliers
                .Where(x => x.IsActive == true && x.ExtendOffersValidityId == extendOfferValidityId).ToListAsync();
            return extendOffersValiditySuppliers;
        }

        public async Task<bool> IsTenderHasActiveExtendOfferValidityRequests(int tenderId)
        {
            var result = await _context.ExtendOffersValiditys
             .Where(e => e.AgencyCommunicationRequest.TenderId == tenderId && e.IsActive == true && e.AgencyCommunicationRequest.IsActive == true)
             .Where(e => e.AgencyCommunicationRequest.StatusId != (int)Enums.AgencyCommunicationRequestStatus.Finished)
             .AnyAsync();
            return result;
        }
    }
}

