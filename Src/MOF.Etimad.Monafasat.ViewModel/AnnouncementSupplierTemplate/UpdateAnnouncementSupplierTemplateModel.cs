using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class UpdateAnnouncementSupplierTemplateModel
    {
        public int AnnouncementId { get; set; }
        public string CreatedBy { get; set; }
        public int? BranchId { get; set; }
        public string AgencyCode { get; set; }
        public string AnnouncementIdString { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementTemplateSuppliersListType")]
        public int? AnnouncementTemplateSuppliersListTypeId { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IsEffectiveList")]
        public bool IsEffectiveList { get; set; } = true;
        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "EffectiveListDate")]
        public DateTime? EffectiveListDate { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.AnnouncementResources.ErrorMessages)), ErrorMessageResourceName = "EnterAgencyAddress")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Address")]
        public string AgencyAddress { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.AnnouncementResources.ErrorMessages)), ErrorMessageResourceName = "EnterPhoneNumber")]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AgencyPhone { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.AnnouncementResources.ErrorMessages)), ErrorMessageResourceName = "EnterFax")]
        [Display(Name = "Fax", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string AgencyFax { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.SharedResources.ErrorMessages), ErrorMessageResourceName = "EnterEmail")]
        [Display(Name = "Email", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "WrongEmail")]

        public string AgencyEmail { get; set; }


        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "SupportingDocuments")]
        public string AttachmentReference { get; set; }
        public List<TenderAttachmentModel> Attachments { get; set; }

        public string AgencyName { get; set; }

        public int CreatedById { get; set; }
    }
}

