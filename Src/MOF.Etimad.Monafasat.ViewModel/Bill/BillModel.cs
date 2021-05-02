using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BillViewModel
    {
        public string BillInvoiceNumber { get; set; }
    }

    public class BillInfoModel
    {
        public int RequestType { get; set; }
        public int? BillId { get; set; }
        public int AgencyType { get; set; }
        public int? ConditionBookletId { get; set; }
        public string BillInvoiceNumber { get; set; }
        public decimal AmountDue { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime ExpDate { get; set; }
        public string BillReferenceInfo { get; set; }
        public string ChapterNumber { get; set; }
        public string BankBranchId { get; set; }
        public string SectionId { get; set; }
        public string SequenceNumber { get; set; }
        public string NumOfSubDepartments { get; set; }
        public string NumOfSubSections { get; set; }
        public string BatchId { get; set; }
        public string AgencyCode { get; set; }
        public string DisplayLabelAr { get; set; }
        public string DisplayLabelEn { get; set; }
        public string ActionReason { get; set; }
        public int? ActionStatus { get; set; }
        public int BillStatusId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public bool? IsActive { get; set; }

        public int? InvitationId { get; set; }

    }
}
