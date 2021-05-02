
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderInvitationDetailsModel
    {
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public DateTime CurrentDateTime { get; set; } = DateTime.Now;
        public int OfferPresentationWayId { get; set; }
        public TimeSpan CurrentTime { get; set; }
        public string BranchName { get; set; }
        public int TenderId { get; set; }
        public int InvitationId { get; set; }
        public string InvitationIdString { get; set; }
        public string InvitationStatusString { get; set; }
        public string TenderIdString { get; set; }
        public int? StatusId { get; set; }
        public int? PreQualificationId { get; set; }
        public int? OfferStatusId { get; set; }
        public string OfferStatusName { get; set; }
        public int? OfferId { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
        public bool IsBlocked { get; set; }
        public string AgencyName { get; set; }
        public int? TenderTypeId { get; set; }
        public int? InvitationTypeId { get; set; }
        public bool HaveToPayFinancialInvitationFees { get; set; }
        public bool HaveToPayFinancialConditionalBookletFees { get; set; }
        public decimal FinancialFees { get; set; }
        public decimal BuyingCost { get; set; }
        public decimal InvitationCost { get; set; }

        public string ReferenceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialName")]
        public string CommericalName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialNumber")]
        public string CommericalRegisterNo { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationStatusName")]
        public string InvitationStatusName { get; set; }
        [StringLength(500)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderName")]
        public string TenderName { get; set; }
        [StringLength(100)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderNumber")]
        public string TenderNumber { get; set; }
        [StringLength(1024)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ExecutionPlace")]
        public bool? InsideKSA { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? LastEnqueriesDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? LastOfferPresentationDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? OffersOpeningDate { get; set; }
        public DateTime? OffersCheckingDate { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalCommittee")]
        public int? TechnicalOrganizationId { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderActivity")]
        public List<string> TenderActivities { set; get; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<string> TenderConstructionWorks { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<string> TenderMaintenanceRunnigWorks { get; set; }
        public List<AreaModel> TenderAreas { set; get; }
        public int TenderStatusId { get; set; }
        public string TenderStatusIdString { get; set; }
        public int InvitationRequistTypeId { get; set; }
        public int InvitationStatusId { get; set; }
        public decimal? CondetionalBookletPrice { get; set; }
        public bool IsInvitedToSolidarity { get; set; }
        public bool IsPurchased { get; set; }
        public bool CanPurchase { get; set; }
        public bool IsTenderOwner { get; set; }
        public string TenderTypeName { get; set; }
        public string TenderStatusName { get; set; }
        public DateTime? SubmitionDate { get; set; }
        public List<int> TenderAreasIds { get; set; }
        public string TenderAreasIdString { get; set; }
        public bool IsFavouriteTender { get; set; }
        public bool HasEnquiry { get; set; }
        public bool IsJoinRequest { get; set; }
        public string TenderActivityName { get; set; }
        public List<string> TenderActivityNameList { get; set; }
        public string CreatedDateHijri { get; set; }
        public string CreatedAtHijri { get; set; }
        public string LastOfferPresentationDateHijri { get; set; }
        public string LastEnqueriesDateHijri { get; set; }
        public string OffersOpeningDateHijri { get; set; }
        public string OffersCheckingDateHijri { get; set; }
        public bool SupplierHasOffer { get; set; }

        public string RejectionReason { get; set; }

        public bool IsFavouriteShow { get; set; } = true;
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
        public int PaymentStatusId { get; set; }
        public bool? IsGateAnnouncement { get; set; }

        public bool HasQualification { get; set; }
        public int? FirstStageId { get; set; }


        public bool IsAvailabletoBuy
        {
            get;
            // {

            //if (!this.IsBlocked && !this.IsTenderOwner && this.PaymentStatusId == 0 && this.TenderStatusId == (int)Enums.TenderStatus.Approved && this.LastOfferPresentationDate >= this.CurrentDateTime /*&& !this.IsInvitedToSolidarity*/)
            //{
            //    switch (this.TenderTypeId)
            //    {
            //        case (int)Enums.TenderType.CurrentTender:
            //            return true;
            //        case (int)Enums.TenderType.NewTender:

            //            if (this.HasQualification)
            //            {
            //                if (this.InvitationStatusId == (int)Enums.InvitationStatus.Approved)
            //                    return true;
            //                else
            //                    return false;
            //            }
            //            else
            //                return true;

            //        case (int)Enums.TenderType.LimitedTender:
            //        case (int)Enums.TenderType.ReverseBidding:
            //        case (int)Enums.TenderType.FrameworkAgreement:
            //        case (int)Enums.TenderType.FirstStageTender:
            //            if (this.InvitationStatusId == (int)Enums.InvitationStatus.Approved)
            //                return true;
            //            else
            //                return false;

            //        default:
            //            return false;
            //    }
            //}
            //else
            //    return false;
            //  }
            set;
        }
        public bool IsAvailabletoApplyOffer
        {
            get;
            //    {
            //if (this.TenderStatusId == (int)Enums.TenderStatus.Approved && !this.SupplierHasOffer && this.LastOfferPresentationDate >= this.CurrentDateTime && !this.IsInvitedToSolidarity)
            //{
            //    switch (this.TenderTypeId)
            //    {
            //        case (int)Enums.TenderType.CurrentTender:
            //        case (int)Enums.TenderType.NewTender:
            //        case (int)Enums.TenderType.LimitedTender:
            //        case (int)Enums.TenderType.ReverseBidding:
            //        case (int)Enums.TenderType.FrameworkAgreement:
            //        case (int)Enums.TenderType.FirstStageTender:
            //            if (this.PaymentStatusId == (int)Enums.BillStatus.Paid)
            //                return true;
            //            else return false;
            //        case (int)Enums.TenderType.SecondStageTender:
            //        case (int)Enums.TenderType.NationalTransformationProjects:
            //            //case (int)Enums.TenderType.VRODirectPurchase:
            //            if (this.InvitationStatusId == (int)Enums.InvitationStatus.Approved)
            //                return true;
            //            else return false;
            //        case (int)Enums.TenderType.CurrentDirectPurchase:
            //        case (int)Enums.TenderType.NewDirectPurchase:
            //            if (this.InvitationStatusId == (int)Enums.InvitationStatus.Approved || this.PaymentStatusId == (int)Enums.BillStatus.Paid)
            //                return true;
            //            else return false;
            //        case (int)Enums.TenderType.Competition:
            //            if (this.InvitationTypeId == (int)Enums.InvitationType.Public)
            //                return true;
            //            else if (this.InvitationTypeId == (int)Enums.InvitationType.Specific && this.InvitationStatusId == (int)Enums.InvitationStatus.Approved)
            //                return true;
            //            else return false;
            //        default:
            //            return false;
            //    }
            //}
            //else
            //    return false;
            //}
            set;
        }
        public bool CanOpenBiddingPage { get; set; }

        public string OfferIdString { get { return this.OfferId.HasValue && OfferId != 0 ? Util.Encrypt(this.OfferId.Value) : ""; } }

        public bool IsOfferWithSolidarity { get; set; }
        public int OfferSolidarityId { get; set; }
        public string OfferSolidarityIdString { get { return OfferSolidarityId != 0 ? Util.Encrypt(this.OfferSolidarityId) : ""; } }

    }
}
