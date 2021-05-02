using System.ComponentModel.DataAnnotations;
namespace MOF.Etimad.Monafasat.ViewModel
{
    public class AdminSupplierBlockModel
    {
        public int SupplierBlockId { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialRegistrationNo")]
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string CommercialRegistrationNo { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialRegistrationNo")]
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string CommercialRegistrationNoOrigin { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialSupplierName")]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string CommercialSupplierName { get; set; }
        //[Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialRegistrationNo")]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AdminFileName { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterFile")]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AdminFileNetReferenceId { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string BlockStatusName { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        public string AdminBlockReason { get; set; }
        public int? AgencyId { get; set; }
        public int BlockStatusId { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AttachmentIdRef { get; set; }
        public string SupplierBlockIdString { get; set; }
        public bool IsOldSystem { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "SelectOrganizationTypeId")]
        public int OrganizationTypeId { get; set; }
        public string OrganizationName { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "SelectSupplierTypeId")]
        public int SupplierTypeId { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterLicenseNumber")]
        [StringLength(40, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less40")]
        public string LicenseNumber { get; set; }


        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockDetails")]
        public string BlockDetails { get; set; }
        public string ToDateString { get; set; }
        public string FromDateString { get; set; }
        public string CreatedBy { get; set; }

        #region unwanred


        //   public int SupplierBlockId { get; set; }

        //   [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialRegistrationNo")]
        //   public string CommercialRegistrationNo { get; set; }

        //   [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialSupplierName")]
        //   public string CommercialSupplierName { get; set; }

        //   [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterResolutionNumber")]
        //   public int? ResolutionNumber { get; set; }

        //  // [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockTypeId")]
        //   public int BlockTypeId { get; set; }

        //   public string BlockTypeName { get; set; }

        // //  [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockDetails")]
        //   public string BlockDetails { get; set; }

        // //  [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockStartDate")]
        //   public Nullable<DateTime> BlockStartDate { get; set; }
        //   public string BlockStartDateString { get; set; }

        //  // [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockEndDate")]
        //   public Nullable<DateTime> BlockEndDate { get; set; }
        //   public string BlockEndDateString { get; set; }

        //  // [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterViolation")]
        //   public string Violation { get; set; }

        ////   [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterPunishment")]
        //   public string Punishment { get; set; }

        // //  [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterOfficialBasisDefamation")]
        //   public string OfficialBasisDefamation { get; set; }

        //   public int? AgencyId { get; set; }

        //   public bool? IsActive { get; set; }

        //   public string CreatedBy { get; set; }



        //   [Display(Name = "نسخة من قرار المنع")]
        //   public string AttachmentIdRef { get; set; }

        //   public string FileName { get; set; }

        //   public string FileNetId { get; set; } 
        //   public string SupplierBlockIdString { get; set; }

        //   public string FileNetReferenceId { get; set; }
        #endregion
    }
}
