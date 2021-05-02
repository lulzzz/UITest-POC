namespace MOF.Etimad.Monafasat.SharedKernel
{
    public class PolicyNames
    {
        [PermissionCaption("إضافة تأهيل سابق")]
        public const string AddPreQualificationPolicy = "AddPreQualificationPolicy";
        public const string PreQualificationApprovalPolicy = "PreQualificationApprovalPolicy";
        public const string CreatePostQualificationPolicy = "CreatePostQualificationPolicy";
        public const string PostQualificationApprovalPolicy = "PostQualificationApprovalPolicy";
        public const string SendQualificationForApprovePolicy = "SendQualificationForApprovePolicy";
        public const string SendQualificationForApproveToCommitteeManagerPolicy = "SendQualificationForApproveToCommitteeManagerPolicy";
        public const string ApproveQualificationPolicy = "ApproveQualificationPolicy";
        public const string ApproveQualificationFromCommitteeManagerPolicy = "ApproveQualificationFromCommitteeManagerPolicy";
        public const string RejectApproveQualificationPolicy = "RejectApproveQualificationPolicy";
        public const string RejectApproveQualificationFromCommitteManagerPolicy = "RejectApproveQualificationFromCommitteManagerPolicy";
        public const string ReopenQualificationPolicy = "ReopenQualificationPolicy";
        public const string ReopenQualificationFromCommitteeSecrtraryPolicy = "ReopenQualificationFromCommitteeSecrtraryPolicy";
        public const string QualificationPagingPolicy = "QualificationPagingPolicy";
        // Mandatory List
        public const string AddMandatoryListPolicy = "AddMandatoryListPolicy";
        public const string ViewMandatoryListPolicy = "ViewMandatoryListPolicy";
        public const string EditMandatoryListPolicy = "EditMandatoryListPolicy";
        public const string MandatoryListApprovalPolicy = "MandatoryListApprovalPolicy";
        // End of mandatory list

        #region Announcement

        public const string CreateAnnouncementPolicy = "CreateAnnouncementPolicy";
        public const string SendAnnouncementForApprovalPolicy = "SendAnnouncementForApprovalPolicy";
        public const string ApproveAnnouncementPolicy = "ApproveAnnouncementPolicy";
        public const string RejectApproveAnnouncementPolicy = "RejectApproveAnnouncementPolicy";
        public const string ReopenAnnouncementPolicy = "ReopenAnnouncementPolicy";
        public const string DeleteAnnouncementPolicy = "DeleteAnnouncementPolicy";
        #endregion


        public const string CreateAnnouncementTemplatePolicy = "CreateAnnouncementTemplatePolicy";
        public const string ApproveAnnouncementTemplatePolicy = "ApproveAnnouncementTemplatePolicy";
        public const string UpdateAnnouncementSuppliersTemplatePolicy = "UpdateAnnouncementSuppliersTemplatePolicy";
        public const string DetailsAnnouncementSuppliersTemplatePolicy = "DetailsAnnouncementSuppliersTemplatePolicy";
        public const string DeleteAnnouncementTemplatePolicy = "DeleteAnnouncementTemplatePolicy";
        public const string SupplierViewCancelationReasonAnnouncementTemplatePolicy = "SupplierViewCancelationReasonAnnouncementTemplatePolicy";
        public const string CancelAnnouncementTemplatePolicy = "CancelAnnouncementTemplatePolicy";
        public const string ExtendAnnouncementSuppliersTemplatePolicy = "ExtendAnnouncementSuppliersTemplatePolicy";
        public const string SearchAnnouncementJoinedSuppliersTemplatePolicy = "SearchAnnouncementJoinedSuppliersTemplatePolicy";


        public const string PrintAnnouncementSuppliersTemplatePolicy = "PrintAnnouncementSuppliersTemplatePolicy";
        public const string EditAnnouncementSuppliersTemplatePolicy = "EditAnnouncementSuppliersTemplatePolicy";
        public const string ReviewAnnouncementTemplateJoinRequestPolicy = "ReviewAnnouncementTemplateJoinRequestPolicy";

        #region Supplier Tamplate

        [PermissionCaption("قوائم اعلانات االموردين")]
        public const string GetAllAnnouncementSupplierTemplatesForSupplierPolicy = "GetAllAnnouncementSupplierTemplatesForSupplierPolicy";
        #endregion
    }
}
