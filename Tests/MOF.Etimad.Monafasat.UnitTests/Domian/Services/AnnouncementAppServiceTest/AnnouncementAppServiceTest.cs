using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.AnnouncementDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services
{
    public class AnnouncementAppServiceTest
    {
        private readonly Mock<IAnnouncementCommands> _moqAnnouncementCommands;
        private readonly Mock<IAnnouncementQueries> _moqAnnouncementQueries;
        private readonly Mock<IAnnouncementDomainService> _moqAnnouncementDomainService;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;


        public readonly AnnouncementAppService _sut;
        public AnnouncementAppServiceTest()
        {
            _moqAnnouncementQueries = new Mock<IAnnouncementQueries>();
            _moqAnnouncementCommands = new Mock<IAnnouncementCommands>();
            _moqAnnouncementDomainService = new Mock<IAnnouncementDomainService>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _sut = new AnnouncementAppService(_moqAnnouncementCommands.Object,_moqAnnouncementQueries.Object,_moqHttpContextAccessor.Object,_moqAnnouncementDomainService.Object);
        }

      
        // [Fact]
        // public async Task ShouldFindAnnouncementDetailsByAnnouncementIdSuccess()
        // {
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaults());
        //            });

        //     await _sut.SendAnnouncementForApproval(1);
        //     _moqAnnouncementCommands.VerifyAll();
        // }

        // [Theory]
        // [InlineData(100)]      
        // public async Task ShouldApproveAnnouncementSuccess(int announcementId)
        // {
        //     VerificationModel verificationModel = new VerificationModel() {VerificationCode="",Id= announcementId ,IdString=""};
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForApproval());
        //            });

        //     await _sut.ApproveAnnouncement(verificationModel);
        //     _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncement(It.IsAny<Announcement>()), Times.Once);
        // }

        // [Fact]
        // public async Task ShouldReOpenAnnouncementSuccess()
        // {
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForReOpen());
        //            });

        //     await _sut.ReOpenAnnouncement(1);
        //     _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncement(It.IsAny<Announcement>()), Times.Once);
        // }



        // [Fact]
        // public async Task ShouldDeleteAnnouncementSuccess()
        // {
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaults());
        //            });

        //     await _sut.DeleteAnnouncement(1);
        //     _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncement(It.IsAny<Announcement>()), Times.Once);
        // }


        // [Fact]
        // public async Task ShouldRejectApproveAnnouncementThrowException_When_LastApplyingRequestsDateGreaterThanToday()
        // {
        //     RejectionReasonModel rejectionReasonModel = new RejectionReasonModel() { IdString = "", RejectionReason = "rejectreason" };
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForRejection());
        //            });

        //     await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectApproveAnnouncement(rejectionReasonModel));
        // }

        // [Fact]
        // public async Task ShouldRejectApproveAnnouncementThrowException_When_StatusIsNotPending()
        // {
        //     RejectionReasonModel rejectionReasonModel = new RejectionReasonModel() { IdString = "", RejectionReason = "rejectreason" };
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForApproval());
        //            });

        //     await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectApproveAnnouncement(rejectionReasonModel));
        // }

        // [Fact]
        // public async Task ShouldDeleteApproveAnnouncementThrowException_When_StatusIsNotUnderCreation()
        // {
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForApproval());
        //            });

        //     await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.DeleteAnnouncement(1));
        // }

        // [Fact]
        // public async Task ShouldReopenApproveAnnouncementThrowException_When_StatusIsNotRejected()
        // {
        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForApproval());
        //            });

        //     await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ReOpenAnnouncement(1));
        // }

        // [Fact]
        // public async Task ShouldApproveApproveAnnouncementThrowException_When_StatusIsNotPending()
        // {
        //     VerificationModel verificationModel = new VerificationModel() { VerificationCode = "", Id = 100, IdString = "" };

        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForReOpen());
        //            });

        //     await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ApproveAnnouncement(verificationModel));
        // }

        // [Fact]
        // public async Task ShouldSendToApproveApproveAnnouncementThrowException_When_StatusIsNotUnderCreation()
        // {

        //     _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<Announcement>(new AnnouncementDefaults().ReturnAnnouncementDefaultsForReOpen());
        //            });

        //     await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.SendAnnouncementForApproval(1));
        // }

        // [Fact]
        // public async Task ShouldGetTenderTypesSuccess()
        // {
        //     _moqAnnouncementQueries.Setup(x => x.GetTenderTypes())
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<List<TenderTypeModel>>(new List<TenderTypeModel>());
        //            });

        //     var result=await _sut.GetTenderTypes();
        //     Assert.NotNull(result);
        // }

        // [Fact]
        // public async Task ShouldCreateVerificationCodeSuccess()
        // {
        //     CreateVerificationCodeModel createVerification = new CreateVerificationCodeModel() { Id = 1 };
        //     _moqAnnouncementDomainService.Setup(x => x.CreateVerificationCode(It.IsAny<int>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<List<TenderTypeModel>>(new List<TenderTypeModel>());
        //            });

        //     await _sut.CreateVerificationCode(createVerification);
        //     _moqAnnouncementCommands.VerifyAll();
        // }


        // [Theory]
        // [InlineData(1)]
        // [InlineData(2)]
        // [InlineData(3)]
        // [InlineData(4)]
        // public async Task ShouldGetAllSupplierAnnouncementsSuccess(int AnnouncementPublishDateCriteriaId)
        // {
        //     List<AllAnouuncementsForSupplierVisitorModel> allAnouuncementsForSupplierVisitors = new List<AllAnouuncementsForSupplierVisitorModel>();
        //     AllSupplierAnnouncementSearchCriteria criteria = new AllSupplierAnnouncementSearchCriteria() { AnnouncementPublishDateCriteriaId = AnnouncementPublishDateCriteriaId };
        //     _moqAnnouncementQueries.Setup(x => x.FindAllSupplierAnnouncements(It.IsAny<AllSupplierAnnouncementSearchCriteria>()))
        //            .Returns(() =>
        //            {
        //                return Task.FromResult<QueryResult<AllAnouuncementsForSupplierVisitorModel>>(new QueryResult<AllAnouuncementsForSupplierVisitorModel>(allAnouuncementsForSupplierVisitors, allAnouuncementsForSupplierVisitors.Count, 1,1));
        //            });

        //     await _sut.GetAllSupplierAnnouncements(criteria);
        //     _moqAnnouncementCommands.VerifyAll();
        // }

    }
}
