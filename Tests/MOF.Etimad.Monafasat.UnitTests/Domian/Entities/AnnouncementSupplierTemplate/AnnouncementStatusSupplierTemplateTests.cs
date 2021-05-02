using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class AnnouncementStatusSupplierTemplateTests
    {
        [Fact]
        public void Should_Construct_AnnouncementStatusSupplierTemplate()
        {
            var announcementStatusSupplierTemplate = new AnnouncementStatusSupplierTemplate
            {
                Name = "name"
            };

            announcementStatusSupplierTemplate.ShouldNotBeNull();
            announcementStatusSupplierTemplate.Id.ShouldBe(0);
            announcementStatusSupplierTemplate.Name.ShouldBe("name");
        }
    }
}