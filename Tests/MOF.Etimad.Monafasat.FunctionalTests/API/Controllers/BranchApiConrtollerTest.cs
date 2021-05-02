using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class BranchApiConrtollerTest : BaseTestApiController
    {
        #region props

        private Claim[] _claims;
        private readonly AutoMapper.IMapper mapper;
        private readonly IOptionsSnapshot<MOF.Etimad.Monafasat.SharedKernal.RootConfigurations> _mockRootConfiguration;
        private readonly IBranchAppService branchAppService;
        private readonly IIDMAppService iDMAppService;
        private readonly IMemoryCache memoryCache;

        private BranchController _branchController;

        #endregion

        public BranchApiConrtollerTest()
        {

            var serviceProvider = services.BuildServiceProvider();
            branchAppService = serviceProvider.GetService<IBranchAppService>();
            iDMAppService = serviceProvider.GetService<IIDMAppService>();

            //Configure mapping just for this test
            var config = new MapperConfiguration(cfg =>
            {

                cfg.ValidateInlineMaps = false;
            });
            mapper = config.CreateMapper();

            _mockRootConfiguration = MockHelper.CreateIOptionSnapshotMock(new MOF.Etimad.Monafasat.SharedKernal.RootConfigurations());
            memoryCache = new Mock<IMemoryCache>().Object;

            _branchController = new BranchController(branchAppService, iDMAppService, _mockRootConfiguration);
        }



        [Fact]
        public async Task FindTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);
            var requestModel = new BranchSearchCriteriaModel() { };

            var response = await _branchController.Find(requestModel);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
        }

        [Fact]
        public async Task AddBranchTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var random = new Random().Next(1, 5000).ToString();
            var requestModel = new BranchModel()
            {
                MainBranchAddress = new MainBranchAddressModel()
                {
                    AddressName = "Riyadh",
                    Address = "Riyadh",
                    BranchAddressId = 1,
                    Position = "Riyadh"
                },
                CommittieeIds = new List<int>() { 1 },
                BranchName = "Riyadh Branch" + random
            };

            var response = await _branchController.AddBranch(requestModel);

            Assert.NotNull(response);
            Assert.Equal("Riyadh Branch" + random, response.BranchName);
            Assert.NotNull(response.MainBranchAddress);
            Assert.Equal("Riyadh", response.MainBranchAddress.Position);
            Assert.Equal("Riyadh", response.MainBranchAddress.Address);
            Assert.Equal("Riyadh", response.MainBranchAddress.AddressName);

        }

        [Fact]
        public async Task GetByIdTest()
        {
            int branchId = 3;

            var response = await _branchController.GetById(branchId);

            Assert.NotNull(response);
            Assert.Equal("0J4PeaOHdn0*@@**2z1YClk0Ng==", response.BranchIdString);
            Assert.Equal(3, response.BranchId);
        }

        [Fact]
        public async Task UpdateTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var random = new Random().Next(1, 5000).ToString();
            var requestModel = new BranchModel()
            {
                BranchId = 2642,
                BranchName = "Riyadh Branch" + random
            };

            var response = await _branchController.Update(requestModel);

            Assert.NotNull(response);
            Assert.Equal("Riyadh Branch" + random, response.BranchName);

        }


        [Fact]
        public async Task DeleteTest()
        {
            int branchId = 2643;

            var response = await _branchController.Delete(branchId);

            Assert.False(response);
        }

        [Fact]
        public async Task GetIDMUsersTest()
        {
            _claims = new Claim[2] { new Claim(ClaimTypes.Role, RoleNames.MonafasatAdmin) ,
            new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000")};
            _branchController = _branchController.WithIdentity(_claims);

            var response = await _branchController.GetIDMUsers();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetIDMUsersSearchTest()
        {
            _claims = new Claim[1] {
            new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAdmin) };
            _branchController = _branchController.WithIdentity(_claims);

            var model = new BranchUserSearchCriteriaModel() { RoleName = RoleNames.MonafasatAdmin };

            var response = await _branchController.GetIDMUsersSearch(model);

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Fact]
        public async Task GetBranchUserModelTest()
        {
            _claims = new Claim[1] {
            new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAdmin) };
            _branchController = _branchController.WithIdentity(_claims);

            var model = new BranchUserSearchCriteriaModel() { RoleName = RoleNames.MonafasatAdmin, BranchId = 3 };

            var response = await _branchController.GetBranchUserModel(model);

            Assert.NotNull(response);
            Assert.Equal(3, response.BranchId);
        }

        [Fact]
        public async Task GetBranchUsersTest()
        {
            _claims = new Claim[1] {
            new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAdmin) };
            _branchController = _branchController.WithIdentity(_claims);

            var model = new BranchUserSearchCriteriaModel() { BranchId = 3 };

            var response = await _branchController.GetBranchUsers(model);

            Assert.NotNull(response);
            Assert.NotEmpty(response.Items);
        }

        [Fact]
        public async Task AssignUsersBranchTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var requestModel = new BranchUserModel()
            {
                BranchId = 2644,
                UserName = "5555555555",
                RoleName = RoleNames.MonafasatAdmin
            };

            await _branchController.AssignUsersBranch(requestModel);

        }

        [Fact]
        public async Task RemoveAssignedUserTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            await _branchController.RemoveAssignedUser(15113, 4, 1);

        }

        [Fact]
        public async Task GetCommitteesNotAssignedToBranchTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var result = await _branchController.GetCommitteesNotAssignedToBranch(27);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAssignedCommitteeByIdAndBranchTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var branchId = 3;
            var committeeId = 1;

            var result = await _branchController.GetAssignedCommitteeByIdAndBranch(committeeId, branchId);
            Assert.NotNull(result);
            Assert.Equal(1, result.CommitteeId);
            Assert.Equal(3, result.BranchId);
        }

        [Fact]
        public async Task GetBranchCommitteesTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var requestModel = new BranchCommitteeSearchCriteriaModel()
            {
                BranchId = 4
            };

            var result = await _branchController.GetBranchCommittees(requestModel);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public async Task RemoveAssignedCommitteeTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);
            var branchId = 3;
            var committeeId = 1;
            await _branchController.RemoveAssignedCommittee(committeeId, branchId);
        }

        [Fact]
        public async Task GetBranchByAgencyTest()
        {
            _claims = new Claim[1] { new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000,022001000000") };
            _branchController = _branchController.WithIdentity(_claims);

            var agencyCode = "022001000000";

            var result = await _branchController.GetBranchByAgency(agencyCode);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }


    }
}
