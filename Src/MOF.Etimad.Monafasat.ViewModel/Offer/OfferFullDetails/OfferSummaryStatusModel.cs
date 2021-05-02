using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferSummaryStatusModel
    {

        public string OfferStringId { get; set; }
        public string tenderStringId { get; set; }
        public int StatusId { get; set; }
        public int tenderTypeId { get; set; }
        public int? TenderLocalContentId { get; set; }
        public bool IsTenderHasLocalContentMechanism { get; set; }
        public bool IsValidToApplyOfferLocalContentChanges { get; set; }
        public bool IsRequiredFilesAttached { get; set; }

        public bool IsSolidarity { get; set; }
        public bool IsOfferOwner { get; set; }
        public string StatusName { get; set; }
        public List<string> ValidationSummary { get; set; } = new List<string>();
        public IEnumerable<string> IValidationSummary { get; set; }
        public string CommercialNumber { get; set; }
        public int? ConfigurationSettingId { get; set; }
         public bool IsOldTender
        {
            get => tenderTypeId == (int)Enums.TenderType.CurrentTender || tenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || tenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }
         
    }
}
