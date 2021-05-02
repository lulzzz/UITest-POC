using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQualificationDetailsModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationPrice")]
        public Decimal ConditionsBookletPrice { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InitiatorName")]
        public string InitiatorName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationNumber")]
        public string QualificationNumber { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ReferenceNumber")]
        public string ReferenceNumber { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationName")]
        public string TenderName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationStatus")]
        public int TenderStatusId { get; set; }

        public int TenderTypeId { get; set; }

        public string TenderStatusIdString { get; set; }
        [Required]
        [StringLength(1024, ErrorMessageResourceName = "PurposeValiation", ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), MinimumLength = 40)]

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int TechnicalOrganizationId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        public int? DirectPurchaseCommitteeId { get; set; }

        public string DirectPurchaseCommitteName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime CreatedDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PublishedBy")]
        public string CreatedBy { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationEvaluationDate")]
        public DateTime OffersCheckingDate { get; private set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationEvaluationTime")]
        public string OffersCheckingTime { get; set; }
        public string TenderStatusName { get; set; }
        public string TechnicalOrganizationName { get; set; }
        public string OffersCheckingCommitteeName { get; set; }
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }
        public string CheckingCommitteeName { get; set; }
        public string SubmitionDateString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        public string ActivityDescription { get; set; }


        public string Details { get; set; }

        public DateTime CurrentDate { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }

        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        public string Createby { get; set; }
        public ClaimsPrincipal User { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }

        public string SubmitionDateHijri { get; set; }
        public string CreatedDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersCheckingDateHijri { get; set; }

        //public List<AttachmentModel> attachments { set; get; }

        public List<TenderAttachmentModel> Attachments { get; set; }


        public int RemainingDays { get; set; }
        public int RemainingHours { get; set; }
        public int RemainingMins { get; set; }

        public List<string> TenderActivities { get; set; }
        public List<string> MaintenanceOperationActions { get; set; }
        public List<string> EstablishingActions { get; set; }

        #region Area

        public List<AreaModel> Areas { get; set; }
        public List<CountryModel> Countries { get; set; }



        #endregion
        public string LastOfferPresentationDateString { get; set; }
        public string LastOfferPresentationDateHijriString { get; set; }
        public string LastEnqueriesDateString { get; set; }
        public string LastEnqueriesDateHijriString { get; set; }
        public string OffersCheckingDateHijriString { get; set; }
        public string OffersCheckingDateString { get; set; }
        #region news
        public List<QualifiqationNewsModel> QualifiqationNews { get; set; }
        #endregion
        public int ChangeRequestTypeId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public string RejectionReason { get; set; }

        public PostQualificationRelatedTenderDetailsModel PostQualificationRelatedTenderDetailsModel { get; set; }

        public List<SupplierPreQualificationDocumentModel> SupplierPreQualificationDocumentModel { get; set; }


    }
}
