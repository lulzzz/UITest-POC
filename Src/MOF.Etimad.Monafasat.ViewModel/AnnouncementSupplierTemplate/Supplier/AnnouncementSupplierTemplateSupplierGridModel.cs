namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementSupplierTemplateSupplierGridModel
    {
        public string AnnouncementIdString { get; set; }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public int StatusId { get; set; }
        public string StatusString { get; set; }
        public int? AnnouncementTemplateSuppliersListTypeId { get; set; }
        public string AnnouncementTemplateSuppliersListTypeString { get; set; }
        public string MainActivity { get; set; }
        public string AgencyCode { get; set; }
        public string AgencyName { get; set; }
        public string LastDateForJoinRequests { get; set; }
        public string PublishedDateString { get; set; }
        public string tenderTypeString { get; set; }
        public bool HasApprovedOrPendingRequest { get; set; }
        public bool IsValidAnnouncement { get; set; }
    }
}
