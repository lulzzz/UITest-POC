using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OtherBranchAddressModel
    {
        public int BranchAddressId { get; set; }
        public string BranchAddressTypeName { get; set; }
        public int BranchAddressTypeId { get; set; }
        public int BranchAddressIdString { get; set; }

        public string Position { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Address")]
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterAddressName")]

        [StringLength(500)]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Phone")]
        [StringLength(100)]
        //[RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        public string Phone { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Description")]
        [StringLength(500)]

        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "DescriptionRequired")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "PostalCode")]
        [StringLength(100)]
        public string PostalCode { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Fax")]
        [StringLength(100)]
        //[RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string Fax { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "CityCode")]
        public string CityCode { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        public string Phone2 { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        public string Fax2 { get; set; }
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        [StringLength(500)]
        public string AddressName { get; set; }


        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "ZipCode")]
        [StringLength(100)]
        public string ZipCode { get; set; }
    }
}
