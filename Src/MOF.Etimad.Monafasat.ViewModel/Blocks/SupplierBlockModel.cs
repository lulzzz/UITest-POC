using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierBlockModel
    {

        public bool EnabledSearchByDate { get; set; }
        public bool IsSecretaryNotify { get; set; }
        public int SupplierBlockId { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialRegistrationNo")]
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]//, ErrorMessage =typeof( Resources.SharedResources.ErrorMessages.LessFourty))]
        public string CommercialRegistrationNo { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterCommercialSupplierName")]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]//, ErrorMessage =typeof( Resources.SharedResources.ErrorMessages.LessFourty))]
        public string CommercialSupplierName { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "InvalidNumber")]
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterResolutionNumber")]
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string ResolutionNumber { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockTypeId")]
        public int BlockTypeId { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string BlockStatusName { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string BlockTypeName { get; set; }

        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockDetails")]
        public string BlockDetails { get; set; }


        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockStartDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> BlockStartDate { get; set; }
        public string BlockStartDateString { get; set; }


        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterBlockEndDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> BlockEndDate { get; set; }
        public string BlockEndDateString { get; set; }

        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterViolation")]
        public string Violation { get; set; }

        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterPunishment")]
        public string Punishment { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        public string OfficialBasisDefamation { get; set; }

        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string AgencyCode { get; set; }

        public int BranchId { get; set; }
        public int BlockStatusId { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string CreatedBy { get; set; }

        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AgencyName { get; set; }

        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterAttachment")]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AttachmentIdRef { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string FileName { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]

        public string FileNetId { get; set; }

        public string SupplierBlockIdString { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]

        public string FileNetReferenceId { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]

        public string AdminBlockReason { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]

        public string SecretaryBlockReason { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]

        public string ManagerRejectionReason { get; set; }
        [StringLength(500, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]

        public string SecretaryRejectionReason { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string BlockDuration { get; set; }

        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AdminFileName { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string AdminFileNetReferenceId { get; set; }
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string SecretaryFileName { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "EnterAttachment")]
        [StringLength(200, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less200")]
        public string SecretaryFileNetReferenceId { get; set; }


        public int? AgencyId { get; set; }
        public bool IsOldSystem { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "SelectOrganizationTypeId")]
        public int OrganizationTypeId { get; set; }
        [Required(ErrorMessageResourceType = (typeof(Resources.BlockResources.ErrorMessages)), ErrorMessageResourceName = "SelectSupplierTypeId")]
        public int SupplierTypeId { get; set; }
        public object OrganizationName { get; set; }

        public string ToDateString { get; set; }
        public string FromDateString { get; set; }
        public string BlockEndDateHijri { get; set; }
        public string BlockStartDateHijri { get; set; }
        public bool IsTotallyBlocked { get; set; }
        public string CommercialSupplierNameEncrypted { get; set; }
        public string CommercialRegistrationNoEncrypted { get; set; }
    }
    public class SupplierAgencyBlockModel
    {
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string CommercialRegistrationNo { get; set; }
        [StringLength(20, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less20")]
        public string AgencyCode { get; set; }
    }
}
