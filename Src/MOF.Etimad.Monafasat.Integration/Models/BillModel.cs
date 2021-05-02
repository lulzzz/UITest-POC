using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class BillModel
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
        public List<string> BillInvoiceNumbers { get; set; }
        public string ActionReason { get; set; }
        public int? ActionStatus { get; set; }
        public List<TenderFinantialCostModel> TenderFees = new List<TenderFinantialCostModel>();
    }


    public class NewBillModel
    {
        public decimal AmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpDate { get; set; }
        public string AgencyCode { get; set; }
        public int AgencyType { get; set; }
        public string SupplierNameAr { get; set; }
        public string SupplierNameEn { get; set; }
        public string AgencyJebayaCodeForSaddad { get; set; } //041001000000002000
        public string billRefIdForBillingForSaddad { get; set; } //Monafasat
        public List<RevenueEntryInfoType> RevList { get; set; }
        public BenAcctInfoType BenAcctInfo { get; set; }
        public string ActionReason { get; set; }
        public int? ActionStatus { get; set; }
        public string ClientKey { get; set; }
        public string BillInvoiceNumber { get; set; }
        public string sadadInvoiceNumber { get; set; }

        public int StatusCode { get; set; }
    }


    public class BillModelForUpdateRequest
    {

        public DateTime ExpDate { get; set; }
        public string sadadInvoiceNumber { get; set; }
        public int StatusCode { get; set; }
        public string ClientKey { get; set; }
    }

    public class BillModelForCancelRequest
    {
        public string ActionReason { get; set; }
        public string sadadInvoiceNumber { get; set; }
        public int StatusCode { get; set; }
        public string ClientKey { get; set; }
    }

    public class RevenueEntryInfoType
    {
        public string BenAgencyId { get; set; }
        public string GFSCode { get; set; }
        public decimal Amt { get; set; }
    }

    public partial class BenAcctInfoType
    {
        public string AgencyId { get; set; }
        public string ChapterNum { get; set; }
        public string BankBranchId { get; set; }
        public string SectionId { get; set; }
        public string SequenceNumber { get; set; }
        public string NumOfSubDepartments { get; set; }
        public string NumOfSubSections { get; set; }
    }
}
