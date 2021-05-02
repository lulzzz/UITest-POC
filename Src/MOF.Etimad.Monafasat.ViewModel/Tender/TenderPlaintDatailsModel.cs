using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderPlaintDatailsModel
    {
        public object Histories;

        public int TenderId { get; set; }
        public int TenderStatusId { get; set; }
        public bool? TenderAwardingType { get; set; }
        public string TenderAwardingTypeName { get; set; }
        public decimal? FullAmount { get; set; }
        public decimal? PartialAmount { get; set; }
        public List<string> AwardedSuppliers { get; set; }
        public List<LookupModel> AwardedSupplierList { get; set; }
        public string TenderIdString { set; get; }
        public int PlaintsNumber { get; set; }
        public string TenderNumber { get; set; }
        public string TenderName { get; set; }
        public string ReferenceNumber { get; set; }
        public int EscalatedPlaintsNumber { get; set; }
        public string AcceptedAnnouncementDate { get; set; }
        public string OfferRejectionReason { get; set; }
        public string TechnicalEvaluation { get; set; }
        public string Notes { get; set; }


        public string AgencyName { get; set; }
        public string AwardingDate { get; set; }
        public string OffersOpeningDate { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string OffersCheckingDate { get; set; }
        public bool ContainsSupply { get; set; }
        public bool? IsTenderContainsTawreedTables { get; set; }
        public int TenderTypeId { get; set; }
        public decimal? NationalProductPercentage { get; set; }
        public decimal? FinalGuaranteePercentage { get; set; }
        public DateTime? AwardingExpectedDate { get; set; }
        public DateTime? StartingBusinessOrServicesDate { get; set; }
        public DateTime? ParticipationConfirmationLetterDate { get; set; }
        public DateTime? StartingSendingEnquiries { get; set; }
        public int? AwardingStoppingPeriod { get; set; }
        public int? MaxTimeToAnswerQuestions { get; set; }

        public int? VersionId { get; set; }

        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }
        public bool? SupplierNeedSample { get; set; }

        public string OfferDeliveryAddress { get; set; }
        public string OfferBuildingName { get; set; }
        public string OfferFloarNumber { get; set; }
        public string OfferDepartmentName { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? OffersDeliveryDate { get; set; }
        public bool IsSampleAddresSameOffersAddress { get; set; }

        public string SamplesDeliveryAddress { get; set; }
        public string BuildingName { get; set; }
        public string FloarNumber { get; set; }
        public string DepartmentName { get; set; }

        public string OffersOpeningAddress { get; set; }

    }
}
