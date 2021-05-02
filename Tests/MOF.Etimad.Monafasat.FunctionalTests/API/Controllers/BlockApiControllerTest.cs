using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class BlockApiControllerTest : BaseTestApiController
    {
        private Claim[] _claims;
        private readonly ILookUpService lookUpService;
        private readonly IBlockAppService blockAppService;
        private readonly ISupplierService supplierService;
        private readonly IVerificationService verificationService;
        private readonly IIDMAppService iDMAppService;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private BlockController _blockController;
        public BlockApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            lookUpService = serviceProvider.GetService<ILookUpService>();
            blockAppService = serviceProvider.GetService<IBlockAppService>();
            supplierService = serviceProvider.GetService<ISupplierService>();
            verificationService = serviceProvider.GetService<IVerificationService>();
            iDMAppService = serviceProvider.GetService<IIDMAppService>();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _blockController = new BlockController(blockAppService, supplierService, verificationService, iDMAppService, rootConfigurationsMock.Object);
        }

        [Fact]
        public async Task GetBlocks_Returns_Data()
        {
            var _blockSearchCriteriaModel = new BlockSearchCriteriaModel()
            {
            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _blockController = _blockController.WithIdentity(_claims);

            var result = await _blockController.GetBlocks(_blockSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
            Assert.Empty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetBlockCommittee_Returns_Data()
        {
            var _blockCommitteeSearchCriteriaModel = new BlockCommitteeSearchCriteriaModel()
            {
            };

            var result = await _blockController.GetBlockCommittee(_blockCommitteeSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<BlockCommitteeModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetBlockListUsers_Returns_Data()
        {
            var _blockCommitteeSearchCriteriaModel = new BlockCommitteeSearchCriteriaModel()
            {
            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.Role, "12"),
            };
            _blockController = _blockController.WithIdentity(_claims);


            var result = await _blockController.GetBlockListUsers(_blockCommitteeSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<UserProfileModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetBlockById_Returns_Data()
        {
            var _supplierBlockId = 64;
            var _expectedResult = new SupplierBlockModel()
            {
                AgencyCode = "VRO000007",
                CommercialRegistrationNo = "1010254223",
                ResolutionNumber = "99",
                SupplierBlockId = 64,
                SupplierBlockIdString = "LREqj6Fy4ZfJ4UFZE8jsVg==",
                SupplierTypeId = 1
            };

            var result = await _blockController.GetBlockById(_supplierBlockId);

            Assert.NotNull(result);
            Assert.IsType<SupplierBlockModel>(result);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.CommercialRegistrationNo, result.CommercialRegistrationNo);
            Assert.Equal(_expectedResult.ResolutionNumber, result.ResolutionNumber);
            Assert.Equal(_expectedResult.SupplierBlockId, result.SupplierBlockId);
            Assert.Equal(_expectedResult.SupplierBlockIdString, result.SupplierBlockIdString);
            Assert.Equal(_expectedResult.SupplierTypeId, result.SupplierTypeId);
            Assert.False(result.IsOldSystem);
        }

        [Fact]
        public async Task GetBlockByIdForSupplier_Returns_Data()
        {
            var _supplierBlockId = 64;
            var _expectedResult = new SupplierBlockModel()
            {
                AgencyCode = "VRO000007",
                CommercialRegistrationNo = "1010254223",
                ResolutionNumber = "99",
                SupplierBlockId = 64,
                SupplierBlockIdString = "LREqj6Fy4ZfJ4UFZE8jsVg==",
                SupplierTypeId = 1
            };

            var result = await _blockController.GetBlockByIdForSupplier(_supplierBlockId);

            Assert.NotNull(result);
            Assert.IsType<SupplierBlockModel>(result);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.CommercialRegistrationNo, result.CommercialRegistrationNo);
            Assert.Equal(_expectedResult.ResolutionNumber, result.ResolutionNumber);
            Assert.Equal(_expectedResult.SupplierBlockId, result.SupplierBlockId);
            Assert.Equal(_expectedResult.SupplierBlockIdString, result.SupplierBlockIdString);
            Assert.Equal(_expectedResult.SupplierTypeId, result.SupplierTypeId);
            Assert.False(result.IsOldSystem);
        }

        [Fact]
        public async Task AddBlock_Returns_Throws_AlreadyBlocked()
        {
            var _branchId = "3";
            var _agencyCode = "019001000000";
            var _supplierBlockModel = new SupplierBlockModel()
            {
                SupplierBlockId = 64,
                CommercialRegistrationNo = "1010274503",
                CommercialSupplierName = "الشركة السعودية لتبادل المعلومات الكترونيا تبادل",
                AdminFileName = "CR  العملات الأجنبية V2.2 (1).pdf",
                AdminFileNetReferenceId = "idd_8A7F7375-6A6F-CAC5-8671-6D78C5300000",
                BlockDetails = "منع دائم - قديم - تاريخ المنع - غدا",
                BlockTypeId = 1,
                SupplierTypeId = 1,
                IsOldSystem = true,
                ResolutionNumber = "99",
                BlockStartDate = DateTime.Now.AddDays(2),
                Punishment = "نص العقوبة",
                AgencyCode = "019001000000",
                BlockStatusId = 5,
                IsTotallyBlocked = false
            };
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.Role, RoleNames.MonafasatAdmin),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.BranchId,_branchId),
            };
            _blockController = _blockController.WithIdentity(_claims);
            var _expectedMessage = Resources.BlockResources.ErrorMessages.AlreadyBlocked;

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _blockController.AddBlock(_supplierBlockModel));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetAgency_Returns_Success()
        {
            var _agencyCode = "019001000000";
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
            };
            _blockController = _blockController.WithIdentity(_claims);

            var result = await _blockController.GetAgency();

            Assert.NotNull(result);
            Assert.IsType<GovAgencyModel>(result);
            Assert.True(result.IsOldSystem);
        }

        [Fact]
        public async Task GetCrName_Returns_Success()
        {
            var _commercialRegistrationNo = "1010274503";
            var _expectedResult = "الشركة السعودية لتبادل المعلومات الكترونيا تبادل";

            var result = await _blockController.GetCrName(_commercialRegistrationNo);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(_expectedResult, result);
        }

        [Fact]
        public async Task UpdateBlock_Returns_Throws_AlreadyBlocked()
        {
            var _supplierBlockModel = new SupplierBlockModel()
            {
                SupplierBlockId = 64,
                CommercialRegistrationNo = "1010274503",
                CommercialSupplierName = "الشركة السعودية لتبادل المعلومات الكترونيا تبادل",
                AdminFileName = "CR  العملات الأجنبية V2.2 (1).pdf",
                AdminFileNetReferenceId = "idd_8A7F7375-6A6F-CAC5-8671-6D78C5300000",
                BlockDetails = "منع دائم - قديم - تاريخ المنع - غدا",
                BlockTypeId = 1,
                SupplierTypeId = 1,
                IsOldSystem = false,
                ResolutionNumber = "99",
                BlockStartDate = DateTime.Now.AddDays(2),
                Punishment = "نص العقوبة",
                AgencyCode = "019001000000",
                BlockStatusId = 5,
                IsTotallyBlocked = false
            };

            var result = await _blockController.UpdateBlock(_supplierBlockModel);

            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeactivateBlock_Returns_Success()
        {
            var _supplierBlockId = 80;

            var result = await _blockController.DeactivateBlock(_supplierBlockId);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task ResetSupplier_Returns_Success()
        {
            var _supplierBlockId = 80;

            var result = await _blockController.ResetSupplier(_supplierBlockId);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAllIDMSuppliers_Returns_Data()
        {
            var _supplierSearchCretriaModel = new SupplierSearchCretriaModel()
            {
            };

            var result = await _blockController.GetAllIDMSuppliers(_supplierSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierIntegrationModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetBlockingListDetails_Returns_Data()
        {
            var _blockSearchCriteria = new BlockSearchCriteria()
            {
                AgencyCode = "022001000000"
            };

            var result = await _blockController.GetBlockingListDetails(_blockSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetAdminBlockingListDetails_Returns_Data()
        {
            var _agencyCode = "022001000000";
            var _blockSearchCriteria = new BlockSearchCriteria()
            {
                AgencyCode = "022001000000"
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _blockController = _blockController.WithIdentity(_claims);

            var result = await _blockController.GetAdminBlockingListDetails(_blockSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetAdminBlockList_Returns_Data()
        {
            var _agencyCode = "6224657560";
            var _blockSearchCriteria = new BlockSearchCriteria()
            {
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _blockController = _blockController.WithIdentity(_claims);

            var result = await _blockController.GetAdminBlockList(_blockSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
        }

        [Fact]
        public async Task GetSecretaryBlockList_Returns_Data()
        {
            var _blockSearchCriteriaModel = new BlockSearchCriteriaModel()
            {
                AgencyCode = "019001000000"
            };

            var result = await _blockController.GetSecretaryBlockList(_blockSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetManagerBlockList_Returns_Data()
        {
            var _blockSearchCriteriaModel = new BlockSearchCriteriaModel()
            {
                AgencyCode = "019001000000"
            };

            var result = await _blockController.GetManagerBlockList(_blockSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierBlockModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.TotalCount > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task ManagerRejectionReason_Success()
        {
            var _supplierBlockModel = new SupplierBlockModel()
            {
                SupplierBlockId = 80,
                ManagerRejectionReason = "Manager Rejection Reason"
            };

            await _blockController.ManagerRejectionReason(_supplierBlockModel);
        }

        [Fact]
        public async Task SecretaryApproval_Success()
        {
            var _SupplierBlockId = 83;

            await _blockController.SecretaryApproval(_SupplierBlockId);
        }

        [Fact]
        public async Task SecretaryRejectionReason_Success()
        {
            var _supplierBlockModel = new SupplierBlockModel()
            {
                SupplierBlockId = 83,
                SecretaryRejectionReason = "Secretary Rejection Reason"
            };

            await _blockController.SecretaryRejectionReason(_supplierBlockModel);
        }

        [Fact]
        public async Task GetAgencyExceptedModel_Returns_Data()
        {
            var _blockSearchCriteriaModel = new BlockSearchCriteriaModel()
            {
                AgencyCode = "019001000000"
            };

            var result = await _blockController.GetAgencyExceptedModel(_blockSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<AgencyExceptedModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task SaveAgency_Returns_Data()
        {
            var _agencyExceptedModel = new AgencyExceptedModel()
            {
                AgencyIdString = "019001000000",
                AgencyCode = "019001000000",
                NameArabic = "Workflow Registered Foreign 2",
                CategoryId = 1,
                PurchaseMethodString = "1,2,4",
                IsOldSystem = true,
                MobileNumber = "0505654343",
                PurchaseMethods = new System.Collections.Generic.List<int>() { 1, 2, 4 }
            };

            var result = await _blockController.SaveAgency(_agencyExceptedModel);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task AddSecretaryBlock_Returns_Ok()
        {
            var _supplierBlockModel = new SupplierBlockModel()
            {
                AgencyCode = "019001000000",
                BlockStatusId = 5,
                CommercialRegistrationNo = "1010274503",
                ResolutionNumber = "99",
                SupplierBlockId = 64,
                SupplierBlockIdString = "LREqj6Fy4ZfJ4UFZE8jsVg==",
                SupplierTypeId = 1
            };

            var result = await _blockController.AddSecretaryBlock(_supplierBlockModel);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetAgencyExceptedById_Returns_Data()
        {
            var _agencyCode = "019001000000";

            var result = await _blockController.GetAgencyExceptedById(_agencyCode);

            Assert.NotNull(result);
            Assert.IsType<AgencyExceptedModel>(result);
            Assert.NotEmpty(result.PurchaseMethods);
            Assert.NotEmpty(result.TenderTypes);
            Assert.True(result.IsOldSystem);
            Assert.True(result.IsRelated);
        }

        [Fact]
        public async Task UpdateAgencyStatus_Success()
        {
            var _agencyId = "001004000000";

            await _blockController.UpdateAgencyStatus(_agencyId, false);
        }
        [Fact]
        public async Task UpdateAgencyIsOld_Success()
        {
            var _agencyId = "001004000000";

            await _blockController.UpdateAgencyIsOld(_agencyId, true);
        }

        [Fact]
        public async Task DownloadSupplierBlockedAccordingAgency_Returns_Data()
        {
            var _agencyCode = "022001000000";
            var _blockSearchCriteria = new BlockSearchCriteria()
            {
                AgencyCode = "022001000000"
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _blockController = _blockController.WithIdentity(_claims);

            var result = await _blockController.DownloadSupplierBlockedAccordingAgency(_blockSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<byte[]>(result);
        }

        [Fact]
        public async Task DownloadSupplierBlocked_Returns_Data()
        {
            var _agencyCode = "022001000000";
            var _blockSearchCriteria = new BlockSearchCriteria()
            {
                AgencyCode = "022001000000"
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode)
            };
            _blockController = _blockController.WithIdentity(_claims);

            var result = await _blockController.DownloadSupplierBlocked(_blockSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<byte[]>(result);
        }
    }
}
