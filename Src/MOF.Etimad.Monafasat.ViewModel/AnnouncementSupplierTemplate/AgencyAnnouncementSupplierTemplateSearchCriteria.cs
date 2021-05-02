using MOF.Etimad.Monafasat.SharedKernal;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AgencyAnnouncementSupplierTemplateSearchCriteria : SearchCriteria
    {
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public int? StatusId { get; set; }
        public string AgencyCode { get; set; }
        public DateTime? FromPublishDate { get; set; }
        public DateTime? ToPublishDate { get; set; }
        public string FromPublishDateString { get; set; }
        public string ToPublishDateString { get; set; }
        public int JoinedCount { get; set; }
        public string CurrentAgencyCode { get; set; }
        public int? BranchId { get; set; }
        public int? CurrentBranchId { get; set; }
        public string CurrentRoleName { get; set; }
        public string Type { get; set; }
    }
}
