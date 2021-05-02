using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class SupplierAttachmentTest
    {
        private const string fileName = "File";
        private const string fileNetReferenceId = "111";

        [Fact]
        public void Should_Construct_SupplierOriginalAttachment()
        {
            _ = new SupplierOriginalAttachment();
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            _ = supplierOriginalAttachment.Offer;
            supplierOriginalAttachment.ShouldNotBeNull();

        }

        [Fact]
        public void Should_Construct_SupplierTechnicalProposalAttachment()
        {
            _ = new SupplierTechnicalProposalAttachment();
            SupplierTechnicalProposalAttachment supplierTechnicalProposalAttachment = new SupplierTechnicalProposalAttachment(fileName, fileNetReferenceId);
            supplierTechnicalProposalAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierFinancialProposalAttachment()
        {
            _ = new SupplierFinancialProposalAttachment();
            SupplierFinancialProposalAttachment supplierFinancialProposalAttachment = new SupplierFinancialProposalAttachment(fileName, fileNetReferenceId);
            supplierFinancialProposalAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierFinancialandTechnicalProposalAttachment()
        {
            _ = new SupplierFinancialandTechnicalProposalAttachment();
            SupplierFinancialandTechnicalProposalAttachment supplierFinancialandTechnicalProposalAttachment = new SupplierFinancialandTechnicalProposalAttachment(fileName, fileNetReferenceId);
            supplierFinancialandTechnicalProposalAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Construct_SupplierCombinedAttachment()
        {
            _ = new SupplierCombinedAttachment();
            SupplierCombinedAttachment supplierCombinedAttachment = new SupplierCombinedAttachment(fileName, fileNetReferenceId);
            supplierCombinedAttachment.ShouldNotBeNull();
        }
         

        [Fact]
        public void Should_UpdatePHPData()
        {
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            supplierOriginalAttachment.UpdatePHPData(fileNetReferenceId);
            Assert.Equal(supplierOriginalAttachment.FileNetReferenceId, fileNetReferenceId);
        }

        [Fact]
        public void Should_UpdateFileName()
        {
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            supplierOriginalAttachment.UpdateFileName(fileName);
            Assert.Equal(supplierOriginalAttachment.FileName, fileName);
        }

        [Fact]
        public void Should_DeActive()
        {
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            supplierOriginalAttachment.DeActive();
            Assert.False(supplierOriginalAttachment.IsActive);
        }

        [Fact]
        public void Should_Delete()
        {
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            supplierOriginalAttachment.Delete();
            Assert.Equal(ObjectState.Deleted, supplierOriginalAttachment.State);
        }

        [Fact]
        public void Should_SetActive()
        {
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            supplierOriginalAttachment.SetActive();
            Assert.True(supplierOriginalAttachment.IsActive);
        }
        [Fact]
        public void Should_DeleteAttachment()
        {
            SupplierOriginalAttachment supplierOriginalAttachment = new SupplierOriginalAttachment(fileName, fileNetReferenceId);
            supplierOriginalAttachment.DeleteAttachment();
            Assert.Equal(ObjectState.Deleted, supplierOriginalAttachment.State);
        }

    }
}
