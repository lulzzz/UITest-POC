
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferOpeningModel
    {
        public int OfferId { get; set; }
        [Display(Name = "رقم المورد")]
        public int SupplierId { get; set; }
        [Display(Name = "رقم المنافسة")]
        public int TenderId { get; set; }

        [Display(Name = "رقم الدعوة")]
        public int InvitationId { get; set; }

        [Display(Name = "حالة العرض")]
        public int OfferStatusId { get; set; }
        [Required]
        [Display(Name = "طريقة استلام العرض")]
        public bool? IsManuallyApplied { get; set; }
        public bool? IsActive { get; set; }

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

        [Display(Name = "تاريخ الإنسحاب")]
        public DateTime? WithdrawalDate { get; set; }
        public int? InvitationTypeId { get; set; }
        public int? OfferAcceptanceStatusId { get; set; }
        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public int? AwardingTypeId { get; set; }
        public string Notes { get; set; }
        public List<SupplierQuantityTableModel> QuantityTable { get; set; }
        public List<SupplierAttachmentModel> Attachment { get; set; }
        public int TenderStatusId { get; set; }
        public string JustificationOfRecommendation { get; set; }

        public OfferAttachmentsModel AttachmentsModel { set; get; }

    }
}
