using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierAnnouncementSearchCriteria : SearchCriteria
    {
        public string CommericalRegisterNo { get; set; }

        public string ReferenceNumber { get; set; }
        public string AnnouncementName { get; set; }

        public string AgencyCode { get; set; }
        public int? TenderTypeId { get; set; }
    }
}
