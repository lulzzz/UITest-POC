using MOF.Etimad.Monafasat.SharedKernel;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SolidarityInvitedModel
    {
        public string TenderIdString { get; set; }
        public string offerIdEncrypted { get; set; }
        public string CrNumber { get; set; }
        public string CrName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Enums InvitationTypeId { get; set; }
        public string Description { get; set; }
        public bool InCreation { get; set; }
        public Enums.InvitationStatus InvitationStatus { get; set; }
    }


    public class SolidarityInvitationModel
    {
        public string offerIdEncrypted { get; set; }
        public string CrNumber { get; set; }
        public string CrName { get; set; }
        public string Description { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public Enums.UnRegisteredSuppliersInvitationType SolidarityInvitationType { get; set; }
    }

}
