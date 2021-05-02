using System;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OpeningSupplierGridData
    {
        public string CommericalRegisterName { get; set; }
        public string CommericalRegisterNo { get; set; }
        public int OfferId { get; set; }
        public decimal? FinalPriceAfterDiscount { get; set; }
        public string InvitationStatus { get; set; }
        public int OfferStatusId { get; set; }
        public int CombinedCount { get; set; }
        public string CombinedIdString { get; set; }
        public DateTime? InvitationDate { get; set; }
        public string OfferIdString { get; set; }
        public int? OfferPresentationWay { get; set; }
        public string OfferStatusName { get; set; }
        public int TenderStatusId { get; set; }
        public string MainActivity { get; set; }

    }
}
