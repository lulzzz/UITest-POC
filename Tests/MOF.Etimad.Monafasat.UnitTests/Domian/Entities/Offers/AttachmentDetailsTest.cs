using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Offers
{
    public class AttachmentDetailsTest
    {
        private const string key = "key";
        private const string value = "val";
        private const int attachmentId = 1;

        [Fact]
        public void Should_Construct_AttachmentDetails()
        {
            AttachmentDetails attachmentDetails = new AttachmentDetails(key, value, attachmentId);
            _ = new AttachmentDetails();
            _ = attachmentDetails.Id;
            _ = attachmentDetails.Attachment;

            attachmentDetails.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Update()
        {
            AttachmentDetails attachmentDetails = new AttachmentDetails(key, value, attachmentId);
            attachmentDetails.Update(key, value);

            attachmentDetails.ShouldNotBeNull();
        }

        [Fact]
        public void Should_DeActive()
        {
            AttachmentDetails attachmentDetails = new AttachmentDetails(key, value, attachmentId);
            attachmentDetails.DeActive();

            Assert.False(attachmentDetails.IsActive);
        }

        [Fact]
        public void Should_SetActive()
        {
            AttachmentDetails attachmentDetails = new AttachmentDetails(key, value, attachmentId);
            attachmentDetails.SetActive();

            Assert.True(attachmentDetails.IsActive);
        }
    }
}
