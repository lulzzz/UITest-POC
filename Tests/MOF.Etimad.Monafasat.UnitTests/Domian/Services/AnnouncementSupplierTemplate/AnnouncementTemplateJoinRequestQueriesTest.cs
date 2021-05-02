using MOF.Etimad.Monafasat.Services;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services
{
    public class AnnouncementTemplateJoinRequestQueriesTest
    {
        private readonly AnnouncementTemplateJoinRequestQueries _sut;
        public AnnouncementTemplateJoinRequestQueriesTest()
        {
            _sut = new AnnouncementTemplateJoinRequestQueries(InitialData.context);
        }


        [Fact]
        public async Task ShouldGetAnnouncementJoinRequestByRequestIdSuccess()
        {
            var result = await _sut.GetAnnouncementJoinRequestByRequestId(1);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetAnnouncementJoinRequestWithAttachmentsAndAnnouncementByRequestIdSuccess()
        {
            var result = await _sut.GetAnnouncementJoinRequestWithAttachmentsAndAnnouncementByRequestId(1);
            Assert.NotNull(result);
        }

       

        [Fact]
        public async Task ShouldGetAnnouncementJoinRequestWithAttachmentsByRequestIdSuccess()
        {
            var result = await _sut.GetAnnouncementJoinRequestWithAttachmentsByRequestId(1);
            Assert.NotNull(result);
        }
    }
}
