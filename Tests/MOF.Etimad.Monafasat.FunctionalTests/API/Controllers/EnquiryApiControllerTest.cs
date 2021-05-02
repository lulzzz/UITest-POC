using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.FunctionalTests.Base;
using MOF.Etimad.Monafasat.FunctionalTests.Helpers;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Controllers;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace MOF.Etimad.Monafasat.FunctionalTests.API.Controllers
{
    public class EnquiryApiControllerTest : BaseTestApiController
    {
        private Claim[] _claims;
        private readonly IEnquiryAppService enquiryAppService;
        private readonly IMapper mapper;
        private readonly Mock<IOptionsSnapshot<RootConfigurations>> rootConfigurationsMock;
        private EnquiryController _enquiryController;
        public EnquiryApiControllerTest()
        {
            var serviceProvider = services.BuildServiceProvider();
            enquiryAppService = serviceProvider.GetService<IEnquiryAppService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EnquiryModel, Enquiry>();
                cfg.CreateMap<EnquiryReplyModel, EnquiryReply>();
                cfg.ValidateInlineMaps = false;
            });
            mapper = config.CreateMapper();
            rootConfigurationsMock = SetupConfigurations.GetApplicationConfiguration(Directory.GetCurrentDirectory());
            _enquiryController = new EnquiryController(enquiryAppService, mapper, rootConfigurationsMock.Object);
        }

        [Fact]
        public async Task SendEnquiry_Returns_EnquiryModel()
        {
            var _commercialRegisterNo = "142154293000206";
            var _agencyCode = "022001000000";
            var _userId = "1";
            _claims = new Claim[4] {
                new Claim(IdentityConfigs.Claims.SelectedCR, _commercialRegisterNo),
                new Claim(IdentityConfigs.Claims.isSemiGovAgency,"1"),
                new Claim(IdentityConfigs.Claims.SelectedCR, _agencyCode),
                new Claim(IdentityConfigs.Claims.NameIdentifier , _userId)
            };
            _enquiryController = _enquiryController.WithIdentity(_claims);

            var _enquiryModel = new EnquiryModel()
            {
                TenderId = 18,
                EnquiryName = "FunctionL Test Enquiry Name",
                CommericalRegisterNo = "10001908502",
            };

            var result = await _enquiryController.SendEnquiry(_enquiryModel);

            Assert.NotNull(result);
            Assert.IsType<EnquiryModel>(result);
            Assert.Equal(_enquiryModel.CommericalRegisterNo, result.CommericalRegisterNo);
            Assert.Equal(_enquiryModel.TenderId, result.TenderId);
            Assert.Equal(_enquiryModel.EnquiryName, result.EnquiryName);
            Assert.True(result.EnquiryId > 0);
        }

        [Fact]
        public async Task GetSuppliersReportByTenderId_Returns_Data()
        {
            var _tenderId = 18;

            var result = await _enquiryController.GetAllSuppliersEnquiriesByTenderId(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<EnquiryModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageSize > 0);
            Assert.True(result.TotalCount > 0);
        }

        [Fact]
        public async Task GetEnquiryById_Returns_EnquiryModel()
        {
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier , _userId)
            };
            _enquiryController = _enquiryController.WithIdentity(_claims);

            var _enquiryId = 1;
            var _enquiryModel = new EnquiryModel()
            {
                TenderId = 18,
                EnquiryName = "Test question 1 by semi gov vendor - 5900037276",
                ReferenceNumber = "191139018001"
            };

            var result = await _enquiryController.GetEnquiryById(_enquiryId);

            Assert.NotNull(result);
            Assert.IsType<EnquiryModel>(result);
            Assert.Equal(_enquiryModel.ReferenceNumber, result.ReferenceNumber);
            Assert.Equal(_enquiryModel.TenderId, result.TenderId);
            Assert.Equal(_enquiryModel.EnquiryName, result.EnquiryName);
            Assert.Equal(_enquiryId, result.EnquiryId);
        }

        [Fact]
        public async Task GetAllEnquiryRepliesByTenderId_Returns_Data()
        {
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier , _userId)
            };
            _enquiryController = _enquiryController.WithIdentity(_claims);

            var _tenderId = 18;

            var result = await _enquiryController.GetAllEnquiryRepliesByTenderId(_tenderId);

            Assert.NotNull(result);
            Assert.IsType<List<EnquiryModel>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllEnquiryRepliesByTenderId_BySearchCriteria_Returns_Data()
        {
            var _userId = "1";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.NameIdentifier , _userId)
            };
            _enquiryController = _enquiryController.WithIdentity(_claims);

            var _simpleSearchCretriaModel = new SimpleSearchCretriaModel()
            {
                TenderId = 18
            };

            var result = await _enquiryController.GetAllEnquiryRepliesByTenderId(_simpleSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<EnquiryModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.TotalCount > 0);
        }

        [Fact]
        public async Task GetAllEnquiryRepliesByEnquiryId_Returns_Data()
        {
            var _simpleSearchCretriaModel = new SimpleSearchCretriaModel()
            {
                EnquiryIdString = Util.Encrypt(1)
            };

            var result = await _enquiryController.GetAllEnquiryRepliesByEnquiryId(_simpleSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<EnquiryReplyModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.TotalCount > 0);
        }

        [Fact]
        public async Task GetEnquiryReplyByReplyId_Returns_Data()
        {
            var _enquiryReplyId = 1;
            var _enquiryReplyMessage = "Enquiry Reply Message1";

            var result = await _enquiryController.GetEnquiryReplyByReplyId(_enquiryReplyId);

            Assert.NotNull(result);
            Assert.IsType<EnquiryReplyModel>(result);
            Assert.Equal(_enquiryReplyMessage, result.EnquiryReplyMessage);
        }

        [Fact]
        public async Task GetJoinTechnicalCommitteeByEnquiryId_Returns_Data()
        {
            var _enquiryId = 1;
            var _enquiryIdString = Util.Encrypt(_enquiryId);
            var _enquiryName = "Test question 1 by semi gov vendor - 5900037276";

            var result = await _enquiryController.GetJoinTechnicalCommitteeByEnquiryId(_enquiryId);

            Assert.NotNull(result);
            Assert.IsType<JoinTechnicalCommitteeModel>(result);
            Assert.Equal(_enquiryIdString, result.EnquiryIdString);
            Assert.Equal(_enquiryName, result.EnquiryName);
        }

        [Fact]
        public async Task AddEnquiryReply_Returns_UserCanNotAddReply()
        {

            var _enquiryReplyModel = new EnquiryReplyModel()
            {
                TenderId = 4,
                EnquiryName = "FunctionL Test Enquiry Reply Name",
                CommericalRegisterNo = "10001908502",
            };

            var _expectedMessage = "User Can Not Add Reply";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _enquiryController.AddEnquiryReply(_enquiryReplyModel));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        [Fact]
        public async Task AddEnquiryComment_Success()
        {
            var _committeeId = "21";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.CommitteeId, _committeeId)
            };
            _enquiryController = _enquiryController.WithIdentity(_claims);

            var _enquiryReplyModel = new EnquiryReplyModel()
            {
                TenderId = 4,
                EnquiryName = "FunctionL Test Enquiry Reply Name",
                CommericalRegisterNo = "10001908502",
                EnquiryId = 1
            };

            await _enquiryController.AddEnquiryComment(_enquiryReplyModel);
        }

        [Fact]
        public async Task EditEnquiryReply_Returns_UserCanNotAddReply()
        {

            var _enquiryReplyModel = new EnquiryReplyModel()
            {
                TenderId = 1,
                EnquiryName = "FunctionL Test Enquiry Reply Name",
                CommericalRegisterNo = "10001908502",
                EnquiryReplyId = 1
            };

            var _expectedMessage = "User Can Not Add Reply";

            var result = await Assert.ThrowsAsync<BusinessRuleException>(() => _enquiryController.EditEnquiryReply(_enquiryReplyModel));

            Assert.NotNull(result);
            Assert.Equal(_expectedMessage, result.Message);
        }

        //DELETE FROM[NewMonafasatDevelopment_DevIteration1.3.2].[Enquiry].[JoinTechnicalCommittee] WHERE CommitteeId = 2
        [Fact]
        public async Task SendInvitationsToJoinCommittee_Success()
        {
            var _committeeId = "32";
            _claims = new Claim[1] {
                new Claim(IdentityConfigs.Claims.CommitteeId, _committeeId)
            };
            _enquiryController = _enquiryController.WithIdentity(_claims);

            var _joinTechnicalCommitteeModel = new JoinTechnicalCommitteeModel()
            {
                EnquiryId = 5,
                JoinedCommitteeId = 1
            };

            var result = await _enquiryController.SendInvitationsToJoinCommittee(_joinTechnicalCommitteeModel);

            Assert.NotNull(result);
            Assert.Equal(_joinTechnicalCommitteeModel.EnquiryId, result.EnquiryId);
        }

        [Fact]
        public async Task GetInvitedCommitesByEnquiryId_Returns_Data()
        {
            var _simpleSearchCretriaModel = new SimpleSearchCretriaModel()
            {
                EnquiryId = 1
            };

            var result = await _enquiryController.GetInvitedCommitesByEnquiryId(_simpleSearchCretriaModel);

            Assert.NotNull(result);
            Assert.IsType<QueryResult<JoinTechnicalCommitteeModel>>(result);
            Assert.NotEmpty(result.Items);
            Assert.True(result.PageSize > 0);
            Assert.True(result.PageNumber > 0);
            Assert.True(result.TotalCount > 0);
        }
    }
}
