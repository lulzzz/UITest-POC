using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationDocuments
{
    public class SupplierPreQualificationAttachmentTest
    {

        private const string FileName = "File";
        private const string FileNetReferenceId = "";
        private const int SupplierPreQualificationDocumentId = 1;


        [Fact]
        public void Should_Construct_SupplierPreQualificationAttachment()
        {
            SupplierPreQualificationAttachment supplierPreQualificationAttachment = new SupplierPreQualificationAttachment(FileName, FileNetReferenceId, SupplierPreQualificationDocumentId);
            supplierPreQualificationAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            SupplierPreQualificationAttachment supplierPreQualificationAttachment = new SupplierPreQualificationAttachment(FileName, FileNetReferenceId, SupplierPreQualificationDocumentId);
            supplierPreQualificationAttachment.DeActive();
            Assert.False(supplierPreQualificationAttachment.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            SupplierPreQualificationAttachment supplierPreQualificationAttachment = new SupplierPreQualificationAttachment(FileName, FileNetReferenceId, SupplierPreQualificationDocumentId);
            supplierPreQualificationAttachment.SetActive();
            Assert.True(supplierPreQualificationAttachment.IsActive);
        }


    }
}
