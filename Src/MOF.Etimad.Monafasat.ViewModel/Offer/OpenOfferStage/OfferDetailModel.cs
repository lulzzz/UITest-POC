using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferDetailModel
    {
        public int OfferId { set; get; }
        public int CombinedDetailId { get; set; }
        public int CombinedId { get; set; }
        public int TenderID { set; get; }
        public int TenderStatusId { set; get; }
        public string TenderIDString { set; get; }
        public string OfferIdString { set; get; }
        public int OfferStatusId { get; set; }
        public bool IsSolidarity { get; set; }

        public int CombinSupplierDetailID { set; get; }
        public string CombinDetailsIDString { set; get; }
        public IList<SupplierBankGuaranteeModel> BankGuaranteeFiles { get; set; } = new List<SupplierBankGuaranteeModel>();
        public IList<SupplierSpecificationModel> SpecificationsFiles { get; set; } = new List<SupplierSpecificationModel>();
        public IList<ConstructionWorkModel> ConstructionWorks { get; set; }
        public IList<MaintenanceRunningWorkModel> MaintenanceRunningWorks { get; set; }
        public IList<BankModel> Banks { get; set; }
        public string ConstructionWork { get; set; }
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
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OfferLetterDate { get; set; }
        public string OfferLetterDateDisplay { get; set; }
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
        public bool IsBankGuaranteeAttached { get; set; } = true;
        public bool IsSpecificationAttached { get; set; } = true;
        public bool IsSpecificationValidDate { get; set; } = true;
        public bool? IsOpened { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public string CombinedIdString { get; set; }
        public string SupplierName { get; set; }
        public AttachmentModel CombinationLetter { get; set; }

        public bool CombinedOwner { get; set; }
        public bool IsFinaintialOfferLetterAttached { get; set; }
        public bool IsFinantialOfferLetterCopyAttached { get; set; }


        public List<CombinedSupplierModel> CombinedSupplierModel { get; set; }

        public int TenderTypeId { get; set; }

        public string Discount { get; set; }
        public string DiscountNotes { get; set; }
        public List<QuantityTableForOpeningModel> QuantityTableForOpeningModel { get; set; }
        public List<SupplierAttachmentModel> Attachment { get; set; }
        public decimal? FinalPrice { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string FinantialOfferLetterNumber { get; set; }
        public DateTime? FinantialOfferLetterDate { get; set; }
        public bool isOldFlow { get; set; }
    }
}
