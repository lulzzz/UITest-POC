using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOffersValiditySavingModel
    {
        public int ExtendOffersValidityId { get; set; }
        public int AgencyRequestId { get; set; }
        public string AgencyRequestIdString { get; set; }

        [Display(Name = "OffersDuration", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(0, 90, ErrorMessageResourceName = "MustNotExceed90Days", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Required(ErrorMessageResourceName = "RequiredOffersDuration", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public int OffersDuration { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "NewOffersExpiryDate", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]

        public DateTime NewOffersExpiryDate { get; set; } = DateTime.Now;

        [MaxLength(250)]
        [Display(Name = "ExtendOffersReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Required(ErrorMessageResourceName = "RequiredExtendOffersReason", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public string ExtendOffersReason { get; set; }

        [Display(Name = "ReplyReceivingDurationDays", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Range(10, 90, ErrorMessageResourceName = "ReplyReceivingDurationDaysMustBeGreaterThanTen", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        [Required(ErrorMessageResourceName = "EnterReplyReceivingDuration", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public int? ReplyReceivingDurationDays { get; set; } = 10;

        [Display(Name = "ReplyReceivingDurationTime", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]

        [Required(ErrorMessageResourceName = "ExtendOfferValidtiyRequiredReplyRecieveDate", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public string ReplyReceivingDurationTime { get; set; }
        public string TenderIdString { get; set; }
        public int TenderId { get; set; }
        public int AgencyId { get; set; }
        public int TenderRequestsCount { get; set; }
        public int TotalOffersDuration { get; set; }
        public int? AgencyRequestStatusId { get; set; }
        public string NewOffersExpiryDateString { get; set; }
        public string NewOffersExpiryDateHijriString { get; set; }
        public bool? IsLowBudgetDirectPurchase { get; set; }
        public int? DirectPurchaseMemberId { get; set; }

    }
}
