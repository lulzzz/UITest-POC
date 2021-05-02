using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderRevisionDateModel
    {
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        public string LastEnqueriesDateString { get; set; }
        public string LastEnqueriesDateHijriString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public string LastOfferPresentationDateString { get; set; }
        public string LastOfferPresentationDateHijriString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        public DateTime? OffersOpeningDate { get; set; }
        public string OffersOpeningDateString { get; set; }
        public string OffersOpeningDateHijriString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningTime")]
        public string OffersOpeningTime { get; set; }
        public string CreatedBy { get; set; }
        //public BasicTenderModel Tender { get; set; }
        public TenderChangeRequestModel ChangeRequest { get; set; }
        public string StatusName { get; set; }
        public string RejectionReason { get; set; }
        // Used to map some Tender info , this will help to hide other variables
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }

        public DateTime? OffersCheckingDate { get; set; }
        public string OffersCheckingTime { get; set; }
        public string OffersCheckingDateString { get; set; }
        public string OffersCheckingDateHijriString { get; set; }
    }
}