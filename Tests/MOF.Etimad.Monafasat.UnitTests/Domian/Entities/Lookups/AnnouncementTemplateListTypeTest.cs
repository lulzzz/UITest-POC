using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Lookups
{
    public class AnnouncementTemplateListTypeTest
    {
        [Fact]
        public void Should_Construct_AnnouncementTemplateListType()
        {
            AnnouncementTemplateListType announcementTemplateListType = new AnnouncementTemplateListType();
            _ = announcementTemplateListType.AnnouncementTemplateSuppliersListTypeId;
            _ = announcementTemplateListType.NameAr;
            _ = announcementTemplateListType.NameEn;

            announcementTemplateListType.ShouldNotBeNull();
        }
    }
}
