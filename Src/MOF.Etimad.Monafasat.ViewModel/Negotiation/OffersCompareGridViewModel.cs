using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat
{
    public class OffersCompareGridViewModel
    {
        //[Display(Name = "الإسم التجاري")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialName")]
        public string CommericalRegisterName { get; set; }
        //[Display(Name = "الرقم التجاري")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "CommercialNumber")]
        public string CommericalRegisterNo { get; set; }
        //[Display(Name = "رقم العرض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferNumber")]
        public int OfferId { get; set; }
        //[Display(Name = "قيمة العرض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferValue")]
        public decimal OfferPrice { get; set; }
        public decimal OfferPriceNP { get; set; }
        public DateTime InvitationDate { get; set; }
        //[Display(Name = "حالة الشراء/ الدعوة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationPurchaseStatus")]
        public string InvitationTypeName { get; set; }
        public string InvitationPurchaseStatus { get; set; }
        public string OfferIdString { get; set; }
        public string CombinedIdString { get; set; }
        public int CombinedId { get; set; }
        public int CombinedSupplierCount { get; set; }
        public string OfferAcceptanceStatus { get; set; }
        public string OfferTechnicalEvaluationStatus { get; set; }
        public bool IsTawreed { get; set; }

        #region [Constructors]
        public OffersCompareGridViewModel(string commericalRegisterName, string commericalRegisterNo, decimal offerPrice,
            string offerIdString, string combinedIdString, int combinedSupplierCount, string offerAcceptanceStatus, string offerTechnicalEvaluationStatus, string _InvitationPurchaseStatus, bool _istawreed = false, decimal _offerPriceNP = 0)
        {
            CommericalRegisterName = commericalRegisterName;
            CommericalRegisterNo = commericalRegisterNo;
            OfferPrice = offerPrice;
            OfferIdString = offerIdString;
            CombinedIdString = combinedIdString;
            OfferId = Util.Decrypt(offerIdString);
            CombinedSupplierCount = combinedSupplierCount;
            OfferAcceptanceStatus = offerAcceptanceStatus;
            OfferTechnicalEvaluationStatus = offerTechnicalEvaluationStatus;
            InvitationPurchaseStatus = _InvitationPurchaseStatus;
            OfferPriceNP = _offerPriceNP;
            IsTawreed = _istawreed;
        }
        #endregion
    }
}
