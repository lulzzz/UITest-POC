using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementDetailsModel
    {
        public bool CanSendAnnouncementForApproval()
        {
            return true;
        }
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
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IntroAboutTender")]
        public string IntroAboutTender { get; set; }
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
    }
    public class SupplierAnnouncementDetailsModel
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementIdString { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementName")]
        public string AnnouncementName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AgencyName")]
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "BranchName")]
        public string BranchName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "ReferenceNumber")]
        public string ReferenceNumber { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "TenderTypeName")]
        public string TenderTypeName { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IntroAboutTender")]
        public string IntroAboutTender { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Status")]
        public string StatusName { get; set; }
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
        public bool hasJoinRequest { get; set; }
        public int JoinRequestStatusId { get; set; }
        public int AnnouncementStatusId { get; set; }
        public string JoinRequestStatusName { get; set; }
        public string PurchaseMethodName { get; set; }
        public Dictionary<string, string> JoinedSupplierList { get; set; }
        public int? AnnouncementJoinRequestsCount { get; set; }
    }
}
