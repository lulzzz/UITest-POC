using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PurchaseModel
    {
        public int ConditionsBookletId { get; set; }

        public int InvitationId { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string TenderIdString { get; set; }
        public int TenderId { get; set; }
        public bool HaveToPayConditionalBookletFees { get; set; }
        public bool SupplierHasActiveInvitation { get; set; }
        public int InvitationStatusId { get; set; }
        public bool CanBuyIfInvitation { get; set; }
        public bool IsPublicCompetition { get; set; }
        public decimal ConditionalBookletFees { get; set; }

        public int StatusId { get; set; }
        public string StatusIdString { get; set; }
        public bool CanSupplierBuyBook { get; set; }
        public bool CanPurchase { get; set; }
        public string SadadNumber { get; set; }
        public decimal Price { get; set; }
        public decimal? ConditionsBookletPrice { get; set; }
        public DateTime EndDate { get; set; }
        public bool ConfirmPurchase { get; set; }
        public DateTime? PurshaseDate { get; set; }
        public string PurshaseDateString { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillBookletFees { get; set; }
        public decimal BillInvitationFees { get; set; }

        public decimal BillBookletPrice { get; set; }

        public string CompanyName { get; set; }
        public string TenderNumber { get; set; }
        public string TenderName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public string CommericalRegisterName { get; set; }
        public int PaymentStatusId { get; set; }
        public string PaymentMessage { get; set; }
        public decimal Total { get; set; }
        public string BillInvoiceNumber { get; set; }
        public ConditionBookletModel ConditionBookletModel { get; set; }
        public bool IsAvalaibleToPurachase { get; set; }
        public bool IsAvalaibleToApply { get; set; }
        public bool IsAvailabletoApplyOffer { get; set; }
        public bool IsAvailabletoBuy { get; set; }
        public bool IsTenderOwner { get; set; }
        public bool IsBillExpired { get; set; }

    }
    public class ConditionBookletModel
    {
        public int PaymentStatusId { get; set; }
        public string CommericalRegisterName { get; set; }
        public int ConditionsBookletId { get; set; }
        public int InvitationId { get; set; }
        public string SadadNumber { get; set; }
        public string PurchaseDateString { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillInvitationFees { get; set; }
        public decimal BillBookletFees { get; set; }
        public decimal BillBookletPrice { get; set; }
    }
}
