using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderDetialsReportModel
    {
        public int TenderId { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string AgnecyName { get; set; }
        public string SubAgnecyName { get; set; }
        public string TenderName { get; set; }
        public string TenderPurpose { get; set; }
        public string LastSupplierActionDateHijri { get; set; }
        public string LastSupplierActionDate { get; set; }
        public string LastOfferApplyingDateHijri { get; set; }
        public string LastOfferApplyingDate { get; set; }
        public string LastOpenOfferDateHijri { get; set; }
        public string LastOpenOfferDate { get; set; }
        public string ApplyingOffersLocation { get; set; }
        public string OpenOffersLocation { get; set; }
        public string SamplesDeliveryAddress { get; set; }
        public string ExcuationLocation { get; set; }
        public List<string> TenderMaintenanceRunnigWorks { get; set; }
        public List<string> TenderConstructionWorks { get; set; }
        public decimal? ConditionsBookletPrice { get; set; }
        public int TenderTypeId { get; set; }
        public int VersionId { get; set; }
        public int? AwardingStoppingPeriod { get; set; }
        public DateTime? StartingBusinessOrServicesDate { get; set; }
        public DateTime? AwardingExpectedDate { get; set; }

        public DateTime? ParticipationConfirmationLetterDate { get; set; }
        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }
        #region Qualification Fields

        public string LastEnqueriesDateString { get; set; }

        public string LastEnqueriesDateHijriString { get; set; }


        public string LastOfferPresentationDateString { get; set; }
        public string LastOfferPresentationDateHijriString { get; set; }
        public string OffersCheckingDateString { get; set; }
        public string OffersCheckingDateHijriString { get; set; }

        #endregion
    }
}
