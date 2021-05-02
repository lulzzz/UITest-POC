using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementSuppliersTemplateDetailsModel
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementIdString { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementName")]
        public string AnnouncementName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AgencyName")]
        public string AgencyName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "BranchName")]
        public string BranchName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "ReferenceNumber")]
        public string ReferenceNumber { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "TenderTypeName")]
        public string TenderTypeName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IntroAboutAnnouncementTemplate")]
        public string IntroAboutAnnouncementTemplate { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Status")]
        public string StatusName { get; set; }
        public int StatusId { get; set; }
        public string RejectionReason { get; set; }
        public string PublishDateHijriString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
            }
        }
        public string PublishDateString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToGregorianDate("dd/MM/yyyy") : "";
            }
        }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "CreatedBy")]
        public string CreatedBy { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AuditedBy")]
        public string AuditedBy { get; set; }
        public int? AnnouncementPeriod { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string LastApplyingRequestsDateString
        {
            get
            {
                return PublishedDate.HasValue && AnnouncementPeriod.HasValue ? PublishedDate.Value.AddDays(AnnouncementPeriod.Value).ToGregorianDate("dd/MM/yyyy") : "";
            }
        }
        public string LastApplyingRequestsHijriDateString
        {
            get
            {
                return PublishedDate.HasValue && AnnouncementPeriod.HasValue ? PublishedDate.Value.AddDays(AnnouncementPeriod.Value).ToHijriDateWithFormat("dd/MM/yyyy") : "";
            }
        }
        public bool IsEndedAnnouncement
        {
            get
            {

                return PublishedDate.HasValue && AnnouncementPeriod.HasValue ? (PublishedDate.Value.AddDays(AnnouncementPeriod.Value) < DateTime.Now ? true : false) : false;

            }
        }
        public string InsideKSAString
        {
            get
            {
                return InsideKSA.HasValue && InsideKSA.Value ? Resources.TenderResources.DisplayInputs.InsideKSA : Resources.TenderResources.DisplayInputs.OutsideKSA;
            }
        }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "InsideKSA")]
        public bool? InsideKSA { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Details")]
        public string Details { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Areas")]
        public List<LookupModel> Areas { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Countries")]
        public List<LookupModel> Countries { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "ConstructionWorks")]
        public List<ConstructionWorkModel> ConstructionWorks { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<MaintenanceRunningWorkModel> MaintenanceOperationActions { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "TenderActivities")]
        public List<ActivityModel> TenderActivities { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "ActivityDescription")]
        public string ActivityDescription { get; set; }
        public bool HasJoinRequest { get; set; }
        public int JoinRequestStatusId { get; set; }
        public string JoinRequestStatusName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "JoinRequestsCount")]
        public int? AnnouncementJoinRequestsCount { get; set; }
        public string TenderTypeIdString { get; set; }
        public string PurchaseMethodName { get; set; }
        public bool IsAnnouncementLinkedToTender { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "PreferredTenderTypeString")]
        public string PreferredTenderTypeString { get; set; }
        public Dictionary<string, string> JoinedSupplierList { get; set; }
        public string AgencyCode { get; set; }



        public string AgencyAddress { get; set; }
        public string AgencyPhone { get; set; }
        public string AgencyFax { get; set; }
        public string AgencyEmail { get; set; }

        public string RequirementConditionsToJoinList { get; set; }
        public string Descriptions { get; set; }

        public string AnnouncementListTypeName { get; set; }


        public bool IsRequiredAttachmentToJoinList { get; set; }
        public string RequiredAttachment { get; set; }
        public bool IsEffectiveList { get; set; } = true;
        public string EffectiveListDate { get; set; }

        public List<TenderAttachmentModel> Attachments { get; set; }

        public bool CanEditAnnouncementTemplate { get; set; }

        public string CancelationReasonDescription { get; set; }
        public bool IsCreatedByAgncy { get; set; }


    }



    public class AnnouncementSuppliersTemplateCancelModel
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementIdString { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.AnnouncementResources.ErrorMessages)), ErrorMessageResourceName = "CancelationReasonAnnouncementRequired")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "CancelationReason")]
        public string CancelationReason { get; set; }
        public string VerificationCode { get; set; }
        public string ReferenceNumber { get; set; }

        public string CreatedBy { get; set; }
        public string IntroAboutAnnouncementTemplate { get; set; }

        public string AnnouncementName { get; set; }
        public string PublishDateHijriString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToHijriDateWithFormat("dd/MM/yyyy") : "";
            }
        }
        public string PublishDateString
        {
            get
            {
                return PublishedDate.HasValue ? PublishedDate.Value.ToGregorianDate("dd/MM/yyyy") : "";
            }
        }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public string Details { get; set; }
        public string StatusName { get; set; }
        public string TenderTypeName { get; set; }
        public string AnnouncementListTypeName { get; set; }
        public DateTime? PublishedDate { get; set; }
        public bool IsEffectiveList { get; set; } = true;
        public string EffectiveListDate { get; set; }
        public string Descriptions { get; set; }
        public bool IsCreatedByAgncy { get; set; }




    }

}
