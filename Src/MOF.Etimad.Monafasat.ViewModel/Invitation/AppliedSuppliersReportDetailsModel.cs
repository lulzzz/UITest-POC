using System;

namespace MOF.Etimad.Monafasat.ViewModel.Invitation
{
    public class AppliedSuppliersReportDetailsModel
    {
        public string CommericalRegisterNo { get; set; }
        public string CompanyName { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public DateTime? SendingDate { get; set; }
        public DateTime? InvitationAcceptanceDate { get; set; }
        public DateTime? InvitationRejectionDate { get; set; }
        public DateTime? InvitationWithdrawalDate { get; set; }
        public DateTime? OfferWithdrawalDate { get; set; }
        public DateTime? InvitationSendingDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string InvitationStatusName { get; set; }
        public string PurchaseStatusName { get; set; }
        public bool? CanSeeDetails { get; set; }
        public int OfferStatusId { get; set; }
        public string OfferStatusName { get; set; }
        public int OfferId { get; set; }
        public string offerIdString { get; set; }

        //will delete
        public string InvitationTypeName { get; set; }
        public int? InvitationTypeId { get; set; }
        public int InvitationId { get; set; }
        public int TenderId { get; set; }




    }
}
