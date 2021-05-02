using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using Shouldly;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Entities.Announcements
{
    public class AnnouncementCountryTests
    {
        [Fact]
        public void Should_Construct_Empty()
        {
            var announcementCountry = new AnnouncementCountry();

            announcementCountry.ShouldNotBeNull();
            announcementCountry.Id.ShouldBe(0);
            announcementCountry.AnnouncementId.ShouldBe(0);
            announcementCountry.Country.ShouldBeNull();
            announcementCountry.Announcement.ShouldBeNull();
        }

        [Fact]
        public void Should_Construct_AnnouncementCountry()
        {
            var announcementCountryId = 1;

            var announcementCountry = new AnnouncementCountry(announcementCountryId);

            announcementCountry.ShouldNotBeNull();
            announcementCountry.CountryId.ShouldBe(announcementCountryId);
        }

        [Fact]
        public void Should_DeActive_AnnouncementCountry()
        {
            var announcementCountryId = 1;
            var announcementCountry = new AnnouncementCountry(announcementCountryId);

            announcementCountry.DeActive();

            announcementCountry.ShouldNotBeNull();
            announcementCountry.IsActive.ShouldBe(false);
        }

        [Fact]
        public void Should_SetActive_AnnouncementCountry()
        {
            var announcementCountryId = 1;
            var announcementCountry = new AnnouncementCountry(announcementCountryId);
            announcementCountry.DeActive();

            announcementCountry.SetActive();

            announcementCountry.ShouldNotBeNull();
            announcementCountry.IsActive.ShouldBe(true);
        }

        [Fact]
        public void Should_Delete_AnnouncementCountry()
        {
            var announcementCountryId = 1;
            var announcementCountry = new AnnouncementCountry(announcementCountryId);

            announcementCountry.Delete();

            announcementCountry.ShouldNotBeNull();
            announcementCountry.State.ShouldBe(ObjectState.Deleted);
        }
    }
}
