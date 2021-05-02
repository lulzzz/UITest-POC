using Microsoft.AspNetCore.Http;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.AnnouncementTemplateDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services
{
    public class AnnouncementTemplateAppService
    {
        private readonly Mock<IAnnouncementSupplierTemplateCommands> _moqAnnouncementCommands;
        private readonly Mock<IAnnouncementSupplierTemplateQueries> _moqAnnouncementQueries;
        private readonly Mock<IAnnouncementSupplierTemplateDomainService> _moqAnnouncementDomainService;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<INotificationAppService> _moqNotificationAppService;
        private readonly Mock<IIDMQueries> _moqIDMQueries;
        private readonly Mock<IIDMProxy> _moqIDMProxy;
        private readonly Mock<IBlockAppService> _moqBlockAppService;

        public readonly AnnouncementSupplierTemplateAppService _sut;
        public AnnouncementTemplateAppService()
        {
            _moqAnnouncementQueries = new Mock<IAnnouncementSupplierTemplateQueries>();
            _moqAnnouncementCommands = new Mock<IAnnouncementSupplierTemplateCommands>();
            _moqAnnouncementDomainService = new Mock<IAnnouncementSupplierTemplateDomainService>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqNotificationAppService = new Mock<INotificationAppService>();
            _moqIDMQueries = new Mock<IIDMQueries>();
            _moqIDMProxy = new Mock<IIDMProxy>();
            _moqBlockAppService = new Mock<IBlockAppService>();

            _sut = new AnnouncementSupplierTemplateAppService(
                _moqAnnouncementCommands.Object,
                _moqAnnouncementQueries.Object,
                _moqHttpContextAccessor.Object,
                _moqAnnouncementDomainService.Object,
                _moqNotificationAppService.Object,
                _moqIDMQueries.Object,
                _moqBlockAppService.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async Task ShouldGetAllAnnouncementTamplatesForSupplier(int announcementPublishedDateDaysId)
        {
            var searchCriteria = new AnnouncementSupplierTemplateSupplierSearchCriteriaModel() { AnnouncementPublishedDateDaysId= announcementPublishedDateDaysId };

            await _sut.GetAllAnnouncementSupplierTemplatesForSupplier(searchCriteria);

            _moqAnnouncementQueries.Verify(s => s.GetAllAnnouncementSupplierTemplatesForSupplier(It.IsAny<AnnouncementSupplierTemplateSupplierSearchCriteriaModel>()), Times.Once);
        }


        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            var result = await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldSaveDraftAnnouncementSupplierTemplateSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            var result = await _sut.SaveDraft(createAnnouncementSupplierTemplateModel);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(4)]
        public async Task ShouldDeleteAnnouncementSupplierTemplateSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults());
                     });
            await _sut.DeleteAnnouncement(announcementId);
            _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncementSupplierTemplate(It.IsAny<AnnouncementSupplierTemplate>()), Times.Once);
        }


        [Fact]
        public async Task ShouldThrowException_WhenNotInsertedGovAgencyData()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModelWithoutGovAgencyData();
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldThrowException_WhenNotInsertedAnnouncementName()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.AnnouncementSupplierTemplateName = "";
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldUpdateNewAnnouncementSupplierTemplateSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelData();

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForCreationStep(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults());
                   });


            var result = await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldUpdateApprovedAnnouncementSupplierTemplateListSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            UpdateAnnouncementSupplierTemplateModel updateAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelDataForUpdateList();

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForCreationStep(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaultsForUpdateApprovedList());
                   });
            var result = await _sut.UpdateAnnouncementSupplierTemplateList(updateAnnouncementSupplierTemplateModel);
            Assert.NotNull(result);
        }



        [Fact]
        public async Task ShouldUpdateApprovedAnnouncementSupplierTemplateListThrowExeptionWhenAgencyDataIsEmpty()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            UpdateAnnouncementSupplierTemplateModel updateAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelDataForUpdateList();

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForCreationStep(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaultsForUpdateApprovedList());
                   });

            updateAnnouncementSupplierTemplateModel.AgencyAddress = "";
            updateAnnouncementSupplierTemplateModel.AgencyFax = "";

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateAnnouncementSupplierTemplateList(updateAnnouncementSupplierTemplateModel));
        }



        [Fact]
        public async Task ShouldUpdateApprovedAnnouncementSupplierTemplateListThrowExeptionWhenEffectiveDateNotInserted()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            UpdateAnnouncementSupplierTemplateModel updateAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelDataForUpdateList();

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForCreationStep(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaultsForUpdateApprovedList());
                   });

            updateAnnouncementSupplierTemplateModel.EffectiveListDate = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateAnnouncementSupplierTemplateList(updateAnnouncementSupplierTemplateModel));
        }

        [Theory]
        [InlineData(100)]
        public async Task ShouldApproveAnnouncementSupplierTemplateSuccess(int announcementId)
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
         
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            _moqAnnouncementDomainService.Setup(x => x.GetUserFullName())
                 .Returns(() =>
                 {
                     return "fullname";
                 });
            _moqAnnouncementDomainService.Setup(x => x.GetUserId())
                   .Returns(() =>
                   {
                       return 1;
                   });

            VerificationModel verificationModel = new VerificationModel() { IdString = "", VerificationCode = "", Id = announcementId };
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults());
                     });
            await _sut.ApproveAnnouncement(verificationModel);
            _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncementSupplierTemplate(It.IsAny<AnnouncementSupplierTemplate>()), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public async Task ShouldAnnouncementSuppliersTemplateCancelSuccess(int announcementId)
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            AnnouncementSuppliersTemplateCancelModel cancelModel = new AnnouncementSuppliersTemplateCancelModel() { AnnouncementId = announcementId, CancelationReason = "reason", AnnouncementIdString = "69N3QwUl%208o0yRoVEsRsYA==" };
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults());
                     });
            await _sut.CancelAnnouncement(cancelModel);
            _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncementSupplierTemplate(It.IsAny<AnnouncementSupplierTemplate>()), Times.Once);
        }

        [Theory]
        [InlineData(100)]
        public async Task ShouldGetAnnouncementByIdForEditSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForEdit(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<CreateAnnouncementSupplierTemplateModel>(new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelData());
                   });
            var result = await _sut.GetAnnouncementByIdForEdit(announcementId);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(100)]
        public async Task ShouldGettAnnouncementTemplateDetailsSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.FindAnnouncementDetailsByAnnouncementId(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSuppliersTemplateDetailsModel>(new AnnouncementTemplateDefaults().GetAnnouncementSupplierTemplateDetailsModel());
                   });
            var result = await _sut.GettAnnouncementTemplateDetails(announcementId);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(100)]
        public async Task ShouldGetAnnouncementDescriptionDetailsByAnnouncementIdSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.FindAnnouncementDescriptionDetailsByAnnouncementId(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementDescriptionModel>(new AnnouncementTemplateDefaults().GetAnnouncementDescriptionModel());
                   });
            var result = await _sut.GetAnnouncementDescriptionDetailsByAnnouncementId(announcementId);
            Assert.NotNull(result);
        }



        [Theory]
        [InlineData(100)]
        public async Task ShouldGetAnnouncementBasicDetailsByAnnouncementIdSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.FindAnnouncementBasicDetailsByAnnouncementId(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementBasicDetailModel>(new AnnouncementTemplateDefaults().GetAnnouncementBasicDetailModelData());
                   });
            var result = await _sut.GetAnnouncementBasicDetailsByAnnouncementId(announcementId);
            Assert.NotNull(result);
        }


        [Theory]
        [InlineData(100)]
        public async Task ShouldFindAnnouncementDetailsByAnnouncementIdForCancelationSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.FindAnnouncementDetailsByAnnouncementIdForCancelation(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSuppliersTemplateCancelModel>(new AnnouncementTemplateDefaults().GetAnnouncementSuppliersTemplateCancelModelData());
                   });
            var result = await _sut.FindAnnouncementDetailsByAnnouncementIdForCancelation(announcementId);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(100)]
        public async Task ShouldGetAnnouncementTemplateListDetailsByAnnouncementIdSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.FindAnnouncementTemplateListDetailsByAnnouncementId(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementTemplateListDetailsModel>(new AnnouncementTemplateDefaults().GetAnnouncementTemplateListDetailsModelData());
                   });
            var result = await _sut.GetAnnouncementTemplateListDetailsByAnnouncementId(announcementId);
            Assert.NotNull(result);
        }


        [Fact]
        public async Task ShouldExtendAnnouncementSupplierTemplateSuccess()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForCreationStep(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaults());
                   });
            _moqAnnouncementQueries.Setup(x => x.GetAcceptedAnnouncementSuppliers(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<List<string>>(new List<string>() { "1010000154" });
                   });


            var result = await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldThrowException_WhenNotInsertedTemplateExtendMechanism()
        {
            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.TemplateExtendMechanism = "";
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Theory]
        [InlineData(100)]
        public async Task ShouldGetAnnouncementByIdForExtendSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementByIdForExtendAnnouncement(It.IsAny<int>()))
                   .Returns(() =>
                   {
                       return Task.FromResult<ExtendAnnouncementSupplierTemplateModel>(new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData());
                   });
            var result = await _sut.GetAnnouncementByIdForExtend(announcementId);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldGetJoinRequestsSuppliersByAnnouncementIdAsyncSuccess()
        {
            var searchCriteria = new JoinRequestSuppliersSearchCriteria();
            await _sut.GetJoinRequestsSuppliersByAnnouncementIdAsync(searchCriteria);
            _moqAnnouncementQueries.Verify(s => s.GetJoinRequestsSuppliersByAnnouncementIdAsync(It.IsAny<JoinRequestSuppliersSearchCriteria>()), Times.Once);
        }
        [Fact]
        public async Task Should_GetJoinRequestsByAnnouncementId()
        {
            _moqAnnouncementQueries.Setup(x => x.GetJoinRequestsByAnnouncementIdAsync(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()))
                           .Returns(() =>
                           {
                               return Task.FromResult(new QueryResult<JoinRequestModel>(new List<JoinRequestModel> { new JoinRequestModel() }, 1, 1, 1));
                           });

            var searchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria();
            var result = await _sut.GetJoinRequestsByAnnouncementIdAsync(searchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<JoinRequestModel>>(result);
            _moqAnnouncementQueries.Verify(s => s.GetJoinRequestsByAnnouncementIdAsync(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetApprovedSuppliersJoinRequestsByAnnouncementId()
        {
            _moqAnnouncementQueries.Setup(x => x.GetApprovedSuppliersJoinRequestsByAnnouncementId(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()))
                           .Returns(() =>
                           {
                               return Task.FromResult(new QueryResult<JoinRequestModel>(new List<JoinRequestModel> { new JoinRequestModel() }, 1, 1, 1));
                           });

            var searchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria();
            var result = await _sut.GetApprovedSuppliersJoinRequestsByAnnouncementId(searchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<JoinRequestModel>>(result);
            _moqAnnouncementQueries.Verify(s => s.GetApprovedSuppliersJoinRequestsByAnnouncementId(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetTendersByAnnouncementIdAsync()
        {
            _moqAnnouncementQueries.Setup(x => x.GetTendersByAnnouncementIdAsync(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()))
                           .Returns(() =>
                           {
                               return Task.FromResult(new QueryResult<TenderAnnouncementSuppliersTemplateDetails>(new List<TenderAnnouncementSuppliersTemplateDetails> { new TenderAnnouncementSuppliersTemplateDetails() }, 1, 1, 1));
                           });

            var searchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria();
            var result = await _sut.GetTendersByAnnouncementIdAsync(searchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<TenderAnnouncementSuppliersTemplateDetails>>(result);
            _moqAnnouncementQueries.Verify(s => s.GetTendersByAnnouncementIdAsync(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetAnnouncementSupplierTemplateDetailsByannouncementId()
        {
            ApproveAnnouncemntSupplierTemplateModel model = new ApproveAnnouncemntSupplierTemplateModel() { AnnouncementId = 1 };
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementSupplierTemplateDetailsForApproval(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<ApproveAnnouncemntSupplierTemplateModel>(model);
                           });
             
            var result = await _sut.GetAnnouncementSupplierTemplateDetailsByannouncementId(model.AnnouncementId);

            Assert.NotNull(result);
            Assert.IsType<ApproveAnnouncemntSupplierTemplateModel>(result);
         }
        
        [Fact]
        public async Task Should_FindAnnouncementDetailsByAnnouncementId()
        {
            AnnouncementSuppliersTemplateDetailsModel model = new AnnouncementSuppliersTemplateDetailsModel() { AnnouncementId = 1 };
            _moqAnnouncementQueries.Setup(x => x.FindAnnouncementDetailsByAnnouncementId(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AnnouncementSuppliersTemplateDetailsModel>(model);
                           });
             
            var result = await _sut.FindAnnouncementDetailsByAnnouncementId(model.AnnouncementId);

            Assert.NotNull(result);
            Assert.IsType<AnnouncementSuppliersTemplateDetailsModel>(result);
         }
     
        [Fact]
        public async Task Should_GetAnnouncementTemplateActivityAndAddressDetails()
        {
            AnnouncementTemplateActivityAndAreaDetailsModel model = new AnnouncementTemplateActivityAndAreaDetailsModel() { AnnouncementId = 1 };
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementTemplateActivityAndAddressDetails(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AnnouncementTemplateActivityAndAreaDetailsModel>(model);
                           });
             
            var result = await _sut.GetAnnouncementTemplateActivityAndAddressDetails(model.AnnouncementId);

            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateActivityAndAreaDetailsModel>(result);
         }

        [Fact]
        public async Task Should_GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists()
        {
            AddPublicListToAgencyAnnouncementListsModel model = new AddPublicListToAgencyAnnouncementListsModel() { AnnouncementId = 1 };
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(It.IsAny<int>(), It.IsAny<string>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AddPublicListToAgencyAnnouncementListsModel>(model);
                           });
             
            var result = await _sut.GetAnnouncementListDetailsToAddListToAgencyAnnouncementLists(model.AnnouncementId, "123");

            Assert.NotNull(result);
            Assert.IsType<AddPublicListToAgencyAnnouncementListsModel>(result);
         }
       
        [Fact]
        public async Task Should_GetAnnouncementTemplateDetailsForSuppliers()
        {
            
            AnnouncementTemplateMainDetailsModel model = new AnnouncementTemplateMainDetailsModel() { AnnouncementId = 1 };
            
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementTemplateDetailsForSuppliers(It.IsAny<int>(), It.IsAny<string>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AnnouncementTemplateMainDetailsModel>(model);
                           });

            var result = await _sut.GetAnnouncementTemplateDetailsForSuppliers(model.AnnouncementId, "123");

            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateMainDetailsModel>(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementTemplateDetailsForBlockedSuppliers()
        {
            
            AnnouncementTemplateMainDetailsModel model = new AnnouncementTemplateMainDetailsModel() { AnnouncementId = 1 };
            
            _moqBlockAppService.Setup(x => x.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(() =>
                {
                    return Task.FromResult<bool>(true);
                });
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementTemplateDetailsForSuppliers(It.IsAny<int>(), It.IsAny<string>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AnnouncementTemplateMainDetailsModel>(model);
                           });

            var result = await _sut.GetAnnouncementTemplateDetailsForSuppliers(model.AnnouncementId, "123");

            Assert.NotNull(result);
            Assert.IsType<AnnouncementTemplateMainDetailsModel>(result);
        }

        [Fact]
        public async Task Should_GetAnnouncementTemplateJoinRequestDetails()
        {

            AnnouncementSuppliersTemplateJoinRequestsDetailsModel model = new AnnouncementSuppliersTemplateJoinRequestsDetailsModel() { AnnouncementId = 1, JoinRequestId = 1 };

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementTemplateJoinRequestDetails(It.IsAny<int>(), It.IsAny<string>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>(model);
                           });

            var result = await _sut.GetAnnouncementTemplateJoinRequestDetails(model.JoinRequestId, "123");

            Assert.NotNull(result);
            Assert.IsType<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>(result);
        }
        [Fact]
        public async Task Should_GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId()
        {

            AnnouncementSuppliersTemplateJoinRequestsDetailsModel model = new AnnouncementSuppliersTemplateJoinRequestsDetailsModel() { AnnouncementId = 1, JoinRequestId = 1 };

            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>(model);
                           });

            var result = await _sut.GetAnnouncementTemplateJoinRequestDetailsByAnnouncementId(model.AnnouncementId, "123","DataEntry");

            Assert.NotNull(result);
            Assert.IsType<AnnouncementSuppliersTemplateJoinRequestsDetailsModel>(result);
        }

        [Theory]
        [InlineData(100)]
        public async Task ShouldGetBeneficiaryAgencyByAnnouncementIdAsyncSuccess(int announcementId)
        {
            var searchCriteria = new AnnouncementSupplierTemplateBasicSearchCriteria() { announcementId = announcementId };
            await _sut.GetBeneficiaryAgencyByAnnouncementIdAsync(searchCriteria);
            _moqAnnouncementQueries.Verify(s => s.GetBeneficiaryAgencyByAnnouncementIdAsync(It.IsAny<AnnouncementSupplierTemplateBasicSearchCriteria>()), Times.Once);
        }
        [Theory]
        [InlineData(4, "123")]
        public async Task ShouldAddPublicAnnouncementListToAgency(int announcementId, string agencyCode)
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithNoIncludsById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateWithLinkedAgencyDefaults());
                     });
            await _sut.AddPublicAnnouncementListToAgency(announcementId, agencyCode);
            _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncementSupplierTemplateAsync(It.IsAny<AnnouncementSupplierTemplate>()), Times.Once);
        }

        [Fact]
        public async Task ShouldRemovePublicAnnouncementListFromAgency()
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithLinkedAgencyById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateWithLinkedAgencyDefaults());
                     });

            await _sut.RemovePublicAnnouncementListFromAgency(1, "agencycode");
            _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncementSupplierTemplateAsync(It.IsAny<AnnouncementSupplierTemplate>()), Times.Once);
        }




        [Theory]
        [InlineData(100)]
        public async Task ShouldGetAnnouncementDetailsByAnnouncementIdForPrintSuccess(int announcementId)
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementDetailsByAnnouncementIdForPrint(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementTemplateDetailsForPrintModel>(new AnnouncementTemplateDefaults().GetGetAnnouncementDetailsByAnnouncementIdForPrintModelData());
                     });
            var result = await _sut.GetAnnouncementDetailsByAnnouncementIdForPrint(announcementId);
            Assert.NotNull(result);
        }



        [Theory]
        [InlineData(100, "10")]
        public async Task ShouldGetAnnouncementDetailsForSupplierForPrintSuccess(int announcementId, string cr)
        {
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementDetailsForSupplierForPrint(It.IsAny<int>(), It.IsAny<string>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementTemplateDetailsForSupplierForPrintModel>(new AnnouncementTemplateDefaults().GetGetAnnouncementDetailsByAnnouncementIdForPrintForSupplierModelData());
                     });
            var result = await _sut.GetAnnouncementDetailsForSupplierForPrint(announcementId, cr);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldSendJoinRequestToAnnouncmentSuccess()
        {
            List<TenderAttachmentModel> attachmentModel = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };
            MockUser();
            
            AnnouncementTemplateMainDetailsModel announcementModel = new AnnouncementTemplateMainDetailsModel() { Attachments= attachmentModel };
            
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithJoinRequestsById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaultsForRequestJoin());
                     });

            _moqIDMQueries.Setup(i => i.FindUserProfileByIdAsync(It.IsAny<int>()))
     .Returns(() =>
     {
         return Task.FromResult<UserProfile>(new BranchUserDefaults().ReturnUserProfileDefaults());
     });

            var result = await _sut.SendJoinRequestToAnnouncment(announcementModel);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ShouldAnnouncementFinalApproveThrowException()
        {
            AnnouncementFinalApprovalModel approvalModel = new AnnouncementFinalApprovalModel()
            {
                AnnouncementTemplateIdString = "69N3QwUl%208o0yRoVEsRsYA=="
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.AnnouncementFinalApprove(approvalModel));
        }


        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_TenderTypesIsNULL()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.TenderTypesId = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_AreasIdsIsNull()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.AreasIds = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_CountriesIdsIsNull()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.InsideKsa = false;
            createAnnouncementSupplierTemplateModel.CountriesIds = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }


        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_ActivityIdIsNull()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.ActivityIds = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_EffectiveListDateIsNull()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.IsEffectiveList = true;
            createAnnouncementSupplierTemplateModel.EffectiveListDate = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }
        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_EffectiveListDateLessThanToday()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.IsEffectiveList = true;
            createAnnouncementSupplierTemplateModel.EffectiveListDate = DateTime.Now.AddDays(-1);            
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldCreateNewAnnouncementSupplierTemplateThrowException_When_RequiredAttachmentIsNull()
        {
            MockUser();
            CreateAnnouncementSupplierTemplateModel createAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetCreateAnnouncementSupplierTemplateModel();
            createAnnouncementSupplierTemplateModel.IsRequiredAttachmentToJoinList = true;
            createAnnouncementSupplierTemplateModel.RequiredAttachment = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.CreateNewAnnouncementSupplierTemplate(createAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_TenderTypesIdIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.TenderTypesId = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_AreasIdsIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.AreasIds = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_CountriesIdsIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.InsideKsa = false;
            extendAnnouncementSupplierTemplateModel.CountriesIds = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_ActivityIdsIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.ActivityIds = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_EffectiveListDateIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.IsEffectiveList = true;
            extendAnnouncementSupplierTemplateModel.EffectiveListDate = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_EffectiveListDateLessThanToday()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.IsEffectiveList = true;
            extendAnnouncementSupplierTemplateModel.EffectiveListDate = DateTime.Now.AddDays(-1);
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_RequiredAttachmentIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.IsRequiredAttachmentToJoinList = true;
            extendAnnouncementSupplierTemplateModel.RequiredAttachment = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldExtendAnnouncementTemplateThrowException_When_AgencyDataIsNull()
        {
            ExtendAnnouncementSupplierTemplateModel extendAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetExtendAnnouncementSupplierTemplateModelData();
            extendAnnouncementSupplierTemplateModel.AgencyAddress = null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.ExtendAnnouncementTemplate(extendAnnouncementSupplierTemplateModel));
        }


        [Fact]
        public async Task ShouldUpdateApprovedAnnouncementSupplierTemplateListThrowExeption_When_EffectiveListDateLessThanToday()
        {
            UpdateAnnouncementSupplierTemplateModel updateAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelDataForUpdateList();
            updateAnnouncementSupplierTemplateModel.IsEffectiveList =true;
            updateAnnouncementSupplierTemplateModel.EffectiveListDate =DateTime.Now.AddDays(-1);
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateAnnouncementSupplierTemplateList(updateAnnouncementSupplierTemplateModel));
        }

        [Fact]
        public async Task ShouldUpdateApprovedAnnouncementSupplierTemplateListThrowExeption_When_EffectiveListDateIsNull()
        {
            UpdateAnnouncementSupplierTemplateModel updateAnnouncementSupplierTemplateModel = new AnnouncementTemplateDefaults().GetAllAnnouncementSupplierTemplateModelDataForUpdateList();
            updateAnnouncementSupplierTemplateModel.IsEffectiveList = true;
            updateAnnouncementSupplierTemplateModel.EffectiveListDate =null;
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.UpdateAnnouncementSupplierTemplateList(updateAnnouncementSupplierTemplateModel));
        }


      
        [Fact]
        public async Task ShouldCreateVerificationCodeThrowExeption()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _sut.CreateVerificationCode(null));
        }


        [Fact]
        public async Task ShouldAnnouncementFinalApproveSuccess()
        {
            AnnouncementFinalApprovalModel approvalModel = new AnnouncementFinalApprovalModel()
            {
                AnnouncementTemplateIdString = "69N3QwUl%208o0yRoVEsRsYA==",
                VerificationCode = "1234",
                SuppliersIdsString = new List<string>() { "69N3QwUl%208o0yRoVEsRsYA==" },                
            };
            _moqAnnouncementDomainService.Setup(x => x.CheckVerificationCode(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<bool>(true);
                     });
            _moqAnnouncementQueries.Setup(x => x.GetAnnouncementWithJoinRequestsById(It.IsAny<int>()))
                     .Returns(() =>
                     {
                         return Task.FromResult<AnnouncementSupplierTemplate>(new AnnouncementTemplateDefaults().ReturnAnnouncementSupplierTemplateDefaultsForRequestJoin());
                     });
            await _sut.AnnouncementFinalApprove(approvalModel);
            _moqAnnouncementCommands.Verify(m => m.UpdateAnnouncementSupplierTemplateAsync(It.IsAny<AnnouncementSupplierTemplate>()), Times.Once);
        }




        private void MockUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var selectedCr = new Claim(IdentityConfigs.Claims.SelectedCR, "1299659801");
            var selectedAgency = new Claim(IdentityConfigs.Claims.SelectedGovAgency, "022001000000");

            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            idintity.AddClaim(selectedCr);
            idintity.AddClaim(selectedAgency);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}