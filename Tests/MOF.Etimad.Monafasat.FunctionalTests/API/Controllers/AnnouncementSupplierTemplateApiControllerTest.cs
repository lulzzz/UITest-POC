using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
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
    public class AnnouncementSupplierTemplateApiControllerTest : BaseTestApiController
    {
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private readonly IAnnouncementSupplierTemplateAppService announcementAppService;
        private readonly IAnnouncementTemplateJoinRequestAppService announcementJoinRequestAppService;
        private AnnouncementSupplierTemplateController _announcementController;
        private Claim[] _claims;
        public AnnouncementSupplierTemplateApiControllerTest()
        {

            var serviceProvider = services.BuildServiceProvider();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            announcementAppService = serviceProvider.GetService<IAnnouncementSupplierTemplateAppService>();
            announcementJoinRequestAppService = serviceProvider.GetService<IAnnouncementTemplateJoinRequestAppService>();
            _announcementController = new AnnouncementSupplierTemplateController(rootConfigurationsMock.Object, announcementAppService, announcementJoinRequestAppService);
        }


        [Fact]
        public async Task GetAnnouncementByIdForEdit_Returns_Data()
        {
            var _announcementIdString = Util.Encrypt(13);
            var _expectedResult = new CreateAnnouncementSupplierTemplateModel()
            {
                ActivityDescription = "NN",
                AgencyCode = "022001000000",
                AnnouncementId = 13,
                AnnouncementIdString = "4D*@@**CkYL*@@**L*@@**yHtLTllLUmsg==",
                AnnouncementSupplierTemplateName = "اعلان اول",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
                BranchId = 4,
                InsideKsa = true,
                TenderTypeId = 2
            };
            var result = await _announcementController.GetAnnouncementSupplierTemplateByIdForEdit(_announcementIdString);
            Assert.NotNull(result);
            Assert.IsType<CreateAnnouncementSupplierTemplateModel>(result);
            Assert.Equal(_expectedResult.ActivityDescription, result.ActivityDescription);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementIdString, result.AnnouncementIdString);
            Assert.Equal(_expectedResult.BranchId, result.BranchId);
            Assert.True(result.InsideKsa);
        }

        [Fact]
        public async Task GetAnnouncementByIdForExtendAnnouncement_Returns_Data()
        {
            var _announcementIdString = Util.Encrypt(13);
            var _expectedResult = new ExtendAnnouncementSupplierTemplateModel()
            {
                ActivityDescription = "NN",
                AgencyCode = "022001000000",
                AnnouncementId = 13,
                AnnouncementIdString = "4D*@@**CkYL*@@**L*@@**yHtLTllLUmsg==",
                AnnouncementSupplierTemplateName = "اعلان اول",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
                BranchId = 4,
                InsideKsa = true,
                TenderTypeId = 2
            };
            var result = await _announcementController.GetAnnouncementByIdForExtendAnnouncement(_announcementIdString);
            Assert.NotNull(result);
            Assert.IsType<ExtendAnnouncementSupplierTemplateModel>(result);
            Assert.Equal(_expectedResult.ActivityDescription, result.ActivityDescription);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementIdString, result.AnnouncementIdString);
            Assert.Equal(_expectedResult.BranchId, result.BranchId);
            Assert.True(result.InsideKsa);
        }



        [Fact]
        public async Task GetAnnouncementDetailsByAnnouncementIdForPrint_Returns_Data()
        {
            var _expectedResult = new AnnouncementTemplateDetailsForPrintModel()
            {
                ActivityDescription = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
                AgencyCode = "030001000000",
                AnnouncementId = 1,
                Details = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "030001000000")
            };
            _announcementController = _announcementController.WithIdentity(_claims);
            var result = await _announcementController.AnnouncementSuppliersTemplateJoinRequestsDetailsReport(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateDetailsForPrintModel>(result);
            Assert.Equal(_expectedResult.ActivityDescription, result.ActivityDescription);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.Details, result.Details);
        }

        [Fact]
        public async Task GetAnnouncementTemplateDetailsForSupplierForPrint_Returns_Data()
        {
            var _expectedResult = new AnnouncementTemplateDetailsForSupplierForPrintModel()
            {
                ActivityDescription = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
                AgencyCode = "030001000000",
                AnnouncementId = 1,
                Details = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "030001000000")
            };
            _announcementController = _announcementController.WithIdentity(_claims);
            var result = await _announcementController.GetAnnouncementTemplateJoinRequestsDetailsReportForSupplier(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateDetailsForSupplierForPrintModel>(result);
            Assert.Equal(_expectedResult.ActivityDescription, result.ActivityDescription);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.Details, result.Details);
        }

        [Fact]
        public async Task GetAnnouncementTemplateListDetail_Returns_Data()
        {
            var _expectedResult = new AnnouncementTemplateListDetailsModel()
            {
                UsingListCount = 0,
                AcceptedSuppliersCount = 2,
                JoinRequestCount = 4,
                UsingListCountInternally = 0
            };

            var result = await _announcementController.GetAnnouncementTemplateListDetail(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateListDetailsModel>(result);
            Assert.Equal(_expectedResult.UsingListCount, result.UsingListCount);
            Assert.Equal(_expectedResult.AcceptedSuppliersCount, result.AcceptedSuppliersCount);
            Assert.Equal(_expectedResult.JoinRequestCount, result.JoinRequestCount);
            Assert.Equal(_expectedResult.UsingListCountInternally, result.UsingListCountInternally);
        }

        


        [Fact]
        public async Task GetAnnouncementBasicDetails_Returns_Data()
        {
            var _expectedResult = new AnnouncementBasicDetailModel()
            {
                AnnouncementName = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
            };

            var result = await _announcementController.GetAnnouncementBasicDetails(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementBasicDetailModel>(result);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
        }

        
        [Fact]
        public async Task GetAnnouncementSupplierTemplateDetails_Returns_Data()
        {
            var _expectedResult = new AnnouncementSuppliersTemplateDetailsModel()
            {
                AnnouncementName = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
                AgencyCode = "030001000000",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
            };

            var result = await _announcementController.GetAnnouncementSupplierTemplateDetails(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementSuppliersTemplateDetailsModel>(result);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
            Assert.Equal(_expectedResult.AgencyCode, result.AgencyCode);
        }

        [Fact]
        public async Task GetAnnouncementSupplierTemplateForApproval_Returns_Data()
        {
            var _expectedResult = new ApproveAnnouncemntSupplierTemplateModel()
            {
              AnnouncementId=1,
            };

            var result = await _announcementController.AnnouncementSupplierTemplateForApproval(1);
            Assert.NotNull(result);
            Assert.IsType<ApproveAnnouncemntSupplierTemplateModel>(result);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
        }

        
       [Fact]
        public async Task GetAnnouncementSuppliersTemplateCancelModel_Returns_Data()
        {
            var _expectedResult = new AnnouncementSuppliersTemplateCancelModel()
            {
                AnnouncementId = 1,
                AnnouncementName = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
                CreatedBy = "مدخل وسكرتير نظام المنافسات والمشتريات | 5555555555",
            };

            var result = await _announcementController.GetAnnouncementSupplierTemplateByIdForCancelation(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementSuppliersTemplateCancelModel>(result);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
        }

        
        [Fact]
        public async Task GetGetAnnouncementTemplateActivityAndAddressDetails_Returns_Data()
        {
            var _expectedResult = new AnnouncementTemplateActivityAndAreaDetailsModel()
            {
                AnnouncementId = 1,
            };

            var result = await _announcementController.GetAnnouncementTemplateActivityAndAddressDetails(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateActivityAndAreaDetailsModel>(result);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
        }


        [Fact]
        public async Task GetAnnouncementTemplateDetailsData_Returns_Data()
        {
            var _expectedResult = new AnnouncementSuppliersTemplateDetailsModel()
            {
                AnnouncementId = 1,
                AnnouncementName = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
            };

            var result = await _announcementController.GettAnnouncementTemplateDetailsData(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementSuppliersTemplateDetailsModel>(result);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
        }

        [Fact]
        public async Task GetAnnouncementTemplateMainDetails_Returns_Data()
        {
            var _expectedResult = new AnnouncementTemplateMainDetailsModel()
            {
                AnnouncementId = 1,
                AnnouncementName = "اول قائمة عامة اعلانات قوائم الموردين من مدخل البيانات بدون مدة سريان",
            };
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "030001000000")
            };
            _announcementController = _announcementController.WithIdentity(_claims);
            var result = await _announcementController.GetAnnouncementTemplateDetailsForSuppliers(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateMainDetailsModel>(result);
            Assert.Equal(_expectedResult.AnnouncementId, result.AnnouncementId);
            Assert.Equal(_expectedResult.AnnouncementName, result.AnnouncementName);
        }


        [Fact]
        public async Task GetGetAnnouncementDescriptionDetails_Returns_Data()
        {
            var result = await _announcementController.GetAnnouncementDescriptionDetails(1);
            Assert.NotNull(result);
            Assert.IsType<AnnouncementDescriptionModel>(result);
        }

        [Fact]
        public async Task GetAnnouncementSuppliersTemplateJoinRequests_Returns_Data()
        {
            _claims = new Claim[2] {
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, "122450691000106")
            };
            _announcementController = _announcementController.WithIdentity(_claims);
            var result = await _announcementController.GetAnnouncementJoinRequestDetails(9);
            Assert.NotNull(result);
        }
    }
}
