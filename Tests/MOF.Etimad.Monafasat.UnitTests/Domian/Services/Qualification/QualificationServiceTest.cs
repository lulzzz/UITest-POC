using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Interfaces;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.TestsBuilders;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Services.Qualification
{
    public class QualificationServiceTest
    {
        private readonly Mock<IQualificationAppService> _moqQualificationAppService;
        private readonly Mock<IQualificationQueries> _moqQualificationQueries;

        private readonly Mock<INotificationAppService> _NotificationService;
        private readonly Mock<IFileNetService> _moqFileNetService;
        private readonly Mock<IQualificationCommands> _moqQualificationCommands;
        private readonly Mock<IMapper> _moqMapper;
        private readonly Mock<IQualificationDomainService> _moqQualificationDomainService;
        private readonly Mock<IHttpContextAccessor> _moqHttpContextAccessor;
        private readonly Mock<IConfigurationRoot> _moqConfigurationRoot;
        private readonly Mock<IGenericCommandRepository> _moqGenericCommandRepository;
        private readonly Mock<IBlockAppService> _moqBlockAppService;
        private readonly Mock<ITenderAppService> _moqTenderAppService;
        private readonly Mock<ITenderQueries> _moqTenderQuereisService;
        private readonly Mock<ITenderCommands> _moqTenderCommandsService;
        private readonly Mock<ISupplierQualificationDocumentDomainService> _moqSupplierQualificationDocumentDomainService;
        private readonly QualificationAppService _sut;
        private readonly Mock<IVerificationService> _Verification;

        public QualificationServiceTest()
        {

            //Mock
            _moqQualificationAppService = new Mock<IQualificationAppService>();
            _moqQualificationQueries = new Mock<IQualificationQueries>();
            _moqFileNetService = new Mock<IFileNetService>();
            _NotificationService = new Mock<INotificationAppService>();
            _Verification = new Mock<IVerificationService>();
            _moqQualificationCommands = new Mock<IQualificationCommands>();
            _moqMapper = new Mock<IMapper>();
            _moqQualificationDomainService = new Mock<IQualificationDomainService>();
            _moqHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _moqConfigurationRoot = new Mock<IConfigurationRoot>();
            _moqGenericCommandRepository = new Mock<IGenericCommandRepository>();
            _moqBlockAppService = new Mock<IBlockAppService>();
            _moqTenderAppService = new Mock<ITenderAppService>();
            _moqTenderQuereisService = new Mock<ITenderQueries>();
            _moqTenderCommandsService = new Mock<ITenderCommands>();
            _moqSupplierQualificationDocumentDomainService = new Mock<ISupplierQualificationDocumentDomainService>();


            //Arrange

            _sut = new QualificationAppService(_NotificationService.Object,
                    _moqQualificationQueries.Object,
                    _moqQualificationCommands.Object,
                    _Verification.Object,
                    _moqQualificationDomainService.Object,
                    _moqMapper.Object,
                    _moqHttpContextAccessor.Object,
                    _moqGenericCommandRepository.Object,
                    _moqBlockAppService.Object,
                    //configBuilder.Build(),
                    _moqTenderAppService.Object,
                    _moqTenderQuereisService.Object,
                    _moqTenderCommandsService.Object,
                    _moqSupplierQualificationDocumentDomainService.Object
                    );

        }

        [Fact]
        public async Task ShouldGetPreQualificationDetailsByIdForCheckingAsync()
        {
            _moqQualificationQueries.Setup(x => x.GetPreQualificationDetailsByIdForChecking(It.IsAny<int>()))
                             .Returns((int x) =>
                             {
                                 return Task.FromResult<PreQualificationBasicDetailsModel>(new PreQualificationBasicDetailsModel());
                             });

            _moqQualificationQueries.Setup(x => x.GetQualificationByIdWithChangRequest(It.IsAny<int>()))
                             .Returns((int x) =>
                             {
                                 return Task.FromResult(new QualificationDefault().GetPreQualification());
                             });
            var result = await _sut.GetPreQualificationDetailsByIdForChecking(4);

            Assert.NotNull(result);
            Assert.IsType<PreQualificationBasicDetailsModel>(result);
        }

        [Fact]
        public async Task GetPreQualificationDetails_ByQualificationId_ReturnesQualificationCheckingModel()
        {

            _moqQualificationQueries.Setup(x =>
                    x.GetSupplierPreQualificationRequestByQualificationIdAsync(It.IsAny<QualificationIdWithSearchCriteria>()))
                .Returns(() => Task.FromResult(
                    new QueryResult<PreQualificationResultForChecking>(
                        new List<PreQualificationResultForChecking>(), 10, 1, 10)));
            QualificationIdWithSearchCriteria qualificationIdWithSearchCriteria =
                new QualificationIdWithSearchCriteria();

            var result =
                await _sut.GetSupplierPreQualificationRequestByQualificationIdAsync(qualificationIdWithSearchCriteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<PreQualificationResultForChecking>>(result);
        }


        [Fact]
        public async Task ShouldRejectCheckPreQualificationOffersAsync()
        {
            MockUser();
            _moqQualificationQueries.Setup(x => x.GetQualificationById(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                        });

            await _sut.RejectCheckPreQualificationOffers(568, "Rejection Reason");
            //====== Assert=====
            _moqQualificationCommands.Verify(x => x.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }


        [Fact]
        public async Task ShouldCreateVerificationCodeAsync()
        {


            //Mock
            _moqQualificationCommands.Setup(x => x.UpdateAsync(It.IsAny<Tender>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<Tender>(new Tender());
                             });
            _moqQualificationQueries.Setup(x => x.FindTenderById(It.IsAny<int>(), It.IsAny<int>()))
                  .Returns(() =>
                  {
                      return Task.FromResult<Tender>(new Tender());
                  });
            //============Return Result===========
            //await _sut.CreateVerificationCode(568, "", "", 1,1);
            //====== Assert=====

        }

        [Fact]
        public async Task ShouldSendPreQualificationToApproveCheckingAsync()
        {
            MockUser();
            _moqQualificationQueries.Setup(x => x.GetQualificationById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                             });
            _moqQualificationQueries.Setup(x => x.GetCheckingCommitteeMembersOnTender(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult(new List<CommitteeUser> { new CommitteeUser(1, 15110, 0) });
              });

            _moqQualificationCommands.Setup(x => x.UpdateAsync(It.IsAny<Tender>()))
                .Returns(() =>
                {
                    return Task.FromResult<Tender>(new Tender());
                });

            _moqQualificationDomainService.Setup(x => x.IsValidToSendTenderToApproveChecking(It.IsAny<Tender>(), new List<string>()));


            //============Return Result===========
            await _sut.SendPreQualificationToApproveChecking(568, "022001000000");
            //====== Assert=====
            _moqQualificationCommands.Verify(x => x.UpdateAsync(It.IsAny<Tender>()));
        }

        [Fact]
        public async Task ShouldReopenPreQualificationCheckingAsync()
        {
            MockUser();
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
                             });

            _moqQualificationDomainService.Setup(x => x.IsValidToReopenCheckTender(It.IsAny<Tender>()));


            _moqQualificationCommands.Setup(x => x.UpdateAsync(It.IsAny<Tender>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<Tender>(new Tender());
                 });

            await _sut.ReopenPreQualificationChecking(568, "022001000000");
            //====== Assert=====
            _moqQualificationCommands.Verify(x => x.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Fact]
        public async Task ShouldStartPreQualificationCheckingOffersAsync()
        {

            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);

            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });

            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            //Mock
            _moqQualificationQueries.Setup(x => x.GetQualificationById(It.IsAny<int>()))
                             .Returns(() =>
                             {
                                 return Task.FromResult<Tender>(new Tender());
                             });

            _moqQualificationDomainService.Setup(x => x.IsValidToStartCheckingTender(It.IsAny<Tender>()));


            _moqQualificationCommands.Setup(x => x.UpdateAsync(It.IsAny<Tender>()))
                 .Returns(() =>
                 {
                     return Task.FromResult<Tender>(new Tender());
                 });

            //============Return Result===========
            await _sut.StartPreQualificationCheckingOffersAsync(568);
            //====== Assert=====

        }


        [Theory]
        [InlineData(568, "1")]
        public async Task ShouldAddTenderToSupplierTendersFavouriteList(int tenderId, string cr)
        {
            var tender = new TenderDefault().GetGeneralTender();
            FavouriteSupplierTender favouriteSupplierTender = new FavouriteSupplierTender(tenderId, cr);
            _moqQualificationQueries.Setup(x => x.GetPreQualificationDetailsById(tenderId))
                .Returns(() =>
                {
                    return Task.FromResult(tender);
                });
            _moqQualificationCommands.Setup(c => c.CreateTenderFavouriteAsync(favouriteSupplierTender)).Returns(() =>
            {
                return Task.FromResult(favouriteSupplierTender);
            });



            var result = await _sut.AddTenderToSupplierTendersFavouriteList(tenderId, cr);


            //_moqQualificationCommands.Verify(x => x.CreateTenderFavouriteAsync(favouriteSupplierTender), Times.Once);
            Assert.IsType<bool>(result);

        }

        [Theory]
        [InlineData(1, "3232", "022001000000")]
        public async Task ShouldApproveQualification(int tenderId, string verificationCode, string agencyCode)
        {
            #region Arrange
            var typeId = (int)Enums.VerificationType.Tender;
            MockUser();
            _moqQualificationQueries.Setup(m => m.GetQualificationById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });
            #endregion Arrange

            await _sut.ApprovePreQualification(tenderId, verificationCode, typeId, agencyCode);

            _moqQualificationCommands.Verify(m => m.UpdatePreQualificationAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "3232", "0220010000300")]
        public async Task ShouldReturnAuthorizationExceptionWhenNotQualificationAgency(int tenderId, string verificationCode, string agencyCode)
        {
            #region Arrange
            var typeId = (int)Enums.VerificationType.Tender;
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
            _moqQualificationQueries.Setup(m => m.GetQualificationById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });
            #endregion Arrange

            await Assert.ThrowsAsync<AuthorizationException>(async () => await _sut.ApprovePreQualification(tenderId, verificationCode, typeId, agencyCode));
        }

        [Theory]
        [InlineData(1, "3232", "022001000000")]
        public async Task ShouldApprovePreQualificationFromCommitteeManager(int tenderId, string verificationCode, string agencyCode)
        {
            #region Arrange
            var typeId = (int)Enums.VerificationType.Tender;
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
            _moqQualificationQueries.Setup(m => m.GetQualificationById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });
            #endregion Arrange

            await _sut.ApprovePreQualificationFromCommitteeManager(tenderId, verificationCode, typeId, agencyCode);

            _moqQualificationCommands.Verify(m => m.UpdatePreQualificationAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "No Reason", "022001000000")]
        public async Task ShouldRejecteQualificationApproval(int tenderId, string rejectionReason, string agencyCode)
        {
            #region Arrange
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
            _moqQualificationQueries.Setup(m => m.GetQualificationById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });
            #endregion Arrange

            await _sut.RejectApprovePreQualification(tenderId, rejectionReason, agencyCode);

            _moqQualificationCommands.Verify(m => m.UpdatePreQualificationAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "No Reason", "022001000000")]
        public async Task ShouldRejectApprovePreQualificationFromCommitteeManager(int tenderId, string rejectionReason, string agencyCode)
        {
            #region Arrange
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
            _moqQualificationQueries.Setup(m => m.GetQualificationById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetTenderWithStatus(Enums.TenderStatus.PendingQualificationCommitteeManagerApproval));
              });

            #endregion Arrange

            await _sut.RejectApprovePreQualificationFromCommitteeManager(tenderId, rejectionReason, agencyCode);
        }

        [Theory]
        [InlineData(1, "No Reason", "022001000000")]
        public async Task ShouldReturnBusinssRuleExIfQualStatusNotPendingManagerApproval(int tenderId, string rejectionReason, string agencyCode)
        {
            #region Arrange
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
            _moqQualificationQueries.Setup(m => m.GetQualificationById(It.IsAny<int>()))
              .Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });
            #endregion Arrange

            await Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.RejectApprovePreQualificationFromCommitteeManager(tenderId, rejectionReason, agencyCode));
        }

        [Fact]
        public async Task ShouldCreatePostQulaification()
        {
            #region Arrange
            var userId = 1;
            var agencyCode = "32323";
            var postQualificationModel = new PostQualificationApplyDocumentsModel()
            {
                CommercialNumbers = new List<string>() { "4343", "1010" },
                PostQualificationTenderId = 2,
                postQualificationIdString = null,
                TenderName = "GGg",
                TenderId = 323,
                LastEnqueriesDate = DateTime.Now,
                LastOfferPresentationDate = DateTime.Now,
                OffersCheckingDate = DateTime.Now,
                branchId = 2,
                TenderActivitieIDs = new List<int>() { 1, 2 },
                TenderAreaIDs = new List<int>() { 1, 2 },

                Attachments = new List<TenderAttachmentModel>(),
            };
            _moqQualificationQueries.Setup(m => m.GetPostQualificationById(It.IsAny<int>()))
            .Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });
            #endregion Arrange

            await _sut.CreatePoatQualification(postQualificationModel, userId, agencyCode);

            _moqGenericCommandRepository.Verify(m => m.SaveAsync(), Times.Once);
        }

        [Theory]
        [InlineData(1, "022001000000")]
        public async Task AcceptQualificationFromCommitteeSecretary(int tenderId, string agencyCode)
        {
            // Arrange
            _moqQualificationQueries.Setup(s => s.GetQualificationByIdWithChangRequest(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });
            MockUser();
            // Act
            await _sut.ApproveQualificationFromQualificationSecritary(tenderId, agencyCode);

            // Assert
            _moqQualificationCommands.Verify(q => q.UpdatePreQualificationAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(7, "022001000000")]
        public async Task ShouldSendPreQualificationToApprove(int tenderId, string agencyCode)
        {
            MockUser();
            _moqQualificationQueries.Setup(x => x.GetQualificationById(It.IsAny<int>()))
                .Returns((int x) =>
                {
                    return Task.FromResult(new QualificationDefault().GetPreQualification());
                });

            await _sut.SendPreQualificationToApprove(tenderId, agencyCode);

            _moqQualificationCommands.Verify(x => x.UpdatePreQualificationAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(7, "4040", 1, "022001000000")]
        public async Task ShouldApprovePreQualificationChecking(int tenderId, string verificarionCode, int typeId, string agencyCode)
        {
            MockUser();
            _moqQualificationQueries.Setup(x => x.GetQualificationByIdWithChangRequest(It.IsAny<int>()))
                .Returns((int x) =>
                {
                    return Task.FromResult(new QualificationDefault().GetPreQualification());
                });

            _moqQualificationQueries.Setup(x => x.GetSupplierResultForQualification(It.IsAny<int>()))
                          .Returns(() => new List<QualificationFinalResult>() { new QualificationDefault().GetQualificationFinalResult() });

            await _sut.ApprovePreQualificationChecking(tenderId, verificarionCode, typeId, agencyCode, new List<string>());

            _moqQualificationCommands.Verify(x => x.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(1)]
        public async Task SendPostQualificationToApproveWithIdUpdateQualificationStatusToBeWaitingApproval(
            int preQualificationId)
        {
            #region Arrange
            var tender = new TenderDefault().GetGeneralTender();
            MockUser();
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            MockUser();
            _moqQualificationCommands.Setup(x => x.UpdatePreQualificationAsync(tender))
                .Returns(() => Task.FromResult<Tender>(tender));
            #endregion

            var result = await _sut.SendPostQualificationToApprove(preQualificationId);

            Assert.Equal((int)Enums.TenderStatus.Pending,result.TenderStatusId);
        }



        [Theory]
        [InlineData(1, "022001000000", "1221")]
        public async Task ApprovePostQualification_WithQualificationId_UpdateQualificationStatusToBeApproved(int tenderId, string agencyCode, string verficationCode)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            _moqQualificationCommands.Setup(x => x.UpdatePreQualificationAsync(tender))
                .Returns(() => Task.FromResult<Tender>(tender));
            #endregion

            var result = await _sut.ApprovePostQualification(tenderId,agencyCode,verficationCode,new List<string>());

            Assert.Equal((int)Enums.TenderStatus.QualificationCommitteeApproval, result.TenderStatusId);
        }

        [Theory]
        [InlineData(1, "02200100004400", "1221")]
        public async Task ApprovePostQualification_WithWrongAgencyCode_RaisesUnHandledException(int tenderId, string agencyCode, string verficationCode)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            #endregion

            await Assert.ThrowsAsync<UnHandledAccessException>(async () =>
                 await _sut.ApprovePostQualification(tenderId, agencyCode, verficationCode, new List<string>()));

        }

        [Theory]
        [InlineData(1, "Reason", "022001000000")]
        public async Task RejectApprovePostQualification_WithRejectionReason_UpdateQualificationStatusToBeRejected(int tenderId, string rejectionReason, string agencyCode)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            _moqQualificationCommands.Setup(x => x.UpdatePreQualificationAsync(tender))
                .Returns(() => Task.FromResult<Tender>(tender));
            #endregion

            var result =
                await _sut.RejectApprovePostQualification(tenderId, rejectionReason, agencyCode, new List<string>());

            Assert.Equal((int)Enums.TenderStatus.Rejected,result.TenderStatusId);

        }

        [Theory]
        [InlineData(1, "022001000000")]
        public async Task ReopenPostQualification_WithQualificationId_UpdateQualificationStatusToBeUnderEstablishing(int tenderId, string agencyCode)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            _moqQualificationCommands.Setup(x => x.UpdatePreQualificationAsync(tender))
                .Returns(() => Task.FromResult<Tender>(tender));
            #endregion

            var result =
                await _sut.ReopenPostQualification(tenderId, agencyCode);

            Assert.Equal((int)Enums.TenderStatus.Established, result.TenderStatusId);

        }

        [Theory]
        [InlineData(1, 1121)]
        public async Task GetPostQualificationDetailsForChecking_WithQualificationId_ReturnesPreQualificationBasicModel(int tenderId, int userId)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", 1221, Enums.TenderActions.Approve);
            _moqQualificationQueries.Setup(x => x.GetQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            _moqQualificationQueries.Setup(x => x.GetQualificationForCheckPostQualificationByQualificationId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>()))
                .Returns(() => Task.FromResult<PreQualificationBasicDetailsModel>(new PreQualificationBasicDetailsModel()));
            #endregion
            
            var result =
                await _sut.GetPostQualificationDetailsByIdForChecking(tenderId, userId,new List<string>());

            Assert.IsType<PreQualificationBasicDetailsModel>(result);
        }

        [Theory]
        [InlineData(1)]
        public async Task SendPostQualificationToApproveChecking_WithQualificationId_HitUpdateQualificationOnce(int tenderId)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", 1221, Enums.TenderActions.Approve);
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            #endregion

            await _sut.SendPostQualificationToApproveChecking(tenderId, new List<string>());

            _moqQualificationCommands.Verify(com =>com.UpdateAsync(It.IsAny<Tender>()),Times.Once); 
        }

        [Theory]
        [InlineData(1)]
        public async Task ApprovePostQualificationChecking_WithQualificationId_HitUpdateQualificationOnce(int tenderId)
        {
            #region Arrange
            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            List<QualificationFinalResult> qualificationFinalResults = new List<QualificationFinalResult>()
            {
                new QualificationFinalResult(1, "101010", 7, 14),
                new QualificationFinalResult(1, "10150010", 7, 15)
            };
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", 1221, Enums.TenderActions.Approve);
            _moqQualificationQueries.Setup(x => x.GetQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            _moqQualificationQueries.Setup(x => x.GetSupplierResultForQualification(It.IsAny<int>()))
                .Returns(() => qualificationFinalResults);
            #endregion

            await _sut.ApprovePostQualificationChecking(tenderId, new List<string>(){ RoleNames.OffersCheckManager });

            _moqQualificationCommands.Verify(com => com.UpdateAsync(It.IsAny<Tender>()), Times.Once);
        }

        [Theory]
        [InlineData(1, "Test")]
        public async Task RejectCheckPostQualification_WithQualificationId_UpdateStatusToRejectedChecking(int tenderId,
            string rejectionReason)
        {
            #region Arrange

            MockUser();
            var tender = new TenderDefault().GetGeneralTender();
            tender.UpdateTenderStatus(Enums.TenderStatus.Approved, "", 1221, Enums.TenderActions.Approve);
            _moqQualificationQueries.Setup(x => x.GetPostQualificationById(It.IsAny<int>()))
                .Returns(() => Task.FromResult<Tender>(tender));
            _moqQualificationCommands.Setup(qualComm => qualComm.UpdateAsync(tender))
                .Returns(() => Task.FromResult(tender));
            #endregion

            var result = await _sut.RejectCheckPostQualificationOffers(tenderId, rejectionReason, new List<string>());

            Assert.Equal((int) Enums.TenderStatus.DocumentCheckRejected, result.TenderStatusId);
        }


        //[Fact]
        //public async Task ShouldCreateNewPostQualificationWithOldPostQualificationData()
        //{
        //    #region Arrange
        //    var userId = 1;
        //    var agencyCode = "32323";
        //    var postQualificationModel = new PostQualificationApplyDocumentsModel()
        //    {
        //        CommercialNumbers = new List<string>() { "4343", "1010" },
        //        PostQualificationTenderId = 2,
        //        postQualificationIdString = null,
        //        TenderName = "GGg",
        //        TenderId = 323,
        //        LastEnqueriesDate = DateTime.Now,
        //        LastOfferPresentationDate = DateTime.Now,
        //        OffersCheckingDate = DateTime.Now,
        //        branchId = 2,
        //        TenderActivitieIDs = new List<int>() { 1, 2 },
        //        TenderAreaIDs = new List<int>() { 1, 2 },

        //        Attachments = new List<TenderAttachmentModel>(),
        //    };
        //    _moqQualificationQueries.Setup(m => m.GetPostQualificationById(It.IsAny<int>()))
        //    .Returns(() =>
        //    {
        //        return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
        //    });
        //    _moqGenericCommandRepository.Setup(q => q.CreateAsync(It.IsAny<Tender>())).Returns(() =>
        //    {
        //        return Task.FromResult<Tender>(new TenderDefault().GetPreQualification());
        //    });
        //    _moqQualificationQueries.Setup(m => m.GetPostQualificationByTenderId(It.IsAny<int>()))
        //    .Returns(() =>
        //    {
        //        return Task.FromResult<Tender>(new TenderDefault().GetPreQualification());
        //    });

        //    #region Mock-User
        //    var context = new Mock<HttpContext>();
        //    var claim = new Claim("sub", "15445");
        //    var idintity = new GenericIdentity("testUser");
        //    idintity.AddClaim(claim);
        //    context.SetupGet(x => x.User.Identity).Returns(() =>
        //    {
        //        return idintity;
        //    });
        //    _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        //    #endregion Mock-User

        //    _moqQualificationQueries.Setup(c => c.GetTenderWithQualificationConfigurations(It.IsAny<int>())).Returns(()
        //        =>
        //    {
        //        return Task.FromResult<Tender>(new TenderDefault().GetQualificationWithConfiguration());
        //    });

        //    #endregion Arrange

        //    await _sut.CreatePoatQualification(postQualificationModel, userId, agencyCode);

        //    _moqGenericCommandRepository.Verify(c => c.SaveAsync(), Times.Once);
        //}



        #region Shared Arrange

        private void MockUser()
        {
            var context = new Mock<HttpContext>();
            var claim = new Claim("sub", "15445");
            var idintity = new GenericIdentity("testUser");
            idintity.AddClaim(claim);
            context.SetupGet(x => x.User.Identity).Returns(() =>
            {
                return idintity;
            });
            _moqHttpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
        }

        #endregion 
    }
}
