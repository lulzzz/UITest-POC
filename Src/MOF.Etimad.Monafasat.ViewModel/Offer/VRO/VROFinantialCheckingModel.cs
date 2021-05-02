using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class VROFinantialCheckingModel
    {
        public string OfferIdString { get; set; }
        public int? OfferPresentationWayId { get; set; }

        public int OfferId { get; set; }

        [Display(Name = "رقم المورد")]
        public int SupplierId { get; set; }
        [Display(Name = "رقم المنافسة")]
        public int TenderId { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderIdString { get; set; }
        [Display(Name = "إسم المنافسة")]
        public string TenderName { get; set; }
        [Display(Name = "رقم المنافسة")]
        public string TenderNumber { get; set; }
        public bool? InsideKSA { get; set; }
        [Display(Name = "رقم الدعوة")]
        public int InvitationId { get; set; }

        [Display(Name = "حالة العرض")]
        public int OfferStatusId { get; set; }
        [Required]
        [Display(Name = "طريقة استلام العرض")]
        public bool? IsManuallyApplied { get; set; }
        public bool? IsActive { get; set; }

        public bool HasAlternativeOffer { get; set; }
        //[Display(Name = "إسم المنافسة")]
        //public string TenderName { get; set; }

        //[Display(Name = "رقم المنافسة")]
        //public string TenderNumber { get; set; }

        [Display(Name = "رقم السجل التجارى")]
        public string CommericalRegisterNo { get; set; }

        [Display(Name = "إسم الشركة")]
        public string CompanyName { get; set; }

        [Display(Name = "تاريخ إرسال الدعوة/الشراء")]
        public DateTime? InvitationSendingDate { get; set; }

        [Display(Name = "حالة الدعوة / الشراء")]
        public string InvitationStatusName { get; set; }

        [Display(Name = "النوع")]
        public string InvitationTypeName { get; set; }
        public int InvitationStatusId { get; set; }

        [Display(Name = "تاريخ قبول / رفض الدعوة")]
        public DateTime? SubmissionDate { get; set; }/// <summary>
                                                     /// 
                                                     /// 
                                                     /// </summary>

        [Display(Name = "تاريخ الإنسحاب")]
        public DateTime? WithdrawalDate { get; set; }
        public DateTime? OfferWithdrawalDate { get; set; }
        public int TenderTypeId { get; set; }

        public int? InvitationTypeId { get; set; }
        public int? OfferAcceptanceStatusId { get; set; }
        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public int? AwardingTypeId { get; set; }
        public string Notes { get; set; }
        public List<SupplierQuantityTableModel> QuantityTable { get; set; }
        public List<SupplierAttachmentModel> Attachment { get; set; }

        public List<CombinedSupplierModel> CombinedSupplierModel { get; set; }
        public int TenderStatusId { get; set; }

        public string JustificationOfRecommendation { get; set; }
        public string DiscountNotes { get; set; }
        public string Discount { get; set; }
        public bool? CanSeeDetails { get; set; }
        public decimal? FinalPrice { get; set; }
        public string OfferStatusName { get; set; }

        public string CombinedIdString { get; set; }
        public bool IsSupplierCombinedLeader { get; set; }
        public int? FinantialOfferStatusId { get; set; }
        public string FinantialRejectionReason { get; set; }
        public string FinantialNotes { get; set; }
        public string FinantialOfferStatusString { get; set; }
        public int? TechnicalOfferStatusId { get; set; }
        public string TechnicalOfferStatusString { get; set; }
        public string TechnicalRejectionReason { get; set; }
        public bool CombinedOwner { get; set; }
        public DateTime? OffersCheckingDateTime { get; set; }
        [MaxLength(3)]
        public int FinancialWeigth { get; set; }
        public int TechnicalWeigth { get; set; }
        public List<TechniciansReportAttachmentModel> TechniciansReportAttachments { get; set; }
        [Display(Name = "تقارير الفنيين")]
        public string TechniciansReportAttachmentsRef { get; set; }
        public QuantitiesTemplateModel QuantityTableForCheckingModel { get; set; }
        public OfferQuantityTableHtmlVM QuantityTableEditableForCheckingModel { get; set; }
        //New quantity tables
        //public QuantitiesTemplateModel QuantitiesTemplateModel { get; set; }
        public QuantityTableStepDTO QuantityTableStepDTO { get; set; }
    }
}
