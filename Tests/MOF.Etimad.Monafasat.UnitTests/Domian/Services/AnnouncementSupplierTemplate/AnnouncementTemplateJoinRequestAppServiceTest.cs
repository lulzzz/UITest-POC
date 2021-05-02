using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services
{
    public class AnnouncementTemplateJoinRequestAppServiceTest
    {
        private readonly Mock<IAnnouncementTemplateJoinRequestCommands> _moqJoinRequestCommands;
        private readonly Mock<IAnnouncementTemplateJoinRequestQueries> _moqJoinRequestQueries;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<IGenericCommandRepository> _moqGenericCommandRepository;

        public readonly AnnouncementTemplateJoinRequestAppService _sut;
        public AnnouncementTemplateJoinRequestAppServiceTest()
        {
            _moqJoinRequestQueries = new Mock<IAnnouncementTemplateJoinRequestQueries>();
            _moqJoinRequestCommands = new Mock<IAnnouncementTemplateJoinRequestCommands>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqGenericCommandRepository = new Mock<IGenericCommandRepository>();
            _moqNotificationAppService = new Mock<INotificationAppService>();

            _sut = new AnnouncementTemplateJoinRequestAppService(
                _moqJoinRequestCommands.Object,
                _moqJoinRequestQueries.Object,
                _moqNotificationAppService.Object
                );
        }

        [Fact]
        public async Task ShouldSaveJoinRequestResult()
        {
            _moqJoinRequestQueries.Setup(x => x.GetAnnouncementJoinRequestByRequestId(It.IsAny<int>()))
                    .Returns(() =>
                    {
                        return Task.FromResult<AnnouncementJoinRequestSupplierTemplate>(new AnnouncementJoinRequestDefaults().GetJoinRequestDefaultData());
                    });

            AnnouncementSuppliersTemplateJoinRequestsDetailsModel joinRequestModel = new AnnouncementJoinRequestDefaults().GetJoinRequestsDetailsModel();


            await _sut.SaveJoinRequestResult(joinRequestModel);
            _moqJoinRequestCommands.Verify(c => c.UpdateAnnouncementJoinRequestAsync(It.IsAny<AnnouncementJoinRequestSupplierTemplate>()), Times.Once);
        }

        [Fact]
        public async Task ShouldWithdrawFromAnnouncementList()
        {
            _moqJoinRequestQueries.Setup(x => x.GetAnnouncementJoinRequestWithAttachmentsByRequestId(It.IsAny<int>()))
                    .Returns(() =>
                    {
                        return Task.FromResult<AnnouncementJoinRequestSupplierTemplate>(new AnnouncementJoinRequestDefaults().GetJoinRequestDefaultData());
                    });

            await _sut.WithdrawFromAnnouncementTemplate(1, 1);
            _moqJoinRequestCommands.Verify(c => c.UpdateAnnouncementJoinRequestAsync(It.IsAny<AnnouncementJoinRequestSupplierTemplate>()), Times.Once);
        }

        [Fact]
        public async Task ShouldDeleteSupplierFromAnnouncementTemplate()
        {
            _moqJoinRequestQueries.Setup(x => x.GetAnnouncementJoinRequestWithAttachmentsAndAnnouncementByRequestId(It.IsAny<int>()))
                    .Returns(() =>
                    {
                        return Task.FromResult<AnnouncementJoinRequestSupplierTemplate>(new AnnouncementJoinRequestDefaults().GetJoinRequestWithAnnouncementDefaultData());
                    });

            DeleteSupplierFromAnnouncementModel deleteModel = new DeleteSupplierFromAnnouncementModel { CR = "123", DeleteReason = "DeleteReason", JoinRequestIdString = Util.Encrypt(1), UserId = 1 };

            await _sut.DeleteSupplierFromAnnouncementTemplate(deleteModel);
            _moqJoinRequestCommands.Verify(c => c.UpdateAnnouncementJoinRequestAsync(It.IsAny<AnnouncementJoinRequestSupplierTemplate>()), Times.Once);
        }

        [Fact]
        public async Task ShouldGetAnnouncementJoinRequestDetails()
        {
            var result = _moqJoinRequestQueries.Setup(x => x.GetAnnouncementJoinRequestByAnnouncementIdAndCR(It.IsAny<int>(), It.IsAny<string>()))
                    .Returns(() =>
                    {
                        return Task.FromResult<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>(new AnnouncementJoinRequestDefaults().GetJoinRequestsDetailsModel());
                    });

            result.ShouldNotBeNull();
        }

    }

}