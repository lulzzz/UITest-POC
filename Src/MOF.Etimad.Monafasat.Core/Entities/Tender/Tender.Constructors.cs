using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class Tender
    {
        #region Constructors====================================================

        public Tender() { }

        public Tender(string agencyCode, int branchId, int tenderType, int? invitationType, string tenderName, string tenderNumber, string purpose, int? announcementTemplateId,
            int? technicalOrganizationId, int? offersOpeningCommitteeId, int? offersCheckingCommitteeId, int? directPurchaseCommitteeId, int? vROCommitteeId,
            int? reasonForPurchaseTenderTypeId, int? reasonForLimitedTenderTypeId, string tenderTypeOtherReason, int? preQualificationId, int? preAnnouncementId, int offerPresentationWayId,
            decimal? estimatedValue, decimal? conditionsBookletPrice, int userId, int? agreementMonthes, int? agreementYears, int? agreementDays, int? agreementTypeId, IList<string> agenciesIds,
            int? numberOfWinnder, decimal? bonusValue, int? tenderCreatedByTypeId, string initialGuaranteeAddress,
            bool isRelatedToVro, bool? supplierNeedSample, string samplesDeliveryAddress, int? quantityTableVersionId, List<AgencyBudgetNumber> agencyBudgetNumbers = null, bool? isLowBudgetDirectPurchase = null, int? directPurchaseCommitteeMemberId = null)
        {
            AgencyCode = agencyCode;
            BranchId = branchId;
            TenderTypeId = tenderType;
            InvitationTypeId = invitationType;
            TenderName = tenderName;
            TenderNumber = tenderNumber;
            TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            Purpose = purpose;
            AnnouncementTemplateId = announcementTemplateId;
            InitialGuaranteeAddress = initialGuaranteeAddress;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersOpeningCommitteeId = offersOpeningCommitteeId;
            OffersCheckingCommitteeId = offersCheckingCommitteeId;
            DirectPurchaseCommitteeId = directPurchaseCommitteeId;
            VROCommitteeId = vROCommitteeId;
            AddActionHistory((int)Enums.TenderStatus.UnderEstablishing, TenderActions.CreateTender, "", userId);
            ConditionsBookletPrice = tenderType == (int)Enums.TenderType.CurrentDirectPurchase || tenderType == (int)Enums.TenderType.NewDirectPurchase || tenderType == (int)Enums.TenderType.Competition || tenderType == (int)Enums.TenderType.NationalTransformationProjects ? 0 : conditionsBookletPrice;
            ReasonForPurchaseTenderTypeId = reasonForPurchaseTenderTypeId;
            ReasonForLimitedTenderTypeId = reasonForLimitedTenderTypeId;
            TenderTypeOtherSelectionReason = tenderTypeOtherReason;
            PreQualificationId = preQualificationId;
            OfferPresentationWayId = isLowBudgetDirectPurchase == true ? (int)Enums.OfferPresentationWayId.OneFile : offerPresentationWayId;
            EstimatedValue = estimatedValue;
            AgreementMonthes = agreementMonthes;
            AgreementYears = agreementYears;
            AgreementDays = agreementDays;
            AgreementTypeId = agreementTypeId;
            UpdateBeneficiaryAgencies(agenciesIds);
            NumberOfWinners = numberOfWinnder;
            BonusValue = bonusValue;
            CreatedByTypeId = tenderCreatedByTypeId;
            if (isRelatedToVro || tenderType == (int)Enums.TenderType.NationalTransformationProjects)
            {
                SupplierNeedSample = supplierNeedSample;
                SamplesDeliveryAddress = samplesDeliveryAddress;
            }
            AgencyBudgetNumbers = agencyBudgetNumbers;
            QuantityTableVersionId = quantityTableVersionId;
            PreAnnouncementId = preAnnouncementId;
            IsLowBudgetDirectPurchase = isLowBudgetDirectPurchase;
            DirectPurchaseMemberId = directPurchaseCommitteeMemberId;
            EntityCreated();
        }


        // add post Qualification 
        public Tender(int tenderID, int tenderType, string tenderName, int? technicalOrganizationId, int? offersCheckingCommitteeId, int? offersDirectPurchaseCommitteeId
            , DateTime lastEnqueriesDate, DateTime lastOfferPresentationDate, DateTime? offersCheckingDate, DateTime? offersOpeningDate
       , int tenderStatusId, string agencyCode, int branchId, int tenderPostQualificationId, int? preQualificationCommitteeId, bool? isLowBudgetDirectPurchase = null, int? directPurchaseCommitteeMemberId = null)
        {
            TenderId = tenderID;
            TenderTypeId = tenderType;
            TenderName = tenderName;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersCheckingCommitteeId = offersCheckingCommitteeId;
            DirectPurchaseCommitteeId = offersDirectPurchaseCommitteeId;

            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            OffersCheckingDate = offersCheckingDate;
            OffersOpeningDate = offersOpeningDate;
            TenderStatusId = tenderStatusId;
            AgencyCode = agencyCode;
            BranchId = branchId;
            PostQualificationTenderId = tenderPostQualificationId;
            PreQualificationCommitteeId = preQualificationCommitteeId;
            IsLowBudgetDirectPurchase = isLowBudgetDirectPurchase;
            DirectPurchaseMemberId = directPurchaseCommitteeMemberId;
            if (tenderID == 0)
                EntityCreated();
            else
                EntityUpdated();
        }


        public void SetPointToPassToQualification(decimal pointToPass, decimal technicalAdministrativeCapacity, decimal financialCapacity, int? qualificationTypeId)
        {
            this.TenderPointsToPass = pointToPass;
            this.TechnicalAdministrativeCapacity = technicalAdministrativeCapacity;
            this.FinancialCapacity = financialCapacity;
            this.QualificationTypeId = qualificationTypeId;
        }
        public void SetPointToPassToPostQualification(decimal pointToPass, decimal technicalAdministrativeCapacity, decimal financialCapacity, int? qualificationTypeId)
        {
            this.TenderPointsToPass = pointToPass;
            this.TechnicalAdministrativeCapacity = technicalAdministrativeCapacity;
            this.FinancialCapacity = financialCapacity;
            this.QualificationTypeId = qualificationTypeId;
            EntityUpdated();
        }


        #endregion

        #region Seoncd-Stage-Constractor
        public Tender(int tenderTypeId, string tenderName, string tenderNumber, string purpose, int? technicalOrganizationId, int? offersCheckingCommitteeId,
         int? offersOpeningCommitteeId, int offerPresentationWayId, int? tenderFirstStageId, string agencyCode, int branchId, decimal? estimatedValue, int? quantitiyTableVersionId
         , string intialGurantteeAddress, decimal? initialGuaranteePercentage, List<AgencyBudgetNumber> agencyBudgetNumbers = null)
        {
            TenderTypeId = tenderTypeId;
            TenderName = tenderName;
            TenderNumber = tenderNumber;
            Purpose = purpose;
            TechnicalOrganizationId = technicalOrganizationId;
            OffersCheckingCommitteeId = offersCheckingCommitteeId;
            OffersOpeningCommitteeId = offersOpeningCommitteeId;
            OfferPresentationWayId = offerPresentationWayId;
            InitialGuaranteeAddress = intialGurantteeAddress;
            InitialGuaranteePercentage = initialGuaranteePercentage;
            TenderFirstStageId = tenderFirstStageId;
            AgencyCode = agencyCode;
            BranchId = branchId;
            EstimatedValue = estimatedValue;
            TenderStatusId = (int)Enums.TenderStatus.UnderEstablishing;
            InvitationTypeId = (int)Enums.InvitationType.Specific;
            AgencyBudgetNumbers = agencyBudgetNumbers;
            QuantityTableVersionId = quantitiyTableVersionId;
            EntityCreated();
        }

        public void SetOfferPresenationDate_ForTest()
        {
            this.LastOfferPresentationDate = DateTime.Now.AddDays(1);
        }

        public void SetTenderName_ForTest(string tenderName)
        {
            this.TenderName = tenderName;
        }
        #endregion Seoncd-Stage-Constractor
    }
}
