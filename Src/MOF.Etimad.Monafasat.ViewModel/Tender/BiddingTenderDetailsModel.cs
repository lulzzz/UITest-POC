using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BiddingTenderDetailsModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public string TenderRefrenceNumber { get; set; }
        public string TenderName { get; set; }
        public int OfferPlaceInBidding { get; set; }

        public string BiddingRoundIdString { get; set; }

        public string BiddingRoundStartTime { get; set; }
        public string BiddingRoundEndTime { get; set; }
        public string biddingRoundDuration { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }
        public int BiddingRoundDaysFromStart { get; set; }
        public int BiddingRoundHoursFromStart { get; set; }
        public int BiddingRoundMinutesFromStart { get; set; }
        public int BiddingRoundDaysToEnd { get; set; }
        public int BiddingRoundHoursToEnd { get; set; }
        public int BiddingRoundMinutesToEnd { get; set; }
        public List<BiddingRoundOfferModel> BiddingRoundOffers { get; set; }
        public int TenderStatusId { get; set; }
        public decimal LastLeastBiddingValue { get; set; }
        public bool CanStartNewRound { get; set; }
        public bool CanEndBidding { get; set; }
        public string BiddingStartDate { get; set; }
        public int ActiveSupplierCount { get; set; }
        public bool HasPreviousRounds { get; set; }
        public string RoundIndexLabel { get; set; }
        public bool InsideKSA { get; set; }
        public string ExectutionPlace { get; set; }
        public decimal EstimateValue { get; set; }
    }

    public class BiddingRoundOfferModel
    {
        public int OfferId { get; set; }
        public decimal OfferValue { get; set; }
        public string CommercialNumber { get; set; }
        public string CommercailName { get; set; }
    }
}
