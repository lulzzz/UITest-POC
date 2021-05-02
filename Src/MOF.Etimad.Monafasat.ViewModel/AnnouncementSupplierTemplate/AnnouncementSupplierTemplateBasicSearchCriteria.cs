using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementSupplierTemplateBasicSearchCriteria : SearchCriteria
    {
        public AnnouncementSupplierTemplateBasicSearchCriteria()
        {
            PageSize = 6;
        }
        public int announcementId { get; set; }
        public string AgencyCode { get; set; }
        public string UserRole { get; set; }
        public string announcementIdString { get; set; }
    }

    public class JoinRequestSuppliersSearchCriteria : SearchCriteria
    {
        public JoinRequestSuppliersSearchCriteria()
        {
            PageSize = 6;
        }
        public int announcementId { get; set; }
        public string announcementIdString { get; set; }
        public string CommericalRegisterName { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string CurrentAgencyCode { get; set; }
        public string UserRole { get; set; }


    }
}
