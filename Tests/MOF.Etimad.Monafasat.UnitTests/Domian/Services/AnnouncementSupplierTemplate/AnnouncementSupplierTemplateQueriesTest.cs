using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services
{
    public class AnnouncementSupplierTemplateQueriesTest
    {
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly AnnouncementSupplierTemplateQueries _sut;
        public AnnouncementSupplierTemplateQueriesTest()
        {
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _sut = new AnnouncementSupplierTemplateQueries(InitialData.context, _moqHttpContextAccessor.Object);
        }

        [Fact]
        public async Task Should_FindAllAnnouncementSupplierTemplates()
        {
            MoqUser();
            var searchCriteria = new AgencyAnnouncementSupplierTemplateSearchCriteria();

            var result = await _sut.FindAllAnnouncementSupplierTemplates(searchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllAnnouncementSupplierTemplatesForSupplier()
        {
            MoqUser();
            var searchCriteria = new AnnouncementSupplierTemplateSupplierSearchCriteriaModel();

            var result = await _sut.GetAllAnnouncementSupplierTemplatesForSupplier(searchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetJoinRequestsByAnnouncementIdAsync()
        {
            var searchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria();

            var result = await _sut.GetJoinRequestsByAnnouncementIdAsync(searchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetApprovedSuppliersJoinRequestsByAnnouncementId()
        {
            var searchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria();

            var result = await _sut.GetApprovedSuppliersJoinRequestsByAnnouncementId(searchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetJoinRequestsSuppliersByAnnouncementIdAsync()
        {
            var searchCriteria = new JoinRequestSuppliersSearchCriteria();

            var result = await _sut.GetJoinRequestsSuppliersByAnnouncementIdAsync(searchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetJoinedAnnouncementSuppliers()
        {
            var result = await _sut.GetJoinedAnnouncementSuppliers(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAcceptedAnnouncementSuppliers()
        {
            var result = await _sut.GetAcceptedAnnouncementSuppliers(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderTypes()
        {
            var result = await _sut.GetTenderTypes();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementByIdForCreationStep()
        {
            var result = await _sut.GetAnnouncementByIdForCreationStep(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementSupplierTemplateDetailsForApproval()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementSupplierTemplateDetailsForApproval(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(1, It.IsAny<string>(), It.IsAny<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindAnnouncementDescriptionDetailsByAnnouncementId()
        {
            MoqUser();
            var result = await _sut.FindAnnouncementDescriptionDetailsByAnnouncementId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementWithNoIncludsById()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementWithNoIncludsById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementWithJoinRequestsById()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementWithJoinRequestsById(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementWithLinkedAgencyById()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementWithLinkedAgencyById(1);
            Assert.NotNull(result);
        }

        
        [Fact]
        public async Task ShouldGetAnnouncementByIdForEditApprovalSuccess()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementByIdForEditApproval(1);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldFindAnnouncementBasicDetailsByAnnouncementIdSuccess()
        {
            MoqUser();
            var result = await _sut.FindAnnouncementBasicDetailsByAnnouncementId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAnnouncementTemplateActivityAndAddressDetailsSuccess()
        {
            MoqUser();
            var result = await _sut.GetAnnouncementTemplateActivityAndAddressDetails(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldFindAnnouncementDetailsByAnnouncementIdForCancelationSuccess()
        {
            MoqUser();
            var result = await _sut.FindAnnouncementDetailsByAnnouncementIdForCancelation(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldFindAnnouncementDetailsByAnnouncementIdSuccess()
        {
            MoqUser();
            var result = await _sut.FindAnnouncementDetailsByAnnouncementId(1);
            Assert.NotNull(result);
        }

        

        [Fact]
        public async Task ShouldGetAnnouncementListDetailsToAddListToAgencyAnnouncementListsSuccess()
        {
            var result = await _sut.GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(1, "022001000000");
            Assert.NotNull(result);
        }


        
              [Fact]
        public async Task ShouldGetAnnouncementTemplateDetailsForSuppliersSuccess()
        {
            var result = await _sut.GetAnnouncementTemplateDetailsForSuppliers(1, "123");
            Assert.NotNull(result);
        }

        
                        [Fact]
        public async Task ShouldGetBeneficiaryAgencyByAnnouncementIdAsyncSuccess()
        {
            AnnouncementSupplierTemplateBasicSearchCriteria criteria = new AnnouncementSupplierTemplateBasicSearchCriteria();
            var result = await _sut.GetBeneficiaryAgencyByAnnouncementIdAsync(criteria);
            Assert.NotNull(result);
        }

        [Fact]

        public async Task ShouldGetTenderCountSuccess()
        {
            var result = await _sut.TenderCount(1);
            Assert.IsType<int>(result);
        }

        
        [Fact]
        public async Task ShouldGetAnnouncementTemplateJoinRequestDetailsSuccess()
        {
            var result = await _sut.GetAnnouncementTemplateJoinRequestDetails(1,"123");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetTendersByAnnouncementIdAsync()
        {
            AnnouncementSupplierTemplateBasicSearchCriteria announcementSupplierTemplateBasicSearchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria() { PageNumber = 1, PageSize = 1 };
            MoqUser();
            var result = await _sut.GetTendersByAnnouncementIdAsync(announcementSupplierTemplateBasicSearchCriteria);
            Assert.NotNull(result);
        }


        private void MoqUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("selectedAgency", "selectedAgency,022001000000");
            var usernum = new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154");
            var agencyCode = new Claim(IdentityConfigs.Claims.Agency, "022001000000");


            var idintity = new GenericIdentity(IdentityConfigs.Claims.SelectedGovAgency, "selectedAgency");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);
            idintity.AddClaim(agencyCode);
            var headers = new Dictionary<string, StringValues>();
            headers.Add("language", "ar-SA");
            context.Setup(x => x.Request.Headers)
                .Returns(new HeaderDictionary(headers));
            context.Setup(x => x.Items)
                .Returns(new Dictionary<object, object>());
            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            context.SetupGet(x => x.User.Claims).Returns(() => { return idintity.Claims; });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}
