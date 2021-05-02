using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class LinkTendersToCommitteeModel
    {
        public string AgencyCode { get; set; }
        public int? BranchId { get; set; }
        public List<int> BrancheIds { get; set; }
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public int? SelectedAgencyCode { get; set; }
        public int? SelectedBranchId { get; set; }
        public int? SelectedCommitteeId { get; set; }
        public int? CommitteeTypeId { get; set; }
        public int CommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        [Required]
        public int TenderTypeId { get; set; }

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
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public string SubmitionDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }
        public string OffersOpeningTime { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        public DateTime? OffersOpeningDate { get; set; }
        public string TenderTypeName { get; set; }
        public string InvitationTypeName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }
        public int? DirectPurchaseCommitteeId { get; set; }
        public int? VROCommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }

        public string TenderStatusName { get; set; }
        public string TechnicalOrganizationName { get; set; }
        public string OffersOpeningCommitteeName { get; set; } //لجنة فتح العروض
        public string OffersCheckingCommitteeName { get; set; } //لجنة فحص العروض

    }
}