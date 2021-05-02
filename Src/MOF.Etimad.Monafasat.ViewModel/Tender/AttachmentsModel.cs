using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AttachmentsModel
    {
        public int TablesCount;

        public int TenderID { set; get; }
        public string TenderIDString { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBooklet")]
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterConditionsBookletAttachments")]
        public string BookletReference { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AttachmentsMainConditions")]
        public string AttachmentReference { get; set; }
        public int TenderStatusId { get; set; }
        public string AgencyCode { get; set; }
        public List<string> BookletReferenceLst { get; set; }
        public List<string> AttachmentReferenceLst { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public int TenderTypeId { get; set; }
        public bool IsAgencyRelatedByVRO { get; set; }
        public bool IsExceptedAgency { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public int? QuantityTableVersionId { get; set; }
        public int? InvitationTypeId { get; set; }
        public int BranchId { get; set; }
        public int? TenderCreatedByTypeId { get; set; }
        public int? PreQualificationId { get; set; }

        public List<TenderAttachmentsModelChanges> AttachmentsChanges = new List<TenderAttachmentsModelChanges>();
    }
}
