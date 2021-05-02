using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CommitteeModel
    {
        [Display(Name = "IndexNumber", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public int CommitteeId { get; set; }
        //[DisplayName("رقم تسلسل نوع اللجنة")]
        [Display(Name = "TypeIndexNumber", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public int CommitteeTypeId { get; set; }
        //[DisplayName("نوع اللجنة")]
        [Display(Name = "CommitteeType", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string CommitteeTypeName { get; set; }
        //[DisplayName("رقم تسلسل الفرع")]
        //[Display(Name = "CommitteeType", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        //public int OrganizationBranchId { get; set; }
        //[DisplayName("اسم الفرع")]
        //public string OrganizationBranchName { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "CommitteNameRequired")]
        [Display(Name = "CommitteeName", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        //[DisplayName("اسم اللجنة")]
        public string CommitteeName { get; set; }
        //[DisplayName("العنوان")]
        [Display(Name = "Address", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string Address { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        //[RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType =  (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        public string Phone { get; set; }
        //[RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        [StringLength(100)]
        [Display(Name = "Fax", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string Fax { get; set; }

        [StringLength(200)]
        [EmailAddress(ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "EmailInvalid")]
        [Display(Name = "Email", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        //[DisplayName("البريد الالكترونى")]
        public string Email { get; set; }
        //[DisplayName("صندوق البريد")]
        [Display(Name = "PostalCode", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string PostalCode { get; set; }
        //[DisplayName("الرمز البريدى")]
        [Display(Name = "ZipCode", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string ZipCode { get; set; }
        public string CommitteeTypeIdString { get; set; }
        public string CommitteeIdString { get; set; }
        public string AgencyCode { get; set; }

        public string RelatedAgencyCode { get; set; }
        public string CommitteeTypeTitle { get; set; }
        public System.DateTime? CreatedAt { get; set; }
        public List<int> BranchIds { get; set; }
    }
}