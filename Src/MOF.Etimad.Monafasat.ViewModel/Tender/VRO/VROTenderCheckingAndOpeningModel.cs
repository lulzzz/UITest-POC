using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class VROTenderCheckingAndOpeningModel
    {
        public int TenderId { get; set; }
        public int OffersCount { get; set; }
        public string TenderIdString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ConditionsBookletPrice")]
        public Decimal? ConditionsBookletPrice { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderStatus")]
        public int TenderStatusId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }
        public string OffersOpeningTime { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        public DateTime? OffersOpeningDate { get; set; }
        public string TenderTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public int UserCommitteeType { get; set; }
        public string SubmitionDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
        public List<int> TenderChangeRequestIds { get; set; }
        public List<int> ChangeRequestStatusIds { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public string ChangeRequestedBy { get; set; }
        public int CancelRequestStatus { get; set; }
        public List<int> TenderChangeRequestIdsForOpeningSecretary { get; set; }
        public List<int> TenderChangeRequestIdsForOpeningManager { get; set; }
        public List<int> TenderChangeRequestIdsForCheckingSecretary { get; set; }
        public List<int> TenderChangeRequestIdsForCheckingManager { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOffersCommittee")]
        public int? OffersOpeningCommitteeId { get; set; }

        //indicate if the tender has any approved posqualification or negotiation or other
        public bool DoIHaveApprovedSideAction { get; set; }
        //indicate if the tender has any pending posqualification or negotiation or other
        public bool DoIHavePendingSideAction { get; set; }

        public bool CanStartTechnicalEvaluation { get; set; }
        public bool CanOpenBiddingPage { get; set; }
        public bool CanStartingAwarding { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CheckOffersCommittee")]
        public int? OffersCheckingCommitteeId { get; set; }
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

        public string TenderReferenceNumber { get; set; }
        public bool CanLevelTwoApprovementDoAction { get; set; }
    }
}
