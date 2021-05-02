using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AllTendersViewModel
    {
        public bool HasPreQualification { get; set; }
        public int TenderId { get; set; }
        public int? PreQualificationId { get; set; }
        public string TenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        public int? InvitationTypeId { get; set; }
        public Decimal? ConditionsBookletPrice { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string TenderReferenceNumber { get; set; }

        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }

        public string Purpose { get; set; }
        public int TechnicalOrganizationId { get; set; }
        public int? OffersOpeningCommitteeId { get; set; }
        public int? OffersCheckingCommitteeId { get; set; }
        public DateTime? SubmitionDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public string LastOfferPresentationTime { get; set; }
        public string OffersOpeningTime { get; set; }
        public DateTime? OffersOpeningDate { get; set; }

        public string OffersCheckTime { get; set; }

        public DateTime? OffersCheckingDate { get; set; }

        public string TenderTypeName { get; set; }
        public string InvitationTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public string TechnicalOrganizationName { get; set; }
        public string OffersOpeningCommitteeName { get; set; } //لجنة فتح العروض
        public string OffersCheckingCommitteeName { get; set; } //لجنة فحص العروض
        public bool? SupplierNeedSample { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public DateTime? LastOfferPresentationDate { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public string AgencyName { get; set; }
        public string BranchName { get; set; }

        public bool CanViewSuppliersReportFinancialSupervisor { get; set; }
        public int OfferPresentationWayId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EvaluationValue")]
        public Decimal? EstimatedValue { get; set; }
        public bool? InsideKSA { get; set; }


        public int? OffersCount { get; set; }
        public int? UserCommitteeId { get; set; }
        public string SubmitionDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
        public string OffersCheckingDateHijri { get; set; }
        public List<int> TenderChangeRequestIds { get; set; }
        public List<int> TenderChangeRequestIdsForAuditor { get; set; }
        public List<int> TenderChangeRequestIdsForDataEntry { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public int QuantitiesTableStatus { get; set; }
        public int AttachmentsStatus { get; set; }
        public int ExtendDatesStatus { get; set; }
        public string ChangeRequestedBy { get; set; }
        public int CancelRequestStatus { get; set; }
        public bool HasSecondStage { get; set; }
        public bool HasAcceptedOffers { get; set; }

        public bool CanRecreateFramWork { get; set; }

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
        public DateTime? DayBeforeLastOfferPresentationDate
        {
            get
            {
                return LastOfferPresentationDate.HasValue ? LastOfferPresentationDate.Value.AddDays(-1) : DateTime.Now;
            }
        }

        public int? UnitStatusId { get; set; }
    }

}
