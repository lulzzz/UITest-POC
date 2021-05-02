using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core
{
    [Table("SupplierBlock", Schema = "Block")]
    public class SupplierBlock : AuditableEntity
    {
        public SupplierBlock()
        {
        }
        public SupplierBlock(
            string commercialRegistrationNo,
            string commercialSupplierName,
            string resolutionNumber,
            int blockStatusId,
            int blockTypeId,
            string agencyId,
            DateTime? blockStartDate,
            DateTime? blockEndDate,
            string punishment,
            string blockDetails,
            string adminfileName,
            string adminFileNetReferenceId,
            string secretaryFileName,
            string secretaryFileNetReferenceId,
            string adminBlockreason,
            string secretaryBlockReason,
            int supplierTypeId,
            int organizationTypeId,
            string licenseNumber,
            string commertialRegistrationNoOrigin,
            bool isOldBlock)
        {
            CommercialRegistrationNo = commercialRegistrationNo;
            CommercialSupplierName = commercialSupplierName;
            ResolutionNumber = resolutionNumber;
            BlockStatusId = blockStatusId;
            AgencyCode = agencyId;
            BlockDetails = blockDetails;
            if (blockTypeId == 0)
                BlockTypeId = (int)Enums.BlockType.Tamporary;
            else
                BlockTypeId = blockTypeId;
            BlockStartDate = blockStartDate;
            BlockEndDate = blockEndDate;
            Punishment = punishment;
            AdminFileName = adminfileName;
            AdminFileNetReferenceId = adminFileNetReferenceId;
            SecretaryFileName = secretaryFileName;
            SecretaryFileNetReferenceId = secretaryFileNetReferenceId;
            AdminBlockReason = adminBlockreason;
            SecretaryBlockReason = secretaryBlockReason;
            IsActive = true;
            SupplierTypeId = supplierTypeId;
            OrganizationTypeId = organizationTypeId;
            LicenseNumber = licenseNumber;
            CommertialRegistrationNoOrigin = commertialRegistrationNoOrigin;
            IsOldBlock = isOldBlock;
            EntityCreated();
        }

        [Key]
        public int SupplierBlockId { get; private set; }
        [StringLength(200)]
        public string CommercialSupplierName { get; private set; }
        [StringLength(200)]
        public string AdminFileName { get; private set; }
        [StringLength(200)]
        public string AdminFileNetReferenceId { get; private set; }
        [StringLength(200)]
        public string SecretaryFileName { get; private set; }
        [StringLength(200)]
        public string SecretaryFileNetReferenceId { get; private set; }
        [StringLength(500)]
        public string BlockDetails { get; private set; }
        public int SupplierTypeId { get; private set; }
        [StringLength(20)]
        public string LicenseNumber { get; private set; }
        public bool IsOldBlock { get; private set; }
        public int OrganizationTypeId { get; private set; }
        [StringLength(20)]
        public string CommercialRegistrationNoOrigin { get; private set; }
        [StringLength(20)]
        public string ResolutionNumber { get; private set; }
        public DateTime? BlockStartDate { get; private set; }
        public DateTime? BlockEndDate { get; private set; }
        [StringLength(500)]
        public string AdminBlockReason { get; private set; }
        [StringLength(500)]
        public string SecretaryBlockReason { get; private set; }
        [StringLength(500)]
        public string ManagerRejectionReason { get; private set; }
        [StringLength(500)]
        public string SecretaryRejectionReason { get; private set; }
        [StringLength(20)]
        public string CommertialRegistrationNoOrigin { get; private set; }
        [StringLength(500)]
        public string Punishment { get; private set; }
        public bool IsTotallyBlocked { get; private set; } = false;
        [StringLength(20)]
        [ForeignKey("Supplier")]
        public string CommercialRegistrationNo { get; private set; }
        [StringLength(20)]
        [ForeignKey("Agency")]
        public string AgencyCode { get; private set; }
        [ForeignKey("BlockStatus")]
        public int BlockStatusId { get; private set; }
        [ForeignKey("BlockType")]
        public int? BlockTypeId { get; private set; }
        public BlockStatus BlockStatus { get; set; }
        public BlockType BlockType { get; private set; }
        public GovAgency Agency { get; set; }
        public Supplier Supplier { get; set; }

        public void UpdateBlock(
        string commercialRegistrationNo,
          string commercialSupplierName,
          string resolutionNumber,
          int blockStatusId,
          string agencyId,
          DateTime? blockStartDate,
          DateTime? blockEndDate,
          string punishment,
          string adminfileName,
          string adminFileNetReferenceId,
          string secretaryFileName,
          string secretaryFileNetReferenceId,
          string adminBlockreason,
          string secretaryBlockReason)
        {
            CommercialRegistrationNo = commercialRegistrationNo;
            CommercialSupplierName = commercialSupplierName;
            ResolutionNumber = resolutionNumber;
            BlockStatusId = blockStatusId;
            AgencyCode = agencyId;
            BlockStartDate = blockStartDate;
            BlockEndDate = blockEndDate;
            Punishment = punishment;
            AdminFileName = adminfileName;
            AdminFileNetReferenceId = adminFileNetReferenceId;
            SecretaryFileName = secretaryFileName;
            SecretaryFileNetReferenceId = secretaryFileNetReferenceId;
            AdminBlockReason = adminBlockreason;
            SecretaryBlockReason = secretaryBlockReason;
            IsActive = true;
            EntityUpdated();
        }

        public void SetManagerRejectionReason(string managerRejectionReason)
        {
            ManagerRejectionReason = managerRejectionReason;
            BlockStatusId = (int)Enums.BlockStatus.RejectedManager;
            EntityUpdated();
        }
        public void SetStatus(int statusId)
        {
            BlockStatusId = statusId;
            EntityUpdated();
        }

        public void TotallyBlocked()
        {
            IsTotallyBlocked = true;
            EntityUpdated();
        }
        public void SetSecretaryRejectionReason(string secretaryRejectionReason)
        {
            SecretaryRejectionReason = secretaryRejectionReason;
            BlockStatusId = (int)Enums.BlockStatus.RejectedSecertary;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void SetAgencyForTest()
        {
            Agency = new GovAgency ("agencycode","name",1,false,1,"mobile",null);
            EntityUpdated();
        }
    }
}
