namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderCancelModel
    {

        public string TenderIdString { get; set; }
        public int TenderId { get; set; }
        public string CancelationReasonDescription { get; set; }
        public int? CancelationReasonId { get; set; }
        public string VerificationCode { get; set; }

    }
}
