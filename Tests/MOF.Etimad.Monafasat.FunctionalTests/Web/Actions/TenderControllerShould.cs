using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Integration.Models;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web;
using MOF.Etimad.Monafasat.Web.Contollers;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.Web.Actions
{

    public class TenderControllerShould
    {
        private readonly IWebHostEnvironment _mockWebHosting;
        private IApiClient _mockApiClient;
        private TenderController _tenderController;
        private readonly IMemoryCache _mockCache;
        private readonly ILogger<TenderController> _mockLogger;
        private readonly IHubContext<BiddingRoundHub> _mockHubContext;
        private readonly IOptionsSnapshot<RootConfiguration> _mockRootConfiguration;
        private Claim[] _claims;
        private readonly HttpContext _httpContext;
        private readonly TempDataDictionary _tempData;
        private readonly string _tenderIdString = "5nDvXCYNEdkAz VWoEarIg==";
        public TenderControllerShould()
        {
            _mockWebHosting = new Mock<IWebHostEnvironment>().Object;
            _mockCache = new Mock<IMemoryCache>().Object;
            _mockLogger = new Mock<ILogger<TenderController>>().Object;
            _mockHubContext = new Mock<IHubContext<BiddingRoundHub>>().Object;
            _mockApiClient = new Mock<IApiClient>().Object;
            _mockRootConfiguration = MockHelper.CreateIOptionSnapshotMock(new RootConfiguration());
            _httpContext = new DefaultHttpContext();
            _tempData = new TempDataDictionary(_httpContext, Mock.Of<ITempDataProvider>());
        }
        [Fact]
        public void Index_Returns_ViewResult()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, "Nothing") };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);


            // act
            var result = _tenderController.WithIdentity(_claims).Index();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Index_RedirectTo_AllTendersForVisitor()
        {
            // arrange
            _claims = new Claim[0];
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).Index();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AllTendersForVisitor", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Index_RedirectTo_AllSuppliersTenders()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.supplier) };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).Index();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AllSuppliersTenders", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Index_RedirectTo_SupplierEnquiryList()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.TechnicalCommitteeUser) };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).Index();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Enquiry", redirectToActionResult.ControllerName);
            Assert.Equal("SupplierEnquiryList", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Index_RedirectTo_Block()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.MonafasatBlackListCommittee) };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).Index();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Block", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void IndexPagingAsync_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new BasicTenderModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.WithAnonymousIdentity().IndexPagingAsync(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void IndexPagingAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().IndexPagingAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void AppliedSuppliersList_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.AppliedSuppliersList();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AppliedSuppliersListPaging_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new BasicTenderModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.AppliedSuppliersListPaging(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void AppliedSuppliersListPaging_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().AppliedSuppliersListPaging(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void GetTendersToJoinCommittees_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new LinkTendersToCommitteeModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new LinkTendersToCommitteeSearchCriteriaModel() { };

            // act
            var result = _tenderController.GetTendersToJoinCommittees(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void GetTendersToJoinCommittees_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().GetTendersToJoinCommittees(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_DataEntryTenderStatusIds()
        {
            // arrange
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = "DataEntry" };
            string listType = "";
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.DataEntry) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.DataEntry, "DataEntry");
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_AuditerTenderStatusIds()
        {
            // arrange
            string listType = "DataEntry";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.Auditer) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.Auditer, listType);
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_OffersOppeningManagerTenderStatusIds()
        {
            // arrange
            string listType = "Open";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.OffersOppeningManager) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersOppeningManager, "Open");
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_OffersOppeningSecretaryTenderStatusIds()
        {
            // arrange
            string listType = "Open";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.OffersOppeningSecretary) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersOppeningSecretary, listType);
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_OffersCheckManagerTenderStatusIds_Check()
        {
            // arrange
            string listType = "Check";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.OffersCheckManager) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckManager, listType);
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }
        [Fact]
        public void GetStatusIDs_Returns_OffersCheckManagerTenderStatusIds_Award()
        {
            // arrange
            string listType = "Award";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.OffersCheckManager) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckManager, listType);
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_OffersCheckSecretaryTenderStatusIds_Check()
        {
            // arrange
            string listType = "Check";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.OffersCheckSecretary) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckSecretary, listType);
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void GetStatusIDs_Returns_OffersCheckSecretaryTenderStatusIds_Award()
        {
            // arrange
            string listType = "Award";
            var tenderSearchCriteriaModel = new TenderSearchCriteriaModel() { TenderTypeString = listType };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.OffersCheckSecretary) };
            var expectedIDs = GetExpectedtenderStatusIds(RoleNames.OffersCheckSecretary, listType);
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act 
            var result = _tenderController.WithIdentity(_claims).GetStatusIDs(tenderSearchCriteriaModel, listType);

            // assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedIDs.Count, result.Count);
            Assert.Equal(expectedIDs[0].ToString(), result[0].ToString());
            Assert.Equal(expectedIDs[1].ToString(), result[1].ToString());
            Assert.Equal(expectedIDs[2].ToString(), result[2].ToString());
            Assert.Equal(expectedIDs[3].ToString(), result[3].ToString());
        }

        [Fact]
        public void TenderAwardedIndex_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.TenderAwardedIndex();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetSupplierInfoByCR_Returns_SupplierInfoPartialView()
        {
            // arrange
            string _supplierCR = "";
            _mockApiClient = MockHelper.CreateApiClientMock(new SupplierFullDataViewModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.GetSupplierInfoByCR(_supplierCR);

            // assert
            Assert.NotNull(result);
            var partialView = Assert.IsType<PartialViewResult>(result.Result);
            Assert.Equal("~/Views/Tender/Partials/_SupplierInfo.cshtml", partialView.ViewName);
            Assert.IsType<SupplierFullDataViewModel>(partialView.Model);
        }

        [Fact]
        public void TenderAwardedIndexPagingAsync_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderCheckingAndAwardingModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.TenderAwardedIndexPagingAsync(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void TenderAwardedIndexPagingAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().TenderAwardedIndexPagingAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void TenderIndexUnderOperationsStage_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.TenderIndexUnderOperationsStage("");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetTendersForUnderOperationsStageIndexAsync_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new BasicTenderModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.GetTendersForUnderOperationsStageIndexAsync(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void GetTendersForUnderOperationsStageIndexAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().GetTendersForUnderOperationsStageIndexAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void TenderIndexOpeningStage_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.TenderIndexOpeningStage("");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetTendersForOpeningStageIndexAsync_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderCheckingAndAwardingModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.GetTendersForOpeningStageIndexAsync(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void GetTendersForOpeningStageIndexAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().GetTendersForOpeningStageIndexAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void VROTendersCreatedByGovAgency_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.VROTendersCreatedByGovAgency();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
        [Fact]
        public void GetVROTendersCreatedByGovAgency_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new VROTendersCreatedByGovAgencyModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.GetVROTendersCreatedByGovAgency(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void GetVROTendersCreatedByGovAgency_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().GetVROTendersCreatedByGovAgency(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void TenderIndexCheckingStage_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.TenderIndexCheckingStage("");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetTendersForCheckingStageIndexAsync_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderCheckingAndAwardingModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.GetTendersForCheckingStageIndexAsync(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void GetTendersForCheckingStageIndexAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().GetTendersForCheckingStageIndexAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void TenderIndexAwardingStage_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.TenderIndexAwardingStage("");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void GetTendersForAwardingStageIndexAsync_Returns_JsonResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderCheckingAndAwardingModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var tenderSearch = new TenderSearchCriteriaModel() { };

            // act
            var result = _tenderController.GetTendersForAwardingStageIndexAsync(tenderSearch);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void GetTendersForAwardingStageIndexAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().GetTendersForAwardingStageIndexAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task RejectInitialAwardTenderOffer_Returns_Ok()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            var rejectionModel = new RejectionModel() { TenderIdString = _tenderIdString };

            // act
            var result = await _tenderController.RejectInitialAwardTenderOffer(rejectionModel);

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public void RejectInitialAwardTenderOffer_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().RejectInitialAwardTenderOffer(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task SendRequestToApplayForTenderPost_Returns_Ok()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SendRequestToApplayForTenderPost(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        }

        [Fact]
        public void SendRequestToApplayForTenderPost_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().SendRequestToApplayForTenderPost("58");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task SendRequestToApplayForTenderGet_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SendRequestToApplayForTenderGet(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderInvitationDetailsModel model = viewResult.ViewData.Model as TenderInvitationDetailsModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task SendRequestToApplayForTenderGet_Returns_RedirectToAction()
        {
            // arrange

            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;

            // act
            var result = await _tenderController.SendRequestToApplayForTenderGet("58");

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AllSuppliersTenders", redirectToActionResult.ActionName);
        }

        [Fact]
        public void ConditionsBookletTemplate_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.ConditionsBookletTemplate();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.Create();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task CreateVerificationCode_Returns_Ok()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.CreateVerificationCode(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public void CreateVerificationCode_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().CreateVerificationCode("58");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void SupplierInvitationTenders_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.SupplierInvitationTenders();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task SupplierInvitationTendersPagingAsync_Returns_JsonReult()
        {
            // arrange
            var tenderSearchCriteria = new TenderSearchCriteria() { };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.supplier) };
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithIdentity(_claims).SupplierInvitationTendersPagingAsync(tenderSearchCriteria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void SupplierInvitationTendersPagingAsync_Returns_UnexpectedError()
        {
            // arrange
            var tenderSearchCriteria = new TenderSearchCriteria() { };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().SupplierInvitationTendersPagingAsync(tenderSearchCriteria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void SupplierTenders_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.SupplierTenders();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllSuppliersTenders_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.AllSuppliersTenders();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllTendersForVisitor_Returns_ViewResult()
        {
            // arrange
            _claims = new Claim[0];
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).AllTendersForVisitor();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllTendersForVisitor_RedirectTo_AllSuppliersTenders()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.supplier) };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).AllTendersForVisitor();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("AllSuppliersTenders", redirectToActionResult.ActionName);
        }

        [Fact]
        public void AllTendersForVisitor_RedirectTo_SupplierEnquiryList()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.TechnicalCommitteeUser) };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).AllTendersForVisitor();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Enquiry", redirectToActionResult.ControllerName);
            Assert.Equal("SupplierEnquiryList", redirectToActionResult.ActionName);
        }

        [Fact]
        public void AllTendersForVisitor_RedirectTo_Block()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.MonafasatBlackListCommittee) };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).AllTendersForVisitor();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Block", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void AllTendersForVisitor_RedirectTo_Dashboard()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, "") };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithIdentity(_claims).AllTendersForVisitor();

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Dashboard", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AgencyProjectBudget_Returns_JsonReult()
        {
            // arrange
            _claims = new Claim[2] { new Claim(ClaimTypes.Role, RoleNames.supplier), new Claim(ClaimTypes.Role, IdentityConfigs.Claims.UserCategory) };
            _mockApiClient = MockHelper.CreateApiClientMock(new AgencyBudgetModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithIdentity(_claims).AgencyProjectBudget("", true);

            // assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);

        }

        [Fact]
        public async Task AllTendersAsync_Returns_JsonReult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.AllTendersAsync();

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task AllSupplierTendersForVisitorAsync_Returns_JsonReult()
        {
            // arrange
            var tenderSearchCriteria = new TenderSearchCriteriaModel() { };
            _mockApiClient = MockHelper.CreateApiClientMock(new AllTendersForVisitorModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.AllSupplierTendersForVisitorAsync(tenderSearchCriteria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void AllSupplierTendersForVisitorAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().AllSupplierTendersForVisitorAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task AllSupplierTendersAsync_Returns_JsonReult()
        {
            // arrange
            var tenderSearchCriteria = new TenderSearchCriteriaModel() { };
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.AllSupplierTendersAsync(tenderSearchCriteria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void AllSupplierTendersAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().AllSupplierTendersAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public void UnSubscribedSuppliers_Returns_ViewResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.UnSubscribedSuppliers();

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AllSubscribedSupplierTendersAsync_Returns_JsonReult()
        {
            // arrange
            var tenderSearchCriteria = new TenderSearchCriteriaModel() { };

            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.AllSubscribedSupplierTendersAsync(tenderSearchCriteria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void AllSubscribedSupplierTendersAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().AllSubscribedSupplierTendersAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task SupplierTendersAsync_Returns_JsonReult()
        {
            // arrange
            var tenderSearchCriteria = new TenderSearchCriteria() { };
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.supplier) };
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithIdentity(_claims).SupplierTendersAsync(tenderSearchCriteria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public void SupplierTendersAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = _tenderController.WithAnonymousIdentity().SupplierTendersAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result.Result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task SuppliersJoiningRequest_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInformationModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SuppliersJoiningRequest(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderInformationModel model = viewResult.ViewData.Model as TenderInformationModel;
            Assert.NotNull(model);
        }

        [Fact]
        public void SuppliersJoiningRequest_Returns_RedirectToAction()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;

            // act
            var result = _tenderController.WithAnonymousIdentity().SuppliersJoiningRequest("158");

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task SuppliersJoininqRequestPagingAsync_Returns_JsonReult()
        {
            // arrange
            _claims = new Claim[1] { new Claim(ClaimTypes.Role, RoleNames.supplier) };
            _mockApiClient = MockHelper.CreateApiClientMock(new SupplierInvitationModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithIdentity(_claims).SuppliersJoininqRequestPagingAsync(_tenderIdString);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task SuppliersJoininqRequestPagingAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().SuppliersJoininqRequestPagingAsync("120");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task TenderInvitationDetails_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.TenderInvitationDetails(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderInvitationDetailsModel model = viewResult.ViewData.Model as TenderInvitationDetailsModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task TenderInvitationDetailss_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().TenderInvitationDetails("120");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task UpdateInvitationStatus_Returns_Ok()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.UpdateInvitationStatus("", "");

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task AcceptInvitationWithFeesAsync_Returns_Ok()
        {
            // arrange
            var tenderFinantialCostModel = new TenderFinantialCostModel();
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.AcceptInvitationWithFeesAsync(tenderFinantialCostModel);

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task JoinDirectPurchaseTenderAsync_Returns_Ok()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.JoinDirectPurchaseTenderAsync(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ApproveJoiningRequestStatus_Returns_Ok()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.ApproveJoiningRequestStatus(_tenderIdString, "");

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ApproveJoiningRequestStatus_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().ApproveJoiningRequestStatus("120", "");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task RejectJoiningRequestStatus_Returns_Ok()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.RejectJoiningRequestStatus(_tenderIdString, "");

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RejectJoiningRequestStatus_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().RejectJoiningRequestStatus("120", "");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task SendTenderInvitations_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new InvitationStepModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SendTenderInvitations(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            InvitationStepModel model = viewResult.ViewData.Model as InvitationStepModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task SendTenderInvitations_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().SendTenderInvitations("120");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task SendInvitationsAsync_Returns_Ok()
        {
            // arrange
            var suppliers = new List<InvitationCrModel>();
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            // act
            var result = await _tenderController.SendInvitationsAsync(suppliers);

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SendInvitationsByEmailAsync_Returns_Ok()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            // act
            var result = await _tenderController.SendInvitationsByEmailAsync(0, new List<string>());

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SendInvitationsBySmsAsync_Returns_Ok()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            // act
            var result = await _tenderController.SendInvitationsBySmsAsync(0, new List<string>());

            // assert
            Assert.NotNull(result);
            var statusCodeResult = Assert.IsType<OkResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetSuppliersBySearchCretria_Returns_JsonResult()
        {
            // arrange
            var supplierSearchCretria = new SupplierSearchCretriaModel();
            _mockApiClient = MockHelper.CreateApiClientMock(new SupplierInvitationModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            // act
            var result = await _tenderController.GetSuppliersBySearchCretria(supplierSearchCretria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task GetSuppliersBySearchCretria_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().GetSuppliersBySearchCretria(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task GetInvitedSuppliers_Returns_JsonResult()
        {
            // arrange
            var supplierSearchCretria = new SupplierSearchCretriaModel();
            _mockApiClient = MockHelper.CreateApiClientMock(new InvitationCrModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetInvitedSuppliers(supplierSearchCretria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task GetInvitedSuppliers_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().GetInvitedSuppliers(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task GetAllInvitedSuppliersAndSendInvitationAgain_Returns_SuccessJsonResult()
        {
            // arrange
            var supplierSearchCretria = new SupplierSearchCretriaModel() { InvitationTenderIdString = _tenderIdString };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetAllInvitedSuppliersAndSendInvitationAgain(supplierSearchCretria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Contains("status = success", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task GetAllInvitedSuppliersAndSendInvitationAgain_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().GetAllInvitedSuppliersAndSendInvitationAgain(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task GetInvitedUnRegisterSuppliers_Returns_JsonResult()
        {
            // arrange
            var supplierSearchCretria = new SupplierSearchCretriaModel();
            _mockApiClient = MockHelper.CreateApiClientMock(new InvitationCrModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetInvitedUnRegisterSuppliers(supplierSearchCretria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task GetInvitedUnRegisterSuppliers_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().GetInvitedUnRegisterSuppliers(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task GetInvitedUnRegisterSuppliersForCreation_Returns_JsonResult()
        {
            // arrange
            var supplierSearchCretria = new SupplierSearchCretriaModel();
            _mockApiClient = MockHelper.CreateApiClientMock(new InvitationCrModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetInvitedUnRegisterSuppliersForCreation(supplierSearchCretria);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task GetInvitedUnRegisterSuppliersForCreation_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().GetInvitedUnRegisterSuppliersForCreation(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task JoiningRequestDetails_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderInvitationDetailsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.JoiningRequestDetails(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderInvitationDetailsModel model = viewResult.ViewData.Model as TenderInvitationDetailsModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task JoiningRequestDetails_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().JoiningRequestDetails("120");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task QuantitiesTableStep_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new QuantityTableStepDTO());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.QuantitiesTableStep(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            QuantityTableStepDTO model = viewResult.ViewData.Model as QuantityTableStepDTO;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task QuantitiesTableStep_Returns_RedirectToAction()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            // act
            var result = await _tenderController.WithAnonymousIdentity().QuantitiesTableStep("120");

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task TenderQuantitiesTableUpdates_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new QuantityTableStepDTO());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.TenderQuantitiesTableUpdates(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            QuantityTableStepDTO model = viewResult.ViewData.Model as QuantityTableStepDTO;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task TenderQuantitiesTableUpdates_Returns_RedirectToAction()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            // act
            var result = await _tenderController.WithAnonymousIdentity().TenderQuantitiesTableUpdates("120");

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task GetEmptyForm_Returns_ContentResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetEmptyForm(1, 1, 2, "");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);
        }
        [Fact]
        public async Task UpdateTableName_Returns_ContentResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.UpdateTableName(1, 1, "");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);
        }

        [Fact]
        public async Task UpdateTableChangesName_Returns_ContentResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.UpdateTableChangesName(1, 1, "");

            // assert
            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);
        }
        [Fact]
        public async Task UpdateHasAlternative_Returns_ContentResult()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.UpdateHasAlternative(1, true);

            // assert
            Assert.NotNull(result);
            Assert.IsType<ContentResult>(result);
        }

        [Fact]
        public async Task SaveTenderQuantityIteme_Returns_viewResult()
        {
            // arrange
            FormCollection formCollection = new FormCollection(new Dictionary<string, StringValues>() { { "TenderId", "12" } });
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SaveTenderQuantityItem(formCollection);

            // assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task SaveTenderQuantityIteme_Returns_JsonErrorMessage()
        {
            // arrange
            FormCollection formCollection = new FormCollection(new Dictionary<string, StringValues>() { { "", "" } });
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.ControllerContext.HttpContext = new DefaultHttpContext();
            // act
            var result = await _tenderController.SaveTenderQuantityItem(formCollection);

            // assert
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public async Task DeleteTenderQuantityItem_Returns_Ok()
        {
            // arrange
            FormCollection formCollection = new FormCollection(new Dictionary<string, StringValues>() { { "TenderId", "12" } });
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.DeleteTenderQuantityItem(formCollection);

            // assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task SaveTenderQuantityItemChangeAsync_Returns_Ok()
        {
            // arrange
            FormCollection formCollection = new FormCollection(new Dictionary<string, StringValues>() { { "TenderId", "12" } });
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SaveTenderQuantityItemChangeAsync(formCollection);

            // assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task DeleteTenderQuantityItemChangeAsync_Returns_Ok()
        {
            // arrange
            FormCollection formCollection = new FormCollection(new Dictionary<string, StringValues>() { { "TenderId", "12" } });
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.DeleteTenderQuantityItemChangeAsync(formCollection);

            // assert
            Assert.NotNull(result);
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, okObjectResult.StatusCode);
        }

        [Fact]
        public async Task QuantitiesTableStep1_Returns_ViewResult()
        {
            // arrange
            FormCollection formCollection = new FormCollection(new Dictionary<string, StringValues>() { { "TenderId", "12" } });
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.QuantitiesTableStep(formCollection);

            // assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task AttachmentsStep_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.AttachmentsStep(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            AttachmentsModel model = viewResult.Model as AttachmentsModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task TenderAttachmentsUpdates_Returns_ViewResult_IfInvalidModel()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            _tenderController.ModelState.AddModelError("Error", "ErrorMsg");
            var attachmentsModel = new AttachmentsModel() { TenderTypeId = (int)Enums.TenderType.CurrentDirectPurchase };

            // act
            var result = await _tenderController.TenderAttachmentsUpdates(attachmentsModel);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            AttachmentsModel model = viewResult.Model as AttachmentsModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task TenderAttachmentsUpdates_Returns_IfSuccessRedirectToAction()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            var attachmentsModel = new AttachmentsModel() { TenderTypeId = (int)Enums.TenderType.CurrentDirectPurchase };

            // act
            var result = await _tenderController.TenderAttachmentsUpdates(attachmentsModel);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("TenderAttachmentsChangesApprovement", redirectToActionResult.ActionName);
            Assert.Equal("Tender", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task GetAttahmentsStepModel_Returns_Model()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetAttahmentsStepModel(_tenderIdString);

            // assert
            Assert.NotNull(result);
            Assert.IsType<AttachmentsModel>(result);
        }

        [Fact]
        public async Task AttachmentsStep_Returns_ViewResult_IfInvalidModel()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            _tenderController.ModelState.AddModelError("Error", "ErrorMsg");
            var attachmentsModel = new AttachmentsModel() { TenderTypeId = (int)Enums.TenderType.CurrentDirectPurchase };

            // act
            var result = await _tenderController.AttachmentsStep(attachmentsModel);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            AttachmentsModel model = viewResult.Model as AttachmentsModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task AttachmentsStep_Returns_IfSuccessRedirectToAction()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            var attachmentsModel = new AttachmentsModel() { TenderTypeId = (int)Enums.TenderType.CurrentDirectPurchase };

            // act
            var result = await _tenderController.AttachmentsStep(attachmentsModel);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task SendInvitationsStep_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new InvitationStepModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.SendInvitationsStep(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            InvitationStepModel model = viewResult.Model as InvitationStepModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task SendInvitationsStep_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().SendInvitationsStep("50");

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task GetAllSuppliersBySearchCretriaForInvitationsAsync_Returns_JsonResult()
        {
            // arrange
            var supplierSearchCretriaModel = new SupplierSearchCretriaModel();
            _mockApiClient = MockHelper.CreateApiClientMock(new SupplierInvitationModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.GetAllSuppliersBySearchCretriaForInvitationsAsync(supplierSearchCretriaModel);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.IsType<PaginationModel>(jsonResult.Value);
        }

        [Fact]
        public async Task GetAllSuppliersBySearchCretriaForInvitationsAsync_Returns_UnexpectedError()
        {
            // arrange
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.WithAnonymousIdentity().GetAllSuppliersBySearchCretriaForInvitationsAsync(null);

            // assert
            Assert.NotNull(result);
            JsonResult jsonResult = Assert.IsType<JsonResult>(result);
            Assert.Equal("{ message = Unexpected Error }", jsonResult.Value.ToString());
        }

        [Fact]
        public async Task Delete_Returns_RedirectToAction()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.Delete(_tenderIdString);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("TenderIndexUnderOperationsStage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_Returns_RedirectToActionIfThrowsException()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;

            // act
            var result = await _tenderController.Delete("60");

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("TenderIndexUnderOperationsStage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ConvertTenderInvitationToPublic_Returns_RedirectToAction()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.ConvertTenderInvitationToPublic(_tenderIdString);

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ConvertTenderInvitationToPublic_Returns_RedirectToActionIfThrowsException()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;

            // act
            var result = await _tenderController.ConvertTenderInvitationToPublic("60");

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ExtendTenderDates_Returns_ViewResult()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderDatesModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);

            // act
            var result = await _tenderController.ExtendTenderDates(_tenderIdString);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderDatesModel model = viewResult.Model as TenderDatesModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task ExtendTenderDates_Returns_RedirectToActionIfThrowsException()
        {
            // arrange
            _mockApiClient = MockHelper.CreateApiClientMock(new AttachmentsModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;

            // act
            var result = await _tenderController.ExtendTenderDates("60");

            // assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ExtendTenderDates_Returns_ViewIfInvalidModelState()
        {
            // arrange
            var tenderDatesModel = new TenderDatesModel() { LastOfferPresentationDate = DateTime.Now };
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderDatesModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            _tenderController.ModelState.AddModelError("Error", "ErrorMsg");
            // act
            var result = await _tenderController.ExtendTenderDates(tenderDatesModel);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderDatesModel model = viewResult.Model as TenderDatesModel;
            Assert.NotNull(model);
        }

        [Fact]
        public async Task ExtendTenderDates_Returns_RedirectToActionIfSuccess()
        {
            // arrange
            var tenderDatesModel = new TenderDatesModel() { TenderIdString = _tenderIdString };
            _mockApiClient = MockHelper.CreateApiClientMock(new TenderDatesModel());
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;
            // act
            var result = await _tenderController.ExtendTenderDates(tenderDatesModel);

            // assert
            Assert.NotNull(result);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("TenderExtendDateApprovement", redirectToActionResult.ActionName);
            Assert.Equal("Tender", redirectToActionResult.ControllerName);
        }
        [Fact]
        public async Task ExtendTenderDates_Returns_ViewIfFailed()
        {
            // arrange
            var tenderDatesModel = new TenderDatesModel() { TenderIdString = "20" };
            _tenderController = new TenderController(_mockWebHosting, _mockApiClient, _mockLogger, _mockCache, _mockHubContext, _mockRootConfiguration);
            _tenderController.TempData = _tempData;

            // act
            var result = await _tenderController.ExtendTenderDates(tenderDatesModel);

            // assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewResult>(result);
            TenderDatesModel model = viewResult.Model as TenderDatesModel;
            Assert.NotNull(model);
        }

        //EditCommittees
        private List<int> GetExpectedtenderStatusIds(string roleName, string listType)
        {
            List<int> tenderStatusIds = new List<int>();
            if ((roleName == RoleNames.DataEntry || roleName == RoleNames.Auditer) && listType == "DataEntry")
            {
                tenderStatusIds.Add((int)Enums.TenderStatus.UnderEstablishing);
                tenderStatusIds.Add((int)Enums.TenderStatus.Pending);
                tenderStatusIds.Add((int)Enums.TenderStatus.Established);
                tenderStatusIds.Add((int)Enums.TenderStatus.Approved);
            }
            else if ((roleName == RoleNames.OffersOppeningManager || roleName == RoleNames.OffersOppeningSecretary) && listType == "Open")
            {
                tenderStatusIds.Add((int)Enums.TenderStatus.Approved);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppening);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedPending);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
                tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedRejected);
            }
            else if ((roleName == RoleNames.OffersCheckManager || roleName == RoleNames.OffersCheckSecretary) && (listType == "Check" || listType == "Award"))
            {
                if (listType.Equals("Check"))
                {
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersOppenedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedPending);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedRejected);
                }
                else if (listType.Equals("Award"))
                {
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersCheckedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwarding);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedPending);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedConfirmed);
                    tenderStatusIds.Add((int)Enums.TenderStatus.OffersAwardedRejected);
                }
                else
                    return null;
            }
            return tenderStatusIds;
        }
    }
}