using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementSuppliersTemplateAttachmentTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementSuppliersTemplateAttachment()
        {
            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment();

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.AttachmentType.ShouldBeNull();
            announcementSuppliersTemplateAttachment.AnnouncementId.ShouldBe(0);
            announcementSuppliersTemplateAttachment.AnnouncementSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementSuppliersTemplateAttachment()
        {
            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = Enums.AttachmentType.SupplierOriginalAttachment;

            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, (int)attachmentType);

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.Name.ShouldBe(name);
            announcementSuppliersTemplateAttachment.FileNetReferenceId.ShouldBe(fileNetReferenceId);
            announcementSuppliersTemplateAttachment.AttachmentTypeId.ShouldBe((int)attachmentType);
            announcementSuppliersTemplateAttachment.ChangeStatusId.ShouldBeNull();
            announcementSuppliersTemplateAttachment.ReviewStatusId.ShouldBeNull();
        }

        [Fact]
        public void Should_DeActive_AnnouncementSuppliersTemplateAttachment()
        {
            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = Enums.AttachmentType.SupplierOriginalAttachment;

            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, (int)attachmentType);

            announcementSuppliersTemplateAttachment.DeActive();

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_UpdateChangStatus_AnnouncementSuppliersTemplateAttachment()
        {
            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = Enums.AttachmentType.SupplierOriginalAttachment;

            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, (int)attachmentType);

            announcementSuppliersTemplateAttachment.UpdateChangStatus(1, 1, "rejectionReason");

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.ChangeStatusId.ShouldBe(1);
            announcementSuppliersTemplateAttachment.ReviewStatusId.ShouldBe(1);
        }

        [Fact]
        public void Should_UpdatePHPData_AnnouncementSuppliersTemplateAttachment()
        {
            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = Enums.AttachmentType.SupplierOriginalAttachment;

            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, (int)attachmentType);

            announcementSuppliersTemplateAttachment.UpdatePHPData("456");

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.FileNetReferenceId.ShouldBe("456");
        }

        [Fact]
        public void Should_SetAddedState_AnnouncementSuppliersTemplateAttachment()
        {
            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = Enums.AttachmentType.SupplierOriginalAttachment;

            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, (int)attachmentType);

            announcementSuppliersTemplateAttachment.SetAddedState();

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.AnnouncementSuppliersTemplateAttachmentId.ShouldBe(0);
        }

        [Fact]
        public void Should_Delete_AnnouncementHistorySupplierTemplate()
        {
            var name = "name";
            var fileNetReferenceId = "123";
            var attachmentType = Enums.AttachmentType.SupplierOriginalAttachment;

            var announcementSuppliersTemplateAttachment = new AnnouncementSuppliersTemplateAttachment(name, fileNetReferenceId, (int)attachmentType);

            announcementSuppliersTemplateAttachment.DeleteAttachment();

            announcementSuppliersTemplateAttachment.ShouldNotBeNull();
            announcementSuppliersTemplateAttachment.State.ShouldBe(ObjectState.Deleted);
        }
    }
}