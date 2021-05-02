using MOF.Etimad.Monafasat.SharedKernal;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllSupplierAnnouncementSearchCriteria : SearchCriteria
    {
        public int? ActivityId { get; set; }
        public int? SubActivityId { get; set; }
        public string AgencyCode { get; set; }
        public int? AreaId { get; set; }
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime? FromLastJoinDate { get; set; }
        public DateTime? ToLastJoinDate { get; set; }
        public string FromLastJoinDateString { get; set; }
        public string ToLastJoinDateString { get; set; }
        public DateTime? FromPublishDate { get; set; }
        public int? AnnouncementPublishDateCriteriaId { get; set; }
        public int? AnnouncementActiveStatusId { get; set; }
        public string CommericalRegisterNo { get; set; }
        public int? TenderTypeId { get; set; }
    }
}
