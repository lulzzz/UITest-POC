using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using System;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Blocks
{
    public class SupplierBlockTests
    {
        private const string commercialSupplierName = "commercialSupplierName";
        private const string resolutionNumber = "commercialSupplierName";
        private const int blockStatusId = 1;
        private const int blockTypeId = 1;
        private const string agencyId = "commercialSupplierName";
        private const string adminfileName = "adminfileName";
        private const string adminBlockreason = "adminBlockreason";
        private const string secretaryBlockReason = "secretaryBlockReason";
        private const string adminFileNetReferenceId = "adminFileNetReferenceId";
        private const string blockDetails = "blockDetails";
        private const string punishment = "punishment";
        private const string secretaryFileName = "secretaryFileName";
        private const string secretaryFileNetReferenceId = "secretaryFileNetReferenceId";
        private const int organizationTypeId = 1;
        private const int supplierTypeId = 1;
        private const string commertialRegistrationNoOrigin = "NoOrigin";
        private const string licenseNumber = "licenseNumber";
        private const bool isOldBlock = false;
        private const string commercialRegistrationNo = "commercialRegistrationNo";
        private readonly DateTime? _blockEndDate = DateTime.Today.AddDays(3);
        private readonly DateTime? _blockStartDate = DateTime.Today;

        [Fact]
        public void Should_Construct_SupplierBlock()
        {
            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.ShouldNotBeNull();
            supplierBlock.CommercialRegistrationNo.ShouldBe(commercialRegistrationNo);
            supplierBlock.CommercialSupplierName.ShouldBe(commercialSupplierName);
            supplierBlock.ResolutionNumber.ShouldBe(resolutionNumber);
            supplierBlock.BlockStatusId.ShouldBe(blockStatusId);
            supplierBlock.BlockTypeId.ShouldBe(blockTypeId);
            supplierBlock.AgencyCode.ShouldBe(agencyId);
            supplierBlock.BlockStartDate.ShouldBe(_blockStartDate);
            supplierBlock.BlockEndDate.ShouldBe(_blockEndDate);
            supplierBlock.Punishment.ShouldBe(punishment);
            supplierBlock.BlockDetails.ShouldBe(blockDetails);
            supplierBlock.AdminFileName.ShouldBe(adminfileName);
            supplierBlock.AdminFileNetReferenceId.ShouldBe(adminFileNetReferenceId);
            supplierBlock.AdminBlockReason.ShouldBe(adminBlockreason);
            supplierBlock.SecretaryFileName.ShouldBe(secretaryFileName);
            supplierBlock.SecretaryFileNetReferenceId.ShouldBe(secretaryFileNetReferenceId);
            supplierBlock.SecretaryBlockReason.ShouldBe(secretaryBlockReason);
            supplierBlock.SupplierTypeId.ShouldBe(supplierTypeId);
            supplierBlock.OrganizationTypeId.ShouldBe(organizationTypeId);
            supplierBlock.LicenseNumber.ShouldBe(licenseNumber);
            supplierBlock.CommertialRegistrationNoOrigin.ShouldBe(commertialRegistrationNoOrigin);
            supplierBlock.IsOldBlock.ShouldBe(isOldBlock);
        }

        [Fact]
        public void Should_UpdateBlock()
        {
            var updatedcommercialRegistrationNo = "updated";
            var updatedcommercialSupplierName = "updated";
            var updatedresolutionNumber = "updated";
            var updatedblockStatusId = 3;
            var updatedagencyId = "updated";
            DateTime? updatedblockStartDate = DateTime.Today.AddDays(4);
            DateTime? updatedblockEndDate = DateTime.Today.AddDays(6);
            var updatedpunishment = "updated";
            var updatedadminfileName = "updated";
            var updatedadminFileNetReferenceId = "updated";
            var updatedsecretaryFileName = "updated";
            var updatedsecretaryFileNetReferenceId = "updated";
            var updatedadminBlockreason = "updated";
            var updatedsecretaryBlockReaso = "updated";

            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.UpdateBlock(updatedcommercialRegistrationNo, updatedcommercialSupplierName,
                updatedresolutionNumber, updatedblockStatusId, updatedagencyId, updatedblockStartDate, updatedblockEndDate,
                updatedpunishment, updatedadminfileName, updatedadminFileNetReferenceId,
                updatedsecretaryFileName, updatedsecretaryFileNetReferenceId,
                updatedadminBlockreason, updatedsecretaryBlockReaso);

            supplierBlock.ShouldNotBeNull();
            supplierBlock.CommercialRegistrationNo.ShouldBe(updatedcommercialRegistrationNo);
            supplierBlock.CommercialSupplierName.ShouldBe(updatedcommercialSupplierName);
            supplierBlock.ResolutionNumber.ShouldBe(updatedresolutionNumber);
            supplierBlock.BlockStatusId.ShouldBe(updatedblockStatusId);
            supplierBlock.AgencyCode.ShouldBe(updatedagencyId);
            supplierBlock.BlockStartDate.ShouldBe(updatedblockStartDate);
            supplierBlock.BlockEndDate.ShouldBe(updatedblockEndDate);
            supplierBlock.Punishment.ShouldBe(updatedpunishment);
            supplierBlock.AdminFileName.ShouldBe(updatedadminfileName);
            supplierBlock.AdminFileNetReferenceId.ShouldBe(updatedadminFileNetReferenceId);
            supplierBlock.AdminBlockReason.ShouldBe(updatedadminBlockreason);
            supplierBlock.SecretaryFileName.ShouldBe(updatedsecretaryFileName);
            supplierBlock.SecretaryFileNetReferenceId.ShouldBe(updatedsecretaryFileNetReferenceId);
            supplierBlock.SecretaryBlockReason.ShouldBe(updatedsecretaryBlockReaso);
            supplierBlock.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_SetManagerRejectionReason()
        {
            var managerReason = "reason updated";

            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.SetManagerRejectionReason(managerReason);

            supplierBlock.ShouldNotBeNull();
            supplierBlock.ManagerRejectionReason.ShouldBe(managerReason);
            supplierBlock.BlockStatusId.ShouldBe((int)Enums.BlockStatus.RejectedManager);
        }
        [Fact]
        public void Should_SetStatus()
        {

            var status = 3;
            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.SetStatus(status);

            supplierBlock.ShouldNotBeNull();
            supplierBlock.BlockStatusId.ShouldBe(status);
        }

        [Fact]
        public void Should_TotallyBlocked()
        {
            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.TotallyBlocked();

            supplierBlock.IsTotallyBlocked.ShouldBeTrue();
        }

        [Fact]
        public void Should_SetSecretaryRejectionReason()
        {
            var secretaryRejectionReason = "reason updated";
            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.SetSecretaryRejectionReason(secretaryRejectionReason);

            supplierBlock.ShouldNotBeNull();
            supplierBlock.SecretaryRejectionReason.ShouldBe(secretaryRejectionReason);
            supplierBlock.BlockStatusId.ShouldBe((int)Enums.BlockStatus.RejectedSecertary);
        }
        [Fact]
        public void Should_DeActive()
        {
            var supplierBlock = new SupplierBlock(commercialRegistrationNo,
                commercialSupplierName,
                resolutionNumber,
                blockStatusId,
                blockTypeId,
                agencyId,
                _blockStartDate,
                _blockEndDate,
                punishment,
                blockDetails,
                adminfileName,
                adminFileNetReferenceId,
                secretaryFileName,
                secretaryFileNetReferenceId,
                adminBlockreason,
                secretaryBlockReason,
                supplierTypeId,
                organizationTypeId,
                licenseNumber,
                commertialRegistrationNoOrigin,
                isOldBlock);

            supplierBlock.DeActive();

            supplierBlock.ShouldNotBeNull();
            supplierBlock.IsActive.ShouldNotBeNull();
            supplierBlock.IsActive.Value.ShouldBeFalse();
        }
    }
}