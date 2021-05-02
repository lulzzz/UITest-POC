using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferDetailsForCheckingModel
    {
        public int CombinedDetailId { get; set; }
        public int CombinedId { get; set; }
        public int TenderID { set; get; }
        public int TenderId { get; set; }
        public int TenderStatusId { set; get; }
        public string TenderIdString { set; get; }
        public string OfferIdString { set; get; }
        public int OfferId { get; set; }
        public int CombinSupplierDetailID { set; get; }
        public string CombinDetailsIDString { set; get; }
        public IList<SupplierBankGuaranteeModel> BankGuaranteeFiles { get; set; } = new List<SupplierBankGuaranteeModel>();
        public IList<SupplierSpecificationModel> SpecificationsFiles { get; set; } = new List<SupplierSpecificationModel>();
        public IList<ConstructionWorkModel> ConstructionWorks { get; set; }
        public IList<MaintenanceRunningWorkModel> MaintenanceRunningWorks { get; set; }
        public IList<BankModel> Banks { get; set; }
        public OfferDetailsForCheckingTwoFilesModel CheckingTwoFilesModel { get; set; }
        public List<SupplierAttachmentModel> Attachment { get; set; }
        public List<CombinedSupplierModel> CombinedSupplierModel { get; set; }
        public List<SupplierQuantityTableModel> QuantityTable { get; set; }
        public AttachmentModel CombinationLetter { get; set; }
        public string ConstructionWork { get; set; }
        public string MaintenanceRunningWork { get; set; }
        public string Bank { get; set; } = "";
        public decimal? FinalPrice { get; set; }
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
        public bool IsBankGuaranteeAttached { get; set; }
        //public string IsBankGuaranteeAttachedString { get; set; }
        public bool IsSpecificationAttached { get; set; }
        public bool IsSpecificationValidDate { get; set; }
        public bool? IsOpened { get; set; }
        public int OfferPresentationWayId { get; set; }
        public string CombinedIdString { get; set; }
        public string SupplierName { get; set; }
        public int? FinantialOfferStatusId { get; set; }

        public int? TechnicalOfferStatusId { get; set; }
        public string TechnicalOfferStatusString { get; set; }
        public string FinantialOfferStatusString { get; set; }
        public string RejectionReason { get; set; }
        public string FinantialRejectionReason { get; set; }
        public string Notes { get; set; }
        public string FinantialNotes { get; set; }

        public bool? IsFinaintialOfferLetterAttached { get; set; }
        [StringLength(500)]
        public string FinantialOfferLetterNumber { get; set; }
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FinantialOfferLetterDate { get; set; }
        public bool? IsFinantialOfferLetterCopyAttached { get; set; }
        public bool? IsSupplierCombinedLeader { get; set; } //TOSO Remove
        public string DiscountNotes { get; set; }
        public string Discount { get; set; }
        public int OfferStatusId { get; set; }
        public string CommericalRegisterNo { get; set; }
        public bool CombinedOwner { get; set; }
        public int FinancialWeight { get; set; }
        public bool HasManyCombinders { get; set; }
        public bool tenderHasGuarantee { get; set; }
        public int TenderTypeId { get; set; }
        public bool isOldFlow { get; set; }
        public bool isLowBudgetFlow { get; set; }
        public bool isUserisDirectPurchaseAssignedMember { get; set; }
    }
}
