
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferCheckingAttachmentsDetailsModel
    {
        public IList<SupplierBankGuaranteeModel> BankGuaranteeFiles { get; set; } = new List<SupplierBankGuaranteeModel>();
        public IList<SupplierSpecificationModel> SpecificationsFiles { get; set; } = new List<SupplierSpecificationModel>();
        public string IsChamberJoiningAttached { get; set; }
        public string IsChamberJoiningValid { get; set; }
        public string IsCommercialRegisterAttached { get; set; }
        public string IsCommercialRegisterValid { get; set; }
        public string IsOfferCopyAttached { get; set; }
        public string IsOfferLetterAttached { get; set; }
        public string OfferLetterNumber { get; set; }
        public DateTime? OfferLetterDate { get; set; }
        public string OfferLetterDateDisplay { get; set; }
        public string OfferLetterDateString { get; set; }
        public string IsPurchaseBillAttached { get; set; }
        public string IsSaudizationAttached { get; set; }
        public string IsSaudizationValidDate { get; set; }
        public string IsSocialInsuranceAttached { get; set; }
        public string IsSocialInsuranceValidDate { get; set; }
        public string IsVisitationAttached { get; set; }
        public string IsZakatAttached { get; set; }
        public string IsZakatValidDate { get; set; }
        public string IsBankGuaranteeAttached { get; set; }
        public string IsSpecificationAttached { get; set; }
        public string IsSpecificationValidDate { get; set; }
        public string IsOpened { get; set; }
    }
}
