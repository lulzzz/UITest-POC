using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class TechnicianReportAttachmentTest
    {
        private const int attachmentTypeId = 1;
        private const string name = "file name";
        private const string fileNetReferenceId = "22";

        [Fact]
        public void Should_Construct_SupplierTenderQuantityTableItemJson()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            _ = new TechnicianReportAttachment();

            technicianReportAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            technicianReportAttachment.Update("New Name");
            technicianReportAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            technicianReportAttachment.DeActive();
            Assert.False(technicianReportAttachment.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            technicianReportAttachment.SetActive();
            Assert.True(technicianReportAttachment.IsActive);
        }

        [Fact]
        public void Should_UpdatefileNetReferenceId()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            var newfileNetReferenceId = "222";
            technicianReportAttachment.UpdatefileNetReferenceId(newfileNetReferenceId);
            Assert.Equal(newfileNetReferenceId, technicianReportAttachment.FileNetReferenceId);
        }

        [Fact]
        public void Should_SetAddedState()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            technicianReportAttachment.SetAddedState();
            Assert.Equal(0, technicianReportAttachment.AttachmentId);
        }

        [Fact]
        public void Should_DeleteAttachment()
        {
            TechnicianReportAttachment technicianReportAttachment = new TechnicianReportAttachment(name, fileNetReferenceId, attachmentTypeId);
            technicianReportAttachment.DeleteAttachment();
            Assert.Equal(ObjectState.Deleted, technicianReportAttachment.State);
        }


    }
}
