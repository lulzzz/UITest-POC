using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.Settings;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.InvitationDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.OfferDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults.ChangeRequestDefault;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.LocalContentSettings;
using MOF.Etimad.Monafasat.ViewModel.Tender;
using MOF.Etimad.Monafasat.ViewModel.Tender.LocalContent;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;
namespace MOF.Etimad.Monafasat.UnitTests.Services.Tenders
{
    // [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class TenderAppServiceTest
    {

        #region Mock

        private readonly Mock<INotificationAppService> _NotificationService;
        private readonly Mock<ISupplierQueries> _ISupplierQueries;
        private readonly Mock<ISupplierCommands> _ISupplierCommands;
        private readonly Mock<IIDMQueries> _IIDMQueries;
        private readonly Mock<IIDMAppService> _IIDMAppService;
        private readonly Mock<ICommitteeQueries> _ICommitteeQueries;
        private readonly Mock<IBlockAppService> _IBlockAppService;
        private readonly Mock<IMemoryCache> _IMemoryCache;
        private readonly Mock<IBranchAppService> _IBranchAppService;
        private readonly Mock<IBillAppService> _IBillAppService;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IVerificationService> _moqVerificationService;
        private readonly Mock<ILogger<TenderAppService>> _loggerMock;
        private readonly Mock<ISRMFrameworkAgreementManageProxy> _moqSRMFrameworkAgreementManageProxy;
        private readonly Mock<ILocalContentSettingsAppService> _moqLocalContentSettingsAppService;

        private readonly TenderAppService _sutTenderService;
        private readonly AppDbContext _moqAppDbContext;
        private readonly Mock<IHttpContextAccessor> _httpContext;
        private readonly Mock<IGenericCommandRepository> _moqCommandRepository;
        private readonly Mock<ITenderQueries> _moqTenderQueries;
        private readonly Mock<ITenderCommands> _moqTenderCommand;
        private readonly Mock<ITenderDomainService> _moqTenderDomainService;

        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;

        private readonly Mock<IQuantityTemplatesProxy> _moqQuantityTableProxy;
        private readonly Mock<IOfferQueries> _moqOfferQueries;
        private readonly Mock<ICommunicationQueries> _moqCommunicationQueries;
        private readonly Mock<IBillCommand> _moqBillCommand;
        private readonly Mock<INegotiationQueries> _moqNegotiationQueries;
        private readonly Mock<ILocalContentConfigurationSettings> _moqLocalContentConfigurationSettings;


        const int TENDER_ID = 1;
        const int STOPING_PERIOD = 5;
        const string AGENCY_CODE = "022001000000";
        const string REJECTION_REASON = "Rejection reason";

        const string VERIFICATION_CODE = "123";


        public TenderAppServiceTest()
        {
            var db = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestingDB")
                .Options;
            _httpContext = new Mock<IHttpContextAccessor>();
            _moqAppDbContext = new AppDbContext(db, _httpContext.Object);
            _moqCommandRepository = new Mock<IGenericCommandRepository>();
            _moqTenderQueries = new Mock<ITenderQueries>();
            _moqTenderCommand = new Mock<ITenderCommands>();
            _moqTenderDomainService = new Mock<ITenderDomainService>();
            //_moqAppDbContext = InitialData.context;
            //_moqAppDbContext.SaveChangesAsync();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _moqQuantityTableProxy = new Mock<IQuantityTemplatesProxy>();
            _IIDMAppService = new Mock<IIDMAppService>();
            _mapper = new Mock<IMapper>();
            _NotificationService = new Mock<INotificationAppService>();
            _ISupplierCommands = new Mock<ISupplierCommands>();
            _ISupplierQueries = new Mock<ISupplierQueries>();
            _IIDMQueries = new Mock<IIDMQueries>();
            _moqVerificationService = new Mock<IVerificationService>();
            _ICommitteeQueries = new Mock<ICommitteeQueries>();
            _IBlockAppService = new Mock<IBlockAppService>();
            _IMemoryCache = new Mock<IMemoryCache>();
            _IBranchAppService = new Mock<IBranchAppService>();
            _IBillAppService = new Mock<IBillAppService>();
            _loggerMock = new Mock<ILogger<TenderAppService>>();

            _moqSRMFrameworkAgreementManageProxy = new Mock<ISRMFrameworkAgreementManageProxy>();
            _moqLocalContentSettingsAppService = new Mock<ILocalContentSettingsAppService>();
            _moqOfferQueries = new Mock<IOfferQueries>();
            _moqCommunicationQueries = new Mock<ICommunicationQueries>();
            _moqBillCommand = new Mock<IBillCommand>();
            _moqNegotiationQueries = new Mock<INegotiationQueries>();
            _moqLocalContentConfigurationSettings = new Mock<ILocalContentConfigurationSettings>();


            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForTender());

            _sutTenderService = new TenderAppService(_moqQuantityTableProxy.Object, _NotificationService.Object, _moqTenderQueries.Object,
               _moqTenderCommand.Object, _moqTenderDomainService.Object,
                _mapper.Object,
                _IBillAppService.Object,
                _httpContext.Object,
                _IIDMQueries.Object,
                _ICommitteeQueries.Object,
                _ISupplierQueries.Object,
                _IBlockAppService.Object,
                _IIDMAppService.Object,
                _ISupplierCommands.Object,
                _moqCommandRepository.Object,
                _IMemoryCache.Object,
                _IIDMAppService.Object,
                _IBranchAppService.Object,
                _moqVerificationService.Object,
                _loggerMock.Object,
                _rootConfigurationMock.Object,
                _moqBillCommand.Object,
                _moqLocalContentSettingsAppService.Object,
                _moqSRMFrameworkAgreementManageProxy.Object,
                _moqOfferQueries.Object,
                _moqCommunicationQueries.Object,
                _moqLocalContentConfigurationSettings.Object);
        }
        #endregion Mock

        [Fact]
        public void ShouldConstructTenderAppService()
        {
            Assert.NotNull(_sutTenderService);
            Assert.IsType<TenderAppService>(_sutTenderService);

        }



        //[Theory]
        //[InlineData(-100.00, 200.00)]
        //[InlineData(300.00, -200.00)]
        //public async Task TestUpdateTenderTypeAddedValue_ThrowsBusinessRuleExceptionAsync(decimal buyingCost, decimal invitationCost)
        //{
        //
        //    // Arrange
        //    var tenderTypes = new List<TenderTypeWithAddedValueModel>()
        //       {
        //        new TenderTypeWithAddedValueModel() {
        //            BuyingCost = buyingCost,
        //        Name = "TEST",
        //        Id = 1,
        //        InvitationCost = invitationCost,
        //        },
        //   };

        //    await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.UpdateTenderTypeAddedValueAsync(tenderTypes));
        //}

        //[Theory]
        //[InlineData(400.00, 400.00)]
        //public async Task TestUpdateTenderTypeAddedValue_SuccessAsync(decimal buyingCost, decimal invitationCost)
        //{
        //    // Arrange
        //    var tenderTypes = new List<TenderTypeWithAddedValueModel>()
        //       {
        //        new TenderTypeWithAddedValueModel() {
        //            BuyingCost = buyingCost,
        //        Name = "TEST",
        //        Id = 1,
        //        InvitationCost = invitationCost,
        //        },
        //   };

        //    await _sutTenderService.UpdateTenderTypeAddedValueAsync(tenderTypes);
        //}

        [Theory, Priority(-10)]
        [InlineData("منافسة لاختبار تقديم العروض ممنوع الاقتراب والتصوير ", (int)Enums.TenderType.NewTender, (int)Enums.OfferPresentationWayId.OneFile)]
        public async Task ShouldCreateBasicTenderStepAsync(string tenderName, int tenderTypeId, int offerPresentationWay)
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            TenderDatesModel datesModel = new TenderDatesModel();

            CreateTenderBasicDataModel createModel = new CreateTenderBasicDataModel();
            if (tenderTypeId == (int)Enums.TenderType.CurrentTender)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.NewTender)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.FirstStageTender)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.Competition)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.FrameworkAgreement)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.LimitedTender)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }
            else if (tenderTypeId == (int)Enums.TenderType.ReverseBidding)
            {
                createModel = new TenderDefault().GetBasicTenderModel();
            }

            _moqTenderQueries.Setup(x => x.GetCurrentActivityVersion()).Returns(() => Task.FromResult(new VersionHistory("current Version", (int)Enums.VersionType.TenderActivity, true, "Description")));

            datesModel = await _sutTenderService.CreateBasicTender(createModel);
            Assert.NotNull(datesModel.TenderIdString);
        }

        //[Fact, Priority(-9)]
        //public async Task ShouldCreateDatesStepAsync()
        //{
        //    #region Mock-User
        //    var context = new Mock<HttpContext>();
        //    var claim = new Claim("sub", "15445");
        //    var idintity = new GenericIdentity("testUser");
        //    idintity.AddClaim(claim);
        //    context.SetupGet(x => x.User.Identity).Returns(() =>
        //    {
        //        return idintity;
        //    });
        //    _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
        //    #endregion Mock-User
        //    TenderDatesModel datesModel;
        //    int tenderTypeId = 1;
        //    if (tenderTypeId == (int)Enums.TenderType.Competition || tenderTypeId == (int)Enums.TenderType.FirstStageTender || tenderTypeId == (int)Enums.TenderType.NewDirectPurchase)
        //    {
        //        datesModel = new TenderDatesDefaults().GetTenderDatesWithCheckDate();
        //    }
        //    else
        //    {
        //        datesModel = new TenderDatesDefaults().GetTenderDatesWithOpenDate();

        //    }
        //    _moqTenderQueries.Setup(m => m.FindTenderRelationsByTenderId(It.IsAny<int>()))
        //        .Returns(() =>
        //        {
        //            return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
        //        });
        //    _moqTenderDomainService.Setup(m => m.IsValidToUpdateDate(It.IsAny<TenderDatesModel>(), It.IsAny<Tender>()));
        //    var result = await _sutTenderService.UpdateTenderDates(datesModel);
        //    Assert.NotNull(result);
        //    Assert.NotNull(result.TenderIdString);
        //}

        [Fact, Priority(-8)]
        public async Task ShouldCreatRelationStepAsync()
        {
            TenderRelationsModel relationsModel;
            relationsModel = new TenderDefault().GetTenderRelationsDefaultData();

            _moqTenderQueries.Setup(m => m.FindTenderRelationsWithoutQuantityTables(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });
            var result = await _sutTenderService.UpdateTenderRelations(relationsModel);
            Assert.NotNull(result);
            Assert.NotNull(result.TenderIdString);
        }

        [Fact, Priority(-6)]
        public async Task ShouldCreateAttachmentsStepAsync()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            List<TenderAttachmentModel> attachmentModel = new List<TenderAttachmentModel>()
            {
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081036",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 1 ,Name = "Test Attachment 1"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000081236",AttachmentTypeId = 16 , TenderStatusId = 2 , TenderId = 2 ,Name = "Test Attachment 2"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011050",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 3 ,Name = "Test Attachment 3"},
                new TenderAttachmentModel{FileNetReferenceId ="idd_2CE19608-84B1-C336-8521-6875EB300000011250",AttachmentTypeId = 1 , TenderStatusId = 2 , TenderId = 4 ,Name = "Test Attachment 4"}
            };
            _moqTenderQueries.Setup(m => m.FindTenderAttachmentsStepById(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderWithPrequalification());
            });

            _moqTenderCommand.Setup(m => m.UpdateAsync(It.IsAny<Tender>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<Tender>(new Tender());
                 });
            var result = await _sutTenderService.UpdateTenderAttachmentsAsync(attachmentModel, 1);

            Assert.NotNull(result);
            Assert.IsType<BasicTenderInfoModel>(result);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);

        }

        [Fact, Priority(-5)]
        public async Task ShouldSendTenderToApproveAsync()
        {
            int tenderId = 4;

            #region Mock-User
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.FindTenderForSendToApproval(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(tender);
                });
            tender.UpdateTenderStatus(Enums.TenderStatus.Established);

            await _sutTenderService.SendTenderToApprove(tenderId);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact, Priority(-4)]
        public async Task ShouldApproveTenderAsync()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            Tender tender = new TenderDefault().GetGeneralTenderWithLocalContent();

            List<string> crs = new List<string> { "123" };
            tender.AddSupplierInvitationWhileCreation(crs, 12);
            tender.UpdateTenderStatus(Enums.TenderStatus.Pending);
            tender.SetCreationDate();
            tender.AddIsTenderContainsTawreedTables_ForTest(true);

            _moqTenderQueries.Setup(m => m.FindTenderForApprove(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });


            _IIDMAppService.Setup(m => m.GetAgencyDetailsRelatedToSadad(It.IsAny<string>(), It.IsAny<int>()))
                .Returns
                (() =>
                {
                    return Task.FromResult<AgencyInfoModel>(new AgencyDefaults().GetAgencyDefaults());
                });

            _moqLocalContentSettingsAppService.Setup(m => m.Get()).Returns
                (() =>
                {
                    return Task.FromResult<LocalContentSettingsViewModel>(new LocalContenttSettingDefault().GetLocalContentSettingDefault());
                });

            _moqTenderQueries.Setup(m => m.IsTenderHasLocalContent(It.IsAny<DateTime>()))
              .Returns(() =>
              {
                  return Task.FromResult<bool>(true);
              });

            TenderLocalContent tenderLocalContent = new TenderLocalContent();
            _moqTenderQueries.Setup(m => m.GetTenderLocalContentByTenderId(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<TenderLocalContent>(tenderLocalContent);
              });

            List<int> tenderLocalContentMechanismIds = new List<int>() { 2 };// (int)Enums.LocalContentMechanismPerfermance.MinimumRequiredMechanismLocalContent };
            _moqTenderQueries.Setup(m => m.GetTenderLocalContentMechanism(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<int>>(tenderLocalContentMechanismIds);
              });
            //The input to be used in a unit test should be the simplest possible in order to verify the behavior that you are currently testing.
            await _sutTenderService.ApproveTenderWithInbudget(TENDER_ID, VERIFICATION_CODE, true, true);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldApproveTenderVRO()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            Tender tender = new TenderDefault().GetGeneralTenderWithPrequalification();

            List<string> crs = new List<string> { "123" };
            tender.AddSupplierInvitationWhileCreation(crs, 12);
            tender.UpdateTenderStatus(Enums.TenderStatus.Pending);

            _moqTenderQueries.Setup(m => m.FindTenderForApproval(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });


            ApproveVROModel approveVROModel = new ApproveVROModel
            {
                TenderIdString = Util.Encrypt(1),
                LastEnqueriesDate = DateTime.Now.AddDays(1),
                LastOfferPresentationDate = DateTime.Now.AddDays(2),
                OffersOpeningDate = DateTime.Now.AddDays(3),
                OffersOpeningAddress = "Address",
                TechnicalOrganizationId = 1,
                OpenCheckCommitteeId = 1,
                OffersOpeningAddressId = 1
            };

            await _sutTenderService.ApproveTenderVRO(approveVROModel, 1);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldRejectTender()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(tender);
                });
            tender.UpdateTenderStatus(Enums.TenderStatus.Pending);
            await _sutTenderService.RejectTender(1, "Rejection Reason");
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenTenderTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(tender);
                });
            tender.UpdateTenderStatus(Enums.TenderStatus.Rejected);

            await _sutTenderService.ReopenTender(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task OpenTenderOffersTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            tender.SetTenderType((int)Enums.TenderType.CurrentDirectPurchase);

            _moqTenderQueries.Setup(m => m.FindTenderByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);
            await _sutTenderService.OpenTenderOffer(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendTenderToApproveOppenningTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOppening);
            await _sutTenderService.SendTenderToApproveOppenning(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderOppeningTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOppenedPending);

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderDomainService.Setup(m => m.GetReceivedOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });
            await _sutTenderService.ApproveTenderOppening(TENDER_ID);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectOpenTenderOffersTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOppenedPending);

            await _sutTenderService.RejectOpenTenderOffers(TENDER_ID, REJECTION_REASON);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenTenderOfferTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOppenedRejected);
            await _sutTenderService.ReopenTenderOffer(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }
        [Fact]
        public async Task SendTenderToApproveCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            await _sutTenderService.SendTenderToApproveChecking(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWitOffersAndBidingRounds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedPending);

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderDomainService.Setup(m => m.GetReceivedAndIdenticalOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });
            IEnumerable<Offer> offersForNotification = new OfferDefaults().GetOfferList();
            _moqTenderQueries.Setup(o => o.GetOffersByTenderIdAsync(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<Offer>>(offersForNotification.ToQueryResult().Result);
                });
            await _sutTenderService.ApproveTenderChecking(TENDER_ID);

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);

        }

        [Fact]
        public async Task EndOpenFinantialOffersStageTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            List<Offer> offers = new OfferDefaults().GetOfferList();
            tender.SetTenderReceivedOffers(offers);

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage);


            await _sutTenderService.EndOpenFinantialOffersStage(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }



        [Fact]
        public async Task EndOpenFinantialOffersStageTest_ThrowException_IfTenderStatusWrong()
        {
            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.EndOpenFinantialOffersStage(TENDER_ID));
        }

        [Fact]
        public async Task RejectCheckTenderOffersTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedPending);

            await _sutTenderService.RejectCheckTenderOffers(TENDER_ID, REJECTION_REASON);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenTenderCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedRejected);

            await _sutTenderService.ReopenTenderChecking(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendTenderToApproveTechnicalCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);

            await _sutTenderService.SendTenderToApproveTechnicalCheckingAsync(TENDER_ID, AGENCY_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTendeTechnicalCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderDomainService.Setup(m => m.GetReceivedAndIdenticalOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });
            IEnumerable<Offer> offersForNotification = new OfferDefaults().GetOfferList();
            _moqTenderQueries.Setup(o => o.GetOffersByTenderIdAsync(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<QueryResult<Offer>>(offersForNotification.ToQueryResult().Result);
                });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending);

            await _sutTenderService.ApproveTendeTechnicalCheckingAsync(TENDER_ID, VERIFICATION_CODE, AGENCY_CODE);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectTendeTechnicalCheckingAsyncTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });


            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending);

            await _sutTenderService.RejectTendeTechnicalCheckingAsync(TENDER_ID, REJECTION_REASON, AGENCY_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReOpenTendeTechnicalCheckingAsyncTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected);

            await _sutTenderService.ReOpenTendeTechnicalCheckingAsync(TENDER_ID, AGENCY_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReOpenTendeTechnicalCheckingAsyncTest_RetuendFromPlaint()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.BackForCheckingFromPlaint);

            await _sutTenderService.ReOpenTendeTechnicalCheckingAsync(TENDER_ID, AGENCY_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReOpenTendeTechnicalCheckingAsyncTest_ThrowException_IfAgencyDifferent()
        {
            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected);
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sutTenderService.ReOpenTendeTechnicalCheckingAsync(TENDER_ID, "123"));

        }
        [Fact]
        public async Task ReOpenTendeTechnicalCheckingAsyncTest_ThrowException_IfAStatusWrong()
        {
            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ReOpenTendeTechnicalCheckingAsync(TENDER_ID, AGENCY_CODE));
        }

        [Fact]
        public async Task MoveTenderToFinancialOffersCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            tender.SetTenderReceivedOffers(new OfferDefaults().GetOfferList());
            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage);

            await _sutTenderService.MoveTenderToFinancialOffersChecking(TENDER_ID);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task MoveTenderToFinancialOffersChecking_ShouldThrowException()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage);

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.MoveTenderToFinancialOffersChecking(TENDER_ID));
        }

        [Fact]
        public async Task MoveTenderToFinancialOffersChecking_ShouldThrowException_IfTenderStatusWrong()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.MoveTenderToFinancialOffersChecking(TENDER_ID));
        }

        [Fact]
        public async Task SendTenderToApproveFinancailCheckingAsyncTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking);

            await _sutTenderService.SendTenderToApproveFinancailCheckingAsync(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendTenderToApproveFinancailCheckingAsyncTest_ShouldThrowException_IfTenderStatusWrong()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.SendTenderToApproveFinancailCheckingAsync(TENDER_ID, AGENCY_CODE));
        }

        [Fact]
        public async Task SendTenderToApproveFinancailCheckingAsyncTest_ShouldThrowException_IfAgencyCodeDifferent()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking);
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sutTenderService.SendTenderToApproveFinancailCheckingAsync(TENDER_ID, "111"));
        }

        [Fact]
        public async Task ApproveTendeFinancialCheckingAsyncTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            List<Offer> offersForNotification = new OfferDefaults().GetOfferList();
            _moqTenderDomainService.Setup(o => o.GetFinancialAcceeptedOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offersForNotification);
                });
            _moqTenderDomainService.Setup(o => o.GetFinancialRejectedOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offersForNotification);
                });

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingPending);

            await _sutTenderService.ApproveTendeFinancialCheckingAsync(TENDER_ID, VERIFICATION_CODE, AGENCY_CODE);

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTendeFinancialCheckingAsyncTest_ShouldThrowException_IfTenderStatusWrong()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ApproveTendeFinancialCheckingAsync(TENDER_ID, VERIFICATION_CODE, AGENCY_CODE));
        }

        [Fact]
        public async Task ApproveTendeFinancialCheckingAsyncTest_ShouldThrowException_IfAgencyCodeDifferent()
        {
            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sutTenderService.ApproveTendeFinancialCheckingAsync(TENDER_ID, VERIFICATION_CODE, "111"));
        }

        [Fact]
        public async Task RejectTendeFinancialCheckingAsyncTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingPending);

            await _sutTenderService.RejectTendeFinancialCheckingAsync(TENDER_ID, REJECTION_REASON, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectTendeFinancialCheckingAsyncTest_ShouldThrowException_IfTenderStatusWrong()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.RejectTendeFinancialCheckingAsync(TENDER_ID, REJECTION_REASON, AGENCY_CODE));
        }

        [Fact]
        public async Task RejectTendeFinancialCheckingAsyncTest_ShouldThrowException_IfAgencyCodeDifferent()
        {
            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sutTenderService.RejectTendeFinancialCheckingAsync(TENDER_ID, REJECTION_REASON, "111"));
        }

        [Fact]
        public async Task ReOpenTendeFinancialCheckingAsyncTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingRejected);

            await _sutTenderService.ReOpenTendeFinancialCheckingAsync(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReOpenTendeFinancialCheckingAsyncTest_ShouldThrowException_IfTenderStatusWrong()
        {

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ReOpenTendeFinancialCheckingAsync(TENDER_ID, AGENCY_CODE));
        }

        [Fact]
        public async Task ReOpenTendeFinancialCheckingAsyncTest_ShouldThrowException_IfAgencyCodeDifferent()
        {
            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(tender);
              });

            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sutTenderService.ReOpenTendeFinancialCheckingAsync(TENDER_ID, "111"));
        }

        #region direct purchase check test
        [Fact]
        public async Task StartDirectPurchaseTenderCheckingOffersTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderDates(DateTime.Now.Date.AddDays(-3), DateTime.Now.Date.AddDays(-2), null, DateTime.Now.Date.AddDays(-1), null, null, false, null, 1, "building name", "Floar number", "Department Number", null);
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);

            await _sutTenderService.StartDirectPurchaseTenderCheckingOffers(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendDirectPurchaseTenderOffersTechnicalCheckingToApproveTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);

            await _sutTenderService.SendDirectPurchaseTenderOffersTechnicalCheckingToApprove(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendDirectPurchaseTenderOffersCheckingToApproveTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);

            await _sutTenderService.SendDirectPurchaseTenderOffersCheckingToApprove(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);

        }

        [Fact]
        public async Task ApproveDirectPurchaseTenderOffersCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending);

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderDomainService.Setup(m => m.GetNotIdenticalOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });
            await _sutTenderService.ApproveDirectPurchaseTenderOffersChecking(TENDER_ID, VERIFICATION_CODE, AGENCY_CODE);

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectDirectPurchaseTenderOffersCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending);

            await _sutTenderService.RejectDirectPurchaseTenderOffersChecking(TENDER_ID, REJECTION_REASON, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenDirectPurchaseTenderOffersCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected);

            await _sutTenderService.ReopenDirectPurchaseTenderOffersChecking(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveDirectPurchaseTenderOffersTechnicalCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending);

            await _sutTenderService.ApproveDirectPurchaseTenderOffersTechnicalChecking(TENDER_ID, VERIFICATION_CODE, AGENCY_CODE);

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectDirectPurchaseTenderOffersTechnicalCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingPending);

            await _sutTenderService.RejectDirectPurchaseTenderOffersTechnicalChecking(TENDER_ID, REJECTION_REASON, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenDirectPurchaseTenderOffersTechnicalCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalCheckingRejected);

            await _sutTenderService.ReopenDirectPurchaseTenderOffersTechnicalChecking(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendDirectPurchaseTenderOffersFinanceCheckingToApproveTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking);

            await _sutTenderService.SendDirectPurchaseTenderOffersFinanceCheckingToApprove(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveDirectPurchaseTenderOffersFinanceCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingPending);

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderDomainService.Setup(m => m.GetNotIdenticalOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });

            _moqTenderDomainService.Setup(m => m.GetFinancialAcceeptedOffersByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });
            _moqTenderDomainService.Setup(m => m.GetFinancialRejectedOffersByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(offers);
               });
            await _sutTenderService.ApproveDirectPurchaseTenderOffersFinanceChecking(TENDER_ID, VERIFICATION_CODE, AGENCY_CODE);

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectDirectPurchaseTenderOffersFinanceCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingPending);

            await _sutTenderService.RejectDirectPurchaseTenderOffersFinanceChecking(TENDER_ID, REJECTION_REASON, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenDirectPurchaseTenderOffersFinancialCheckingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialOfferCheckingRejected);

            await _sutTenderService.ReopenDirectPurchaseTenderOffersFinancialChecking(TENDER_ID, AGENCY_CODE);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }
        #endregion

        [Fact]
        public async Task AwardTenderOffersTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersCheckedConfirmed);

            await _sutTenderService.AwardTenderOffers(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task SendAwardTenderToApproveTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwarding);

            await _sutTenderService.SendAwardTenderToApprove(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ViewDirectPurchaseDetailsForBasicWay()
        {
            TenderDetailsModel _tender = new TenderDetailsModel()
            {
                AgencyCode = "123456789",
                TenderTypeId = (int)Enums.TenderType.NewDirectPurchase,
                TenderName = "direct purchase Tender 1",
                EstimatedValue = 40000,
                IsLowBudgetPurchase = false,
                DirectPurchaseCommitteeName = "Purchase Committee",
            };
            _moqTenderQueries.Setup(m => m.GetMainTenderDetailsByTenderId(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<TenderDetailsModel>(_tender);
               });

            var tender = await _sutTenderService.GetMainTenderDetailsByTenderId(It.IsAny<int>(), It.IsAny<int>());

            Assert.Equal("direct purchase Tender 1", tender.TenderName);
            Assert.False(tender.IsLowBudgetPurchase);
            Assert.Equal("Purchase Committee", tender.DirectPurchaseCommitteeName);
            Assert.Null(tender.PurchaseMemberName);
        }

        [Fact]
        public async Task ViewDirectPurchaseDetailsForLowBudgetWay()
        {
            TenderDetailsModel _tender = new TenderDetailsModel()
            {
                AgencyCode = "123456789",
                TenderTypeId = (int)Enums.TenderType.NewDirectPurchase,
                TenderName = "direct purchase Tender 1",
                EstimatedValue = 30000,
                IsLowBudgetPurchase = true,
                NationalProductRate = 0,
                PurchaseMemberName = "Purchase Member"
            };
            _moqTenderQueries.Setup(m => m.GetMainTenderDetailsForUnit(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() =>
               {
                   return Task.FromResult<TenderDetailsModel>(_tender);
               });

            _moqLocalContentSettingsAppService.Setup(m => m.Get())
                .Returns(() =>
                {
                    return Task.FromResult<LocalContentSettingsViewModel>(new LocalContentSettingsViewModel() { NationalProductPercentage = 1 });
                });

            var tender = await _sutTenderService.GetMainTenderDetailsForUnit(It.IsAny<int>(), It.IsAny<string>());

            Assert.Equal("direct purchase Tender 1", tender.TenderName);
            Assert.True(tender.IsLowBudgetPurchase);
            Assert.Equal("Purchase Member", tender.PurchaseMemberName);
            Assert.Null(tender.DirectPurchaseCommitteeName);
        }

        //[Fact]
        //public async Task SendAwardTenderToInitialApproveTest()
        //{
        //    #region Mock-User
        //    var context = new Mock<HttpContext>();
        //    var claim = new Claim("sub", "15445");
        //    var idintity = new GenericIdentity("testUser");
        //    idintity.AddClaim(claim);
        //    context.SetupGet(x => x.User.Identity).Returns(() =>
        //    {
        //        return idintity;
        //    });
        //    _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
        //    #endregion Mock-User
        [Fact]
        public async Task SendAwardTenderToInitialApproveTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderByIdForAwarding(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwarding);

            tender.UpdateAwardingStoppingPeriod(STOPING_PERIOD);

            await _sutTenderService.SendAwardTenderToInitialApprove(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderInitialAwardingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderByIdForAwarding(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedPending);

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderQueries.Setup(m => m.FindSuppliersOffersInAwardingStageByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });

            await _sutTenderService.ApproveTenderInitialAwarding(TENDER_ID, AGENCY_CODE, VERIFICATION_CODE);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderInitialAwardingThrowBusinessRuleExceptionIfStatusWrongTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.FindTenderByIdForAwarding(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderQueries.Setup(m => m.FindSuppliersOffersInAwardingStageByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });

            await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.ApproveTenderInitialAwarding(TENDER_ID, AGENCY_CODE, VERIFICATION_CODE));
        }

        [Fact]
        public async Task ApproveTenderInitialAwardingNewDirectpurchaseLowValueThrowBusinessRuleExceptionIfStatusWrongTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchaseLowValue();

            _moqTenderQueries.Setup(m => m.FindTenderByIdForAwarding(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderQueries.Setup(m => m.FindSuppliersOffersInAwardingStageByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });

            await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.ApproveTenderInitialAwarding(TENDER_ID, AGENCY_CODE, VERIFICATION_CODE));
        }

        [Fact]
        public async Task ApproveTenderInitialAwardingForNewDirectPurchaseTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.FindTenderByIdForAwarding(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedPending);

            List<Offer> offers = new OfferDefaults().GetOfferList();
            _moqTenderQueries.Setup(m => m.FindSuppliersOffersInAwardingStageByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<List<Offer>>(offers);
                });

            await _sutTenderService.ApproveTenderInitialAwarding(TENDER_ID, AGENCY_CODE, VERIFICATION_CODE);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectInitialAwardTenderOfferTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderFoAwardingStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedPending);

            await _sutTenderService.RejectInitialAwardTenderOffer(TENDER_ID, REJECTION_REASON, AGENCY_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderAwarding_WithGurantee_Test()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            tender.SetCreationDate();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithAwardingHistoy(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderAwardingNotification(true, 1, 1, "finalGuaranteeDeliveryAddress");
            tender.AddActionHistory((int)Enums.TenderStatus.OffersAwardedConfirmed, Enums.TenderActions.ApproveAwarding, "", 123);

            List<Offer> offers = new OfferDefaults().GetOfferList();

            _moqTenderQueries.Setup(m => m.FindSuppliersOffersInAwardingStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(offers);
               });

            _moqTenderQueries.Setup(m => m.FindNotAwardedSuppliersCauseExcludedReason(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(offers);
               });

            List<string> suppliers = new List<string> { "111" };
            List<string> failedSuppliers = new List<string> { "222" };
            List<string> suppliersAcceptInitialyExtendOfferValidity = new List<string> { "333" };
            List<string> suppliersRejectExtendOfferValidity = new List<string> { "444" };
            List<string> suppliersNotRespontToExtendOfferValidity = new List<string> { "555" };

            List<Offer> FaildTechnicaloffers = new OfferDefaults().GetFaildTechnicalOfferList();
            List<Offer> FaildFinantialoffers = new OfferDefaults().GetFailedFinantialOfferList();


            _moqTenderQueries.Setup(m => m.GetFailedSuppliersOnTechnicalEvaluation(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(FaildTechnicaloffers);
               });
            _moqTenderQueries.Setup(m => m.GetFailedSuppliersOnFinancialEvaluation(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(FaildFinantialoffers);
               });

            _moqOfferQueries.Setup(m => m.GetFailedSuppliersOnPostQualification(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<List<string>>(failedSuppliers);
            });

            _moqCommunicationQueries.Setup(m => m.FindSuppliersThatAcceptInitialyExtendOfferValidity(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<string>>(suppliersAcceptInitialyExtendOfferValidity);
               });
            _moqCommunicationQueries.Setup(m => m.FindSuppliersThatRejectExtendOfferValidity(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<string>>(suppliersRejectExtendOfferValidity);
              });

            _moqCommunicationQueries.Setup(m => m.FindSuppliersThatNotResponseToExtendOfferValidity(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<string>>(suppliersNotRespontToExtendOfferValidity);
              });

            _moqOfferQueries.Setup(o => o.GetLocalContentSettingsDate())
                .Returns(() => { return Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }); });

            tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedConfirmed);

            await _sutTenderService.ApproveTenderAwarding(TENDER_ID, VERIFICATION_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderAwarding_NewDirectPurchase_Test()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();
            tender.SetCreationDate();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithAwardingHistoy(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.AddActionHistory((int)Enums.TenderStatus.OffersAwardedConfirmed, Enums.TenderActions.ApproveAwarding, "", 123);

            List<Offer> offers = new OfferDefaults().GetOfferList();

            _moqTenderQueries.Setup(m => m.FindSuppliersOffersInAwardingStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(offers);
               });

            _moqTenderQueries.Setup(m => m.FindNotAwardedSuppliersCauseExcludedReason(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(offers);
               });

            List<string> suppliers = new List<string> { "111" };
            List<string> failedSuppliers = new List<string> { "222" };
            List<string> suppliersAcceptInitialyExtendOfferValidity = new List<string> { "333" };
            List<string> suppliersRejectExtendOfferValidity = new List<string> { "444" };
            List<string> suppliersNotRespontToExtendOfferValidity = new List<string> { "555" };

            List<Offer> FaildTechnicaloffers = new OfferDefaults().GetFaildTechnicalOfferList();
            List<Offer> FaildFinantialoffers = new OfferDefaults().GetFailedFinantialOfferList();


            _moqTenderQueries.Setup(m => m.GetFailedSuppliersOnTechnicalEvaluation(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(FaildTechnicaloffers);
               });
            _moqTenderQueries.Setup(m => m.GetFailedSuppliersOnFinancialEvaluation(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<Offer>>(FaildFinantialoffers);
               });


            _moqOfferQueries.Setup(m => m.GetFailedSuppliersOnPostQualification(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<List<string>>(failedSuppliers);
            });

            _moqCommunicationQueries.Setup(m => m.FindSuppliersThatAcceptInitialyExtendOfferValidity(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<List<string>>(suppliersAcceptInitialyExtendOfferValidity);
               });
            _moqCommunicationQueries.Setup(m => m.FindSuppliersThatRejectExtendOfferValidity(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<string>>(suppliersRejectExtendOfferValidity);
              });

            _moqCommunicationQueries.Setup(m => m.FindSuppliersThatNotResponseToExtendOfferValidity(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<string>>(suppliersNotRespontToExtendOfferValidity);
              });

            _moqOfferQueries.Setup(o => o.GetLocalContentSettingsDate())
               .Returns(() => { return Task.FromResult(new ConfigurationSetting() { Id = 1, Date = new DateTime(2021, 4, 1), Description = "local content" }); });


            tender.UpdateTenderStatus(Enums.TenderStatus.OffersInitialAwardedConfirmed);

            await _sutTenderService.ApproveTenderAwarding(TENDER_ID, VERIFICATION_CODE);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ApproveTenderAwarding_ThrowBusinessRuleException_IfStatusWrong_Test()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithAwardingHistoy(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.ApproveTenderAwarding(TENDER_ID, VERIFICATION_CODE));
        }

        [Fact]
        public async Task RejectAwardTenderOfferTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedPending);

            await _sutTenderService.RejectAwardTenderOffer(TENDER_ID, REJECTION_REASON);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task RejectAwardTenderOffer_NewDirectPurchaseTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetNewDirectPurchasePrivate();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedPending);

            await _sutTenderService.RejectAwardTenderOffer(TENDER_ID, REJECTION_REASON);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ReopenTenderAwardingTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.OffersAwardedRejected);

            await _sutTenderService.ReopenTenderAwarding(TENDER_ID);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatIntroductionToConditionsTemplate()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateSecondSectionModel editConditionTemplateSecondSectionModel;
            editConditionTemplateSecondSectionModel = new TenderDefault().GetIntroductionToConditionsTemplateDefaultData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateIntroductionById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });
            await _sutTenderService.AddEditIntroductionTemplate(editConditionTemplateSecondSectionModel, 1);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatPreparingOffersToConditionsTemplate()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateThridSectionModel editConditionTemplateThridSectionModel;
            editConditionTemplateThridSectionModel = new TenderDefault().GetPreparingOffersToConditionsTemplateDefaultData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateIntroductionById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.PreparteOffers));
                });

            await _sutTenderService.AddEditPreparingOffersTemplate(editConditionTemplateThridSectionModel, 1);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }
        [Fact]
        public async Task ShouldCreatPreparingOffersToConditionsTemplateActivityVersion()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateThridSectionModel editConditionTemplateThridSectionModel;
            editConditionTemplateThridSectionModel = new TenderDefault().GetPreparingOffersNewVersionDefaultData();

            TenderDates tenderDates = new TenderDates();
            Tender tender = new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.PreparteOffers);
            tender.SetTenderDates(tenderDates);
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateIntroductionById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(tender);
                });

            await _sutTenderService.AddEditPreparingOffersTemplate(editConditionTemplateThridSectionModel, 1);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatTechnicalDeclerationsToConditionsTemplate()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateSeventhSectionModel editConditionTemplateSeventhSectionModel;
            editConditionTemplateSeventhSectionModel = new TenderDefault().GetTechnicalDeclerationsToConditionsTemplateDefaultData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateTechnicalOutputs(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.Specifications));
                });

            await _sutTenderService.AddEditTechnicalDeclerationsTemplate(editConditionTemplateSeventhSectionModel, 1);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatTechnicalDeclerationsThrowExceptionInNoTerms()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateSeventhSectionModel model = new TenderDefault().GetTechnicalDeclerationsToConditionsTemplateDefaultData();
            model.TechnicalDeclrations = null;
            model.VersionId = (int)Enums.ActivityVersions.Sprint7Activities;
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateTechnicalOutputs(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.Specifications));
                });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditTechnicalDeclerationsTemplate(model, 1));
        }

        [Fact]
        public async Task ShouldCreatTechnicalDeclerationsThrowExceptionInNoOutput()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateSeventhSectionModel model = new TenderDefault().GetTechnicalDeclerationsToConditionsTemplateDefaultData();
            model.TenderConditionsTemplateOutputs = null;
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateTechnicalOutputs(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.Specifications));
                });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditTechnicalDeclerationsTemplate(model, 1));
        }
        [Fact]
        public async Task ShouldCreatTechnicalDeclerationsThrowExceptionInNoWorkScope()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateSeventhSectionModel model = new TenderDefault().GetTechnicalDeclerationsToConditionsTemplateDefaultData();
            model.ProjectsScope = null;
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateTechnicalOutputs(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.Specifications));
                });
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditTechnicalDeclerationsTemplate(model, 1));
        }

        [Fact]
        public async Task ShouldCreatDescriptionToConditionsTemplate()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            EditConditionTemplateEighthSectionModel editConditionTemplateEighthSectionModel;
            editConditionTemplateEighthSectionModel = new TenderDefault().GetDescriptionToConditionsTemplateDefaultData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.Specifications));
                });

            _moqTenderQueries.Setup(m => m.GetCurrentTenderActivityVersion(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<int>(3);
            });

            await _sutTenderService.AddEditDescriptionAndConditionsTemplate(editConditionTemplateEighthSectionModel, 1);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldSendInvitationsInTenderCreationAsync()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            InvitationsInCreateTenderModel invitationsInCreateTenderModel;
            invitationsInCreateTenderModel = new TenderDefault().GetInvitationsInCreateTenderModelDefaultData();
            _moqTenderQueries.Setup(m => m.GetTenderByIdForInvitations(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });

            await _sutTenderService.SendInvitationsInTenderCreation(invitationsInCreateTenderModel);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        //[Theory]
        //[InlineData(1, "1024901843")]
        //public async Task ShouldGetCommunicationRequestsDetailsTenderIdSuccess(int tenderId, string cr)
        //{
        //    var result = await _sutTenderService.GetCommunicationRequestsDetailsTenderId(tenderId, cr);
        //    Assert.NotNull(result);
        //    Assert.IsType<List<BranchModel>>(result);
        //}

        #region Tender Cancelation

        [Fact]
        public async Task ShouldCreateTenderCancelRequest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            TenderRevisionCancelModel tenderRevisionCancelModel = new ChangeRequestDefault().GetTenderRevisionCancelModel();

            Tender tender = new TenderDefault().GetGeneralTender();

            _moqTenderQueries.Setup(m => m.FindTenderByTenderId(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(tender);
              });
            await _sutTenderService.CreateCancelRequest(tenderRevisionCancelModel);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldApproveCancelTenderRequest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            TenderCancelModel tenderCancelModel = new ChangeRequestDefault().GetTenderCancelModel();

            var tenderChangeRequest = new ChangeRequestDefault().GetCancelChangeRequestWithTender();
            tenderChangeRequest.UpdateStatus((int)Enums.ChangeStatusType.Pending, 1, Enums.TenderActions.SendtenderCancelRequest);
            _moqTenderQueries.Setup(x => x.GetCancelChangeRequest(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<TenderChangeRequest>(tenderChangeRequest);
            });
            _moqTenderQueries.Setup(x => x.GetCancelChangeRequest(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<TenderChangeRequest>(tenderChangeRequest);
            });
            _moqTenderQueries.Setup(s => s.FindTenderWithConditionsBookletsBills(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new Tender());
            });
            _moqTenderQueries.Setup(s => s.FindTenderWithInvitationsBills(It.IsAny<int>())).Returns(() =>
                 {
                     return Task.FromResult<Tender>(new Tender());
                 });
            await _sutTenderService.ApproveTenderCancelRequest(tenderCancelModel);

            _moqTenderCommand.Verify(c => c.UpdateTenderChangeRequestAsync(It.IsAny<TenderChangeRequest>()), Times.Once);
        }

        #endregion

        #region Qualification 

        [Fact]
        public async Task ShloudCreateQualificationCancelRequest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            _moqTenderQueries.Setup(x => x.FindTenderByTenderId(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });
            TenderRevisionCancelModel tenderRevisionCancelModel = new ChangeRequestDefault().GetTenderRevisionCancelModel();

            await _sutTenderService.CreateCancelRequestForQualification(tenderRevisionCancelModel);

            _moqTenderCommand.Verify(c => c.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShloudNotCreateQualificationCancelRequestIfThereActiveRequest()
        {
            _moqTenderQueries.Setup(x => x.FindTenderByTenderId(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });
            _moqTenderQueries.Setup(x => x.GetNotPendingCancelChangeRequest(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<TenderChangeRequest>(new ChangeRequestDefault().GetCancelChangeRequestData());
            });
            TenderRevisionCancelModel tenderRevisionCancelModel = new ChangeRequestDefault().GetTenderRevisionCancelModel();

            var result = await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.CreateCancelRequestForQualification(tenderRevisionCancelModel));
            Assert.Equal(Resources.TenderResources.Messages.CannotCreateCancelTender, result.Message);
        }

        [Theory]
        [InlineData(1, "3322", (int)Enums.VerificationType.Tender)]
        public async Task ShouldApproveCancelQualificationRequest(int tenderId, string verificationCode, int typeId)
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            _moqTenderQueries.Setup(x => x.FindTenderToApproveCancel(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });
            _moqTenderQueries.Setup(x => x.GetCancelChangeRequest(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<TenderChangeRequest>(new ChangeRequestDefault().GetCancelChangeRequestWithTender());
            });
            _IIDMAppService.Setup(s => s.GetAllSupplierOnQualification(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<List<string>>(new List<string>());
            });
            await _sutTenderService.ApproveQualificationCancelRequestAsync(tenderId, verificationCode, tenderId);

            _moqTenderCommand.Verify(c => c.UpdateTenderChangeRequestAsync(It.IsAny<TenderChangeRequest>()), Times.Once);
        }
        #endregion Qualification


        #region reda


        //[Fact]
        //public async Task ShouldPurshaseTender()
        //{
        //    #region Mock-User
        //    var context = new Mock<HttpContext>();
        //    var claim = new Claim("sub", "15445");
        //    var idintity = new GenericIdentity("testUser");
        //    idintity.AddClaim(claim);
        //    context.SetupGet(x => x.User.Identity).Returns(() =>
        //    {
        //        return idintity;
        //    });
        //    _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
        //    #endregion Mock-User
        //    Tender tender = new TenderDefault().GetGeneralTender();


        //  tender.SetTenderType

        //    _moqTenderQueries.Setup(m => m.FindTenderAndAgencyByTenderId(It.IsAny<int>()))
        //       .Returns(() =>
        //       {
        //           return Task.FromResult<Tender>(tender);
        //       });
        //    ConditionsBooklet conditionsBooklet = new ConditionsBooklet();
        //    _moqTenderQueries.Setup(m => m.FindConditionsBookletForRePurchase(It.IsAny<int>(), It.IsAny<string>()))
        //      .Returns(() =>
        //      {
        //          return Task.FromResult<ConditionsBooklet>(conditionsBooklet);
        //      });

        //    _IBlockAppService.Setup(m => m.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
        //        .Returns
        //        (() =>
        //        {
        //            return Task.FromResult<bool>(false);
        //        });

        //    _IIDMAppService.Setup(m => m.GetSupplierInfoByCR(It.IsAny<string>()))
        //      .Returns
        //      (() =>
        //      {
        //          return Task.FromResult<SupplierFullDataModel>(null);
        //      });

        //    await _sutTenderService.PurshaseTender(1, "123", "0546896578", "email");
        //    _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        //}

        [Fact]
        public async Task ShouldSendInvitationsForUnRegisteredSupplier()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            Tender tender = new TenderDefault().GetGeneralTenderWithPrequalification();


            _IIDMAppService.Setup(m => m.GetSupplierInfoByCR(It.IsAny<string>()))
                .Returns
                (() =>
                {
                    return Task.FromResult<SupplierFullDataModel>(null);
                });

            _IBlockAppService.Setup(m => m.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
                .Returns
                (() =>
                {
                    return Task.FromResult<bool>(false);
                });

            _moqTenderQueries.Setup(m => m.FindTenderInvitationsWithById(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            _moqTenderQueries.Setup(m => m.FindUnRegisteredSuppliersInvitationByTenderIDAndEmail(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
               .Returns(() =>
               {
                   return Task.FromResult<bool>(true);
               });

            UnRegisteredSuppliersInvitationsModel invitationsModel = new UnRegisteredSuppliersInvitationsModel
            {
                CrName = "Supplier name",
                CrNumber = "123",
                Email = "abc@mail.com",
                TenderIdString = Util.Encrypt(1),
                InvitationTypeId = 1,
                Mobile = "0542783945",
                Description = "Description"
            };

            //The input to be used in a unit test should be the simplest possible in order to verify the behavior that you are currently testing.
            await _sutTenderService.SendInvitationsForUnRegisteredSupplier(invitationsModel);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldSendInvitationsForUnRegisteredSupplier_ThrowException_IfSupplierBlocked()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            _IIDMAppService.Setup(m => m.GetSupplierInfoByCR(It.IsAny<string>()))
                .Returns
                (() =>
                {
                    return Task.FromResult<SupplierFullDataModel>(null);
                });
            _IBlockAppService.Setup(m => m.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
                .Returns
                (() =>
                {
                    return Task.FromResult<bool>(true);
                });

            UnRegisteredSuppliersInvitationsModel invitationsModel = new UnRegisteredSuppliersInvitationsModel
            {
                CrNumber = "123"
            };

            await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.SendInvitationsForUnRegisteredSupplier(invitationsModel));
        }

        [Fact]
        public async Task ShouldSendInvitationsForUnRegisteredSupplier_ThrowException()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User


            _IIDMAppService.Setup(m => m.GetSupplierInfoByCR(It.IsAny<string>()))
                .Returns
                (() =>
                {
                    return Task.FromResult<SupplierFullDataModel>(new SupplierFullDataModel());
                });

            UnRegisteredSuppliersInvitationsModel invitationsModel = new UnRegisteredSuppliersInvitationsModel
            {
                CrNumber = "123"
            };

            await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.SendInvitationsForUnRegisteredSupplier(invitationsModel));
        }


        [Fact]
        public async Task ShouldSubmitTenderInvitationsStep()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User 

            _moqTenderQueries.Setup(m => m.GetTenderByIdForInvitations(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
               });

            await _sutTenderService.SubmitTenderInvitationsStep(1);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldGetTenderCommitteesByTenderId()
        {
            _moqTenderQueries.Setup(m => m.GetTenderCommitteesByTenderId(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<EditeCommitteesModel>(new EditeCommitteesModel());
               });

            var result = await _sutTenderService.GetTenderCommitteesByTenderId(1, 1);

            Assert.NotNull(result);
            Assert.IsType<EditeCommitteesModel>(result);
        }


        [Fact]
        public async Task ShouldGetTenderCommitteesByTenderId_ThrowException_IfNull()
        {
            _moqTenderQueries.Setup(m => m.GetTenderCommitteesByTenderId(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<EditeCommitteesModel>(null);
               });

            await Assert.ThrowsAsync<UnHandledAccessException>(() => _sutTenderService.GetTenderCommitteesByTenderId(1, 1));

        }

        [Fact]
        public async Task ShouldGetTenderSamplesDeliveryAddress()
        {
            _moqTenderQueries.Setup(m => m.GetTenderByIdAndBranch(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new Tender());
               });

            var result = await _sutTenderService.GetTenderSamplesDeliveryAddress(1, 0);

            Assert.NotNull(result);
            Assert.IsType<Tender>(result);
        }

        [Fact]
        public async Task ShouldGetTenderSamplesDeliveryAddress_ThrowException_IfNull()
        {
            _moqTenderQueries.Setup(m => m.GetTenderByIdAndBranch(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(null);
               });

            await Assert.ThrowsAsync<UnHandledAccessException>(() => _sutTenderService.GetTenderSamplesDeliveryAddress(1, 1));

        }

        [Fact]
        public async Task ShouldGetTenderForEditAreas()
        {
            _moqTenderQueries.Setup(m => m.GetTenderWithAreas(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new Tender());
               });

            var result = await _sutTenderService.GetTenderForEditAreas(1, 1);

            Assert.NotNull(result);
            Assert.IsType<Tender>(result);
        }


        [Fact]
        public async Task ShouldGetTenderForEditAreas_ThrowException_IfNull()
        {
            _moqTenderQueries.Setup(m => m.GetTenderWithAreas(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(null);
               });

            await Assert.ThrowsAsync<UnHandledAccessException>(() => _sutTenderService.GetTenderForEditAreas(1, 1));

        }

        [Fact]
        public async Task ShouldConvertTenderInvitationToPublic()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User 

            Tender tender = new Tender();
            _moqTenderQueries.Setup(m => m.FindTenderByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new Tender());
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);

            var result = await _sutTenderService.ConvertTenderInvitationToPublic(1);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);

            Assert.NotNull(result);
            Assert.IsType<Tender>(result);
        }

        [Fact]
        public async Task ShouldEditCommittees()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User 


            EditeCommitteesModel committeemodel = new EditeCommitteesModel
            {
                OffersOpeningCommitteeId = 1,
                OffersCheckingCommitteeId = 1,
                TechnicalOrganizationId = 1
            };
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new Tender());
               });

            await _sutTenderService.EditCommittees(committeemodel);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldEditSamplesDeliveryAddress()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User 

            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new Tender());
               });

            await _sutTenderService.EditSamplesDeliveryAddress(1, "address");

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldEditCountries()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User 


            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.FindTenderWithAreasById(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);

            List<int> countriesIds = new List<int> { 1 };
            await _sutTenderService.EditCountries(1, countriesIds);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldEditCountries_ThrowException_IfStatusNotApproved()
        {


            _moqTenderQueries.Setup(m => m.FindTenderWithAreasById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new Tender());
              });

            List<int> countriesIds = new List<int> { 1 };
            await Assert.ThrowsAsync<UnHandledAccessException>(() => _sutTenderService.EditCountries(1, countriesIds));
        }

        [Fact]
        public async Task ShouldEditAreas()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User 


            Tender tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(m => m.FindTenderWithAreasById(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);

            TenderAreasModel areasModel = new TenderAreasModel
            {
                TenderAreaIDs = new List<int> { 1 },
            };
            await _sutTenderService.EditAreas(areasModel);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }



        #endregion



        [Theory]
        [InlineData("1")]
        public async Task CheckIfActivityCanHasTawreed_WithTawreedActivity_ReturnFalse(string activityIds)
        {
            #region Arrange
            ActivityVersionModel activityVersionModel = new ActivityVersionModel()
            {
                ActivityIdsString = activityIds,
                ActivityVersionId = 4,
            };
            _moqTenderQueries.Setup(t => t.GetTemplatesByActivityIdsAndVersionId(It.IsAny<List<int>>(), It.IsAny<int>()))
                 .Returns(() => Task.FromResult(new List<int>() { (int)Enums.ActivityTemplate.TawreedActivityVersion4 }));

            #endregion

            var result = await _sutTenderService.CheckIfActivityCanHasTawreed(activityVersionModel);

            Assert.False(result);
        }

        [Theory]
        [InlineData("14")]
        public async Task CheckIfActivityCanHasTawreed_WithConsultingServicesActivity_ReturnFalse(string activityIds)
        {
            #region Arrange
            ActivityVersionModel activityVersionModel = new ActivityVersionModel()
            {
                ActivityIdsString = activityIds,
                ActivityVersionId = 4,
            };
            _moqTenderQueries.Setup(t => t.GetTemplatesByActivityIdsAndVersionId(It.IsAny<List<int>>(), It.IsAny<int>()))
                     .Returns(() => Task.FromResult(new List<int>() { 12 }));


            #endregion

            var result = await _sutTenderService.CheckIfActivityCanHasTawreed(activityVersionModel);

            Assert.False(result);
        }

        [Theory]
        [InlineData("14")]
        public async Task CheckIfActivityCanHasTawreed_WithNotTawreedOrConsultingActivity_ReturnTrue(string activityIds)
        {
            #region Arrange
            ActivityVersionModel activityVersionModel = new ActivityVersionModel()
            {
                ActivityIdsString = activityIds,
                ActivityVersionId = 4,
            };
            _moqTenderQueries.Setup(t => t.GetTemplatesByActivityIdsAndVersionId(It.IsAny<List<int>>(), It.IsAny<int>()))
                .Returns(() => Task.FromResult(new List<int>() { 1 }));

            #endregion

            var result = await _sutTenderService.CheckIfActivityCanHasTawreed(activityVersionModel);

            Assert.True(result);
        }

        [Theory]
        [InlineData("9FqqzWC1*@@**FVAt Mcj3sYjw==", 4)]
        public async Task ShouldFindConditionTemplateReturnSucess(string tenderIdString, int branchId)
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            _moqTenderQueries.Setup(m => m.FindConditionTemplate(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<GetConditionTemplateModel>(new TenderDefault().GetConditionTemplateModelModelData());

               });

            var result = await _sutTenderService.FindConditionTemplate(tenderIdString, branchId);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task ShouldGetAwardingInformationForSupplier()
        {
            
            _moqTenderQueries.Setup(m => m.GetAwardingInformationForSupplier(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() =>
               {
                   return Task.FromResult<AwardingDetailsModel>(new AwardingDetailsModel());

               });

            var result = await _sutTenderService.GetAwardingInformationForSupplier(1, "111");
            Assert.NotNull(result);
            Assert.IsType<AwardingDetailsModel>(result);

        }


        [Theory]
        [InlineData("9FqqzWC1*@@**FVAt Mcj3sYjw==", 5)]
        public async Task ShouldFindConditionTemplate_ThrowBusinessRuleException_WhenBranchIdNotEquality(string tenderIdString, int branchId)
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            _moqTenderQueries.Setup(m => m.FindConditionTemplate(It.IsAny<int>(), It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<GetConditionTemplateModel>(new TenderDefault().GetConditionTemplateModelModelData());

             });
            await Assert.ThrowsAsync<BusinessRuleException>(() => _sutTenderService.FindConditionTemplate(tenderIdString, branchId));
        }

        [Fact]
        public async Task ShouldAddEditLocalContentTemplateSuccess()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            LocalContentModel localContentModel = new TenderDefault().GetLocalContentModelData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplates(Enums.TenderConditoinsStatus.LocalContent));
                });

            _moqTenderQueries.Setup(m => m.FindTenderForLocalContentByTenderId(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<List<Template>>(new TenderDefault().GetTemplateData());
              });
            _moqTenderQueries.Setup(m => m.GetCurrentTenderActivityVersion(It.IsAny<int>()))
          .Returns(() =>
          {
              return Task.FromResult<int>(5);
          });


            await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 4);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }
        [Fact]
        public async Task ShouldUpdateTenderDatesTest()
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
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            LocalContentModel localContentModel = new TenderDefault().GetLocalContentModelData();

            Tender tender = new TenderDefault().GetGeneralTender();
            TenderDates tenderDates = new TenderDates(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), DateTime.Now.AddDays(7), DateTime.Now.AddDays(7));
            TenderAddress address = new TenderAddress(false, "Address", "buliding", "floar", "department");
            tender.SetTenderAddress(address);
            tender.SetTenderDates(tenderDates);
            _moqTenderQueries.Setup(m => m.GetTenderWithDateAndAddressByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(tender);
                });
            TenderRelationsModel relationsModel = new TenderDefault().GetTenderRelationsDefaultData();

            _moqTenderQueries.Setup(m => m.FindTenderRelationsByTenderIdAfterUpdateDates(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<TenderRelationsModel>(relationsModel);
                });

            TenderDatesModel tenderDatesModel = new TenderDatesDefaults().GetTenderDatesWithOpenDate();

            await _sutTenderService.UpdateTenderDates(tenderDatesModel);
            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);

        }



        [Theory]
        [InlineData(1)]
        public async Task UpdateHasAlternative_WithRightConditions_UpdateOfferAlternativeValueToBeTrue(int tenderId)
        {
            var tender = new TenderDefault().GetGeneralTender();
            _moqTenderQueries.Setup(tender => tender.GetTenderWithFormQuantityTablesWithoutActivitiesAndChanges(It.IsAny<int>())).Returns(() => Task.FromResult(tender));

            await _sutTenderService.UpdateHasAlternative(tenderId, true);

            _moqTenderCommand.Verify(comm => comm.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task Should_GetInvitation()
        {
            _moqTenderQueries.Setup(q => q.GetInvitation(It.IsAny<List<string>>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<List<Invitation>>(new InvitationDefaults().GetInvitations()); });
            var result = await _sutTenderService.GetInvitation(It.IsAny<List<string>>(), It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetNewInvitationsByCRNo()
        {
            _moqTenderQueries.Setup(q => q.GetNewInvitationsByCRNo(It.IsAny<TenderSearchCriteria>()))
                .Returns(() => { return new InvitationDefaults().GetInvitations().ToQueryResult(); });
            var result = await _sutTenderService.GetNewInvitationsByCRNo(It.IsAny<TenderSearchCriteria>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetSupplierTenders()
        {
            _moqTenderQueries.Setup(q => q.GetSupplierTenders(It.IsAny<TenderSearchCriteria>()))
                .Returns(() => { return Task.FromResult<QueryResult<TenderInvitationDetailsModel>>(new InvitationDefaults().GetTenderInvitationDetailsModelQueryResult()); });
            var result = await _sutTenderService.GetSupplierTenders(It.IsAny<TenderSearchCriteria>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetAllTenders()
        {
            _moqTenderQueries.Setup(q => q.GetAllTenders(It.IsAny<string>()))
                .Returns(() => { return new TenderDefault().GetGenerlTenders().ToQueryResult(); });
            var result = await _sutTenderService.GetAllTenders(It.IsAny<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderByAgencyCode()
        {
            _moqTenderQueries.Setup(q => q.FindTenderByAgencyCode(It.IsAny<string>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            var result = await _sutTenderService.FindTenderByAgencyCode(It.IsAny<string>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderDetailsByTenderId()
        {
            _moqTenderQueries.Setup(q => q.FindTenderDetailsByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            var result = await _sutTenderService.FindTenderDetailsByTenderId(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderByIdToApplyOffer()
        {
            _moqTenderQueries.Setup(q => q.FindTenderByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            var result = await _sutTenderService.GetTenderByIdToApplyOffer(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderOfferDetailsByTenderId()
        {
            _moqTenderQueries.Setup(q => q.FindTenderOfferDetailsByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            var result = await _sutTenderService.FindTenderOfferDetailsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindRelationsDetailsByTenderId()
        {
            TenderRelationsModel relationsModel = new TenderDefault().GetTenderRelationsDefaultData();

            _moqTenderQueries.Setup(q => q.FindRelationsDetailsByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<TenderRelationsModel>(relationsModel);
                });
            var result = await _sutTenderService.FindRelationsDetailsByTenderId(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_GetTenderLocalContenetValuesByTenderId()
        {
            TenderLocalContentValuesViewModel tenderLocalContent = new TenderLocalContentValuesViewModel();

            _moqTenderQueries.Setup(q => q.GetTenderLocalContenetValuesByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<TenderLocalContentValuesViewModel>(tenderLocalContent);
                });
            var result = await _sutTenderService.GetTenderLocalContenetValuesByTenderId(1);
            Assert.NotNull(result);
            Assert.IsType<TenderLocalContentValuesViewModel>(result);

        }

        [Fact]
        public async Task Should_GetLocalContentDetailsForSupplierByTenderId()
        {
            LocalContentDetailsViewModel tenderLocalContent = new LocalContentDetailsViewModel();

            _moqTenderQueries.Setup(q => q.GetLocalContentDetailsForSupplierByTenderId(It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<LocalContentDetailsViewModel>(tenderLocalContent);
                });
            var result = await _sutTenderService.GetLocalContentDetailsForSupplierByTenderId(1);
            Assert.NotNull(result);
            Assert.IsType<LocalContentDetailsViewModel>(result);
        }

        [Fact]
        public async Task Should_FindTenderAttachmentsById()
        {
            _moqTenderQueries.Setup(q => q.FindTenderAttachmentsById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            var result = await _sutTenderService.FindTenderAttachmentsById(It.IsAny<int>(), It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_FindTenderByInvitationId()
        {
            _moqTenderQueries.Setup(q => q.FindTenderByInvitationId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            var result = await _sutTenderService.FindTenderByInvitationId(It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_ApproveTendeFinancialOpeningAsync()
        {
            MoqUser();

            _moqVerificationService.Setup(v => v.CheckForVerificationCode(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Verifiable();
            _moqTenderQueries.Setup(t => t.FindTenderToApproveFinancialOpening(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToApproveOrRejectTenderFinancialOpening(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();
            _moqTenderQueries.Setup(t => t.GetPendingCancelChangeRequest(It.IsAny<int>(), It.IsAny<string>()))
                 .Returns(() => { return Task.FromResult<TenderChangeRequest>(new ChangeRequestDefault().GetCancelChangeRequestData()); });
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqTenderQueries.Setup(t => t.GetOffersByTenderIdAsync(It.IsAny<int>()))
                 .Returns(() => { return new OfferDefaults().GetOfferList().ToQueryResult(); });
            _NotificationService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForSuppliers(It.IsAny<int>(), It.IsAny<List<string>>(), It.IsAny<MainNotificationTemplateModel>(), null))
                .Verifiable();

            await _sutTenderService.ApproveTendeFinancialOpeningAsync(It.IsAny<int>(), It.IsAny<string>());
            _moqCommandRepository.Verify(g => g.Update(It.IsAny<Tender>()), Times.Once);
            _moqCommandRepository.Verify(g => g.SaveAsync());

        }

        [Fact]
        public async Task Should_RejectTendeFinancialOpeningAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(t => t.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToApproveOrRejectTenderFinancialOpening(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();
            _NotificationService.Setup(o => o.SendNotificationForCommitteeUsers(It.IsAny<int>(), null, It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sutTenderService.RejectTendeFinancialOpeningAsync(It.IsAny<int>(), It.IsAny<string>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_ReOpenTendeFinancialOpeningAsync()
        {
            MoqUser();

            _moqTenderQueries.Setup(t => t.FindTenderWithStatusAndOffersByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToReOpenTenderFinancialOpening(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();

            await _sutTenderService.ReOpenTendeFinancialOpeningAsync(It.IsAny<int>());
            _moqTenderCommand.Verify(g => g.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task Should_FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage()
        {
            TenderOffersModel tenderOffersModel = new TenderOffersModel()
            {
                IsLowBudgetDirectPurchase = true,
                DirectPurchaseMemberId = 1
            };
            _moqTenderQueries.Setup(q => q.FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderOffersModel>(tenderOffersModel); });
            _moqTenderDomainService.Setup(d => d.CheckIfUserCanAccessLowBudgetTender(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>()))
                .Verifiable();

            var result = await _sutTenderService.FindDirectPurchaseTenderOfferDetailsByTenderIdForCheckStage(It.IsAny<int>(), It.IsAny<int>());
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_UpdateTenderExtendDates()
        {
            TenderChangeRequest tenderChangeRequest = new ChangeRequestDefault().GetCancelChangeRequestWithTender();
            TenderDatesModel tenderDatesModel = new TenderDatesModel() { TenderId = 1 };

            _moqTenderQueries.Setup(q => q.GetExtendDateChangeRequest(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderChangeRequest>(tenderChangeRequest); });
            _moqTenderQueries.Setup(q => q.FindTenderByTenderId(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToUpdateTender(It.IsAny<Tender>(), It.IsAny<string>()))
                .Verifiable();
            _moqTenderDomainService.Setup(d => d.IsValidToUpdateExtendDate(It.IsAny<TenderDatesModel>(), It.IsAny<Tender>()))
                .Verifiable();

            await _sutTenderService.UpdateTenderExtendDates(tenderDatesModel, It.IsAny<string>(), It.IsAny<string>());
            _moqTenderCommand.Verify(c => c.CreateTenderChangeRequestAsync(It.IsAny<TenderChangeRequest>()), Times.Once);
        }

        [Fact]
        public async Task Should_SendUpdateDatesToApprove()
        {
            MoqUser();
            TenderChangeRequest tenderChangeRequest = new ChangeRequestDefault().GetCancelChangeRequestWithTender();
            tenderChangeRequest.AddRevisionDates(DateTime.Now, DateTime.Now, "10:00", DateTime.Now, "10:00", DateTime.Now, "10:00", 1);

            _moqTenderQueries.Setup(q => q.GetExtendDateChangeRequest(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderChangeRequest>(tenderChangeRequest); });
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();

            await _sutTenderService.SendUpdateDatesToApprove(It.IsAny<int>());
            _moqTenderCommand.Verify(c => c.UpdateTenderChangeRequestAsync(It.IsAny<TenderChangeRequest>()), Times.Once);
        }

        [Fact]
        public async Task Should_CancelTenderExtendDateAsync()
        {
            MoqUser();
            TenderChangeRequest tenderChangeRequest = new ChangeRequestDefault().GetCancelChangeRequestWithTender();
            tenderChangeRequest.AddRevisionDates(DateTime.Now, DateTime.Now, "10:00", DateTime.Now, "10:00", DateTime.Now, "10:00", 1);

            _moqTenderQueries.Setup(q => q.GetExtendDateChangeRequestForCancel(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<TenderChangeRequest>(tenderChangeRequest); });

            await _sutTenderService.CancelTenderExtendDateAsync(It.IsAny<int>());
            _moqTenderCommand.Verify(c => c.UpdateTenderChangeRequestAsync(It.IsAny<TenderChangeRequest>()), Times.Once);
        }

        [Fact]
        public async Task Should_StartTenderCheckingOffers()
        {
            MoqUser();

            _moqTenderQueries.Setup(q => q.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender()); });
            _moqTenderDomainService.Setup(d => d.IsValidToStartCheckingTender(It.IsAny<Tender>()))
                .Verifiable();
            _moqTenderQueries.Setup(q => q.GetExtendDateChangeRequestForCancel(It.IsAny<int>()))
               .Returns(() => { return Task.FromResult<TenderChangeRequest>(new ChangeRequestDefault().GetCancelChangeRequestWithTender()); });
            await _sutTenderService.CancelTenderExtendDateAsync(It.IsAny<int>());
            _moqTenderCommand.Verify(c => c.UpdateTenderChangeRequestAsync(It.IsAny<TenderChangeRequest>()), Times.Once);
        }

        [Fact]
        public async Task Should_RejectTenderForUpdateQuantityTable()
        {
            MoqUser();
            Tender tender = new TenderDefault().GetGeneralTender();
            tender.SetReferenceNumber("111");
            tender.AddChangeRequestForUnitTest(new ChangeRequestDefault().GetQTChangeRequestDataList());
            tender.AddActionHistory(1, Enums.TenderActions.RejecToq, "a", 1);

            _moqTenderQueries.Setup(q => q.FindTenderQuantityTablesById(It.IsAny<int>()))
                .Returns(() => { return Task.FromResult<Tender>(tender); });
            _NotificationService.Setup(o => o.SendNotificationForBranchUsers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<MainNotificationTemplateModel>()))
                .Verifiable();
            _moqTenderCommand.Setup(x => x.UpdateAsync(tender)).Returns(() => Task.FromResult(tender));

            await _sutTenderService.RejectTenderForUpdateQuantityTable(It.IsAny<int>(), It.IsAny<string>());
            _moqTenderCommand.Verify(c => c.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }



        #region hamed - plz don't change anything
        [Fact]
        public async Task ShouldCreatLocalContentToConditionsTemplateSuccess()
        {
            MoqUser();
            LocalContentModel localContentModel = new TenderDefault().GetLocalContentModelDefaultData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                });
            _moqTenderQueries.Setup(m => m.FindTenderForLocalContentByTenderId(It.IsAny<int>(), It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<List<Template>>(new List<Template>());
             });
            _moqTenderQueries.Setup(m => m.GetCurrentTenderActivityVersion(It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<int>(4);
             });
            await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldCreatLocalContentThrowException_When_TenderIsNull()
        {
            MoqUser();
            LocalContentModel localContentModel = new TenderDefault().GetLocalContentModelDefaultData();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(null);
                });
            _moqTenderQueries.Setup(m => m.FindTenderForLocalContentByTenderId(It.IsAny<int>(), It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<List<Template>>(new List<Template>());
             });
            await Assert.ThrowsAsync<UnHandledAccessException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        [Fact]
        public async Task ShouldCreatLocalContentToConditionsTemplateThrowException_When_LocalContentMechanismIDs_NotDetemine()
        {
            MoqUser();
            LocalContentModel localContentModel = new LocalContentModel()
            {
                IsApplyProvisionsMandatoryList = true,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        [Fact]
        public async Task ShouldCreatLocalContentThrowException_When_LocalContentMechanismIsMinimumBaselineAndLocalContentConditionsWeight()
        {
            MoqUser();
            LocalContentModel localContentModel = new LocalContentModel()
            {
                IsApplyProvisionsMandatoryList = true,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
                LocalContentMechanismIDs = new List<int>() { 1, 2 }
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        [Fact]
        public async Task ShouldCreatLocalContentThrowException_When_TenderMoreThan50Million_And_ContainsTawreedTablesIsFalse()
        {

            MoqUser();
            _moqTenderQueries.Setup(m => m.FindTenderWithConditionsTemplateById(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new TenderDefault().GetGeneralTenderForConditoinTemplatesTenderMoreThan50Million());
                });
            _moqTenderQueries.Setup(m => m.FindTenderForLocalContentByTenderId(It.IsAny<int>(), It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<List<Template>>(new List<Template>());
             });
            _moqTenderQueries.Setup(m => m.GetCurrentTenderActivityVersion(It.IsAny<int>()))
             .Returns(() =>
             {
                 return Task.FromResult<int>(4);
             });
            LocalContentModel localContentModel = new LocalContentModel()
            {
                IsApplyProvisionsMandatoryList = true,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
                LocalContentMechanismIDs = new List<int>() { 3 }
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        // الحد الأدني لخط الأساس
        [Theory]
        [InlineData(200)]
        [InlineData(-1)]
        public async Task ShouldCreatLocalContentThrowException_When_MinimumBaselineGreaterThan100OrLessThanZero(int minimumBaseline)
        {
            MoqUser();
            LocalContentModel localContentModel = new LocalContentModel()
            {
                IsApplyProvisionsMandatoryList = true,
                MinimumBaseline = minimumBaseline,
                MinimumPercentageLocalContentTarget = 10,
                LocalContentMechanismIDs = new List<int>() { 1 }
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        //الحد الادنى لنسبة المحتوى المحلي المستهدفة
        [Theory]
        [InlineData(200)]
        [InlineData(-1)]
        public async Task ShouldCreatLocalContentThrowException_When_MinimumPercentageLocalContentTargetGreaterThan100OrLessThanZero(int minimumPercentageLocalContentTarget)
        {
            MoqUser();
            LocalContentModel localContentModel = new LocalContentModel()
            {
                IsApplyProvisionsMandatoryList = true,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = minimumPercentageLocalContentTarget,
                LocalContentMechanismIDs = new List<int>() { 2 }
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        // ارفاق ملف دراسة الحد الأدنى للمحتوى المحلي المستهدف
        [Fact]
        public async Task ShouldCreatLocalContentThrowException_When_StudyMinimumTargetFileNetReferenceIdIsNull()
        {
            MoqUser();
            LocalContentModel localContentModel = new LocalContentModel()
            {
                IsApplyProvisionsMandatoryList = true,
                MinimumBaseline = 10,
                MinimumPercentageLocalContentTarget = 10,
                LocalContentMechanismIDs = new List<int>() { 2 }
            };
            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.AddEditLocalContentTemplate(localContentModel, 1));
        }

        #endregion


        [Fact]
        public async Task ShouldSendRequestToApplayForTender()
        {

            MoqUser();
            _moqTenderQueries.Setup(m => m.FindTenderInvitationByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
               });

            _IBlockAppService.Setup(m => m.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
               .Returns
               (() =>
               {
                   return Task.FromResult<bool>(false);
               });

            await _sutTenderService.SendRequestToApplayForTender(1, "123");
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }
        [Fact]
        public async Task ShouldSendRequestToApplayForVROTender()
        {

            MoqUser();
            _moqTenderQueries.Setup(m => m.FindTenderInvitationByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(new TenderDefault().GetVROTenderData());
               });

            _IBlockAppService.Setup(m => m.CheckifSupplierBlockedByCrNo(It.IsAny<string>(), It.IsAny<string>()))
               .Returns
               (() =>
               {
                   return Task.FromResult<bool>(false);
               });

            await _sutTenderService.SendRequestToApplayForTender(1, "123");
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        #region VRO
        [Fact]
        public async Task ShouldStartVROTenderOffersCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.Approved);

            await _sutTenderService.StartVROTenderOffersCheckingAsync(Util.Encrypt(TENDER_ID));
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldStartVROTenderOffersCheckingThrowExceptinIfStatusNotValid()
        {

            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.StartVROTenderOffersCheckingAsync(Util.Encrypt(TENDER_ID)));
        }


        [Fact]
        public async Task ShouldSendVROTenderOffersTechnicalCheckingToApproveAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersCheckingAndTechnicalEval);

            await _sutTenderService.SendVROTenderOffersTechnicalCheckingToApproveAsync(Util.Encrypt(TENDER_ID));
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }


        [Fact]
        public async Task ShouldSendVROTenderOffersTechnicalCheckingToApproveThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.SendVROTenderOffersTechnicalCheckingToApproveAsync(Util.Encrypt(TENDER_ID)));
        }

        [Fact]
        public async Task ShouldReopenVROTenderOffersTechnicalCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingRejected);

            await _sutTenderService.ReopenVROTenderOffersTechnicalCheckingAsync(Util.Encrypt(TENDER_ID));
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }


        [Fact]
        public async Task ShouldReopenVROTenderOffersTechnicalCheckingAsyncThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ReopenVROTenderOffersTechnicalCheckingAsync(Util.Encrypt(TENDER_ID)));
        }

        [Fact]
        public async Task ShouldApproveVROTenderOffersTechnicalCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            _moqTenderQueries.Setup(m => m.GetPendingCancelChangeRequest(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() =>
               {
                   return Task.FromResult<TenderChangeRequest>(new ChangeRequestDefault().GetCancelChangeRequestDataVRO());
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingPending);

            await _sutTenderService.ApproveVROTenderOffersTechnicalCheckingAsync(Util.Encrypt(TENDER_ID));

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);

        }


        [Fact]
        public async Task ShouldApproveVROTenderOffersTechnicalCheckingThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ApproveVROTenderOffersTechnicalCheckingAsync(Util.Encrypt(TENDER_ID)));
        }

        [Fact]
        public async Task ShouldRejectVROTenderOffersTechnicalCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingPending);

            await _sutTenderService.RejectVROTenderOffersTechnicalCheckingAsync(Util.Encrypt(TENDER_ID), REJECTION_REASON);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);

        }


        [Fact]
        public async Task ShouldRejectVROTenderOffersTechnicalCheckingAsyncThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.RejectVROTenderOffersTechnicalCheckingAsync(Util.Encrypt(TENDER_ID), REJECTION_REASON));
        }



        [Fact]
        public async Task ShouldStartVROTenderOffersFinancialCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersTechnicalCheckingApproved);

            await _sutTenderService.StartVROTenderOffersFinancialCheckingAsync(Util.Encrypt(TENDER_ID), 100);
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }


        [Fact]
        public async Task ShouldStartVROTenderOffersFinancialCheckingAsyncThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.StartVROTenderOffersFinancialCheckingAsync(Util.Encrypt(TENDER_ID), 100));
        }


        [Fact]
        public async Task ShouldSendVROTenderOffersFinanceCheckingToApproveAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialChecking);

            await _sutTenderService.SendVROTenderOffersFinanceCheckingToApproveAsync(Util.Encrypt(TENDER_ID));
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }


        [Fact]
        public async Task ShouldSendVROTenderOffersFinanceCheckingToApproveAsyncThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.SendVROTenderOffersFinanceCheckingToApproveAsync(Util.Encrypt(TENDER_ID)));
        }

        [Fact]
        public async Task ShouldReopenVROTenderOffersFinancialCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialCheckingRejected);

            await _sutTenderService.ReopenVROTenderOffersFinancialCheckingAsync(Util.Encrypt(TENDER_ID));
            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }


        [Fact]
        public async Task ShouldReopenVROTenderOffersFinancialCheckingThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ReopenVROTenderOffersFinancialCheckingAsync(Util.Encrypt(TENDER_ID)));
        }

        [Fact]
        public async Task ShouldApproveVROTenderOffersFinanceCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            _moqTenderQueries.Setup(m => m.GetPendingCancelChangeRequest(It.IsAny<int>(), It.IsAny<string>()))
               .Returns(() =>
               {
                   return Task.FromResult<TenderChangeRequest>(new ChangeRequestDefault().GetCancelChangeRequestDataVRO());
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialCheckingPending);

            await _sutTenderService.ApproveVROTenderOffersFinanceCheckingAsync(Util.Encrypt(TENDER_ID));

            _moqCommandRepository.Verify(m => m.Update(It.IsAny<Tender>()), Times.Once);

        }


        [Fact]
        public async Task ShouldApproveVROTenderOffersFinanceCheckingThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.GetTenderByIdWithoutAnyIncluds(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.ApproveVROTenderOffersFinanceCheckingAsync(Util.Encrypt(TENDER_ID)));
        }


        [Fact]
        public async Task ShouldRejectVROTenderOffersFinanceCheckingAsync()
        {

            MoqUser();
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            tender.UpdateTenderStatus(Enums.TenderStatus.VROOffersFinancialCheckingPending);

            await _sutTenderService.RejectVROTenderOffersFinanceCheckingAsync(Util.Encrypt(TENDER_ID), REJECTION_REASON);

            _moqTenderCommand.Verify(m => m.UpdateAsync(It.IsAny<Tender>()), Times.Once);

        }


        [Fact]
        public async Task ShouldRejectVROTenderOffersFinanceCheckingThrowExceptinIfStatusNotValid()
        {
            Tender tender = new TenderDefault().GetVROTenderData();
            _moqTenderQueries.Setup(m => m.FindTenderForOpenCheckStageByTenderId(It.IsAny<int>()))
               .Returns(() =>
               {
                   return Task.FromResult<Tender>(tender);
               });

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sutTenderService.RejectVROTenderOffersFinanceCheckingAsync(Util.Encrypt(TENDER_ID), REJECTION_REASON));
        }

        #endregion

        private void MoqUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var usernum = new Claim(IdentityConfigs.Claims.SelectedCR, "1010000154");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(usernum);
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() => { return idintity; });
            _httpContext.SetupGet(x => x.HttpContext).Returns(context.Object);
        }
    }
}
