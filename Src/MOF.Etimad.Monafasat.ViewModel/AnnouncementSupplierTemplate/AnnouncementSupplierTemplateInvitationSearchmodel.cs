using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AnnouncementSupplierTemplateInvitationSearchmodel : SearchCriteria
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public int? AnnouncementTemplateId { get; set; }
    }
}
