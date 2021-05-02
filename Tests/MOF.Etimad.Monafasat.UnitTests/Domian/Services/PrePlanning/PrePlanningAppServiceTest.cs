using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.PrePlanningDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.PrePlanning
{
    public class PrePlanningAppServiceTest
    {
        private readonly Mock<IPrePlanningDomainService> _moqPrePlanningDomain;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<ITenderQueries> _moqTenderQueries;
        private readonly Mock<IPrePlanningQueries> _moqPrePlanningQueries;
        private readonly AppDbContext _moqAppDbContext;
        private readonly PrePlanningAppService _sut;
        private readonly Mock<INotificationAppService> _notificationAppService;
        private readonly Mock<IVerificationService> _moqVerificationService;
        private readonly PrePlanningDefaults prePlanningDefaults = new PrePlanningDefaults();
        private readonly Mock<IHttpContextAccessor> _httpContext;
        public PrePlanningAppServiceTest()
        {

            var db = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _moqPrePlanningDomain = new Mock<IPrePlanningDomainService>();
            _notificationAppService = new Mock<INotificationAppService>();
            _moqTenderQueries = new Mock<ITenderQueries>();
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqPrePlanningQueries = new Mock<IPrePlanningQueries>();
            _moqVerificationService = new Mock<IVerificationService>();
            _sut = new PrePlanningAppService(_moqTenderQueries.Object, _moqPrePlanningDomain.Object, _moqCommandRepository.Object, _moqPrePlanningQueries.Object, _notificationAppService.Object, _httpContext.Object,_moqVerificationService.Object);
        }


        [Theory]
        [InlineData(1)]
        public async Task ShouldFindPrePlanningByIdSuccess(int preplanningId)
        {
            _moqPrePlanningQueries.Setup(x => x.FindPrePlanningById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<PrePlanningModel>(prePlanningDefaults.GetPrePlanningModelData());
                });
            var result = await _sut.FindPrePlanningById(preplanningId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldGetPrePlanningByIdSuccess(int preplanningId)
        {
            _moqPrePlanningQueries.Setup(x => x.GetPrePlanningDetailsById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<PrePlanningModel>(prePlanningDefaults.GetPrePlanningModelData());
                });
            var result = await _sut.GetPrePlanningDetailsById(preplanningId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task ShouldThrowExceptionWhenPrePlanningIsNull(int preplanningId)
        {
            _moqPrePlanningQueries.Setup(x => x.GetPrePlanningDetailsById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<PrePlanningModel>(null);
                });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.GetPrePlanningDetailsById(preplanningId));
        }

        

       

        [Theory]
        [InlineData(1)]
        public async Task ShouldThrowExceptionWhenFindPrePlanningIsNull(int preplanningId)
        {
            _moqPrePlanningQueries.Setup(x => x.FindPrePlanningById(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<PrePlanningModel>(null);
                });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.FindPrePlanningById(preplanningId));
        }

        [Fact]
        public async Task ShouldSetPrePlanningLookUpsThrowException()
        {
            _moqPrePlanningQueries.Setup(x => x.SetPrePlanningLookUps())
                .Returns(() =>
                {
                    return Task.FromResult<PrePlanningModel>(null);
                });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.SetPrePlanningLookUps());
        }

        [Fact]
        public async Task ShouldRejectPrePlanningThrowExceptisn()
        {
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.ReturnPrePlanningDefaults_Update());
              });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.Reject(1, prePlanningDefaults._rejectionReason));
        }

       
        [Fact]
        public async Task ShouldRejectPrePlanningThrowExceptionWhenRejectreasonMoreThan500Characters()
        {
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.ReturnPrePlanningDefaults_Update());
              });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.Reject(1, prePlanningDefaults._rejectReasonMoreThan500Charachers));
        }


        [Fact]
        public async Task ShouldRejectPrePlanningSuccess()
        {
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningData());
              });
             await _sut.Reject(1, prePlanningDefaults._rejectionReason);
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }


        [Fact]
        public async Task ShouldReOpenForCancelationSuccess()
        {
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningData());
              });
            await _sut.ReOpenForCancelation(1);
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task ShouldReOpenForCancelationThrowExceptisn()
        {

            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningDataReOpenForCancelation());
              });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.ReOpenForCancelation(1));
        }



        [Fact]
        public async Task ShouldApprovePrePlanningThrowExceptisn()
        {
            
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.ReturnPrePlanningDefaults_Update());
              });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.Approve(1, prePlanningDefaults._verificationCode));
        }

        [Fact]
        public async Task ShouldApprovePrePlanningSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningDataForApproval());
              });
            await _sut.Approve(1, prePlanningDefaults._verificationCode);
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }



        [Fact]
        public async Task ShouldSendToApproveSuccess()
        {
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.ReturnPrePlanningDefaults_Update());
              });
            await _sut.SendToApprove(1);
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task ShouldSendToApproveThrowExceptisn()
        {

            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningData());
              });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.SendToApprove(1));
        }


       
        [Fact]
        public async Task ShouldReOpenSuccess()
        {
            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningDataForReOpen());
              });
            await _sut.ReOpen(1);
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task ShouldReopenThrowExceptisn()
        {

            _moqPrePlanningQueries.Setup(x => x.FindById(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningData());
              });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.ReOpen(1));
        }

        [Fact]
        public async Task ShouldCreateAsyncThrowException_When_DurationInDaysLessThanZero()
        {
            PrePlanningModel model = new PrePlanningModel()
            {
                PrePlanningId = 0,
                BranchId = 1,
                YearQuarterId = 1,
                ProjectName = "ProjectName",
                AgencyCode = "1"
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateAsync(model));
        }

        [Fact]
        public async Task ShouldCreateAsyncSuccess()
        {
            _moqPrePlanningQueries.Setup(x => x.FindByIdForEdit(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.ReturnPrePlanningDefaults());
              });
            PrePlanningModel model = new PrePlanningModel()
            {
                PrePlanningId = 1,
                BranchId = 1,
                YearQuarterId = 1,
                ProjectName = "ProjectName",
                AgencyCode = "1",
                DurationInDays=1
            };
            await _sut.CreateAsync(model);
            _moqCommandRepository.Verify(g => g.SaveAsync());
        }

        [Fact]
        public async Task ShouldCreateAsyncThrowException()
        {
            _moqPrePlanningQueries.Setup(x => x.FindByIdForEdit(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Core.Entities.PrePlanning>(prePlanningDefaults.GetPrePlanningDataForReOpen());
              });
            PrePlanningModel model = new PrePlanningModel()
            {
                PrePlanningId = 1,
                BranchId = 1,
                YearQuarterId = 1,
                ProjectName = "ProjectName",
                AgencyCode = "1",
                DurationInDays = 1
            };
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sut.CreateAsync(model));
        }
    }
}
