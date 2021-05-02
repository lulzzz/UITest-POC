using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.ViewModel;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Suppliers
{
    public class SupplierQueriesTest
    {
        private readonly SupplierQueries _sut;
        public SupplierQueriesTest()
        {
            _sut = new SupplierQueries(InitialData.context);
        }

        [Fact]
        public async Task ShouldGetTenderByTenderIdSucess()
        {
            var result = await _sut.GetTenderByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAllSuppliersDataSucess()
        {
            var result = await _sut.GetAllSuppliersData();
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetSolidarityInvitedUnregisteredSuppliersSucess()
        {
            SolidarityUnregisteredSearchCriteria cretria = new SolidarityUnregisteredSearchCriteria();
            var result = await _sut.GetSolidarityInvitedUnregisteredSuppliers(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetSolidarityInvitedSuppliersSucess()
        {
            SolidaritySearchCriteria cretria = new SolidaritySearchCriteria();
            var result = await _sut.GetSolidarityInvitedSuppliers(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitedSuppliersSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetInvitedSuppliers(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitedUnRegisterSuppliersSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetInvitedUnRegisterSuppliers(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitedUnRegisterSuppliersForCreationSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetInvitedUnRegisterSuppliersForCreation(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitedSuppliersForInvitationInTenderCreationSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetInvitedSuppliersForInvitationInTenderCreation(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetQualifiedSuppliersSucess()
        {
            TenderIdSearchCretriaModel cretria = new TenderIdSearchCretriaModel();
            var result = await _sut.GetQualifiedSuppliers(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetAcceptedSupplierInFirstStageTenderSucess()
        {
            TenderIdSearchCretriaModel cretria = new TenderIdSearchCretriaModel();
            var result = await _sut.GetAcceptedSupplierInFirstStageTender(cretria);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetAnnouncementTemplateSuppliersSucess()
        {
            AnnouncementSupplierTemplateInvitationSearchmodel cretria = new AnnouncementSupplierTemplateInvitationSearchmodel();
            var result = await _sut.GetAnnouncementTemplateSuppliers(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetEmailsForUnregisteredSuppliersSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetEmailsForUnregisteredSuppliers(cretria);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetMobileForUnregisteredSuppliersSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetMobileForUnregisteredSuppliers(cretria);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldGetAllSuppliersBySearchCretriaForInvitationsSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetAllSuppliersBySearchCretriaForInvitations(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovementSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitedSuppliersForInvitationAfterTenderApprovementSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            var result = await _sut.GetInvitedSuppliersForInvitationAfterTenderApprovement(cretria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetInvitatedSupplierSucess()
        {
            SupplierSearchCretriaModel cretria = new SupplierSearchCretriaModel();
            await _sut.GetInvitatedSupplier(cretria,new System.Collections.Generic.List<InvitationCrModel>());
            Assert.True(true);
        }
    }
}
