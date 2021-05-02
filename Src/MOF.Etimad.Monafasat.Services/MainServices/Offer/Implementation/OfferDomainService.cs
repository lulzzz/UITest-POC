using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class OfferDomainService : IOfferDomainService
    {
        private readonly IAppDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenderAppService _tenderAppService;
        private readonly ITenderQueries _tenderQueries;
        private readonly ITenderCommands _tenderCommands;
        private readonly RootConfigurations _rootConfiguration;



        public OfferDomainService(IAppDbContext context, IHttpContextAccessor httpContextAccessor, ITenderAppService tenderAppService, ITenderQueries tenderQueries, ITenderCommands tenderCommands, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
            _tenderAppService = tenderAppService;
            _tenderQueries = tenderQueries;
            _tenderCommands = tenderCommands;
            _rootConfiguration = rootConfiguration.Value;

        }
        public async Task IsValidToCreate(Offer offer)
        {
            var tender = await context.Tenders.FirstOrDefaultAsync(x => x.TenderId.Equals(offer.TenderId));
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderStatusIsNotApproved);
            if (tender.LastOfferPresentationDate.HasValue && tender.LastOfferPresentationDate.Value.Date < DateTime.Today)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderLastPresentationDatePassed);
        }

        public void IsValidToSend(Offer offer, Tender tender)
        {
            IsValid(offer, tender);
            if (tender.Status.TenderStatusId != (int)Enums.TenderStatus.Approved)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderStatusIsNotApproved);
        }
        private Offer IsValid(Offer offer, Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            var _offer = tender.Offers.FirstOrDefault(x => x.OfferId.Equals(offer.OfferId));
            if (_offer == null)
                throw new Exception(Resources.OfferResources.ErrorMessages.OfferNotExist);
            return _offer;
        }
        public void IsValidAndSent(Offer offer, Tender tender)
        {
            var _offer = IsValid(offer, tender);
            if (tender.Status.TenderStatusId != (int)Enums.TenderStatus.OffersOppening)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderStatusIsNotApproved);
            if (offer.OfferStatusId != (int)Enums.OfferStatus.Received)
                throw new BusinessRuleException(Resources.OfferResources.DisplayInputs.OfferStatus +
                    $" {Enum.Parse(typeof(Enums.OfferStatus), _offer.OfferStatusId.ToString())} " + Resources.OfferResources.ErrorMessages.OfferNotRecievedForOffer);
        }


        #region QuantityTable
        public void IsValidToUpdateTwoFilesFinancialDetails(Tender tender, Offer offer, TenderTowFilesFinancialDetailsDetails model)
        {
            if (offer == null)
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.OfferNotExist);
            if (string.IsNullOrEmpty(model.FinantialOfferLetterNumber) && model.IsFinaintialOfferLetterAttached)
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.OfferLetterNumberMustHaveAValue);
            if (model.FinantialOfferLetterDate == DateTime.MinValue && model.IsFinaintialOfferLetterAttached)
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.OfferLetterDateMustHaveAValue);
            if (!(tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage || tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderMustBeInTechnicalCheckingStageOrInFinancailCheckingStage);
        }

        public void IsValidToUpdateTwoFilesTechnicalCheck(Tender tender, Offer offer, OfferDetailsForCheckingTwoFilesModel model)
        {
            if (offer == null)
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.OfferNotExist);

            if (!(tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderMustBeInOfferCheckingStageOrOpenOffersConfirmed);
        }

        #endregion QuantityTable


        public void IsValidAddBidding(Tender tender, List<Offer> offers, int biddingRoundId, decimal biddingValue, string cR)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (tender.BiddingRounds.OrderByDescending(o => o.BiddingRoundId).FirstOrDefault(r => r.StartDate <= DateTime.Now && DateTime.Now < r.EndDate) == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (tender.BiddingRounds.OrderByDescending(o => o.BiddingRoundId).FirstOrDefault(r => r.StartDate <= DateTime.Now && DateTime.Now < r.EndDate).BiddingRoundId != biddingRoundId)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (biddingValue <= 0)
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.BiddingValueMustBeGreaterThanZero);
            if (!(offers.Any(o => o.IsActive == true && o.CommericalRegisterNo == cR && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
                || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary)))
                throw new UnHandledAccessException();
            if (tender.BiddingRounds.Any(b => b.BiddingRoundOffers.Any(o => o.OfferValue <= biddingValue && o.IsActive == true) && b.IsActive == true))
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.BiddingValueMustBeLessThanLastBidding);
        }

        public void IsVaildToAcceptOrRejectCombinedInvitation(OfferSolidarity combined)
        {
            if (combined.StatusId != (int)Enums.SupplierSolidarityStatus.ToBeSent)
            {
                throw new BusinessRuleException(Resources.OfferResources.ErrorMessages.CanNotAcceptCombinedInvitation);
            }
        }

        public async Task<Tender> UpdateTenderAwardingTypeAndStoppingPeriodAndAwardingReport(int tenderId, bool? awardingType, int? StoppingPeriod, string awardingFileName, string awardingFileNameId, bool? hasGuarantee, decimal? awardingPercentage, int? monthCount, string finalGuaranteeDeliveryAddress)
        {
            DateTime hayzNafazDate = _rootConfiguration.OldFlow.EndDate.ToDateTime();

            Check.ArgumentNotNullOrEmpty(nameof(tenderId), tenderId.ToString());
            Tender tenderObject = await _tenderQueries.FindTenderByTenderId(tenderId);
            IsValidToUpdateTenderAwardingDetails(tenderObject, StoppingPeriod, awardingFileName, awardingFileNameId);
            tenderObject.UpdateTenderAwardingType(tenderObject.TenderId, awardingType);
            if (((tenderObject.TenderTypeId == (int)Enums.TenderType.Competition || tenderObject.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) && tenderObject.SubmitionDate < hayzNafazDate)
                || !(tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentTender || tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderObject.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects || tenderObject.TenderTypeId == (int)Enums.TenderType.Competition || tenderObject.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase))
            {
                tenderObject.UpdateAwardingStoppingPeriod(StoppingPeriod);
            }
            if (!(tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentTender || tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderObject.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects))
            {
                tenderObject.UpdateAwardingReport(awardingFileName, awardingFileNameId);
                tenderObject.UpdateTenderAwardingNotification(hasGuarantee, awardingPercentage, monthCount, finalGuaranteeDeliveryAddress);
            }
            return await _tenderCommands.UpdateAsync(tenderObject);
        }

        private void IsValidToUpdateTenderAwardingDetails(Tender tenderObject, int? stoppingPeriod, string awardingFileName, string awardingFileNameId)
        {
            DateTime hayzNafazDate = _rootConfiguration.OldFlow.EndDate.ToDateTime();

            if (tenderObject == null)
                throw new AuthorizationException();
            if (tenderObject.TenderStatusId != (int)Enums.TenderStatus.OffersAwarding)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersAwarding));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }

            if (((tenderObject.TenderTypeId == (int)Enums.TenderType.Competition || tenderObject.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase) && tenderObject.SubmitionDate < hayzNafazDate)
               || !(tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentTender || tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderObject.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects || tenderObject.TenderTypeId == (int)Enums.TenderType.Competition || tenderObject.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase))
            {
                if (!(stoppingPeriod >= 5 && stoppingPeriod <= 10))
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.AwardingStoppingMustBeEqualOrGreaterThen5);
            }
            if (!(tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentTender || tenderObject.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderObject.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                && (string.IsNullOrEmpty(awardingFileName) || string.IsNullOrEmpty(awardingFileNameId)))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustIncludeAwardingReport);
            }
        }

    }
}
