using MOF.Etimad.Monafasat.Core.Entities;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.AnnouncementSupplierTemplate
{
    public class LinkedAgenciesAnnouncementTemplateTests
    {
        private const string AGENCY_CODE = "123";

        [Fact]
        public void Should_Empty_Construct_LinkedAgenciesAnnouncementTemplate()
        {
            var linkedAgenciesAnnouncement = new LinkedAgenciesAnnouncementTemplate();

            linkedAgenciesAnnouncement.ShouldNotBeNull();
            linkedAgenciesAnnouncement.Id.ShouldBe(0);
            linkedAgenciesAnnouncement.Agency.ShouldBeNull();
            linkedAgenciesAnnouncement.AnnouncementId.ShouldBe(0);
            linkedAgenciesAnnouncement.AnnouncementSupplierTemplate.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_LinkedAgenciesAnnouncementTemplate()
        {
            var linkedAgenciesAnnouncement = new LinkedAgenciesAnnouncementTemplate(AGENCY_CODE);

            linkedAgenciesAnnouncement.ShouldNotBeNull();
            linkedAgenciesAnnouncement.AgencyCode.ShouldBe(AGENCY_CODE);
        }

        [Fact]
        public void Should_DeActivate()
        {
            var linkedAgenciesAnnouncement = new LinkedAgenciesAnnouncementTemplate(AGENCY_CODE);

            linkedAgenciesAnnouncement.DeActivate();
            linkedAgenciesAnnouncement.ShouldNotBeNull();
            linkedAgenciesAnnouncement.IsActive.ShouldBe(false);
        }

    }
}