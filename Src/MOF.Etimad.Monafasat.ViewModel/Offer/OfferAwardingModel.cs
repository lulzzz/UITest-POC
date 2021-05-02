using MOF.Etimad.Monafasat.ViewModel.Offer;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferAwardingModel
    {
        public int TenderId { get; set; }
        public int OfferId { get; set; }
        public bool? TenderAwardingType { get; set; }//نوع الترسية كاملة أو جزئية
        public string AwardingComments { get; set; } //مبررات التوصية
        public int? AwardingStoppingPeriod { get; set; }
        public string AwardingReportFileName { get; set; }
        public string AwardingReportFileNameId { get; set; }
        public decimal? AwardingDiscountPercentage { get; set; }
        public string FinalGuaranteeDeliveryAddress { get; set; }

        public int? AwardingMonths { get; set; }
        public bool? HasGuarantee { get; set; }
        public List<OfferFinancialDetailsModel> OfferAwardingList { get; set; }
        public string ExclusionReason { get; set; }
        public string CommericalRegisterNo { get; set; }

        public string offerIdString { get; set; }
    }

    public class ExclusionReasonAwardingModel
    {
        public string ExclusionReason { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string offerIdString { get; set; }
        public int OfferId { get; set; }
    }
}
