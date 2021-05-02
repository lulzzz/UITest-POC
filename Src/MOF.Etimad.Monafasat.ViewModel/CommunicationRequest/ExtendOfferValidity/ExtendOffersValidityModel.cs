using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOffersValidityModel
    {
        public bool IsTenderHasExecludedReasonOrAwardingValue { get; set; }
        public int AgencyRequestId { get; set; }
        public string AgencyRequestIdString { get; set; }
        public int TenderId { get; set; }
        public int ExtendOfferValidityId { get; set; }
        public string TenderIdString { get; set; }
        public int TenderTypeId { get; set; }
        public string AgencyId { get; set; }
        public int? AgencyRequestStatusId { get; set; }
        [MaxLength(250)]
        [Display(Name = "RejectionReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Required(ErrorMessageResourceName = "RejectionReason", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public string RejectionReason { get; set; }
        public TenderBasicInfoModel TenderBasicInfoModel { get; set; }
        public ExtendOffersValiditySavingModel ExtendOffersValiditySavingModel { get; set; }
        public int StatusId { get; set; }
        public bool? IsLowBudgetDirectPurchase { get; set; }
        public int? DirectPurchaseMemberId { get; set; }
    }
}
