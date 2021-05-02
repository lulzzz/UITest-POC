using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.ExtendOffersValidity
{
    public class ExtendOffersValidityAttachmentTests
    {
        private const string Name = "Name";
        private const string FileNetReferenceId = "FileNetReferenceId";
        private const int AttachmentTypeId = 1;
        private const int ExtendOffersValiditySupplierId = 1;


        [Fact]
        public void Should_Empty_Construct_ExtendOffersValidityAttachment()
        {
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment();
            extendOffersValidityAttachment.ShouldNotBeNull();
        }
        [Fact]
        public void Should_Construct_ExtendOffersValidityAttachment_With_AttachmentTypeId_And_ExtendOffersValiditySupplierId()
        {
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment(Name, FileNetReferenceId,
                AttachmentTypeId, ExtendOffersValiditySupplierId);

            extendOffersValidityAttachment.ShouldNotBeNull();
            extendOffersValidityAttachment.FileNetReferenceId.ShouldBe(FileNetReferenceId);
            extendOffersValidityAttachment.AttachmentTypeId.ShouldBe(AttachmentTypeId);
            extendOffersValidityAttachment.ExtendOffersValiditySupplierId.ShouldBe(ExtendOffersValiditySupplierId);
        }
        [Fact]
        public void Should_Construct_ExtendOffersValidityAttachment()
        {
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment(Name, FileNetReferenceId);

            extendOffersValidityAttachment.ShouldNotBeNull();
            extendOffersValidityAttachment.FileNetReferenceId.ShouldBe(FileNetReferenceId);
        }
        [Fact]
        public void Should_Delete_ExtendOffersValidityAttachment()
        {
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment(Name, FileNetReferenceId);

            extendOffersValidityAttachment.DeleteAttachment();

            extendOffersValidityAttachment.ShouldNotBeNull();
            extendOffersValidityAttachment.State.ShouldBe(ObjectState.Deleted);
        }
        [Fact]
        public void Should_DeActive_ExtendOffersValidityAttachment()
        {
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment(Name, FileNetReferenceId);

            extendOffersValidityAttachment.DeActive();

            extendOffersValidityAttachment.ShouldNotBeNull();
            extendOffersValidityAttachment.IsActive.ShouldNotBeNull();
            extendOffersValidityAttachment.IsActive.Value.ShouldBeFalse();
        }
        [Fact]
        public void Should_SetActive_ExtendOffersValidityAttachment()
        {
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment(Name, FileNetReferenceId);

            extendOffersValidityAttachment.DeActive();
            extendOffersValidityAttachment.SetActive();

            extendOffersValidityAttachment.ShouldNotBeNull();
            extendOffersValidityAttachment.IsActive.ShouldNotBeNull();
            extendOffersValidityAttachment.IsActive.Value.ShouldBeTrue();
        }
        [Fact]
        public void Should_Update_ExtendOffersValidityAttachment()
        {
            var updatedName = "updated name";
            var extendOffersValidityAttachment = new ExtendOffersValidityAttachment(Name, FileNetReferenceId);

            extendOffersValidityAttachment.Update(updatedName);

            extendOffersValidityAttachment.ShouldNotBeNull();
            extendOffersValidityAttachment.Name.ShouldBe(updatedName);
        }
    }
}