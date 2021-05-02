using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CreateAnnouncementSupplierTemplateModel
    {
        public int AnnouncementId { get; set; }
        public string CreatedBy { get; set; }
        public int? BranchId { get; set; }
        public string AgencyCode { get; set; }
        public string AnnouncementIdString { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseEnterAnnouncementName")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementSupplierTemplateName")]
        public string AnnouncementSupplierTemplateName { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IntroAboutAnnouncementTemplate")]
        public string IntroAboutAnnouncementTemplate { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "AnnouncementTemplateSuppliersListType")]
        public int? AnnouncementTemplateSuppliersListTypeId { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "InsideKSA")]
        public bool InsideKsa { get; set; } = true;

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Details")]
        public string Details { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Descriptions")]
        public string Descriptions { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ActivityDescription")]
        public string ActivityDescription { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public int TenderTypeId { get; set; }

        [MinLength(1, ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectActivities")]
        [Required(ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectActivities")]

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Activities")]
        public List<int> ActivityIds { get; set; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "MaintenanceOperationActions")]
        public List<int> MaintenanceRunnigWorkIds { get; set; } = new List<int>();

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "EstablishingActions")]
        public List<int> ConstructionWorkIds { get; set; } = new List<int>();

        [MinLength(1, ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectAreas")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Areas")]
        public List<int> AreasIds { get; set; } = new List<int>();

        [MinLength(1, ErrorMessageResourceType = typeof(Resources.AnnouncementResources.ErrorMessages), ErrorMessageResourceName = "PleaseSelectCountries")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "Countries")]
        public List<int> CountriesIds { get; set; } = new List<int>();

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "RequirementConditionsToJoinList")]
        public string RequirementConditionsToJoinList { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IsEffectiveList")]
        public bool IsEffectiveList { get; set; } = true;

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "RequiredAttachment")]
        public string RequiredAttachment { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "IsRequiredAttachmentToJoinList")]
        public bool IsRequiredAttachmentToJoinList { get; set; } = true;

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "EffectiveListDate")]
        public DateTime? EffectiveListDate { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "Address")]
        public string AgencyAddress { get; set; }
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AgencyPhone { get; set; }
        [Display(Name = "Fax", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string AgencyFax { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string AgencyEmail { get; set; }
        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "SupportingDocuments")]
        public string AttachmentReference { get; set; }
        public string ReferenceNumber { get; set; }
        public List<TenderAttachmentModel> Attachments { get; set; }

        public string AgencyName { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderType")]
        public string TenderTypesId { get; set; }
        public List<int> TenderItemTypes { get; set; } = new List<int>();
        public int CreatedById { get; set; }

        public string BranchName { get; set; }

        [Display(ResourceType = typeof(Resources.AnnouncementResources.DisplayInputs), Name = "EffectiveListTime")]
        public string EffectiveListTime { get; set; }
        public List<LookupModel> TenderTypes
        {
            get
            {
                return new List<LookupModel>() {
                    new LookupModel { Id = (int)Enums.TenderType.NewDirectPurchase, Name =  Resources.TenderResources.DisplayInputs.DirectPurshase } ,
                    new LookupModel { Id = (int)Enums.TenderType.LimitedTender, Name =  Resources.TenderResources.DisplayInputs.LimitedTender }
                    };
            }
        }
    }
}
