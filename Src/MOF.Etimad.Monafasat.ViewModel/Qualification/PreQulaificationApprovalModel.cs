namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQulaificationApprovalModel
    {
        public int PreQualificationId { get; set; }
        public string PreQualificationIdString { get; set; }
        public int PreQualificationStatusId { get; set; }
        public string RejectionReason { get; set; }
        public int BranchId { get; set; }
        public string AgencyCode { get; set; }
        public int? PreQualificationTypeId { get; set; }
        public int? QualificationTypeId { get; set; }
        public int? CommitteeId { get; set; }
        public bool? IsLowBudgetTender { get; set; }
        public bool IsUserHasAccessToLowBudgetTender { get; set; }
        public int? DirectPurchaseMemberId { get; set; }
        public int QualificationTenderTypeId { get; set; }
    }
}
