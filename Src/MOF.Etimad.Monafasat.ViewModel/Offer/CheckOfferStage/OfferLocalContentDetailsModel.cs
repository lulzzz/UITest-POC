using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferLocalContentDetailsModel
    {
        public int OfferId { get; set; }
        public string OfferIdString { get; set; }
        public string IsSmallOrMediumCorporation { get; set; }
        public string IsIncludedInMoneyMarket { get; set; }
        public string LocalContentPercentage { get;  set; } 
        public string LocalContentDesiredPercentage { get;  set; }
        public string CorporationSizeDate { get; set; }
        public string BaseLineDate { get; set; }
        public string IsIncludedInMoneyMarketDate { get; set; }
        public string LocalContentDesiredPercentageDate { get; set; }
        public string SupplierCr { get; set; }
        public int TenderStatusId { get; set; }
        public bool IsDirectPurchaseMember { get; set; }

        public bool TenderStatusToViewUpdateButton
        {
            get
            {
                return TenderStatusId != (int)Enums.TenderStatus.BackForAwardingFromPlaint
                         && TenderStatusId != (int)Enums.TenderStatus.OffersAwardedConfirmed
                         && TenderStatusId != (int)Enums.TenderStatus.OffersAwardedPending
                         && TenderStatusId != (int)Enums.TenderStatus.OffersAwardedRejected
                         && TenderStatusId != (int)Enums.TenderStatus.OffersAwarding
                         && TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedConfirmed
                         && TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedPending
                         && TenderStatusId != (int)Enums.TenderStatus.OffersInitialAwardedRejected;
            }
        }

    }
}
