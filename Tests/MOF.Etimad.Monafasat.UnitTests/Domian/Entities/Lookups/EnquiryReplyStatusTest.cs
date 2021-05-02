using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class EnquiryReplyStatusTest
    {
        [Fact]
        public void Should_Construct_EnquiryReplyStatus()
        {
            EnquiryReplyStatus enquiryReplyStatus = new EnquiryReplyStatus();
            _ = enquiryReplyStatus.Id;
            _ = enquiryReplyStatus.NameAr;
            _ = enquiryReplyStatus.NameEn;

            enquiryReplyStatus.ShouldNotBeNull();
        }
    }
}
