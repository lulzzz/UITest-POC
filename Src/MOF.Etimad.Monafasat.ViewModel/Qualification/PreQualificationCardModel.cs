using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PreQualificationCardModel
    {
        //User Story 44 - 45 - 46
        public int TenderId { get; set; }

        public string AgencyCode { get; set; }


        public string TenderIdString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InitiatorName")]
        public string InitiatorName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationNumber")]
        public string QualificationNumber { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationName")]
        public string QualificationName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QualificationStatus")]
        public int TenderStatusId { get; set; }
        public bool IsFavouriteQualification { get; set; }
        public string QualificationReferenceNumber { get; set; }
        public List<int> TenderChangeRequestIdsForQualificationManager { get; set; }
        public List<int> TenderChangeRequestIdsForQualificationSecretary { get; set; }
        public string QualificationStatusName { get; set; }
        public string TenderStatusIdString { get; set; }
        [Required]
        [StringLength(1024, ErrorMessageResourceName = "PurposeValiation", ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), MinimumLength = 40)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string Purpose { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime CreatedDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        public DateTime? LastEnqueriesDate { get; set; }
        public string LastEnqueriesTime { get; set; }
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationEvaluationDate")]
        public string LastOfferPresentationTime { get; set; }
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationEvaluationDate")]
        public DateTime? OffersCheckingDate { get; set; }
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationEvaluationTime")]
        public string OffersCheckingTime { get; set; }
        public string TenderStatusName { get; set; }
        public List<int> TenderChangeRequestIds { get; set; }
        public List<int> ChangeRequestStatusIds { get; set; }
        public int UserCommitteeType { get; set; }
        public int UserCheckCommitteeType { get; set; }
        public int UserDirectPurchaseCommitteeType { get; set; }
        public bool IsBlocked { get; set; }
        public string CancelRequestRoleName { get; set; }
        public List<TenderChangeRequestModel> lstOfChangeRequest { get; set; }

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
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        public string ActivityDescription { get; set; }
        [Display(ResourceType = typeof(Resources.QualificationResources.DisplayInputs), Name = "QualificationType")]
        public string QualificationTypeName { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        public DateTime? LastOfferPresentationDate { get; set; }
        public int AgencyId { get; set; }
        public string AgencyName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SubmtionDate")]
        public DateTime? SubmitionDate { get; set; }


        public string SubmitionDateHijri { get; set; }
        public string CreatedDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersCheckingDateHijri { get; set; }
        public bool SupplierHasOffer { get; set; }
        public int? QualificationStatusId { get; set; }
        public bool SupplierHasPostQualificationInvitations { get; set; }
        public string SupplierOfferStatusName { get; set; }
        public int SupplierOfferStatusId { get; set; }
        public bool isSupplierAwarded { get; set; }
        public string LastOfferPresentationDateString { get; set; }
        public string ChangeRequestedBy { get; set; }
        public int QuantitiesTableStatus { get; set; }
        public int AttachmentsStatus { get; set; }
        public int ExtendDatesStatus { get; set; }
        public int CancelRequestStatus { get; set; }
        public int TenderTypeId { get; set; }
        public bool HasPendingCancelRequest { get; set; }
        public int PostQualificationTenderTypeId { get; set; }
        public List<int> TenderChangeRequestIdsForAuditor { get; set; }
        public List<int> TenderChangeRequestIdsForDataEntry { get; set; }
        public List<int> TenderChangeRequestIdsForManager { get; set; }
        public List<int> TenderChangeRequestIdsForSecretary { get; set; }

        public List<int> TenderChangeRequestIdsForDirectSecretary { get; set; }

        public List<int> TenderChangeRequestIdsForOfferCheckSecretary { get; set; }

        public List<string> ChangeRequestStatusNames { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public bool CanApplyPostQualificationDocument { get; set; }
        public bool CanApplyPreQualificationDocument
        {
            get
            {
                return !SupplierHasOffer && LastOfferPresentationDate >= CurrentDate && TenderStatusId == (int)Enums.TenderStatus.Approved && TenderTypeId == (int)Enums.TenderType.PreQualification;
            }
        }

        public int? TechnicalOrganizationId { get; set; }

        public bool CanSendEnquiry { get; set; }
        public bool IsUserHasAccessToLowBudgetTender { get; set; }
        public bool IsLowBudgetTender { get; set; }


    }






}
