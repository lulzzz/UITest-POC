using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MainBranchAddressModel
    {
        public int BranchAddressId { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterPosition")]
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Place")]
        [StringLength(500)]
        public string Position { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterAddressName")]
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "MainAddress")]
        public string Address { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterPhone")]
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Phone1")]
        [StringLength(100)]
        // [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterFax")]
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Fax1")]
        [StringLength(100)]
        //  [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string Fax { get; set; }


        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Phone2")]
        //  [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        public string Phone2 { get; set; }


        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Fax2")]
        // [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string Fax2 { get; set; }

        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Email")]
        [StringLength(100)]
        [Required(ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "EnterEmail")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessageResourceType = (typeof(Resources.BranchResources.ErrorMessages)), ErrorMessageResourceName = "InvalidMailAddress")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        [StringLength(500)]
        public string AddressName { get; set; }

        public string CityCode { get; set; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "PostalCode")]
        [StringLength(100)]
        public string PostalCode { get; set; }

        [Display(ResourceType = typeof(Resources.CommitteeResources.DisplayInputs), Name = "UsersNames")]
        [StringLength(100)]
        public string ZipCode { get; set; }
    }
}
