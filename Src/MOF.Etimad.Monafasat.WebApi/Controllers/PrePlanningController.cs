using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PrePlanningController : BaseController
    {
        private readonly IPrePlanningAppService _prePlanningAppService;
        private readonly ILookUpService _lookupAppService;
        private readonly IVerificationService _verification;
        public PrePlanningController(IPrePlanningAppService prePlanningAppService, ILookUpService lookupAppService, IVerificationService verification, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _verification = verification;
            _prePlanningAppService = prePlanningAppService;
            _lookupAppService = lookupAppService;
        }

        #region lookUps
        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.CustomerService + "," + RoleNames.EtimadOfficer + "," + RoleNames.PurshaseSpecialist)]
        [HttpGet]
        [Route("GetStatus")]
        public async Task<List<LookupModel>> GetStatus()
        {
            var tenderStatusList = await _lookupAppService.GetQualificationStatusLookup();
            return tenderStatusList;
        }
        #endregion

        [Authorize(RoleNames.PrePlanningIndexPolicy)]
        [HttpGet]
        [Route("GetPrePlanningModelById/{id}")]
        public async Task<PrePlanningModel> GetPrePlanningModelById(int id)
        {
            PrePlanningModel model;
            model = await _prePlanningAppService.FindPrePlanningById(id);
            return model;
        }

        [Authorize(RoleNames.PrePlanningIndexPolicy)]
        [HttpGet]
        [Route("GetPrePlanningDetailsById/{id}")]
        public async Task<PrePlanningModel> GetPrePlanningDetailsById(int id)
        {
            PrePlanningModel model;
            model = await _prePlanningAppService.GetPrePlanningDetailsById(id);
            return model;
        }
        [Authorize(RoleNames.PrePlanningIndexPolicy)]
        [HttpGet]
        [Route("SetPrePlanningLookUps")]
        public async Task<PrePlanningModel> SetPrePlanningLookUps()
        {
            PrePlanningModel model;
            model = await _prePlanningAppService.SetPrePlanningLookUps();
            return model;
        }
        [Authorize(RoleNames.PrePlanningIndexPolicy)]
        [HttpGet]
        [Route("Deactivate/{id}")]
        public async Task<int> Deactivate(int id)
        {
            return await _prePlanningAppService.Deactivate(id, User.IsInRole(RoleNames.PrePlanningCreator));
        }

        [Authorize(RoleNames.PrePlanningIndexPolicy)]
        [HttpGet]
        [Route("FindPrePlanningBySearchCriteria")]
        public async Task<QueryResult<PrePlanningModel>> FindPrePlanningBySearchCriteria(PrePlanningSearchCriteria criteria)
        {
            if (User.IsInRole(RoleNames.supplier))
            {
                criteria.StatusId = (int)Enums.PrePlanningStatus.Approved;
            }
            else
            {
                criteria.AgencyCode = User.UserAgency();
            }
            return await _prePlanningAppService.FindPrePlanningBySearchCriteria(criteria);
        }

        [Authorize(RoleNames.PrePlanningCreationPolicy)]
        [HttpPost("Add")]
        public async Task<PrePlanningModel> Add([FromBody] PrePlanningModel model)
        {
            model.AgencyCode = User.UserAgency();
            model.BranchId = User.UserBranch();
            PrePlanningModel PreplanningModel = await _prePlanningAppService.CreateAsync(model);
            return PreplanningModel;
        }

        [Authorize(RoleNames.PrePlanningCreationPolicy)]
        [HttpPost]
        [Route("SendToApprove/{id}")]
        public async Task SendToApprove(int id)
        {
            await _prePlanningAppService.SendToApprove(id);
        }

        [Authorize(RoleNames.PrePlanningCreationPolicy)]
        [HttpPost]
        [Route("ReOpen/{id}")]
        public async Task ReOpen(int id)
        {
            await _prePlanningAppService.ReOpen(id);
        }


        [HttpPost]
        [Route("ReOpenForCancelation/{id}")]
        public async Task ReOpenForCancelation(int id)
        {
            await _prePlanningAppService.ReOpenForCancelation(id);
        }



        [Authorize(RoleNames.PrePlanningAuditingPolicy)]
        [HttpPost]
        [Route("Reject/{id}")]
        public async Task Reject(int id, [FromBody] string rejectionReason = "")
        {
            await _prePlanningAppService.Reject(id, rejectionReason);

        }

        [Authorize(RoleNames.PrePlanningAuditingPolicy)]
        [HttpPost]
        [Route("CreateVerificationCode/{planningId}")]
        public async Task CreateVerificationCode(int planningId)
        {
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            await _verification.CreateVerificationCode(planningId, userMobile, userEmail, User.UserId(), (int)Enums.VerificationType.PrePlanning);
        }

        [Authorize(RoleNames.PrePlanningAuditingPolicy)]
        [HttpPost]
        [Route("Approve/{id}/{verificationCode}")]
        public async Task Approve(int id, string verificationCode)
        {
            await _prePlanningAppService.Approve(id, verificationCode);
        }
    }
}
