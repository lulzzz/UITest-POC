using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierGridAnnouncementModel
    {
        public int Id { get; set; }
        public string AnnouncementName { get; set; }
        public string AnnouncementIdString { get; set; }
        public string ReferenceNumber { get; set; }
        public string ActivityDescription { get; set; }
        public bool hasJoinRequest { get; set; }
        public int JoinRequestStatusId { get; set; }
        public string JoinRequestStatusName { get; set; }
        public string announcementStatusName { get; set; }
        public string agencyName { get; set; }
        public string announcementTypeName { get; set; }
        public DateTime? lastApplyDate { get; set; }
        public DateTime? publishDate { get; set; }
        public string publishDateString { get; set; }
        public string lastApplyDateString { get; set; }
        public int announcementPeriod { get; set; }
        public int AnnouncementStatusId { get; set; }
    }
}
