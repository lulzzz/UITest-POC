using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllTendersForVisitorModel
    {
        public int TenderId { get; set; }
        public string ReferenceNumber { get; set; }
        [StringLength(500)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string AgencyName { get; set; }
        public string TenderIdString { get; set; }
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        public string TenderStatusName { get; set; }
        public int? TenderTypeId { get; set; }
        public string TenderTypeName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }
        public decimal? CondetionalBookletPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? LastOfferPresentationDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? OffersOpeningDate { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        [StringLength(1024)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        public string TenderActivityName { get; set; }
        public List<string> TenderActivityNameList { get; set; }
        public DateTime? SubmitionDate { get; set; }
        public decimal FinancialFees { get; set; }
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
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public DateTime CurrentDateTime { get; set; } = DateTime.Now;

        public TimeSpan CurrentTime { get; set; }

    }
}
