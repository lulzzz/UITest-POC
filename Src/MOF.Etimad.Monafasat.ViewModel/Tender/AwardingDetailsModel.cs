using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AwardingDetailsModel
    {
        public int OffersCount { get; set; }
        public decimal? AverageOffersValue { get; set; }
        public List<string> SupplierNames { get; set; }
        public bool? AwardingType { get; set; }
        public bool IsSupplierFailedInFinancialEvaluation { get; set; }
        public bool IsSupplierFailedInTechnicalEvaluation { get; set; }
        public bool IsValidToApplyOfferLocalContent { get; set; }
        public bool IsNotBindedToMandatoryList { get; set; }
        public bool IsNotBindedToTheLowestBaseLine { get; set; }
        public bool IsNotBindedToTheLowestLocalContent { get; set; }
        public bool IsSupplierFailedInPostQualifacatoin { get; set; }
        public bool IsSupplierRejectExtendOfferValidity { get; set; }
        public bool IsSupplierNotRespontExtendOfferValidity { get; set; }
        public bool IsSupplierNotAttcheFileInExtendOfferValidity { get; set; }
        public bool IsAwardedSupplier { get; set; }
        public string ExclusionReason { get; set; }
        public List<AwardedOffersDataModel> AwardedOffers { get; set; } = new List<AwardedOffersDataModel>();
    }
    public class AwardedOffersDataModel
    {
        public string cr { get; set; }
        public string SupplierName { get; set; }
        public decimal? OfferValue { get; set; }
    }

}
