using AutoMapper;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.IDMDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Suppliers
{
    public class SupplierServiceTest
    {
        private readonly Mock<IYasserProxy> _moqIYasserproxy;
        private readonly Mock<ISupplierCommands> _moqSupplierCommands;
        private readonly Mock<ISupplierQueries> _moqSupplierQueries;
        private readonly Mock<IIDMAppService> _moqIidmService;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<IBlockAppService> _moqBlockAppService;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<IGenericCommandRepository> _moqGenericCommandRepository;
        private readonly SupplierService _sut;

        public SupplierServiceTest()
        {
            _moqIYasserproxy = new Mock<IYasserProxy>();
            _moqSupplierCommands = new Mock<ISupplierCommands>();
            _moqSupplierQueries = new Mock<ISupplierQueries>();
            _moqIidmService = new Mock<IIDMAppService>();
            _moqMapper = new Mock<IMapper>();
            _moqBlockAppService = new Mock<IBlockAppService>();
            _moqNotificationAppService = new Mock<INotificationAppService>();
            _moqGenericCommandRepository = new Mock<IGenericCommandRepository>();
            _sut = new SupplierService(InitialData.context, _moqIYasserproxy.Object, _moqSupplierCommands.Object, _moqSupplierQueries.Object, _moqIidmService.Object, _moqMapper.Object,
                _moqBlockAppService.Object, _moqNotificationAppService.Object);
        }

        [Fact]
        public async Task Should_GetInvitedSuppliers()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetInvitedSuppliers(supplierSearchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetInvitedSuppliers(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetInvitedUnRegisterSuppliers()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetInvitedUnRegisterSuppliers(supplierSearchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetInvitedUnRegisterSuppliers(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetInvitedUnRegisterSuppliersForCreation()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetInvitedUnRegisterSuppliersForCreation(supplierSearchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetInvitedUnRegisterSuppliersForCreation(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetSolidarityInvitedSuppliers()
        {
            SolidaritySearchCriteria solidaritySearchCriteria = new SolidaritySearchCriteria();
            _moqSupplierQueries.Setup(s => s.GetSolidarityInvitedSuppliers(solidaritySearchCriteria))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SolidarityInvitedRegisteredSupplierModel>>(new OfferDefaults().GetSolidarityInvitedRegisteredSupplierModelQueryResult());
                });

            var result = await _sut.GetSolidarityInvitedSuppliers(solidaritySearchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetInvitedUnregisteredSuppliers()
        {
            SolidarityUnregisteredSearchCriteria solidarityUnregisteredSearchCriteria = new SolidarityUnregisteredSearchCriteria();
            _moqSupplierQueries.Setup(s => s.GetSolidarityInvitedUnregisteredSuppliers(solidarityUnregisteredSearchCriteria))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SolidarityInvitedUnRegisteredSupplierModel>>(new OfferDefaults().GetSolidarityInvitedUnRegisteredSupplierModelQueryResult());
                });

            var result = await _sut.GetInvitedUnregisteredSuppliers(solidarityUnregisteredSearchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSuppliersBySearchCretriaForSolidarity()
        {
            SolidaritySearchCriteria solidaritySearchCriteria = new SolidaritySearchCriteria();
            solidaritySearchCriteria.CurrentSupplierCR = "1299659801";
            SupplierIntegrationSearchCriteria integrationSearchCriteria = new SupplierIntegrationSearchCriteria() { CrNumber = "1299659801" };

            _moqMapper.Setup(m => m.Map<SupplierIntegrationSearchCriteria>(It.IsAny<SolidaritySearchCriteria>()))
                .Returns(() => { return integrationSearchCriteria; });
            _moqIidmService.Setup(s => s.GetSupplierDetailsBySearchCriteria(It.IsAny<SupplierIntegrationSearchCriteria>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierIntegrationModel>>(new IDMDefaults().GetSupplierIntegrationModelQueryResult());
                });

            var result = await _sut.GetAllSuppliersBySearchCretriaForSolidarity(solidaritySearchCriteria);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSuppliersBySearchCretriaForInvitation()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();

            _moqSupplierQueries.Setup(q => q.GetTenderByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });
            _moqIidmService.Setup(s => s.GetSupplierDetailsBySearchCriteria(It.IsAny<SupplierIntegrationSearchCriteria>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierIntegrationModel>>(new IDMDefaults().GetSupplierIntegrationModelQueryResult());
                });
            _moqBlockAppService.Setup(b => b.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<bool>(false);
                });

            var result = await _sut.GetAllSuppliersBySearchCretriaForInvitation(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSuppliersData()
        {
            _moqSupplierQueries.Setup(s => s.GetAllSuppliersData())
                .Returns(() =>
                {
                    return Task.FromResult<List<SupplierModel>>(new OfferDefaults().GetSupplierModels());
                });

            var result = await _sut.GetAllSuppliersData();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetSuppliersCountFromIDM()
        {

            _moqIidmService.Setup(s => s.GetSupplierDetailsBySearchCriteria(It.IsAny<SupplierIntegrationSearchCriteria>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierIntegrationModel>>(new IDMDefaults().GetSupplierIntegrationModelQueryResult());
                });

            var result = await _sut.GetSuppliersCountFromIDM();
            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task Should_GetAllSuppliers_ByFavoriteSupplierCrs()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            supplierSearchCretriaModel.FavouriteSupplierListId = 1;
            _moqSupplierQueries.Setup(s => s.GetFavouriteSuppliersByListId(It.IsAny<SupplierSearchCretriaModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierInvitationModel>>(new IDMDefaults().GetSupplierInvitationModelQueryResults());
                });
            _moqIidmService.Setup(i => i.GetSupplierDetailsBySearchCriteria(It.IsAny<SupplierIntegrationSearchCriteria>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierIntegrationModel>>(new IDMDefaults().GetSupplierIntegrationModelQueryResult());
                });


            _moqSupplierQueries.Setup(s => s.SuppliersInFavourite(supplierSearchCretriaModel, It.IsAny<List<SupplierInvitationModel>>()))
                .Returns(() =>
                {
                    return Task.FromResult<Task>(It.IsAny<Task>());
                });


            _moqMapper.Setup(x => x.Map<QueryResult<SupplierInvitationModel>>(It.IsAny<QueryResult<SupplierIntegrationModel>>())).Returns(() =>
            {
                return new IDMDefaults().GetSupplierInvitationModelQueryResults();
            });


            var result = await _sut.GetAllSuppliers(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSuppliers_WithoutFavoriteCr()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            _moqIidmService.Setup(i => i.GetSupplierDetailsBySearchCriteria(It.IsAny<SupplierIntegrationSearchCriteria>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierIntegrationModel>>(new IDMDefaults().GetSupplierIntegrationModelQueryResult());
                });
            _moqSupplierQueries.Setup(s => s.SuppliersInFavourite(supplierSearchCretriaModel, It.IsAny<List<SupplierInvitationModel>>()))
                .Returns(() =>
                {
                    return Task.FromResult<Task>(It.IsAny<Task>());
                });
            _moqMapper.Setup(x => x.Map<QueryResult<SupplierInvitationModel>>(It.IsAny<QueryResult<SupplierIntegrationModel>>())).Returns(() =>
            {
                return new IDMDefaults().GetSupplierInvitationModelQueryResults();
            });

            var result = await _sut.GetAllSuppliers(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetInvitedSuppliersForInvitationInTenderCreation()
        {
            SupplierSearchCretriaModel supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetInvitedSuppliersForInvitationInTenderCreation(supplierSearchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetInvitedSuppliersForInvitationInTenderCreation(supplierSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetQualifiedSuppliers()
        {
            TenderIdSearchCretriaModel tenderIdSearchCretriaModel = new TenderIdSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetQualifiedSuppliers(tenderIdSearchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetQualifiedSuppliers(tenderIdSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAcceptedSupplierInFirstStageTender()
        {
            TenderIdSearchCretriaModel tenderIdSearchCretriaModel = new TenderIdSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetAcceptedSupplierInFirstStageTender(tenderIdSearchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetAcceptedSupplierInFirstStageTender(tenderIdSearchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementTemplateSuppliers()
        {
            AnnouncementSupplierTemplateInvitationSearchmodel searchCretriaModel = new AnnouncementSupplierTemplateInvitationSearchmodel();
            _moqSupplierQueries.Setup(s => s.GetAnnouncementTemplateSuppliers(searchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<InvitationCrModel>>(new IDMDefaults().InvitationCrModelsQueryResults());
                });

            var result = await _sut.GetAnnouncementTemplateSuppliers(searchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetEmailsForUnregisteredSuppliers()
        {
            SupplierSearchCretriaModel searchCretriaModel = new SupplierSearchCretriaModel();
            List<string> vs = new List<string>() { "a@a.a" };
            _moqSupplierQueries.Setup(s => s.GetEmailsForUnregisteredSuppliers(searchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<string>>(new QueryResult<string>(vs, vs.Count, 1,1));
                });

            var result = await _sut.GetEmailsForUnregisteredSuppliers(searchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetMobileForUnregisteredSuppliers()
        {
            SupplierSearchCretriaModel searchCretriaModel = new SupplierSearchCretriaModel();
            List<string> vs = new List<string>() { "1111111111" };
            _moqSupplierQueries.Setup(s => s.GetMobileForUnregisteredSuppliers(searchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<string>>(new QueryResult<string>(vs, vs.Count, 1, 1));
                });

            var result = await _sut.GetMobileForUnregisteredSuppliers(searchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllSuppliersBySearchCretriaForInvitations()
        {
            SupplierSearchCretriaModel searchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetAllSuppliersBySearchCretriaForInvitations(searchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierInvitationModel>>(new IDMDefaults().GetSupplierInvitationModelQueryResults());
                });

            var result = await _sut.GetAllSuppliersBySearchCretriaForInvitations(searchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement()
        {
            SupplierSearchCretriaModel searchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(searchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<UnRegisterSupplierInvitationModel>>(new IDMDefaults().UnRegisterSupplierInvitationModelQueryResults());
                });

            var result = await _sut.GetInvitedUnregisteredSuppliersForInvitationAfterTenderApprovement(searchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetInvitedSuppliersForInvitationAfterTenderApprovement()
        {
            SupplierSearchCretriaModel searchCretriaModel = new SupplierSearchCretriaModel();
            _moqSupplierQueries.Setup(s => s.GetInvitedSuppliersForInvitationAfterTenderApprovement(searchCretriaModel))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<SupplierInvitationModel>>(new IDMDefaults().GetSupplierInvitationModelQueryResults());
                });

            var result = await _sut.GetInvitedSuppliersForInvitationAfterTenderApprovement(searchCretriaModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldAddFavouriteSupplierListThrowException()
        {
            FavouriteSupplierListModel favouriteSupplierList = new FavouriteSupplierListModel() {Name="name",BranchId=1 };
                _moqSupplierQueries.Setup(s => s.FindFavouriteListByName(It.IsAny<string>(),It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<FavouriteSupplierList>(new IDMDefaults().GetFavouriteSupplierList());
                });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.AddFavouriteSupplierList(favouriteSupplierList));
        }

        [Fact]
        public async Task ShouldGetNitaqatInformationInquirySuccess()
        {
            _moqIYasserproxy.Setup(s => s.NitaqatInformationInquiry(It.IsAny<NitaqatInformationRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<NitaqatInformationResponseModel>(new IDMDefaults().GetNitaqatInformationResponseModel());
                });

            var result = await _sut.NitaqatInformationInquiry("1","1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetNitaqatInformationInquiryWarningMessageSuccess()
        {
            _moqIYasserproxy.Setup(s => s.NitaqatInformationInquiry(It.IsAny<NitaqatInformationRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<NitaqatInformationResponseModel>(null);
                });

            var result = await _sut.NitaqatInformationInquiry("1", "1");
            Assert.NotNull(result);
        }


       




        [Theory]
        [InlineData(true,"success")]
        [InlineData(true, "Invalid")]
        [InlineData(false, "Invalid")]
        public async Task ShouldGetLicenseStatusInquirySuccess(bool _hasLicense,string _statusLicence)
        {
            _moqIYasserproxy.Setup(s => s.LicenseStatusInquiry(It.IsAny<LicenseDetailsRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<LicenseDetailsResponseModel>(new IDMDefaults().GetLicenseDetailsResponseData(_hasLicense, _statusLicence));
                });

            var result = await _sut.LicenseStatusInquiry("licenseNumber");
            Assert.NotNull(result);
        }

      

        [Fact]
        public async Task ShouldGetGOSICertificateInquirySuccess()
        {
            _moqIYasserproxy.Setup(s => s.GOSICertificateInquiry(It.IsAny<GOSICertificateInquiryRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<GOSICertificateInquiryResponseModel>(new IDMDefaults().GetGOSICertificateInquiryResponseModel());
                });

            var result = await _sut.GOSICertificateInquiry("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetGOSICertificateInquiryWarningMesssageSuccess()
        {
            _moqIYasserproxy.Setup(s => s.GOSICertificateInquiry(It.IsAny<GOSICertificateInquiryRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<GOSICertificateInquiryResponseModel>(null);
                });

            var result = await _sut.GOSICertificateInquiry("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetContractorDetailsInquiryWarnningMessageSuccess()
        {
            _moqIYasserproxy.Setup(s => s.ContractorDetailsInquiry(It.IsAny<ContractorDetailsRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<ContractorDetailsResponseModel>(null);
                });

            var result = await _sut.ContractorDetailsInquiry("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetContractorDetailsInquirySuccess()
        {
            _moqIYasserproxy.Setup(s => s.ContractorDetailsInquiry(It.IsAny<ContractorDetailsRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<ContractorDetailsResponseModel>(new IDMDefaults().GetContractorDetailsResponseData());
                });

            var result = await _sut.ContractorDetailsInquiry("1");
            Assert.NotNull(result);
        }

       

        [Theory]
        [InlineData(5)]
        [InlineData(-5)]
        public async Task ShouldZakatCertificateInquiryByCRSuccess(int days)
        {
            _moqIYasserproxy.Setup(s => s.ZakatCertificateInquiryByCR(It.IsAny<ZakatCertificateInquiryRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<ZakatCertificateInquiryResponseModel>(new IDMDefaults().GetZakatCertificateInquiryResponseData(days));
                });

            var result = await _sut.ZakatCertificateInquiryByCR("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldZakatCertificateInquiryByCRWarnningMessageSuccess()
        {
            _moqIYasserproxy.Setup(s => s.ZakatCertificateInquiryByCR(It.IsAny<ZakatCertificateInquiryRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<ZakatCertificateInquiryResponseModel>(null);
                });

            var result = await _sut.ZakatCertificateInquiryByCR("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetCOCSubscriptionBySijilNumberWarnningMessageSuccess()
        {
            _moqIYasserproxy.Setup(s => s.GetCOCSubscriptionBySijilNumber(It.IsAny<COCSubscriptionInquiryRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<COCSubscriptionInquiryModel>(null);
                });

            var result = await _sut.GetCOCSubscriptionBySijilNumber("1","1");
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(-5)]
        public async Task ShouldGetCOCSubscriptionBySijilNumberSuccess(int days)
        {
            _moqIYasserproxy.Setup(s => s.GetCOCSubscriptionBySijilNumber(It.IsAny<COCSubscriptionInquiryRequestModel>()))
                .Returns(() =>
                {
                    return Task.FromResult<COCSubscriptionInquiryModel>(new IDMDefaults().GetCOCSubscriptionInquiryData(days));
                });

            var result = await _sut.GetCOCSubscriptionBySijilNumber("1", "1");
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldValidateMCICRAndGetInfoWarnningMessageSuccess()
        {
            _moqIYasserproxy.Setup(s => s.ValidateMCICRAndGetInfo(It.IsAny<MCICRInfoModelRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult<MCICRInfoModel>(null);
                });
            var result = await _sut.ValidateMCICRAndGetInfo("1");
            Assert.NotNull(result);
        }

        

       [Fact]
        public async Task ShouldValidateMCICRAndGetInfoWarnningMessageWhenCrExpiredSuccess()
        {
            _moqIYasserproxy.Setup(s => s.ValidateMCICRAndGetInfo(It.IsAny<MCICRInfoModelRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult<MCICRInfoModel>(new MCICRInfoModel() {ResponseCode= "E001199" });
                });
            var result = await _sut.ValidateMCICRAndGetInfo("1");
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(-5)]
        public async Task ShouldValidateMCICRAndGetInfSuccess(int days)
        {
            _moqIYasserproxy.Setup(s => s.ValidateMCICRAndGetInfo(It.IsAny<MCICRInfoModelRequest>()))
                .Returns(() =>
                {
                    return Task.FromResult<MCICRInfoModel>(new MCICRInfoModel() {ExpiryDateHjri=DateTime.Now.AddDays(days)});
                });
            var result = await _sut.ValidateMCICRAndGetInfo("1");
            Assert.NotNull(result);
        }

    }
}
