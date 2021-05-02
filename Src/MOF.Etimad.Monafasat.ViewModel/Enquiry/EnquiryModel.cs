using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EnquiryModel
    {
        public int EnquiryId { get; set; }

        public string EnquiryIdString { get; set; }
        [Display(Name = "رقم المنافسة")]
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public int TechnicalCommitteeId { get; set; }
        public string TechnicalCommitteeIdString { get; set; }
        public string TechnicalCommitteeName { get; set; }
        public string SupplierName { get; set; }

        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }

        public int TenderTypeId { get; set; }
        public int TenderStatusId { get; set; }

        [Display(Name = "إسم الإستفسار")]
        public string EnquiryName { get; set; }

        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        public string ReferenceNumber { get; set; }

        [Display(Name = "رقم السجل التجارى")]
        public string CommericalRegisterNo { get; set; }
        public int EnquiryReplyStatusId { get; set; }

        [Display(Name = "حالة الرد على الإستفسار")]
        public string EnquiryReplyStatusName { get; set; }

        [Display(Name = "تاريخ الإرسال")]
        public DateTime? EnquirySendingDate { get; set; }

        public List<EnquiryReplyModel> EnquiryReplies { get; set; }
        public int ReplyCount { get; set; }
        public int PendingEnquiryReplyCount { get; set; }
        public int ApprovedEnquiryReplyCount { get; set; }
        public bool HasInternalComments { get; set; }
        public int UserCommitteeType { get; set; }
        public string TechnicalCommitteeMember { get; set; }

        public bool? IsJoinedCommittee { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
    }
}
