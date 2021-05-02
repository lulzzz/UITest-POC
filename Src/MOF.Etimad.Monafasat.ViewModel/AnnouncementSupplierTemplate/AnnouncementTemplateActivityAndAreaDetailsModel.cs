using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementTemplateActivityAndAreaDetailsModel
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementIdString { get; set; }

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


        public string RequirementConditionsToJoinList { get; set; }
        public string Descriptions { get; set; }

        public string AnnouncementListTypeName { get; set; }


        public bool IsRequiredAttachmentToJoinList { get; set; }
        public string RequiredAttachment { get; set; }
        public bool IsEffectiveList { get; set; } = true;
        public string EffectiveListDate { get; set; }
        public bool IsCreatedByAgncy { get; set; }

        public List<TenderAttachmentModel> Attachments { get; set; }


    }
}
