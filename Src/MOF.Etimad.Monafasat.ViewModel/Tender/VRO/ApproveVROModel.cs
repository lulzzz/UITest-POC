using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ApproveVROModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public string OffersOpeningTime { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public string LastOfferPresentationTime { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public int OpenCheckCommitteeId { get; set; }
        public int? TechnicalOrganizationId { get; set; }
        public string OffersOpeningAddress { get; set; }
        public int OffersOpeningAddressId { get; set; }
        public string VerificationCode { get; set; }
        //public bool? SupplierNeedSample { get; set; }
        //public string SamplesDeliveryAddress { get; set; }
    }
}
