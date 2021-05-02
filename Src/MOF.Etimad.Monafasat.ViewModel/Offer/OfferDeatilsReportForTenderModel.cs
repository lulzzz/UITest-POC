using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferDeatilsReportForTenderModel
    {
        public int TenderId { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string OpenOffersDate { get; set; }
        public int OfferId { get; set; }
        public string SupplierName { get; set; }
        public string CreateDate { get; set; }
        public string CommercialRegistrationValidity { get; set; }
        public string ZekatCertificateValidity { get; set; }
        public string ChamberJoiningValidity { get; set; }
        public string OfferLetter { get; set; }
        public string ConstructionCertificationValidity { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? OfferDiscountValue { get; set; }
        public List<BankGuarantee> BankGuarantees { get; set; }
        public string Notes { get; set; }
        public bool IsRegistry { get; set; }
        public List<bool> IsValidList { get; set; }
        public bool CanViewPrice { get; set; }
    }

    public class BankGuarantee
    {
        public string GuaranteeNumber { get; set; }
        public Decimal Amount { get; set; }
        public string GuaranteeStartDate { get; set; }
        public string BankName { get; set; }
        public string Percentage { get; set; }
    }
}
