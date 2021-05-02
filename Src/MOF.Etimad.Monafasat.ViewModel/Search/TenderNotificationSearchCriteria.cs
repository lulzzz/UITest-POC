using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderNotificationSearchCriteria : SearchCriteria
    {
        public int tId { get; set; }
        public string Key { get; set; }
        public string Email { get; set; }
        public string OperationCode { get; set; }
        public int SendingStatusId { get; set; }
        public string tenderId { get; set; }
        public string tenderName { get; set; }
    }
}
