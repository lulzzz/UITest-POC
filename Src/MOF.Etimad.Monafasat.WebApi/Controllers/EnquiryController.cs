using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class EnquiryController : BaseController
    {
        private readonly IEnquiryAppService _enquiryAppService;
        private readonly IMapper _mapper;

        public EnquiryController(IEnquiryAppService enquiryAppService, IMapper mapper, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _enquiryAppService = enquiryAppService;
            _mapper = mapper;
        }


        #region Enquiry
        [Authorize(RoleNames.SupplierPolicy)]
        [HttpPost]
        [Route("SendEnquiry")]
        public async Task<EnquiryModel> SendEnquiry([FromBody] EnquiryModel enquiryModel)
        {
            string cr = User.SupplierNumber();
            int userIdFlag = User.UserId();
            enquiryModel.CommericalRegisterNo = cr;
            var result = await _enquiryAppService.SendEnquiry(enquiryModel);
            var model = _mapper.Map<EnquiryModel>(result, opt => { opt.Items["statusFlag"] = true; opt.Items[nameof(userIdFlag)] = userIdFlag; });
            return model;
        }

        [HttpGet]
        [Route("GetAllSuppliersEnquiriesByTenderId/{tenderId}")]
        public async Task<QueryResult<EnquiryModel>> GetAllSuppliersEnquiriesByTenderId(int tenderId)
        {
            var result = await _enquiryAppService.GetAllPendingEnquiriesByTenderId(tenderId);
            var enquiryModel = _mapper.Map<QueryResult<EnquiryModel>>(result);
            return enquiryModel;
        }

        [Authorize(RoleNames.TechnicalCommitteeUserPolicy)]
        [HttpGet]
        [Route("GetEnquiryById/{enquiryId}")]
        public async Task<EnquiryModel> GetEnquiryById(int enquiryId)
        {
            var enquiryModel = await _enquiryAppService.GetEnquiryById(enquiryId, User.UserId());
            return enquiryModel;
        }

        #endregion

        #region Enquiry Reply

        [HttpGet]
        [Route("GetAllEnquiryRepliesByTenderId/{tenderId}")]
        public async Task<List<EnquiryModel>> GetAllEnquiryRepliesByTenderId(int tenderId)
        {
            bool statusFlag = false;
            int userIdFlag = User.UserId();
            if (User.IsInRole(RoleNames.supplier))
                statusFlag = true;
            List<Core.Entities.Enquiry> result = await _enquiryAppService.GetAllEnquiryRepliesByTenderId(tenderId, userIdFlag);
            List<EnquiryModel> enquiryModel = _mapper.Map<List<EnquiryModel>>(result, opt => { opt.Items[nameof(statusFlag)] = statusFlag; opt.Items[nameof(userIdFlag)] = userIdFlag; });
            return enquiryModel;
        }

        [HttpGet]
        [Route("GetAllEnquiryRepliesByTenderId")]
        public async Task<QueryResult<EnquiryModel>> GetAllEnquiryRepliesByTenderId(SimpleSearchCretriaModel criteria)
        {
            bool statusFlag = false;
            if (User.IsInRole(RoleNames.supplier))
                statusFlag = true;
            int userIdFlag = User.UserId();
            criteria.UserId = User.UserId();
            QueryResult<Core.Entities.Enquiry> result = await _enquiryAppService.GetAllEnquiryRepliesByTenderId(criteria);
            QueryResult<EnquiryModel> enquiryModel = _mapper.Map<QueryResult<EnquiryModel>>(result, opt => { opt.Items[nameof(statusFlag)] = statusFlag; opt.Items[nameof(userIdFlag)] = userIdFlag; });
            return enquiryModel;
        }

        [HttpGet]
        [Route("GetAllEnquiryRepliesByEnquiryId")]
        public async Task<QueryResult<EnquiryReplyModel>> GetAllEnquiryRepliesByEnquiryId(SimpleSearchCretriaModel criteria)
        {
            var enquiryModel = await _enquiryAppService.GetAllEnquiryRepliesByEnquiryId(criteria);
            return enquiryModel;
        }

        [HttpGet]
        [Route("GetEnquiryReplyByReplyId/{enquiryReplyId}")]
        public async Task<EnquiryReplyModel> GetEnquiryReplyByReplyId(int enquiryReplyId)
        {
            var enquiryModel = await _enquiryAppService.GetEnquiryReplyById(enquiryReplyId);
            return enquiryModel;
        }

        [HttpGet]
        [Route("GetJoinTechnicalCommitteeByEnquiryId/{enquiryId}")]
        public async Task<JoinTechnicalCommitteeModel> GetJoinTechnicalCommitteeByEnquiryId(int enquiryId)
        {
            var result = await _enquiryAppService.GetJoinTechnicalCommitteeByEnquiryId(enquiryId);

            var enquiryModel = result != null ? result : new JoinTechnicalCommitteeModel();

            return enquiryModel;

        }

        [HttpGet]
        [Route("ApproveEnquiryReply/{enquiryReplyId}")]
        public async Task<EnquiryReplyModel> ApproveEnquiryReply(int enquiryReplyId)
        {
            var enquiryReply = await _enquiryAppService.ApproveEnquiryReply(enquiryReplyId);
            EnquiryReplyModel enquiryReplyModel = _mapper.Map<EnquiryReplyModel>(enquiryReply);
            return enquiryReplyModel;
        }

        [HttpPost]
        [Route("AddEnquiryReply")]
        public async Task<EnquiryReplyModel> AddEnquiryReply([FromBody] EnquiryReplyModel enquiryReplyModel)
        {
            var result = await _enquiryAppService.AddEnquiryReply(enquiryReplyModel);
            var model = _mapper.Map<EnquiryReplyModel>(result);
            return model;
        }

        [HttpPost]
        [Route("AddEnquiryComment")]
        public async Task AddEnquiryComment([FromBody] EnquiryReplyModel enquiryReplyModel)
        {
            enquiryReplyModel.CommitteeId = User.SelectedCommittee();
            await _enquiryAppService.AddEnquiryComment(enquiryReplyModel);
        }

        [HttpPost]
        [Route("EditEnquiryReply")]
        public async Task<EnquiryReplyModel> EditEnquiryReply([FromBody] EnquiryReplyModel enquiryReplyModel)
        {
            var result = await _enquiryAppService.EditEnquiryReply(enquiryReplyModel);
            var model = _mapper.Map<EnquiryReplyModel>(result);
            return model;
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task Delete(int id)
        {
            await _enquiryAppService.DeleteReply(id);
        }

        #endregion

        #region Join Committee

        [HttpPost]
        [Route("SendInvitationsToJoinCommittee")]
        public async Task<JoinTechnicalCommitteeModel> SendInvitationsToJoinCommittee([FromBody] JoinTechnicalCommitteeModel joinTechnicalCommitteeModel)
        {
            joinTechnicalCommitteeModel.SelectedUserCommitteeId = User.SelectedCommittee();
            await _enquiryAppService.SendInvitationsToJoinCommittee(joinTechnicalCommitteeModel);
            return joinTechnicalCommitteeModel;
        }

        [HttpPost]
        [Route("RemoveInvitedCommittee")]
        public async Task RemoveInvitedCommittee(int committeeId, int replyId, int enquiryId)
        {
            await _enquiryAppService.RemoveInvitedCommittee(committeeId, replyId, enquiryId);
        }

        [HttpGet]
        [Route("GetInvitedCommitesByEnquiryId")]
        public async Task<QueryResult<JoinTechnicalCommitteeModel>> GetInvitedCommitesByEnquiryId(SimpleSearchCretriaModel criteria)
        {

            var result = await _enquiryAppService.GetInvitedCommitesByEnquiryId(criteria);
            return result;
        }



        #endregion
    }
}
