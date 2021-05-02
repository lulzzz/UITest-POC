using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{

    public class TenderDatesModel
    {
        public int TenderId { get; set; }
        public int RevisionDateId { get; set; }
        public string TenderIdString { get; set; }
        public int TenderStatusId { get; set; }
        public int TenderTypeId { get; set; }
        public int? InvitationTypeId { get; set; }
        public bool IsAgencyRelatedByVRO { get; set; }
        public bool IsExceptedAgency { get; set; }

        public int? PreQualificationId { get; set; }

        public bool ShowInvitationTab { get; set; }
        public string VROOfficeCode { get; set; }
        public int? TenderCreatedByTypeId { get; set; }

        public string VRORelatedBranchAgencyCode { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastEnqueriesDate")]
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = typeof(Resources.QualificationResources.ErrorMessages), ErrorMessageResourceName = "EnterQualificationEnquiryDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastEnqueriesDate { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterLastOfferPresentationDate")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? LastOfferPresentationDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterLastOfferPresentationTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "LastOfferPresentationTime")]
        public string LastOfferPresentationTime { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningDate")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OffersOpeningDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersOpeningTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersOpeningTime")]
        public string OffersOpeningTime { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.Messages)), ErrorMessageResourceName = "EnterOffersCheckingData")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersCheckingDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OffersCheckingDate { get; set; }

        //[Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersCheckingTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersCheckingTime")]
        public string OffersCheckingTime { get; set; }
        public string LastEnqueriesDateString { get; set; }

        public string LastEnqueriesDateHijriString { get; set; }
        public string LastOfferPresentationDateString { get; set; }
        public string LastOfferPresentationDateHijriString { get; set; }
        public string OffersOpeningDateString { get; set; }
        public string OffersOpeningDateHijriString { get; set; }
        public string OffersCheckingDateString { get; set; }
        public string OffersCheckingDateHijriString { get; set; }
        public List<VacationsDateModel> Vacations { get; set; }
        public TenderRevisionDateModel TenderRevisionDate { get; set; }
        public int TenderChangeRequestId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public string RejectionReason { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public int? OfferPresentationWayId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "NeddedInitialGurantee")]
        public bool NeedInitialGuarantee { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "InitialGuranteePresentationAddress")]
        public string InitialGuaranteeAddress { get; set; }
        [Display(Name = "InitialGuaranteePercentage", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public decimal? InitialGuaranteePercentage { get; set; }

        [Display(Name = "FinalGuaranteePercentage", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public decimal? FinalGuaranteePercentage { get; set; }

        [Display(Name = "StoppingPeriod", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? AwardingStoppingPeriod { get; set; } = 5;

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SupplierNeedSample")]
        public bool? SupplierNeedSample { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SamplesDeliveryAddress")]
        public string SamplesDeliveryAddress { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public int? OffersOpeningAddressId { get; set; }
        public List<AddressModel> OffersOpeningAddressList { get; set; }
       
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OpenOfferLocation")]
        public string OffersOpeningAddress { get; set; }


        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterBuildingName")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "BuildingName")]
        public string BuildingName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterFloarNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "FloarNumber")]
        public string FloarNumber { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterDepartmentName")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DepartmentName")]
        public string DepartmentName { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterDeliveryDate")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DeliveryDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterDeliveryTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DeliveryTime")]
        public string DeliveryTime { get; set; }

        public string DeliveryDateString { get; set; }
        public string DeliveryDateHijriString { get; set; }
 
        public int? QuantityTableVersionId { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "AwardingExpectedDateRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AwardingExpectedDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AwardingExpectedDate { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "StartingBusinessOrServicesDateRequired")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "StartingBusinessOrServicesDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartingBusinessOrServicesDate { get; set; }
        public DateTime? ParticipationConfirmationLetterDate { get; set; }
        public DateTime? StartingSendingEnquiries { get; set; }
         
         
        public int? VersionId { get; set; }

        public bool IsOldTender
        {
            get => TenderTypeId == (int)Enums.TenderType.CurrentTender || TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects;
        }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "IsSampleAddresSameOffersAddress")]
        public bool IsSampleAddresSameOffersAddress { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "OfferDeliveryAddressRequired")]

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferDeliveryAddress")]
        public string OfferDeliveryAddress { get; set; }
        
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterBuildingName")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "BuildingName")]
        public string OfferBuildingName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterFloarNumber")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "FloarNumber")]
        public string OfferFloarNumber { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterDepartmentName")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DepartmentName")]
        public string OfferDepartmentName { get; set; }


        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterDeliveryDate")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferDeliveryDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OffersDeliveryDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterDeliveryTime")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OfferDeliveryTime")]
        public string OffersDeliveryTime { get; set; }

         public int? MaxTimeToAnswerQuestions { get; set; }

    }
}