namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllAnnouncementSupplierTemplateAgencyModel
    {
        public int AnnouncementId { get; set; }
        public string AnnouncementIdString { get; set; }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public int StatusId { get; set; }
        public string StatusString { get; set; }
        public int? AnnouncementTemplateSuppliersListTypeId { get; set; }
        public string AnnouncementTemplateSuppliersListTypeString { get; set; }
        public string MainActivity { get; set; }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public int JoinRequestsCount { get; set; }
        public int AcceptedJoinRequestsCount { get; set; }
        public int UsageCount { get; set; }
        public string PublishedDateString { get; set; }
        public bool isCreator { get; set; }
        public bool CanAddAnnouncementListToAgency { get; set; }
        public bool CanRemoveAnnouncementListFromAgency { get; set; }
        public bool IsReadyForApproval { get; set; }
        public bool canCancel { get; set; }
        public bool CanReviewJoinRequests { get; set; }
        public bool isUserAgency { get; set; }
        public bool CanEditAnnouncementTemplate { get; set; }

        public bool CanExtendAnnouncementTemplate { get; set; }

    }
}
