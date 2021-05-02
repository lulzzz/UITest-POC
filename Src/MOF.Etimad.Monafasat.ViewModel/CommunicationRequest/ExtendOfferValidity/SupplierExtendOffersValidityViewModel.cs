using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierExtendOffersValidityViewModel
    {
        public string SupplierExtendOffersValidityId { get; set; }
        public string ExtendOffersValidityIdString { get; set; }
        public string TenderIdString { get; set; }
        public string TenderTypeName { get; set; }
        public string TenderName { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string AgencyName { get; set; }
        public int SupplierExtendOffersValidityStatusId { get; set; }
        public bool IsFileUploaded { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "NewOffersExpiryDate", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public DateTime NewOffersExpiryDate { get; set; } = DateTime.Now;
        public int OffersValidityDays { get; set; }

        [Display(Name = "ExtendOffersReason", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        public string ExtendOffersReason { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "PurposeValiation", ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), MinimumLength = 40)]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderPurpose")]
        public string TenderPurpose { get; set; }

        public ExtendOffersValidityAttachementViewModel ExtendOffersValidityAttachementViewModel { get; set; }

        [Display(Name = "InitialGuarantee", ResourceType = typeof(Resources.CommunicationRequest.DisplayInputs))]
        [Required(ErrorMessageResourceName = "RequiredInitialGuarantee", ErrorMessageResourceType = typeof(Resources.CommunicationRequest.ErrorMessages))]
        public string InitialGuaranteerefId { get; set; } = "";
        public int AgencyRequestStatusId { get; set; }
    }
}
