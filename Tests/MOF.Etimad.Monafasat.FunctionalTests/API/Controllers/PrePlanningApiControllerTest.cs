using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class PrePlanningApiControllerTest : BaseTestApiController
    {
        private Claim[] _claims;
        private readonly IPrePlanningAppService prePlanningAppService;
        private readonly IAuthorizationService authorizationService;
        private readonly ILookUpService lookUpService;
        private readonly IVerificationService verificationService;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private PrePlanningController _prePlanningController;
        public PrePlanningApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            prePlanningAppService = serviceProvider.GetService<IPrePlanningAppService>();
            authorizationService = serviceProvider.GetService<IAuthorizationService>();
            lookUpService = serviceProvider.GetService<ILookUpService>();
            verificationService = serviceProvider.GetService<IVerificationService>();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _prePlanningController = new PrePlanningController(prePlanningAppService, lookUpService, verificationService, rootConfigurationsMock.Object);
        }

        [Fact]
        public async Task GetStatus_Returns_Data()
        {
            var result = await _prePlanningController.GetStatus();

            Assert.NotNull(result);
            Assert.IsType<List<LookupModel>>(result);
            Assert.NotEmpty(result);
        }


        [Fact]
        public async Task GetPrePlanningModelById_Returns_Data()
        {
            var _id = 1;
            var _prePlanningModel = new PrePlanningModel()
            {
                AgencyCode = "018001000000",
                BranchId = 5,
                InsideKSA = true
            };
            var result = await _prePlanningController.GetPrePlanningModelById(_id);

            Assert.NotNull(result);
            Assert.IsType<PrePlanningModel>(result);
            Assert.Equal(_prePlanningModel.AgencyCode, result.AgencyCode);
            Assert.NotEmpty(result.PrePlanningAreaIDs);
            Assert.NotEmpty(result.ProjectTypesIDs);
            Assert.True(result.InsideKSA);
        }

        [Fact]
        public async Task GetPrePlanningDetailsById_Returns_Data()
        {
            var _id = 1;
            var _prePlanningModel = new PrePlanningModel()
            {
                AgencyCode = "018001000000",
                BranchId = 5,
                InsideKSA = true,
            };
            var result = await _prePlanningController.GetPrePlanningDetailsById(_id);

            Assert.NotNull(result);
            Assert.IsType<PrePlanningModel>(result);
            Assert.Equal(_prePlanningModel.AgencyCode, result.AgencyCode);
            Assert.NotEmpty(result.PrePlanningAreaIDs);
            Assert.NotEmpty(result.ProjectTypesIDs);
            Assert.True(result.InsideKSA);
        }

        [Fact]
        public async Task SetPrePlanningLookUps_Returns_Data()
        {
            var result = await _prePlanningController.SetPrePlanningLookUps();

            Assert.NotNull(result);
            Assert.IsType<PrePlanningModel>(result);
            Assert.NotEmpty(result.Countries);
            Assert.NotEmpty(result.Areas);
        }

        [Fact]
        public async Task Deactivate_Returns_Data()
        {
            var _id = 2;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _prePlanningController = _prePlanningController.WithIdentity(_claims);

            var result = await _prePlanningController.Deactivate(_id);

            Assert.IsType<int>(result);
            Assert.Equal(_id, result);
        }

        [Fact]
        public async Task FindPrePlanningBySearchCriteria_Returns_Data()
        {
            var _prePlanningSearchCriteria = new PrePlanningSearchCriteria()
            {
                AgencyCode = "022001000000"
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "022001000000")
            };
            _prePlanningController = _prePlanningController.WithIdentity(_claims);

            var result = await _prePlanningController.FindPrePlanningBySearchCriteria(_prePlanningSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<PrePlanningModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }
        [Fact]
        public async Task Add_Success()
        {
            var _prePlanningModel = new PrePlanningModel()
            {
                AgencyCode = "022001000000",
                BranchId = 1,
                InsideKSA = true,
                ProjectName = "Functional Test" + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Millisecond,
                ProjectNature = "Testing",
                ProjectDescription = "API Functional Test",
                Duration = "1",
                YearQuarterId = 4,
                DurationInDays = 1,
                DurationInMonths = 2,
                DurationInYears = 3
            };
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId,"1"),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "022001000000")
            };
            _prePlanningController = _prePlanningController.WithIdentity(_claims);

            var result = await _prePlanningController.Add(_prePlanningModel);

            Assert.NotNull(result);
            Assert.IsType<PrePlanningModel>(result);
            Assert.Equal(_prePlanningModel.AgencyCode, result.AgencyCode);
            Assert.Equal(_prePlanningModel.ProjectName, result.ProjectName);
            Assert.Equal(_prePlanningModel.ProjectNature, result.ProjectNature);
            Assert.Equal(_prePlanningModel.ProjectDescription, result.ProjectDescription);
            Assert.Equal(_prePlanningModel.Duration, result.Duration);
            Assert.Equal(_prePlanningModel.YearQuarterId, result.YearQuarterId);
            Assert.True(result.InsideKSA);
        }

        [Fact]
        public async Task SendToApprove_Throws_UnHandledAccessException()
        {
            var _id = 2;
            var _expectedMessage = "UnHandledAccessException";
            var result = await Assert.ThrowsAsync<UnHandledAccessException>(() => _prePlanningController.SendToApprove(_id));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task ReOpen_Throws_UnHandledAccessException()
        {
            var _id = 2;
            var _expectedMessage = "UnHandledAccessException";
            var result = await Assert.ThrowsAsync<UnHandledAccessException>(() => _prePlanningController.ReOpen(_id));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task Reject_Throws_UnHandledAccessException()
        {
            var _id = 2;
            var _expectedMessage = "UnHandledAccessException";
            var result = await Assert.ThrowsAsync<UnHandledAccessException>(() => _prePlanningController.Reject(_id, "reject reason"));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task CreateVerificationCode_Success()
        {
            var _planningId = 1;
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.Email,"A@A.com"),
                new Claim(IdentityConfigs.Claims.Mobile,"0593321765"),
                new Claim("sub","123")
            };
            _prePlanningController = _prePlanningController.WithIdentity(_claims);

            await _prePlanningController.CreateVerificationCode(_planningId);

        }
        [Fact]
        public async Task Approve_Success()
        {
            var _planningId = 1;
            var result = _prePlanningController.Approve(_planningId, "02130");

            Assert.NotNull(result);
            Assert.Equal("WaitingForActivation", result.Status.ToString());
        }
    }
}
