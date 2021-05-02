using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOffersGridModel
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
        public decimal? OfferPrice { get; set; }
        public decimal? OfferPriceNP { get; set; }
        public DateTime InvitationDate { get; set; }
        //[Display(Name = "حالة الشراء/ الدعوة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationPurchaseStatus")]
        public string InvitationTypeName { get; set; }
        public string OfferIdString { get; set; }
        public string status { get; set; }
        public string InvitationPurchaseStatus { get; set; }
        public string CombinedIdString { get; set; }
        public int CombinedId { get; set; }
        public int CombinedCount { get; set; }
        public string OfferAcceptanceStatus { get; set; }
        public string OfferTechnicalEvaluationStatus { get; set; }
        public int InvitationTypeId { get; set; }
        public int TenderTypeId { get; set; }
        public bool isTawreed { get; set; }
        public int? OfferPresentationWay { get; set; }
        public CommunicationAttachmentModel GuaranteeAttachment { get; set; }
    }
}
