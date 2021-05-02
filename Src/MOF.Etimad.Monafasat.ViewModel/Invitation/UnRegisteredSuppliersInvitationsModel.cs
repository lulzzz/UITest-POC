using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UnRegisteredSuppliersInvitationsModel
    {
        public string TenderIdString { get; set; }
        public int TenderId { get; set; }
        public string CrNumber { get; set; }
        public string CrName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int InvitationTypeId { get; set; }
        public string Description { get; set; }
        public bool InCreation { get; set; }
        public Enums.InvitationStatus InvitationStatus { get; set; }
    }
}
