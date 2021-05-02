using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQualificationSavingModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationName")]
        //[StringLength(500)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationName")]
        public string TenderName { get; set; }

        public string ReferenceNumber { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationTechnicalCommittee")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersDirectPurchaseCommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PreQualificationCommittee")]
        public int? PreQualificationCommitteeId { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationEnquiryDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "PostQualificationLastEnquiryDate")]
        public DateTime? LastEnqueriesDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationApplyingTime")]
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationLastQualificationPresentationTime")]
        public string LastOfferPresentationTime { get; set; }


        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationDocumentsApplying")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationLastQualificationPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }

        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationEvaluationDateOnly")]
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationDocumentsEvaluationgDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OffersCheckingDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationEvaluationTime")]
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationEvaluationTimeOnly")]
        public string OffersCheckingTime { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }

        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }
        public List<CommitteeModel> PreQualificationCommittees { set; get; }
        public bool BranchHasNoTechnical { get; set; }
        public bool NoTechnicalItems { get; set; }
        public bool IsAgancyHasTechnicalCommittee { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool InsideKSA { get { return ((TenderAreaIDs.Count == 0 && TenderCountriesIDs.Count == 0) || TenderAreaIDs.Count > 0); } }


        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        //[Required]
        public List<int> TenderAreaIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        public List<LookupModel> Areas { get; set; } = new List<LookupModel>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]

        public List<int> TenderCountriesIDs { set; get; } = new List<int>();
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        public List<LookupModel> Countries { get; set; }
        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Details")]
        [StringLength(1024)]
        public string Details { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<int> TenderMentainanceRunnigWorkIDs { set; get; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<int> TenderConstructionWorkIDs { set; get; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<ConstructionWorkModel> ConstructionWorks { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<MaintenanceRunningWorkModel> MaintenanceWorks { get; set; }

        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationActivity")]
        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationActivities")]
        public List<int> TenderActivitieIDs { set; get; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Activities")]
        public List<ActivityModel> Activities { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        [StringLength(2000)]
        public string ActivityDescription { get; set; }

        public string AgencyCode { get; set; }

        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "SupportingDocuments")]
        public List<TenderAttachmentModel> Attachments { get; set; }
        //[Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationDocuments")]
        public string AttachmentReference { get; set; }

        public int RemainingDays { get; set; }
        public int RemainingHours { get; set; }
        public int RemainingMins { get; set; }

        public int branchId { get; set; }
        public List<VacationsDateModel> Vacations { get; set; } = new List<VacationsDateModel>();

        public string CreatedBy { get; set; }

        public int TenderTypeId { get; set; }
        public int ActivityVersionId { get; set; }

        // [Required]
        public int? QualificationTypeId { get; set; }
        public decimal TenderPointsToPass { get; set; }
        public decimal TechnicalAdministrativeCapacity { get; set; }
        public decimal FinancialCapacity { get; set; }


        public QualificationSmallEvaluation QualificationSmallEvaluation { get; set; }
        public QualificationMediumEvaluation QualificationMediumEvaluation { get; set; }
        public QualificationLargeEvaluation QualificationLargeEvaluation { get; set; }
        public List<int> ActivitiesWithYears { get; set; }


    }
}
