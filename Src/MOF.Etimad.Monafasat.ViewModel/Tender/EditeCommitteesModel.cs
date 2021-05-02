using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class EditeCommitteesModel
    {
        public int TenderId { get; set; }
        public string TenderIdString { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public string TenderName { get; set; }
        public int TenderTypeId { get; set; }
        public decimal? EstimatedValue { get; set; }
        public bool? IsLowBudgetDirectPurchase { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "PreQualificationCommittee")]
        public int? PreQualificationCommitteeId { get; set; }
        public List<CommitteeModel> PreQualificationCommittees { set; get; }

        //[Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "TechnicalOrganizationRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "VROCommittee")]
        public int? VROCommitteeId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SelectDirectPurchaseCommittee")]
        public int? DirectPurchaseCommitteeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DirectPurchaseCommitteeMemberId")]
        public int? DirectPurchaseCommitteeMemberId { get; set; }
        public int TenderStatusId { get; set; }
        public int BranchId { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public List<CommitteeModel> TechnicalCommittees { set; get; }
        public List<CommitteeModel> OfferOpenningCommittees { set; get; }
        public List<CommitteeModel> OfferCheckingCommittees { set; get; }
        public List<CommitteeModel> DirectPurchaseCommittees { set; get; }
        public List<CommitteeUserModel> DirectPurchaseCommitteeMember { get; set; }
        public List<CommitteeModel> VROCommittees { set; get; }
        public bool IsAgancyHasTechnicalCommittee { get; set; }
        public bool BranchHasTechnicalCommittees { get; set; }
    }
}
