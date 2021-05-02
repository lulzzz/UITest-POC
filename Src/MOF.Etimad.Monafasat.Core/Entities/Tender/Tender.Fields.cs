using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Tender", Schema = "Tender")]

    public partial class Tender : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int TenderId { get; private set; }
        [StringLength(1024)]
        public string TenderName { get; private set; }
        [StringLength(100)]
        public string ReferenceNumber { get; private set; }
        [StringLength(100)]
        public string TenderNumber { get; private set; }
        [StringLength(1024)]
        public string Purpose { get; private set; }
        public decimal? ConditionsBookletPrice { get; private set; }
        public decimal? EstimatedValue { get; private set; }
        public bool? SupplierNeedSample { get; set; }
        [StringLength(2048)]
        public string SamplesDeliveryAddress { get; set; }
        public decimal? InitialGuaranteePercentage { get; set; }
        public DateTime? LastEnqueriesDate { get; private set; }
        public DateTime? LastOfferPresentationDate { get; private set; }
        public DateTime? OffersOpeningDate { get; private set; }
        public bool? InsideKSA { get; private set; }
        [StringLength(5000)]
        public string Details { get; private set; }
        public DateTime? SubmitionDate { get; private set; }
        public bool? IsUnitSecreteryAccepted { get; private set; }
        [StringLength(2000)]
        public string ActivityDescription { get; private set; }
        public bool? TenderAwardingType { get; private set; }
        public DateTime? OffersCheckingDate { get; private set; }
        public bool? AuditorAgree { get; private set; }
        public bool? CompetitionIsBudgeted { get; private set; }
        [StringLength(1024)]
        public string TenderTypeOtherSelectionReason { get; private set; }
        [StringLength(1024)]
        public string InitialGuaranteeAddress { get; private set; }
        public DateTime? AcceptedAnnouncementDate { get; private set; }
        public int? AwardingStoppingPeriod { get; private set; }
        [StringLength(500)]
        public string AwardingReportFileName { get; private set; }
        [StringLength(500)]
        public string AwardingReportFileNameId { get; private set; }
        public int? AgreementMonthes { get; private set; }
        public int? AgreementDays { get; private set; }
        public int? AgreementYears { get; private set; }
        public int? NumberOfWinners { get; private set; }
        public decimal? BonusValue { get; private set; }
        public decimal? AwardingDiscountPercentage { get; private set; }
        public int? AwardingMonths { get; private set; }
        [StringLength(500)]
        public string FinalGuaranteeDeliveryAddress { get; set; }
        public bool? HasGuarantee { get; private set; }
        public bool? HasAlternativeOffer { get; set; }
        public decimal TenderPointsToPass { get; private set; }
        public decimal TechnicalAdministrativeCapacity { get; private set; }
        public decimal FinancialCapacity { get; private set; }
        public int? TemplateYears { get; private set; }
        [StringLength(100)]
        public string BuildingName { get; private set; }
        [StringLength(100)]
        public string FloarNumber { get; private set; }
        [StringLength(100)]
        public string DepartmentName { get; private set; }
        public DateTime? DeliveryDate { get; private set; }
        public bool? CheckingDateSet { get; private set; }
        public bool? FinancialCheckingDateSet { get; private set; }
        public bool? OpeningNotificationSent { get; private set; }
        public bool? CheckingNotificationSent { get; private set; }
        public bool? UnitSpacialistWouldLikeToAttendTheCommitte { get; private set; }
        public int? QuantityTableVersionId { get; set; }
        public decimal? NationalProductPercentage { get; set; }
        public bool? IsSendToEmarketPlace { get; set; }
        public bool? IsNotificationSentForStoppingPeriod { get; set; }
        public bool? IsLowBudgetDirectPurchase { get; set; }
        public bool? IsTenderContainsTawreedTables { get; set; }


        #region Foriegn Keys



        [ForeignKey(nameof(QualificationType))]
        public int? QualificationTypeId { get; private set; }

        [ForeignKey(nameof(TenderCreatedByType))]
        public int? CreatedByTypeId { get; private set; }

        [ForeignKey(nameof(Status))]
        public int TenderStatusId { get; private set; }

        [ForeignKey(nameof(TenderType))]
        public int TenderTypeId { get; private set; }

        [ForeignKey(nameof(TenderConditoinsStatus))]
        [DefaultValue((int)Enums.TenderConditoinsStatus.GeneralStage)]
        public int? ConditionTemplateStageStatusId { get; private set; }

        [ForeignKey(nameof(InvitationType))]
        public int? InvitationTypeId { get; private set; }

        [ForeignKey(nameof(TechnicalOrganization))]
        public int? TechnicalOrganizationId { get; private set; }

        [ForeignKey(nameof(OffersOpeningCommittee))]
        public int? OffersOpeningCommitteeId { get; private set; }

        [ForeignKey(nameof(OffersCheckingCommittee))]
        public int? OffersCheckingCommitteeId { get; private set; }

        [ForeignKey(nameof(DirectPurchaseCommittee))]
        public int? DirectPurchaseCommitteeId { get; private set; }

        [ForeignKey(nameof(VROCommittee))]
        public int? VROCommitteeId { get; private set; }

        [ForeignKey(nameof(OffersPreQualificationCommittee))]
        public int? PreQualificationCommitteeId { get; private set; }

        [ForeignKey(nameof(SpendingCategory))]
        public int? SpendingCategoryId { get; private set; }

        [ForeignKey(nameof(OfferPresentationWay))]
        public int? OfferPresentationWayId { get; private set; }

        [ForeignKey(nameof(ReasonForPurchaseTenderType))]
        public int? ReasonForPurchaseTenderTypeId { get; private set; }

        [ForeignKey(nameof(PreQualification))]
        public int? PreQualificationId { get; private set; }

        [ForeignKey(nameof(PreAnnouncement))]
        public int? PreAnnouncementId { get; private set; }

        [ForeignKey(nameof(AnnouncementSupplierTemplate))]
        public int? AnnouncementTemplateId { get; private set; }

        [ForeignKey(nameof(AgreementType))]
        public int? AgreementTypeId { get; private set; }

        [ForeignKey(nameof(ReasonForLimitedTenderType))]
        public int? ReasonForLimitedTenderTypeId { get; private set; }

        [ForeignKey(nameof(PreviousFramWork))]
        public int? PreviousFramWorkId { get; private set; }

        [ForeignKey(nameof(PostQualificationTender))]
        public int? PostQualificationTenderId { get; private set; }

        [ForeignKey(nameof(TenderFirstStage))]
        public int? TenderFirstStageId { get; private set; }

        [ForeignKey(nameof(OffersOpeningAddress))]
        public int? OffersOpeningAddressId { get; private set; }
        [ForeignKey(nameof(TenderConditionsTemplate))]
        public int? TenderConditionsTemplateId { get; private set; }
        [ForeignKey(nameof(ConditionsBookletDeliveryAddress))]
        public int? ConditionsBookletDeliveryAddressId { get; private set; }
        [StringLength(20)]
        [ForeignKey(nameof(Agency))]
        public string AgencyCode { get; private set; }
        [ForeignKey(nameof(Branch))]
        public int BranchId { get; private set; }
        [ForeignKey(nameof(VRORelatedBranch))]
        public int? VRORelatedBranchId { get; private set; }
        [ForeignKey(nameof(TenderUnitStatus))]
        public int? TenderUnitStatusId { get; private set; }

        [ForeignKey(nameof(UserProfile))]
        public int? DirectPurchaseMemberId { get; set; }


        [ForeignKey(nameof(TenderLocalContent))]
        public int? TenderLocalContentId { get; private set; }


        #endregion
        #endregion
        #region Navigation 

        public TenderDates TenderDates { get; private set; }
        public TenderAddress TenderAddress { get; private set; }
        public TenderLocalContent TenderLocalContent { get; private set; }

        public UserProfile UserProfile { get; private set; }

        public QualificationType QualificationType { get; private set; }
        public TenderCreatedByType TenderCreatedByType { get; private set; }
        public TenderType TenderType { get; private set; }
        public TenderConditoinsStatus TenderConditoinsStatus { get; private set; }
        public TenderStatus Status { get; private set; }
        public SpendingCategory SpendingCategory { get; private set; }
        public InvitationType InvitationType { get; private set; }
        public Committee TechnicalOrganization { get; private set; }
        public Committee OffersOpeningCommittee { get; private set; }
        public Committee OffersCheckingCommittee { get; private set; }
        public Committee DirectPurchaseCommittee { get; private set; }
        public Committee VROCommittee { get; private set; }
        public Committee OffersPreQualificationCommittee { get; private set; }
        public TenderUnitStatus TenderUnitStatus { get; private set; }
        public OfferPresentationWay OfferPresentationWay { get; private set; }
        public Address ConditionsBookletDeliveryAddress { get; private set; }
        public Address OffersOpeningAddress { get; private set; }
        public virtual Tender PreQualification { get; private set; }
        public virtual Announcement PreAnnouncement { get; private set; }
        public virtual AnnouncementSupplierTemplate AnnouncementSupplierTemplate { get; private set; }
        public AgreementType AgreementType { get; private set; }
        public ReasonForPurchaseTenderType ReasonForPurchaseTenderType { get; private set; }
        public ReasonForLimitedTenderType ReasonForLimitedTenderType { get; private set; }
        public virtual Tender PreviousFramWork { get; private set; }
        public virtual Tender PostQualificationTender { get; private set; }
        public virtual Tender TenderFirstStage { get; private set; }
        public GovAgency Agency { get; private set; }
        public Branch Branch { get; private set; }
        public Branch VRORelatedBranch { get; private set; }
        public TenderConditionsTemplate TenderConditionsTemplate { get; private set; }



        public List<SupplierPreQualificationDocument> PreQualificationApplyDocuments { get; private set; } = new List<SupplierPreQualificationDocument>();
        public List<TenderChangeRequest> ChangeRequests { private set; get; } = new List<TenderChangeRequest>();
        public List<TenderArea> TenderAreas { private set; get; } = new List<TenderArea>();
        public List<TenderCountry> TenderCountries { private set; get; } = new List<TenderCountry>();
        public List<FavouriteSupplierTender> FavouriteSupplierTenders { private set; get; } = new List<FavouriteSupplierTender>();
        public List<TenderHistory> TenderHistories { get; private set; } = new List<TenderHistory>();
        public List<TenderMaintenanceRunnigWork> TenderMaintenanceRunnigWorks { private set; get; } = new List<TenderMaintenanceRunnigWork>();
        public List<TenderConstructionWork> TenderConstructionWorks { private set; get; } = new List<TenderConstructionWork>();
        public List<TenderActivity> TenderActivities { private set; get; } = new List<TenderActivity>();
        public List<Offer> Offers { get; private set; } = new List<Offer>();
        public List<TenderAttachment> Attachments { private set; get; } = new List<TenderAttachment>();
        public List<TenderUnitStatusesHistory> TenderUnitStatusesHistories { private set; get; } = new List<TenderUnitStatusesHistory>();
        public List<TenderUnitAssign> TenderUnitAssigns { private set; get; } = new List<TenderUnitAssign>();
        public List<TenderAwardingHistory> TenderAwardingHistories { private set; get; } = new List<TenderAwardingHistory>();
        public List<Invitation> Invitations { get; private set; } = new List<Invitation>();
        public List<UnRegisteredSuppliersInvitation> UnRegisteredSuppliersInvitation { get; private set; } = new List<UnRegisteredSuppliersInvitation>();
        public List<ConditionsBooklet> ConditionsBooklets { get; private set; } = new List<ConditionsBooklet>();
        public List<Enquiry> Enquiries { get; private set; }
        public virtual List<Tender> PostQualifications { get; private set; } = new List<Tender>();
        public List<PostQualificationSuppliersInvitations> PostQualificationInvitations { private set; get; } = new List<PostQualificationSuppliersInvitations>();
        public List<AgencyCommunicationRequest> AgencyCommunicationRequests { private set; get; } = new List<AgencyCommunicationRequest>();
        public List<TenderAgreementAgency> TenderAgreementAgencies { private set; get; } = new List<TenderAgreementAgency>();
        public List<BiddingRound> BiddingRounds { private set; get; } = new List<BiddingRound>();
        public List<TenderQuantityTable> TenderQuantityTables { private set; get; } = new List<TenderQuantityTable>();
        public List<QualificationFinalResult> QualificationFinalResults { get; set; } = new List<QualificationFinalResult>();
        public List<QualificationSubCategoryResult> QualificationSubCategoryResults { get; private set; } = new List<QualificationSubCategoryResult>();
        public List<QualificationCategoryResult> QualificationCategoryResults { get; private set; } = new List<QualificationCategoryResult>();

        public List<QualificationSubCategoryConfiguration> QualificationSubCategoryConfigurations { get; private set; } = new List<QualificationSubCategoryConfiguration>();
        public List<QualificationConfiguration> QualificationConfigurations { get; private set; } = new List<QualificationConfiguration>();
        public List<QualificationSupplierData> QualificationSupplierData { get; private set; } = new List<QualificationSupplierData>();
        public List<AgencyBudgetNumber> AgencyBudgetNumbers { get; private set; }
        public List<TenderVersion> TenderVersions { get; private set; } = new List<TenderVersion>();
        #endregion

        [NotMapped]
        public bool ContainSupply
        {
            get
            {
                return QuantityTableVersionId > (int)Enums.QuantityTableVersion.Version1 &&
                      TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(v => v.TemplateId == (int)TenderActivityTamplate.GeneralSupply || v.TemplateId == (int)TenderActivityTamplate.GeneralMaterials));

            }
        }

        [NotMapped]
        public int ActivityVersionId
        {
            get
            {
                return TenderVersions != null && TenderVersions.Any() ? TenderVersions.FirstOrDefault(d => d.Version.VersionTypeId == (int)Enums.VersionType.TenderActivity).VersionId : 0;
            }
        }

    }
}
