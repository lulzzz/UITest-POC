using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EnquiryReplyModel
    {
        [Display(Name = "رقم الرد على الإستفسار")]
        public int EnquiryReplyId { get; set; }
        public int AgencyCommunicationRequestId { get; set; }
        public int TenderTypeId { get; set; }
        public int CommitteeId { get; set; }
        public int CommitteeUserId { get; set; }
        public int BranchId { get; set; }
        public string CommitteeName { get; set; }
        public string EnquiryReplyIdString { get; set; }

        [Display(Name = "رقم الإستفسار")]
        public int EnquiryId { get; set; }

        [Display(Name = "الإستفسار")]
        public string EnquiryName { get; set; }
        public string TenderPurpose { get; set; }

        [Display(Name = "نص الرد على الإستفسار")]
        public string EnquiryReplyMessage { get; set; }

        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }

        [Display(Name = "رقم المنافسة")]
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }

        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }

        [Display(Name = "رقم السجل التجارى")]
        public string CommericalRegisterNo { get; set; }

        [Display(Name = "حالة الرد")]
        public string EnquiryReplyStatusName { get; set; }

        public int EnquiryReplyStatusId { get; set; }

        [Display(Name = "تاريخ إرسال الإستفسار")]
        public DateTime EnquirySendingDate { get; set; }

        [Display(Name = "تاريخ الرد")]
        public DateTime? EnquiryReplyDate { get; set; }
        [Display(Name = "تاريخ الانشاء")]
        public DateTime? CreationDate { get; set; }
        public int? TechnicalCommitteeId { get; private set; }

        public string TechnicalCommitteeMemberName { get; set; }
        public bool IsComment { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }

    }
}
