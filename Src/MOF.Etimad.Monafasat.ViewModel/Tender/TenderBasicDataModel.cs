using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    /// <summary>
    /// Used for create and update first step (basic data) of the tender
    /// </summary>
    public class TenderBasicDataModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }

        [Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationType")]
        public int? InvitationTypeId { get; set; }
        public int? OfferStatusId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletPrice")]
        public Decimal? ConditionsBookletPrice { get; set; }
        [Required]
        [StringLength(500)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }

        [Required]
        [StringLength(1024, ErrorMessageResourceName = "PurposeValiation", ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), MinimumLength = 40)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferOpenningCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }
        public bool? IsCommitteeUpdate { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        public bool CurrentGreaterEnquiryDate { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public int? OffersOpeningAddressId { get; set; }
        public List<AddressModel> OffersOpeningAddressList { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public string OffersOpeningAddress { get; set; }
        public bool IsFavouriteTender { get; set; }
        public string AgencyName { get; set; }
        public string TenderTypeName { get; set; }
        public string InvitationTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public string CreatedBy { get; set; }

        public string TechnicalOrganizationName { get; set; }
        public string OffersOpeningCommitteeName { get; set; } //لجنة فتح العروض
        public string OffersCheckingCommitteeName { get; set; } //لجنة فحص العروض
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DirectPurchaseProcedureType")]
        public bool? IsLowBudgetDirectPurchase { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DirectPurchaseCommitteeMemberId")]
        public int? DirectPurchaseCommitteeMemberId { get; set; }
        public string DirectPurchaseCommitteeMemberName { get; set; }
        public int RemainingDays
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Days : 0;

            }
        }
        public int RemainingHours
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Hours : 0;
            }
        }
        public int RemainingMins
        {
            get
            {
                return LastOfferPresentationDate.HasValue && LastOfferPresentationDate > DateTime.Now ? (LastOfferPresentationDate.Value - DateTime.Now).Minutes : 0;

            }
        }


    }
}
