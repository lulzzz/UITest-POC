using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class VROTendersCreatedByGovAgencyModel
    {
        public string TenderReferenceNumber { get; set; }

        public int TenderStatusId { get; set; }
        public string TenderIdString { get; set; }
        public string AgencyIdString { get; set; }
        public string BranchIdString { get; set; }
        public Decimal? ConditionsBookletPrice { get; set; }

        public string TenderName { get; set; }

        public string TenderNumber { get; set; }


        public DateTime? SubmitionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime? LastEnqueriesDate { get; set; }

        public string LastOfferPresentationTime { get; set; }
        public string OffersOpeningTime { get; set; }
        public string OffersCheckingTime { get; set; }


        public DateTime? OffersOpeningDate { get; set; }
        public DateTime? OffersCheckingDate { get; set; }
        public string TenderTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        public DateTime? LastOfferPresentationDate { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }

        public string SubmitionDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }

        public string OffersCheckingDateHijri { get; set; }


        public List<TenderHistoryModel> TenderHistoryModels { get; set; }
        public int RemainingDays
        {
            get
            {
                return (LastOfferPresentationDate.HasValue && LastOfferPresentationDate.Value > DateTime.Now) ? (LastOfferPresentationDate.Value - DateTime.Now).Days : 0;
            }
        }
        public int RemainingHours
        {
            get
            {
                return (LastOfferPresentationDate.HasValue && LastOfferPresentationDate.Value > DateTime.Now) ? (LastOfferPresentationDate.Value - DateTime.Now).Hours : 0;
            }
        }
        public int RemainingMins
        {
            get
            {
                return (LastOfferPresentationDate.HasValue && LastOfferPresentationDate.Value > DateTime.Now) ? (LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0;
            }
        }

    }
}
