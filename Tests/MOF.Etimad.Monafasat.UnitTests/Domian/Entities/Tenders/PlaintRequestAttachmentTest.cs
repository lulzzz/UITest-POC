using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Tenders
{
    public class PlaintRequestAttachmentTest
    {
        private readonly string _name = "testName";
        private readonly string _fileNetReferenceId = "1001";
        private readonly int _attachmentTypeId = 10;

        [Fact]
        public void Should_Empty_Construct_PlaintRequestAttachment()
        {
            var plaintRequestAttachment = new PlaintRequestAttachment();
            plaintRequestAttachment.ShouldNotBeNull();
        }

        [Fact]
        public void Should_CreatePlaintRequestAttachmentObj()
        {
            var plaintRequestAttachment = new PlaintRequestAttachment(_name, _fileNetReferenceId, _attachmentTypeId);

            plaintRequestAttachment.ShouldNotBeNull();
            plaintRequestAttachment.Name.ShouldBe(_name);
            plaintRequestAttachment.FileNetReferenceId.ShouldBe(_fileNetReferenceId);
            plaintRequestAttachment.AttachmentTypeId.ShouldBe(_attachmentTypeId);
            plaintRequestAttachment.State.ShouldBe(SharedKernal.ObjectState.Added);
        }

        [Fact]
        public void Should_UpdateName()
        {
            var plaintRequestAttachment = new PlaintRequestAttachment();
            plaintRequestAttachment.Update(_name);
            plaintRequestAttachment.ShouldNotBeNull();
            plaintRequestAttachment.Name.ShouldBe(_name);
            plaintRequestAttachment.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_DeActive()
        {
            var plaintRequestAttachment = new PlaintRequestAttachment();
            plaintRequestAttachment.DeActive();
            plaintRequestAttachment.IsActive.ShouldBe(false);
            plaintRequestAttachment.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_SetActive()
        {
            var plaintRequestAttachment = new PlaintRequestAttachment();
            plaintRequestAttachment.SetActive();
            plaintRequestAttachment.IsActive.ShouldBe(true);
            plaintRequestAttachment.State.ShouldBe(SharedKernal.ObjectState.Modified);
        }

        [Fact]
        public void Should_Delete()
        {
            var plaintRequestAttachment = new PlaintRequestAttachment();
            plaintRequestAttachment.DeleteAttachment();
            plaintRequestAttachment.State.ShouldBe(SharedKernal.ObjectState.Deleted);
        }
    }
}
