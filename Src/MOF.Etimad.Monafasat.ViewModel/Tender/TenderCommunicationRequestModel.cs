using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderCommunicationRequestModel
    {
        public string TenderCommunicationRequestString { get; set; }
        public string TenderIdString { get; set; }
        public string TenderTypeIdString { get; set; }
        public bool CanAddPlaint { set; get; }
        public bool IsPurchased { set; get; }
        public bool IsNewNegotiation { set; get; }
        public bool IsSupplierCombined { set; get; }
        public int InvitationStatusId { set; get; }
        public int? InvitationTypeId { set; get; }
        public DateTime? LastOfferPresentationDate { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public int? TechnicalCommitteeId { get; set; }
        public int TenderStatusId
        {
            get => Util.Decrypt(TenderStatusIdString);

            set { }
        }
        public string TenderStatusIdString { get; set; }
        public int TenderTypeId
        {
            get => Util.Decrypt(TenderTypeIdString);
            set { }
        }
        public bool TenderHasExtendOffersValidity { get; set; }
        public bool CanAddExtendOffersValidity { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
        public bool SupplierHasOffers { get; set; }
        public bool SupplierUploadQualificationDoc { get; set; }
        public bool SupplierHasExtendRequest { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AgencyReply { get; set; }
        public DateTime ReplyDate { get; set; }
        public DateTime? OfferOpeningDate { get; set; }
        public DateTime? OfferCheckingDate { get; set; }
        public bool? IsLowBudgetDirectPurchase { get; set; }
        public int? DirectPurchaseMemberId { get; set; }
        public bool IsUserHasAccessToLowBudgetTender { get; set; }
        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }
    }
}
