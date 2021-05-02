using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class OfferApiControllerTest : BaseTestApiController
    {
        private readonly IOfferAppService offerAppService;
        private readonly ITenderAppService tenderAppService;
        private readonly IVerificationCodeService verificationCodeService;
        private readonly ISupplierService supplierService;
        private readonly IMapper mapper;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private Claim[] _claims;
        private OfferController _offerController;
        private OfferDefaults _offerDefaults;
        public OfferApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            offerAppService = serviceProvider.GetService<IOfferAppService>();
            tenderAppService = serviceProvider.GetService<ITenderAppService>();
            verificationCodeService = serviceProvider.GetService<IVerificationCodeService>();
            supplierService = serviceProvider.GetService<ISupplierService>();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OfferSearchCriteria, OfferSearchCriteriaModel>();
                cfg.CreateMap<OfferModel, Offer>();
                //cfg.CreateMap<SupplierSpecificationModel, SupplierSpecificationAttachment>();
                cfg.CreateMap<SupplierSpecificationModel, SupplierSpecificationAttachment>()
               .ConstructUsing(tr => new SupplierSpecificationAttachment(tr.AttachmentId, tr.OfferId, tr.IsForApplier, tr.Degree, tr.ConstructionWorkId, tr.MaintenanceRunningWorkId));

                cfg.ValidateInlineMaps = false;
            });
            mapper = config.CreateMapper();
            _offerDefaults = new OfferDefaults();
            _offerController = new OfferController(offerAppService, tenderAppService, verificationCodeService, mapper, supplierService, rootConfigurationsMock.Object);
        }

        [Fact]
        public void GetOffer_Returns_NewObject()
        {
            var result = _offerController.GetOffer();

            Assert.NotNull(result);
            Assert.IsType<OfferModel>(result);
        }

        [Fact]
        public async Task GetOffers_Returns_Data()
        {
            var _offerSearchCriteriaModel = new OfferSearchCriteriaModel()
            {
                TenderId = 4,
            };

            var result = await _offerController.GetOffers(_offerSearchCriteriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<OfferModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetOfferDetailsById_Returns_Data()
        {
            var _tenderId = 3;
            var _offerId = 2;

            var result = await _offerController.GetOfferDetailsById(_tenderId, _offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferModel>(result);
            Assert.NotEmpty(result.Attachment);
            Assert.NotEmpty(result.CombinedSupplierModel);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.False(result.IsExclusionReasonEmpty);
            Assert.True(result.isOldFlow);
        }

        [Fact]
        public async Task GetOfferDetailsById_Returns_OfferData()
        {
            var _tenderId = 4;
            var _offerId = 1;

            var result = await _offerController.GetOfferDetailsById(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferCheckingDetailsModel>(result);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_offerId, result.OfferId);
        }

        [Fact]
        public async Task GetOfferById_Returns_OfferData()
        {
            var _tenderId = 4;
            var _offerId = 1;
            var _commericalRegisterNo = "10001908502";

            var result = await _offerController.GetOfferById(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferModel>(result);
            Assert.Equal(_offerId, result.OfferId);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_commericalRegisterNo, result.CommericalRegisterNo);
            Assert.NotEmpty(result.Attachment);
        }

        [Fact]
        public async Task GetAllSuppliersBySearchCretriaForCombined_Returns_Data()
        {
            var _tenderId = 4;
            var _commercialRegisterNo = "10001908502";
            var _supplierSearchCretriaModel = new SupplierSearchCretriaModel()
            {
                InvitationTenderId = _tenderId
            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetAllSuppliersBySearchCretriaForCombined(_supplierSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierCombinedModelView>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetCombinedSuppliersByOfferId_Returns_Data()
        {
            var _combinedSearchCretriaModel = new CombinedSearchCretriaModel()
            {
                OfferId = 1
            };

            var result = await _offerController.GetCombinedSuppliersByOfferId(_combinedSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<SupplierCombinedModelView>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public void AddSuppliertoCombinedList_Returns_Data()
        {
            var _combinedSupplierInsertModel = new CombinedSupplierInsertModel()
            {

            };

            var result = _offerController.AddSuppliertoCombinedList(_combinedSupplierInsertModel);

            Assert.Null(result);
        }

        [Fact]
        public async Task WithdrawOffer_Returns_Data()
        {
            var _offerId = 4;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.WithdrawOffer(_offerId);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("", result);
        }

        [Fact]
        public async Task GeOfferByTenderIdAndOfferIdForOpening_Returns_Data()
        {
            var _tenderId = 4;
            var _offerId = 1;
            var _commericalRegisterNo = "10001908502";

            var result = await _offerController.GeOfferByTenderIdAndOfferIdForOpening(_tenderId, _offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferModel>(result);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.Equal(_offerId, result.OfferId);
            Assert.Equal(_commericalRegisterNo, result.CommericalRegisterNo);
            Assert.NotEmpty(result.Attachment);
            Assert.NotEmpty(result.CombinedSupplierModel);
        }

        [Fact]
        public async Task GetTenderStatus_Returns_Data()
        {
            var _tenderId = 4;
            var _tenderStatusId = 6;

            var result = await _offerController.GetTenderStatus(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailModel>(result);
            Assert.Equal(_tenderStatusId, result.TenderStatusId);
            Assert.True(result.isOldFlow);
            Assert.True(result.IsSpecificationValidDate);
            Assert.True(result.IsSpecificationAttached);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsZakatAttached);
            Assert.True(result.IsVisitationAttached);
            Assert.True(result.IsSocialInsuranceValidDate);
            Assert.True(result.IsSocialInsuranceAttached);
            Assert.True(result.IsSaudizationValidDate);
            Assert.True(result.IsSaudizationAttached);
            Assert.True(result.IsPurchaseBillAttached);
            Assert.True(result.IsOfferLetterAttached);
            Assert.True(result.IsOfferCopyAttached);
            Assert.True(result.IsCommercialRegisterValid);
            Assert.False(result.CombinedOwner);
            Assert.False(result.IsFinaintialOfferLetterAttached);
            Assert.False(result.IsFinantialOfferLetterCopyAttached);
        }

        [Fact]
        public async Task GetOpenOfferData_Returns_Data()
        {
            var _offerId = 4;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOpenOfferData(_offerId);
            Assert.NotNull(result);
            Assert.IsType<OpenOfferModel>(result);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.NotEmpty(result.OfferAttachmentModels);
        }

        [Fact]
        public async Task OpenOffersDetailsForAwarding_Returns_Data()
        {
            var _offerId = 4;
            var _userId = "1";
            var _offerStatus = Enums.OfferStatus.Canceled;
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.OpenOffersDetailsForAwarding(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OpenOfferModel>(result);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_offerStatus, result.OfferStatus);
            Assert.NotEmpty(result.CombinedSupplierModel);
            Assert.NotEmpty(result.OfferAttachmentModels);
        }

        [Fact]
        public async Task OfferFinancialDetails_Returns_Data()
        {
            var _offerId = 1;

            var result = await _offerController.OfferFinancialDetails(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferFinancialDetailsModel>(result);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_offerId, result.OfferId);
            Assert.NotEmpty(result.Attachments);
        }

        [Fact]
        public async Task GetSuppliersReportByTenderId_ThrowsUnHandledAccessException()
        {
            var _tenderId = 4;
            var _branchId = "31";
            var _agencyCode = "022001000000";
            var _committedId = "1";
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.BranchId,_branchId),
                 new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.CommitteeId, _committedId)
            };
            _offerController = _offerController.WithIdentity(_claims);
            var _expectedMessage = Resources.SharedResources.ErrorMessages.YouHaveNoAccess;
            var result = await Assert.ThrowsAsync<UnHandledAccessException>(() => _offerController.GetSuppliersReportByTenderId(_tenderId));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetSuppliersReportByTenderId_Returns_Data()
        {
            var _tenderIdString = Util.Encrypt(4);
            var _pageNumber = 1;

            var result = await _offerController.GetSuppliersReportByTenderId__(_tenderIdString, _pageNumber);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<AppliedSuppliersReportDetailsModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.Equal(_pageNumber, result.PageNumber);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task ExportAppliedSuppliersReport_Returns_Data()
        {
            var _tenderIdString = Util.Encrypt(4);

            var result = await _offerController.ExportAppliedSuppliersReport(_tenderIdString);

            Assert.NotNull(result);
            Assert.IsType<List<AppliedSuppliersReportDetailsModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task AddOffer_Throws_UnHandledAccessException()
        {
            var _offerModel = new OfferModel()
            {
                TenderId = 13,
                CommericalRegisterNo = "1010000154",
            };

            var result = await Assert.ThrowsAsync<UnHandledAccessException>(() => _offerController.AddOffer(_offerModel));
            Assert.NotNull(result);
            Assert.Contains("UnHandledAccessException", result.Message);
        }

        [Fact]
        public async Task SendOffer_Returns_OK()
        {
            var _offerId = 6;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.SendOffer(_offerId);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task ConfirmReceivedOfferAsync_Returns_OK()
        {
            var _offerId = 6;

            var result = await _offerController.ConfirmReceivedOfferAsync(_offerId);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task AddOfferAttachmentsDetails_Returns_BadRequest()
        {

            var _offerAttachmentsModel = new OfferAttachmentsModel();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.AddOfferAttachmentsDetails(_offerAttachmentsModel);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddOfferAttachmentsDetails_Returns_OK()
        {
            var _offerAttachmentsModel = _offerDefaults.GetOfferAttachmentsModelDefaults();
            _offerAttachmentsModel.BankGuaranteeFiles = new List<SupplierBankGuaranteeModel>();
            _offerAttachmentsModel.SpecificationsFiles = new List<SupplierSpecificationModel>();
            _offerAttachmentsModel.CombinedId = 7;
            _offerAttachmentsModel.CombinedIdString = Util.Encrypt(7);
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.AddOfferAttachmentsDetails(_offerAttachmentsModel);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task AddOfferDetails_Returns_BadRequest()
        {

            var _offerDetailModel = new OfferDetailModel();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.AddOfferDetails(_offerDetailModel);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddOfferDetails_Returns_OK()
        {

            var _offerDetailModel = _offerDefaults.GetOfferDetailModelDefaults();
            _offerDetailModel.BankGuaranteeFiles = new List<SupplierBankGuaranteeModel>();
            _offerDetailModel.SpecificationsFiles = new List<SupplierSpecificationModel>();
            var result = await _offerController.AddOfferDetails(_offerDetailModel);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task SaveDiscountNotes_Returns_OK()
        {

            var _discountNotesModel = _offerDefaults.GetDiscountNotesModelDefaults();

            var result = await _offerController.SaveDiscountNotes(_discountNotesModel);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task AddClassification_Returns_BadRequest()
        {

            var _supplierSpecificationModel = _offerDefaults.GetSupplierSpecificationModelDefaults();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.AddClassification(_supplierSpecificationModel);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddClassification_Returns_Ok()
        {

            var _supplierSpecificationModel = _offerDefaults.GetSupplierSpecificationModelDefaults();

            _supplierSpecificationModel.ConstructionWork = new { NameAr = "FOR TEST ONLY" };
            var result = await _offerController.AddClassification(_supplierSpecificationModel);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetOffersForAwardingByTenderId_Returns_Data()
        {
            var _tenderId = 13;
            var _crSuppliers = "1010000154";

            var result = await _offerController.GetOffersForAwardingByTenderId(_tenderId, _crSuppliers);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<OfferFinancialDetailsModel>>(result);
            Assert.Empty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task OffersOpeningReport_Returns_Data()
        {
            var _tenderId = 6;

            var result = await _offerController.OffersOpeningReport(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<OfferFinancialDetailsModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetOfferStatusForOfferSummary_Returns_Data()
        {
            var _offerId = 9;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferStatusForOfferSummary(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferSummaryStatusModel>(result);
            Assert.Equal(_commercialRegisterNo, result.CommercialNumber);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferStringId);
            Assert.Equal((int)Enums.OfferStatus.UnderEstablishing, result.StatusId);
        }

        [Fact]
        public async Task GetOfferStatusForOfferSummary_Returns_NotAddedAuthorized()
        {
            var _expectedMessage = Resources.SharedResources.ErrorMessages.NotAddedAuthorized;
            var _offerId = 0;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetOfferStatusForOfferSummary(_offerId));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetOfferStatusForOfferSummary_Returns_ReceivedStatus()
        {
            var _expectedMessage = Resources.SharedResources.ErrorMessages.NotAddedAuthorized;
            var _offerId = 1;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetOfferStatusForOfferSummary(_offerId));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetOfferStatusForOfferSummary_Returns_CanceledStatus()
        {
            var _expectedMessage = "تم سحب العرض";
            var _offerId = 4;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetOfferStatusForOfferSummary(_offerId));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetAwardedOffersByTenderId_Returns_Data()
        {
            var _tenderId = 3;

            var result = await _offerController.GetAwardedOffersByTenderId(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<OfferFinancialDetailsModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetOfferUploadedAttachmentsDetails_Returns_Data()
        {
            var _offerId = 2;
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferUploadedAttachmentsDetails(_offerId, _combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferAttachmentsModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(_offerId, result.OfferId);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task GetOfferDetails_Returns_Data()
        {
            var _offerId = 2;
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetails(_combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(_offerId, result.OfferId);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task GetOfferDetailsDisplayOnly_Returns_Data()
        {
            var _offerId = 2;
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsDisplayOnly(_combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(_offerId, result.OfferId);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task GetOfferAttachmentsDetails_Returns_Data()
        {
            var _offerId = 1;

            var result = await _offerController.GetOfferAttachmentsDetails(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferCheckingAttachmentsDetailsModel>(result);
            Assert.NotEmpty(result.SpecificationsFiles);
        }

        [Fact]
        public async Task OfferDeatilsReportForTender_Returns_Data()
        {
            var _tenderId = 8;

            var result = await _offerController.OfferDeatilsReportForTender(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<List<OfferDeatilsReportForTenderModel>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync_Returns_Data()
        {
            var _offerId = 4;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(_offerId);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<CombinedSupplierModel>>(result);
            Assert.Empty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetTenderOfferIDsByOfferID_Returns_Null()
        {
            var _offerId = 1;

            var result = await _offerController.GetTenderOfferIDsByOfferID(_offerId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetOfferTableQuantityItems_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 8,
                TenderId = 103,
                FormId = 7,
                QuantityTableId = 17
            };

            var result = await _offerController.GetOfferTableQuantityItems(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetOfferDetailsForCombinedChecking_Returns_Data()
        {
            var _offerId = 2;
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsForCombinedChecking(_combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailsForCheckingModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task AddTwoFilesFinancialDetails_Returns_BadRequest()
        {

            var _tenderTowFilesFinancialDetailsDetails = new TenderTowFilesFinancialDetailsDetails();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.AddTwoFilesFinancialDetails(_tenderTowFilesFinancialDetailsDetails);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddTwoFilesFinancialDetails_Returns_OK()
        {
            var _tenderTowFilesFinancialDetailsDetails = new TenderTowFilesFinancialDetailsDetails()
            { OfferIdString = Util.Encrypt(129) };

            var result = await _offerController.AddTwoFilesFinancialDetails(_tenderTowFilesFinancialDetailsDetails);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task AddTwoFilesFinancialCheck_Returns_BadRequest()
        {

            var _offerDetailsForCheckingModel = new OfferDetailsForCheckingModel();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.AddTwoFilesFinancialCheck(_offerDetailsForCheckingModel);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddTwoFilesFinancialCheck_Returns_OK()
        {
            var _offerDetailsForCheckingModel = new OfferDetailsForCheckingModel()
            { OfferIdString = Util.Encrypt(129) };

            var result = await _offerController.AddTwoFilesFinancialCheck(_offerDetailsForCheckingModel);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task AddTechnicalOfferResultForTwoFilesTender_Returns_BadRequest()
        {

            var _offerDetailsForCheckingTwoFilesModel = new OfferDetailsForCheckingTwoFilesModel();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.AddTechnicalOfferResultForTwoFilesTender(_offerDetailsForCheckingTwoFilesModel);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddTechnicalOfferResultForTwoFilesTender_Returns_OK()
        {
            var _offerDetailsForCheckingTwoFilesModel = new OfferDetailsForCheckingTwoFilesModel()
            { OfferIdString = Util.Encrypt(116) };

            var result = await _offerController.AddTechnicalOfferResultForTwoFilesTender(_offerDetailsForCheckingTwoFilesModel);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task ValidateandSaveCheckingQuantitiesTable_Returns_Null()
        {
            var _encryptedOfferId = Util.Encrypt(2);
            var _formId = "7";
            var _tableId = "2";

            var _authorList = new Dictionary<string, string>() {
                { "EncryptedOfferId", _encryptedOfferId },
                { "FormId", _formId },
                { "TableId",_tableId}
            };
            var result = await _offerController.ValidateandSaveCheckingQuantitiesTable(_authorList);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task OfferCheckingDetailsForAwarding_Returns_Data()
        {
            var _tenderId = 3;
            var _offerId = 2;

            var result = await _offerController.OfferCheckingDetailsForAwarding(_tenderId, _offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferModel>(result);
            Assert.Equal(_offerId, result.OfferId);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.True(result.IsExclusionReasonEmpty);
            Assert.False(result.isOldFlow);
        }

        [Fact]
        public async Task GetBayanTableAsync_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 8,
                TenderId = 103,
                FormId = 7,
                QuantityTableId = 17
            };

            var result = await _offerController.GetBayanTableAsync(_quantityTableSearchCretriaModel);

            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }
        [Fact]
        public async Task GetBayanTableReadOnlyAsync_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                TenderId = 3,
                FormId = 7,
                QuantityTableId = 2
            };

            var result = await _offerController.GetBayanTableReadOnlyAsync(_quantityTableSearchCretriaModel);

            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task ValidateBayanQuantitiesTables_Returns_Null()
        {
            var _offerId = "2";
            var _formId = "7";
            var _tableId = "2";
            var _tenderId = "72";
            var _intialTableId = "22857";
            var _tableName = "Table Name";
            var _itemNumber = "1";
            var _authorList = new Dictionary<string, string>() {
                { "OfferId", _offerId },
                { "FormId",_formId},
                { "TableId",_tableId},
                { "TenderId",_tenderId},
                { "IntialTableId",_intialTableId},
                {"TableName",_tableName },
                { "ItemNumber",_itemNumber}
            };
            var result = await _offerController.ValidateBayanQuantitiesTables(_authorList);

            Assert.NotNull(result);
            Assert.IsType<ValidationReturndTemplate>(result);
            Assert.Equal(_tableId, result.tableId);
        }

        [Fact]
        public async Task GetCoastsTablForDirectPurchaseAsync_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                TenderId = 3,
                FormId = 7,
                QuantityTableId = 2
            };

            var result = await _offerController.GetCoastsTablForDirectPurchaseAsync(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<TableModel>(result);
            Assert.Equal(_quantityTableSearchCretriaModel.OfferId, result.OfferId);
        }

        [Fact]
        public async Task GetCoastsTablForApplyOfferAsync_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                TenderId = 3,
                FormId = 7,
                QuantityTableId = 2
            };

            var result = await _offerController.GetCoastsTablForApplyOfferAsync(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<TableModel>(result);
            Assert.Equal(_quantityTableSearchCretriaModel.OfferId, result.OfferId);
        }

        [Fact]
        public async Task GetCoastsTablForOpenDetails_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                TenderId = 3,
                FormId = 7,
                QuantityTableId = 2
            };

            var result = await _offerController.GetCoastsTablForOpenDetails(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<TableModel>(result);
            Assert.Equal(_quantityTableSearchCretriaModel.OfferId, result.OfferId);
        }

        [Fact]
        public async Task GetOfferTableQuantityItemsForDirectPurchase_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                TenderId = 3,
                FormId = 7,
                QuantityTableId = 2
            };

            var result = await _offerController.GetOfferTableQuantityItemsForDirectPurchase(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetApplyOfferTableQuantityItems_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 8,
                TenderId = 103,
                FormId = 31,
                QuantityTableId = 17
            };

            var result = await _offerController.GetApplyOfferTableQuantityItems(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetTableQuantityItemsOpenDetails_Returns_Data()
        {
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                TenderId = 3,
                FormId = 7,
                QuantityTableId = 2
            };

            var result = await _offerController.GetTableQuantityItemsOpenDetails(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GeOfferByTenderIdAndOfferIdForDirectPurchase_Returns_Data()
        {
            var _tenderId = 103;
            var _offerId = 50;

            var result = await _offerController.GeOfferByTenderIdAndOfferIdForDirectPurchase(_tenderId, _offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferModel>(result);
            Assert.NotEmpty(result.Attachment);
            Assert.NotEmpty(result.CombinedSupplierModel);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.False(result.IsExclusionReasonEmpty);
            Assert.True(result.isOldFlow);
        }

        [Fact]
        public async Task GetOfferQuantityTableForAwarding_Returns_Data()
        {
            var _tenderId = 3;
            var _offerId = 2;

            var result = await _offerController.GetOfferQuantityTableForAwarding(_tenderId, _offerId);

            Assert.NotNull(result);
            Assert.IsType<QuantitiesTablesForAwardingModel>(result);
            Assert.NotEmpty(result.Attachment);
            Assert.NotEmpty(result.CombinedSupplierModel);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_tenderId, result.TenderId);
            Assert.False(result.IsExclusionReasonEmpty);
        }

        [Fact]
        public async Task GetOfferDetailsDirectPurchase_Returns_Data()
        {
            var _combinedId = 2;
            var _tenderStatusId = 60;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsDirectPurchase(_combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(_tenderStatusId, result.TenderStatusId);
        }

        [Fact]
        public async Task GetOfferDetailsForDirectPurchaseTenderChecking_Returns_Data()
        {
            var _offerId = 2;
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsForDirectPurchaseTenderChecking(_combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailsForCheckingModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task GetOfferDetailsForDirectPurchaseTenderFinancialChecking_Returns_Data()
        {
            var _offerId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsForDirectPurchaseTenderFinancialChecking(_offerId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailsForCheckingModel>(result);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.NotEmpty(result.BankGuaranteeFiles);
            Assert.False(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial_Returns_BadRequest()
        {

            var _offerCheckingContainer = new OfferCheckingContainer();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial(_offerCheckingContainer);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial_Returns_OK()
        {
            var _offerCheckingContainer = new OfferCheckingContainer()
            {
                model = new OfferDetailModel() { OfferIdString = Util.Encrypt(129), CombinedIdString = Util.Encrypt(2) },
                checkingResult = new CheckOfferResultModel() { TechniciansReportAttachmentsRef = "" }
            };

            var result = await _offerController.SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial(_offerCheckingContainer);
            var okResult = result as OkResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetOfferDetailsForDirectPurchase_Returns_Data()
        {
            var _offerId = 2;
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsForDirectPurchase(_combinedId);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailsForCheckingModel>(result);
            Assert.Equal(_combinedId, result.CombinedId);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.True(result.IsBankGuaranteeAttached);
            Assert.True(result.IsChamberJoiningAttached);
            Assert.True(result.IsPurchaseBillAttached);
        }

        [Fact]
        public async Task DeleteAlternativeItem_Returns_Data()
        {
            var _tableId = 2;
            var _itemNumber = 2;

            var result = await _offerController.DeleteAlternativeItem(_tableId, _itemNumber);

            Assert.NotNull(result);
            Assert.IsType<AlternativeItemResponseModel>(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetVROOfferDetailsById_Returns_Data()
        {
            var _offerIdString = Util.Encrypt(16);
            var _vROOffersTechnicalCheckingViewModel = new VROOffersTechnicalCheckingViewModel()
            {
                IsOpened = "No",
                Notes = "New notes",
                OfferAcceptanceStatusId = 1,
                OfferIdString = "PyteQrXhrrrHDl4mBYqtGg==",
                OfferTechnicalEvaluationStatusId = 1,
                TechnicalEvaluationLevel = 10,
                TenderIdString = "6u2U7s0zjWoRHSbJC2zeLw==",
                TenderStatusId = 59,
                TenderTypeId = 13
            };

            var result = await _offerController.GetVROOfferDetailsById(_offerIdString);

            Assert.NotNull(result);
            Assert.IsType<VROOffersTechnicalCheckingViewModel>(result);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.IsOpened, result.IsOpened);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.Notes, result.Notes);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.OfferAcceptanceStatusId, result.OfferAcceptanceStatusId);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.OfferIdString, result.OfferIdString);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.OfferTechnicalEvaluationStatusId, result.OfferTechnicalEvaluationStatusId);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.TechnicalEvaluationLevel, result.TechnicalEvaluationLevel);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.TenderIdString, result.TenderIdString);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.TenderStatusId, result.TenderStatusId);
            Assert.Equal(_vROOffersTechnicalCheckingViewModel.TenderTypeId, result.TenderTypeId);
        }

        [Fact]
        public async Task GetOfferTableQuantityItemsVRO_Returns_Data()
        {
            var _offerIdString = Util.Encrypt(16);
            var _quantityTableSearchCretriaModel = new QuantityTableSearchCretriaModel()
            {
                OfferId = 2,
                QuantityTableId = 2
            };

            var result = await _offerController.GetOfferTableQuantityItemsVRO(_quantityTableSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TableModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task VROOffersTechnicalChecking_Returns_OK()
        {
            var _vROOffersTechnicalCheckingViewModel = new VROOffersTechnicalCheckingViewModel()
            {
                TenderTypeId = 6,
                OfferAcceptanceStatusId = 1,
                OfferTechnicalEvaluationStatusId = 1,
                Notes = "",
                OfferIdString = Util.Encrypt(19)
            };

            var result = await _offerController.VROOffersTechnicalChecking(_vROOffersTechnicalCheckingViewModel);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetOfferFinancialCheckingDetailsById_Returns_Data()
        {
            var _tenderId = 3;
            var _offerId = 2;

            var result = await _offerController.GetOfferFinancialCheckingDetailsById(_tenderId, _offerId);

            Assert.NotNull(result);
            Assert.IsType<VROFinantialCheckingModel>(result);
            Assert.NotEmpty(result.Attachment);
            Assert.NotEmpty(result.CombinedSupplierModel);
            Assert.Equal(Util.Encrypt(_offerId), result.OfferIdString);
            Assert.Equal(_tenderId, result.TenderId);
        }

        [Fact]
        public async Task SaveCombinedTechnicalDetailsVRO_Returns_BadRequest()
        {

            var _offerDetailModel = new OfferDetailModel();
            _offerController.ModelState.AddModelError("Error", "Invalid Model");

            var result = await _offerController.SaveCombinedTechnicalDetailsVRO(_offerDetailModel);
            var badRequestResult = result as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task SaveCombinedTechnicalDetailsVRO_Returns_OK()
        {
            var _offerDetailModel = new OfferDetailModel()
            {
                OfferIdString = Util.Encrypt(34),
                CombinedIdString = Util.Encrypt(2)
            };

            var result = await _offerController.SaveCombinedTechnicalDetailsVRO(_offerDetailModel);
            var okResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetOfferFullDetails_Returns_Data()
        {
            var _OfferIdString = Util.Encrypt(2);

            var result = await _offerController.GetOfferFullDetails(_OfferIdString);

            Assert.NotNull(result);
            Assert.IsType<OfferFullDetailsModel>(result);
            Assert.NotEmpty(result.CombinedSuppliers);
            Assert.NotEmpty(result.attachments);
        }

        [Fact]
        public async Task GetOfferDetailsByCombinedId_Returns_Data()
        {
            var _expectedResult = new OfferDetailsForAcceptingSolidarityInviationsModel()
            {
                InvitaionStatusId = 4,
                InvitaionStatusName = "بانتظار الرد",
                OfferIdString = "ifjK8HP3U0T1w9GYK6pVew==",
                OfferOpenDate = DateTime.Parse("2/12/2020 12:00:00 AM"),
                OfferStatusId = 4,
                SolidarityIdString = "ifjK8HP3U0T1w9GYK6pVew==",
                TenderTypeId = 2,
                tenderIdString = "0J4PeaOHdn0*@@**2z1YClk0Ng=="
            };
            var _combinedId = 2;
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier, _userId)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOfferDetailsByCombinedId(_combinedId);
            Assert.NotNull(result);
            Assert.IsType<OfferDetailsForAcceptingSolidarityInviationsModel>(result);
            Assert.NotEmpty(result.OfferAttachmentModels);
            Assert.Equal(_expectedResult.InvitaionStatusId, result.InvitaionStatusId);
            Assert.Equal(_expectedResult.InvitaionStatusName, result.InvitaionStatusName);
            Assert.Equal(_expectedResult.OfferIdString, result.OfferIdString);
            Assert.Equal(_expectedResult.OfferOpenDate, result.OfferOpenDate);
            Assert.Equal(_expectedResult.OfferStatusId, result.OfferStatusId);
            Assert.Equal(_expectedResult.SolidarityIdString, result.SolidarityIdString);
            Assert.Equal(_expectedResult.TenderTypeId, result.TenderTypeId);
            Assert.Equal(_expectedResult.tenderIdString, result.tenderIdString);
        }

        [Fact]
        public async Task GetAllCombinedInvitationForSupplier_Returns_OfferData()
        {
            var _commercialRegisterNo = "142154293000206";
            var _combinedSearchCriteria = new CombinedSearchCriteria()
            {
                CommericalRegisterNo = _commercialRegisterNo
            };
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetAllCombinedInvitationForSupplier(_combinedSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<CombinedListModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.PageSize > 0);
        }

        [Fact]
        public async Task GetCombinedInvitationDetails_Throws_BusinessRuleException()
        {
            var _expectedMessage = "تم سحب دعوة التضامن ";
            var _offerId = 2;
            var _commercialRegisterNo = "10001908502";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo)
            };
            _offerController = _offerController.WithIdentity(_claims);

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetCombinedInvitationDetails(_offerId));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetQuantityTables_Returns_Data()
        {
            var _offerId = 2;
            var _expectedResult = new OfferQuantityTableHtmlVM()
            {
                offerStatusModel = new OfferStatusModel()
                {
                    OfferStatusId = 4,
                    offerIdString = "ifjK8HP3U0T1w9GYK6pVew==",
                    offerStatusName = "مستلم"
                }
            };

            var result = await _offerController.GetQuantityTables(_offerId);

            Assert.NotNull(result);
            Assert.NotNull(result.offerStatusModel);
            Assert.IsType<OfferQuantityTableHtmlVM>(result);
            Assert.Equal(_expectedResult.offerStatusModel.OfferStatusId, result.offerStatusModel.OfferStatusId);
            Assert.Equal(_expectedResult.offerStatusModel.offerIdString, result.offerStatusModel.offerIdString);
            Assert.Equal(_expectedResult.offerStatusModel.offerStatusName, result.offerStatusModel.offerStatusName);
        }

        [Fact]
        public async Task GetOpeningQuantityTables_Returns_Data()
        {
            var _offerId = 2;
            var _expectedResult = new OfferQuantityTableHtmlVM()
            {
                OfferPresentationWayId = 2,
                TenderStatusId = 60,
                TenderTypeId = 2,
                offerId = 2
            };

            var result = await _offerController.GetOpeningQuantityTables(_offerId, false);

            Assert.NotNull(result);
            Assert.IsType<OfferQuantityTableHtmlVM>(result);
            Assert.Equal(_expectedResult.OfferPresentationWayId, result.OfferPresentationWayId);
            Assert.Equal(_expectedResult.TenderTypeId, result.TenderTypeId);
            Assert.Equal(_expectedResult.offerId, result.offerId);
        }

        [Fact]
        public async Task GetOpeningQuantityTablesTest_Returns_Data()
        {
            var _offerId = 2;
            var _tenderId = 3;
            var _referenceNumber = "410108004956402000001";
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetOpeningQuantityTablesTest(_offerId, false);

            Assert.NotNull(result);
            Assert.IsType<QuantitiesTemplateModel>(result);
            Assert.Equal(_tenderId, result.TenderID);
            Assert.Equal(_referenceNumber, result.ReferenceNumber);
            Assert.NotEmpty(result.FormIds);
        }

        [Fact]
        public async Task GetQuantityTablesDisplay_Returns_Data()
        {
            var _offerId = 2;
            _offerController = _offerController.WithIdentity(_claims);

            var result = await _offerController.GetQuantityTablesDisplay(_offerId, false, false);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetEmptyForm_Returns_Data()
        {
            var _offerId = 2;
            var _tenderId = 3;
            var _expectedString = "ifjK8HP3U0T1w9GYK6pVew==";

            var result = await _offerController.GetEmptyForm(_offerId, _tenderId);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Contains(_expectedString, result);
        }

        [Fact]
        public async Task GetOfferQuantityTableModelAsync_Throws_BusinessRuleException()
        {
            var _offerId = 2;
            var _expectedMessage = "غير مسموح عرض بيانات العرض";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetOfferQuantityTableModelAsync(_offerId));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetApplyOfferQuantityTableModelAsync_Throws_BusinessRuleException()
        {
            var _offerId = 2;
            var _expectedMessage = "غير مسموح عرض بيانات العرض";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetApplyOfferQuantityTableModelAsync(_offerId));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetQuantityTablesReadOnly_Returns_Data()
        {
            var _offerId = 3;
            var _expectedResult = new OfferQuantityTableHtmlVM()
            {
                offerStatusModel = new OfferStatusModel()
                {
                    OfferStatusId = 4,
                    offerIdString = "0J4PeaOHdn0*@@**2z1YClk0Ng==",
                    offerStatusName = "مستلم"
                }
            };

            var result = await _offerController.GetQuantityTablesReadOnly(_offerId);

            Assert.NotNull(result);
            Assert.NotNull(result.offerStatusModel);
            Assert.NotEmpty(result.tableFormHtmls);
            Assert.IsType<OfferQuantityTableHtmlVM>(result);
            Assert.Equal(_expectedResult.offerStatusModel.OfferStatusId, result.offerStatusModel.OfferStatusId);
            Assert.Equal(_expectedResult.offerStatusModel.offerIdString, result.offerStatusModel.offerIdString);
            Assert.Equal(_expectedResult.offerStatusModel.offerStatusName, result.offerStatusModel.offerStatusName);
        }

        [Fact]
        public async Task OfferFilesAsync_Throws_BusinessRuleException()
        {
            var _offerId = 2;
            var _expectedMessage = "غير مسموح عرض بيانات العرض";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.OfferFilesAsync(_offerId));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetOfferMain_Throws_BusinessRuleException()
        {
            var _offerId = 2;
            var _tenderId = 3;
            var _expectedMessage = "المنافسة ليست فى المرحلة الصحيحة لتقديم العرض";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _offerController.GetOfferMain(_offerId, _tenderId));

            Assert.NotNull(result);
            Assert.Contains(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task GetFinancialOfferDetailsForDisplay_Returns_Data()
        {
            var _offerIdString = Util.Encrypt(3);
            var _expectedResult = new OfferDetailsDisplayModel()
            {
                CombinedIdString = "DOcyLqg3acDr0mcd6 nGEA==",
                Discount = "10",
                DiscountNotes = "25",
                OfferIdString = "0J4PeaOHdn0*@@**2z1YClk0Ng==",
                OfferPresentationWayId = 1,
                TenderIdString = "ifjK8HP3U0T1w9GYK6pVew==",
                TenderTypeId = 2
            };

            var result = await _offerController.GetFinancialOfferDetailsForDisplay(_offerIdString);

            Assert.NotNull(result);
            Assert.IsType<OfferDetailsDisplayModel>(result);
            Assert.Equal(_expectedResult.CombinedIdString, result.CombinedIdString);
            Assert.Equal(_expectedResult.Discount, result.Discount);
            Assert.Equal(_expectedResult.DiscountNotes, result.DiscountNotes);
            Assert.Equal(_expectedResult.OfferIdString, result.OfferIdString);
            Assert.Equal(_expectedResult.OfferPresentationWayId, result.OfferPresentationWayId);
            Assert.Equal(_expectedResult.TenderIdString, result.TenderIdString);
            Assert.Equal(_expectedResult.TenderTypeId, result.TenderTypeId);

        }
    }
}
