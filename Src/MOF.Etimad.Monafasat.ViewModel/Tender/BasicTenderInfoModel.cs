namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BasicTenderInfoModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        public int? InvitationTypeId { get; set; }
        public int TenderStatusId { get; set; }
        public int? PreQualificationId { get; set; }
        public bool HasInvitaitons { get; set; }
    }
}
