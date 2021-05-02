using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.CommunicationRequestDefaults;
using MOF.Etimad.Monafasat.TestsBuilders.Enquiry;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.UnitTests.Domian.Services.Enquiry
{
    public class EnquiryAppServiceTests
    {
        private readonly EnquiryAppService _sut;
        private readonly Mock<IEnquiryQueries> _enquiryQueries;
        private readonly Mock<IEnquiryCommands> _enquiryCommands;
        private readonly Mock<IEnquiryDomainService> _enquiryDomain;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ITenderAppService> _tenderAppService;
        private readonly Mock<IIDMAppService> _iDMAppService;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<INotificationAppService> _notificationAppService;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> _rootConfigurationMock;

        public EnquiryAppServiceTests()
        {
            _enquiryQueries = new Mock<IEnquiryQueries>();
            _enquiryCommands = new Mock<IEnquiryCommands>();
            _enquiryDomain = new Mock<IEnquiryDomainService>();
            _mapper = new Mock<IMapper>();
            _tenderAppService = new Mock<ITenderAppService>();
            _iDMAppService = new Mock<IIDMAppService>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _notificationAppService = new Mock<INotificationAppService>();
            _rootConfigurationMock = new Mock<IOptionsSnapshot<RootConfigurations>>();
            _rootConfigurationMock.Setup(x => x.Value).Returns(new RootConfigurationDefaults().DefaultRootConfigurationsForTender());

            _sut = new EnquiryAppService(_iDMAppService.Object, _notificationAppService.Object, _mapper.Object,
                _enquiryQueries.Object, _enquiryCommands.Object, _tenderAppService.Object, _enquiryDomain.Object, _httpContextAccessor.Object, _rootConfigurationMock.Object);
        }

        [Fact]
        public void Should_SendEnquiry()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            var enquiryModel = new EnquiryDefaults().GetEnquiryModel();
            var enquiryEntity = new EnquiryDefaults().GetEnquiryData();
            _mapper.Setup(x => x.Map<Core.Entities.Enquiry>(It.IsAny<EnquiryModel>())).Returns(() =>
            {
                return enquiryEntity;
            });

            _tenderAppService.Setup(x => x.FindTenderByTenderId(It.IsAny<int>())).Returns(() =>
              {
                  return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
              });

            var result = _sut.SendEnquiry(enquiryModel);

            result.ShouldNotBeNull();

            _enquiryCommands.Verify(x => x.CreateAsync(It.IsAny<Core.Entities.Enquiry>()), Times.Once);

        }

        [Fact]
        public void Should_AddEnquiryReply()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            var enquiryReplyModel = new EnquiryDefaults().GetEnquiryReplyModel();


            _enquiryDomain.Setup(x => x.UserCanAddReplyToEnquiry(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });

            var result = _sut.AddEnquiryReply(enquiryReplyModel);

            result.ShouldNotBeNull();

            _enquiryCommands.Verify(x => x.CreateReplyAsync(It.IsAny<Core.Entities.EnquiryReply>()), Times.Once);

        }

        [Fact]
        public void Should_AddEnquiryComment()
        {

            var enquiryReplyModel = new EnquiryDefaults().GetEnquiryReplyModel();

            var result = _sut.AddEnquiryComment(enquiryReplyModel);

            result.ShouldNotBeNull();

            _enquiryCommands.Verify(x => x.CreateReplyAsync(It.IsAny<Core.Entities.EnquiryReply>()), Times.Once);

        }

        [Fact]
        public void Should_EditEnquiryReply()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User


            var enquiryReplyModel = new EnquiryDefaults().GetEnquiryReplyModel();

            _enquiryQueries.Setup(x => x.GetEnquiryReplyByReplyId(It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<EnquiryReply>(new EnquiryDefaults().GetEnquiryReplyData());
            });


            _enquiryDomain.Setup(x => x.UserCanAddReplyToEnquiry(It.IsAny<int>(), It.IsAny<int>())).Returns(() =>
            {
                return Task.FromResult<Tender>(new TenderDefault().GetGeneralTender());
            });

            var result = _sut.EditEnquiryReply(enquiryReplyModel);

            result.ShouldNotBeNull();

            _enquiryCommands.Verify(x => x.UpdateReplyAsync(It.IsAny<Core.Entities.EnquiryReply>()), Times.Once);
        }
        [Fact]
        public void Should_GetInvitedCommitesByEnquiryId()
        {
            _enquiryQueries.Setup(x => x.GetInvitedCommitesByEnquiryId(It.IsAny<SimpleSearchCretriaModel>()))
                            .Returns(() =>
                            {
                                return Task.FromResult(new QueryResult<JoinTechnicalCommitteeModel>(new List<JoinTechnicalCommitteeModel> { new JoinTechnicalCommitteeModel() }, 1, 1, 1));
                            });
            var criteria = new SimpleSearchCretriaModel();

            var result = _sut.GetInvitedCommitesByEnquiryId(criteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<JoinTechnicalCommitteeModel>>(result.Result);
        }

        [Fact]
        public void Should_GetAllEnquiryRepliesByEnquiryId()
        {
            _enquiryQueries.Setup(x => x.GetAllEnquiryRepliesByEnquiryId(It.IsAny<SimpleSearchCretriaModel>()))
                            .Returns(() =>
                            {
                                return Task.FromResult(new QueryResult<EnquiryReplyModel>(new List<EnquiryReplyModel> { new EnquiryReplyModel() }, 1, 1, 1));
                            });
            var criteria = new SimpleSearchCretriaModel();

            var result = _sut.GetAllEnquiryRepliesByEnquiryId(criteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<EnquiryReplyModel>>(result.Result);
        }

        [Fact]
        public void Should_GetAllEnquiryRepliesByTenderId_ReturnEnquiry()
        {
            _enquiryQueries.Setup(x => x.GetAllEnquiryRepliesByTenderId(It.IsAny<SimpleSearchCretriaModel>()))
                            .Returns(() =>
                            {
                                return Task.FromResult(new QueryResult<Core.Entities.Enquiry>(new List<Core.Entities.Enquiry> { new Core.Entities.Enquiry() }, 1, 1, 1));
                            });
            var criteria = new SimpleSearchCretriaModel();

            var result = _sut.GetAllEnquiryRepliesByTenderId(criteria);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<Core.Entities.Enquiry>>(result.Result);
        }

        [Fact]
        public void Should_GetAllEnquiryRepliesByTenderId_ReturnList()
        {
            _enquiryQueries.Setup(x => x.GetAllEnquiryRepliesByTenderId(It.IsAny<int>(), It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult(new List<Core.Entities.Enquiry>(new List<Core.Entities.Enquiry>()));
                            });

            var result = _sut.GetAllEnquiryRepliesByTenderId(1, 1);

            Assert.NotNull(result);
            Assert.IsType<List<Core.Entities.Enquiry>>(result.Result);
        }


        [Fact]
        public void Should_GetAllPendingEnquiriesByTenderId()
        {
            _enquiryQueries.Setup(x => x.GetAllPendingEnquiriesByTenderId(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult(new QueryResult<Core.Entities.Enquiry>(new List<Core.Entities.Enquiry> { new Core.Entities.Enquiry() }, 1, 1, 1));
                            });

            var result = _sut.GetAllPendingEnquiriesByTenderId(1);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<Core.Entities.Enquiry>>(result.Result);
        }

        [Fact]
        public void Should_GetEnquiryById()
        {
            _enquiryQueries.Setup(x => x.FindEnquiryById(It.IsAny<int>(), It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryModel>(new EnquiryDefaults().GetEnquiryModel());
                            });

            var result = _sut.GetEnquiryById(1, 1);

            Assert.NotNull(result);
            Assert.IsType<EnquiryModel>(result.Result);
        }

        [Fact]
        public void Should_GetEnquiryReplyById()
        {
            _enquiryQueries.Setup(x => x.FindEnquiryReplyById(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReplyModel>(new EnquiryDefaults().GetEnquiryReplyModel());
                            });

            var result = _sut.GetEnquiryReplyById(1);

            Assert.NotNull(result);
            Assert.IsType<EnquiryReplyModel>(result.Result);
        }

        [Fact]
        public void Should_DeleteReply()
        {
            _enquiryQueries.Setup(x => x.GetEnquiryReplyByReplyId(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReply>(new EnquiryDefaults().GetEnquiryReplyData());
                            });

            var result = _sut.DeleteReply(1);
            Assert.NotNull(result);
            _enquiryCommands.Verify(x => x.UpdateReplyAsync(It.IsAny<EnquiryReply>()), Times.Once);

        }
        [Fact]
        public void Should_DeleteReply_ThrowException()
        {
            EnquiryReply enquiryReply = new EnquiryDefaults().GetEnquiryReplyData();

            _enquiryQueries.Setup(x => x.GetEnquiryReplyByReplyId(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReply>(enquiryReply);
                            });

            enquiryReply.ApproveEnquiryReply();

            Assert.ThrowsAsync<BusinessRuleException>(async () => await _sut.DeleteReply(1));
        }

        [Fact]
        public void Should_ApproveEnquiryReply_OnTender()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User
            _enquiryQueries.Setup(x => x.GetEnquiryReplyWithTender(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReply>(new EnquiryDefaults().GetEnquiryReplyDataWithTender());
                            });

            List<string> suppliers = new List<string> { "123" };

            _iDMAppService.Setup(x => x.GetAllSupplierOnTender(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<List<string>>(suppliers);
                            });

            _enquiryQueries.Setup(x => x.GetEnquiryReplyWithCommunicationRequest(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReply>(new EnquiryDefaults().GetEnquiryReplyDataWithCommunicationRequest());
                            });

            var communicationRequest = new CommunicationRequestDefault().GetGeneralAgencyCommunicationRequest();

            _enquiryDomain.Setup(x => x.GetEnquiryCommunicationRequestByRequestId(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<AgencyCommunicationRequest>(communicationRequest);
                        });

            communicationRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.PendingEnquiryForReply);

            var result = _sut.ApproveEnquiryReply(1);
            Assert.NotNull(result);
            _enquiryCommands.Verify(x => x.UpdateReplyAsync(It.IsAny<EnquiryReply>()), Times.Once);

        }

        [Fact]
        public void Should_ApproveEnquiryReply_OnPrequalification()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            var enquiryWithTender = new EnquiryDefaults().GetEnquiryReplyDataWithTender();
            enquiryWithTender.Enquiry.Tender.SetTenderType((int)Enums.TenderType.PreQualification);
            _enquiryQueries.Setup(x => x.GetEnquiryReplyWithTender(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReply>(enquiryWithTender);
                            });

            List<string> suppliers = new List<string> { "123" };

            _iDMAppService.Setup(x => x.GetAllSupplierOnQualification(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<List<string>>(suppliers);
                            });

            _enquiryQueries.Setup(x => x.GetEnquiryReplyWithCommunicationRequest(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<EnquiryReply>(new EnquiryDefaults().GetEnquiryReplyDataWithCommunicationRequest());
                            });

            var communicationRequest = new CommunicationRequestDefault().GetGeneralAgencyCommunicationRequest();

            _enquiryDomain.Setup(x => x.GetEnquiryCommunicationRequestByRequestId(It.IsAny<int>()))
                        .Returns(() =>
                        {
                            return Task.FromResult<AgencyCommunicationRequest>(communicationRequest);
                        });

            communicationRequest.UpdateAgencyCommunicationRequestStatus((int)Enums.AgencyCommunicationRequestStatus.PendingEnquiryForReply);

            var result = _sut.ApproveEnquiryReply(1);
            Assert.NotNull(result);
            _enquiryCommands.Verify(x => x.UpdateReplyAsync(It.IsAny<EnquiryReply>()), Times.Once);

        }

        [Fact]
        public void Should_GetJoinTechnicalCommitteeByEnquiryId()
        {
            _enquiryQueries.Setup(x => x.GetJoinCommitteeByEnquiryId(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<JoinTechnicalCommitteeModel>(new EnquiryDefaults().GetJoinTechnicalCommitteeModel());
                            });

            var result = _sut.GetJoinTechnicalCommitteeByEnquiryId(1);

            Assert.NotNull(result);
            Assert.IsType<JoinTechnicalCommitteeModel>(result.Result);
        } 
        [Fact]
        public void Should_SendInvitationsToJoinCommittee()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            JoinTechnicalCommitteeModel joinModel = new EnquiryDefaults().GetJoinTechnicalCommitteeModel();
            _enquiryQueries.Setup(x => x.GetJoinCommitteeByEnquiryId(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<JoinTechnicalCommitteeModel>(joinModel);
                            });

            _enquiryQueries.Setup(x => x.FindEnquiryByEnquiryId(It.IsAny<int>()))
                           .Returns(() =>
                           {
                               return Task.FromResult<Core.Entities.Enquiry>(new EnquiryDefaults().GetEnquiryDataWithTender());
                           });
            var result = _sut.SendInvitationsToJoinCommittee(joinModel);

            Assert.NotNull(result);
            Assert.IsType<JoinTechnicalCommitteeModel>(result.Result);
        }
        [Fact]
        public void Should_RemoveInvitedCommittee()
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
            _httpContextAccessor.SetupGet(x => x.HttpContext).Returns(context.Object);
            #endregion Mock-User

            _enquiryQueries.Setup(x => x.FindEnquiryByEnquiryId(It.IsAny<int>()))
                            .Returns(() =>
                            {
                                return Task.FromResult<Core.Entities.Enquiry>(new EnquiryDefaults().GetEnquiryDataWithTender());
                            });

            var result = _sut.RemoveInvitedCommittee(1, 1, 1);

            Assert.NotNull(result);
            _enquiryCommands.Verify(x => x.UpdateAsync(It.IsAny<Core.Entities.Enquiry>()), Times.Once);
        }

    }
}