using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferAttachmentsModel
    {
        public int TenderID { set; get; }
        public int TenderStatusId { set; get; }
        public string TenderIDString { set; get; }
        public int OfferID { set; get; }
        public int OfferId { get; set; }
        public string OfferIDString { set; get; }

        public int CombinedDetailId { get; set; }
        public int CombinedId { get; set; }
        public string OfferIdString { set; get; }
        public int CombinSupplierDetailID { set; get; }
        public string CombinDetailsIDString { set; get; }

        public int OfferPresentationWayId { get; set; }
        public string CombinedIdString { get; set; }
        public string SupplierName { get; set; }
        public AttachmentModel CombinationLetter { get; set; }
        [Display(Name = "جداول الكميات")]
        public IList<SupplierBankGuaranteeModel> BankGuaranteeFiles { get; set; } = new List<SupplierBankGuaranteeModel>();
        [Display(Name = "جداول الكميات")]
        public IList<SupplierSpecificationModel> SpecificationsFiles { get; set; } = new List<SupplierSpecificationModel>();

        [Display(Name = "أعمال الإنشاء ")]
        public IList<ConstructionWorkModel> ConstructionWorks { get; set; }
        [Display(Name = "أعمال الصيانة و التشغيل ")]

        public IList<MaintenanceRunningWorkModel> MaintenanceRunningWorks { get; set; }

        [Display(Name = "إسم البنك مصدر الضمان")]
        public IList<BankModel> Banks { get; set; }

        [Display(Name = "أعمال الإنشاء ")]
        public string ConstructionWork { get; set; }
        [Display(Name = "أعمال الصيانة و التشغيل ")]
        public string MaintenanceRunningWork { get; set; }


        public string Bank { get; set; } = "";

        [Required]
        public bool? IsChamberJoiningAttached { get; set; } = true;
        [Required]
        public bool? IsChamberJoiningValid { get; set; } = true;
        [Required]
        public bool? IsCommercialRegisterAttached { get; set; } = true;
        [Required]
        public bool? IsCommercialRegisterValid { get; set; } = true;
        [Required]
        public bool? IsOfferCopyAttached { get; set; } = true;
        [Required]
        public bool? IsOfferLetterAttached { get; set; } = true;
        public string OfferLetterNumber { get; set; }
        public DateTime? OfferLetterDate { get; set; }
        public string OfferLetterDateDisplay { get; set; }
        public string OfferLetterDateString { get; set; }
        [Required]
        public bool? IsPurchaseBillAttached { get; set; } = true;
        [Required]
        public bool? IsSaudizationAttached { get; set; } = true;
        [Required]
        public bool? IsSaudizationValidDate { get; set; } = true;
        [Required]
        public bool? IsSocialInsuranceAttached { get; set; } = true;
        [Required]
        public bool? IsSocialInsuranceValidDate { get; set; } = true;
        [Required]
        public bool? IsVisitationAttached { get; set; } = true;
        [Required]
        public bool? IsZakatAttached { get; set; } = true;
        [Required]
        public bool? IsZakatValidDate { get; set; } = true;
        public bool IsBankGuaranteeAttached { get; set; }
        //public string IsBankGuaranteeAttachedString { get; set; }
        public bool IsSpecificationAttached { get; set; }
        public bool IsSpecificationValidDate { get; set; }
        public bool? IsOpened { get; set; }
        public int? OfferAcceptanceStatusId { get; set; }
        public int? OfferTechnicalEvaluationStatusId { get; set; }
        public string Notes { get; set; }
        public string Discount { get; set; }
        public string DiscountNotes { get; set; }

        public decimal? FinalPrice { set; get; }
        //public string IsSpecificationAttachedString { get; set; }
        //public string IsSpecificationValidDateString { get; set; }



    }
}
