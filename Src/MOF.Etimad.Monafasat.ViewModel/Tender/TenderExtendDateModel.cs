using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderExtendDateModel
    {
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        public DateTime? OffersOpeningDate { get; set; }
        public DateTime? OffersCheckingDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningTime")]
        public string OffersOpeningTime { get; set; }
        public string CreatedBy { get; set; }
        //public BasicTenderModel Tender { get; set; }
        public TenderChangeRequestModel ChangeRequest { get; set; }
        public string ChangeRequestStatusName { get; set; }
        public string RejectionReason { get; set; }
        // Used to map some Tender info , this will help to hide other variables
        public string TenderIdString { get; set; }
        public string TenderName { get; set; }
        public string TenderStatusIdString { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
    }
}