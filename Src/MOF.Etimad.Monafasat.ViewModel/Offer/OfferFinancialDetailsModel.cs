using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferFinancialDetailsModel
    {
        [Display(Name = "رقم العرض")]
        public int OfferId { get; set; }
        public int TenderId { get; set; }
        public string OfferIdString { get { return Util.Encrypt(OfferId); } }
        public int? TenderStatusId { get; set; }
        public string Notes { get; set; }
        public bool? IsPasssPreqaulification { get; set; }
        public bool? IsPasssPostQaulification { get; set; }
        public bool? HasActivePostQaulification { get; set; }
        public bool CanAddExclustionReasonIfExtendOfferValidityExist { get; set; }
        public string State { get; set; }
        public string CommericalRegisterName { get; set; }
        public string CommericalRegisterNo { get; set; }
        [Display(Name = "حالة الشراء/ الدعوة")]
        public string InvitationTypeName { get; set; }
        [Display(Name = "فحص العرض")]
        public int? OfferAcceptanceStatusId { get; set; }
        [Display(Name = "التقييم الفني")]
        public int? OfferTechnicalEvaluationStatusId { get; set; }

        public decimal? AwardingValue { get; set; }
        public decimal? OfferPrice { get; set; }
        public decimal? OfferDiscountValue { get; set; }

        public List<OfferFinancialTableModel> Tables { get; set; }
        public List<BankGuaranteeAttachmentModel> BankGuarantee { get; set; }
        public List<SpecificationAttachmentModel> Specification { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
        public dynamic testTata { get; set; }

        public string JustificationOfRecommendation { get; set; }
        public decimal? FinalPrice { get; set; }
        public string DiscountNotes { get; set; }
        public string Discount { get; set; }
        public bool CanAddPostQualification { get; set; }
        public bool HasPrequalification { get; set; }
        public bool HasPostQualification { get; set; }
        public List<TenderHistoryModel> PrequalificationHistoryModels { get; set; }
        public string postQualificationTenderIdString { get; set; }
        public int FinancialEvaluationLevel { get; set; }
        public int TechnicalEvaluationLevel { get; set; }
        public decimal? OfferWeightAfterCalcNPA { get; set; }
        public string ExclusionReason { get; set; }
        public int? DirectPurchaseReasonId { get; set; }
        public bool IsExclusionReasonEmpty { get; set; }

        public bool isNewAwarding { get; set; }
    }

    public class OfferFinancialTableModel
    {
        public OfferFinancialTableModel() { }
        public OfferFinancialTableModel(string tableName, decimal totalAmount, decimal totalAmountAfterDiscount, decimal discount)
        {
            TableName = tableName;
            TotalAmount = totalAmount;
            TotalAmountAfterDiscount = totalAmountAfterDiscount;
            Discount = discount;
        }


        public string TableName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountAfterDiscount { get; set; }
        public decimal Discount { get; set; }


    }


    public class searchAwardingSupplier
    {
        public string crsuppliers { get; set; }
        public string TenderIdString { get; set; }

    }
}
