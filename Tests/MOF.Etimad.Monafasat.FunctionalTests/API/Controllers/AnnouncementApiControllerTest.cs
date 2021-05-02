using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class AnnouncementApiControllerTest : BaseTestApiController
    {
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private readonly IAnnouncementAppService announcementAppService;
        private Claim[] _claims;
        private AnnouncementController _announcementController;
        public AnnouncementApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            announcementAppService = serviceProvider.GetService<IAnnouncementAppService>();
            _announcementController = new AnnouncementController(rootConfigurationsMock.Object, announcementAppService);
        }

        [Fact]
        public async Task GetAnnouncementDetails_Returns_Data()
        {
            var _announcementIdString = Util.Encrypt(13);
            var _expectedResult = new AnnouncementDetailsModel()
            {
                AgencyCode = "",
                AgencyName = "وزارة الصحة - الديوان العام",
                AnnouncementId = 13,
                AnnouncementIdString = "4D*@@**CkYL*@@**L*@@**yHtLTllLUmsg==",
                AnnouncementJoinRequestsCount = 1,
                AnnouncementName = "اعلان اول",
                AnnouncementPeriod = 1,
                BranchName = "فرع ابراهيم 1",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
                Details = "تفاصيل"
            };

            var result = await _announcementController.GetAnnouncementDetails(_announcementIdString);

            Assert.NotNull(result);
            Assert.IsType<AnnouncementDetailsModel>(result);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AgencyName, result.AgencyName);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementIdString, result.AnnouncementIdString);
            Assert.Equal(_expectedResult.AnnouncementJoinRequestsCount, result.AnnouncementJoinRequestsCount);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
            Assert.Equal(_expectedResult.AnnouncementPeriod, result.AnnouncementPeriod);
            Assert.Equal(_expectedResult.BranchName, result.BranchName);
            Assert.Equal(_expectedResult.CreatedBy, result.CreatedBy);
            Assert.Equal(_expectedResult.Details, result.Details);
        }

        [Fact]
        public async Task GetAnnouncementNameByAnnouncementId_Returns_Data()
        {
            var _announcementId = 13;
            var _expectedResult = new LookupModel()
            {
                Id = 0,
                Name = "اعلان اول",
                Value = null
            };

            var result = await _announcementController.GetAnnouncementNameByAnnouncementId(_announcementId);

            Assert.NotNull(result);
            Assert.IsType<LookupModel>(result);
            Assert.Equal(_expectedResult.Id, result.Id);
            Assert.Equal(_expectedResult.Name, result.Name);
            Assert.Equal(_expectedResult.Value, result.Value);
        }

        [Fact]
        public async Task GetSupplierAnnouncementDetails_Returns_Data()
        {
            var _announcementIdString = Util.Encrypt(13);
            var _expectedResult = new SupplierAnnouncementDetailsModel()
            {
                ActivityDescription = "activity description",
                AgencyCode = "022001000000",
                AgencyName = "وزارة الصحة - الديوان العام",
                AnnouncementId = 13,
                AnnouncementIdString = "4D*@@**CkYL*@@**L*@@**yHtLTllLUmsg==",
                AnnouncementJoinRequestsCount = 1,
                AnnouncementName = "اعلان اول",
                AnnouncementPeriod = 1,
                BranchName = "فرع ابراهيم 1",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
                Details = "تفاصيل",
                InsideKSA = true,
                IntroAboutTender = "تعريف",
                ReferenceNumber = "200641021249",
                hasJoinRequest = false
            };

            var result = await _announcementController.GetSupplierAnnouncementDetails(_announcementIdString);

            Assert.NotNull(result);
            Assert.IsType<SupplierAnnouncementDetailsModel>(result);
            Assert.Equal(_expectedResult.ActivityDescription, result.ActivityDescription);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AgencyName, result.AgencyName);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementIdString, result.AnnouncementIdString);
            Assert.Equal(_expectedResult.AnnouncementJoinRequestsCount, result.AnnouncementJoinRequestsCount);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
            Assert.Equal(_expectedResult.AnnouncementPeriod, result.AnnouncementPeriod);
            Assert.Equal(_expectedResult.BranchName, result.BranchName);
            Assert.Equal(_expectedResult.CreatedBy, result.CreatedBy);
            Assert.Equal(_expectedResult.Details, result.Details);
            Assert.Equal(_expectedResult.IntroAboutTender, result.IntroAboutTender);
            Assert.Equal(_expectedResult.ReferenceNumber, result.ReferenceNumber);
            Assert.False(result.hasJoinRequest);
            Assert.False(result.InsideKSA);
        }

        [Fact]
        public async Task JoinAnnouncement_Throws_AnnoucementNotApprovedException()
        {
            var _announcementId = 18;
            var _expectedMessage = Resources.AnnouncementResources.ErrorMessages.AnnoucementNotApproved;

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _announcementController.JoinAnnouncement(_announcementId));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task JoinAnnouncement_Throws_AnnouncmentDateExpired()
        {
            var _announcementId = 13;
            var _expectedMessage = Resources.AnnouncementResources.ErrorMessages.AnnouncmentDateExpired;

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _announcementController.JoinAnnouncement(_announcementId));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task WithdrawJoinRequest_Throws_AnnouncmentDateExpired()
        {
            var _announcementId = 13;
            var _expectedMessage = Resources.AnnouncementResources.ErrorMessages.AnnouncmentDateExpired;

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _announcementController.WithdrawJoinRequest(_announcementId));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetSupplierAnnouncements_Returns_Data()
        {
            var _supplierAnnouncementSearchCriteria = new SupplierAnnouncementSearchCriteria()
            {
            };

            var result = await _announcementController.GetSupplierAnnouncements(_supplierAnnouncementSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierGridAnnouncementModel>>(result);
            Assert.Empty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetAllSupplierAnnouncements_Returns_Data()
        {
            var _allSupplierAnnouncementSearchCriteria = new AllSupplierAnnouncementSearchCriteria()
            {
            };
            var _commercialRegisterNo = "142154293000206";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _announcementController = _announcementController.WithIdentity(_claims);

            var result = await _announcementController.GetAllSupplierAnnouncements(_allSupplierAnnouncementSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<AllAnouuncementsForSupplierVisitorModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetAllAgencyAnnouncements_Returns_Data()
        {
            var _allAgencyAnnouncementSearchCriteriaModel = new AllAgencyAnnouncementSearchCriteriaModel()
            {
            };
            var _commercialRegisterNo = "142154293000206";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _announcementController = _announcementController.WithIdentity(_claims);

            var result = await _announcementController.GetAllAgencyAnnouncements(_allAgencyAnnouncementSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<AllAnouuncementsForAgencyModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetUnderOperationAgencyAnnouncements_Returns_Data()
        {
            var _underOperationAgencyAnnouncementSearchCriteria = new UnderOperationAgencyAnnouncementSearchCriteria()
            {
            };
            var _commercialRegisterNo = "142154293000206";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _announcementController = _announcementController.WithIdentity(_claims);

            var result = await _announcementController.GetUnderOperationAgencyAnnouncements(_underOperationAgencyAnnouncementSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<UnderOperationAnouuncementsForAgencyModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetAllVisitorAnnouncements_Returns_Data()
        {
            var _allSupplierAnnouncementSearchCriteria = new AllSupplierAnnouncementSearchCriteria()
            {
            };

            var result = await _announcementController.GetAllVisitorAnnouncements(_allSupplierAnnouncementSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<AllAnouuncementsForSupplierVisitorModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task CreateNewAnnouncement_CreateSuccess()
        {
            var _createAnnouncementModel = new CreateAnnouncementModel()
            {
                AnnouncementName = "announcement name",
                IntroAboutTender = "intro about tender",
                AnnouncementPeriod = 1,
                InsideKsa = true,
                Details = "details",
                ActivityDescription = "activity description",
                TenderTypeId = 1,
                AgencyCode = "019001000000",
                ReasonIdForSelectingTenderType = 12,
            };

            var _branchId = "3";
            var _agencyCode = "019001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId,_branchId),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
            };
            _announcementController = _announcementController.WithIdentity(_claims);

            await _announcementController.CreateNewAnnouncement(_createAnnouncementModel);
        }

        [Fact]
        public async Task CreateNewAnnouncement_UpdateSuccess()
        {
            var _createAnnouncementModel = new CreateAnnouncementModel()
            {
                AnnouncementName = "announcement name",
                IntroAboutTender = "into about tender",
                AnnouncementPeriod = 1,
                InsideKsa = true,
                Details = "details",
                ActivityDescription = "activity description",
                AnnouncementId = 14,
                AnnouncementIdString = Util.Encrypt(14),
                TenderTypeId = 1,
                AgencyCode = "019001000000",
                ReasonIdForSelectingTenderType = 0,
            };

            var _branchId = "3";
            var _agencyCode = "019001000000";
            _claims = new Claim[3] {
                new Claim(IdentityConfigs.Claims.BranchId,_branchId),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
            };
            _announcementController = _announcementController.WithIdentity(_claims);

            await _announcementController.CreateNewAnnouncement(_createAnnouncementModel);
        }

        [Fact]
        public async Task GetAnnouncementByIdForEdit_Returns_Data()
        {
            var _announcementIdString = Util.Encrypt(13);
            var _expectedResult = new CreateAnnouncementModel()
            {
                AgencyCode = "022001000000",
                AnnouncementId = 13,
                AnnouncementIdString = "4D*@@**CkYL*@@**L*@@**yHtLTllLUmsg==",
                AnnouncementName = "اعلان اول",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
                Details = "تفاصيل",
                BranchId = 4,
                InsideKsa = true,
                IntroAboutTender = "تعريف",
                TenderTypeId = 2
            };

            var result = await _announcementController.GetAnnouncementByIdForEdit(_announcementIdString);

            Assert.NotNull(result);
            Assert.IsType<CreateAnnouncementModel>(result);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementIdString, result.AnnouncementIdString);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
            Assert.Equal(_expectedResult.CreatedBy, result.CreatedBy);
            Assert.Equal(_expectedResult.Details, result.Details);
            Assert.Equal(_expectedResult.BranchId, result.BranchId);
            Assert.Equal(_expectedResult.IntroAboutTender, result.IntroAboutTender);
            Assert.Equal(_expectedResult.TenderTypeId, result.TenderTypeId);
            Assert.False(result.InsideKsa);
        }

        [Fact]
        public async Task ApproveAnnouncement_Throws_BusinessRuleException()
        {

            var _verificationModel = new VerificationModel()
            {
                IdString = Util.Encrypt(14),
                VerificationCode = "51428",
            };
            var _exceptionMessage = "Verification Code Error Please resend verification code";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _announcementController.ApproveAnnouncement(_verificationModel));

            Assert.NotNull(result);
            Assert.Equal(_exceptionMessage, result.Message);
        }
    }
}
