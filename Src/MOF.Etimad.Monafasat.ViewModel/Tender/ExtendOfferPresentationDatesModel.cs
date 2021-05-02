using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ExtendOfferPresentationDatesModel
    {
        public int TenderId { get; set; }
        public int RevisionDateId { get; set; }
        public string TenderIdString { get; set; }
        public int TenderStatusId { get; set; }
        public int TenderTypeId { get; set; }
        public bool ShowInvitationTab { get; set; }
        public int? PreQualificationId { get; set; }
        public int? OffersOpeningAddressId { get; set; }

        public string AgencyId { get; set; }

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
        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersCheckingData")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "OffersCheckingDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? OffersCheckingDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.TenderResources.ErrorMessages)), ErrorMessageResourceName = "EnterOffersCheckingTime")]
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
        public int AgencyRequestId { get; set; }
        public string AgencyRequestIdString { get; set; }

    }
}
