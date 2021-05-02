using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AnnouncementJoinRequestStatusSupplierTemplateTest
    {
        [Fact]
        public void Should_Construct_AnnouncementJoinRequestStatusSupplierTemplate()
        {
            AnnouncementJoinRequestStatusSupplierTemplate announcementJoinRequestStatusSupplierTemplate = new AnnouncementJoinRequestStatusSupplierTemplate();
            _ = announcementJoinRequestStatusSupplierTemplate.Id;
            _ = announcementJoinRequestStatusSupplierTemplate.NameAr;
            _ = announcementJoinRequestStatusSupplierTemplate.NameEn;

            announcementJoinRequestStatusSupplierTemplate.ShouldNotBeNull();
        }
    }
}
