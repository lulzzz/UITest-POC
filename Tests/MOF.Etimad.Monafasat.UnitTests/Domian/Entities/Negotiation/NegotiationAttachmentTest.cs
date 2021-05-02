
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Negotiation
{
    public class NegotiationAttachmentTest
    {
        [Fact]
        public void ShouldCreateNewNegotiationAttachment()
        {
            NegotiationAttachment negotiationAttachment = new NegotiationAttachment("Attachment 1", "ddC_sdds", 1);

            Assert.Equal("Attachment 1", negotiationAttachment.Name);
            Assert.Equal(ObjectState.Added, negotiationAttachment.State);
        }
    }
}
