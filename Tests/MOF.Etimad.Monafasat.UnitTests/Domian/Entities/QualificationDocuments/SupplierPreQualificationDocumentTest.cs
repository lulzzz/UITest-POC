using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.QualificationDocuments
{
    public class SupplierPreQualificationDocumentTest
    {
        private const string SupplierId = "1";
        private const int statusId = 1;
        private const int PreQualificationId = 1;
        private const int PreQualificationResult = 1;
        private const bool isActive = true;
        private const string RejectReason = "Reject Reason";

        private const string FileName = "File";
        private const string FileNetReferenceId = "";
        private const int SupplierPreQualificationDocumentId = 1;

        [Fact]
        public void Should_Construct_SupplierPreQualificationDocument()
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = new SupplierPreQualificationDocument(SupplierId, statusId, PreQualificationId, PreQualificationResult, isActive);
            _ = supplierPreQualificationDocument.PreQualificationResult;
            _ = supplierPreQualificationDocument.SupplierPreQualificationDocumentId;
            _ = supplierPreQualificationDocument.SupplierId;
            _ = supplierPreQualificationDocument.PreQualificationId;
            _ = supplierPreQualificationDocument.StatusId;
            _ = supplierPreQualificationDocument.RejectionReason;
            _ = supplierPreQualificationDocument.Supplier;
            _ = supplierPreQualificationDocument.PreQualification;
            _ = supplierPreQualificationDocument.Status;
            _ = supplierPreQualificationDocument.supplierPreQualificationAttachments;

            supplierPreQualificationDocument.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update_SupplierPreQualificationDocument()
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = new SupplierPreQualificationDocument(SupplierId, statusId, PreQualificationId, PreQualificationResult, isActive);
            supplierPreQualificationDocument.Update(SupplierId, PreQualificationId, SharedKernel.Enums.PreQSupplierDocumentStatus.Delivered);
            Assert.Equal((int)SharedKernel.Enums.PreQSupplierDocumentStatus.Delivered, supplierPreQualificationDocument.StatusId);
        }

        [Fact]
        public void Should_DeActive()
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = new SupplierPreQualificationDocument(SupplierId, statusId, PreQualificationId, PreQualificationResult, isActive);
            supplierPreQualificationDocument.DeActive();
            Assert.False(supplierPreQualificationDocument.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = new SupplierPreQualificationDocument(SupplierId, statusId, PreQualificationId, PreQualificationResult, isActive);
            supplierPreQualificationDocument.SetActive();
            Assert.True(supplierPreQualificationDocument.IsActive);
        }

        [Fact]
        public void Should_UpdatePreQualificationDocumentStatus()
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = new SupplierPreQualificationDocument(SupplierId, statusId, PreQualificationId, PreQualificationResult, isActive);
            supplierPreQualificationDocument.UpdatePreQualificationDocumentStatus(PreQualificationResult, RejectReason);
            Assert.Equal(PreQualificationResult, supplierPreQualificationDocument.PreQualificationResult);
        }

        [Fact]
        public void Should_UpdateAttachments()
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = new SupplierPreQualificationDocument(SupplierId, statusId, PreQualificationId, PreQualificationResult, isActive);
            SupplierPreQualificationAttachment supplierPreQualificationAttachment = new SupplierPreQualificationAttachment(FileName, FileNetReferenceId, SupplierPreQualificationDocumentId);
            List<SupplierPreQualificationAttachment> lstAttachments = new List<SupplierPreQualificationAttachment>() { supplierPreQualificationAttachment };
            supplierPreQualificationDocument.UpdateAttachments(lstAttachments);
            supplierPreQualificationDocument.UpdateAttachments(lstAttachments);
            supplierPreQualificationDocument.supplierPreQualificationAttachments.ShouldNotBeEmpty();
        }
    }
}
