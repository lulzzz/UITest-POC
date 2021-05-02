using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.JobService
{
    public class AnnouncementTamplateJobServiceTest
    {
        private readonly Mock<IGenericCommandRepository> _genericCommandRepository;
        private readonly Mock<IAnnouncementListJobQueries> _announcementQueires;
        public readonly AnnouncementListJobAppService _sut;
        public AnnouncementTamplateJobServiceTest()
        {
            _genericCommandRepository = new Mock<IGenericCommandRepository>();
            _announcementQueires = new Mock<IAnnouncementListJobQueries>();
            _sut = new AnnouncementListJobAppService(_announcementQueires.Object, _genericCommandRepository.Object);
        }

        [Fact]
        public async Task ShouldUpdateEndedAnnouncementStatusToBeEnded()
        {
            _announcementQueires.Setup(x => x.getAllEndedTemplates()).Returns(() =>
            {
                return Task.FromResult<List<AnnouncementSupplierTemplate>>(new List<AnnouncementSupplierTemplate>() { new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults() });
            });

            var result = await _sut.updateAnnouncementListStatus();

            Assert.True(result);
            _genericCommandRepository.Verify(comm => comm.SaveAsync(), Times.Once);
        }
    }
}
