using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AttachmentTypeTest
    {
        [Fact]
        public void Should_Construct_AttachmentType()
        {
            AttachmentType attachmentType = new AttachmentType();
            _ = attachmentType.AttachmentTypeId;
            _ = attachmentType.NameAr;
            _ = attachmentType.NameEn;

            attachmentType.ShouldNotBeNull();
        }
    }
}
