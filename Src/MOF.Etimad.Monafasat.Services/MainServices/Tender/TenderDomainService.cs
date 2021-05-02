using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration.Enums;
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
    public partial class TenderDomainService : ITenderDomainService
    {
        private IOfferQueries _offerQueries;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INegotiationQueries _negotiationQueries;
        private readonly ICommunicationQueries _communicationQueries;
        private readonly RootConfigurations _rootConfiguration;
        public TenderDomainService(IOfferQueries offerQueries, IHttpContextAccessor httpContextAccessor, INegotiationQueries negotiationQueries, ICommunicationQueries communicationQueries, IOptionsSnapshot<RootConfigurations> rootConfiguration)
        {
            _offerQueries = offerQueries;
            _httpContextAccessor = httpContextAccessor;
            _negotiationQueries = negotiationQueries;
            _communicationQueries = communicationQueries;
            _rootConfiguration = rootConfiguration.Value;
        }


        public void HasAccessToTender(AllBasicTenderDataModel allBasicTenderDataModel)
        {
            if (allBasicTenderDataModel == null)
                throw new AuthorizationException();
        }

        public void IsValidToCreateTender(CreateTenderBasicDataModel tender)
        {
            if (tender.TenderTypeId == (int)Enums.TenderType.NewTender)
            {
                IsValidGeneralTender(tender);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                IsValidDirectPurchaseTender(tender);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.LimitedTender)
            {
                IsValidLimitedTender(tender);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
                IsValidReverseBiddingTender(tender);
            }
            else if (tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                IsValidTwoStagesTender(tender);
            }
        }

        #region  tender validation

        private void NotWhiteSpacesValidation(CreateTenderBasicDataModel tender)
        {
            if (tender.TenderName == null || tender.TenderName.Trim().Length == 0)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.NoSpaceTenderName);
        }

        private void IsValidGeneralTender(CreateTenderBasicDataModel tender)
        {
            NotWhiteSpacesValidation(tender);
            if (tender.ConditionsBookletPrice < 0 || tender.EstimatedValue < 1)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.InvalidNumber);
            if (tender.EstimatedValue < 1)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EstimatedValueCannotBeZero);
        }

        private void IsValidDirectPurchaseTender(CreateTenderBasicDataModel tender)
        {
            NotWhiteSpacesValidation(tender);
            if (tender.ReasonForPurchaseTenderTypeId == null)
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.RequiredTenderTypeSelectionReason);
            if (tender.ReasonForPurchaseTenderTypeId == (int)Enums.ReasonForPurchaseTenderType.Other && tender.TenderTypeOtherSelectionReason == null)
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.RequiredOtherReason);
        }

        private void IsValidLimitedTender(CreateTenderBasicDataModel tender)
        {
            NotWhiteSpacesValidation(tender);
            if (tender.ReasonForLimitedTenderTypeId == null)
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.RequiredTenderTypeSelectionReason);
            if (tender.ReasonForLimitedTenderTypeId == (int)Enums.ReasonForLimitedTenderType.Other && tender.TenderTypeOtherSelectionReason == null)
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.RequiredOtherReason);
            if (tender.ConditionsBookletPrice < 0)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterConditionBook);
        }

        private void IsValidReverseBiddingTender(CreateTenderBasicDataModel tender)
        {
            NotWhiteSpacesValidation(tender);
            if (tender.ConditionsBookletPrice < 0)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterConditionBook);
            if (tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding && tender.EstimatedValue > 50000000)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ReverseTenderEstimateValueLess50Milion);
        }

        private void IsValidTwoStagesTender(CreateTenderBasicDataModel tender)
        {
            NotWhiteSpacesValidation(tender);
        }

        #endregion

 


        public void IsValidToUpdateModel(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender)
                if (string.IsNullOrEmpty(tender.OffersOpeningAddressId.ToString()))
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterOffersOpeningAddress);
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentTender || (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && !string.IsNullOrEmpty(tender.OffersOpeningAddressId.ToString())))
                if (tender.OffersOpeningDate == null)
                    throw new BusinessRuleException("Please enter offers opening date and time");
        }

        public void IsValidToStartCheckingTender(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppenedConfirmed)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOppenedConfirmed));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }
        #region two files tender technical check

        #region Financial stage




        public void IsValidToApproveOrRejectTenderFinancialOpening(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOpenFinancialStagePending)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOpenFinancialStagePending));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToReOpenTenderFinancialOpening(Tender tender, string agencyCode)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersOpenFinancialStageRejected)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersOpenFinancialStageRejected));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        #endregion Financial stage

        #endregion two files tender technical check

        public void IsValidToUpdateTender(Tender tender, string agencyCode)
        {
            Check.BusinessRuleProprtyNotNull(tender, tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification ? Resources.QualificationResources.ErrorMessages.QualificationNotExit : Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyCode)
                throw new UnHandledAccessException();
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.Approved));
                throw new BusinessRuleException(tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification ? Resources.QualificationResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus : Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToUpdateDate(TenderDatesModel tenderDatesModel, Tender tender)
        {
            if (tenderDatesModel == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            }
            CheckTenderDatesNullability(tenderDatesModel);
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                CheckLastEnqueriesDateForDirectPurchase(tenderDatesModel);
                CheckOffersOpeningDateForDirectPurchase(tenderDatesModel);
            }
            else
            {
                CheckLastEnqueriesDate(tenderDatesModel);
                CheckOffersOpeningDate(tenderDatesModel);
                if (tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tender.TenderTypeId == (int)Enums.TenderType.Competition)
                    CheckOffersCheckingDate(tenderDatesModel);
            }

            if (tenderDatesModel.OffersOpeningDate != null)
            {
                Util.DetermineTimesForDates(tenderDatesModel.OffersOpeningDate.Value, (int)Enums.TimeMessagesType.TimeOffersOpeningDate, _rootConfiguration);
            }
            if (tenderDatesModel.LastOfferPresentationDate != null)
            {
                Util.DetermineTimesForDates(tenderDatesModel.LastOfferPresentationDate.Value, (int)Enums.TimeMessagesType.LastOfferPresentationDate, _rootConfiguration);
            }
            if (tenderDatesModel.OffersCheckingDate != null)
            {
                Util.DetermineTimesForDates(tenderDatesModel.OffersCheckingDate.Value, (int)Enums.TimeMessagesType.TimeOfferChecking, _rootConfiguration);
            }
            if (tenderDatesModel.DeliveryDate != null && tenderDatesModel.SupplierNeedSample == true)
            {
                Util.DetermineTimesForDates(tenderDatesModel.DeliveryDate.Value, (int)Enums.TimeMessagesType.SamplesDeliveryTime, _rootConfiguration);
            }

            if (tenderDatesModel.VersionId >= 4 && !tenderDatesModel.IsOldTender)
            {
                CheckAwardingExpectedDate(tenderDatesModel);
                CheckStartingBusinessOrServicesDate(tenderDatesModel);
                if(tenderDatesModel.OffersDeliveryDate != null)
                {
                    Util.DetermineTimesForDates(tenderDatesModel.OffersDeliveryDate.Value, (int)Enums.TimeMessagesType.OffersDeliveryTime, _rootConfiguration);
                }
            }
            if (tenderDatesModel.SupplierNeedSample == true && (tenderDatesModel.SamplesDeliveryAddress == null || tenderDatesModel.SamplesDeliveryAddress.Trim().Length == 0))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.NoSpaceSamplesDeliveryAddress);
            if (tenderDatesModel.OffersOpeningAddressId == null && tenderDatesModel.OffersOpeningAddress == null && !(tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tender.TenderTypeId == (int)Enums.TenderType.Competition))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnterOffersOpeningAddress);
        }

        public void IsValidToUpdateExtendDate(TenderDatesModel tenderDatesModel, Tender tender)
        {
            if (tenderDatesModel == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.PreQualification || tender.TenderTypeId == (int)Enums.TenderType.PostQualification)
            {
                CheckPrequalificationrDatesNullabilityOrEquality(tenderDatesModel, tender);

            }
            else
            {
                CheckTenderDatesNullabilityOrEquality(tenderDatesModel, tender);
            }
            if (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                CheckLastEnqueriesDateForDirectPurchase(tenderDatesModel);
                CheckOffersOpeningDateForDirectPurchase(tenderDatesModel);
            }
            else if (tender.TenderTypeId != (int)Enums.TenderType.PreQualification && tender.TenderTypeId != (int)Enums.TenderType.PostQualification)
            {
                CheckLastEnqueriesDate(tenderDatesModel);
                CheckOffersOpeningDate(tenderDatesModel);
                CheckOffersCheckingDate(tenderDatesModel);
            }
            else
            {
                CheckLastEnqueriesDate(tenderDatesModel);
                CheckOffersCheckingDate(tenderDatesModel);
            }

            if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                Util.DetermineTimesForDates(tenderDatesModel.LastOfferPresentationDate.Value, (int)Enums.TimeMessagesType.TimeOffersOpeningDate, _rootConfiguration);
            if (tenderDatesModel.OffersOpeningDate.HasValue)
                Util.DetermineTimesForDates(tenderDatesModel.OffersOpeningDate.Value, (int)Enums.TimeMessagesType.TimeOffersOpeningDate, _rootConfiguration);
            if (tenderDatesModel.OffersCheckingDate.HasValue)
                Util.DetermineTimesForDates(tenderDatesModel.OffersCheckingDate.Value, (int)Enums.TimeMessagesType.TimeOfferChecking, _rootConfiguration);
        }

        private void CheckTenderDatesNullability(TenderDatesModel tenderDatesModel)
        {
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.LastEnqueriesDate, Resources.TenderResources.ErrorMessages.EnterLastEnqueriesDate);
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.LastOfferPresentationDate, Resources.TenderResources.ErrorMessages.EnterLastOfferPresentationDate);

            Check.BusinessRule(tenderDatesModel.OffersOpeningDate == null && ((tenderDatesModel.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.FirstStageTender && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.Competition && tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentTender) || (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && tenderDatesModel.OffersOpeningAddressId != null)), Resources.SharedResources.ErrorMessages.EnterOffersOpeningDate);

            Check.BusinessRule(tenderDatesModel.OffersCheckingDate == null && (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.Competition), Resources.TenderResources.Messages.EnterOffersCheckingData);
            Check.BusinessRule(tenderDatesModel.OffersCheckingTime == null && (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.Competition), Resources.TenderResources.ErrorMessages.EnterOffersCheckingTime);
        }

        private void CheckTenderDatesNullabilityOrEquality(TenderDatesModel tenderDatesModel, Tender tender)
        {
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.LastEnqueriesDate, Resources.TenderResources.ErrorMessages.EnterLastEnqueriesDate);
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.LastOfferPresentationDate, Resources.TenderResources.ErrorMessages.EnterLastOfferPresentationDate);
            Check.BusinessRule(tenderDatesModel.OffersOpeningDate == null && ((tenderDatesModel.TenderTypeId != (int)Enums.TenderType.NewDirectPurchase && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.FirstStageTender && tenderDatesModel.TenderTypeId != (int)Enums.TenderType.Competition && tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentTender) || (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && tenderDatesModel.OffersOpeningAddressId != null)), Resources.SharedResources.ErrorMessages.EnterOffersOpeningDate);

            Check.BusinessRule(tender.OffersOpeningDate == null && tender.OffersCheckingDate != null && (tenderDatesModel.LastEnqueriesDate.Value.Date == tender.LastEnqueriesDate.Value.Date && tenderDatesModel.LastOfferPresentationDate.Value.Date == tender.LastOfferPresentationDate.Value.Date && tenderDatesModel.OffersCheckingDate.Value.Date == tender.OffersCheckingDate.Value.Date), Resources.TenderResources.ErrorMessages.YouMustChangeOneDateAtLeast);
            Check.BusinessRule(tender.OffersCheckingDate == null && tender.OffersOpeningDate != null && (tenderDatesModel.LastEnqueriesDate.Value.Date == tender.LastEnqueriesDate.Value.Date && tenderDatesModel.LastOfferPresentationDate.Value.Date == tender.LastOfferPresentationDate.Value.Date && tenderDatesModel.OffersOpeningDate.Value.Date == tender.OffersOpeningDate.Value.Date), Resources.TenderResources.ErrorMessages.YouMustChangeOneDateAtLeast);
            Check.BusinessRule(tender.OffersOpeningDate.HasValue && tender.OffersCheckingDate.HasValue && (tenderDatesModel.LastEnqueriesDate.Value.Date == tender.LastEnqueriesDate.Value.Date && tenderDatesModel.LastOfferPresentationDate.Value.Date == tender.LastOfferPresentationDate.Value.Date && tenderDatesModel.OffersOpeningDate.Value.Date == tender.OffersOpeningDate.Value.Date && tenderDatesModel.OffersCheckingDate.Value.Date == tender.OffersCheckingDate.Value.Date), Resources.TenderResources.ErrorMessages.YouMustChangeOneDateAtLeast);

            Check.BusinessRule(tenderDatesModel.LastEnqueriesDate.Value.Date < tender.LastEnqueriesDate.Value.Date || tenderDatesModel.LastOfferPresentationDate.Value.Date < tender.LastOfferPresentationDate.Value.Date, Resources.TenderResources.ErrorMessages.DateMustMoreThanTenderDates);
            Check.BusinessRule(tender.OffersOpeningDate.HasValue && tenderDatesModel.OffersOpeningDate.Value.Date < tender.OffersOpeningDate.Value.Date, Resources.TenderResources.ErrorMessages.DateMustMoreThanTenderDates);
            Check.BusinessRule(tender.OffersCheckingDate.HasValue && tenderDatesModel.OffersCheckingDate.HasValue && tenderDatesModel.OffersCheckingDate.Value.Date < tender.OffersCheckingDate.Value.Date, Resources.TenderResources.ErrorMessages.DateMustMoreThanTenderDates);


            Check.BusinessRule(tenderDatesModel.OffersCheckingDate == null && (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.Competition), Resources.TenderResources.Messages.EnterOffersCheckingData);
            Check.BusinessRule(tenderDatesModel.OffersCheckingTime == null && (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.Competition), Resources.TenderResources.ErrorMessages.EnterOffersCheckingTime);
        }

        private void CheckPrequalificationrDatesNullabilityOrEquality(TenderDatesModel tenderDatesModel, Tender tender)
        {
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.LastEnqueriesDate, Resources.QualificationResources.ErrorMessages.EnterQualificationEnquiryDate);
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.LastOfferPresentationDate, Resources.QualificationResources.ErrorMessages.EnterQualificationDocumentsApplying);
            Check.BusinessRuleProprtyNotNull(tenderDatesModel.OffersCheckingDate, Resources.QualificationResources.ErrorMessages.EnterQualificationDocumentsEvaluationgDate);


            Check.BusinessRule(tender.OffersOpeningDate == null && tender.OffersCheckingDate != null && (tenderDatesModel.LastEnqueriesDate.Value.Date == tender.LastEnqueriesDate.Value.Date && tenderDatesModel.LastOfferPresentationDate.Value.Date == tender.LastOfferPresentationDate.Value.Date && tenderDatesModel.OffersCheckingDate.Value.Date == tender.OffersCheckingDate.Value.Date), Resources.TenderResources.ErrorMessages.YouMustChangeOneDateAtLeast);
            Check.BusinessRule(tender.OffersCheckingDate == null && tender.OffersOpeningDate != null && (tenderDatesModel.LastEnqueriesDate.Value.Date == tender.LastEnqueriesDate.Value.Date && tenderDatesModel.LastOfferPresentationDate.Value.Date == tender.LastOfferPresentationDate.Value.Date /*&& tenderDatesModel.OffersOpeningDate.Value.Date == tender.OffersOpeningDate.Value.Date*/), Resources.TenderResources.ErrorMessages.YouMustChangeOneDateAtLeast);
            Check.BusinessRule(tender.OffersOpeningDate.HasValue && tender.OffersCheckingDate.HasValue && (tenderDatesModel.LastEnqueriesDate.Value.Date == tender.LastEnqueriesDate.Value.Date && tenderDatesModel.LastOfferPresentationDate.Value.Date == tender.LastOfferPresentationDate.Value.Date && /*tenderDatesModel.OffersOpeningDate.Value.Date == tender.OffersOpeningDate.Value.Date && */tenderDatesModel.OffersCheckingDate.Value.Date == tender.OffersCheckingDate.Value.Date), Resources.TenderResources.ErrorMessages.YouMustChangeOneDateAtLeast);
            Check.BusinessRule(tenderDatesModel.LastEnqueriesDate.Value.Date < DateTime.Now || tenderDatesModel.LastOfferPresentationDate.Value.Date < DateTime.Now, Resources.TenderResources.ErrorMessages.DateLessThanToday);

            Check.BusinessRule(tenderDatesModel.LastOfferPresentationDate.Value.Date >= tenderDatesModel.OffersCheckingDate.Value.Date, Resources.QualificationResources.ErrorMessages.InvalidOffersCheckingDate);
            Check.BusinessRule(tenderDatesModel.LastEnqueriesDate.Value.Date < tender.LastEnqueriesDate.Value.Date || tenderDatesModel.LastOfferPresentationDate.Value.Date < tender.LastOfferPresentationDate.Value.Date, Resources.QualificationResources.ErrorMessages.DateMustMoreThanQualificationDates);

            Check.BusinessRule(tender.OffersCheckingDate.HasValue && tenderDatesModel.OffersCheckingDate.Value.Date < tender.OffersCheckingDate.Value.Date, Resources.QualificationResources.ErrorMessages.DateMustMoreThanQualificationDates);
            Check.BusinessRule(tenderDatesModel.OffersCheckingDate == null && (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.Competition || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PostQualification || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PreQualification), Resources.TenderResources.Messages.EnterOffersCheckingData);
            Check.BusinessRule(tenderDatesModel.OffersCheckingTime == null && (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.Competition || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PostQualification || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PreQualification), Resources.TenderResources.ErrorMessages.EnterOffersCheckingTime);
        }

        private void CheckLastEnqueriesDate(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.LastEnqueriesDate.HasValue)
            {
                if (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
                {
                    if (tenderDatesModel.LastEnqueriesDate.Value.Date < DateTime.Now.Date)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.LastEnqueriesDateForPurchase);
                    }
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.LastEnqueriesDate.Value.Date > tenderDatesModel.LastOfferPresentationDate.Value.Date)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.EnquiryDateLargerThanPresentationDateForPurchase);
                        }
                    }
                }
                else
                {
                    if (tenderDatesModel.LastEnqueriesDate.Value.Date <= DateTime.Now.Date)
                    {
                        throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.LastEnqueriesDateLessThanToday);
                    }
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.LastEnqueriesDate.Value.Date >= tenderDatesModel.LastOfferPresentationDate.Value.Date)
                        {
                            throw new BusinessRuleException((tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PostQualification || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PreQualification ? Resources.QualificationResources.ErrorMessages.InvalidLastEnqueriesDate : Resources.TenderResources.ErrorMessages.EnquiryDateLargerThanPresentationDate));
                        }
                    }
                }
            }
        }

        private void CheckAwardingExpectedDate(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.AwardingExpectedDate.HasValue && tenderDatesModel.OffersOpeningDate.HasValue)
            {
                if (tenderDatesModel.AwardingExpectedDate.Value <= tenderDatesModel.OffersOpeningDate.Value)
                {
                    throw new BusinessRuleException("تاريخ الترسية يجب ان يكون اكبر من موعد فتح العروض.");
                }
            }
        }
        private void CheckStartingBusinessOrServicesDate(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.StartingBusinessOrServicesDate.HasValue)
            {
                if (tenderDatesModel.StartingBusinessOrServicesDate.Value <= tenderDatesModel.AwardingExpectedDate.Value)
                {
                    throw new BusinessRuleException("تاريخ بدء الاعمال / الخدمات يجب ان يكون أكبر من موعد الترسية");
                }
            }
        }
        private void CheckLastEnqueriesDateForDirectPurchase(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.LastEnqueriesDate.HasValue)
            {
                if (tenderDatesModel.LastEnqueriesDate.Value.Date < DateTime.Now.Date)
                {
                    throw new BusinessRuleException(" أخر موعد لتقديم الإستفسارات لا يمكن أن يكون أقل من تاريخ اليوم ");
                }
                if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                {
                    if (tenderDatesModel.LastEnqueriesDate.Value.Date > tenderDatesModel.LastOfferPresentationDate.Value.Date)
                    {
                        throw new BusinessRuleException("أخر موعد لتقديم العروض لا يمكن أن يكون أقل من أخر موعد لتقديم الإستفسارات");
                    }
                }
            }
        }

        private void CheckOffersOpeningDate(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentTender || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                if (tenderDatesModel.OffersOpeningDate.HasValue)
                {
                    if (tenderDatesModel.LastEnqueriesDate.HasValue)
                    {
                        if (tenderDatesModel.OffersOpeningDate.Value <= tenderDatesModel.LastEnqueriesDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanEnquiriesDate);
                        }
                    }
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.OffersOpeningDate.Value <= tenderDatesModel.LastOfferPresentationDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanPresentationDate);
                        }
                    }
                }
            }
            else
            {
                if (tenderDatesModel.OffersOpeningDate.HasValue)
                {
                    if (tenderDatesModel.LastEnqueriesDate.HasValue)
                    {
                        if (tenderDatesModel.OffersOpeningDate.Value <= tenderDatesModel.LastEnqueriesDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanEnquiriesDate);
                        }
                    }
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.OffersOpeningDate.Value < tenderDatesModel.LastOfferPresentationDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanPresentationDate);
                        }
                    }
                }
            }
        }

        private void CheckOffersCheckingDate(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                if (tenderDatesModel.OffersCheckingDate.HasValue)
                {
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.OffersCheckingDate.Value < tenderDatesModel.LastOfferPresentationDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.InvalidOffersCheckingDatePurchase);
                        }
                    }
                    if (tenderDatesModel.OffersOpeningDate.HasValue)
                    {
                        if (tenderDatesModel.OffersCheckingDate.Value < tenderDatesModel.OffersOpeningDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CheckingDateGreaterThanOpeiningPurchase);
                        }
                    }
                }
            }
            else
            {
                if (tenderDatesModel.OffersCheckingDate.HasValue)
                {
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.OffersCheckingDate.Value <= tenderDatesModel.LastOfferPresentationDate.Value)
                        {
                            throw new BusinessRuleException((tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PostQualification || tenderDatesModel.TenderTypeId == (int)Enums.TenderType.PreQualification ? Resources.QualificationResources.ErrorMessages.InvalidOffersCheckingDate : Resources.TenderResources.ErrorMessages.InvalidOffersCheckingDate));
                        }
                    }
                    if (tenderDatesModel.OffersOpeningDate.HasValue)
                    {
                        if (tenderDatesModel.OffersCheckingDate.Value <= tenderDatesModel.OffersOpeningDate.Value)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CheckingDateGreaterThanOpeining);
                        }
                    }
                }
            }
        }

        private void CheckOffersOpeningDateForDirectPurchase(TenderDatesModel tenderDatesModel)
        {
            if (tenderDatesModel.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                if (tenderDatesModel.OffersOpeningDate.HasValue)
                {
                    if (tenderDatesModel.LastEnqueriesDate.HasValue)
                    {
                        if (tenderDatesModel.OffersOpeningDate.Value.Date < tenderDatesModel.LastEnqueriesDate.Value.Date)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanEnquiriesDate);
                        }
                    }
                    if (tenderDatesModel.LastOfferPresentationDate.HasValue)
                    {
                        if (tenderDatesModel.OffersOpeningDate.Value.Date < tenderDatesModel.LastOfferPresentationDate.Value.Date)
                        {
                            throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.OpeningDateLessThanPresentationDate);
                        }

                        if ((tenderDatesModel.OffersOpeningDate.Value.Date == tenderDatesModel.LastOfferPresentationDate.Value.Date)
                            && (tenderDatesModel.OffersOpeningDate.Value.TimeOfDay < tenderDatesModel.LastOfferPresentationDate.Value.TimeOfDay))
                        {
                            throw new BusinessRuleException("ساعه فتح العروض يجب أن تكون أكبر من ساعة أخر موعد لتقديم العروض فى نفس اليوم");
                        }
                    }
                }

            }
        }


        public bool CheckIfCanCancelTender(Tender tender)
        {
            if (tender == null)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            }
            int[] allowedCancelStatus = {
                (int)Enums.TenderStatus.Approved,
                (int)Enums.TenderStatus.OffersOppening,
                (int)Enums.TenderStatus.OffersOppenedConfirmed,
                (int)Enums.TenderStatus.OffersOppenedRejected,
                (int)Enums.TenderStatus.OffersOppenedPending,

                (int)Enums.TenderStatus.OffersOpenFinancialStageApproved,
                (int)Enums.TenderStatus.OffersOpenFinancialStage,
                (int)Enums.TenderStatus.OffersTechnicalOppening,
                (int)Enums.TenderStatus.OffersTechnicalChecking,
                (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed,
                (int)Enums.TenderStatus.OffersTechnicalOppeningRejected,
                (int)Enums.TenderStatus.OffersTechnicalOppeningPending,
                (int)Enums.TenderStatus.OffersOpenFinancialStagePending,

                (int)Enums.TenderStatus.OffersCheckedPending,
                (int)Enums.TenderStatus.OffersCheckedConfirmed,
                (int)Enums.TenderStatus.OffersCheckedRejected,
                (int)Enums.TenderStatus.OffersAwarding,
                (int)Enums.TenderStatus.OffersAwardedPending,
                (int)Enums.TenderStatus.OffersAwardedConfirmed,
                (int)Enums.TenderStatus.OffersAwardedRejected,
                (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved,
                (int)Enums.TenderStatus.OffersInitialAwardedPending,
                (int)Enums.TenderStatus.OffersInitialAwardedConfirmed,
                (int)Enums.TenderStatus.OffersInitialAwardedRejected,
                (int)Enums.TenderStatus.DocumentChecking,
                (int)Enums.TenderStatus.DocumentCheckPending,
                (int)Enums.TenderStatus.DocumentCheckConfirmed,
                (int)Enums.TenderStatus.DocumentCheckRejected,
                (int)Enums.TenderStatus.OffersAwardedRejected,
                (int)Enums.TenderStatus.OffersChecking,
                (int)Enums.TenderStatus.OffersTechnicalCheckingPending,
                (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed,
                (int)Enums.TenderStatus.OffersTechnicalCheckingRejected,
                (int)Enums.TenderStatus.OffersTechnicalOppening,
                (int)Enums.TenderStatus.OffersTechnicalOppeningRejected,
                (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed,
                (int)Enums.TenderStatus.OffersFinancialChecking,
                (int)Enums.TenderStatus.OffersOpenFinancialStage,
                (int)Enums.TenderStatus.OffersOpenFinancialStagePending,
                (int)Enums.TenderStatus.OffersOpenFinancialStageRejected,
                (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved,
                (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending,
                (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected,
                (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApproved,
                (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending,
                (int)Enums.TenderStatus.DirectPurchaseOffersCheckingRejected,
                (int)Enums.TenderStatus.DirectPurchaseOffersChecking,
                (int)Enums.TenderStatus.VROOffersTechnicalChecking,
                (int)Enums.TenderStatus.VROOffersCheckingAndTechnicalEval,
                (int)Enums.TenderStatus.VROOffersTechnicalCheckingApproved,
                (int)Enums.TenderStatus.VROOffersTechnicalCheckingPending,
                (int)Enums.TenderStatus.VROOffersTechnicalCheckingRejected,

                (int)Enums.TenderStatus.BiddingOffersCheckedConfirmed,
                (int)Enums.TenderStatus.BiddingOffersCheckedPending,
                (int)Enums.TenderStatus.BiddingOffersCheckedRejected,

                (int)Enums.TenderStatus.VROOffersFinancialChecking,
                (int)Enums.TenderStatus.VROFinancialCheckingOpening,
                (int)Enums.TenderStatus.VROOffersFinancialCheckingPending,
                (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved,
                (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected,
                (int)Enums.TenderStatus.VROOffersFinancialCheckingApproved,
                (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved,
                (int)Enums.TenderStatus.VROOffersFinancialCheckingRejected,
                (int)Enums.TenderStatus.Pending,
                (int)Enums.TenderStatus.QualificationCommitteeApproval,
            };
            if (!allowedCancelStatus.Contains(tender.TenderStatusId))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderActionFailed);
            }
            return true;
        }


        public void IsValidVerificationCode(DateTime expireDate, string verificationCodeInput, string verificationCode)
        {
            if (DateTime.Now > expireDate || verificationCodeInput != verificationCode)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ExpiredActivationCode);
            }
        }

        #region Tender Open Offers

        public void IsValidToGetTenderOfferDetailsForOppeningStage(TenderOffersModel tenderOffersModel, string AgencyId)
        {
            if (tenderOffersModel == null)
                throw new UnHandledAccessException();
            if (tenderOffersModel.AgencyCode != AgencyId)
                throw new UnHandledAccessException();
        }

        #endregion

        public void IsValidToSendToApproveTenderCheckingWithBidding(Tender tender, string agencyId, BiddingDateTimeViewModel biddingDateTimeViewModel)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyId)
                throw new UnHandledAccessException();
            if (biddingDateTimeViewModel == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.BiddingInfomrationIsUnCorrect);
            if (!tender.Offers.All(a => a.IsActive == true && a.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer) && biddingDateTimeViewModel.BiddingStartDateTime <= DateTime.Now)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.BiddingStartTimeMustBeGreaterNow);
            if (!tender.Offers.All(a => a.IsActive == true && a.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.NotIdenticalOffer) && biddingDateTimeViewModel.BiddingStartDateTime >= biddingDateTimeViewModel.BiddingEndDateTime)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.BiddingEndTimeMustBeGreaterBiddingStartTime);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                var tenderStatus = Resources.TenderResources.Messages.ResourceManager.GetString(nameof(Enums.TenderStatus.OffersChecking));
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.CanNotProceedStatusIs + tenderStatus);
            }
        }

        public void IsValidToAddNewBiddingRound(Tender tender, string agencyId, BiddingDateTimeViewModel biddingDateTimeViewModel)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderNotExist);
            if (tender.AgencyCode != agencyId)
                throw new UnHandledAccessException();
            if (biddingDateTimeViewModel == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.BiddingInfomrationIsUnCorrect);
            if (biddingDateTimeViewModel.BiddingStartDateTime <= DateTime.Now)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.BiddingStartTimeMustBeGreaterNow);
            if (biddingDateTimeViewModel.BiddingStartDateTime >= biddingDateTimeViewModel.BiddingEndDateTime)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.BiddingEndTimeMustBeGreaterBiddingStartTime);
        }

        #region Tender Bidding

        public void IsValidToGetTenderBiddings(Tender tender, string cR)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (tender.BiddingRounds == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (!tender.Offers.Any(o => o.IsActive == true && o.CommericalRegisterNo == cR && o.OfferTechnicalEvaluationStatusId == (int)Enums.OfferTechnicalEvaluationStatusId.IdenticalOffer)
            && !_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary))
                throw new AuthorizationException();
        }

        public void IsValidToEndBiddingRound(BiddingRound biddingRound)
        {
            if (biddingRound == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (biddingRound.BiddingRoundStatusId != (int)Enums.BiddingRoundStatus.Stopped)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (biddingRound.StartDate >= DateTime.Now && DateTime.Now < biddingRound.EndDate)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            var minOfferValue = biddingRound.BiddingRoundOffers.Min(o => o.OfferValue);
            if (biddingRound.BiddingRoundOffers.Where(c => c.OfferValue == minOfferValue).Count() > 1)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ToEndThisBidOnlyOneMinumumValueAllowed);
            if (minOfferValue > biddingRound.Tender.EstimatedValue)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MinumumValueMustBeEqualOrGreaterThanExpectedValue);
            if (!_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary))
                throw new UnHandledAccessException();
        }

        public void IsValidToStartNewRouind(Tender tender)
        {
            if (tender == null)
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.TenderBiddingNotExist);
            if (!(_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckManager)))
                throw new UnHandledAccessException();
            if (tender.BiddingRounds != null)
                if (tender.BiddingRounds.FirstOrDefault(r => r.IsActive == true && r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started && r.StartDate >= DateTime.Now && DateTime.Now < r.EndDate) != null)
                    throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.ThereAreAnActiveBidding);
        }

        #endregion Tender Bidding 
        public async Task<List<PostQualificationSuppliersInvitations>> GetPostqualificationOnTender(int tenderId)
        {
            var result = await _offerQueries.GetPostqualificationOnTender(tenderId);
            return result;
        }
        public async Task<bool> IsTenderHasActiveNegotiationRequests(int tenderId)
        {
            bool isTenderHasActiveNegotiationRequest = await _negotiationQueries.IsTenderHasActiveNegotiationRequests(tenderId);
            return isTenderHasActiveNegotiationRequest;
        }
        public async Task<bool> IsTenderHasActiveExtendOfferValidityRequests(int tenderId)
        {
            bool isTenderHasActiveExtendOfferValidityRequests = await _communicationQueries.IsTenderHasActiveExtendOfferValidityRequests(tenderId);
            return isTenderHasActiveExtendOfferValidityRequests;
        }

        public void CheckIfQuantityTablesHasItems(Tender tender)
        {
            if (!tender.TenderQuantityTables.Any(a => a.IsActive == true))
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.YouNeedToInsertAtLeastOneItem);
        }

        public void CheckIfAllE3ashaQuantityTablesHasItems(Tender tender, Func<int, int, Task<List<int>>> GetTemplateFormsByTemplateId)
        {

            var templateIds = tender.TenderActivities.SelectMany(a => a.Activity.ActivityTemplateVersions
           .Where(t => t.VersionId == tender.TenderVersions.FirstOrDefault(v => v.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity).VersionId)
           .Select(v => v.TemplateId.Value)).ToList();

            if (templateIds.Contains((int)Enums.ActivityTemplate.E3asha))
            {
                var formIds = GetTemplateFormsByTemplateId((int)Enums.ActivityTemplate.E3asha, tender.QuantityTableVersionId.Value);
                if (tender.TenderQuantityTables.Where(f => formIds.Result.Contains(f.FormId)).Count() != formIds.Result.Count || tender.TenderQuantityTables.Any(x => formIds.Result.Contains(x.FormId) && !x.QuantitiyItemsJson.TenderQuantityTableItems.Any()))
                {
                    throw new BusinessRuleException("يجب إدخال علي الاقل صنف واحد في جميع جداول الإعاشة");
                }
            }
        }

        public ContractResponseViewModel BuildContractResponseViewModel(Tender tender, string agencyCode)
        {


            if (tender == null)
            {
                return new ContractResponseViewModel
                {
                    StatusCode = ServiceStatusCodes.InvalidTenderReference,
                    StatusDesc = ServiceStatusCodes.FailedString,
                    Message = Resources.MonafasatContract.Messages.ReferenceNumberNotFound,
                    MessageCode = "invalid_tender_reference",
                    canRegisterContract = false
                };
            }


            if (tender.AgencyCode != agencyCode)
            {
                return new ContractResponseViewModel
                {
                    StatusCode = ServiceStatusCodes.InvalidTenderReference,
                    StatusDesc = ServiceStatusCodes.FailedString,
                    Message = Resources.MonafasatContract.Messages.AgencyCodeNotSameAsTender,
                    MessageCode = "invalid_tender_reference",
                    canRegisterContract = false,
                    TenderReferenceNumber = tender.ReferenceNumber
                };
            }

            if (tender.TenderStatusId != (int)Enums.TenderStatus.OffersAwardedConfirmed)
            {
                return new ContractResponseViewModel
                {
                    StatusCode = ServiceStatusCodes.StillInAwardingProcess,
                    StatusDesc = ServiceStatusCodes.FailedString,
                    Message = Resources.MonafasatContract.Messages.NotAwardedStatusYet,
                    MessageCode = "tender_still_in_awarding_process",
                    canRegisterContract = false,
                    TenderReferenceNumber = tender.ReferenceNumber,
                    TenderName = tender.TenderName,
                    AgencyCode = tender.AgencyCode
                };
            }
            if (tender.AwardingStoppingPeriod.HasValue)
            {
                int plaintReviewingPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.PlaintReviewingPeriod);
                int esclationPeriod = int.Parse(_rootConfiguration.PlaintSettingConfiguration.EscalationPeriod);
                if (!tender.isTenderFinalAwarded(esclationPeriod, plaintReviewingPeriod))
                {
                    return new ContractResponseViewModel
                    {
                        StatusCode = ServiceStatusCodes.StillInStoppingPeriod,
                        StatusDesc = ServiceStatusCodes.FailedString,
                        Message = Resources.MonafasatContract.Messages.PlaintStopPeriodNotEndedYet,
                        MessageCode = "tender_still_in_complain_process",
                        canRegisterContract = false,
                        TenderReferenceNumber = tender.ReferenceNumber,
                        TenderName = tender.TenderName,
                        AgencyCode = tender.AgencyCode
                    };
                }
            }


            return new ContractResponseViewModel
            {
                StatusCode = ServiceStatusCodes.TenderCanBeLinked,
                StatusDesc = ServiceStatusCodes.ContractSuccessString,
                Message = Resources.MonafasatContract.Messages.SuccessMessage,
                MessageCode = "tender_valid_can_link",
                canRegisterContract = true,
                TenderReferenceNumber = tender.ReferenceNumber,
                TenderName = tender.TenderName,
                AgencyCode = tender.AgencyCode
            };
        }

        public async Task<List<Offer>> GetReceivedOffersByTenderId(int tenderId)
        {
            List<Offer> offers = await _offerQueries.GetReceivedOffersByTenderId(tenderId);
            return offers;
        }

        public async Task<List<Offer>> GetReceivedAndIdenticalOffersByTenderId(int tenderId)
        {
            List<Offer> offers = await _offerQueries.GetReceivedAndIdenticalOffersByTenderId(tenderId);
            return offers;
        }
        public async Task<List<Offer>> GetNotIdenticalOffersByTenderId(int tenderId)
        {
            List<Offer> offers = await _offerQueries.GetNotIdenticalOffersByTenderId(tenderId);
            return offers;
        }
        public async Task<List<Offer>> GetFinancialAcceeptedOffersByTenderId(int tenderId)
        {
            List<Offer> offers = await _offerQueries.GetFinancialAcceeptedOffersByTenderId(tenderId);
            return offers;
        }
        public async Task<List<Offer>> GetFinancialRejectedOffersByTenderId(int tenderId)
        {
            List<Offer> offers = await _offerQueries.GetFinancialRejectedOffersByTenderId(tenderId);
            return offers;
        }
        public async Task CheckIfUserCanAccessLowBudgetTender(bool? isTenderLowBudget, int? tenderDirectPurchasememberId, int userId)
        {
            if (isTenderLowBudget == true && tenderDirectPurchasememberId != userId)
            {
                throw new AuthorizationException();
            }

        }
    }
}





