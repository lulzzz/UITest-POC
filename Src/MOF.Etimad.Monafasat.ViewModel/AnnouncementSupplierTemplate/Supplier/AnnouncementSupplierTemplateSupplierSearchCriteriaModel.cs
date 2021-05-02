using MOF.Etimad.Monafasat.SharedKernal;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementSupplierTemplateSupplierSearchCriteriaModel : SearchCriteria
    {
        public int? ActivityId { get; set; }
        public int? SubActivityId { get; set; }
        public string AgencyCode { get; set; }
        public int? AreaId { get; set; }

        // Advance Search Criteria
        public string AnnouncementName { get; set; }
        public string ReferenceNumber { get; set; }
        public int? AnnouncementPublishedDateDaysId { get; set; }
        public int? AnnouncementApplyingStatusId { get; set; }
        public int? AnnouncementTabId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }
        public DateTime? FromPublishDate { get; set; }
    }
}
