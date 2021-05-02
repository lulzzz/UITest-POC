using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementSuppliersTemplateJoinRequestsDetailsModel
    {
        public string AnnouncementIdString { get; set; }
        public string AnnouncementName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.AnnouncementSupplierTemplateResources.ErrorMessages)), ErrorMessageResourceName = "EnterJoinRequestResult")]
        [Display(ResourceType = typeof(Resources.AnnouncementSupplierTemplateResources.DisplayInputs), Name = "JoinRequestResult")]
        public int RequestResultId { get; set; }

        public int AnnouncementId { get; set; }
        public int UserId { get; set; }
        public int JoinRequestId { get; set; }
        public string JoinRequestIdString { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialName")]
        public string CommericalRegisterName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialNumber")]
        public string CommericalRegisterNo { get; set; }
        public int JoinRequestStatusId { get; set; }
        public string JoinRequestStatusName { get; set; }
        public string RejectionReason { get; set; }
        public string DeleteReason { get; set; }
        public string TemplateExtendMechanism { get; set; }
        public string StopUsingAnnouncemetMechanism { get; set; }

        public string Notes { get; set; }

        public string JoinRequestSendingDate { get; set; }
        public string WithdrawalDate { get; set; }

        public List<TenderAttachmentModel> Attachments { get; set; }

        public string AttachmentReference { get; set; }
        public bool IsOwnerAgency { get; set; }
    }
}
