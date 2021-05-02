using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderDetailsModel
    {
        public bool CanAddPlaint { set; get; }

        public int? CreatedByType { get; set; }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        
        [Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }
        public int? CreatedByTypeId { get; set; }
        public int? PreQualificationId { get; set; }
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        public int? OfferStatusId { get; set; }

        public SupplierInvitationModel SupplierInvitation { get; set; }
        public string AgencyCode { get; set; }
        public int? InvitationStatusId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]

        public int? TechnicalOrganizationId { get; set; }
        public bool IsPurchased { get; set; }
        public bool IsFavouriteTender { get; set; }
        public int SubscriptionTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public Decimal? EstimatedValue { get; set; }
        public string EstimatedValueString { get; set; }
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        //public DateTime? LastEnqueriesDate { get; set; }
        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        //public DateTime? LastOfferPresentationDate { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public bool SupplierHasOffer { get; set; }
        public bool SupplierHasRecievedOrExcludedOffer { get; set; }
        ///////
        ///
        public int InvitationId { get; set; }
        public bool IsSupplierInvited { get; set; }
        public int? InvitationTypeId { get; set; }
        public string InvitationIdString { get; set; }
        public string InvitationStatusString { get; set; }
        public string InvitationTypeIdString { get; set; }

        public bool IsSupplierBlocked { get; set; }
        public bool IsTenderOwner { get; set; }
        //new
        public string TenderTypeName { get; set; }
        public string InvitationTypeName { get; set; }
        public string TechnicalOrganizationName { get; set; }
        public string OffersOpeningCommitteeName { get; set; }
        public string OffersCheckingCommitteeName { get; set; }
        public string DirectPurchaseCommitteeName { get; set; }
        public string VROCommitteeName { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RejectedBy { get; set; }
        public string TenderStatusName { get; set; }
        public string AgencyName { get; set; }
        public string Purpose { get; set; }

        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public Decimal? ConditionsBookletPrice { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public string OffersOpeningAddress { get; set; }
        public int BranchId { get; set; }

        public string RejectionReason { get; set; }
        public bool TenderHasExtendOffersValidity { get; set; }
        public bool? IsUnitSecreteryAccepted { get; set; }
        public int? TenderUnitStatusId { get; set; }
        public string TenderUnitStatusComment { get; set; }
        public bool CanUnitDoAnyAction { get; set; }
        public string UnitRejectionReason { get; set; }
        public string UnitModificationType { get; set; }
        public string UnitNotes { get; set; }
        public List<TenderAttachmentModel> UnitModificatationsFiles { get; set; }

        public string TenderTypeOtherSelectionReason { get; set; }
        public string PreQualificationName { get; set; }
        public string ReasonForPurchaseTenderTypeName { get; set; }
        public string ReasonForLimitedTenderTypeName { get; set; }
        public int? ReasonForPurchaseTenderTypeId { get; set; }
        public int? ReasonForLimitedTenderTypeId { get; set; }
        public string AgreementTypeName { get; set; }
        public string OfferPresentationWay { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public string NeededInitialGurantee { get; set; }
        public string ReferenceNumber { get; set; }
        public string InitialGuaranteeAddress { get; set; }
        public decimal? FinalGuaranteePercentage { get; set; }
        public int? AgreementYears { get; set; }
        public int? AgreementMonthes { get; set; }
        public List<GovAgencyModel> GovAgencies { get; set; }
        public int? NumberOfWinners { get; set; }
        public decimal? BonusValue { get; set; }
        public decimal? EstimateValue { get; set; }
        public decimal TenderInvitationFees { get; set; }
        public decimal TenderPurchaseFees { get; set; }
        public decimal FinancialFees { get; set; }
        public bool CanPurchase { get; set; }
        public string CanBuyBooklet { get; set; }
        public bool IsUnitAgencyCheckNeeded { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]

        public bool OpenCheckCommitteeId { get; set; }
        public List<CommitteeModel> OpenCheckCommittees { set; get; } = new List<CommitteeModel>();
        public List<CommitteeModel> TechnicalCommittees { set; get; } = new List<CommitteeModel>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationEnquiryDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastEnqueriesDate { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterLastOfferPresentationDate")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastOfferPresentationDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterLastOfferPresentationTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningDate")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OffersOpeningDate { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AlternativeOffer")]
        public bool HasAlternativeOffer { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningTime")]
        public string OffersOpeningTime { get; set; }
        public int RemainingDays
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Days : 0;

            }
        }
        public int RemainingHours
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Hours : 0;
            }
        }
        public int RemainingMins
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0;

            }
        }

        public string HasPreQualification { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }
        public bool HasNoTechnical { get; set; }
        public bool BrancHasNoCommittees { get; set; }
        public bool IsSupplierCombined { get; set; }
        public bool ShowInvitationReceipt { get; set; }
        public List<VacationsDateModel> Vacations { get; set; }
        public string DeliveryDate { get; set; }

        public bool TenderTypeMustPurchase
        {
            get
            {
                if (TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.NewTender || TenderTypeId == (int)Enums.TenderType.LimitedTender || TenderTypeId == (int)Enums.TenderType.ReverseBidding || TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || TenderTypeId == (int)Enums.TenderType.FirstStageTender)
                {
                    return true;
                }
                return false;

            }
        }
        public int? AgreementDays { get; set; }

        [Display(Name = nameof(Resources.SharedResources.DisplayInputs.Percentage), ResourceType = typeof(Resources.SharedResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.FieldRequired), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.PercentageOnly))]

        public decimal NationalProductRate { get; set; }

        [Display(Name = nameof(Resources.TenderResources.DisplayInputs.MinimumBaseline), ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]

        public decimal? MinimumBaseline { get; set; }

        [Display(Name = nameof(Resources.TenderResources.DisplayInputs.MinimumPercentageLocalContentTarget), ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
 
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? MinimumPercentageLocalContentTarget { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.NationalProductPercentageAdvantage), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.PercentageOnly))]
        public decimal? NationalProductPercentage { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.HighValueContractsAmmount), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]

        public decimal? HighValueContractsAmmount { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.LocalContentMaximumPercentage), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? LocalContentMaximumPercentage { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.PriceWeightAfterAdjustment), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? PriceWeightAfterAdjustment { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.LocalContentWeightAndFinancialMarket), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? LocalContentWeightAndFinancialMarket { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.BaselineWeight), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? BaselineWeight { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.LocalContentTargetWeight), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? LocalContentTargetWeight { get; set; }

        [Display(Name = nameof(Resources.LocalContentSettingsResources.DisplayInputs.FinancialMarketListedWeight), ResourceType = typeof(Resources.LocalContentSettingsResources.DisplayInputs))]
        [Required(ErrorMessageResourceName = nameof(Resources.SharedResources.ErrorMessages.Required), ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages))]
        [RegularExpression(RegexHelper.PERCENTAGE, ErrorMessageResourceType = (typeof(Resources.LocalContentSettingsResources.DisplayInputs)), ErrorMessageResourceName = nameof(Resources.LocalContentSettingsResources.DisplayInputs.ValueBetweenZeroAnd100))]
        public decimal? FinancialMarketListedWeight { get; set; }
        public bool IsTenderContainsTawreedTables { get; set; }
        public int? TenderLocalContentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<int> LocalContentMechanismIds { get; set; }
        public bool IsTenderNewWithLocalContent { get; set; }

        public bool ContainsSupply { get; set; }
        public bool IsTenderTypeHasNPPrecentage { get; set; }
        public bool IsLinkedToAnnouncement { get; set; }
         public bool ConShowNationalProduct { get; set; }
         public bool ConShowLocalContentValues { get; set; }

        public int? PreAnnouncementId { get; set; }
        public string PreAnnouncementName { get; set; }

        public bool? IsLowBudgetPurchase { get; set; }

        [Display(Name = nameof(Resources.TenderResources.DisplayInputs.DirectPurchaseMemberName), ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string PurchaseMemberName { get; set; }
        public int VersionId { get; set; }
        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }

    }
}
