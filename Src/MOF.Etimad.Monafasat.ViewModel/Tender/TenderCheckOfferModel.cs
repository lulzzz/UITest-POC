using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderCheckOfferModel
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
        public decimal? OfferWeightAfterCalcNPA { get; set; }
        //[Display(Name = "حالة الشراء/ الدعوة")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InvitationPurchaseStatus")]
        public string InvitationTypeName { get; set; }
        public string OfferStatus { get; set; }
        //[Display(Name = "فحص العرض")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferCheck")]
        public int? OfferAcceptanceStatusId { get; set; }
        //[Display(Name = "التقييم الفني")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalEvaluation")]
        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public int OfferStatusId { get; set; }
        public bool IsChecked { get; set; }
        public decimal? TotalOfferAwardingValue { get; set; }
        public decimal? PartialOfferAwardingValue { get; set; }
        public decimal? AwardingValue { get; set; }
        public string OfferIdString { get { return Util.Encrypt(OfferId); } }
        public int CombinedCount { get; set; }
        public bool? TenderAwardingType { get; set; }//نوع الترسية كاملة أو جزئية
        public string BaiscActivity { get; set; }
        public DateTime? InvitationPurchaseDate { get; set; }
        public string InvitationPurchaseDateString { get; set; }
        public string InvitationPurchaseStatus { get; set; }
        public DateTime? CheckingDate { get; set; }
        public int TenderTypeId { get; set; }
        public int TechnicalEvaluationLevel { get; set; }
        public int FinancialEvaluationLevel { get; set; }
        public bool ContainSupply { get; set; }
        public bool isNewAwarding { get; set; }

        public decimal? LocalContentWeight { get; set; }
        public decimal? FinancialEvaluation { get; set; }
        public decimal? PriceAfterCalculateSMEA { get; set; }

        public decimal? FinalPrice { get; set; }
    }
}
