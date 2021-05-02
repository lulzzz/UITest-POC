using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderAttachmentModel
    {
        public int TenderId { get; set; }
        public string STenderId { get; set; }
        public int TenderTypeId { get; set; }
        public int? TenderAttachmentId { get; set; }

        public string Name { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public int AttachmentTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }
        public int TenderStatusId { get; set; }
        public int TenderActivityVersionId { get; set; }
        public int TenderChangeRequestId { get; set; }
        public int? ConditionTemplateStageStatusId { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public string RejectionReason { get; set; }
        public int InvitationStatusId { get; set; }
        public int InvitationTypeId { get; set; }

        public bool? IsPurchased { get; set; }

        public string FileNetReferenceId { get; set; }

        public List<TenderOldAttachmentsModel> OldAttachments = new List<TenderOldAttachmentsModel>();
       
        public List<TenderAttachmentsModelChanges> AttachmentsChanges = new List<TenderAttachmentsModelChanges>();
    }
    public class TenderAttachmentsModelChanges
    {
        public int AttachmentId { get; set; }
        public int TenderId { get; set; }
        public string Name { get; set; }
        public int AttachmentTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }
        public int TenderStatusId { get; set; }
        public int? DeletedAttachmentId { get; set; }
        public string FileNetReferenceId { get; set; }
        public string AttachmentTypeName { get; set; }
    }
    public class TenderOldAttachmentsModel
    {
        public int TenderId { get; set; }
        public string Name { get; set; }

        public int AttachmentTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }
        public int TenderStatusId { get; set; }
        public int ChangeStatusId { get; set; }
        public int? ReviewStatusId { get; set; }

        public string FileNetReferenceId { get; set; }
        public bool canView { get; set; }
    }
}
