using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderInformationModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public string TenderTypeName { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        public bool CanAddEnquiry { get; set; }
        public int? TechnicalOrganizationId { get; set; }
        public int RemainingDays
        {
            get
            {
                return LastOfferPresentationDate.HasValue ? (LastOfferPresentationDate.Value - DateTime.Now).Days : 0;
            }
        }
        public int RemainingHours
        {
            get
            {
                return LastOfferPresentationDate.HasValue ? (LastOfferPresentationDate.Value - DateTime.Now).Hours : 0;
            }
        }
        public int RemainingMins
        {
            get
            {
                return LastOfferPresentationDate.HasValue ? (LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0;
            }
        }
        public int EnquiriesCountForTechnical { get; set; }
        public int EnquiriesCountForAuditor { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderReferenceNumber { get; set; }
        public DateTime? OffersOpeningDate { get; set; }
        public string UserCR { get; set; }


    }
}
