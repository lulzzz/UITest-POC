using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementTemplateJoinRequestAttachmentTests
    {
        [Fact]
        public void Should_Empty_Construct_AnnouncementTemplateJoinRequestAttachment()
        {
            var announcementTemplateJoinRequestAttachment = new AnnouncementTemplateJoinRequestAttachment();

            announcementTemplateJoinRequestAttachment.ShouldNotBeNull();
            announcementTemplateJoinRequestAttachment.JoinRequestSupplierTemplateId.ShouldBe(0);
            announcementTemplateJoinRequestAttachment.JoinRequestSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementTemplateJoinRequestAttachment()
        {
            var name = "template attachment";
            var fileNetReferenceId = "123";

            var announcementTemplateJoinRequestAttachment = new AnnouncementTemplateJoinRequestAttachment(name, fileNetReferenceId);

            announcementTemplateJoinRequestAttachment.ShouldNotBeNull();
            announcementTemplateJoinRequestAttachment.Name.ShouldBe(name);
            announcementTemplateJoinRequestAttachment.FileNetReferenceId.ShouldBe(fileNetReferenceId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementTemplateJoinRequestAttachment()
        {
            var name = "template attachment";
            var fileNetReferenceId = "123";

            var announcementTemplateJoinRequestAttachment = new AnnouncementTemplateJoinRequestAttachment(name, fileNetReferenceId);

            announcementTemplateJoinRequestAttachment.DeActive();

            announcementTemplateJoinRequestAttachment.ShouldNotBeNull();
            announcementTemplateJoinRequestAttachment.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetAddedState_AnnouncementTemplateJoinRequestAttachment()
        {
            var name = "template attachment";
            var fileNetReferenceId = "123";

            var announcementTemplateJoinRequestAttachment = new AnnouncementTemplateJoinRequestAttachment(name, fileNetReferenceId);

            announcementTemplateJoinRequestAttachment.SetAddedState();

            announcementTemplateJoinRequestAttachment.ShouldNotBeNull();
            announcementTemplateJoinRequestAttachment.Id.ShouldBe(0);
        }

        [Fact]
        public void Should_DeleteAttachment_AnnouncementTemplateJoinRequestAttachment()
        {
            var name = "template attachment";
            var fileNetReferenceId = "123";

            var announcementTemplateJoinRequestAttachment = new AnnouncementTemplateJoinRequestAttachment(name, fileNetReferenceId);

            announcementTemplateJoinRequestAttachment.DeleteAttachment();

            announcementTemplateJoinRequestAttachment.ShouldNotBeNull();
            announcementTemplateJoinRequestAttachment.State.ShouldBe(ObjectState.Deleted);
        }
    }
}